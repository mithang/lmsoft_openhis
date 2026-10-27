using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Common;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Infrastructure.EF;
using SqlSugar;

namespace Newtouch.Herp.Application.Implementation
{
/// <summary>
/// Đơn hàng mua sắm (Core port)
/// </summary>
public class PurchasingOrderApp: AppBase, IPurchasingOrderApp
{
private readonly ICgOrderRepo _cgOrderRepo;
private readonly ICgOrderDetailRepo _cgOrderDetailRepo;

// Original relied on binary field injection; Core port injects explicitly.
public PurchasingOrderApp(ICgOrderRepo cgOrderRepo, ICgOrderDetailRepo cgOrderDetailRepo)
{
_cgOrderRepo = cgOrderRepo;
_cgOrderDetailRepo = cgOrderDetailRepo;
}

/// <summary>
/// Gửi đơn đặt mua sắm.
/// Inlined port of SubmitGeneratingPurchaseprocess (the original
/// ProcessorFun&lt;T&gt; base does not exist in this copy): same validation,
/// same grouping by supplier (one sub-order per supplier), same header +
/// detail insert + sjsl update inside one transaction. Runs sequentially
/// (the original used Parallel.ForEach; SqlSugarScope is not thread-safe).
/// </summary>
/// <param name="cgmx"></param>
/// <param name="userCode"></param>
/// <param name="organizeId"></param>
/// <param name="remark"></param>
/// <returns></returns>
public string SubmitGeneratingPurchaseOrder(List<VCgPurchaseOrderDetailEntity> cgmx, string userCode, string organizeId, string remark)
{
if (cgmx == null) return "Chi tiết đơn mua sắm không được để trống";
foreach (var p in cgmx)
{
if (p.jxcgs <= 0) return string.Format("Số lượng tiếp tục mua phải lớn hơn 0", p.wzmc);
if (string.IsNullOrWhiteSpace(p.gysId)) return string.Format("Vui lòng chọn nhà cung cấp", p.wzmc);
if (p.zxqds > 0)
{
var zsl = cgmx.FindAll(t => t.productId == p.productId).Sum(s => s.jxcgs);
if (zsl * p.zhyz < p.zxqds) return string.Format("Không được thấp hơn số lượng đặt tối thiểu {2}", p.wzmc, p.zxqds, p.zxdwmc);
}
}

var submitTime = DateTime.Now;
var remaining = new List<VCgPurchaseOrderDetailEntity>(cgmx);
var detailEntities = new List<CgOrderDetailEntity>();
while (remaining.Count > 0)
{
var curItem = remaining.FirstOrDefault();
if (curItem == null) break;
var subOrderNo = ReceiptNoManage.GetNewOrderNoV1();
var group = remaining.FindAll(p => p.gysId == curItem.gysId);
foreach (var p in group)
{
detailEntities.Add(new CgOrderDetailEntity
{
supplierId = p.gysId,
subOrderNo = subOrderNo,
pdId = p.pdId,
productId = p.productId,
sl = p.jxcgs,
zhyz = p.zhyz,
jj = p.jj,
dwmc = p.unitName,
unitId = p.unitId,
orderProcess = (int)EnumOrderProcess.Waiting,
remark = p.remark,
zt = ((int)Enumzt.Enable).ToString(),
CreatorCode = userCode,
CreateTime = submitTime
});
remaining.Remove(p);
}
}

if (detailEntities.Count == 0) return "Vui lòng đảm bảo tồn tại chi tiết đơn mua sắm hợp lệ";
var cgOrderEntity = new CgOrderEntity
{
OrganizeId = organizeId,
CreateTime = submitTime,
CreatorCode = userCode,
orderNo = ReceiptNoManage.GetNewOrderNoV1(),
orderProcess = (int)EnumOrderProcess.Waiting,
orderType = (int)EnumOrderTypeHrp.TempOrder,
zt = ((int)Enumzt.Enable).ToString(),
remark = remark
};
if (_cgOrderRepo.Insert(cgOrderEntity) <= 0) return "Lưu đơn mua sắm chính thất bại";
// Same long-identity caveat as PurchasingPlanApp: re-read by the
// unique order number when the insert did not populate Id.
if (cgOrderEntity.Id <= 0)
{
var fresh = _cgOrderRepo.SelectData(cgOrderEntity.orderNo, organizeId);
if (fresh!= null && fresh.Id > 0) cgOrderEntity.Id = fresh.Id;
}
try
{
using (var db = new EFDbTransaction(HerpDb.GetConnection()).BeginTrans())
{
foreach (var p in detailEntities)
{
p.orderId = cgOrderEntity.Id;
db.Insert(p);
const string sql = @"
UPDATE dbo.cg_purchaseOrderDetail
SET sjsl=sjsl+@sl, LastModifyTime=@operateTime, LastModifierCode=@userCode
WHERE Id=@pdId AND zt='1'
";
db.Db.Ado.ExecuteCommand(sql,
new SugarParameter("@sl", p.sl),
new SugarParameter("@operateTime", p.CreateTime),
new SugarParameter("@userCode", p.CreatorCode),
new SugarParameter("@pdId", p.pdId));
}
db.Commit();
}
}
catch (Exception e)
{
_cgOrderRepo.Delete(cgOrderEntity);
return e.Message;
}
return "";
}

/// <summary>
/// Duyệt đơn mua sắm
/// </summary>
/// <param name="orderNo"></param>
/// <param name="orderType"></param>
/// <param name="remarks"></param>
/// <param name="userCode"></param>
/// <param name="organizeId"></param>
/// <returns></returns>
public string SubmitAuditPurchasingOrder(string orderNo, int orderType, string remarks, string userCode,
string organizeId)
{
var cgOrder = _cgOrderRepo.SelectData(orderNo, organizeId);
if (cgOrder == null || cgOrder.Id <= 0) return "Không tìm thấy đơn mua sắm hợp lệ";
if (cgOrder.orderType!= (int)EnumOrderTypeHrp.TempOrder || cgOrder.orderProcess!= (int)EnumOrderProcess.Waiting) return "Thao tác duyệt chỉ áp dụng cho đơn lưu tạm đang chờ duyệt";
switch (orderType)
{
case (int)EnumOrderTypeHrp.TempOrder:
case (int)EnumOrderTypeHrp.OfficialOrder:
case (int)EnumOrderTypeHrp.BadOrder:
return _cgOrderRepo.AuditPurchasingOrder(orderNo, orderType, userCode, organizeId, remarks) > 0
? ""
: "Thao tác thất bại";
default:
return "Thao tác không hợp lệ";
}
}
}
}
