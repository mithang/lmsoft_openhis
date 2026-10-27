using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtouch.Sett.API.Models;

namespace Newtouch.Sett.API.Services
{
    /// <summary>
    /// Nhóm A: api/OutpBook/BookingESB — port từ Sett.API gốc
    /// (Controllers/Booking/BookingController.cs, route gốc api/Booking/BookingESB).
    /// Dispatch theo methodcode trong body; optype chỉ có ý nghĩa với Y004
    /// (0 = tra cứu, 1 = thêm mới) và Y009 (1 = phân trang).
    ///
    /// Bảng mã ĐÃ ĐỐI CHIẾU đủ mọi BuildBookRequest trong 8016:
    /// - Có caller 8016, triển khai: Y002, Y003, Y004 (optype 0/1), Y009 (optype
    ///   null/1), Y021, Y022, Y023, Y024 (optype 0), Y026, Y027, Y028, Y029.
    /// - Có trong switch gốc nhưng KHÔNG có caller 8016, KHÔNG triển khai:
    ///   Y005, Y006, Y007, Y008, Y010, Y011, Y012, Y025, Y030.
    /// - Y004 optype 3 (đăng ký đồng bộ, chỉ thấy trong code gốc, không có
    ///   caller 8016): KHÔNG triển khai, trả FAIL rõ ràng.
    /// - Y027/Y028/Y029: CÓ caller 8016 nhưng KHÔNG CÓ case trong switch gốc
    ///   (bản gốc trả về response rỗng vì không khớp case nào) — triển khai
    ///   dạng tái cấu trúc từ các truy vấn gốc tương đương (xem từng hàm).
    /// </summary>
    public partial class BookingService
    {
        private readonly SettDb _db;

        public BookingService(SettDb db)
        {
            _db = db;
        }

        public ResponseBaseOld Dispatch(BookingRequest req)
        {
            if (req == null) return ResponseBaseOld.Fail("Vui lòng kiểm tra định dạng tham số có đúng không");
            if (string.IsNullOrWhiteSpace(req.OrganizeId) || string.IsNullOrWhiteSpace(req.methodcode))
                return ResponseBaseOld.Fail("Tham số quan trọng không được để trống");
            var para = new ParaData(req.paradata);
            try
            {
                switch (req.methodcode)
                {
                    case "Y002": return DeptInfo(req, para);
                    case "Y003": return StaffInfo(req, para);
                    case "Y004": return PatInfoDispatch(req, para);
                    case "Y009": return Schedule(req, para);
                    case "Y021": return BookRecord(req, para);
                    case "Y022": return BookApply(req, para);
                    case "Y023": return BookCancel(req, para);
                    case "Y024": return BookPay(req, para);
                    case "Y026": return CommonRegist(req, para);
                    case "Y027": return RegTodayQuery(req, para);
                    case "Y028": return BillUnpaidByMzh(req, para);
                    case "Y029": return BillItemDetail(req, para);
                    default:
                        return ResponseBaseOld.Fail("Mã phương thức " + req.methodcode + " không có caller 8016 nên chưa triển khai (xem REPORT.md)");
                }
            }
            catch (FailException ex)
            {
                return ResponseBaseOld.Fail(ex.Message, ex.SubCode);
            }
            catch (Exception ex)
            {
                return ResponseBaseOld.Error(ex.Message);
            }
        }

