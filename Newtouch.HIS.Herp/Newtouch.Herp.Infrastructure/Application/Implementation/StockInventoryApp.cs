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
    /// Kiểm kê tồn kho (port of the Framework StockInventoryApp; the original
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
        /// Bắt đầu kiểm kê
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
        /// Lưu kiểm kê
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

            // G12: resultObjArr null -> ToList<T>(null) trả null -> foreach null
            // trong repo NRE; rỗng -> lưu 0 dòng nhưng controller vẫn Success.
            if (inventoryInfoList == null || inventoryInfoList.Count == 0)
            {
                throw new Exception("Không có dữ liệu kiểm kê để lưu");
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
        /// Hủy kiểm kê
        /// </summary>
        public void CancelInventory(long pdId)
        {
            // G12: phân biệt không-tìm-thấy với đã-kết-thúc — trước đây pdId
            // bogus cũng báo "Đợt kiểm kê này đã kết thúc, không thể hủy"
            // (message gây hiểu lầm: phiếu không tồn tại mà báo đã kết thúc).
            var pdInfo = _kcPdxxRepo.SelectPdInfByPdId(pdId, OrganizeId);
            if (pdInfo == null)
            {
                throw new Exception("Không tìm thấy thông tin kiểm kê đã chỉ định");
            }
            //Kiểm tra có kiểm kê chưa hoàn thành không
            var count = _kcPdxxRepo.SelectUnfinishedInventoryByPdId(pdId, OrganizeId);
            if (count != 1)
            {
                throw new Exception("Đợt kiểm kê này đã kết thúc, không thể hủy");
            }
            _stockInventoryDmnService.CancelInventory(pdId);
        }

        /// <summary>
        /// Kết thúc kiểm kê
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
            //Tạo thông tin kiểm kê
            var resultMessage = _stockInventoryDmnService.EndInventory(pdId, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            if (!string.IsNullOrEmpty(resultMessage) && !resultMessage.Contains("thành công"))
            {
                throw new Exception(resultMessage);
            }
        }

        /// <summary>
        /// Gửi chênh lệch
        /// </summary>
        public string LossAndProfitSubmit(List<LossAndProditSubmitDTO> plist, string warehouseId)
        {
            // G14: guard TRUOC AssembleSyxxEntities — Assemble doi dau Sysl
            // (Loss -> -Sysl) nen guard Sysl<=0 dat sau Assemble se chan
            // nham MOI phieu bao hong (post v1: d6-loss tra "Số lượng phải
            // là số dương" thay vi "Không đủ tồn kho..."). Guard tren gia
            // tri client gui: Sysl phai > 0.
            if (plist == null || plist.Count <= 0) return "Thiếu thông tin lãi lỗ";
            foreach (var item in plist)
            {
                if (item == null || string.IsNullOrWhiteSpace(item.productId)) return "Vật tư bắt buộc chọn";
                if (string.IsNullOrWhiteSpace(item.pc)) return "Lô hàng không được để trống";
                if (System.Text.Encoding.Default.GetBytes(item.pc).Length > 30)
                    return "Lô hàng không được vượt quá 30 ký tự";
                if (item.Sysl <= 0) return "Số lượng phải là số dương";
            }
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
