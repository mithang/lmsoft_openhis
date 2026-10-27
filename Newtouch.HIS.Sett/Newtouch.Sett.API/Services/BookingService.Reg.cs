using Microsoft.Data.SqlClient;
using Newtouch.Sett.API.Models;

namespace Newtouch.Sett.API.Services
{
    /// <summary>
    /// Phần ghi đăng ký của BookingService: Y024 (đăng ký từ lịch đã đặt, có
    /// thanh toán) và Y026 (đăng ký trực tiếp), cùng Save port từ
    /// OutPatientSettleDmnService.Save.
    ///
    /// Hai điểm KHÁC BIỆT so với code gốc, bắt buộc để Y024 chạy được (ghi rõ,
    /// không lặng lẽ):
    /// 1. PaymentModelAccountReserveII gốc đọc feeRelated.yjjzfje.Value và
    ///    feeRelated.xjwc trực tiếp; luồng API (OutpatRegApply) không set hai
    ///    trường này (null) nên bản gốc sẽ NullReferenceException khi xmList
    ///    non-empty. Port coi null là 0/rỗng.
    /// 2. fph luôn null trong luồng API nên bước UpdateCurrentFinancialInvoice
    ///    (cập nhật số hóa đơn giấy hiện tại) được bỏ qua; nhánh BHYT Trùng
    ///    Khánh/Quý An (medicalInsurance == "chongqing"/"guian") cũng không áp
    ///    dụng vì cấu hình demo là "tongyong".
    /// </summary>
    public partial class BookingService
    {
        // ---------------- Y024: đăng ký từ lịch đã đặt (OutBookPay -> OutpatRegApply) ----------------
        // Handler gốc yêu cầu bookId + kh + PayFee + PayLsh (optype "0" của 8016
        // không được handler gốc dùng tới). 8016 chỉ validate kh/BookId nên
        // caller thiếu PayFee/PayLsh sẽ nhận FAIL đúng như bản gốc.
        private ResponseBaseOld BookPay(BookingRequest req, ParaData para)
        {
            if (!(para.Has("bookId") && para.Has("kh") && para.Has("PayFee") && para.Has("PayLsh")))
                return ResponseBaseOld.Fail("Thông tin quan trọng không được để trống");
            using (var conn = _db.OpenSett())
            {
                var bookId = para.Get("bookId");
                var kh = para.Get("kh");
                decimal payFee;
                if (!decimal.TryParse(para.Get("PayFee"), out payFee))
                    return ResponseBaseOld.Fail("Thông tin quan trọng không được để trống");
                Dictionary<string, object> book;
                using (var cmd = new SqlCommand(@"select top 1 * from mz_gh_book with(nolock) where BookId=@bid and yyzt=@book and zt='1' and OrganizeId=@org and OutDate>=@today", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@bid", bookId));
                    cmd.Parameters.Add(SettDb.P("@book", SettConstants.YyBook));
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    cmd.Parameters.Add(SettDb.P("@today", DateTime.Now.Date));
                    book = SettDb.ReadOne(cmd);
                }
                if (book == null) return ResponseBaseOld.Fail("Vui lòng xác nhận trạng thái đặt hẹn và ngày có hiệu lực hay không.");
                if (SettDb.Str(book, "kh") != kh) return ResponseBaseOld.Fail("Thông tin thẻ bất thường, vui lòng xác nhận thông tin đặt hẹn.");
                var regFee = SettDb.Dec(book, "RegFee") ?? 0m;
                if (regFee != payFee) return ResponseBaseOld.Fail("Số tiền đăng ký khám có誤.");
                var patList = GetPatInfo(conn, null, req.OrganizeId, kh, null, null);
                if (patList.Count == 0) return ResponseBaseOld.Fail("Thông tin thẻ bất thường, vui lòng liên hệ quản trị viên.");
                var pat = patList[0];
                int patid = SettDb.Int(pat, "patid") ?? -1;
                if (patid != (SettDb.Int(book, "patid") ?? -1)) return ResponseBaseOld.Fail("Thông tin đặt hẹn và thông tin thẻ không khớp, vui lòng thử lại.");
                var scheduId = SettDb.Str(book, "ScheduId");
                var queueNo = SettDb.Str(book, "QueueNo");
                var ks = SettDb.Str(book, "ks");
                // GetRegMzh (qua GetCQMzhJzxh với QueueNo có sẵn): kiểm tra nợ
                // phí tồn đọng trừ khi cấu hình cho phép đăng ký trùng.
                string mzh;
                short jzxh;
                try
                {
                    // GetCQMzhJzxh: có QueueNo thì dùng luôn số đó làm jzxh.
                    var t = GetCqMzhJzxh(conn, null, req.OrganizeId, patid, scheduId, ks, null, SettConstants.ApiUser, queueNo, null, Convert.ToDateTime(book["OutDate"]).ToString("yyyy-MM-dd"));
                    jzxh = t.Item1;
                    mzh = t.Item2;
                }
                catch (FailException ex) { return ResponseBaseOld.Fail(ex.Message, ex.SubCode); }
                var sched = GetSchedule(conn, null, req.OrganizeId, null, null, null, null, null, null, null, scheduId);
                if (sched.Count == 0) return ResponseBaseOld.Fail("Tạo số khám ngoại trú thất bại");
                var scheduety = sched[0];
                var fee = new FeeRelated
                {
                    zje = regFee,
                    orglxjzfys = regFee,
                    ssk = regFee,
                    zhaoling = 0m,
                    xjzfys = regFee
                };
                string ghly = "1";
                string zffs = "";
                var appId = SettDb.Str(book, "AppId") ?? "";
                if (appId == "WeChat") { zffs = CashPayModel(conn, null, "微信"); ghly = "4"; }
                else if (appId == "Alipay") { zffs = CashPayModel(conn, null, "支付宝"); ghly = "5"; }
                fee.zffs1 = zffs;
                fee.zfje1 = string.IsNullOrEmpty(zffs) ? (decimal?)null : regFee;
                object regObj;
                try
                {
                    regObj = SaveRegistration(conn, req.OrganizeId, patid, kh, ghly,
                        SettDb.Str(scheduety, "RegType"), SettDb.Str(scheduety, "czks"), "",
                        SettDb.Str(scheduety, "czksmc"), "", SettDb.Str(scheduety, "ghlx"), SettDb.Str(scheduety, "zlxm"),
                        null, null, false, false, jzxh, Convert.ToInt32(Convert.ToDecimal(scheduId)),
                        fee, SettDb.Str(book, "ghxz"), null, mzh, "0", null, null, null, null, null, null, null);
                }
                catch (FailException ex) { return ResponseBaseOld.Fail(ex.Message, ex.SubCode); }
                // Cập nhật bản ghi đặt hẹn + đăng ký khám (OutpatRegApply phần cuối)
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("update mz_gh set ghrq=@od where mzh=@mzh and zt='1' and OrganizeId=@org", conn, tx))
                        {
                            cmd.Parameters.Add(SettDb.P("@od", Convert.ToDateTime(book["OutDate"])));
                            cmd.Parameters.Add(SettDb.P("@mzh", mzh));
                            cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                            cmd.ExecuteNonQuery();
                        }
                        using (var cmd = new SqlCommand(@"update mz_gh_book set PayFee=@pf, PayLsh=@pl, LastModifyTime=getdate(), LastModifierCode=@user, yyzt=@reg, ghrq=@od, mzh=@mzh
where BookId=@bid and OrganizeId=@org", conn, tx))
                        {
                            cmd.Parameters.Add(SettDb.P("@pf", payFee));
                            cmd.Parameters.Add(SettDb.P("@pl", para.Get("PayLsh")));
                            cmd.Parameters.Add(SettDb.P("@user", SettConstants.ApiUser));
                            cmd.Parameters.Add(SettDb.P("@reg", SettConstants.YyReg));
                            cmd.Parameters.Add(SettDb.P("@od", Convert.ToDateTime(book["OutDate"])));
                            cmd.Parameters.Add(SettDb.P("@mzh", mzh));
                            cmd.Parameters.Add(SettDb.P("@bid", bookId));
                            cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch (Exception)
                    {
                        try { tx.Rollback(); } catch { }
                        throw;
                    }
                }
                return ResponseBaseOld.Ok(regObj);
            }
        }

        // ---------------- Y026: đăng ký trực tiếp (CommonRegist -> OutpatReg) ----------------
        // SHIM TƯƠNG THÍCH 8016 (điểm lệch có chủ ý, xem REPORT.md): cổng gốc
        // bắt buộc ks+kh+ghxz, nhưng 8016 OutpatientRegist KHÔNG có trường ks
        // (OutpatientRegistRequest chỉ có kh/BookId/ScheduId/ghxz/...) nên qua
        // 8016, Y026 gốc sẽ LUÔN FAIL ở cổng ks. Khi thiếu ks nhưng có
        // ScheduId, host suy ks từ chính dòng lịch trực đó (lịch hôm nay,
        // RegType "1") rồi chạy tiếp đúng luồng gốc; khi không có cả ks lẫn
        // ScheduId thì vẫn FAIL như gốc. Đường ks đầy đủ không đổi hành vi.
        private ResponseBaseOld CommonRegist(BookingRequest req, ParaData para)
        {
            if (!((para.Has("ks") || para.Has("ScheduId") || para.Has("scheduId")) && para.Has("kh") && para.Has("ghxz")))
                return ResponseBaseOld.Fail("Thông tin quan trọng không được để trống");
            using (var conn = _db.OpenSett())
            {
                var kh = para.Get("kh");
                var orgId = req.OrganizeId;
                var patList = GetPatInfo(conn, null, orgId, kh, null, null);
                if (patList.Count == 0) return ResponseBaseOld.Fail("Thông tin thẻ bất thường, vui lòng liên hệ quản trị viên.");
                var pat = patList[0];
                int patid = SettDb.Int(pat, "patid") ?? -1;
                var ghrq = DateTime.Now.ToString("yyyy-MM-dd");
                var ks = para.Get("ks");
                var ghxz = para.Get("ghxz");
                // RegLimit: chỉ kiểm tra khi Sys_Config RegLimit == "1" (demo: không có key này -> bỏ qua)
                if (_db.SysConfig(conn, null, orgId, "RegLimit") == "1" && !string.IsNullOrWhiteSpace(ks))
                {
                    if (!RegPermitCheck(conn, null, orgId, SettDb.Str(pat, "blh"), ks, ghrq))
                        return ResponseBaseOld.Fail("Cùng một số thẻ, cùng khoa không cho phép đăng ký trùng.");
                }
                // Lịch trực hôm nay của khoa, RegType "1" (bản gốc truyền RegType="1" cứng);
                // shim: nếu thiếu ks thì tra lịch theo ScheduId rồi lấy czks làm ks.
                Dictionary<string, object> scheduety;
                if (string.IsNullOrWhiteSpace(ks))
                {
                    var sid = para.Get("ScheduId") ?? para.Get("scheduId");
                    var sched = GetSchedule(conn, null, orgId, ghrq, null, null, null, null, "1", null, sid);
                    if (sched.Count == 0) return ResponseBaseOld.Fail("Khoa này hôm nay không có lịch trực liên quan.");
                    scheduety = sched[0];
                    ks = SettDb.Str(scheduety, "czks");
                }
                else
                {
                    var sched = GetSchedule(conn, null, orgId, ghrq, null, null, ks, null, "1", null, null);
                    if (sched.Count == 0) return ResponseBaseOld.Fail("Khoa này hôm nay không có lịch trực liên quan.");
                    scheduety = sched[0];
                }
                var scheduId = SettDb.Str(scheduety, "ScheduId");
                string mzh;
                short jzxh;
                try
                {
                    var t = GetCqMzhJzxh(conn, null, orgId, patid, scheduId, ks, null, SettConstants.ApiUser, null, null, ghrq);
                    jzxh = t.Item1;
                    mzh = t.Item2;
                }
                catch (FailException ex) { return ResponseBaseOld.Fail(ex.Message, ex.SubCode); }
                var regFee = SettDb.Dec(scheduety, "RegFee") ?? 0m;
                var fee = new FeeRelated
                {
                    zje = regFee,
                    orglxjzfys = regFee,
                    ssk = regFee,
                    zhaoling = 0m,
                    xjzfys = regFee
                };
                object regObj;
                try
                {
                    // Y026 truyền ghxm=null, zlxm=null -> xmList rỗng -> Save chỉ
                    // insert mz_gh (không tạo mz_js/mz_jsmx), đúng hành vi gốc.
                    regObj = SaveRegistration(conn, orgId, patid, kh, "1",
                        SettDb.Str(scheduety, "RegType"), SettDb.Str(scheduety, "czks"), "",
                        SettDb.Str(scheduety, "czksmc"), "", null, null,
                        null, null, false, false, jzxh, Convert.ToInt32(Convert.ToDecimal(scheduId)),
                        fee, ghxz, null, mzh, "0", null, null, null, null, null, null,
                        para.Get("ghlybz") == "1" ? "1" : null);
                }
                catch (FailException ex) { return ResponseBaseOld.Fail(ex.Message, ex.SubCode); }
                return ResponseBaseOld.Ok(regObj, "Đăng ký khám thành công.");
            }
        }

        private bool RegPermitCheck(SqlConnection conn, SqlTransaction tx, string orgId, string blh, string ks, string ghrq)
        {
            using (var cmd = new SqlCommand(@"select mzh from mz_gh with(nolock)
where organizeid=@org and blh=@blh and ks=@ks and convert(date,ghrq)=convert(date,@ghrq) and ghzt=0", conn, tx))
            {
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                cmd.Parameters.Add(SettDb.P("@blh", blh));
                cmd.Parameters.Add(SettDb.P("@ks", ks));
                cmd.Parameters.Add(SettDb.P("@ghrq", ghrq));
                return SettDb.ReadOne(cmd) == null;
            }
        }

        private string CashPayModel(SqlConnection conn, SqlTransaction tx, string namePart)
        {
            using (var cmd = new SqlCommand("select top 1 xjzffs from xt_xjzffs with(nolock) where xjzffsmc like @p and zt='1'", conn, tx))
            {
                cmd.Parameters.Add(SettDb.P("@p", "%" + namePart + "%"));
                var r = SettDb.ReadOne(cmd);
                return r == null ? "" : SettDb.Str(r, "xjzffs");
            }
        }

        // GetCQMzhJzxh gốc: có QueueNo thì dùng luôn, không thì GetMzhJzxh + GetRegMzh.
        private Tuple<short, string> GetCqMzhJzxh(SqlConnection conn, SqlTransaction tx, string orgId, int patid,
            string scheduId, string ks, string ys, string user, string queueNo, string mjzbz, string outDate)
        {
            if (!string.IsNullOrWhiteSpace(queueNo))
            {
                short j = Convert.ToInt16(queueNo);
                var m = GetRegMzh(conn, tx, orgId, patid, scheduId, ks, user, j, outDate);
                return Tuple.Create(j, m);
            }
            var t = GetMzhJzxh(conn, tx, orgId, patid, scheduId, ks, ys, user);
            var mzh = GetRegMzh(conn, tx, orgId, patid, scheduId, ks, user, t.Item1, DateTime.Now.ToString());
            return Tuple.Create(t.Item1, mzh);
        }

        private Tuple<short, string> GetMzhJzxh(SqlConnection conn, SqlTransaction tx, string orgId, int patid,
            string ghpbId, string ks, string ys, string user)
        {
            if (patid <= 0 || string.IsNullOrWhiteSpace(ghpbId) || string.IsNullOrWhiteSpace(ks))
                throw new FailException("Dữ liệu không đầy đủ");
            if (!_db.SysConfigBool(conn, tx, orgId, "Outpatient_Register_UnSetted_Multi"))
            {
                if (HasUnsettedRegister(conn, tx, orgId, patid)) throw new FailException("Tồn tại đăng ký chưa quyết toán trong lịch sử, không thể đăng ký mới");
                if (HasUnsetted(conn, tx, orgId, patid)) throw new FailException("Tồn tại lịch sử chưa quyết toán, không thể đăng ký mới");
            }
            short jzxh = SettDb.GetJzxh(conn, tx, ghpbId, ks, ys ?? "", "", user, orgId);
            var ghpbIdStr = ghpbId.PadLeft(3, '0');
            if (ghpbIdStr.Length > 3) ghpbIdStr = ghpbIdStr.Substring(ghpbIdStr.Length - 3, 3);
            var mzh = DateTime.Now.ToString("yyMMdd") + ghpbIdStr + jzxh.ToString().PadLeft(4, '0');
            return Tuple.Create(jzxh, mzh);
        }

        private string GetRegMzh(SqlConnection conn, SqlTransaction tx, string orgId, int patid,
            string ghpbId, string ks, string user, short jzxh, string jzrq)
        {
            if (patid <= 0 || string.IsNullOrWhiteSpace(ghpbId) || string.IsNullOrWhiteSpace(ks))
                throw new FailException("Dữ liệu không đầy đủ");
            if (!_db.SysConfigBool(conn, tx, orgId, "Outpatient_Register_UnSetted_Multi"))
            {
                if (HasUnsettedRegister(conn, tx, orgId, patid)) throw new FailException("Tồn tại đăng ký chưa quyết toán trong lịch sử, không thể đăng ký mới");
                if (HasUnsetted(conn, tx, orgId, patid)) throw new FailException("Tồn tại lịch sử chưa quyết toán, không thể đăng ký mới");
            }
            var ghpbIdStr = ghpbId.PadLeft(3, '0');
            if (ghpbIdStr.Length > 3) ghpbIdStr = ghpbIdStr.Substring(ghpbIdStr.Length - 3, 3);
            var ghrq = Convert.ToDateTime(jzrq).ToString("yyMMdd");
            return ghrq + ghpbIdStr + jzxh.ToString().PadLeft(4, '0');
        }

        private bool HasUnsettedRegister(SqlConnection conn, SqlTransaction tx, string orgId, int patid)
        {
            using (var cmd = new SqlCommand("select '1' from mz_gh(nolock) where patid=@patid and OrganizeId=@org and zt='1' and ghzt='0'", conn, tx))
            {
                cmd.Parameters.Add(SettDb.P("@patid", patid));
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                return SettDb.ReadOne(cmd) != null;
            }
        }

        private bool HasUnsetted(SqlConnection conn, SqlTransaction tx, string orgId, int patid)
        {
            using (var cmd = new SqlCommand(@"select '1' from mz_xm(nolock) where patid=@patid and OrganizeId=@org and zt='1' and xmzt='0'
union select '1' from mz_cf(nolock) where patid=@patid and OrganizeId=@org and zt='1' and cfzt='0'", conn, tx))
            {
                cmd.Parameters.Add(SettDb.P("@patid", patid));
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                return SettDb.ReadOne(cmd) != null;
            }
        }

        private class FeeRelated
        {
            public decimal? zje;
            public decimal? orglxjzfys;
            public decimal? ssk;
            public decimal? zhaoling;
            public decimal? xjzfys;
            public string zffs1;
            public decimal? zfje1;
            public decimal? zkbl;
            public decimal? zkje;
            public string ybjslsh;
            public string ecToken;
        }

        private class FeeItem
        {
            public int xmnm;
            public string sfxm;
            public string dl;
            public decimal dj;
            public decimal sl = 1m;
            public decimal je;
            public string dw;
            public decimal? zfbl;
            public string zfxz;
        }

        // Save port từ OutPatientSettleDmnService.Save (luồng API: fph/sfrq null,
        // isCkf/isGbf false). Trả về object newJszbInfo {mzh, jsnm, ybjsh, jszje, jsxjzf}.
        private object SaveRegistration(SqlConnection conn, string orgId, int patid, string kh, string ghly,
            string mjzbz, string ks, string ys, string ksmc, string ysmc, string ghxm, string zlxm,
            string fph, DateTime? sfrq, bool isCkf, bool isGbf, short jzxh, int ghpbId,
            FeeRelated fee, string brxz, string ybjsh, string mzh, string jzyy, string jzid, string jzlx,
            string bzbm, string bzmc, string mzyyghId, string isjm, string ghlybzFlag)
        {
            Dictionary<string, object> dup;
            using (var cmd = new SqlCommand("select top 1 ghnm from mz_gh with(nolock) where mzh=@mzh", conn))
            {
                cmd.Parameters.Add(SettDb.P("@mzh", mzh));
                dup = SettDb.ReadOne(cmd);
            }
            if (dup != null) throw new FailException("Số khám ngoại trú trùng lặp");
            Dictionary<string, object> br;
            using (var cmd = new SqlCommand("select top 1 * from xt_brjbxx with(nolock) where patid=@patid and zt='1' and OrganizeId=@org", conn))
            {
                cmd.Parameters.Add(SettDb.P("@patid", patid));
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                br = SettDb.ReadOne(cmd);
            }
            if (br == null) throw new FailException("OUTPAT_NO_PATIENT_INFORMATION");
            string cardType = "";
            string cardTypeName = "";
            using (var cmd = new SqlCommand("select top 1 CardType, CardTypeName from xt_card with(nolock) where CardNo=@kh and OrganizeId=@org", conn))
            {
                cmd.Parameters.Add(SettDb.P("@kh", kh));
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                var card = SettDb.ReadOne(cmd);
                if (card != null) { cardType = SettDb.Str(card, "CardType"); cardTypeName = SettDb.Str(card, "CardTypeName"); }
            }
            // GetXmList (trước transaction, như bản gốc)
            decimal ghf, zlf, ckf, gbf;
            var xmList = GetXmList(conn, null, orgId, ghxm, zlxm, isCkf, isGbf, out ghf, out zlf, out ckf, out gbf);
            // fzbz: còn đăng ký hiệu lực khác của bệnh nhân (ghzt != '2')
            bool hasOther;
            using (var cmd = new SqlCommand("select top 1 ghnm from mz_gh with(nolock) where patid=@patid and zt='1' and ghzt<>'2'", conn))
            {
                cmd.Parameters.Add(SettDb.P("@patid", patid));
                hasOther = SettDb.ReadOne(cmd) != null;
            }
            var medicalInsurance = _db.SysConfig(conn, null, orgId, "Outpatient_MedicalInsurance");
            bool? notSett = null;
            if (brxz != "0" && cardType == "2") // YBJYK (thẻ BHXH)
            {
                notSett = _db.SysConfigBool(conn, null, orgId, "Outpatient_ChargeFee_OpenYbSett")
                    && _db.SysConfigBool(conn, null, orgId, "Outpatient_ChargeFee_yb_SettOnce");
            }
            if (!(notSett == true))
            {
                if (medicalInsurance == "guian") notSett = true;
                if (medicalInsurance == "chongqing") notSett = false;
            }
            var ghzt = notSett == true ? "0" : "1";
            int ghnm;
            int jsnm = 0;
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    ghnm = SettDb.GetNewId(conn, tx, "mz_gh");
                    var csny = br["csny"];
                    string nlshow = "";
                    if (csny != null && csny != DBNull.Value)
                    {
                        var age = DateTime.Now.Year - Convert.ToDateTime(csny).Year;
                        nlshow = age + " tuổi";
                    }
                    using (var cmd = new SqlCommand(@"insert into mz_gh (ghnm, OrganizeId, ghly, mjzbz, ks, ys, jzbz, jzxh, mzh, ghlx, fzbz, kh, ScheduId, ghrq, patid, brxz, ghzt, jsrq, ghf, zlf, ckf, gbf, xm, xb, csny, blh, zjlx, zjh, lxr, lxrgx, lxrdh, nlshow, CardType, CardTypeName, jzyy, yyghId, zt, CreatorCode, CreateTime)
values (@ghnm, @org, @ghly, @mjzbz, @ks, @ys, '1', @jzxh, @mzh, @ghlx, @fzbz, @kh, @sid, getdate(), @patid, @brxz, @ghzt, @jsrq, @ghf, @zlf, @ckf, @gbf, @xm, @xb, @csny, @blh, @zjlx, @zjh, @lxr, @lxrgx, @lxrdh, @nlshow, @ct, @ctn, @jzyy, @yygh, '1', @user, getdate())", conn, tx))
                    {
                        cmd.Parameters.Add(SettDb.P("@ghnm", ghnm));
                        cmd.Parameters.Add(SettDb.P("@org", orgId));
                        cmd.Parameters.Add(SettDb.P("@ghly", ghly));
                        cmd.Parameters.Add(SettDb.P("@mjzbz", mjzbz));
                        cmd.Parameters.Add(SettDb.P("@ks", ks));
                        cmd.Parameters.Add(SettDb.P("@ys", ys));
                        cmd.Parameters.Add(SettDb.P("@jzxh", jzxh));
                        cmd.Parameters.Add(SettDb.P("@mzh", mzh));
                        cmd.Parameters.Add(SettDb.P("@ghlx", ghxm));
                        cmd.Parameters.Add(SettDb.P("@fzbz", (byte)(hasOther ? 1 : 0)));
                        cmd.Parameters.Add(SettDb.P("@kh", kh));
                        cmd.Parameters.Add(SettDb.P("@sid", (decimal)ghpbId));
                        cmd.Parameters.Add(SettDb.P("@patid", patid));
                        cmd.Parameters.Add(SettDb.P("@brxz", brxz));
                        cmd.Parameters.Add(SettDb.P("@ghzt", ghzt));
                        cmd.Parameters.Add(SettDb.P("@jsrq", ghzt == "1" ? (object)DateTime.Now : DBNull.Value));
                        cmd.Parameters.Add(SettDb.P("@ghf", ghf));
                        cmd.Parameters.Add(SettDb.P("@zlf", zlf));
                        cmd.Parameters.Add(SettDb.P("@ckf", ckf));
                        cmd.Parameters.Add(SettDb.P("@gbf", gbf));
                        cmd.Parameters.Add(SettDb.P("@xm", SettDb.Str(br, "xm")));
                        cmd.Parameters.Add(SettDb.P("@xb", SettDb.Str(br, "xb")));
                        cmd.Parameters.Add(SettDb.P("@csny", csny));
                        cmd.Parameters.Add(SettDb.P("@blh", SettDb.Str(br, "blh")));
                        cmd.Parameters.Add(SettDb.P("@zjlx", SettDb.Str(br, "zjlx")));
                        cmd.Parameters.Add(SettDb.P("@zjh", SettDb.Str(br, "zjh")));
                        cmd.Parameters.Add(SettDb.P("@lxr", SettDb.Str(br, "jjllr")));
                        cmd.Parameters.Add(SettDb.P("@lxrgx", SettDb.Str(br, "jjllrgx")));
                        cmd.Parameters.Add(SettDb.P("@lxrdh", SettDb.Str(br, "jjlldh")));
                        cmd.Parameters.Add(SettDb.P("@nlshow", nlshow));
                        cmd.Parameters.Add(SettDb.P("@ct", cardType));
                        cmd.Parameters.Add(SettDb.P("@ctn", cardTypeName));
                        cmd.Parameters.Add(SettDb.P("@jzyy", jzyy));
                        cmd.Parameters.Add(SettDb.P("@yygh", mzyyghId));
                        cmd.Parameters.Add(SettDb.P("@user", SettConstants.ApiUser));
                        cmd.ExecuteNonQuery();
                    }
                    if (xmList.Count > 0)
                    {
                        if (ghzt == "0")
                        {
                            foreach (var xm in xmList)
                            {
                                InsertXm(conn, tx, orgId, ghnm, patid, brxz, mjzbz, ys, ks, ysmc, ksmc, xm, "0");
                            }
                        }
                        else
                        {
                            jsnm = SettDb.GetNewId(conn, tx, "mz_js");
                            decimal mxzje = 0m;
                            foreach (var xm in xmList)
                            {
                                mxzje += xm.je;
                                InsertXm(conn, tx, orgId, ghnm, patid, brxz, mjzbz, ys, ks, ysmc, ksmc, xm, "1");
                                using (var cmd = new SqlCommand(@"insert into mz_jsmx (jsmxnm, jsnm, mxnm, sl, jslx, jyje, OrganizeId, zt, CreatorCode, CreateTime)
values (@id, @jsnm, @mxnm, @sl, '0', @je, @org, '1', @user, getdate())", conn, tx))
                                {
                                    cmd.Parameters.Add(SettDb.P("@id", SettDb.GetNewId(conn, tx, "mz_jsmx")));
                                    cmd.Parameters.Add(SettDb.P("@jsnm", jsnm));
                                    cmd.Parameters.Add(SettDb.P("@mxnm", xm.xmnm));
                                    cmd.Parameters.Add(SettDb.P("@sl", xm.sl));
                                    cmd.Parameters.Add(SettDb.P("@je", xm.je));
                                    cmd.Parameters.Add(SettDb.P("@org", orgId));
                                    cmd.Parameters.Add(SettDb.P("@user", SettConstants.ApiUser));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            if (mxzje != (fee.zje ?? 0m))
                                throw new FailException("Tổng tiền quyết toán bất thường: " + mxzje + " không bằng " + fee.zje);
                            using (var cmd = new SqlCommand(@"insert into mz_js (jsnm, OrganizeId, patid, brxz, jslx, fph, jszt, ghnm, xm, xb, csny, blh, zjh, zjlx, jzsj, jmbl, jmje, xjzf, zje, xjzffs, ysk, zl, ybjslsh, isjm, zlfy, zffy, flzffy, jzfy, zhjz, cxjsnm, zt, CreatorCode, CreateTime)
values (@jsnm, @org, @patid, @brxz, '0', @fph, 1, @ghnm, @xm, @xb, @csny, @blh, @zjh, @zjlx, @jzsj, @jmbl, @jmje, @xjzf, @zje, @xjzffs, @ysk, @zl, @ybjslsh, @isjm, 0, 0, 0, 0, 0, 0, '1', @user, getdate())", conn, tx))
                            {
                                cmd.Parameters.Add(SettDb.P("@jsnm", jsnm));
                                cmd.Parameters.Add(SettDb.P("@org", orgId));
                                cmd.Parameters.Add(SettDb.P("@patid", patid));
                                cmd.Parameters.Add(SettDb.P("@brxz", brxz));
                                cmd.Parameters.Add(SettDb.P("@fph", fph));
                                cmd.Parameters.Add(SettDb.P("@ghnm", ghnm));
                                cmd.Parameters.Add(SettDb.P("@xm", SettDb.Str(br, "xm")));
                                cmd.Parameters.Add(SettDb.P("@xb", SettDb.Str(br, "xb")));
                                cmd.Parameters.Add(SettDb.P("@csny", br["csny"]));
                                cmd.Parameters.Add(SettDb.P("@blh", SettDb.Str(br, "blh")));
                                cmd.Parameters.Add(SettDb.P("@zjh", SettDb.Str(br, "zjh")));
                                cmd.Parameters.Add(SettDb.P("@zjlx", SettDb.Str(br, "zjlx")));
                                cmd.Parameters.Add(SettDb.P("@jzsj", sfrq));
                                cmd.Parameters.Add(SettDb.P("@jmbl", fee.zkbl ?? 0m));
                                cmd.Parameters.Add(SettDb.P("@jmje", fee.zkje ?? 0m));
                                cmd.Parameters.Add(SettDb.P("@xjzf", fee.xjzfys ?? 0m));
                                cmd.Parameters.Add(SettDb.P("@zje", mxzje));
                                cmd.Parameters.Add(SettDb.P("@xjzffs", fee.zffs1));
                                cmd.Parameters.Add(SettDb.P("@ysk", fee.ssk ?? 0m));
                                cmd.Parameters.Add(SettDb.P("@zl", fee.zhaoling ?? 0m));
                                cmd.Parameters.Add(SettDb.P("@ybjslsh", fee.ybjslsh));
                                cmd.Parameters.Add(SettDb.P("@isjm", isjm));
                                cmd.Parameters.Add(SettDb.P("@user", SettConstants.ApiUser));
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
                catch (FailException)
                {
                    try { tx.Rollback(); } catch { }
                    throw;
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    throw new FailException(ex.Message);
                }
            }
            // ghlybz: bản gốc chỉ update khi ghlybz == "1" (8016 không gửi trường này)
            if (ghlybzFlag == "1")
            {
                using (var cmd = new SqlCommand("update mz_gh set ghlybz=@g where mzh=@mzh and zt='1' and OrganizeId=@org", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@g", ghly));
                    cmd.Parameters.Add(SettDb.P("@mzh", mzh));
                    cmd.Parameters.Add(SettDb.P("@org", orgId));
                    cmd.ExecuteNonQuery();
                }
            }
            return new { mzh = mzh, jsnm = jsnm, ybjsh = ybjsh, jszje = fee.zje, jsxjzf = fee.xjzfys };
        }

        private void InsertXm(SqlConnection conn, SqlTransaction tx, string orgId, int ghnm, int patid, string brxz,
            string mjzbz, string ys, string ks, string ysmc, string ksmc, FeeItem xm, string xmzt)
        {
            using (var cmd = new SqlCommand(@"insert into mz_xm (xmnm, ghnm, patid, brxz, mjzbz, xmzt, ys, ks, ysmc, ksmc, sfxm, dl, sl, dw, zfbl, zfxz, dj, je, jmbl, jmje, zt, OrganizeId, CreatorCode, CreateTime)
values (@xmnm, @ghnm, @patid, @brxz, @mjzbz, @xmzt, @ys, @ks, @ysmc, @ksmc, @sfxm, @dl, @sl, @dw, @zfbl, @zfxz, @dj, @je, 0, 0, '1', @org, @user, getdate())", conn, tx))
            {
                cmd.Parameters.Add(SettDb.P("@xmnm", xm.xmnm));
                cmd.Parameters.Add(SettDb.P("@ghnm", ghnm));
                cmd.Parameters.Add(SettDb.P("@patid", patid));
                cmd.Parameters.Add(SettDb.P("@brxz", brxz));
                cmd.Parameters.Add(SettDb.P("@mjzbz", mjzbz));
                cmd.Parameters.Add(SettDb.P("@xmzt", xmzt));
                cmd.Parameters.Add(SettDb.P("@ys", ys));
                cmd.Parameters.Add(SettDb.P("@ks", ks));
                cmd.Parameters.Add(SettDb.P("@ysmc", ysmc));
                cmd.Parameters.Add(SettDb.P("@ksmc", ksmc));
                cmd.Parameters.Add(SettDb.P("@sfxm", xm.sfxm));
                cmd.Parameters.Add(SettDb.P("@dl", xm.dl));
                cmd.Parameters.Add(SettDb.P("@sl", xm.sl));
                cmd.Parameters.Add(SettDb.P("@dw", xm.dw));
                cmd.Parameters.Add(SettDb.P("@zfbl", xm.zfbl));
                cmd.Parameters.Add(SettDb.P("@zfxz", xm.zfxz));
                cmd.Parameters.Add(SettDb.P("@dj", xm.dj));
                cmd.Parameters.Add(SettDb.P("@je", xm.je));
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                cmd.Parameters.Add(SettDb.P("@user", SettConstants.ApiUser));
                cmd.ExecuteNonQuery();
            }
        }

        // GetXmList gốc qua GetOutpatientFees / GetOutpatientFeesbyGroup.
        // Luồng API luôn có ghxm/zlxm từ lịch trực (Y024) hoặc null (Y026).
        private List<FeeItem> GetXmList(SqlConnection conn, SqlTransaction tx, string orgId, string ghxm, string zlxm,
            bool isCkf, bool isGbf, out decimal ghf, out decimal zlf, out decimal ckf, out decimal gbf)
        {
            var items = new List<FeeItem>();
            ghf = 0m; zlf = 0m; ckf = 0m; gbf = 0m;
            bool grouped = _db.SysConfig(conn, tx, orgId, "EnableZlxmGroup") == "1";
            if (grouped)
            {
                if (!string.IsNullOrWhiteSpace(ghxm))
                {
                    var rows = SfxmLookup(conn, tx, orgId, ghxm);
                    foreach (var r in rows)
                    {
                        ghf += SettDb.Dec(r, "dj") ?? 0m;
                        items.Add(ToFeeItem(conn, tx, r));
                    }
                }
                if (!string.IsNullOrWhiteSpace(zlxm))
                {
                    using (var cmd = new SqlCommand(_db.Q(@"select * from [NewtouchHIS_Base]..V_S_xt_sfxm with(nolock)
where sfxmCode in (select zlxm from mz_gh_zlxmzh with(nolock) where zt='1' and OrganizeId=@org and zhcode=@zh) and OrganizeId=@org"), conn, tx))
                    {
                        cmd.Parameters.Add(SettDb.P("@org", orgId));
                        cmd.Parameters.Add(SettDb.P("@zh", zlxm));
                        foreach (var r in SettDb.Read(cmd))
                        {
                            zlf += SettDb.Dec(r, "dj") ?? 0m;
                            items.Add(ToFeeItem(conn, tx, r));
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(ghxm))
                {
                    var r = SfxmLookup(conn, tx, orgId, ghxm).FirstOrDefault();
                    if (r == null) throw new FailException("Không tìm thấy hạng mục thu phí đăng ký khám");
                    ghf = SettDb.Dec(r, "dj") ?? 0m;
                    items.Add(ToFeeItem(conn, tx, r));
                }
                if (!string.IsNullOrWhiteSpace(zlxm))
                {
                    var r = SfxmLookup(conn, tx, orgId, zlxm).FirstOrDefault();
                    if (r == null) throw new FailException("Không tìm thấy hạng mục thu phí chẩn đoán");
                    zlf = SettDb.Dec(r, "dj") ?? 0m;
                    items.Add(ToFeeItem(conn, tx, r));
                }
            }
            if (isCkf)
            {
                var code = _db.SysConfig(conn, tx, orgId, "SFXM_GHCKF");
                var r = string.IsNullOrWhiteSpace(code) ? null : SfxmLookup(conn, tx, orgId, code).FirstOrDefault();
                if (r == null) throw new FailException("THE_CIKA_CHARGE_ITEM_WAS_NOT_FOUND");
                ckf = SettDb.Dec(r, "dj") ?? 0m;
                items.Add(ToFeeItem(conn, tx, r));
            }
            if (isGbf)
            {
                var code = _db.SysConfig(conn, tx, orgId, "SFXM_GHGBF");
                var r = string.IsNullOrWhiteSpace(code) ? null : SfxmLookup(conn, tx, orgId, code).FirstOrDefault();
                if (r == null) throw new FailException("THE_GONGBEN_CHARGE_ITEM_WAS_NOT_FOUND");
                gbf = SettDb.Dec(r, "dj") ?? 0m;
                items.Add(ToFeeItem(conn, tx, r));
            }
            return items;
        }

        private List<Dictionary<string, object>> SfxmLookup(SqlConnection conn, SqlTransaction tx, string orgId, string code)
        {
            // Bản gốc nối chuỗi sfxmCode vào SQL (ghxm từ lịch trực nội bộ, không
            // phải input trực tiếp); port dùng tham số hoá cho an toàn.
            using (var cmd = new SqlCommand(_db.Q("select * from [NewtouchHIS_Base]..V_S_xt_sfxm where sfxmCode=@code and OrganizeId=@org"), conn, tx))
            {
                cmd.Parameters.Add(SettDb.P("@code", code));
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                return SettDb.Read(cmd);
            }
        }

        private FeeItem ToFeeItem(SqlConnection conn, SqlTransaction tx, Dictionary<string, object> r)
        {
            var dj = SettDb.Dec(r, "dj") ?? 0m;
            return new FeeItem
            {
                xmnm = SettDb.GetNewId(conn, tx, "mz_xm"),
                sfxm = SettDb.Str(r, "sfxmCode"),
                dl = SettDb.Str(r, "sfdlCode"),
                dj = dj,
                sl = 1m,
                je = dj,
                dw = SettDb.Str(r, "dw"),
                zfbl = SettDb.Dec(r, "zfbl"),
                zfxz = SettDb.Str(r, "zfxz")
            };
        }
    }
}