        // ---------------- Y002: danh sách khoa (BookingDmnService.GetDepartmentInfo) ----------------
        private ResponseBaseOld DeptInfo(BookingRequest req, ParaData para)
        {
            using (var conn = _db.OpenSett())
            {
                var sql = @"select OrganizeId, Name as ksmc, Code as ks, mzzybz from [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] where 1=1 and zt='1' and organizeId=@orgId";
                var ps = new List<SqlParameter> { SettDb.P("@orgId", req.OrganizeId) };
                if (!string.IsNullOrEmpty(para.Get("ks"))) { sql += " and code=@code "; ps.Add(SettDb.P("@code", para.Get("ks"))); }
                if (!string.IsNullOrEmpty(para.Get("ksmc"))) { sql += " and name=@name "; ps.Add(SettDb.P("@name", para.Get("ksmc"))); }
                using (var cmd = new SqlCommand(_db.Q(sql), conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    var list = SettDb.Read(cmd);
                    if (list.Count > 0) return ResponseBaseOld.Ok(list);
                    return ResponseBaseOld.Fail("Không tìm thấy bản ghi liên quan");
                }
            }
        }

        // ---------------- Y003: danh sách bác sĩ (GetStaffInfo) ----------------
        // QUIRK GỐC ĐƯỢC GIỮ NGUYÊN: chỉ nhân viên có Gender=true mới được đưa
        // vào kết quả (vòng if Gender==true trong SysStaffInfo gốc) — nhân viên
        // Gender=false/null bị lược bỏ. Đây là hành vi gốc, cần quyết định sửa
        // riêng (xem REPORT.md); host không tự ý đổi để khỏi lệch contract.
        private ResponseBaseOld StaffInfo(BookingRequest req, ParaData para)
        {
            using (var conn = _db.OpenSett())
            {
                var sql = @"select staff.organizeId, departmentCode, department.Name as departmentName, staff.Name as staffName, Gender, gh, DutyId, duty.Name as dutyName
from [NewtouchHIS_Base].[dbo].[Sys_Staff] staff
left join [NewtouchHIS_Base].[dbo].[Sys_Department] department on department.code=staff.departmentCode
left join [NewtouchHIS_Base].[dbo].[Sys_StaffDuty] staffduty on staff.id=staffduty.staffId
left join [NewtouchHIS_Base].[dbo].[Sys_Duty] duty on duty.id=staffduty.dutyId
where 1=1 and staff.zt='1' and staff.organizeId=@orgId";
                var ps = new List<SqlParameter> { SettDb.P("@orgId", req.OrganizeId) };
                if (!string.IsNullOrEmpty(para.Get("ys"))) { sql += " and gh=@gh "; ps.Add(SettDb.P("@gh", para.Get("ys"))); }
                if (!string.IsNullOrEmpty(para.Get("xm"))) { sql += " and staff.Name=@staffName "; ps.Add(SettDb.P("@staffName", para.Get("xm"))); }
                using (var cmd = new SqlCommand(_db.Q(sql), conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    var rows = SettDb.Read(cmd);
                    var list = new List<object>();
                    foreach (var r in rows)
                    {
                        object g;
                        if (!r.TryGetValue("Gender", out g) || g == null || !Convert.ToBoolean(g)) continue;
                        list.Add(new
                        {
                            departmentCode = SettDb.Str(r, "departmentCode"),
                            departmentName = SettDb.Str(r, "departmentName"),
                            DutyId = r["DutyId"],
                            dutyName = SettDb.Str(r, "dutyName"),
                            Gender = "Nam",
                            gh = SettDb.Str(r, "gh"),
                            organizeId = SettDb.Str(r, "organizeId"),
                            staffName = SettDb.Str(r, "staffName")
                        });
                    }
                    if (list.Count > 0) return ResponseBaseOld.Ok(list);
                    return ResponseBaseOld.Fail("Không tìm thấy bản ghi liên quan");
                }
            }
        }

        // ---------------- Y004 ----------------
        private ResponseBaseOld PatInfoDispatch(BookingRequest req, ParaData para)
        {
            if (req.optype == "1")
            {
                if (para.Has("zjh") && para.Has("xm") && para.Has("xb") && para.Has("csrq")) return PatInfoSet(req, para);
                return ResponseBaseOld.Fail("Tham số quan trọng không được để trống");
            }
            if (req.optype == "3")
                return ResponseBaseOld.Fail("Y004 optype 3 (đăng ký đồng bộ) không có caller 8016 nên chưa triển khai");
            if (para.Get("xm") == null || string.IsNullOrWhiteSpace(para.Get("xm")))
                return ResponseBaseOld.Fail("Tham số quan trọng không được để trống");
            if (!((para.Has("zjh") && !string.IsNullOrWhiteSpace(para.Get("zjh")))
                || (para.Has("kh") && !string.IsNullOrWhiteSpace(para.Get("kh")))))
                return ResponseBaseOld.Fail("Tham số quan trọng không được để trống");
            using (var conn = _db.OpenSett())
            {
                var list = GetPatInfo(conn, null, req.OrganizeId, para.Get("kh"), para.Get("zjh"), para.Get("xm"));
                if (list != null && list.Count > 0) return ResponseBaseOld.Ok(list, "Tra cứu thành công");
                return ResponseBaseOld.Fail("Thông tin bệnh nhân không tồn tại");
            }
        }

        /// <summary>
        /// GetPatInfo gốc, THÍCH ỨNG schema DB demo: xt_brjbxx demo không có cột
        /// dz và brxz (brxz nằm ở xt_card) nên join xt_brxz theo b.brxz và lấy
        /// dz từ xian_dz. Các cột còn lại giữ nguyên câu SQL gốc.
        /// </summary>
        public static List<Dictionary<string, object>> GetPatInfo(SqlConnection conn, SqlTransaction tx, string orgId, string kh, string zjh, string xm)
        {
            var sql = @"select a.organizeId, a.patid, c.brxzmc brxz, a.blh, a.xm, a.xb, a.csny, a.zjh, a.dh, a.xian_dz dz, a.mz, a.zy, b.CardNo kh, b.CardTypeName klx
from xt_brjbxx a with(nolock)
left join xt_card b with(nolock) on a.organizeId=b.organizeid and a.patid=b.patid and b.zt='1'
left join dbo.xt_brxz c with(nolock) on b.organizeId=c.organizeid and b.brxz=c.brxz and c.zt='1'
where a.zt='1' and a.organizeId=@orgId";
            var ps = new List<SqlParameter> { SettDb.P("@orgId", orgId) };
            if (!string.IsNullOrEmpty(kh)) { sql += " and b.CardNo=@kh "; ps.Add(SettDb.P("@kh", kh)); }
            if (!string.IsNullOrEmpty(zjh)) { sql += " and a.zjh=@zjh "; ps.Add(SettDb.P("@zjh", zjh)); }
            if (!string.IsNullOrEmpty(xm)) { sql += " and a.xm=@xm "; ps.Add(SettDb.P("@xm", xm)); }
            sql += " group by a.organizeId, a.patid, c.brxzmc, a.blh, a.xm, a.xb, a.csny, a.zjh, a.dh, a.xian_dz, a.mz, a.zy, b.CardNo, b.CardTypeName";
            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddRange(ps.ToArray());
                return SettDb.Read(cmd);
            }
        }

        // Y004 optype 1: thêm mới bệnh nhân (SysPatInfoSet + SavePatBasicCardInfo, nhánh tạo mới).
        private ResponseBaseOld PatInfoSet(BookingRequest req, ParaData para)
        {
            var zjh = para.Get("zjh");
            if (!CheckIdCard(zjh)) return ResponseBaseOld.Fail("Định dạng giấy tờ không đúng chuẩn", "10002");
            using (var conn = _db.OpenSett())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    Dictionary<string, object> dup;
                    using (var cmd = new SqlCommand("select top 1 patid from xt_brjbxx with(nolock) where zjlx='1' and zjh=@zjh and OrganizeId=@org and zt='1'", conn, tx))
                    {
                        cmd.Parameters.Add(SettDb.P("@zjh", zjh));
                        cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                        dup = SettDb.ReadOne(cmd);
                    }
                    if (dup != null) return ResponseBaseOld.Fail("Thông tin giấy tờ đã tồn tại", "10001");
                    var xb = para.Get("xb");
                    if (xb != "1" && xb != "2") return ResponseBaseOld.Fail("Tham số từ điển không đúng chuẩn", "10003");
                    var kh = _db.GetFieldNewId(conn, tx, req.OrganizeId, "xt_card", "");
                    var blh = _db.GetFieldNewId(conn, tx, req.OrganizeId, "xt_blh", "{0:D5}");
                    // Tính chất bệnh nhân "Tự chi trả" (bản gốc tìm theo tên "自费",
                    // đã dịch thành "Tu chi tra" trong đợt dịch DB); fallback brxz '0'.
                    string brxz = "0";
                    using (var cmd = new SqlCommand("select top 1 brxz from xt_brxz with(nolock) where OrganizeId=@org and zt='1' and brxzmc=@mc", conn, tx))
                    {
                        cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                        cmd.Parameters.Add(SettDb.P("@mc", "Tu chi tra"));
                        var r = SettDb.ReadOne(cmd);
                        if (r != null) brxz = SettDb.Str(r, "brxz");
                    }
                    // cardtype theo brxz: "2" -> YBJYK, "3" -> XNHJYK, còn lại XNK (thẻ ảo, mã '1')
                    string cardType = "1";
                    string cardTypeName = "The ao";
                    if (brxz == "2") { cardType = "0"; cardTypeName = "The BHYT"; }
                    var csrq = para.Get("csrq");
                    DateTime csny;
                    var idBirth = BirthFromId(zjh);
                    if (idBirth.HasValue) csny = idBirth.Value;
                    else if (!DateTime.TryParse(csrq, out csny)) return ResponseBaseOld.Fail("Ngày sinh không hợp lệ");
                    int patid = SettDb.GetNewId(conn, tx, "xt_brjbxx");
                    using (var cmd = new SqlCommand(@"insert into xt_brjbxx (patid, OrganizeId, blh, xm, py, xb, csny, zjlx, zjh, dh, zt, CreatorCode, CreateTime)
values (@patid, @org, @blh, @xm, @py, @xb, @csny, '1', @zjh, @dh, '1', @user, getdate())", conn, tx))
                    {
                        cmd.Parameters.Add(SettDb.P("@patid", patid));
                        cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                        cmd.Parameters.Add(SettDb.P("@blh", blh));
                        cmd.Parameters.Add(SettDb.P("@xm", para.Get("xm")));
                        cmd.Parameters.Add(SettDb.P("@py", para.Get("xm")));
                        cmd.Parameters.Add(SettDb.P("@xb", xb));
                        cmd.Parameters.Add(SettDb.P("@csny", csny));
                        cmd.Parameters.Add(SettDb.P("@zjh", zjh));
                        cmd.Parameters.Add(SettDb.P("@dh", para.Get("dh")));
                        cmd.Parameters.Add(SettDb.P("@user", SettConstants.ApiUser));
                        cmd.ExecuteNonQuery();
                    }
                    using (var cmd = new SqlCommand(@"insert into xt_card (CardId, OrganizeId, CardNo, CardType, CardTypeName, brxz, hzxm, patid, zt, CreatorCode, CreateTime)
values (@id, @org, @kh, @ct, @ctn, @brxz, @xm, @patid, '1', @user, getdate())", conn, tx))
                    {
                        cmd.Parameters.Add(SettDb.P("@id", Guid.NewGuid().ToString()));
                        cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                        cmd.Parameters.Add(SettDb.P("@kh", kh));
                        cmd.Parameters.Add(SettDb.P("@ct", cardType));
                        cmd.Parameters.Add(SettDb.P("@ctn", cardTypeName));
                        cmd.Parameters.Add(SettDb.P("@brxz", brxz));
                        cmd.Parameters.Add(SettDb.P("@xm", para.Get("xm")));
                        cmd.Parameters.Add(SettDb.P("@patid", patid));
                        cmd.Parameters.Add(SettDb.P("@user", SettConstants.ApiUser));
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                    var list = GetPatInfo(conn, null, req.OrganizeId, kh, zjh, para.Get("xm"));
                    return ResponseBaseOld.Ok(list, "Thêm thông tin bệnh nhân thành công");
                }
                catch (Exception)
                {
                    try { tx.Rollback(); } catch { }
                    throw;
                }
            }
        }

        /// <summary>CheckIDCard gốc (CommmHelper): 18 số GB11643-1999 hoặc 15 số.</summary>
        public static bool CheckIdCard(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
            if (id.Length == 18) return CheckIdCard18(id);
            if (id.Length == 15) return CheckIdCard15(id);
            return false;
        }

        private static bool CheckIdCard18(string id)
        {
            long n;
            if (!long.TryParse(id.Remove(17), out n) || n < Math.Pow(10, 16) || !long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n)) return false;
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2)) == -1) return false;
            var birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime t;
            if (!DateTime.TryParse(birth, out t)) return false;
            var arrVarifyCode = "1,0,x,9,8,7,6,5,4,3,2".Split(',');
            var wi = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(',');
            var ai = id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++) sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            int y;
            Math.DivRem(sum, 11, out y);
            return arrVarifyCode[y] == id.Substring(17, 1).ToLower();
        }

