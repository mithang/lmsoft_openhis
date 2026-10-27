using System;
using System.Collections.Generic;
using System.Linq;
using NewtouchHIS.Lib.Framework.Operator;
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
    /// 库存盘点 (port of the Framework StockInventoryApp; the original
    /// FailedException type does not exist in the Core copy, so plain
    /// Exception carries the same user-visible messages).
    /// </summary>
    public class StockInventoryApp : AppBase, IStockInventoryApp
    {
        private readonly IStockInventoryDmnService _stockInventoryDmnService;
        private readonly IKcPdxxRepo _kcPdxxRepo;
        private readonly IKcPdxxmxRepo _kcPdxxmxRepo;

        public StockInventoryApp(IStockInventoryDmnService stockInventoryDmnService,
            IKcPdxxRepo kcPdxxRepo, IKcPdxxmxRepo kcPdxxmxRepo)
        {
            _stockInventoryDmnService = stockInventoryDmnService;
            _kcPdxxRepo = kcPdxxRepo;
            _kcPdxxmxRepo = kcPdxxmxRepo;
        }

        /// <summary>
        /// 开始盘点
        /// </summary>
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
                throw new Exception(inventoryResult);
            }
            var result = _stockInventoryDmnService.GetPdSj(warehouseId, organizeId).FirstOrDefault();
            if (result == null)
            {
                throw new Exception("Lấy lại thời gian kiểm kê thất bại");
            }
            return result;
        }

        /// <summary>
        /// 盘点保存
        /// </summary>
        public void SaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList, long pdId, string noPc = "0")
        {
            var pdInfo = _kcPdxxRepo.SelectPdInfByPdId(pdId, OperatorProvider.GetCurrent().OrganizeId);
            if (pdInfo == null)
            {
                throw new Exception("Không tìm thấy thông tin kiểm kê đã chỉ định");
            }
            if (pdInfo.jssj != null || pdInfo.jssj >= Constants.MinDateTime)
            {
                throw new Exception("Đợt kiểm kê này đã kết thúc");
            }

            SaveInventoryInfo(inventoryInfoList, pdId, noPc, Constants.CurrentKf.currentKfId, OrganizeId);
        }

        private void SaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList, long pdId, string noPc, string warehouseId, string organizeId)
        {
            switch (noPc)
            {
                case "0":
                    _kcPdxxmxRepo.UpdateSlBySaveInventoryInfo(inventoryInfoList);
                    break;
                case "1":
                    var result = _stockInventoryDmnService.SaveInventoryDetailNoPc(inventoryInfoList, pdId, warehouseId, organizeId);
                    if (!string.IsNullOrWhiteSpace(result)) throw new Exception(result);
                    break;
            }
        }

        /// <summary>
        /// 取消盘点
        /// </summary>
        public void CancelInventory(long pdId)
        {
            //判断是否有未完成的盘点
            var count = _kcPdxxRepo.SelectUnfinishedInventoryByPdId(pdId, OrganizeId);
            if (count != 1)
            {
                throw new Exception("Đợt kiểm kê này đã kết thúc, không thể hủy");
            }
            _stockInventoryDmnService.CancelInventory(pdId);
        }

        /// <summary>
        /// 结束盘点
        /// </summary>
        public void EndInventory(long pdId)
        {
            var pdInfo = _kcPdxxRepo.SelectPdInfByPdId(pdId, OrganizeId);
            if (pdInfo == null)
            {
                throw new Exception("Không tìm thấy thông tin kiểm kê đã chỉ định");
            }
            if (pdInfo.jssj != null || pdInfo.jssj >= Constants.MinDateTime)
            {
                throw new Exception("Đợt kiểm kê này đã kết thúc");
            }
            //生成盘点信息
            var resultMessage = _stockInventoryDmnService.EndInventory(pdId, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            if (!string.IsNullOrEmpty(resultMessage) && !resultMessage.Contains("thành công"))
            {
                throw new Exception(resultMessage);
            }
        }

        /// <summary>
        /// 提交损益
        /// </summary>
        public string LossAndProfitSubmit(List<LossAndProditSubmitDTO> plist, string warehouseId)
        {
            var syxxlist = AssembleSyxxEntities(plist, warehouseId);
            if (syxxlist.Count <= 0) return "Thiếu thông tin lãi lỗ";
            _stockInventoryDmnService.InsertSyxxBatch(syxxlist, warehouseId, OrganizeId);
            return "";
        }

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
                    warehouseId = warehouseId,
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
