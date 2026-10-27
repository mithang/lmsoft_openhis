using Newtouch.Infrastructure.EF;
using FrameworkBase.MultiOrg.DmnService;
using System;
using NewtouchHIS.Lib.Base.Model;
using System.Collections;
using System.Collections.Generic;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.DomainServices.DrugStorage;
using Newtouch.PDS.Requset;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.DomainServices;
using Newtouch.PDS.Requset.Stock;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Ứng dụng tồn kho
    /// </summary>
    public class DrugStorageApp : AppBase, IDrugStorageApp
    {
        private readonly ISysMedicineRepo _sysMedicineRepo;
        private readonly IDrugStorageDmnService _drugStorageDmnService;
        private readonly ISysMedicinePriceAdjustmentRepo _sysMedicinePriceAdjustmentRepo;
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly ISysMedicineStockCarryDownDmnService _sysMedicineStockCarryDownDmnService;

        /// <summary>
        /// Truy vấn thuốc của bộ phận hiện tại(Nhập kho)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        public List<DepartmentMedicineVO> SelectDepartmentMedicineList(string keyword, string yfbmCode, string organizeId)
        {
            return _drugStorageDmnService.SelectDepartmentMedicineList(keyword, yfbmCode, organizeId);
        }

        /// <summary>
        /// Truy vấn thuốc của bộ phận hiện tại(Xuất kho)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="fph"></param>
        /// <param name="gyscode"></param>
        /// <returns></returns>
        public List<DepartmentMedicineVO> SelectDepartmentMedicineList2(string keyword, string fph, string gyscode)
        {
            return _drugStorageDmnService.SelectDepartmentMedicineList2(keyword, fph, gyscode);
        }

        /// <summary>
        /// Thông tin hóa đơn thuốc
        /// </summary>
        /// <param name="fph">fph</param>
        /// <returns></returns>
        public List<MedicineInvoiceInfoVO> SelectMedicineListByFPH(string fph)
        {
            return _drugStorageDmnService.SelectMedicineListByFPH(fph);
        }

        /// <summary>
        /// Nhập kho
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailList"></param>
        public void SaveInStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailEntity> ioReceiptDetailList)
        {
            _drugStorageDmnService.SaveInStorageInfo(ioReceiptEntity, ioReceiptDetailList);
        }

        /// <summary>
        /// Xuất kho
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailList"></param>
        public void SaveOutStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailVO> ioReceiptDetailList)
        {
            _drugStorageDmnService.SaveOutStorageInfo(ioReceiptEntity, ioReceiptDetailList);
        }

        #region Điều chỉnh giá thuốc
        /// <summary>
        /// Đơn xin điều chỉnh giá Truy vấn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public IList<AdjustPriceMedicineInfoVO> SelectAdjustPriceMedicineInfoList(Pagination pagination, string inputCode)
        {
            return _drugStorageDmnService.SelectAdjustPriceMedicineInfoList(pagination, inputCode);
        }

        /// <summary>
        /// Truy vấn duyệt điều chỉnh giá
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public IList<AdjustPriceMedicineInfoVO> SelectMedicineAdjustPriceApprovalInfoList(Pagination pagination, string inputCode, string shzt)
        {
            return _drugStorageDmnService.SelectMedicineAdjustPriceApprovalInfoList(pagination, inputCode, shzt);
        }

        /// <summary>
        /// Thực hiện điều chỉnh giá thuốc
        /// </summary>
        /// <param name="ypCodeList"></param>
        public string ExecteAdjustPrice(ArrayList ypCodeList)
        {
            var result = _sysMedicinePriceAdjustmentRepo.CheckStatus(ypCodeList);//Kiểm tra có dữ liệu chưa xử lý không
            return string.IsNullOrEmpty(result) ? _drugStorageDmnService.ExecteAdjustPrice(ypCodeList) : result;
        }

        /// <summary>
        /// Truy vấn lịch sử điều chỉnh giá
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public IList<AdjustPriceHistoryInfoVO> SelectMedicineAdjustPriceHistoryInfoList(Pagination pagination, string inputCode, string startTime, string endTime)
        {
            return _drugStorageDmnService.SelectMedicineAdjustPriceHistoryInfoList(pagination, inputCode, startTime, endTime);
        }
        #endregion

        #region Kết chuyển

        /// <summary>
        /// Kết chuyển
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        public void CarryOverMedicine(string yfbmCode, string organizeId)
        {
            //var needCarryOverMedicineList = _pharmacyDrugStorageDmnService.SelectNeedCarryOverMedicineList();//Truy vấn trước thông tin thuốc cần kết chuyển
            //if (needCarryOverMedicineList.Count > 0)
            //{
            //    var result = _sysMedicineStockCarryDownDmnService.CarryOverMedicine(needCarryOverMedicineList, yfbmCode, organizeId, DateTime.Now, UserIdentity.UserCode);
            //    if (!string.IsNullOrWhiteSpace(result)) throw new FailedException(result);
            //}
            //else
            //{
            //    throw new FailedCodeException("OUT_OF_STOCK");
            //}
            var result = _sysMedicineStockCarryDownDmnService.CarryOverMedicine(yfbmCode, organizeId, DateTime.Now, UserIdentity.UserCode);
            if (!string.IsNullOrWhiteSpace(result)) throw new FailedException(result);
        }
        #endregion

        /// <summary>
        /// Truy vấn tồn kho
        /// </summary>
        /// <param name="yplist"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public StockQueryResponseDTO StockQuery(List<DrugInfo> yplist, string organizeId)
        {
            if (yplist == null || yplist.Count == 0) return null;
            var result = new StockQueryResponseDTO
            {
                drugStockInfos = new List<DrugStockInfo>()
            };
            for(int i = 0; i < yplist.Count; i ++)
            {
                DrugInfo p = yplist[i];
                if (string.IsNullOrEmpty(p.lyyf))
                {
                    p.lyyf = "";
                }
                result.drugStockInfos.AddRange(_drugStorageDmnService.SelectStock(p.ypCode, p.lyyf, organizeId));
            }
            /*
            yplist.ForEach(p => { result.drugStockInfos.AddRange(_drugStorageDmnService.SelectStock(p.ypCode, p.lyyf, organizeId)); });
            */
            return result;
        }
        public string PrepareMedicine(BYDjInfoDTO yplist, string organizeId,string yhgh)
        {
            var excResult = new DispensingDmnService(new DefaultDatabaseFactory(), false)
                                  .PrepareMedicine(yplist, organizeId,yhgh);
            return excResult;
        }
