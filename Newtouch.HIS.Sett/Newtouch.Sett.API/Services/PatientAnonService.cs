using Microsoft.Data.SqlClient;
using Newtouch.Sett.API.Models;

namespace Newtouch.Sett.API.Services
{
    /// <summary>
    /// Nhóm B: api/PatientAnon/* (gốc PatientApiController, tất cả
    /// [IgnoreTokenDecrypt], methodcode = null).
    ///
    /// CẢNH BÁO TÁI CẤU TRÚC: implementation của IPatientCenterDmnService
    /// (OutPatReportList/InHosPatReportList/PatLisReportDetail/GetPatRegList/
    /// InHosPatientBasic) KHÔNG có trong checkout gốc — chỉ có interface và VO.
    /// Implementation nằm ở assembly nhị phân ngoài checkout. Các route dưới
    /// đây vì vậy được tái cấu trúc từ (a) tên route + request mà 8016 gửi,
    /// (b) schema DB thật đã kiểm tra trên demo, (c) câu SQL gốc còn lại trong
    /// checkout (GetHospGroupFeeVOList). Riêng LIS: nguồn duy nhất khớp hình
    /// báo cáo là Newtouch_Interface.Lis_Report_MZ/ZY (sqdh, ghnm/zyh, xmid,
    /// xmzwmc, jyjg, ckz...) — bảng đang RỖNG trên demo nên LIS chỉ kiểm chứng
    /// được negative test cho tới khi có dữ liệu LIS thật. Không bịa dòng LIS.
    /// </summary>
    public class PatientAnonService
    {
        private readonly SettDb _db;

        public PatientAnonService(SettDb db) { _db = db; }

        public ResponseBaseOld Dispatch(string action, BookingRequest req, ParaData para)
        {
            try
            {
                switch (action)
                {
                    case "OutpatRegRecord": return OutpatRegRecord(req, para);
                    case "InHosBillUnpaid": return InHosBillUnpaid(req, para);
                    case "InHosPatientQuery": return InHosPatientQuery(req, para);
                    case "PatientLisReport": return PatientLisReport(req, para);
                    case "PatientLisReportDetail": return PatientLisReportDetail(req, para);
                    default: return ResponseBaseOld.Fail("Action không hỗ trợ: " + action);
                }
            }
            catch (FailException ex) { return ResponseBaseOld.Fail(ex.Message, ex.SubCode); }
            catch (Exception ex) { return ResponseBaseOld.Error(ex.Message); }
        }

        // OutpRegQuery của 8016: cần kh hoặc mzh. Trả danh sách mz_gh (không
        // lọc ngày — khác Y027 vốn chỉ lấy đăng ký trong ngày).
        private ResponseBaseOld OutpatRegRecord(BookingRequest req, ParaData para)
        {
            var kh = para.Get("kh");
            var mzh = para.Get("mzh");
            if (string.IsNullOrWhiteSpace(kh) && string.IsNullOrWhiteSpace(mzh))
                return ResponseBaseOld.Fail("Thông tin quan trọng(kh/mzh) không được để trống tất cả");
            using (var conn = _db.OpenSett())
            {
                var sql = @"select ghnm, mzh, kh, patid, ks, ys, ghly, mjzbz, ghzt, ghf, zlf, ckf, gbf, xm, xb, blh,
convert(varchar(19), ghrq, 120) ghrq, convert(varchar(19), jsrq, 120) jsrq, ScheduId, brxz
from mz_gh with(nolock) where OrganizeId=@org and zt='1'";
                var ps = new List<SqlParameter> { SettDb.P("@org", req.OrganizeId) };
                if (!string.IsNullOrWhiteSpace(kh)) { sql += " and kh=@kh"; ps.Add(SettDb.P("@kh", kh)); }
                if (!string.IsNullOrWhiteSpace(mzh)) { sql += " and mzh=@mzh"; ps.Add(SettDb.P("@mzh", mzh)); }
                sql += " order by ghrq desc";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    var rows = SettDb.Read(cmd);
                    if (rows.Count == 0) return ResponseBaseOld.Fail("Không tìm thấy bản ghi liên quan");
                    return ResponseBaseOld.Ok(rows);
                }
            }
        }

