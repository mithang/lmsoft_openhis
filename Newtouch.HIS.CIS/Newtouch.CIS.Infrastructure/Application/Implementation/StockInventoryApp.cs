using System;
using System.Collections.Generic;
using System.Linq;

using Newtouch.Application.Interface;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.IDomainServices.DeptStorageManage;
using Newtouch.Domain.IRepository.DeptStorageManage;
using Newtouch.Domain.ValueObjects.Storage;
using Newtouch.Infrastructure;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Kiểm kê
    /// </summary>
    public class StockInventoryApp : AppBase, IStockInventoryApp
    {
        private readonly IStockInventoryDmnService _stockInventoryDmnService;
        private readonly IKcPdxxRepo _kcPdxxRepo;
        private readonly IKcPdxxmxRepo _kcPdxxmxRepo;

        /// <summary>
        /// Bắt đầuKiểm kê
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public VInventoryDateDropDownEntity StartInventory(string warehouseId, string organizeId)
        {
            var pdDate = _stockInventoryDmnService.GetHangUpPdDates(warehouseId, organizeId);
            if (pdDate != null && pdDate.Count > 0)
            {
                return null;
            }
            var inventoryResult = _stockInventoryDmnService.GenerateInventoryInfo(warehouseId, organizeId);
            if (!string.IsNullOrEmpty(inventoryResult))
            {
                throw new FailedException(inventoryResult);
            }
            var result = _stockInventoryDmnService.GetPdSj(warehouseId, organizeId).FirstOrDefault();
            if (result == null)
            {
                throw new FailedException("lạiLấyKiểm kêThời gianthất bại");
            }
            return result;
        }

        /// <summary>
        /// Kiểm kêLưu
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId">Kiểm kêphiếuID</param>
        /// <param name="noPc">  0：theoLôKiểm kê 1：khôngtheoLôKiểm kê</param>
        public void SaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList, long pdId, string noPc = "0")
        {
            var pdInfo = _kcPdxxRepo.SelectPdInfByPdId(pdId, OperatorProvider.GetCurrent().OrganizeId);
            if (pdInfo == null)
            {
                throw new FailedException("chưatìmđếnchỉđịnhKiểm kêthông tin");
            }
            if (pdInfo.jssj != null || pdInfo.jssj >= Constants.MinDateTime)
            {
                throw new FailedException("Kiểm kêđãKết thúc");
            }
            SaveInventoryInfo(inventoryInfoList, pdId, noPc, UserIdentity.DepartmentCode, OrganizeId);
        }

        /// <summary>
        /// Kiểm kêLưu
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId"></param>
        /// <param name="noPc">0：theoLôKiểm kê 1：khôngtheoLôKiểm kê</param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        private void SaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList, long pdId, string noPc, string warehouseId, string organizeId)
        {
            switch (noPc)
            {
                case "0":
                    _kcPdxxmxRepo.UpdateSlBySaveInventoryInfo(inventoryInfoList);
                    break;
                case "1":
                    var result = _stockInventoryDmnService.SaveInventoryDetailNoPc(inventoryInfoList, pdId, warehouseId, organizeId);
                    if (!string.IsNullOrWhiteSpace(result)) throw new FailedException(result);
                    break;
            }
        }

        /// <summary>
        /// HủyKiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        public void CancelInventory(long pdId)
        {
            //kiểm tralàKhôngcóchưaHoàn thànhcủaKiểm kê
            var count = _kcPdxxRepo.SelectUnfinishedInventoryByPdId(pdId, OrganizeId);
            if (count != 1)
            {
                throw new FailedException("Kiểm kêđãKết thúc,không thểHủy");
            }
            _stockInventoryDmnService.CancelInventory(pdId);
        }

        /// <summary>
        /// Kết thúcKiểm kê
        /// </summary>
        /// <returns></returns>
        public void EndInventory(long pdId)
        {
            var pdInfo = _kcPdxxRepo.SelectPdInfByPdId(pdId, OrganizeId);
            if (pdInfo == null)
            {
                throw new FailedException("chưatìmđếnchỉđịnhKiểm kêthông tin");
            }
            if (pdInfo.jssj != null || pdInfo.jssj >= Constants.MinDateTime)
            {
                throw new FailedException("Kiểm kêđãKết thúc");
            }
            //tạoKiểm kêthông tin
            var resultMessage = _stockInventoryDmnService.EndInventory(pdId, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            if (!string.IsNullOrEmpty(resultMessage) && resultMessage.IndexOf("thành công", StringComparison.OrdinalIgnoreCase) < 0)
            {
                throw new FailedException(resultMessage);
            }
        }

        /// <summary>
        /// Gửilãi lỗ
        /// </summary>
        /// <param name="plist"></param>
        /// <param name="warehouseId"></param>
        /// <returns>error msg</returns>
        public string LossAndProfitSubmit(List<LossAndProditSubmitDTO> plist, string warehouseId)
        {
            var syxxlist = AssembleSyxxEntities(plist, UserIdentity.DepartmentCode);
            if (syxxlist.Count <= 0) return "thiếulãi lỗthông tin";
            _stockInventoryDmnService.InsertSyxxBatch(syxxlist, UserIdentity.DepartmentCode, OrganizeId);
            return "";
        }

        /// <summary>
        /// lắp ráplãi lỗthông tin
        /// </summary>
        /// <param name="plist"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        private List<KcSyxxEntity> AssembleSyxxEntities(List<LossAndProditSubmitDTO> plist, string warehouseId)
        {
            if (plist == null || plist.Count <= 0) return new List<KcSyxxEntity>();
            var result = new List<KcSyxxEntity>();
            plist.ForEach(p =>
            {
                result.Add(new KcSyxxEntity
                {
                    Bgsj = DateTime.Now,
                    CreateTime = DateTime.Now,
                    CreatorCode = OperatorProvider.GetCurrent().UserCode,
                    Djh = p.Djh,
                    Jj = p.Jj,
                    LastModifierCode = null,
                    LastModifyTime = null,
                    Lsj = p.Lsj,
                    OrganizeId = OrganizeId,
                    pc = p.pc,
                    Ph = p.Ph,
                    productId = p.productId,
                    px = 0,
                    remark = p.remark,
                    Sykc = p.Sykc,
                    Sysl = p.Sysl = p.sybz == ((int)EnumSybz.Loss).ToString() ? -1 * p.Sysl : p.Sysl,
                    Syyy = p.syyyId,
                    UnitId = p.UnitId,
                    ks = warehouseId,
                    Yxq = p.Yxq,
                    Zhyz = p.zhyz,
                    Zrr = p.Zrr,
                    zt = "1"
                });
            });
            return result;
        }
    }
}
