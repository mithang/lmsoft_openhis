using EFDbTransaction = Newtouch.Infrastructure.EFDbTransaction;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Infrastructure.EF;
using System;
using System.Data.Common;
using SqlSugar;
using System.Linq;

using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Exceptions;
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

        
        /// <summary>
        /// Chứng từ xuất nhập kho Duyệt
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx">Loại chứng từ</param>
        /// <param name="dstnShzt">Kết quả duyệt</param>
        /// <returns></returns>
        public bool ApprovalStorageIoReceipt(string crkId, int djlx, string dstnShzt)
        {
            var curUser = OperatorProvider.GetCurrent();
            using (var db = new EFDbTransaction().BeginTrans())
            {
                var djsh = db.IQueryable<SysMedicineReceiptApprovalEntity>().FirstOrDefault(p => p.crkId == crkId);
                if (dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())
                {
                    if (djsh == null)   //Thao tác hủy bỏ，Phải tồn tại bản ghi duyệt chứng từ
                    {
                        throw new FailedException("Không tìm thấy bản ghi duyệt chứng từ，Hủy bỏ thất bại");
                    }
                }
                else
                {
                    if (djsh != null)   //Phí thao tác không hủy bỏ，Phải không tồn tại bản ghi duyệt chứng từ
                    {
                        throw new FailedException("Chứng từ đã duyệt，Không thể duyệt trùng lặp");
                    }
                }

                var crkdj = db.FindEntity<SysMedicineStorageIOReceiptEntity>(p => p.crkId == crkId && p.djlx == djlx && p.zt == "1");
                var djmxList = db.IQueryable<SysMedicineStorageIOReceiptDetailEntity>().Where(p => p.crkId == crkId && p.zt == "1").ToList();
                if (crkdj == null || djmxList.Count == 0)
                {
                    throw new FailedException("Chứng từ/Chi tiết chứng từ không tồn tại");
                }
                if (djmxList.Any(p => p.Sl < 0))
                {
                    throw new FailedException("Bản ghi Số lượng chi tiết không thể là Số âm，Thao tác thất bại");
                }

                djmxList = djmxList.FindAll(p => p.Sl > 0);
                if (djmxList.Count > 0)
                {
                    foreach (var djmxItem in djmxList)
                    {
                        //Xử lý lặp cho mỗi chi tiết chứng từ xuất nhập kho
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
                                    //Xuất kho ra ngoài
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
                                    //Phát thuốc theo đơn lĩnh
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
                #region Cập nhật trạng thái duyệt chứng từ
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
                else if (djsh == null)   //Xuống rồi thì phải luôn đúng
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
        /// Chứng từ xuất nhập kho Duyệt
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx">Loại chứng từ</param>
        /// <param name="dstnShzt">Kết quả duyệt</param>
        /// <returns></returns>
        public bool SubmitDrupreparation(string byId, string dstnShzt)
        {
            var curUser = OperatorProvider.GetCurrent();
            using (var db = new EFDbTransaction().BeginTrans())
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
        /// <param name="crkdj">Chứng từ xuất nhập kho</param>
        /// <param name="mx">Chi tiết chứng từ xuất nhập kho</param>
        /// <param name="dstnShzt">Kết quả duyệt</param>
        /// <param name="curUser">Thông tin người dùng đăng nhập</param>
        private void Yaopinruku(Newtouch.Infrastructure.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác：Duyệt đạt
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
                    ypkw = _sysPharmacyDepartmentMedicineRepo.GetYpkw(mx.Ypdm, crkdj.Rkbm, curUser.OrganizeId),  //Thông tin thuốc bộ phận
                    kzbz = 0,
                    tybz = 0,
                    crkmxId = mx.crkmxId,
                    jj = mx.jj,
                    zhyz = mx.Rkzhyz,   //Bộ phận kho thuốc này Thuốc của Hệ số chuyển đổi
                    cd = mx.cd, //Danh mục nơi sản xuất
                };
                entity.Create();
                db.Insert(entity);
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Rejected).ToString())   //Loại thao tác：Duyệt không đạt
            {

            }
            else if (crkdj.shzt == ((int)EnumDjShzt.Approved).ToString() && dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())   //Loại thao tác：Hủy bỏ
            {
                var entity = db.IQueryable<SysMedicineStockInfoEntity>().Single(p => p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Rkbm && p.pc == mx.pc && p.ph == mx.Ph);
                if (entity.djsl > 0)
                {
                    throw new FailedException("Tồn tại tồn kho đóng băng，Không thể hủy bỏ");
                }
                if (entity.kcsl < mx.Sl * mx.Rkzhyz)   //tức Tồn kho đã thay đổi thì không thể hủy bỏ
                {
                    throw new FailedException("Tồn kho kho thuốc không đủ，Không thể hủy bỏ");
                }
                entity.kcsl -= mx.Sl * mx.Rkzhyz;
                db.Update(entity);
            }
        }

        /// <summary>
        /// Xuất kho ra ngoài
        /// </summary>
        /// <param name="db"></param>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        private void Waibuchuku(Newtouch.Infrastructure.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác：Duyệt đạt
            {
                var entities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm
                  && p.yfbmCode == crkdj.Ckbm
                  && p.OrganizeId == curUser.OrganizeId
                  && p.ph == mx.Ph
                  && p.pc == mx.pc
                  && p.zt == ((int)EnumKCZT.Enabled).ToString());
                if (entities == null || !entities.Any())
                {
                    throw new FailedException("Trong kho đích không tìm thấy thuốc lô/số lô chỉ định,Duyệt thất bại");
                }
                if (entities.Where(p => p.kcsl > 0 && p.djsl > 0).Sum(p => p.kcsl) < mx.Sl * mx.Ckzhyz)
                {
                    throw new FailedException("Tồn kho kho đích không đủ，Duyệt thất bại");
                }
                var sysl = mx.Sl * mx.Ckzhyz;
                foreach (var ckbmkcEntity in entities)
                {
                    if (sysl <= 0) break;
                    var curMin = ckbmkcEntity.kcsl <= ckbmkcEntity.djsl ? ckbmkcEntity.kcsl : ckbmkcEntity.djsl;//Lấy số nhỏ nhất
                    if (curMin <= 0) continue;
                    if (curMin >= sysl)
                    {
                        ckbmkcEntity.kcsl -= sysl;  //Tồn kho bộ phận xuất kho Trừ tồn kho
                        ckbmkcEntity.djsl -= sysl;  //Khi gửi phát thuốc sẽ đóng băng số lượng
                        sysl = 0;
                    }
                    else
                    {
                        ckbmkcEntity.kcsl -= curMin;  //Tồn kho bộ phận xuất kho Trừ tồn kho
                        ckbmkcEntity.djsl -= curMin;  //Khi gửi phát thuốc sẽ đóng băng số lượng
                        sysl = sysl - curMin;
                    }
                    db.Update(ckbmkcEntity);
                }
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Rejected).ToString())   //Loại thao tác：Duyệt không đạt
            {
                //Số lượng đóng băng cần trả lại
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
            else if (crkdj.shzt == ((int)EnumDjShzt.Approved).ToString() && dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())   //Loại thao tác：Hủy bỏ
            {
                var ckbmkcEntity = db.FindEntity<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm
                    && p.zt == ((int)EnumKCZT.Enabled).ToString()
                    && p.ph == mx.Ph
                    && p.OrganizeId == curUser.OrganizeId
                    && p.yfbmCode == crkdj.Ckbm
                    && p.pc == mx.pc);
                if (ckbmkcEntity == null) throw new FailedException("Bộ phận nhập kho gốc không tìm thấy tồn kho lô/số lô này");
                ckbmkcEntity.kcsl += mx.Sl * mx.Ckzhyz;
                db.Update(ckbmkcEntity);
            }
        }

        /// <summary>
        /// Xuất kho ra ngoài
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        /// <param name="db"></param>
        private void Waibucuku(Newtouch.Infrastructure.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác：Duyệt đạt
            {
                var entity = db.IQueryable<SysMedicineStockInfoEntity>().Single(p => p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Ckbm && p.pc == mx.pc);
                if (entity.kcsl < mx.Sl * mx.Ckzhyz)
                {
                    throw new FailedException("Tồn kho kho thuốc không đủ，Duyệt thất bại");
                }
                entity.kcsl -= mx.Sl * mx.Ckzhyz;
                entity.djsl -= mx.Sl * mx.Ckzhyz;   //Khi xin xuất kho sẽ đóng băng
                db.Update(entity);
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Rejected).ToString())   //Loại thao tác：Duyệt không đạt
            {
                //Số lượng đóng băng cần trả lại
                var entity = db.IQueryable<SysMedicineStockInfoEntity>().Single(p => p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Ckbm && p.pc == mx.pc);
                //Trả lại số lượng đóng băng
                entity.djsl -= mx.Sl * mx.Ckzhyz;  //Phần này không còn bị đóng băng，Không trả cho nhà cung cấp。Có thể dùng được
                db.Update(entity);
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.Approved).ToString() && dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())   //Loại thao tác：Hủy bỏ
            {
                var entity = db.IQueryable<SysMedicineStockInfoEntity>().Single(p => p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Ckbm && p.pc == mx.pc);
                //if (entity.djsl > 0)
                //{
                //    throw new FailedException("Tồn tại tồn kho đóng băng，Không thể hủy bỏ");
                //}
                //else if (entity.kcsl < mx.Sl)   //tức Tồn kho đã thay đổi thì không thể hủy bỏ
                //{
                //    throw new FailedException("Tồn kho không đủ，Không thể hủy bỏ");
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
        private void Zhijiefayao(Newtouch.Infrastructure.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            UnfreezeAndSubtractKcsl(db, crkdj, mx, dstnShzt, curUser);
        }

        /// <summary>
        /// Phát thuốc theo đơn lĩnh
        /// </summary>
        /// <param name="db"></param>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        private void Shenlingfayao(Newtouch.Infrastructure.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            UnfreezeAndSubtractKcsl(db, crkdj, mx, dstnShzt, curUser);
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác：Duyệt đạt
            {
            }
            else
            {
                var sldmxEntity = db.IQueryable<SysMedicineRequisitionDetailEntity>().SingleOrDefault(p => p.sldmxId == mx.sldmxId);//Đơn lĩnh liên kết Xử lý trạng thái
                if (sldmxEntity == null) throw new FailedException("Thao tác thất bại，Ngoại lệ chi tiết đơn lĩnh liên kết");
                var sldEntity = db.IQueryable<SysMedicineRequisitionEntity>().SingleOrDefault(p => p.sldId == sldmxEntity.sldId);
                if (sldEntity == null) throw new FailedException("Thao tác thất bại，Ngoại lệ thông tin chính đơn lĩnh liên kết");
                var sldmxList = db.IQueryable<SysMedicineRequisitionDetailEntity>().Where(p => p.sldId == sldEntity.sldId).ToList();//Tất cả bản ghi chi tiết thuộc đơn lĩnh này
                switch (sldEntity.ffzt)
                {
                    //Hủy bỏ và không đạt Có Xử lý giống nhau
                    case (int)EnumSLDDeliveryStatus.None:
                        throw new FailedException("Thao tác thất bại，Đơn lĩnh chưa phát thuốc。");
                    case (int)EnumSLDDeliveryStatus.Abandon:
                        throw new FailedException("Thao tác thất bại，Đơn lĩnh đã chấm dứt，Không thay đổi trạng thái phát của nó nữa。");
                }

                sldmxEntity.yfsl -= mx.Sl;
                if (sldmxEntity.yfsl < 0) throw new FailedException("Thao tác thất bại，Số lượng chi tiết đơn lĩnh không cho phép là Số âm");
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
        private void Neibufayaotuihui(Newtouch.Infrastructure.EFDbTransaction db, SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity mx, string dstnShzt, OperatorModel curUser)
        {
            UnfreezeAndSubtractKcsl(db, crkdj, mx, dstnShzt, curUser);
        }

        /// <summary>
        /// Phát thuốc Giải đông và giảm tồn kho（Xuất kho trực tiếp、Xuất kho theo đơn lĩnh、Trả lại phát thuốc nội bộ）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="crkdj"></param>
        /// <param name="mx"></param>
        /// <param name="dstnShzt"></param>
        /// <param name="curUser"></param>
        private void UnfreezeAndSubtractKcsl(Newtouch.Infrastructure.EFDbTransaction db,
            SysMedicineStorageIOReceiptEntity crkdj,
            SysMedicineStorageIOReceiptDetailEntity mx,
            string dstnShzt,
            OperatorModel curUser)
        {
            if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Approved).ToString())   //Loại thao tác：Duyệt đạt
            {
                var ckbmkcEntities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm
                && p.OrganizeId == curUser.OrganizeId
                && p.yfbmCode == crkdj.Ckbm
                && p.pc == mx.pc
                && p.ph == mx.Ph
                && p.zt == ((int)EnumKCZT.Enabled).ToString());
                if (ckbmkcEntities == null || !ckbmkcEntities.Any())
                {
                    throw new FailedException(string.Format("Mã là【{0}】thuốc không tra được số lô chỉ định【{1}】Lô【{2}】", mx.Ypdm, mx.Ph, mx.pc));
                }
                var effectiveData = ckbmkcEntities.Where(p => p.kcsl > 0 && p.djsl > 0);
                if (!effectiveData.Any())
                {
                    throw new FailedException(string.Format("Mã là【{0}】thuốc，Tồn kho kho thuốc bằng 0，Duyệt thất bại", mx.Ypdm));
                }
                if (effectiveData.Sum(p => p.kcsl) < mx.Sl * mx.Ckzhyz)
                {
                    throw new FailedException(string.Format("Mã là【{0}】thuốc，Tồn kho kho thuốc không đủ，Duyệt thất bại", mx.Ypdm));
                }
                var sysl = mx.Sl * mx.Ckzhyz;

                foreach (var ckbmkcEntity in ckbmkcEntities)
                {
                    if (sysl <= 0) break;
                    var curMin = ckbmkcEntity.kcsl <= ckbmkcEntity.djsl ? ckbmkcEntity.kcsl : ckbmkcEntity.djsl;//Lấy số nhỏ nhất
                    if (curMin <= 0) continue;
                    if (curMin >= sysl)
                    {
                        ckbmkcEntity.kcsl -= sysl;  //Tồn kho bộ phận xuất kho Trừ tồn kho
                        ckbmkcEntity.djsl -= sysl;  //Khi gửi phát thuốc sẽ đóng băng số lượng
                        sysl = 0;
                    }
                    else
                    {
                        ckbmkcEntity.kcsl -= curMin;  //Tồn kho bộ phận xuất kho Trừ tồn kho
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

                if (rkbmkcEntity != null)//Nhà thuốc Tăng tồn kho
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
                        ypkw = _sysPharmacyDepartmentMedicineRepo.GetYpkw(mx.Ypdm, crkdj.Rkbm, curUser.OrganizeId),  //Thông tin thuốc bộ phận
                        kzbz = 0,
                        tybz = 0,
                        crkmxId = mx.crkmxId,
                        jj = mx.jj,//Bảng tồn kho lưu giá nhập đơn vị kho thuốc
                        zhyz = mx.Ckzhyz,   //Bộ phận nhà thuốc này Thuốc của Hệ số chuyển đổi
                        cd = mx.cd, //Danh mục nơi sản xuất
                    };
                    entity.Create();
                    db.Insert(entity);
                }
            }
            else if (crkdj.shzt == ((int)EnumDjShzt.WaitingApprove).ToString() && dstnShzt == ((int)EnumDjShzt.Rejected).ToString())   //Loại thao tác：Duyệt không đạt
            {
                //Số lượng đóng băng cần trả lại
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
            else if (crkdj.shzt == ((int)EnumDjShzt.Approved).ToString() && dstnShzt == ((int)EnumDjShzt.Cancelled).ToString())   //Loại thao tác：Hủy bỏ
            {
                #region Nhập kho lúc đó của Liên quan Đếm
                var ckbmkcEntities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm && p.zt == ((int)EnumKCZT.Enabled).ToString() && p.ph == mx.Ph && p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Ckbm && p.pc == mx.pc);
                var rkbmkcEntities = db.IQueryable<SysMedicineStockInfoEntity>(p => p.ypdm == mx.Ypdm && p.zt == ((int)EnumKCZT.Enabled).ToString() && p.ph == mx.Ph && p.OrganizeId == curUser.OrganizeId && p.yfbmCode == crkdj.Rkbm && p.pc == mx.pc);
                #endregion
                if (ckbmkcEntities == null || !ckbmkcEntities.Any()) throw new FailedException(string.Format("Mã là【{0}】thuốc bộ phận phát thuốc gốc không tìm thấy số lô【{1}】Lô【{2}】Tồn kho", mx.Ypdm, mx.Ph, mx.pc));
                if (rkbmkcEntities == null || !rkbmkcEntities.Any()) throw new FailedException(string.Format("Mã là【{0}】thuốc bộ phận nhập kho gốc không tìm thấy số lô này【{1}】Lô【{2}】Tồn kho", mx.Ypdm, mx.Ph, mx.pc));
                var rkSysl = mx.Sl * mx.Ckzhyz;
                var ckSysl = rkSysl;
                if ((rkbmkcEntities.Where(p => p.kcsl > 0 && p.djsl >= 0).Sum(p => p.kcsl) - rkbmkcEntities.Where(p => p.kcsl > 0 && p.djsl >= 0).Sum(p => p.djsl)) < rkSysl)
                {
                    throw new FailedException(string.Format("Mã là【{0}】thuốc tồn kho nhà thuốc không đủ，Không thể hủy bỏ", mx.Ypdm));   //Nhà thuốc không trả lại được nữa
                }
                foreach (var rkbmkcEntity in rkbmkcEntities)//Số tồn kho bộ phận nhập kho phải trả về
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
            var param = new SugarParameter[]
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@ypCode", ypCode)
            };
            return FirstOrDefault<int>(@"SELECT ISNULL(CONVERT(INT,dbo.f_getyfbmZhyz(@yfbmCode, @ypCode, @OrganizeId)),0) zhyz; ", param);
        }
    }
}
