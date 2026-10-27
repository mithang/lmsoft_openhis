using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Quản lý phiếu lĩnh (Core port, parity audit 2026-10-27).
    /// SubmitApplyOutStock is a faithful port of the original (which built
    /// one out-stock bill per warehouse and ran DeptApplyOutStockProcess);
    /// the earlier port returned an explicit stub because that process was
    /// not ported yet. It is ported now (DeptApplyOutStockProcess.cs).
    /// Receipt number: the original built it from the PROTECTED
    /// chukuzhikeshi Description via GBK pinyin initials; GBK pinyin is not
    /// available on this Linux runtime, so the prefix is the explicit
    /// "CKZKS" used by the Web-side ReceiptNoHelper for the same type.
    /// </summary>
    public class ApplyBillApp : AppBase, IApplyBillApp
    {
        private readonly IKfApplyOrderRepo _kfApplyOrderRepo;
        private readonly IKfApplyOrderDetailRepo _kfApplyOrderDetailRepo;
        private readonly IWzCrkfsRepo _crkFs;
        private readonly IKfKcxxRepo _kfKcxxRepo;
        private readonly IWzProductDmnService _wzProductDmnService;
        private readonly IKfCrkdjRepo _kfCrkdjRepo;
        private readonly IKfCrkmxRepo _kfCrkmxRepo;
        private readonly IKfKcxxDmnService _kfKcxxDmnService;

        public ApplyBillApp(IKfApplyOrderRepo kfApplyOrderRepo,
        IKfApplyOrderDetailRepo kfApplyOrderDetailRepo,
        IWzCrkfsRepo crkFs, IKfKcxxRepo kfKcxxRepo,
        IWzProductDmnService wzProductDmnService,
        IKfCrkdjRepo kfCrkdjRepo, IKfCrkmxRepo kfCrkmxRepo,
        IKfKcxxDmnService kfKcxxDmnService)
        {
            _kfApplyOrderRepo = kfApplyOrderRepo;
            _kfApplyOrderDetailRepo = kfApplyOrderDetailRepo;
            _crkFs = crkFs;
            _kfKcxxRepo = kfKcxxRepo;
            _wzProductDmnService = wzProductDmnService;
            _kfCrkdjRepo = kfCrkdjRepo;
            _kfCrkmxRepo = kfCrkmxRepo;
            _kfKcxxDmnService = kfKcxxDmnService;
        }

        /// <summary>
        /// Gửi phiếu lĩnh của khoa phòng
        /// </summary>
        /// <param name="sld"></param>
        /// <param name="sldmx"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SubmitDepartmentApply(KfApplyOrderEntity sld, List<KfApplyOrderDetailEntity> sldmx, string organizeId)
        {
            if (sld == null) return "Thông tin chính phiếu lĩnh không được để trống";
            if (sldmx == null || sldmx.Count == 0) return "Chi tiết phiếu lĩnh không được để trống";
            sld.OrganizeId = organizeId;
            sld.applyProcess = (int)EnumApplyProcess.AuditApproved;
            sld.applyType = (int)EnumApplyType.DepartmentApply;
            // Create(false): long identity Id is assigned by the DB, not here.
            sld.Create(false);
            if (_kfApplyOrderRepo.Insert(sld) <= 0) return "Lưu thông tin chính phiếu lĩnh thất bại";
            // Same identity caveat as PurchasingPlanApp: re-read by plan number
            // when the insert did not populate the long Id.
            if (sld.Id <= 0)
            {
                var fresh = _kfApplyOrderRepo.SelectData(sld.sldh, organizeId);
                if (fresh != null && fresh.Id > 0) sld.Id = fresh.Id;
            }
            sldmx.ForEach(p =>
            {
                p.sldId = sld.Id;
                p.Create(false);
            });
            if (_kfApplyOrderDetailRepo.Insert(sldmx) > 0) return "";
            _kfApplyOrderRepo.Delete(sld);
            return "Lưu chi tiết phiếu lĩnh thất bại";
        }

        /// <summary>
        /// Gửi phát hàng theo phiếu lĩnh
        /// </summary>
        /// <param name="ckmx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string SubmitApplyOutStock(List<VApplyBillDetailEntity> ckmx, string organizeId, string userCode)
        {
            var result = "";
            try
            {
                if (ckmx == null || ckmx.Count == 0) return "Chi tiết xuất kho không được để trống!";
                var crkfs = _crkFs.SelectData("1").FirstOrDefault();
                foreach (var rkbm in ckmx.Select(p => p.rkbm).Distinct())
                {
                    var partCkmx = ckmx.FindAll(p => p.rkbm == rkbm && p.bmdwsysl >= p.xfsl && p.sl >= p.bmdwsysl);
                    if (partCkmx.Count == 0) continue;
                    var djInfDto = new DjInfDTO
                    {
                        crkdj = new KfCrkdjEntity
                        {
                            auditState = ((int)EnumAuditState.Adopt).ToString(), // Không cần duyệt thủ công
                            Pdh = "CKZKS" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            deliveryNo = "",
                            crkfs = crkfs == null ? "" : crkfs.Id,
                            rkbm = rkbm,
                            OrganizeId = organizeId,
                            ckczy = userCode,
                            shczy = userCode,
                            ckbm = Constants.CurrentKf.currentKfId,
                            rksj = DateTime.Now,
                            rkczy = userCode,
                            cksj = DateTime.Now,
                            zt = ((int)Enumzt.Enable).ToString(),
                            djlx = (int)EnumOutOrInStorageBillType.chukuzhikeshi,
                            CreateTime = DateTime.Now,
                            CreatorCode = userCode,
                        },
                        crkdjmx = new List<KfCrkmxEntity>(),
                        applyBillDetail = partCkmx
                    };
                    djInfDto.crkdj.Create(false);
                    foreach (var item in partCkmx)
                    {
                        var kcxx = _kfKcxxRepo.SelectData(djInfDto.crkdj.ckbm, item.productId);
                        if (kcxx == null || kcxx.Count == 0) throw new Exception(string.Format("{0} không tìm thấy tồn kho hợp lệ", item.wzmc));
                        if (kcxx.Sum(p => p.kcsl - p.djsl) < item.xfsl * item.zhyz) throw new Exception(string.Format("{0} tồn kho không đủ", item.wzmc));
                        var wzxx = _wzProductDmnService.GetProductInfo(item.productId, organizeId);
                        if (wzxx == null) throw new Exception(string.Format("{0} đã bị vô hiệu", item.wzmc));
                        var sysl = item.xfsl * item.zhyz;
                        foreach (var kcmx in kcxx.OrderBy(p => p.yxq))
                        {
                            if (sysl <= 0) break;
                            var curkykc = kcmx.kcsl - kcmx.djsl;
                            var crkmx = new KfCrkmxEntity
                            {
                                productId = item.productId,
                                lsj = wzxx.lsj * item.zhyz,
                                jj = wzxx.jj * item.zhyz,
                                unitId = item.bmdwId,
                                unitName = item.bmdwmc,
                                zhyz = item.zhyz,
                                yxq = kcmx.yxq,
                                scrq = null,
                                fph = "",
                                pc = kcmx.pc,
                                ph = kcmx.ph,
                                remark = "",
                                ckbmkc = kcmx.kcsl,
                                rkbmkc = 0,
                                zt = ((int)Enumzt.Enable).ToString(),
                                CreateTime = DateTime.Now,
                                CreatorCode = userCode
                            };
                            crkmx.Create(false);
                            djInfDto.crkdjmx.Add(crkmx);
                            if (curkykc >= sysl)
                            {
                                crkmx.sl = sysl;
                                sysl = 0;
                            }
                            else
                            {
                                crkmx.sl = curkykc;
                                sysl -= curkykc;
                            }
                        }
                    }

                    var partResult = new DeptApplyOutStockProcess(djInfDto, _kfCrkdjRepo, _kfCrkmxRepo, _kfKcxxDmnService, _kfApplyOrderRepo, _kfApplyOrderDetailRepo).Process();
                    if (!partResult.IsSucceed) throw new Exception(partResult.ResultMsg);
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
    }
}
