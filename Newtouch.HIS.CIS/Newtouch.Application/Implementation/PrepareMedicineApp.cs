using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Application.Interface;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Newtouch.Application.Implementation
{
    public class PrepareMedicineApp : AppBase, IPrepareMedicineApp
    {

        private readonly ISysMedicineProfitLossReasonRepo _sysMedicineProfitLossReasonRepo;

        private readonly IPrepareMedicineDmnService _preparemMedicineDmnService;

        #region Báo hỏng/báo thừa
        /// <summary>
        /// Lấy nguyên nhân hỏng/thừa theo loại hỏng/thừa
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        public List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx)
        {
            return _sysMedicineProfitLossReasonRepo.GetLossProfitReasonListByType(sylx);
        }

        /// <summary>
        /// Tra cứu danh sách thuốc báo hỏng/báo thừa
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        public List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode)
        {
            var list= _preparemMedicineDmnService.SelectLossAndProfitMedicineList(inputCode);
            return list;
        }

        /// <summary>
        /// Gửi báo hỏng/báo thừa
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public string SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
        {
            if (syxx == null || syxx.Length <= 0) return "损益明细不能为空";
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

                            var tmpResult = _preparemMedicineDmnService.SubmitReportLossAndProfit(item);
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

        #endregion

        #region Tra cứu báo hỏng/báo thừa
        /// <summary>
        /// Tra cứu báo hỏng/báo thừa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        public IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk)
        {
            return _preparemMedicineDmnService.SelectLossAndProditInfoList(pagination, startTime, endTime, inputCode, syyy, syqk);
        }

        /// <summary>
        /// Tra cứu tổng tiền giá bán buôn, tổng tiền giá bán lẻ
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">Nguyên nhân hỏng/thừa</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">Tình hình hỏng/thừa</param>
        /// <returns></returns>
        public LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            return _preparemMedicineDmnService.ComputePjzeAndLjze(startTime, endTime, syyy, inputCode, syqk);
        }

        /// <summary>
        /// Lưu báo hỏng/báo thừa
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        //public void SaveReportLossAndProfit(List<SysMedicineProfitLossEntity> profitLossEntityList)
        //{
        //    foreach (var item in profitLossEntityList)
        //    {
        //        int kcsl = 0;
        //        decimal jj = 0;
        //        //1. Tìm ra kcsl và jj
        //        _medicineDmnService.SelectKcslAndJj(item.Ypdm, item.Ph, item.pc, item.Yxq, out kcsl, out jj);
        //        if (item.Sysl <= 0)  //Báo hỏng
        //        {
        //            if (kcsl + item.Sysl >= 0)
        //            {
        //                //Lưu (update tồn kho, insert bảng hỏng/thừa)
        //                _medicineDmnService.SaveReportLossAndProfit(item, kcsl, jj);
        //            }
        //            else
        //            {
        //                throw new FailedCodeException("INSUFFICIENT_STOCK_NOT_ENOUGH_TO_REPORT_LOSS");
        //            }
        //        }
        //        else  //Báo thừa
        //        {
        //            //Lưu (update tồn kho, insert bảng hỏng/thừa)
        //            _medicineDmnService.SaveReportLossAndProfit(item, kcsl, jj);
        //        }
        //    }

        //}

        #endregion
    }
}