        private static bool CheckIdCard15(string id)
        {
            long n;
            if (!long.TryParse(id, out n) || n < Math.Pow(10, 14)) return false;
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2)) == -1) return false;
            var birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime t;
            return DateTime.TryParse(birth, out t);
        }

        /// <summary>Ngày sinh suy từ CMND (GetBirthdayAdnSix): 18 số lấy yyyyMMdd
        /// ở vị trí 6-13; 15 số lấy 19 + yyMMdd. SavePatBasicCardInfo gốc dùng
        /// ngày này thay cho csrq khi zjlx là CMND.</summary>
        private static DateTime? BirthFromId(string id)
        {
            try
            {
                if (id.Length == 18) return DateTime.ParseExact(id.Substring(6, 8), "yyyyMMdd", null);
                if (id.Length == 15) return DateTime.ParseExact("19" + id.Substring(6, 6), "yyyyMMdd", null);
            }
            catch { }
            return null;
        }

        // ---------------- Y009: lịch trực (GetMzpbSchedule) ----------------
        // THÍCH ỨNG: 8016 gửi ksrq/jsrq (khoảng ngày), ysgh, ScheduId, RegType
        // (hoa đầu), trong khi handler gốc đọc outDate/FromoutDate, ys, scheduId,
        // regType (thường đầu) qua Dictionary case-sensitive nên các filter đó
        // bị bỏ qua lặng lẽ. Host đọc case-insensitive và map ksrq -> từ ngày,
        // jsrq -> đến ngày, outDate -> đúng ngày, FromoutDate -> từ ngày.
        public static List<Dictionary<string, object>> GetSchedule(SqlConnection conn, SqlTransaction tx, string orgId,
            string outDate, string fromDate, string toDate, string ks, string ysgh, string regType, string ghpbId, string scheduId)
        {
            var sql = @"select convert(varchar(20),ScheduId) ScheduId, a.[OrganizeId], convert(varchar(10),[OutDate],120) [OutDate], [ysgh],
[ysxm], [czks], [czksmc], [RegType], [Title], [Period], [PeriodDesc], [TotalNum]
, [PeriodStart], [PeriodEnd], [RegFee], [IsCancel], [CancelReason], [CancelTime],
YYNum, ghpbid, IsBook, [ghlx], [zlxm], weekdd, d.sfxmmc ghxmmc, e.sfxmmc zlxmmc
from mz_ghpb_schedule a with(nolock)
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] d with(nolock) on a.organizeid = d.organizeid and d.zt='1' and a.ghlx = d.sfxmcode
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] e with(nolock) on a.organizeid = e.organizeid and e.zt='1' and a.zlxm = e.sfxmcode
where a.[OrganizeId]=@orgId and a.zt='1' and OutDate>=@limitdate and IsBook='1' and IsCancel='0' and IsBook=1 ";
            var ps = new List<SqlParameter>
            {
                SettDb.P("@orgId", orgId),
                SettDb.P("@limitdate", DateTime.Now.ToString("yyyy-MM-dd"))
            };
            if (!string.IsNullOrWhiteSpace(scheduId)) { sql += " and ScheduId=@Id"; ps.Add(SettDb.P("@Id", Convert.ToDecimal(scheduId))); }
            if (!string.IsNullOrWhiteSpace(outDate)) { sql += " and OutDate=@date"; ps.Add(SettDb.P("@date", Convert.ToDateTime(outDate))); }
            if (!string.IsNullOrWhiteSpace(fromDate)) { sql += " and OutDate>=@datefrom"; ps.Add(SettDb.P("@datefrom", Convert.ToDateTime(fromDate))); }
            if (!string.IsNullOrWhiteSpace(toDate)) { sql += " and OutDate<=@dateto"; ps.Add(SettDb.P("@dateto", Convert.ToDateTime(toDate))); }
            if (!string.IsNullOrWhiteSpace(ks)) { sql += " and czks=@czks"; ps.Add(SettDb.P("@czks", ks)); }
            if (!string.IsNullOrWhiteSpace(ysgh)) { sql += " and ysgh=@ysgh"; ps.Add(SettDb.P("@ysgh", ysgh)); }
            if (!string.IsNullOrWhiteSpace(regType)) { sql += " and RegType=@RegType"; ps.Add(SettDb.P("@RegType", regType)); }
            if (!string.IsNullOrWhiteSpace(ghpbId)) { sql += " and ghpbid=@ghpbid"; ps.Add(SettDb.P("@ghpbid", ghpbId)); }
            // NOTE: câu SQL gốc join sfxm thứ hai dùng d.zt='1' (lỗi copy-paste gốc:
            // điều kiện zt của bảng e); giữ nguyên để khỏi lệch kết quả.
            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddRange(ps.ToArray());
                return SettDb.Read(cmd);
            }
        }

        private ResponseBaseOld Schedule(BookingRequest req, ParaData para)
        {
            using (var conn = _db.OpenSett())
            {
                var list = GetSchedule(conn, null, req.OrganizeId,
                    para.Get("outDate"), para.Get("FromoutDate") ?? para.Get("ksrq"), para.Get("jsrq"),
                    para.Get("ks"), para.Get("ys") ?? para.Get("ysgh"), para.Get("regType") ?? para.Get("RegType"),
                    para.Get("ghpbId"), para.Get("scheduId") ?? para.Get("ScheduId"));
                if (list.Count == 0) return ResponseBaseOld.Fail("Không tìm thấy bản ghi liên quan");
                if (req.optype == "1")
                {
                    // Phân trang phía server: paradata.pagination là chuỗi JSON
                    // {page, rows, sidx, records, sord} (8016 GetOutSchedulePage).
                    // sub_msg mang Pagination đã cập nhật records = tổng số, để
                    // BuildResponseBase(resp, pagination) của 8016 đọc lại.
                    int page = 1, rows = 20;
                    string sidx = "", sord = "";
                    var pagRaw = para.Get("pagination");
                    if (!string.IsNullOrWhiteSpace(pagRaw))
                    {
                        try
                        {
                            var jo = JObject.Parse(pagRaw);
                            if (jo["page"] != null) page = jo["page"].Value<int>();
                            if (jo["rows"] != null) rows = jo["rows"].Value<int>();
                            if (jo["sidx"] != null) sidx = jo["sidx"].Value<string>();
                            if (jo["sord"] != null) sord = jo["sord"].Value<string>();
                        }
                        catch { }
                    }
                    if (page < 1) page = 1;
                    if (rows < 1) rows = 20;
                    var total = list.Count;
                    var slice = list.Skip((page - 1) * rows).Take(rows).ToList();
                    var resp = ResponseBaseOld.Ok(slice);
                    resp.sub_msg = JsonConvert.SerializeObject(new { page = page, rows = rows, sidx = sidx, sord = sord, records = total });
                    return resp;
                }
                return ResponseBaseOld.Ok(list);
            }
        }

        // ---------------- Y021: bản ghi đặt lịch (OutbookRecord) ----------------
        // SỬA 2 LỖI GỐC (ghi rõ, không lặng lẽ): (1) SQL gốc không lọc OrganizeId
        // dù đã nhận tham số — host thêm điều kiện a.OrganizeId=@orgId; (2) nhánh
        // lọc ysgh của gốc bind nhầm tham số @ks — host bind đúng @ys.
        private ResponseBaseOld BookRecord(BookingRequest req, ParaData para)
        {
            using (var conn = _db.OpenSett())
            {
                var list = BookRecordQuery(conn, null, req.OrganizeId, para, req.AppId);
                if (list.Count > 0) return ResponseBaseOld.Ok(list);
                return ResponseBaseOld.Fail("Không tìm thấy bản ghi đặt hẹn");
            }
        }

        public static List<Dictionary<string, object>> BookRecordQuery(SqlConnection conn, SqlTransaction tx, string orgId, ParaData para, string appId)
        {
            var sql = @"SELECT a.[OrganizeId], [BookId],
(case [yyzt] when 1 then (case when datediff(dd,getdate(),a.outdate)>=0 then 'Đã đặt hẹn' else 'Đã hết hạn' end)
when 2 then 'Đã đăng ký' when 3 then 'Đã hủy đặt hẹn' when 4 then 'Đã hết hạn' end) yyzt,
[ScheduId], b.brxzmc [ghxz], [patid], [xm], (case when [xb]=1 then 'Nam' else 'Nữ' end) [xb], [lxdh], [kh],
c.name [ks], d.name [ys], [OutDate]
, e.name [RegType], [Period], [PeriodStart], [PeriodEnd], [QueueNo], [RegFee], [PayFee], [PayLsh], [PayTime]
, [CancelReason], [CancelTime], [mzh], [ghrq], [AppId], yynr
from [mz_gh_book] a with(nolock)
left join xt_brxz b with(nolock) on a.organizeid=b.organizeid and a.ghxz=b.brxz
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] c on a.organizeid=c.organizeid and a.ks=c.code and c.zt='1'
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] d on a.ys=d.gh and a.organizeid=d.organizeid and d.zt='1'
left join [NewtouchHIS_Base].[dbo].[V_C_Sys_ItemsDetail] e with(nolock) on a.[RegType]=e.code and e.CateCode='RegType' and e.zt='1'
where a.zt='1' and a.OrganizeId=@orgId";
            var ps = new List<SqlParameter> { SettDb.P("@orgId", orgId) };
            var ksrq = para.Get("ksrq");
            var jsrq = para.Get("jsrq");
            sql += " and a.OutDate>=@ksrq and a.OutDate<@jsrq ";
            if (!string.IsNullOrWhiteSpace(ksrq) && !string.IsNullOrWhiteSpace(jsrq))
            {
                ps.Add(SettDb.P("@ksrq", ksrq));
                ps.Add(SettDb.P("@jsrq", Convert.ToDateTime(jsrq).AddDays(1).ToString("yyyy-MM-dd")));
            }
            else
            {
                ps.Add(SettDb.P("@ksrq", DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd")));
                ps.Add(SettDb.P("@jsrq", DateTime.Now.AddDays(15).ToString("yyyy-MM-dd")));
            }
            if (!string.IsNullOrWhiteSpace(appId)) { sql += " and a.AppId=@appId "; ps.Add(SettDb.P("@appId", appId)); }
            if (!string.IsNullOrWhiteSpace(para.Get("kh"))) { sql += " and a.kh=@kh "; ps.Add(SettDb.P("@kh", para.Get("kh"))); }
            if (!string.IsNullOrWhiteSpace(para.Get("lxdh"))) { sql += " and a.lxdh=@lxdh "; ps.Add(SettDb.P("@lxdh", para.Get("lxdh"))); }
            if (!string.IsNullOrWhiteSpace(para.Get("bookId"))) { sql += " and a.BookId=@BookId "; ps.Add(SettDb.P("@BookId", para.Get("bookId"))); }
            int yyzt;
            if (int.TryParse(para.Get("yyzt"), out yyzt)) { sql += " and a.yyzt=@yyzt "; ps.Add(SettDb.P("@yyzt", yyzt)); }
            if (!string.IsNullOrWhiteSpace(para.Get("ks"))) { sql += " and a.ks=@ks "; ps.Add(SettDb.P("@ks", para.Get("ks"))); }
            var ys = para.Get("ys") ?? para.Get("ysgh");
            if (!string.IsNullOrWhiteSpace(ys)) { sql += " and a.ys=@ys "; ps.Add(SettDb.P("@ys", ys)); }
            sql += " order by a.outdate desc";
            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddRange(ps.ToArray());
                return SettDb.Read(cmd);
            }
        }

        // ---------------- Y022: đặt lịch (OutBookApply + OutbookingApply) ----------------
        private ResponseBaseOld BookApply(BookingRequest req, ParaData para)
        {
            if (!(para.Has("ghxz") && para.Has("scheduId") && para.Has("kh")))
                return ResponseBaseOld.Fail("Thông tin quan trọng không được để trống");
            using (var conn = _db.OpenSett())
            {
                var pat = GetPatInfo(conn, null, req.OrganizeId, para.Get("kh"), null, null);
                if (pat.Count == 0) return ResponseBaseOld.Fail("Không tìm thấy thông tin thẻ");
                var patRow = pat[0];
                var nowday = DateTime.Now.ToString("yyyy-MM-dd");
                var pbList = GetSchedule(conn, null, req.OrganizeId, null, nowday, null, null, null, null, null, para.Get("scheduId"));
                // GetMzpbSchedule gốc trong OutBookApply gọi với pblx=yy (IsBook=1)
                // và FromOutDate = hôm nay; GetSchedule trên đã lọc IsBook='1' và
                // OutDate >= hôm nay, cộng thêm FromOutDate = hôm nay.
                var pb = pbList.Count > 0 ? pbList[0] : null;
                if (pb == null) return ResponseBaseOld.Fail("Lịch trực hiện tại không đặt được");
                var scheduId = para.Get("scheduId");
                var kh = para.Get("kh");
                Dictionary<string, object> dupSame, dupDept;
                using (var cmd = new SqlCommand(@"select top 1 BookId from mz_gh_book with(nolock) where OrganizeId=@org and yyzt<>@cancel and zt='1' and kh=@kh and ScheduId=@sid", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    cmd.Parameters.Add(SettDb.P("@cancel", SettConstants.YyCancel));
                    cmd.Parameters.Add(SettDb.P("@kh", kh));
                    cmd.Parameters.Add(SettDb.P("@sid", Convert.ToDecimal(scheduId)));
                    dupSame = SettDb.ReadOne(cmd);
                }
                if (dupSame != null) return ResponseBaseOld.Fail("Cùng một nguồn số không được đặt trùng");
                var pbOutDate = Convert.ToDateTime(pb["OutDate"]);
                using (var cmd = new SqlCommand(@"select top 1 BookId from mz_gh_book with(nolock) where OrganizeId=@org and yyzt<>@cancel and zt='1' and kh=@kh and ks=@ks and OutDate=@od", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    cmd.Parameters.Add(SettDb.P("@cancel", SettConstants.YyCancel));
                    cmd.Parameters.Add(SettDb.P("@kh", kh));
                    cmd.Parameters.Add(SettDb.P("@ks", SettDb.Str(pb, "czks")));
                    cmd.Parameters.Add(SettDb.P("@od", pbOutDate));
                    dupDept = SettDb.ReadOne(cmd);
                }
                if (dupDept != null) return ResponseBaseOld.Fail("Cùng khoa trong ngày không được đặt trùng");
                // Còn lại nguồn số (GetScheduleDetail)
                var detail = ScheduleDetail(conn, null, req.OrganizeId, scheduId);
                if (detail.Count == 0) return ResponseBaseOld.Fail("Không lấy được thông tin nguồn số, vui lòng thử lại");
                var bookNum = SettDb.Int(detail[0], "BookNum");
                if (bookNum.HasValue && bookNum.Value == 0) return ResponseBaseOld.Fail("Lịch trực này đã hết nguồn số có thể đặt");
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        short queno = SettDb.GetJzxh(conn, tx, scheduId, SettDb.Str(pb, "czks"), SettDb.Str(pb, "ysgh"), "", req.AppId, req.OrganizeId);
                        int bookId = SettDb.GetNewId(conn, tx, "mz_gh_book");
                        using (var cmd = new SqlCommand(@"insert into mz_gh_book (OrganizeId, BookId, ScheduId, kh, AppId, OutDate, patid, Period, PeriodStart, PeriodEnd, QueueNo, ghxz, RegType, RegFee, ks, lxdh, ys, xm, xb, yynr, yyzt, zt, CreatorCode, CreateTime)
values (@org, @bookId, @sid, @kh, @app, @od, @patid, @period, @ps, @pe, @q, @ghxz, @rt, @fee, @ks, @lxdh, @ys, @xm, @xb, @yynr, '1', '1', @user, getdate())", conn, tx))
                        {
                            cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                            cmd.Parameters.Add(SettDb.P("@bookId", bookId));
                            cmd.Parameters.Add(SettDb.P("@sid", Convert.ToDecimal(scheduId)));
                            cmd.Parameters.Add(SettDb.P("@kh", kh));
                            cmd.Parameters.Add(SettDb.P("@app", req.AppId));
                            cmd.Parameters.Add(SettDb.P("@od", pbOutDate));
                            cmd.Parameters.Add(SettDb.P("@patid", SettDb.Int(patRow, "patid")));
                            cmd.Parameters.Add(SettDb.P("@period", Convert.ToInt32(pb["Period"])));
                            cmd.Parameters.Add(SettDb.P("@ps", SettDb.Str(pb, "PeriodStart")));
                            cmd.Parameters.Add(SettDb.P("@pe", SettDb.Str(pb, "PeriodEnd")));
                            cmd.Parameters.Add(SettDb.P("@q", queno));
                            cmd.Parameters.Add(SettDb.P("@ghxz", para.Get("ghxz")));
                            cmd.Parameters.Add(SettDb.P("@rt", SettDb.Str(pb, "RegType")));
                            cmd.Parameters.Add(SettDb.P("@fee", SettDb.Dec(pb, "RegFee") ?? 0m));
                            cmd.Parameters.Add(SettDb.P("@ks", SettDb.Str(pb, "czks")));
                            cmd.Parameters.Add(SettDb.P("@lxdh", para.Get("dh") ?? SettDb.Str(patRow, "dh")));
                            cmd.Parameters.Add(SettDb.P("@ys", SettDb.Str(pb, "ysgh")));
                            cmd.Parameters.Add(SettDb.P("@xm", SettDb.Str(patRow, "xm")));
                            cmd.Parameters.Add(SettDb.P("@xb", SettDb.Str(patRow, "xb")));
                            cmd.Parameters.Add(SettDb.P("@yynr", SettDb.Str(pb, "Title")));
                            cmd.Parameters.Add(SettDb.P("@user", SettConstants.ApiUser));
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        var bookPara = new ParaData(JObject.FromObject(new { bookId = bookId.ToString() }));
                        var record = BookRecordQuery(conn, null, req.OrganizeId, bookPara, null);
                        if (record.Count > 0) return ResponseBaseOld.Ok(record, "Đặt hẹn thành công");
                        return ResponseBaseOld.Fail("Đặt hẹn thất bại");
                    }
                    catch (Exception)
                    {
                        try { tx.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }

        /// <summary>Chi tiết nguồn số còn lại của lịch trực (GetScheduleDetail gốc).</summary>
        public static List<Dictionary<string, object>> ScheduleDetail(SqlConnection conn, SqlTransaction tx, string orgId, string scheduId)
        {
            var sql = @"select convert(varchar(20),a.ScheduId) ScheduId, a.ghpbId, convert(varchar(10),a.OutDate,120) OutDate, a.ysgh, a.ysxm, a.czks, a.czksmc,
a.RegType, a.Title, a.Period, a.PeriodDesc, a.TotalNum, a.PeriodStart, a.PeriodEnd,
a.RegFee, a.IsBook, a.ghlx, a.zlxm, a.weekdd, a.IsCancel, a.TotalNum-count(b.bookid) BookNum
from [mz_ghpb_schedule] a with(nolock)
left join mz_gh_book b with(nolock) on a.organizeid=b.organizeid and a.scheduid=b.scheduid and b.zt=1 and b.yyzt<>@yyzt
where a.zt=1 and a.organizeid=@orgId";
            var ps = new List<SqlParameter>
            {
                SettDb.P("@orgId", orgId),
                SettDb.P("@yyzt", SettConstants.YyCancel)
            };
            if (!string.IsNullOrWhiteSpace(scheduId)) { sql += " and a.scheduId=@scheduId"; ps.Add(SettDb.P("@scheduId", scheduId)); }
            sql += @" group by a.ScheduId, a.ghpbId, a.OutDate, a.ysgh, a.ysxm, a.czks, a.czksmc,
a.RegType, a.Title, a.Period, a.PeriodDesc, a.TotalNum, a.PeriodStart, a.PeriodEnd,
a.RegFee, a.IsBook, a.ghlx, a.zlxm, a.weekdd, a.IsCancel";
            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddRange(ps.ToArray());
                return SettDb.Read(cmd);
            }
        }

        // ---------------- Y023: hủy đặt lịch (OutBookCancel) ----------------
        private ResponseBaseOld BookCancel(BookingRequest req, ParaData para)
        {
            if (!(para.Has("bookId") && para.Has("kh") && para.Has("lxdh")))
                return ResponseBaseOld.Fail("Thông tin quan trọng không được để trống");
            using (var conn = _db.OpenSett())
            {
                Dictionary<string, object> rec;
                using (var cmd = new SqlCommand("select top 1 * from mz_gh_book with(nolock) where BookId=@bid and OrganizeId=@org and zt='1'", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@bid", para.Get("bookId")));
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    rec = SettDb.ReadOne(cmd);
                }
                if (rec == null) return ResponseBaseOld.Fail("Hủy đặt hẹn thất bại, trạng thái đặt hẹn hiện tại không thể hủy");
                if (SettDb.Str(rec, "kh") != para.Get("kh")) return ResponseBaseOld.Fail("Thao tác thất bại, thông tin thẻ bất thường");
                if (SettDb.Str(rec, "yyzt") != SettConstants.YyBook) return ResponseBaseOld.Fail("Hủy đặt hẹn thất bại, trạng thái đặt hẹn hiện tại không thể hủy");
                using (var cmd = new SqlCommand(@"update mz_gh_book set yyzt=@cancel, CancelReason=@cr, CancelTime=getdate(), LastModifyTime=getdate()
where BookId=@bid and OrganizeId=@org and zt='1'", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@cancel", SettConstants.YyCancel));
                    cmd.Parameters.Add(SettDb.P("@cr", req.AppId + ";" + para.Get("lxdh")));
                    cmd.Parameters.Add(SettDb.P("@bid", para.Get("bookId")));
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    var n = cmd.ExecuteNonQuery();
                    if (n > 0) return ResponseBaseOld.Ok(new { BookId = rec["BookId"] }, "Hủy đặt hẹn thành công");
                }
                return ResponseBaseOld.Fail("Hủy đặt hẹn thất bại, trạng thái đặt hẹn hiện tại không thể hủy");
            }
        }

        // ---------------- Y027/Y028/Y029: tái cấu trúc (không có case gốc) ----------------
        // Y027 OutpRegTodayQuery (8016): danh sách đăng ký khám hiệu lực trong
        // ngày theo kh/mzh. Gốc không có case Y027; tái cấu trúc từ
        // GetCurrentDayRegList (mz_gh trong ngày) + điều kiện ghzt <> '2' (chưa
        // hủy) và ghrq = hôm nay.
        private ResponseBaseOld RegTodayQuery(BookingRequest req, ParaData para)
        {
            using (var conn = _db.OpenSett())
            {
                var sql = @"select gh.*, staff.Name ysmc, dept.Name ksmc, brxz.brxzmc
from mz_gh gh with(nolock)
left join [NewtouchHIS_Base]..V_S_Sys_Staff staff on staff.gh = gh.ys and staff.Organizeid = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department dept on dept.Code = gh.ks and dept.Organizeid = gh.OrganizeId
left join xt_brxz brxz with(nolock) on brxz.brxz = gh.brxz and brxz.OrganizeId = gh.OrganizeId and brxz.zt='1'
where gh.OrganizeId=@org and gh.zt='1' and gh.ghzt<>'2' and convert(date, gh.ghrq)=convert(date, getdate())";
                var ps = new List<SqlParameter> { SettDb.P("@org", req.OrganizeId) };
                if (!string.IsNullOrWhiteSpace(para.Get("kh"))) { sql += " and gh.kh=@kh "; ps.Add(SettDb.P("@kh", para.Get("kh"))); }
                if (!string.IsNullOrWhiteSpace(para.Get("mzh"))) { sql += " and gh.mzh=@mzh "; ps.Add(SettDb.P("@mzh", para.Get("mzh"))); }
                sql += " order by gh.CreateTime desc";
                using (var cmd = new SqlCommand(_db.Q(sql), conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    var list = SettDb.Read(cmd);
                    if (list.Count > 0) return ResponseBaseOld.Ok(list);
                    return ResponseBaseOld.Fail("Không tìm thấy bản ghi liên quan");
                }
            }
        }

        // Y028 OutpBillUnpaidByMzh (8016): hóa đơn ngoại trú chưa thanh toán
        // theo mzh. Gốc không có case Y028 (Y030 OutpatChargeInfo trả về hóa đơn
        // ĐÃ quyết toán, khác bản chất). Tái cấu trúc: đơn thuốc chưa thu phí
        // (cfzt='0') của lần khám, qua GetNewUnSettedPrescriptionByMzh gốc.
        private ResponseBaseOld BillUnpaidByMzh(BookingRequest req, ParaData para)
        {
            using (var conn = _db.OpenSett())
            {
                var sql = @"SELECT CAST(cf.cfnm AS VARCHAR(20)) AS cfnm, cf.cfh, CAST(cf.cflx AS VARCHAR(20)) AS cflx, cf.cflxmc, cf.zje, cf.ksmc, cf.ysmc, CONVERT(VARCHAR(20),cf.createtime,120) AS klsj
FROM mz_cf(NOLOCK) cf
INNER JOIN mz_gh(NOLOCK) gh ON cf.ghnm=gh.ghnm AND gh.OrganizeId=cf.OrganizeId
WHERE cf.OrganizeId=@org AND cf.zt='1' AND cf.cfzt='0' AND gh.mzh=@mzh AND gh.zt='1'
ORDER BY cf.CreateTime";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    cmd.Parameters.Add(SettDb.P("@mzh", para.Get("mzh")));
                    var list = SettDb.Read(cmd);
                    if (list.Count > 0) return ResponseBaseOld.Ok(list);
                    return ResponseBaseOld.Fail("Không tìm thấy hóa đơn chưa thanh toán liên quan");
                }
            }
        }

        // Y029 OutpBillItemDetail (8016): chi tiết hạng mục hóa đơn theo danh
        // sách PresIds (cfnm) + mzh. Gốc không có case Y029 (OutpatChargeDetail
        // gốc tra theo jsnm, khác khóa). Tái cấu trúc: mz_cfmx join mz_cf/mz_gh.
        private ResponseBaseOld BillItemDetail(BookingRequest req, ParaData para)
        {
            var presId = para.Get("PresId");
            if (string.IsNullOrWhiteSpace(presId)) return ResponseBaseOld.Fail("Tham số quan trọng không được để trống");
            var ids = presId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (ids.Length == 0) return ResponseBaseOld.Fail("Tham số quan trọng không được để trống");
            using (var conn = _db.OpenSett())
            {
                var inParams = new List<string>();
                var ps = new List<SqlParameter> { SettDb.P("@org", req.OrganizeId) };
                for (int i = 0; i < ids.Length; i++)
                {
                    inParams.Add("@id" + i);
                    ps.Add(SettDb.P("@id" + i, ids[i].Trim()));
                }
                var sql = @"select mx.*, cf.cfh, cf.cflx, cf.cflxmc
from mz_cfmx mx with(nolock)
join mz_cf cf with(nolock) on mx.cfnm=cf.cfnm and mx.OrganizeId=cf.OrganizeId
join mz_gh gh with(nolock) on cf.ghnm=gh.ghnm and cf.OrganizeId=gh.OrganizeId
where mx.OrganizeId=@org and mx.zt='1' and cf.cfnm in (" + string.Join(",", inParams) + ")";
                if (!string.IsNullOrWhiteSpace(para.Get("mzh"))) { sql += " and gh.mzh=@mzh "; ps.Add(SettDb.P("@mzh", para.Get("mzh"))); }
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    var list = SettDb.Read(cmd);
                    if (list.Count > 0) return ResponseBaseOld.Ok(list);
                    return ResponseBaseOld.Fail("Không tìm thấy chi tiết hóa đơn liên quan");
                }
            }
        }
    }

    public class FailException : Exception
    {
        public string SubCode { get; }
        public FailException(string msg, string subCode = "") : base(msg)
        {
            SubCode = subCode;
        }
    }
}
