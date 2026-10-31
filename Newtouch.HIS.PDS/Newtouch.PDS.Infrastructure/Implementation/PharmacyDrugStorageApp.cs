using Newtouch.Infrastructure.EF;
using System;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class PharmacyDrugStorageApp : AppBase, IPharmacyDrugStorageApp
    {
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly ISysMedicineInventoryRepo _sysMedicineInventoryRepo;
        private readonly ISysMedicineInventoryDetailRepo _sysMedicineInventoryDetailRepo;
        private readonly ISysMedicineFormulationRepo _sysMedicineFormulationRepo;
        private readonly ISysMedicineStockCarryDownRepo _sysMedicineStockCarryDownRepo;

        /// <summary>
        /// Truy vấn lượng tồn kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="tybz"></param>
        /// <param name="zt"></param>
        /// <param name="noShow0Kc"></param>
        /// <returns></returns>
        public IList<MedicineStockQueryVO> SelectStockTotal(Pagination pagination, string inputCode, string tybz, string zt, bool noShow0Kc)
        {
            return _pharmacyDrugStorageDmnService.SelectStockTotal(pagination, inputCode, tybz, zt, noShow0Kc);
        }

        /// <summary>
        /// Chi tiết truy vấn lượng tồn kho
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public IList<MedicineStockQueryDetailVO> SelectStockDetail(string ypCode, string yfbmCode)
        {
            return _pharmacyDrugStorageDmnService.SelectStockDetail(ypCode, yfbmCode);
        }

        /// <summary>
        /// Chi tiết truy vấn chứng từ
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        public IList<ReceiptQueryDetailVO> SelectReceipDetailInfo(string crkId, int djlx)
        {
            if (string.IsNullOrEmpty(crkId))
            {
                throw new FailedException("CrkId_is_empty");
            }
            if (djlx < 0)
            {
                throw new FailedCodeException("RECEIPTNO_IS_EMPTY");
            }
            return _pharmacyDrugStorageDmnService.SelectReceipDetailInfo(crkId, djlx, OperatorProvider.GetCurrent().OrganizeId);
        }


        #region Kiểm kê tồn kho

        /// <summary>
        /// Lấy ngày kiểm kê
        /// </summary>
        /// <returns></returns>
        public List<InventoryDateDropDownVO> GetPdDateDropdownList()
        {
            return _pharmacyDrugStorageDmnService.GetPdDateDropdownList();
        }

        /// <summary>
        /// Lấy loại thuốc (Kiểm kê)
        /// </summary>
        /// <returns></returns>
        public List<MedicineCategoryVO> GetMedicineCategoryList()
        {
            return _pharmacyDrugStorageDmnService.GetMedicineCategoryList();
        }

        /// <summary>
        /// Bắt đầu kiểm kê
        /// </summary>
        public InventoryDateDropDownVO StartInventory()
        {
            //Kiểm tra có kiểm kê chưa hoàn thành không
            var pdDate = _pharmacyDrugStorageDmnService.GetHangUpPdDates();
            if (pdDate != null && pdDate.Count > 0)
            {
                return null;
            }
            //Tạo thông tin kiểm kê
            var resultMessage = _pharmacyDrugStorageDmnService.GenerateInventoryInfo(Constants.CurrentYfbm.yfbmCode, OperatorProvider.GetCurrent().OrganizeId, OperatorProvider.GetCurrent().UserCode);
            if (!string.IsNullOrEmpty(resultMessage))
            {
                throw new FailedException(resultMessage);
            }
            //Lấy thời gian bắt đầu kiểm kê
            var inventoryDateDropDown = GetPdDateDropdownList().FirstOrDefault();
            if (inventoryDateDropDown == null)
            {
                throw new FailedCodeException("GET_INVENTORY_START_TIME_FAILED");
            }
            return inventoryDateDropDown;
        }

        /// <summary>
        /// Truy vấn thông tin thuốc cần kiểm kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="pdId"></param>
        /// <param name="pdsj"></param>
        /// <param name="srm"></param>
        /// <param name="ypzt"></param>
        /// <param name="yplb"></param>
        /// <param name="kcxs"></param>
        /// <returns></returns>
        public IList<InventoryInfoVO> SelectInventoryInfoList(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs)
        {
            return _pharmacyDrugStorageDmnService.SelectInventoryInfoList(pagination, pdId, pdsj, srm, ypzt, yplb, kcxs);
        }

        /// <summary>
        /// Lưu kiểm kê
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId"></param>
        public void SaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList, string pdId)
        {
            var pdInfo = _sysMedicineInventoryRepo.SelectPdInfByPdId(pdId, OperatorProvider.GetCurrent().OrganizeId);
            if (pdInfo == null)
            {
                throw new FailedException("Không tìm thấy thông tin kiểm kê chỉ định");
            }
            if (pdInfo.Jssj != null || pdInfo.Jssj >= Constants.MinDateTime)
            {
                throw new FailedException("Kiểm kê này đã kết thúc");
            }
            _sysMedicineInventoryDetailRepo.UpdateSlBySaveInventoryInfo(inventoryInfoList);
        }

        public void SaveInventoryZsmInfo(List<SaveInventoryInfoVO> inventoryInfoList)
        {
            _sysMedicineInventoryDetailRepo.UpdateZsmBySaveInventoryInfo(inventoryInfoList);
        }

        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        /// <returns></returns>
        public void EndInventory(string pdId)
        {
            var pdInfo = _sysMedicineInventoryRepo.SelectPdInfByPdId(pdId, OperatorProvider.GetCurrent().OrganizeId);
            if (pdInfo == null)
            {
                throw new FailedException("Không tìm thấy thông tin kiểm kê chỉ định");
            }
            if (pdInfo.Jssj != null || pdInfo.Jssj >= Constants.MinDateTime)
            {
                throw new FailedException("Kiểm kê này đã kết thúc");
            }
            //Tạo thông tin kiểm kê
            var resultMessage = _pharmacyDrugStorageDmnService.EndInventoryInfo(pdId);
            if (!string.IsNullOrEmpty(resultMessage) && resultMessage.IndexOf("thành công", StringComparison.OrdinalIgnoreCase) < 0)
            {
                throw new FailedException(resultMessage);
            }
        }

        /// <summary>
        /// Hủy kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        public void CancelInventory(string pdId)
        {
            //Kiểm tra có kiểm kê chưa hoàn thành không
            var count = _sysMedicineInventoryRepo.SelectUnfinishedInventoryByPdId(pdId);
            if (count != 1)
            {
                throw new FailedCodeException("THE_INVENTORY_HAS_ENDED_AND_CAN_NOT_BE_CANCELED");
            }
            _pharmacyDrugStorageDmnService.CancelInventory(pdId);
        }

        #endregion

        #region Kết chuyển tồn kho
        /// <summary>
        /// Lấy thuộc tính đặc biệt của thuốc
        /// </summary>
        /// <returns></returns>
        public List<DrugSpecialPropertiesVO> GetDrugSpecialPropertiesList()
        {
            return _pharmacyDrugStorageDmnService.GetDrugSpecialPropertiesList();
        }

        /// <summary>
        /// Kết chuyển
        /// </summary>
        public void CarryOverMedicine(string zq, DateTime kssj, DateTime jssj)
        {
            var needCarryOverMedicineList = _pharmacyDrugStorageDmnService.SelectNeedCarryOverMedicineList();//Truy vấn trước thông tin thuốc cần kết chuyển
            if (needCarryOverMedicineList.Count > 0)
            {
                _pharmacyDrugStorageDmnService.CarryOverMedicine(needCarryOverMedicineList, zq, kssj, jssj);
            }
            else
            {
                throw new FailedCodeException("OUT_OF_STOCK");
            }
        }

        /// <summary>
        /// Truy vấn thuốc đã kết chuyển
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="inputCode"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IList<CarryOverMedicineVO> SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode)
        {
            return _pharmacyDrugStorageDmnService.SelectCarryOverMedicineList(pagination, zq, inputCode);
        }

        #endregion

        #region Thống kê nhập xuất tồn
        /// <summary>
        /// Trả về dạng bào chế thuốclist
        /// </summary>
        /// <returns></returns>
        public List<MedicineFormulationVO> GetMedicineFormulationList()
        {
            return _sysMedicineFormulationRepo.GetMedicineFormulationList();
        }

        /// <summary>
        /// Loại thuốc (Nhập xuất tồn)
        /// </summary>
        /// <returns></returns>
        public List<MedicineCategoryVO> GetMedicineCategoryList2()
        {
            return _pharmacyDrugStorageDmnService.GetMedicineCategoryList2();
        }

        #endregion


    }
}
