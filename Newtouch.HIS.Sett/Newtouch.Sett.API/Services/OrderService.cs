using Microsoft.Data.SqlClient;
using Newtouch.Sett.API.Models;

namespace Newtouch.Sett.API.Services
{
    /// <summary>
    /// Nhóm C: api/OrderCenter/* — hợp của 2 controller gốc cùng
    /// RoutePrefix("api/OrderCenter"): bản có xác thực (OrderController.cs)
    /// và bản UnAuthorized. 8016 không gửi token nên thực tế bản
    /// UnAuthorized phục vụ; các route trùng nhau giữ hành vi bản
    /// UnAuthorized, riêng PayOutpOrder/PayInHosOrder chỉ có ở bản có xác thực.
    ///
    /// ĐIỂM LỆCH CÓ CHỦ Ý:
    /// - OrderNoGen gốc dùng Redis counter (khóa "orderNo"); port sinh số từ
    ///   DB (max số đơn trong phút hiện tại + 1, kiểm tra trùng, thử lại khi
    ///   trùng) để tránh lẫn key Redis và phụ thuộc Redis Db.
    /// - PayOrder/PayOutpOrder/PayInHosOrder: chạy đủ bước kiểm tra trước
    ///   (PayOrderBefore + kiểm tra chi tiết đơn ngoại trú) rồi trả FAIL rõ
    ///   ràng sub_code=SETT_ENGINE_NOT_PORTED, KHÔNG GHI GÌ. Engine quyết toán
    ///   (submitOutpatCharge / discharge settle) chưa được port — đây là
    ///   blocker cho giai đoạn sau, không phải lỗi lặng. Tác dụng phụ "hủy đơn
    ///   khi thông tin đơn thuốc đã đổi" của bản gốc trong bước kiểm tra cũng
    ///   được bỏ qua có chủ ý (chỉ đọc) để Pay* không ghi staging.
    /// - LockerApply giữ nguyên quirk gốc: LockExpTime = Now - expConfig
    ///   (thời điểm đã quá hạn), chỉ flag chứ không tự sửa.
    /// - GetNewAllUnSettedList mà OrderCreateMz gốc gọi KHÔNG có trong
    ///   checkout (chỉ có GetNewUnSettedPrescriptionByMzh và
    ///   GetNewAllUnSettedListByMzh); danh sách đơn chưa quyết toán dưới đây
    ///   được tái cấu trúc từ nhánh else của GetNewAllUnSettedListByMzh
    ///   (từng dòng cfmx + từng dòng xm chưa thu phí), đủ cho logic trùng đơn
    ///   của OrderCreateMz (so khớp bộ ba cfh/cfje/sfxm).
    /// </summary>
    public class OrderService
    {
        private readonly SettDb _db;
        private static readonly object _orderNoLock = new object();

        public OrderService(SettDb db) { _db = db; }

        public ResponseBaseOld Dispatch(string action, BookingRequest req, ParaData para)
        {
            try
            {
                switch (action)
                {
                    case "OrderQuery": return OrderQuery(req, para);
                    case "OrderDetailQuery": return OrderDetailQuery(req, para);
                    case "OrderCreate": return OrderCreate(req, para);
                    case "LockerApply": return LockerApply(req, para);
                    case "PayOrder": return PayMz(req, para, "PayOrder");
                    case "PayOutpOrder": return PayMz(req, para, "PayOutpOrder");
                    case "PayInHosOrder": return PayZy(req, para);
                    case "test": return Test(req, para);
                    default: return ResponseBaseOld.Fail("Action không hỗ trợ: " + action);
                }
            }
            catch (FailException ex) { return ResponseBaseOld.Fail(ex.Message, ex.SubCode); }
            catch (Exception ex) { return ResponseBaseOld.Error(ex.Message); }
        }

        // OrderQuery gốc: yêu cầu ít nhất một trong OrderNo/CardNo/VisitNo
        // (bản gốc viết nhầm CardNo hai lần; 8016 validate cả ba).
        private ResponseBaseOld OrderQuery(BookingRequest req, ParaData para)
        {
            var orderNo = para.Get("OrderNo");
            var cardNo = para.Get("CardNo");
            var visitNo = para.Get("VisitNo");
            if (string.IsNullOrWhiteSpace(orderNo) && string.IsNullOrWhiteSpace(cardNo) && string.IsNullOrWhiteSpace(visitNo))
                return ResponseBaseOld.Fail("Thông tin khám bệnh không được để trống");
            using (var conn = _db.OpenSett())
            {
                var sql = "select * from order_info with(nolock) where organizeid=@org and zt='1'";
                var ps = new List<SqlParameter> { SettDb.P("@org", req.OrganizeId) };
                if (!string.IsNullOrWhiteSpace(orderNo)) { sql += " and OrderNo=@OrderNo"; ps.Add(SettDb.P("@OrderNo", orderNo)); }
                if (!string.IsNullOrWhiteSpace(cardNo)) { sql += " and cardno=@kh"; ps.Add(SettDb.P("@kh", cardNo)); }
                if (!string.IsNullOrWhiteSpace(visitNo)) { sql += " and VisitNo=@VisitNo"; ps.Add(SettDb.P("@VisitNo", visitNo)); }
                int stu;
                if (para.Has("OrderStu") && int.TryParse(para.Get("OrderStu"), out stu) && stu >= 0) { sql += " and OrderStu=@stu"; ps.Add(SettDb.P("@stu", stu)); }
                int typ;
                if (para.Has("OrderType") && int.TryParse(para.Get("OrderType"), out typ) && typ >= 0) { sql += " and OrderType=@typ"; ps.Add(SettDb.P("@typ", typ)); }
                sql += " order by OrderNo desc";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    var rows = SettDb.Read(cmd);
                    if (rows.Count == 0) return ResponseBaseOld.Fail("Không tìm thấy đơn hàng");
                    return ResponseBaseOld.Ok(rows);
                }
            }
        }