public string PrepareMedicineReturn (BythDjInfoDTO yplist, string organizeId, string yhgh)
        {
            var excResult = new DispensingDmnService(new DefaultDatabaseFactory(), false)
                                  .PrepareMedicineReturn(yplist, organizeId, yhgh);
            return excResult;
        }
        public string WithdrawalPreparation(string Djh, string organizeId, string yhgh,string shzt)
        {
            var excResult = new DispensingDmnService(new DefaultDatabaseFactory(), false)
                                  .WithdrawalPreparation(Djh, organizeId, yhgh,shzt);
            return excResult;
        }

        /// <summary>
        /// Gửi báo hỏng báo thừa
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public string SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
        {
            if (syxx == null || syxx.Length <= 0) return "Chi tiết lãi lỗ không được để trống";
            var errorMsg = "";
            var cts = new CancellationTokenSource();
            try
            {
                var parent = new Task(() =>
                {
                    var taskFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                    foreach (var item in syxx)
                    {
                        taskFactory.StartNew(() =>
                        {
                            if (cts.IsCancellationRequested)
                            {
                                return false;
                            }

                            var tmpResult = new SysMedicineStockInfoDmnService(new DefaultDatabaseFactory(), false).SubmitReportLossAndProfit(item);
                            if (string.IsNullOrWhiteSpace(tmpResult)) return true;
                            cts.Cancel();
                            errorMsg = tmpResult;
                            return false;

                        }, cts.Token);
                    }
                });
                parent.Start();
                parent.Wait();
                if (cts.IsCancellationRequested || !string.IsNullOrWhiteSpace(errorMsg))
                {
                    throw new Exception(errorMsg);
                }

                return "";
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
        }

        public string WithdrawalPreparationReturn(string Djh, string organizeId, string yhgh, string thzt)
        {
            var excResult = new DispensingDmnService(new DefaultDatabaseFactory(), false)
                                  .WithdrawalPreparationReturn(Djh, organizeId, yhgh, thzt);
            return excResult;
        }
    }
}