        // InHosPatientQuery của 8016: SearchPatientRequest + ywlx="zy".
        private ResponseBaseOld InHosPatientQuery(BookingRequest req, ParaData para)
        {
            var kh = para.Get("kh");
            var zjh = para.Get("zjh");
            var xm = para.Get("xm");
            var zyh = para.Get("zyh");
            using (var conn = _db.OpenSett())
            {
                var sql = _db.Q(@"select a.zyh, a.xm, a.xb, a.zybz, b.brxzmc, a.kh, a.nlshow nl, c.cwmc cw,
convert(varchar(19), a.ryrq, 120) ryrq, convert(varchar(19), a.cyrq, 120) cyrq, d.bqmc bqmc,
case when a.cyrq is null then datediff(day, a.ryrq, getdate()) else datediff(day, a.ryrq, a.cyrq) end zyts
from zy_brjbxx a with(nolock)
left join xt_brxz b with(nolock) on a.OrganizeId=b.OrganizeId and a.brxz=b.brxz and b.zt='1'
left join [NewtouchHIS_Base].dbo.xt_cw c with(nolock) on a.OrganizeId=c.OrganizeId and a.cw=c.cwCode and c.zt='1'
left join [NewtouchHIS_Base].dbo.xt_bq d with(nolock) on a.OrganizeId=d.OrganizeId and a.bq=d.bqCode and d.zt='1'
where a.OrganizeId=@org and a.zt='1'");
                var ps = new List<SqlParameter> { SettDb.P("@org", req.OrganizeId) };
                if (!string.IsNullOrWhiteSpace(kh)) { sql += " and a.kh=@kh"; ps.Add(SettDb.P("@kh", kh)); }
                if (!string.IsNullOrWhiteSpace(zjh)) { sql += " and a.zjh=@zjh"; ps.Add(SettDb.P("@zjh", zjh)); }
                if (!string.IsNullOrWhiteSpace(xm)) { sql += " and a.xm=@xm"; ps.Add(SettDb.P("@xm", xm)); }
                if (!string.IsNullOrWhiteSpace(zyh)) { sql += " and a.zyh=@zyh"; ps.Add(SettDb.P("@zyh", zyh)); }
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    var rows = SettDb.Read(cmd);
                    if (rows.Count == 0) return ResponseBaseOld.Fail("Không tìm thấy bản ghi liên quan");
                    return ResponseBaseOld.Ok(rows);
                }
            }
        }

