using Microsoft.Data.SqlClient;
using Newtouch.Sett.API.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace Newtouch.Sett.API.Services
{
    /// <summary>
    /// T-24: api/Patient/OutPatientRegistrationQuery — danh sách đăng ký khám
    /// ngoại trú chờ khám, backed by Sett DB thật qua stored procedure gốc của
    /// vendor usp_interface_OutPatientRegistrationQuery (đã có sẵn trong DB
    /// Sett, cùng proc mà CIS Getdjzlist gọi trực tiếp). KHÔNG fake data:
    /// list trả về đúng các dòng mz_gh (jzbz = jiuzhenbz, mặc định "1" = chờ
    /// khám) join xt_brjbxx / xt_card / xt_brxz và view Base (tên khoa, tên
    /// bác sĩ), đúng cột mà CIS RegisteredInfoRespVO map (mzh, blh, brxm,
    /// sexValue, birth, brxzmc, brxz, zjlx, zjh, ksmc, ysxm, ghsj, mjzbz,
    /// operatingTime, ys, ks, ybjsh, fzbz, sbbh, cbdbm, py, kh, ContactNum,
    /// province, city, county, ADDRESS→address, nlshow, grbh, queno).
    ///
    /// Envelope/auth đúng mẫu các nhóm Sett-API khác: luôn HTTP 200 kèm
    /// ResponseBaseOld (10000/40004/20000), KHÔNG yêu cầu token (host này
    /// không có auth middleware; CIS có thể đính kèm Bearer app token và
    /// token đó được bỏ qua, như các route Booking/PatientAnon/OrderCenter).
    ///
    /// Lưu ý cố tình:
    /// - List RỖNG vẫn trả code 10000 với data = {list: [], pagination} (không
    ///   Fail): CIS chỉ kiểm tra apiResp.data == null để báo lỗi, nên Fail khi
    ///   0 dòng sẽ khiến lưới báo "Gọi giao diện thất bại" dù truy vấn thành
    ///   công. Lưới rỗng là kết quả đúng khi không có ca chờ khám khớp filter.
    /// - Proc KHÔNG BAO GIỜ set @flag/@msg (đo trực tiếp: cả hai NULL dù
    ///   @records = 9), nên endpoint không gate theo flag mà theo việc thực
    ///   thi proc thành công.
    /// - Proc tự nối @orgId/@ksCode/... vào SQL động (có qua
    ///   fn_SQLInjectionParamReplace bên trong proc) nên endpoint validate
    ///   whitelist các field đó trước khi gọi, để body caller không thành
    ///   vector injection qua proc.
    /// - @keyword của proc là mẫu LIKE (CIS Getdjzlist truyền '%%'): keyword
    ///   thường không có %/_ được bọc thành %keyword% để tìm theo tên/mzh/blh/py
    ///   như lưới CIS mong đợi; keyword đã chứa %/_ được giữ nguyên.
    /// - @zzhz proc nhận nhưng thân proc không dùng để lọc (đo từ sp_helptext)
    ///   — vẫn truyền qua đúng contract, không tự chế filter chuyển tuyến.
    /// - Proc tham chiếu NewtouchHIS_Base.. (view tên khoa/bác sĩ) nên khi chạy
    ///   staging nó đọc Base demo (chỉ đọc); prod đọc Base prod — đúng như
    ///   bản gốc, không tạo bản proc staging riêng để khỏi lệch vendor.
    /// - Giới hạn vendor: @ksCode của proc là VARCHAR(50) — danh sách khoa dài
    ///   hơn sẽ bị cắt ở tầng proc, giống hệt caller gốc.
    /// </summary>
    public class PatientQueryService
    {
        private readonly SettDb _db;

        public PatientQueryService(SettDb db) { _db = db; }

        public ResponseBaseOld Query(OutPatientRegistrationQueryRequest req)
        {
            if (req == null) return ResponseBaseOld.Fail("Request rỗng hoặc không hợp lệ");

            var org = string.IsNullOrWhiteSpace(req.OrganizeId) ? _db.TopOrganizeId : req.OrganizeId.Trim();
            if (string.IsNullOrWhiteSpace(org))
                return ResponseBaseOld.Fail("Thiếu OrganizeId (body) và Top_OrganizeId (cấu hình)");
            if (org.Length > 50 || !Regex.IsMatch(org, @"^[A-Za-z0-9\-]+$"))
                return ResponseBaseOld.Fail("OrganizeId không hợp lệ");

            var ksCode = (req.ksCode ?? "").Trim();
            var ysgh = (req.ysgh ?? "").Trim();
            var mjzbz = (req.mjzbz ?? "").Trim();
            var jiuzhenbz = string.IsNullOrWhiteSpace(req.jiuzhenbz) ? "1" : req.jiuzhenbz.Trim();
            if (!Regex.IsMatch(ksCode, @"^[A-Za-z0-9,\-_]*$") || !Regex.IsMatch(ysgh, @"^[A-Za-z0-9,\-_]*$"))
                return ResponseBaseOld.Fail("ksCode/ysgh không hợp lệ");
            if (!Regex.IsMatch(mjzbz, @"^[0-9]*$") || !Regex.IsMatch(jiuzhenbz, @"^[0-9]$"))
                return ResponseBaseOld.Fail("mjzbz/jiuzhenbz không hợp lệ");

            var keyword = (req.keyword ?? "").Trim();
            string keywordParam;
            if (string.IsNullOrEmpty(keyword)) keywordParam = "";
            else if (keyword.IndexOf('%') >= 0 || keyword.IndexOf('_') >= 0) keywordParam = keyword;
            else keywordParam = "%" + keyword + "%";

            var page = req.pagination?.page ?? req.page ?? 1;
            var rows = req.pagination?.rows ?? req.rows ?? 150;
            if (page <= 0) page = 1;
            if (rows <= 0) rows = 150;
            var sidx = string.IsNullOrWhiteSpace(req.pagination?.sidx) ? (req.sidx ?? "") : req.pagination.sidx;
            if (string.IsNullOrWhiteSpace(sidx)) sidx = "operatingTime";
            var sord = req.pagination?.sord ?? req.sord ?? "";

            try
            {
                using (var conn = _db.OpenSett())
                using (var cmd = new SqlCommand("dbo.usp_interface_OutPatientRegistrationQuery", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(SettDb.P("@orgId", org));
                    cmd.Parameters.Add(new SqlParameter("@lastUpdateTime", SqlDbType.DateTime) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@outpatientNumber", SqlDbType.VarChar, 50) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@ksCode", SqlDbType.VarChar, 50) { Value = (object)ksCode ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@ysgh", SqlDbType.VarChar, 50) { Value = (object)ysgh ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@mjzbz", SqlDbType.VarChar, 4) { Value = (object)mjzbz ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@jiuzhenbz", SqlDbType.VarChar, 1) { Value = (object)jiuzhenbz ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@keyword", SqlDbType.VarChar, 50) { Value = (object)keywordParam ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@zzhz", SqlDbType.VarChar, 10) { Value = (object)(req.zzhz ?? "0") ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@page", SqlDbType.Int) { Value = page });
                    cmd.Parameters.Add(new SqlParameter("@rows", SqlDbType.Int) { Value = rows });
                    cmd.Parameters.Add(new SqlParameter("@sidx", SqlDbType.VarChar, 50) { Value = sidx });
                    cmd.Parameters.Add(new SqlParameter("@sord", SqlDbType.VarChar, 50) { Value = sord });
                    var recordsOut = new SqlParameter("@records", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var flagOut = new SqlParameter("@flag", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output };
                    var msgOut = new SqlParameter("@msg", SqlDbType.VarChar, 128) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(recordsOut);
                    cmd.Parameters.Add(flagOut);
                    cmd.Parameters.Add(msgOut);

                    var list = SettDb.Read(cmd);
                    var records = recordsOut.Value == DBNull.Value || recordsOut.Value == null
                        ? list.Count
                        : Convert.ToInt32(recordsOut.Value);
                    var total = rows > 0 ? (records + rows - 1) / rows : 0;
                    var data = new
                    {
                        list = list,
                        pagination = new { page = page, rows = rows, sidx = sidx, sord = sord, records = records, total = total }
                    };
                    return ResponseBaseOld.Ok(data);
                }
            }
            catch (Exception ex)
            {
                return ResponseBaseOld.Error(ex.Message);
            }
        }
    }
}
