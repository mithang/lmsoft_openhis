using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineReceiptDmnService : DmnServiceBase, ISysMedicineReceiptDmnService
    {
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacyDepartmentMedicineRepo;
        private readonly ISysMedicineStockInfoRepo _sysMedicineStockInfoRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;

        public SysMedicineReceiptDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Duyệt phiếu xuất nhập kho
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx">Loại phiếu</param>
        /// <param name="dstnShzt">Kết quả duyệt</param>
        /// <returns></returns>
        public bool ApprovalStorageIoReceipt(string crkId, int djlx, string dstnShzt)
        {
            var curUser = OperatorProvider.GetCurrent();
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                var djsh = db.IQueryable<SysMedicineReceiptApprovalEntity>().FirstOrDefault(p => p.crkId == crkId);
                if (dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())
                {
                    if (djsh == null)   //Thao tác hủy bỏ, phải tồn tại bản ghi duyệt phiếu
                    {
                        throw new FailedException("未找到单据审核记录，作废失败");
                    }
                }
                else
                {
                    if (djsh != null)   //Thao tác phí không phải hủy bỏ, phải không tồn tại bản ghi duyệt phiếu
                    {
                        throw new FailedException("单据已审核，不能重复审核");
                    }
                }

                var crkdj = db.FindEntity<SysMedicineStorageIOReceiptEntity>(p => p.crkId == crkId && p.djlx == djlx && p.zt == "1");
                var djmxList = db.IQueryable<SysMedicineStorageIOReceiptDetailEntity>().Where(p => p.crkId == crkId && p.zt == "1").ToList();
                if (crkdj == null || djmxList.Count == 0)
                {
                    throw new FailedException("单据/单据明细不存在");
                }
                if (djmxList.Any(p => p.Sl < 0))
                {
                    throw new FailedException("明细数量不能为负数记录，操作失败");
                }

                djmxList = djmxList.FindAll(p => p.Sl > 0);
                if (djmxList.Count > 0)
                {
                    foreach (var djmxItem in djmxList)
                    {
                        //Xử lý vòng lặp cho từng chi tiết phiếu xuất nhập kho
                        switch (djlx)
                        {
                            case (int)EnumDanJuLX.yaopinruku:
                                {
                                    //Nhập kho thuốc
                                    Yaopinruku(db, crkdj, djmxItem, dstnShzt, curUser);
                                    crkdj.Rksj = DateTime.Now;
                                }
                                break;
                            case (int)EnumDanJuLX.waibucuku:
                                {
                                    //Xuất kho ngoài
                                    Waibuchuku(db, crkdj, djmxItem, dstnShzt, curUser);
                                    crkdj.Cksj = DateTime.Now;
                                }
                                break;
                            case (int)EnumDanJuLX.zhijiefayao:
                                {
                                    //Phát thuốc trực tiếp
                                    Zhijiefayao(db, crkdj, djmxItem, dstnShzt, curUser);
                                    crkdj.Cksj = DateTime.Now;
                                    crkdj.Rksj = DateTime.Now;
                                }
                                break;
                            case (int)EnumDanJuLX.shenlingfayao:
                                {
                                    //Phát thuốc theo phiếu lĩnh
                                    Shenlingfayao(db, crkdj, djmxItem, dstnShzt, curUser);
                                    crkdj.Cksj = DateTime.Now;
                                    crkdj.Rksj = DateTime.Now;
                                }
                                break;
                            case (int)EnumDanJuLX.neibufayaotuihui:
                                {
                                    //Trả lại phát thuốc nội bộ
                                    Neibufayaotuihui(db, crkdj, djmxItem, dstnShzt, curUser);
                                    crkdj.Cksj = DateTime.Now;
                                    crkdj.Rksj = DateTime.Now;
                                }
                                break;
                        }
                    }
                }
                #region 更新单据的审核状态
                crkdj.shzt = dstnShzt;
                crkdj.Shczy = curUser.UserCode;
                crkdj.Modify();
                db.Update(crkdj);
                #endregion

                if (dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())
                {
                    //Cập nhật
                    djsh.Qxczy = curUser.UserCode;
                    djsh.Qxsj = DateTime.Now;
                    djsh.Modify();
                    db.Update(djsh);
                }
                else if (djsh == null)   //Đã xuống thì hẳn là luôn đúng
                {
                    djsh = new SysMedicineReceiptApprovalEntity()
                    {
                        djshId = Guid.NewGuid().ToString(),
                        OrganizeId = curUser.OrganizeId,
                        crkId = crkId,
                        Shczy = curUser.UserCode,
                        Shsj = DateTime.Now,
                        zt = "1",
                        CreateTime = DateTime.Now,
                        CreatorCode = curUser.UserCode
                    };
                    db.Insert(djsh);
                }

                db.Commit();
            }
            return true;
        }
        /// <summary>
        /// Duyệt phiếu xuất nhập kho
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx">Loại phiếu</param>
        /// <param name="dstnShzt">Kết quả duyệt</param>
        /// <returns></returns>
        public bool SubmitDrupreparation(string byId, string dstnShzt)
        {
            var curUser = OperatorProvider.GetCurrent();
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                var djsh = db.IQueryable<XtksbyEntity>().FirstOrDefault(p => p.Id == byId);

                //Cập nhật
                djsh.shzt = dstnShzt;
                djsh.LastModifierCode = curUser.UserCode;
                djsh.LastModifyTime = DateTime.Now;
                djsh.Modify();
                db.Update(djsh);
                db.Commit();
            }
            return true;
        }
        #region private methods

        /// <summary>
        /// Nhập kho thuốc
        /// </summary>
        /// <param name="db">EFDbTransaction</param>
        /// <param name="crkdj">Phiếu xuất nhập kho</param>
        /// <param name="mx">Chi tiết phiếu xuất nhập kho</param>
        /// <param name="dstnShzt">Kết quả duyệt</param>
        /// <param name="curUser">Thông tin người dùng đăng nhập</param>
        private void Yaopinruku(Infrastructure.EF.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác: Duyệt đạt
            {
                var entity = new SysMedicineStockInfoEntity
                {
                    kcId = Guid.NewGuid().ToString(),
                    OrganizeId = curUser.OrganizeId,
                    yfbmCode = crkdj.Rkbm,
                    ypdm = mx.Ypdm,
                    ph = mx.Ph,
                    pc = mx.pc,
                    yxq = mx.Yxq,
                    kcsl = mx.Sl * mx.Rkzhyz,
                    djsl = 0,
                    ypkw = _sysPharmacyDepartmentMedicineRepo.GetYpkw(mx.Ypdm, crkdj.Rkbm, curUser.OrganizeId),  //Thông tin thuốc của bộ phận
                    kzbz = 0,
                    tybz = 0,
                    crkmxId = mx.crkmxId,
                    jj = mx.jj,
                    zhyz = mx.Rkzhyz,   //Hệ số chuyển đổi của thuốc đó tại bộ phận kho thuốc
                    cd = mx.cd, //Danh mục nơi sản xuất
                };
                entity.Create();
                db.Insert(entity);
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Rejected).ToString())   //Loại thao tác: Duyệt không đạt
            {

            }
            else if (crkdj.shzt == ((int)EnumDjShzt.Approved).ToString() && dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())   //Loại thao tác: Hủy bỏ
            {
                var entity = db.IQueryable<SysMedicineStockInfoEntity>().Single(p => p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Rkbm && p.pc == mx.pc && p.ph == mx.Ph);
                if (entity.djsl > 0)
                {
                    throw new FailedException("存在冻结库存，不能作废");
                }
                if (entity.kcsl < mx.Sl * mx.Rkzhyz)   //Tức là nếu tồn kho đã thay đổi thì không được hủy bỏ
                {
                    throw new FailedException("药库库存不足，不能作废");
                }
                entity.kcsl -= mx.Sl * mx.Rkzhyz;
                db.Update(entity);
            }
        }

        /// <summary>
        /// Xuất kho ngoài
        /// </summary>
        /// <param name="db"></param>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        private void Waibuchuku(Infrastructure.EF.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác: Duyệt đạt
            {
                var entities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm
                  && p.yfbmCode == crkdj.Ckbm
                  && p.OrganizeId == curUser.OrganizeId
                  && p.ph == mx.Ph
                  && p.pc == mx.pc
                  && p.zt == ((int)EnumKCZT.Enabled).ToString());
                if (entities == null || !entities.Any())
                {
                    throw new FailedException("目标库房中未找到指定批号批次的药品,审核失败");
                }
                if (entities.Where(p => p.kcsl > 0 && p.djsl > 0).Sum(p => p.kcsl) < mx.Sl * mx.Ckzhyz)
                {
                    throw new FailedException("目标库房库存不足，审核失败");
                }
                var sysl = mx.Sl * mx.Ckzhyz;
                foreach (var ckbmkcEntity in entities)
                {
                    if (sysl <= 0) break;
                    var curMin = ckbmkcEntity.kcsl <= ckbmkcEntity.djsl ? ckbmkcEntity.kcsl : ckbmkcEntity.djsl;//Lấy số nhỏ nhất
                    if (curMin <= 0) continue;
                    if (curMin >= sysl)
                    {
                        ckbmkcEntity.kcsl -= sysl;  //Tồn kho của bộ phận xuất kho, trừ tồn kho
                        ckbmkcEntity.djsl -= sysl;  //Khi gửi phát thuốc sẽ đóng băng số lượng
                        sysl = 0;
                    }
                    else
                    {
                        ckbmkcEntity.kcsl -= curMin;  //Tồn kho của bộ phận xuất kho, trừ tồn kho
                        ckbmkcEntity.djsl -= curMin;  //Khi gửi phát thuốc sẽ đóng băng số lượng
                        sysl = sysl - curMin;
                    }
                    db.Update(ckbmkcEntity);
                }
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Rejected).ToString())   //Loại thao tác: Duyệt không đạt
            {
                //Số lượng đóng băng cần trả
                var entities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm
                    && p.OrganizeId == curUser.OrganizeId
                    && p.yfbmCode == crkdj.Ckbm
                    && p.ph == mx.Ph
                    && p.pc == mx.pc
                    && p.zt == ((int)EnumKCZT.Enabled).ToString());
                if (entities != null && entities.Any())
                {
                    var sl = mx.Sl * mx.Ckzhyz;
                    if (sl <= 0) return;
                    var sysl = sl;
                    foreach (var p in entities)
                    {
                        //Trả lại số lượng đóng băng
                        if (sysl <= 0) break;
                        if (p.djsl == 0) continue;
                        if (p.djsl >= sysl)
                        {
                            p.djsl -= sysl;
                            sysl = 0;
                        }
                        else
                        {
                            p.djsl = 0;
                            sysl -= p.djsl;
                        }
                        db.Update(p);
                    }
                }
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.Approved).ToString() && dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())   //Loại thao tác: Hủy bỏ
            {
                var ckbmkcEntity = db.FindEntity<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm
                    && p.zt == ((int)EnumKCZT.Enabled).ToString()
                    && p.ph == mx.Ph
                    && p.OrganizeId == curUser.OrganizeId
                    && p.yfbmCode == crkdj.Ckbm
                    && p.pc == mx.pc);
                if (ckbmkcEntity == null) throw new FailedException("原入库部门未找到该批号批次库存");
                ckbmkcEntity.kcsl += mx.Sl * mx.Ckzhyz;
                db.Update(ckbmkcEntity);
            }
        }

        /// <summary>
        /// Xuất kho ngoài
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        /// <param name="db"></param>
        private void Waibucuku(Infrastructure.EF.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác: Duyệt đạt
            {
                var entity = db.IQueryable<SysMedicineStockInfoEntity>().Single(p => p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Ckbm && p.pc == mx.pc);
                if (entity.kcsl < mx.Sl * mx.Ckzhyz)
                {
                    throw new FailedException("药库库存不足，审核失败");
                }
                entity.kcsl -= mx.Sl * mx.Ckzhyz;
                entity.djsl -= mx.Sl * mx.Ckzhyz;   //Khi đề nghị xuất kho sẽ đóng băng
                db.Update(entity);
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Rejected).ToString())   //Loại thao tác: Duyệt không đạt
            {
                //Số lượng đóng băng cần trả
                var entity = db.IQueryable<SysMedicineStockInfoEntity>().Single(p => p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Ckbm && p.pc == mx.pc);
                //Trả lại số lượng đóng băng
                entity.djsl -= mx.Sl * mx.Ckzhyz;  //Phần này không còn bị đóng băng nữa, không trả lại cho nhà cung cấp. Có thể dùng được rồi
                db.Update(entity);
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.Approved).ToString() && dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())   //Loại thao tác: Hủy bỏ
            {
                var entity = db.IQueryable<SysMedicineStockInfoEntity>().Single(p => p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Ckbm && p.pc == mx.pc);
                //if (entity.djsl > 0)
                //{
                //    throw new FailedException("Tồn tại tồn kho đóng băng, không được hủy bỏ");
                //}
                //else if (entity.kcsl < mx.Sl)   //Tức là nếu tồn kho đã thay đổi thì không được hủy bỏ
                //{
                //    throw new FailedException("Tồn kho không đủ, không được hủy bỏ");
                //}
                entity.kcsl += mx.Sl * mx.Ckzhyz;
                db.Update(entity);
            }
        }

        /// <summary>
        /// Phát thuốc trực tiếp
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        private void Zhijiefayao(Infrastructure.EF.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            UnfreezeAndSubtractKcsl(db, crkdj, mx, dstnShzt, curUser);
        }

        /// <summary>
        /// Phát thuốc theo phiếu lĩnh
        /// </summary>
        /// <param name="db"></param>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        private void Shenlingfayao(Infrastructure.EF.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            UnfreezeAndSubtractKcsl(db, crkdj, mx, dstnShzt, curUser);
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác: Duyệt đạt
            {
            }
            else
            {
                var sldmxEntity = db.IQueryable<SysMedicineRequisitionDetailEntity>().SingleOrDefault(p => p.sldmxId == mx.sldmxId);//Xử lý trạng thái phiếu lĩnh liên quan
                if (sldmxEntity == null) throw new FailedException("操作失败，关联申领单明细异常");
                var sldEntity = db.IQueryable<SysMedicineRequisitionEntity>().SingleOrDefault(p => p.sldId == sldmxEntity.sldId);
                if (sldEntity == null) throw new FailedException("操作失败，关联申领单主信息异常");
                var sldmxList = db.IQueryable<SysMedicineRequisitionDetailEntity>().Where(p => p.sldId == sldEntity.sldId).ToList();//Tất cả các bản ghi chi tiết dưới phiếu lĩnh đó
                switch (sldEntity.ffzt)
                {
                    //Hủy bỏ và không đạt là xử lý giống nhau
                    case (int)EnumSLDDeliveryStatus.None:
                        throw new FailedException("操作失败，申领单未发药。");
                    case (int)EnumSLDDeliveryStatus.Abandon:
                        throw new FailedException("操作失败，已终止的申领单，不再变更其发放状态。");
                }

                sldmxEntity.yfsl -= mx.Sl;
                if (sldmxEntity.yfsl < 0) throw new FailedException("操作失败，申领单明细数量不允许为负数");
                sldEntity.ffzt = sldmxList.Any(p => p.yfsl > 0) ? (int)EnumSLDDeliveryStatus.Part : (int)EnumSLDDeliveryStatus.None;//Không thể phát hết

                db.Update(sldmxEntity);
                db.Update(sldEntity);
            }

        }

        /// <summary>
        /// Trả lại phát thuốc nội bộ
        /// </summary>
        /// <param name="db"></param>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        private void Neibufayaotuihui(Infrastructure.EF.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            UnfreezeAndSubtractKcsl(db, crkdj, mx, dstnShzt, curUser);
        }

        /// <summary>
        /// Phát thuốc, giải đông và giảm tồn kho (xuất kho trực tiếp, xuất kho theo phiếu lĩnh, trả lại phát thuốc nội bộ）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        private void UnfreezeAndSubtractKcsl(Infrastructure.EF.EFDbTransaction db,
            SysMedicineStorageIOReceiptEntity crkdj,
            SysMedicineStorageIOReceiptDetailEntity mx,
            string dstnShzt,
            OperatorModel curUser)
        {
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác: Duyệt đạt
            {
                var ckbmkcEntities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm
                && p.OrganizeId == curUser.OrganizeId
                && p.yfbmCode == crkdj.Ckbm
                && p.pc == mx.pc
                && p.ph == mx.Ph
                && p.zt == ((int)EnumKCZT.Enabled).ToString());
                if (ckbmkcEntities == null || !ckbmkcEntities.Any())
                {
                    throw new FailedException(string.Format("编码为【{0}】的药未查到指定批号【{1}】批次【{2}】", mx.Ypdm, mx.Ph, mx.pc));
                }
                var effectiveData = ckbmkcEntities.Where(p => p.kcsl > 0 && p.djsl > 0);
                if (!effectiveData.Any())
                {
                    throw new FailedException(string.Format("编码为【{0}】的药，药库库存为零，审核失败", mx.Ypdm));
                }
                if (effectiveData.Sum(p => p.kcsl) < mx.Sl * mx.Ckzhyz)
                {
                    throw new FailedException(string.Format("编码为【{0}】的药，药库库存不足，审核失败", mx.Ypdm));
                }
                var sysl = mx.Sl * mx.Ckzhyz;

                foreach (var ckbmkcEntity in ckbmkcEntities)
                {
                    if (sysl <= 0) break;
                    var curMin = ckbmkcEntity.kcsl <= ckbmkcEntity.djsl ? ckbmkcEntity.kcsl : ckbmkcEntity.djsl;//Lấy số nhỏ nhất
                    if (curMin <= 0) continue;
                    if (curMin >= sysl)
                    {
                        ckbmkcEntity.kcsl -= sysl;  //Tồn kho của bộ phận xuất kho, trừ tồn kho
                        ckbmkcEntity.djsl -= sysl;  //Khi gửi phát thuốc sẽ đóng băng số lượng
                        sysl = 0;
                    }
                    else
                    {
                        ckbmkcEntity.kcsl -= curMin;  //Tồn kho của bộ phận xuất kho, trừ tồn kho
                        ckbmkcEntity.djsl -= curMin;  //Khi gửi phát thuốc sẽ đóng băng số lượng
                        sysl = sysl - curMin;
                    }
                    db.Update(ckbmkcEntity);
                }
                var rkbmkcEntity = _sysMedicineStockInfoRepo.FindEntity(p => p.yfbmCode == crkdj.Rkbm &&
                p.OrganizeId == curUser.OrganizeId &&
                p.pc == mx.pc &&
                p.ph == mx.Ph &&
                p.ypdm == mx.Ypdm &&
                p.zt == ((int)EnumKCZT.Enabled).ToString());

                if (rkbmkcEntity != null)//Nhà thuốc tăng tồn kho
                {
                    rkbmkcEntity.kcsl += mx.Sl * mx.Ckzhyz;
                    db.Update(rkbmkcEntity);
                }
                else
                {
                    //var yp = _baseDataDmnService.GetYpDetails(curUser.OrganizeId, mx.Ypdm);
                    //Nhà thuốc thêm một thông tin tồn kho
                    var entity = new SysMedicineStockInfoEntity
                    {
                        kcId = Guid.NewGuid().ToString(),
                        OrganizeId = curUser.OrganizeId,
                        yfbmCode = crkdj.Rkbm,
                        ypdm = mx.Ypdm,
                        ph = mx.Ph,
                        pc = mx.pc,
                        yxq = mx.Yxq,
                        kcsl = mx.Sl * mx.Ckzhyz,
                        djsl = 0,
                        ypkw = _sysPharmacyDepartmentMedicineRepo.GetYpkw(mx.Ypdm, crkdj.Rkbm, curUser.OrganizeId),  //Thông tin thuốc của bộ phận
                        kzbz = 0,
                        tybz = 0,
                        crkmxId = mx.crkmxId,
                        jj = mx.jj,//Bảng tồn kho lưu giá nhập theo đơn vị kho thuốc
                        zhyz = mx.Ckzhyz,   //Hệ số chuyển đổi của thuốc đó tại bộ phận nhà thuốc
                        cd = mx.cd, //Danh mục nơi sản xuất
                    };
                    entity.Create();
                    db.Insert(entity);
                }
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Rejected).ToString())   //Loại thao tác: Duyệt không đạt
            {
                //Số lượng đóng băng cần trả
                var entities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm
                    && p.OrganizeId == curUser.OrganizeId
                    && p.yfbmCode == crkdj.Ckbm
                    && p.ph == mx.Ph
                    && p.pc == mx.pc
                    && p.zt == ((int)EnumKCZT.Enabled).ToString());
                if (entities != null && entities.Any())
                {
                    var sl = mx.Sl * mx.Ckzhyz;
                    if (sl <= 0) return;
                    var sysl = sl;
                    foreach (var p in entities.ToList())
                    {
                        //Trả lại số lượng đóng băng
                        if (sysl <= 0) break;
                        if (p.djsl == 0) continue;
                        if (p.djsl >= sysl)
                        {
                            p.djsl -= sysl;
                            sysl = 0;
                        }
                        else
                        {
                            p.djsl = 0;
                            sysl -= p.djsl;
                        }
                        db.Update(p);
                    }
                }
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.Approved).ToString() && dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())   //Loại thao tác: Hủy bỏ
            {
                #region 当时入库 的 相关 计数
                var ckbmkcEntities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm && p.zt == ((int)EnumKCZT.Enabled).ToString() && p.ph == mx.Ph && p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Ckbm && p.pc == mx.pc);
                var rkbmkcEntities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm && p.zt == ((int)EnumKCZT.Enabled).ToString() && p.ph == mx.Ph && p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Rkbm && p.pc == mx.pc);
                #endregion
                if (ckbmkcEntities == null || !ckbmkcEntities.Any()) throw new FailedException(string.Format("编码为【{0}】的药品原发药部门未找到批号【{1}】批次【{2}】库存", mx.Ypdm, mx.Ph, mx.pc));
                if (rkbmkcEntities == null || !rkbmkcEntities.Any()) throw new FailedException(string.Format("编码为【{0}】的药品原入库部门未找到该批号【{1}】批次【{2}】库存", mx.Ypdm, mx.Ph, mx.pc));
                var rkSysl = mx.Sl * mx.Ckzhyz;
                var ckSysl = rkSysl;
                if ((rkbmkcEntities.Where(p => p.kcsl > 0 && p.djsl >= 0).Sum(p => p.kcsl) - rkbmkcEntities.Where(p => p.kcsl > 0 && p.djsl >= 0).Sum(p => p.djsl)) < rkSysl)
                {
                    throw new FailedException(string.Format("编码为【{0}】的药品药房库存不足，不能作废", mx.Ypdm));   //Nhà thuốc không trả lại được nữa
                }
                foreach (var rkbmkcEntity in rkbmkcEntities)//Số tồn kho của bộ phận nhập kho cần trả về
                {
                    if (rkSysl <= 0) break;
                    if (rkbmkcEntity.kcsl <= 0) continue;
                    if (rkbmkcEntity.kcsl >= rkSysl)
                    {
                        rkbmkcEntity.kcsl -= rkSysl;
                        rkSysl = 0;
                    }
                    else
                    {
                        rkSysl = rkSysl - rkbmkcEntity.kcsl;
                        rkbmkcEntity.kcsl = 0;
                    }
                    db.Update(rkbmkcEntity);
                }
                var ckbmkcEntity = ckbmkcEntities.FirstOrDefault();
                ckbmkcEntity.kcsl += ckSysl;
                db.Update(ckbmkcEntity);
            }
        }

        #endregion

        /// <summary>
        /// Lấy hệ số chuyển đổi
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public int GetZhyz(string organizeId, string yfbmCode, string ypCode)
        {
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@ypCode", ypCode)
            };
            return FirstOrDefault<int>(@"SELECT ISNULL(CONVERT(INT,dbo.f_getyfbmZhyz(@yfbmCode, @ypCode, @OrganizeId)),0) zhyz; ", param);
        }
    }
}