        // InHosFeeTypeBillUnpaid của 8016: cần zyh hoặc kh hoặc zjh.
        // Gốc: InHosPatientBasic -> yêu cầu zybz = Djz(2) hoặc Ycy(3), rồi
        // GetHospGroupFeeVOList(zyh, orgId, "2"). Gốc trả SUCCESS cả khi list rỗng.
        private ResponseBaseOld InHosBillUnpaid(BookingRequest req, ParaData para)
        {
            var zyh = para.Get("zyh");
            var kh = para.Get("kh");
            var zjh = para.Get("zjh");
            if (string.IsNullOrWhiteSpace(zyh) && string.IsNullOrWhiteSpace(kh) && string.IsNullOrWhiteSpace(zjh))
                return ResponseBaseOld.Fail("Thông tin quan trọng không được để trống");
            using (var conn = _db.OpenSett())
            {
                var sql = "select top 1 zyh, zybz from zy_brjbxx with(nolock) where OrganizeId=@org and zt='1'";
                var ps = new List<SqlParameter> { SettDb.P("@org", req.OrganizeId) };
                if (!string.IsNullOrWhiteSpace(zyh)) { sql += " and zyh=@zyh"; ps.Add(SettDb.P("@zyh", zyh)); }
                if (!string.IsNullOrWhiteSpace(kh)) { sql += " and kh=@kh"; ps.Add(SettDb.P("@kh", kh)); }
                if (!string.IsNullOrWhiteSpace(zjh)) { sql += " and zjh=@zjh"; ps.Add(SettDb.P("@zjh", zjh)); }
                Dictionary<string, object> pat;
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    pat = SettDb.ReadOne(cmd);
                }
                if (pat == null) return ResponseBaseOld.Fail("Không tìm thấy bệnh nhân nội trú");
                var zybz = SettDb.Str(pat, "zybz");
                if (zybz != SettConstants.ZybzDjz && zybz != SettConstants.ZybzYcy)
                    return ResponseBaseOld.Fail("Bệnh nhân chưa tới trạng thái quyết toán (cần đang quyết toán hoặc đã xuất viện)");
                var realZyh = SettDb.Str(pat, "zyh");
                // Câu SQL gốc GetHospGroupFeeVOList ver="2" (gộp je, 2 chữ số thập phân).
                var feeSql = _db.Q(@"select dljf.dl dlCode, sfdl.dlmc, dljf.je from (
select dl, sum(je) je from(
select dl, CONVERT(NUMERIC(10,2), isnull(je, 0)) je from [V_C_Sys_WsfZyXmjfb]
where zyh = @zyh and OrganizeId = @orgId and zt= '1'
union all
select dl, CONVERT(NUMERIC(10,2), isnull(je, 0) ) je from [V_C_Sys_WsfZyYpjfb]
where zyh = @zyh and OrganizeId = @orgId and zt= '1'
) jfmx
group by dl
) as dljf
left join [NewtouchHIS_Base]..V_S_xt_sfdl sfdl
on sfdl.dlCode = dljf.dl and sfdl.OrganizeId = @orgId");
                using (var cmd = new SqlCommand(feeSql, conn))
                {
                    cmd.Parameters.Add(SettDb.P("@orgId", req.OrganizeId));
                    cmd.Parameters.Add(SettDb.P("@zyh", realZyh));
                    return ResponseBaseOld.Ok(SettDb.Read(cmd));
                }
            }
        }

        // PatientLisReport: 8016 set ywlx="1" (mz) hoặc "2" (zy), sqdzt="ywc".
        // Bảng Interface không có cột sqdzt/mzh/kh nên không lọc theo sqdzt;
        // MZ tra qua mz_gh.mzh -> ghnm, ZY dùng trực tiếp zyh (hoặc qua kh).
        private ResponseBaseOld PatientLisReport(BookingRequest req, ParaData para)
        {
            var ywlx = para.Get("ywlx");
            using (var conn = _db.OpenSett())
            {
                if (ywlx == "2")
                {
                    var zyh = para.Get("zyh");
                    if (string.IsNullOrWhiteSpace(zyh))
                    {
                        var kh = para.Get("kh");
                        if (string.IsNullOrWhiteSpace(kh)) return ResponseBaseOld.Fail("Thông tin quan trọng(Số nhập viện/Số thẻ) không được để trống");
                        using (var cmd = new SqlCommand("select top 1 zyh from zy_brjbxx with(nolock) where OrganizeId=@org and kh=@kh and zt='1' order by ryrq desc", conn))
                        {
                            cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                            cmd.Parameters.Add(SettDb.P("@kh", kh));
                            var r = SettDb.ReadOne(cmd);
                            if (r == null) return ResponseBaseOld.Fail("Không tìm thấy bệnh nhân nội trú");
                            zyh = SettDb.Str(r, "zyh");
                        }
                    }
                    using (var cmd = new SqlCommand(_db.Q("select * from Newtouch_Interface..Lis_Report_ZY with(nolock) where zyh=@zyh and OrganizeId=@org order by sqdh, xh"), conn))
                    {
                        cmd.Parameters.Add(SettDb.P("@zyh", zyh));
                        cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                        var rows = SettDb.Read(cmd);
                        if (rows.Count == 0) return ResponseBaseOld.Fail("Không tìm thấy báo cáo xét nghiệm");
                        return ResponseBaseOld.Ok(rows);
                    }
                }
                // Mặc định ngoại trú (ywlx "1" hoặc rỗng).
                var mzh = para.Get("mzh");
                string ghnm = null;
                if (!string.IsNullOrWhiteSpace(mzh))
                {
                    using (var cmd = new SqlCommand("select top 1 ghnm from mz_gh with(nolock) where OrganizeId=@org and mzh=@mzh and zt='1'", conn))
                    {
                        cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                        cmd.Parameters.Add(SettDb.P("@mzh", mzh));
                        var r = SettDb.ReadOne(cmd);
                        if (r == null) return ResponseBaseOld.Fail("Không tìm thấy lần khám ngoại trú");
                        ghnm = SettDb.Str(r, "ghnm");
                    }
                }
                else
                {
                    var kh = para.Get("kh");
                    if (string.IsNullOrWhiteSpace(kh)) return ResponseBaseOld.Fail("Thông tin quan trọng(Số ngoại trú/Số thẻ) không được để trống");
                    using (var cmd = new SqlCommand("select top 1 ghnm from mz_gh with(nolock) where OrganizeId=@org and kh=@kh and zt='1' order by ghrq desc", conn))
                    {
                        cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                        cmd.Parameters.Add(SettDb.P("@kh", kh));
                        var r = SettDb.ReadOne(cmd);
                        if (r == null) return ResponseBaseOld.Fail("Không tìm thấy lần khám ngoại trú");
                        ghnm = SettDb.Str(r, "ghnm");
                    }
                }
                using (var cmd = new SqlCommand(_db.Q("select * from Newtouch_Interface..Lis_Report_MZ with(nolock) where ghnm=@ghnm and OrganizeId=@org order by sqdh, xh"), conn))
                {
                    cmd.Parameters.Add(SettDb.P("@ghnm", ghnm));
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    var rows = SettDb.Read(cmd);
                    if (rows.Count == 0) return ResponseBaseOld.Fail("Không tìm thấy báo cáo xét nghiệm");
                    return ResponseBaseOld.Ok(rows);
                }
            }
        }

        private ResponseBaseOld PatientLisReportDetail(BookingRequest req, ParaData para)
        {
            var sqdh = para.Get("sqdh");
            var ywlx = para.Get("ywlx");
            if (string.IsNullOrWhiteSpace(sqdh) || string.IsNullOrWhiteSpace(ywlx))
                return ResponseBaseOld.Fail("Thông tin quan trọng(sqdh,ywlx) không được để trống");
            using (var conn = _db.OpenSett())
            {
                var table = ywlx == "2" ? "Lis_Report_ZY" : "Lis_Report_MZ";
                using (var cmd = new SqlCommand(_db.Q("select * from Newtouch_Interface.." + table + " with(nolock) where sqdh=@sqdh and OrganizeId=@org order by xh"), conn))
                {
                    cmd.Parameters.Add(SettDb.P("@sqdh", sqdh));
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    var rows = SettDb.Read(cmd);
                    if (rows.Count == 0) return ResponseBaseOld.Fail("Không tìm thấy chi tiết báo cáo xét nghiệm");
                    return ResponseBaseOld.Ok(rows);
                }
            }
        }
    }
}