        private ResponseBaseOld OrderDetailQuery(BookingRequest req, ParaData para)
        {
            var orderNo = para.Get("OrderNo");
            if (string.IsNullOrWhiteSpace(orderNo)) return ResponseBaseOld.Fail("Thông tin quan trọng(OrderNo) không được để trống");
            using (var conn = _db.OpenSett())
            {
                return ResponseBaseOld.Ok(OrderDetail(conn, orderNo, req.OrganizeId, req.AppId));
            }
        }

        // Trả {mzList} hoặc {zyList} đúng hình OrderDetailVO gốc.
        private object OrderDetail(SqlConnection conn, string orderNo, string orgId, string appId)
        {
            Dictionary<string, object> info;
            using (var cmd = new SqlCommand("select top 1 * from order_info with(nolock) where OrderNo=@no and OrganizeId=@org and zt='1'", conn))
            {
                cmd.Parameters.Add(SettDb.P("@no", orderNo));
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                info = SettDb.ReadOne(cmd);
            }
            if (info == null) throw new FailException("Không tìm thấy đơn hàng hợp lệ");
            var orderType = SettDb.Int(info, "OrderType");
            if (orderType == SettConstants.OrderTypeMz)
            {
                var sql = @"select a.OrderNo,a.mzh,a.cfh,a.cfje,b.cflxmc cfmc,a.cflx,b.ksmc,b.ysmc,b.brxz,a.sfxmmc,a.sfxm,a.cfxmmx
from order_mz a with(nolock)
left join mz_cf b with(nolock) on a.cfh=b.cfh and a.OrganizeId=b.OrganizeId and b.zt='1'
where a.orderno=@no and a.organizeId=@org and a.zt='1'";
                var ps = new List<SqlParameter> { SettDb.P("@no", orderNo), SettDb.P("@org", orgId) };
                if (!string.IsNullOrWhiteSpace(appId)) { sql += " and a.CreatorCode=@app"; ps.Add(SettDb.P("@app", appId)); }
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    return new { mzList = SettDb.Read(cmd) };
                }
            }
            if (orderType == SettConstants.OrderTypeZy)
            {
                var sql = @"select a.OrderNo,a.zyh,a.dlcode,a.dlmc,sum(a.je)je
from order_zy a with(nolock)
where a.orderno=@no and a.organizeId=@org and a.zt='1'";
                var ps = new List<SqlParameter> { SettDb.P("@no", orderNo), SettDb.P("@org", orgId) };
                if (!string.IsNullOrWhiteSpace(appId)) { sql += " and a.CreatorCode=@app"; ps.Add(SettDb.P("@app", appId)); }
                sql += " group by a.OrderNo,a.zyh,a.dlcode,a.dlmc";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(ps.ToArray());
                    return new { zyList = SettDb.Read(cmd) };
                }
            }
            throw new FailException("Loại đơn hàng bất thường");
        }

        private ResponseBaseOld OrderCreate(BookingRequest req, ParaData para)
        {
            var cardNo = para.Get("CardNo");
            var visitNo = para.Get("VisitNo");
            int orderType;
            if (string.IsNullOrWhiteSpace(cardNo) || string.IsNullOrWhiteSpace(visitNo) || !int.TryParse(para.Get("OrderType"), out orderType))
                return ResponseBaseOld.Fail("Thông tin quan trọng(kh,OrderType) không được để trống");
            int patId = 0;
            int.TryParse(para.Get("patId"), out patId);
            var dzId = para.Get("dzId");
            using (var conn = _db.OpenSett())
            {
                if (orderType == SettConstants.OrderTypeMz) return ResponseBaseOld.Ok(OrderCreateMz(conn, cardNo, visitNo, req.OrganizeId, Terminal(req), patId, dzId));
                if (orderType == SettConstants.OrderTypeZy) return ResponseBaseOld.Ok(OrderCreateZy(conn, cardNo, visitNo, req.OrganizeId, Terminal(req)));
                return ResponseBaseOld.Fail("Loại đơn hàng bất thường");
            }
        }

        // Terminal: bản gốc dùng AppId làm terminal (LastModifierCode/AppId).
        // 8016 gửi AppId của app gọi (qua BuildBookRequest).
        private string Terminal(BookingRequest req)
        {
            return string.IsNullOrWhiteSpace(req.AppId) ? SettConstants.ApiUser : req.AppId;
        }

        private class UnsettledItem
        {
            public string cfh;
            public string cfnm;
            public string sfxm;
            public string sfxmmc;
            public decimal zje;
            public string cflx;
            public string yzlx;
            public string kh;
        }

        // Tái cấu trúc GetNewAllUnSettedList: đơn thuốc chưa thu phí (cfzt='0')
        // của mzh + hạng mục xm chưa thu phí (xmzt='0') chưa gắn đơn đã thu.
        private List<UnsettledItem> GetUnsettledMz(SqlConnection conn, string mzh, string orgId)
        {
            var items = new List<UnsettledItem>();
            var sql = _db.Q(@"select cf.cfh, cast(cf.cfnm as varchar(20)) cfnm, mx.yp sfxm, yp.ypmc sfxmmc, mx.je zje, cf.cflx, '1' yzlx, gh.kh
from mz_cf cf with(nolock)
inner join mz_cfmx mx with(nolock) on cf.cfnm = mx.cfnm and mx.OrganizeId = cf.OrganizeId and mx.zt = '1'
inner join mz_gh gh with(nolock) on gh.ghnm = cf.ghnm and gh.zt = '1' and gh.OrganizeId = cf.OrganizeId
left join [NewtouchHIS_Base].dbo.V_C_xt_yp yp with(nolock) on yp.ypCode = mx.yp and yp.OrganizeId = cf.OrganizeId and yp.zt = '1'
where cf.OrganizeId = @org and cf.zt = '1' and cf.cfzt = '0' and gh.mzh = @mzh
union all
select cf.cfh, cast(xm.cfnm as varchar(20)) cfnm, xm.sfxm sfxm, sfxm.sfxmmc sfxmmc, xm.je zje, cf.cflx, '2' yzlx, gh.kh
from mz_xm xm with(nolock)
inner join mz_gh gh with(nolock) on gh.ghnm = xm.ghnm and gh.OrganizeId = xm.OrganizeId and gh.zt = '1'
left join mz_cf cf with(nolock) on cf.cfnm = xm.cfnm and cf.OrganizeId = xm.OrganizeId
left join [NewtouchHIS_Base].dbo.V_S_xt_sfxm sfxm with(nolock) on sfxm.sfxmCode = xm.sfxm and sfxm.OrganizeId = xm.OrganizeId
where xm.OrganizeId = @org and xm.zt = '1' and xm.xmzt = '0' and gh.mzh = @mzh
and (cf.zt is null or (cf.zt = '1' and cf.cfzt = '0'))
order by cfh");
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                cmd.Parameters.Add(SettDb.P("@mzh", mzh));
                foreach (var r in SettDb.Read(cmd))
                {
                    items.Add(new UnsettledItem
                    {
                        cfh = SettDb.Str(r, "cfh"),
                        cfnm = SettDb.Str(r, "cfnm"),
                        sfxm = SettDb.Str(r, "sfxm"),
                        sfxmmc = SettDb.Str(r, "sfxmmc"),
                        zje = SettDb.Dec(r, "zje") ?? 0m,
                        cflx = SettDb.Str(r, "cflx"),
                        yzlx = SettDb.Str(r, "yzlx"),
                        kh = SettDb.Str(r, "kh")
                    });
                }
            }
            return items;
        }

        private object OrderCreateMz(SqlConnection conn, string kh, string mzh, string orgId, string appId, int patId, string dzId)
        {
            var cfmxData = GetUnsettledMz(conn, mzh, orgId);
            if (cfmxData.Count == 0) throw new FailException("Không tìm thấy hóa đơn chờ thanh toán");
            if (cfmxData[0].kh != kh) throw new FailException("Thông tin hóa đơn không khớp thông tin thẻ khám bệnh");
            ExpOrder(conn, mzh, orgId, appId);
            var unpayMxArr = cfmxData.Select(p => p.cfh).ToArray();
            var orders = ReadOrders(conn, mzh, kh, orgId);
            if (orders.Count > 0)
            {
                var orderNos = orders.Select(o => SettDb.Str(o, "OrderNo")).ToArray();
                var orderMx = ReadOrderMz(conn, orderNos, orgId);
                var paidNos = orders.Where(o => SettDb.Int(o, "OrderStu") == SettConstants.OrderStuYzf).Select(o => SettDb.Str(o, "OrderNo")).ToArray();
                if (paidNos.Length > 0)
                {
                    if (orderMx.Any(p => paidNos.Contains(SettDb.Str(p, "OrderNo")) && unpayMxArr.Contains(SettDb.Str(p, "cfh"))))
                        throw new FailException("Một phần đơn thuốc đã thanh toán, vui lòng làm mới hóa đơn chờ thanh toán");
                }
                var lockOrders = orders.Where(o => SettDb.Int(o, "OrderStu") == SettConstants.OrderStuZfz).ToList();
                if (lockOrders.Count > 0)
                {
                    var lockNos = lockOrders.Select(o => SettDb.Str(o, "OrderNo")).ToArray();
                    var lockMx = orderMx.Where(p => lockNos.Contains(SettDb.Str(p, "OrderNo"))).ToList();
                    var left = cfmxData.Select(p => new { cfh = p.cfh, cfje = p.zje, sfxm = p.sfxm, cfxmmx = p.cfnm }).OrderBy(p => p.cfh).ThenBy(p => p.sfxm).ToList();
                    var right = lockMx.Select(p => new { cfh = SettDb.Str(p, "cfh"), cfje = SettDb.Dec(p, "cfje") ?? 0m, sfxm = SettDb.Str(p, "sfxm"), cfxmmx = SettDb.Str(p, "cfxmmx") }).OrderBy(p => p.cfh).ThenBy(p => p.sfxm).ToList();
                    if (left.All(t => right.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.sfxm == t.sfxm)) && left.Count == right.Count)
                    {
                        if (SettDb.Str(lockOrders[0], "LastModifierCode") == appId)
                            return ToOrderVo(lockOrders[0]);
                        throw new FailException("Đơn hàng đang được thanh toán ở kênh khác, vui lòng thử lại sau.");
                    }
                    else if (!left.Any(t => right.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.cfxmmx == t.cfxmmx)))
                    {
                        // Nhánh rỗng của bản gốc: rơi qua, tạo đơn mới bên dưới.
                    }
                    else
                    {
                        if (SettDb.Str(lockOrders[0], "LastModifierCode") == appId)
                            OrderCancel(conn, lockOrders, appId, "Thông tin đơn thuốc cập nhật, hủy đơn gốc");
                        throw new FailException("Đơn hàng đang được thanh toán ở kênh khác, vui lòng thử lại sau.");
                    }
                }
                var unpayOrders = orders.Where(o => SettDb.Int(o, "OrderStu") == SettConstants.OrderStuDzf).ToList();
                if (unpayOrders.Count > 0)
                {
                    var unpayNos = unpayOrders.Select(o => SettDb.Str(o, "OrderNo")).ToArray();
                    var unpayMx = orderMx.Where(p => unpayNos.Contains(SettDb.Str(p, "OrderNo"))).Select(p => new { cfh = SettDb.Str(p, "cfh"), cfje = SettDb.Dec(p, "cfje") ?? 0m, sfxm = SettDb.Str(p, "sfxm"), cfxmmx = SettDb.Str(p, "cfxmmx") }).OrderBy(p => p.cfh).ThenBy(p => p.sfxm).ToList();
                    var unpayCf = cfmxData.Select(p => new { cfh = p.cfh, cfje = p.zje, sfxm = p.sfxm, cfxmmx = p.cfnm }).OrderBy(p => p.cfh).ThenBy(p => p.sfxm).ToList();
                    if (unpayCf.All(t => unpayMx.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.sfxm == t.sfxm)) && unpayMx.All(t => unpayCf.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.sfxm == t.sfxm)))
                        return ToOrderVo(unpayOrders[0]);
                    else if (!unpayCf.Any(t => unpayMx.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.cfxmmx == t.cfxmmx)))
                    {
                        // Nhánh rỗng của bản gốc: rơi qua, tạo đơn mới bên dưới.
                    }
                    else
                    {
                        OrderCancel(conn, unpayOrders, appId, "Thông tin đơn thuốc cập nhật, hủy đơn gốc");
                    }
                }
            }
            string orderNo;
            lock (_orderNoLock)
            {
                orderNo = OrderNoGen(conn, SettConstants.OrderTypeMz, orgId);
            }
            var amt = cfmxData.Sum(p => p.zje);
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    using (var cmd = new SqlCommand(@"insert into order_info (Id, OrderNo, CardNo, OrderType, OrderAmt, OrderStu, VisitNo, OrganizeId, patid, dzId, CreatorCode, CreateTime, zt)
values (newid(), @no, @kh, @typ, @amt, @stu, @visit, @org, @pat, @dz, @user, getdate(), '1')", conn, tx))
                    {
                        cmd.Parameters.Add(SettDb.P("@no", orderNo));
                        cmd.Parameters.Add(SettDb.P("@kh", kh));
                        cmd.Parameters.Add(SettDb.P("@typ", SettConstants.OrderTypeMz));
                        cmd.Parameters.Add(SettDb.P("@amt", amt));
                        cmd.Parameters.Add(SettDb.P("@stu", SettConstants.OrderStuDzf));
                        cmd.Parameters.Add(SettDb.P("@visit", mzh));
                        cmd.Parameters.Add(SettDb.P("@org", orgId));
                        cmd.Parameters.Add(SettDb.P("@pat", patId == 0 ? (object)DBNull.Value : patId));
                        cmd.Parameters.Add(SettDb.P("@dz", string.IsNullOrWhiteSpace(dzId) ? (object)DBNull.Value : dzId));
                        cmd.Parameters.Add(SettDb.P("@user", appId));
                        cmd.ExecuteNonQuery();
                    }
                    foreach (var item in cfmxData)
                    {
                        using (var cmd = new SqlCommand(@"insert into order_mz (Id, cfh, cfje, OrderNo, sfxm, sfxmmc, cfxmmx, OrganizeId, mzh, cflx, isyp, CreatorCode, CreateTime, zt)
values (newid(), @cfh, @cfje, @no, @sfxm, @sfxmmc, @cfxmmx, @org, @mzh, @cflx, @isyp, @user, getdate(), '1')", conn, tx))
                        {
                            cmd.Parameters.Add(SettDb.P("@cfh", item.cfh));
                            cmd.Parameters.Add(SettDb.P("@cfje", item.zje));
                            cmd.Parameters.Add(SettDb.P("@no", orderNo));
                            cmd.Parameters.Add(SettDb.P("@sfxm", item.sfxm));
                            cmd.Parameters.Add(SettDb.P("@sfxmmc", item.sfxmmc));
                            cmd.Parameters.Add(SettDb.P("@cfxmmx", item.cfnm));
                            cmd.Parameters.Add(SettDb.P("@org", orgId));
                            cmd.Parameters.Add(SettDb.P("@mzh", mzh));
                            cmd.Parameters.Add(SettDb.P("@cflx", item.cflx));
                            cmd.Parameters.Add(SettDb.P("@isyp", item.yzlx != "1" ? "0" : item.yzlx));
                            cmd.Parameters.Add(SettDb.P("@user", appId));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch { try { tx.Rollback(); } catch { } throw; }
            }
            return new { OrderNo = orderNo, CardNo = kh, VisitNo = mzh, OrderAmt = amt, OrderStu = SettConstants.OrderStuDzf, OrderType = SettConstants.OrderTypeMz, patId = patId, dzId = dzId };
        }

        private object OrderCreateZy(SqlConnection conn, string kh, string zyh, string orgId, string appId)
        {
            Dictionary<string, object> pat;
            using (var cmd = new SqlCommand("select top 1 * from zy_brjbxx with(nolock) where OrganizeId=@org and zt='1' and (zyh=@zyh or kh=@kh)", conn))
            {
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                cmd.Parameters.Add(SettDb.P("@zyh", zyh));
                cmd.Parameters.Add(SettDb.P("@kh", kh));
                pat = SettDb.ReadOne(cmd);
            }
            if (pat == null) throw new FailException("Vui lòng xác nhận thông tin bệnh nhân và trạng thái nội trú, sau đó làm thủ tục xuất khu rồi mới quyết toán");
            var zybz = SettDb.Str(pat, "zybz");
            if (zybz != SettConstants.ZybzDjz && zybz != SettConstants.ZybzYcy)
                throw new FailException("Vui lòng xác nhận thông tin bệnh nhân và trạng thái nội trú, sau đó làm thủ tục xuất khu rồi mới quyết toán");
            if (SettDb.Str(pat, "brxz") != "0")
                throw new FailException("Bệnh nhân BHYT vui lòng ra quầy làm thủ tục quyết toán xuất viện");
            var yzmxData = GetHospGroupFeeDetail(conn, zyh, orgId);
            if (yzmxData.Count == 0) throw new FailException("Không tìm thấy hóa đơn chờ thanh toán");
            decimal zje = yzmxData.Sum(p => SettDb.Dec(p, "je") ?? 0m);
            ExpOrder(conn, zyh, orgId, appId);
            var orders = ReadOrders(conn, zyh, kh, orgId);
            if (orders.Count > 0)
            {
                var orderNos = orders.Select(o => SettDb.Str(o, "OrderNo")).ToArray();
                var orderMx = ReadOrderZy(conn, orderNos, zyh, orgId);
                decimal mxzje = orderMx.Sum(p => SettDb.Dec(p, "je") ?? 0m);
                if (orders.Any(o => SettDb.Int(o, "OrderStu") == SettConstants.OrderStuYzf))
                    throw new FailException("Một phần hóa đơn đã thanh toán, vui lòng ra quầy thu phí làm thủ tục.");
                var lockOrders = orders.Where(o => SettDb.Int(o, "OrderStu") == SettConstants.OrderStuZfz).ToList();
                if (lockOrders.Count > 0)
                {
                    var lockedApp = lockOrders.FirstOrDefault(o => SettDb.Str(o, "LastModifierCode") == appId);
                    if (lockedApp == null) throw new FailException("Đơn hàng đang được thanh toán ở kênh khác, vui lòng thử lại sau.");
                    if (zje == (SettDb.Dec(lockedApp, "OrderAmt") ?? 0m) && (SettDb.Dec(lockedApp, "OrderAmt") ?? 0m) == mxzje)
                        return ToOrderVo(lockedApp);
                    OrderCancel(conn, new List<Dictionary<string, object>> { lockedApp }, appId, "Hóa đơn cập nhật, hủy đơn gốc");
                }
                var unpayOrders = orders.Where(o => SettDb.Int(o, "OrderStu") == SettConstants.OrderStuDzf).ToList();
                if (unpayOrders.Count == 1)
                {
                    if (zje == (SettDb.Dec(unpayOrders[0], "OrderAmt") ?? 0m) && mxzje == zje)
                        return ToOrderVo(unpayOrders[0]);
                    OrderCancel(conn, unpayOrders, appId, "Hóa đơn cập nhật, hủy đơn gốc");
                }
                else if (unpayOrders.Count > 1)
                {
                    OrderCancel(conn, unpayOrders, appId, "Hóa đơn cập nhật, hủy đơn gốc");
                }
            }
            string orderNo;
            lock (_orderNoLock)
            {
                orderNo = OrderNoGen(conn, SettConstants.OrderTypeZy, orgId);
            }
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    using (var cmd = new SqlCommand(@"insert into order_info (Id, OrderNo, CardNo, OrderType, OrderAmt, OrderStu, VisitNo, OrganizeId, CreatorCode, CreateTime, zt)
values (newid(), @no, @kh, @typ, @amt, @stu, @visit, @org, @user, getdate(), '1')", conn, tx))
                    {
                        cmd.Parameters.Add(SettDb.P("@no", orderNo));
                        cmd.Parameters.Add(SettDb.P("@kh", kh));
                        cmd.Parameters.Add(SettDb.P("@typ", SettConstants.OrderTypeZy));
                        cmd.Parameters.Add(SettDb.P("@amt", zje));
                        cmd.Parameters.Add(SettDb.P("@stu", SettConstants.OrderStuDzf));
                        cmd.Parameters.Add(SettDb.P("@visit", zyh));
                        cmd.Parameters.Add(SettDb.P("@org", orgId));
                        cmd.Parameters.Add(SettDb.P("@user", appId));
                        cmd.ExecuteNonQuery();
                    }
                    foreach (var item in yzmxData)
                    {
                        using (var cmd = new SqlCommand(@"insert into order_zy (Id, zyh, OrderNo, dlcode, dlmc, sfxm, sl, dj, je, jfdw, sfxmmx, zfxz, zzfbz, OrganizeId, CreatorCode, CreateTime, zt)
values (newid(), @zyh, @no, @dlcode, @dlmc, @sfxm, @sl, @dj, @je, @jfdw, @sfxmmx, @zfxz, @zzfbz, @org, @user, getdate(), '1')", conn, tx))
                        {
                            cmd.Parameters.Add(SettDb.P("@zyh", zyh));
                            cmd.Parameters.Add(SettDb.P("@no", orderNo));
                            cmd.Parameters.Add(SettDb.P("@dlcode", SettDb.Str(item, "dlCode")));
                            cmd.Parameters.Add(SettDb.P("@dlmc", SettDb.Str(item, "dlmc")));
                            cmd.Parameters.Add(SettDb.P("@sfxm", SettDb.Str(item, "sfxm")));
                            cmd.Parameters.Add(SettDb.P("@sl", SettDb.Dec(item, "sl") ?? 0m));
                            cmd.Parameters.Add(SettDb.P("@dj", SettDb.Dec(item, "dj") ?? 0m));
                            cmd.Parameters.Add(SettDb.P("@je", SettDb.Dec(item, "je") ?? 0m));
                            cmd.Parameters.Add(SettDb.P("@jfdw", SettDb.Str(item, "jfdw")));
                            cmd.Parameters.Add(SettDb.P("@sfxmmx", SettDb.Str(item, "sfxmmc")));
                            cmd.Parameters.Add(SettDb.P("@zfxz", SettDb.Str(item, "zfxzcode")));
                            cmd.Parameters.Add(SettDb.P("@zzfbz", SettDb.Str(item, "zzfbzcode")));
                            cmd.Parameters.Add(SettDb.P("@org", orgId));
                            cmd.Parameters.Add(SettDb.P("@user", appId));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch { try { tx.Rollback(); } catch { } throw; }
            }
            return new { OrderNo = orderNo, CardNo = kh, VisitNo = zyh, OrderAmt = zje, OrderStu = SettConstants.OrderStuDzf, OrderType = SettConstants.OrderTypeZy };
        }

        // Câu SQL gốc GetHospGroupFeeVOList (overload chi tiết, sfdl=null).
        private List<Dictionary<string, object>> GetHospGroupFeeDetail(SqlConnection conn, string zyh, string orgId)
        {
            var sql = _db.Q(@"select dd.dlCode,dd.dlmc,dd.sfxm,dd.sfxmmc,Convert(decimal(18,2),dd.dj)dj,sum(dd.sl)sl,dd.jfdw,dd.zfxz,dd.zzfbz,sum(dd.je)je,dd.zfxzcode,dd.zzfbzcode
from(
select dl dlCode,b.dlmc,sfxm,c.sfxmmc,a.dj,sl,jfdw,
(case a.zfxz when 1 then 'Tu chi tra' when 4 then 'Loai A' when 5 then 'Loai B' when 6 then 'Loai C' end)zfxz,
CONVERT(NUMERIC(10,2), isnull(a.dj, 0) * isnull(sl, 0)) je ,c.gg ,case isnull(zzfbz,0) when '1' then 'Co' else 'Khong' end zzfbz, a.zfxz zfxzcode, a.zzfbz zzfbzcode
from [V_C_Sys_WsfZyXmjfb] a
left join [NewtouchHIS_Base].dbo.V_S_xt_sfdl b with(nolock) on a.OrganizeId=b.OrganizeId and a.dl=b.dlCode and b.zt='1'
left join [NewtouchHIS_Base].dbo.V_S_xt_sfxm c with(nolock) on a.OrganizeId=c.OrganizeId and a.sfxm=c.sfxmcode and c.zt='1'
where zyh =@zyh and a.OrganizeId =@orgId and b.zt= '1'
union all
select dl dlCode,bb.dlmc,yp,cc.ypmc,aa.dj,sl,jfdw,
(case aa.zfxz when 1 then 'Tu chi tra' when 4 then 'Loai A' when 5 then 'Loai B' when 6 then 'Loai C' end)zfxz,
CONVERT(NUMERIC(10,2), isnull(aa.dj, 0) * isnull(sl, 0)) je ,cc.ypgg,case isnull(zzfbz,0) when '1' then 'Co' else 'Khong' end zzfbz, aa.zfxz zfxzcode, aa.zzfbz zzfbzcode
from [V_C_Sys_WsfZyYpjfb] aa
left join [NewtouchHIS_Base].dbo.V_S_xt_sfdl bb with(nolock) on aa.OrganizeId=bb.OrganizeId and aa.dl=bb.dlCode and bb.zt='1'
left join [NewtouchHIS_Base].dbo.V_C_xt_yp cc with(nolock) on aa.OrganizeId=cc.OrganizeId and aa.yp=cc.ypcode and cc.zt='1'
where zyh = @zyh and aa.OrganizeId = @orgId and bb.zt= '1'
)dd
group by dd.dlCode,dd.dlmc,dd.sfxm,dd.sfxmmc,dd.dj,dd.jfdw,dd.zfxz,dd.zzfbz,dd.zfxzcode,dd.zzfbzcode");
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(SettDb.P("@orgId", orgId));
                cmd.Parameters.Add(SettDb.P("@zyh", zyh));
                return SettDb.Read(cmd);
            }
        }

        private List<Dictionary<string, object>> ReadOrders(SqlConnection conn, string visitNo, string kh, string orgId)
        {
            using (var cmd = new SqlCommand("select * from order_info with(nolock) where VisitNo=@visit and CardNo=@kh and OrderStu<>@zf and OrganizeId=@org and zt='1'", conn))
            {
                cmd.Parameters.Add(SettDb.P("@visit", visitNo));
                cmd.Parameters.Add(SettDb.P("@kh", kh));
                cmd.Parameters.Add(SettDb.P("@zf", SettConstants.OrderStuZf));
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                return SettDb.Read(cmd);
            }
        }

        private List<Dictionary<string, object>> ReadOrderMz(SqlConnection conn, string[] orderNos, string orgId)
        {
            if (orderNos.Length == 0) return new List<Dictionary<string, object>>();
            var names = new List<string>();
            var ps = new List<SqlParameter> { SettDb.P("@org", orgId) };
            for (int i = 0; i < orderNos.Length; i++) { names.Add("@n" + i); ps.Add(SettDb.P("@n" + i, orderNos[i])); }
            using (var cmd = new SqlCommand("select * from order_mz with(nolock) where OrderNo in (" + string.Join(",", names) + ") and OrganizeId=@org and zt='1'", conn))
            {
                cmd.Parameters.AddRange(ps.ToArray());
                return SettDb.Read(cmd);
            }
        }

        private List<Dictionary<string, object>> ReadOrderZy(SqlConnection conn, string[] orderNos, string zyh, string orgId)
        {
            if (orderNos.Length == 0) return new List<Dictionary<string, object>>();
            var names = new List<string>();
            var ps = new List<SqlParameter> { SettDb.P("@org", orgId), SettDb.P("@zyh", zyh) };
            for (int i = 0; i < orderNos.Length; i++) { names.Add("@n" + i); ps.Add(SettDb.P("@n" + i, orderNos[i])); }
            using (var cmd = new SqlCommand("select * from order_zy with(nolock) where OrderNo in (" + string.Join(",", names) + ") and zyh=@zyh and OrganizeId=@org and zt='1'", conn))
            {
                cmd.Parameters.AddRange(ps.ToArray());
                return SettDb.Read(cmd);
            }
        }

        private void OrderCancel(SqlConnection conn, List<Dictionary<string, object>> orders, string appId, string msg)
        {
            foreach (var o in orders)
            {
                var memo = (SettDb.Str(o, "Memo") ?? "") + "|([" + appId + "]" + msg + "(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "))";
                using (var cmd = new SqlCommand("update order_info set OrderStu=@zf, Memo=@memo, LastModifierCode=@user, LastModifyTime=getdate() where OrderNo=@no and OrganizeId=@org", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@zf", SettConstants.OrderStuZf));
                    cmd.Parameters.Add(SettDb.P("@memo", memo));
                    cmd.Parameters.Add(SettDb.P("@user", appId));
                    cmd.Parameters.Add(SettDb.P("@no", SettDb.Str(o, "OrderNo")));
                    cmd.Parameters.Add(SettDb.P("@org", SettDb.Str(o, "OrganizeId")));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ExpOrder gốc: hủy khóa quá hạn (mặc định 5 phút) của visitNo.
        private void ExpOrder(SqlConnection conn, string visitNo, string orgId, string appId)
        {
            var sj = DateTime.Now.AddMinutes(-_db.LockedOrderExpiredMinute);
            List<Dictionary<string, object>> expired;
            using (var cmd = new SqlCommand("select OrderNo from order_info with(nolock) where VisitNo=@visit and OrderStu=@zfz and LastModifyTime<@sj and OrganizeId=@org and zt='1'", conn))
            {
                cmd.Parameters.Add(SettDb.P("@visit", visitNo));
                cmd.Parameters.Add(SettDb.P("@zfz", SettConstants.OrderStuZfz));
                cmd.Parameters.Add(SettDb.P("@sj", sj));
                cmd.Parameters.Add(SettDb.P("@org", orgId));
                expired = SettDb.Read(cmd);
            }
            foreach (var e in expired)
            {
                var no = SettDb.Str(e, "OrderNo");
                using (var cmd = new SqlCommand("update order_info set OrderStu=@zf, memo=isnull(memo,'')+'|Het han huy khoa don hang (" + appId + ":" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ")', LastModifierCode=@user, LastModifyTime=getdate() where OrderNo=@no and OrganizeId=@org", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@zf", SettConstants.OrderStuZf));
                    cmd.Parameters.Add(SettDb.P("@user", appId));
                    cmd.Parameters.Add(SettDb.P("@no", no));
                    cmd.Parameters.Add(SettDb.P("@org", orgId));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string OrderNoGen(SqlConnection conn, int orderType, string orgId)
        {
            var prefix = orderType + DateTime.Now.ToString("yyyyMMddHHmm");
            for (int attempt = 0; attempt < 20; attempt++)
            {
                int cnt;
                using (var cmd = new SqlCommand("select max(OrderNo) from order_info with(nolock) where OrderNo like @p", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@p", prefix + "%"));
                    var max = SettDb.Scalar(cmd) as string;
                    cnt = 0;
                    if (!string.IsNullOrWhiteSpace(max) && max.Length >= 5)
                    {
                        int.TryParse(max.Substring(max.Length - 5), out cnt);
                    }
                    cnt += attempt;
                }
                cnt++;
                var no = prefix + cnt.ToString().PadLeft(5, '0');
                using (var cmd = new SqlCommand("select top 1 OrderNo from order_info with(nolock) where OrderNo=@no and OrganizeId=@org", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@no", no));
                    cmd.Parameters.Add(SettDb.P("@org", orgId));
                    if (SettDb.ReadOne(cmd) == null) return no;
                }
            }
            throw new FailException("Lỗi sinh số đơn hàng (trùng lặp), vui lòng thử lại");
        }

        private object ToOrderVo(Dictionary<string, object> o)
        {
            return new
            {
                OrderNo = SettDb.Str(o, "OrderNo"),
                CardNo = SettDb.Str(o, "CardNo"),
                VisitNo = SettDb.Str(o, "VisitNo"),
                OrderAmt = SettDb.Dec(o, "OrderAmt"),
                OrderStu = SettDb.Int(o, "OrderStu"),
                OrderType = SettDb.Int(o, "OrderType"),
                patId = SettDb.Int(o, "patid"),
                dzId = SettDb.Str(o, "dzId")
            };
        }

        private ResponseBaseOld LockerApply(BookingRequest req, ParaData para)
        {
            var orderNo = para.Get("OrderNo");
            var kh = para.Get("CardNo");
            decimal amt;
            if (string.IsNullOrWhiteSpace(orderNo) || string.IsNullOrWhiteSpace(kh) || !decimal.TryParse(para.Get("OrderAmt"), out amt) || amt <= 0)
                return ResponseBaseOld.Fail("Thông tin quan trọng(OrderNo,kh) không được để trống");
            using (var conn = _db.OpenSett())
            {
                Dictionary<string, object> order;
                using (var cmd = new SqlCommand("select top 1 * from order_info with(nolock) where OrderNo=@no and OrderStu=@dzf and OrganizeId=@org and zt='1'", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@no", orderNo));
                    cmd.Parameters.Add(SettDb.P("@dzf", SettConstants.OrderStuDzf));
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    order = SettDb.ReadOne(cmd);
                }
                if (order == null) return ResponseBaseOld.Fail("Không tìm thấy đơn hàng, vui lòng xác nhận lại trạng thái đơn hàng");
                if (SettDb.Str(order, "CardNo") != kh || (SettDb.Dec(order, "OrderAmt") ?? 0m) != amt)
                    return ResponseBaseOld.Fail("Thông tin đơn hàng bất thường hoặc không khớp thông tin thẻ khám bệnh");
                var terminal = Terminal(req);
                var memo = (SettDb.Str(order, "Memo") ?? "") + "|(appid:" + terminal + " khóa đơn hàng(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "))";
                using (var cmd = new SqlCommand("update order_info set OrderStu=@zfz, Memo=@memo, LastModifierCode=@user, LastModifyTime=getdate() where OrderNo=@no and OrganizeId=@org", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@zfz", SettConstants.OrderStuZfz));
                    cmd.Parameters.Add(SettDb.P("@memo", memo));
                    cmd.Parameters.Add(SettDb.P("@user", terminal));
                    cmd.Parameters.Add(SettDb.P("@no", orderNo));
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    cmd.ExecuteNonQuery();
                }
                // Quirk gốc giữ nguyên: LockExpTime = Now - expConfig.
                return ResponseBaseOld.Ok(new
                {
                    OrderNo = orderNo,
                    OrderAmt = amt,
                    OrderStu = SettConstants.OrderStuZfz,
                    LockExpTime = DateTime.Now.AddMinutes(-_db.LockedOrderExpiredMinute)
                });
            }
        }

        // PayOrderBefore gốc (chỉ đọc): phương thức thanh toán phải tồn tại
        // trong xt_xjzffs, đơn phải đang khóa (zfz), số tiền khớp, thẻ khớp,
        // VisitNo khớp nếu gửi kèm.
        private Dictionary<string, object> PayOrderBefore(SqlConnection conn, BookingRequest req, ParaData para)
        {
            var payWay = para.Get("PayWay");
            using (var cmd = new SqlCommand("select top 1 xjzffs from xt_xjzffs with(nolock) where xjzffs=@w and zt='1'", conn))
            {
                cmd.Parameters.Add(SettDb.P("@w", payWay));
                if (SettDb.ReadOne(cmd) == null) throw new FailException("Tạm thời chưa hỗ trợ phương thức thanh toán này!");
            }
            var orderNo = para.Get("OrderNo");
            Dictionary<string, object> order;
            using (var cmd = new SqlCommand("select top 1 * from order_info with(nolock) where OrderNo=@no and OrderStu=@zfz and OrganizeId=@org and zt='1'", conn))
            {
                cmd.Parameters.Add(SettDb.P("@no", orderNo));
                cmd.Parameters.Add(SettDb.P("@zfz", SettConstants.OrderStuZfz));
                cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                order = SettDb.ReadOne(cmd);
            }
            if (order == null) throw new FailException("Thanh toán thất bại, thông tin đơn hàng đã hết hạn, vui lòng làm mới trạng thái đơn hàng");
            decimal payFee;
            if (!decimal.TryParse(para.Get("PayFee"), out payFee) || (SettDb.Dec(order, "OrderAmt") ?? 0m) != payFee)
                throw new FailException("Số tiền đơn hàng không khớp số tiền thanh toán");
            var cardNo = para.Get("CardNo");
            var visitNo = para.Get("VisitNo");
            if (SettDb.Str(order, "CardNo") != cardNo || (!string.IsNullOrWhiteSpace(visitNo) && SettDb.Str(order, "VisitNo") != visitNo))
                throw new FailException("Thông tin đơn hàng không khớp thông tin khám bệnh");
            return order;
        }

        private ResponseBaseOld PayMz(BookingRequest req, ParaData para, string action)
        {
            using (var conn = _db.OpenSett())
            {
                var order = PayOrderBefore(conn, req, para);
                if (SettDb.Int(order, "OrderType") != SettConstants.OrderTypeMz)
                    return ResponseBaseOld.Fail("Không phải đơn hàng ngoại trú");
                // PayForOutpBillOrderBefore (chỉ đọc): chi tiết đơn phải khớp
                // đơn thuốc chưa thu phí, số dòng bằng nhau.
                var orderNos = new[] { SettDb.Str(order, "OrderNo") };
                var orderMx = ReadOrderMz(conn, orderNos, req.OrganizeId);
                if (orderMx.Count == 0 || orderMx[0]["mzh"] == null || SettDb.Str(orderMx[0], "mzh") != SettDb.Str(order, "VisitNo"))
                    return ResponseBaseOld.Fail("Chi tiết đơn hàng bất thường, hủy đơn và thử lại");
                var cfxmmxList = orderMx.Select(p => SettDb.Str(p, "cfxmmx")).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                var inList = string.Join(",", cfxmmxList.Select(s => { int n; return int.TryParse(s, out n) ? n.ToString() : "-1"; }));
                int cfCount = 0;
                if (cfxmmxList.Count > 0)
                {
                    using (var cmd = new SqlCommand(@"select count(*) from mz_cf cf with(nolock)
where cf.cfnm in (" + inList + @") and (cf.zt is null or (cf.zt='1' and cf.cfzt='0'))
and exists(select 1 from order_mz b with(nolock) where cf.cfnm=b.cfxmmx and cf.zje=b.cfje and cf.OrganizeId=@org)
and cf.OrganizeId=@org", conn))
                    {
                        cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                        cfCount = Convert.ToInt32(SettDb.Scalar(cmd));
                    }
                }
                if (cfCount == 0 || cfCount != orderMx.Count)
                    return ResponseBaseOld.Fail("Thông tin đơn hàng đã hết hạn, vui lòng tạo lại đơn hàng");
                // Engine quyết toán ngoại trú (submitOutpatCharge) chưa port.
                return ResponseBaseOld.Fail("Engine quyết toán ngoại trú chưa được port (SETT_ENGINE_NOT_PORTED)", "SETT_ENGINE_NOT_PORTED");
            }
        }

        private ResponseBaseOld PayZy(BookingRequest req, ParaData para)
        {
            using (var conn = _db.OpenSett())
            {
                var order = PayOrderBefore(conn, req, para);
                if (SettDb.Int(order, "OrderType") != SettConstants.OrderTypeZy)
                    return ResponseBaseOld.Fail("Không phải đơn hàng nội trú");
                Dictionary<string, object> pat;
                using (var cmd = new SqlCommand("select top 1 zyh from zy_brjbxx with(nolock) where zyh=@zyh and OrganizeId=@org and zt='1'", conn))
                {
                    cmd.Parameters.Add(SettDb.P("@zyh", SettDb.Str(order, "VisitNo")));
                    cmd.Parameters.Add(SettDb.P("@org", req.OrganizeId));
                    pat = SettDb.ReadOne(cmd);
                }
                if (pat == null) return ResponseBaseOld.Fail("Thông tin bệnh nhân bất thường, vui lòng liên hệ quản trị viên");
                // Engine quyết toán xuất viện chưa port.
                return ResponseBaseOld.Fail("Engine quyết toán nội trú chưa được port (SETT_ENGINE_NOT_PORTED)", "SETT_ENGINE_NOT_PORTED");
            }
        }

        // test gốc: OrderDetailQuery + PushPresInfo (jsnm hardcode 88514).
        // Push bị bỏ qua có chủ ý: nó gọi CIS/PDS và ghi mz_gh.outpId, sẽ chạm
        // DB demo ngoài phạm vi staging khi chạy kiểm chứng.
        private ResponseBaseOld Test(BookingRequest req, ParaData para)
        {
            var orderNo = para.Get("OrderNo");
            if (string.IsNullOrWhiteSpace(orderNo)) return ResponseBaseOld.Fail("Thông tin quan trọng(OrderNo) không được để trống");
            using (var conn = _db.OpenSett())
            {
                return ResponseBaseOld.Ok(OrderDetail(conn, orderNo, req.OrganizeId, req.AppId));
            }
        }
    }
}
