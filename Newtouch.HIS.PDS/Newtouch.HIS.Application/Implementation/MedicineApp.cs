using System;
using Newtouch.Core.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class MedicineApp : AppBase, IMedicineApp
    {
        private readonly ISysMedicineProfitLossReasonRepo _sysMedicineProfitLossReasonRepo;
        private readonly IMedicineDmnService _medicineDmnService;
        private readonly IPyDmnService _pyDmnService;

        #region 报损报溢
        /// <summary>
        /// Lấy lý do lãi lỗ theo loại lãi lỗ
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        public List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx)
        {
            return _sysMedicineProfitLossReasonRepo.GetLossProfitReasonListByType(sylx);
        }

        /// <summary>
        /// Tra cứu thuốc lãi lỗlist
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        public List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode)
        {
            return _medicineDmnService.SelectLossAndProfitMedicineList(inputCode);
        }

        /// <summary>
        /// Lưu báo hỏng báo thừa
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        public void SaveReportLossAndProfit(List<SysMedicineProfitLossEntity> profitLossEntityList)
        {
            foreach (var item in profitLossEntityList)
            {
                int kcsl = 0;
                decimal jj = 0;
                //1.Tra rakcslVàjj
                _medicineDmnService.SelectKcslAndJj(item.Ypdm, item.Ph, item.pc, item.Yxq, out kcsl, out jj);
                if (item.Sysl <= 0)  //Báo hỏng
                {
                    if (kcsl + item.Sysl >= 0)
                    {
                        //Lưu（updateTồn kho insertBảng lãi lỗ）
                        _medicineDmnService.SaveReportLossAndProfit(item, kcsl, jj);
                    }
                    else
                    {
                        throw new FailedCodeException("INSUFFICIENT_STOCK_NOT_ENOUGH_TO_REPORT_LOSS");
                    }
                }
                else  //Báo thừa
                {
                    //Lưu（updateTồn kho insertBảng lãi lỗ）
                    _medicineDmnService.SaveReportLossAndProfit(item, kcsl, jj);
                }
            }

        }

        #endregion

        #region 报损报溢查询
        /// <summary>
        /// Tra cứu báo hỏng báo thừa
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
            return _medicineDmnService.SelectLossAndProditInfoList(pagination, startTime, endTime, inputCode, syyy, syqk);
        }

        /// <summary>
        /// Tra cứu tổng tiền giá bán buôn, tổng tiền giá bán lẻ
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">Lý do lãi lỗ</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">Tình hình lãi lỗ</param>
        /// <returns></returns>
        public LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            return _medicineDmnService.ComputePjzeAndLjze(startTime, endTime, syyy, inputCode, syqk);
        }
        #endregion

        #region 首页统计

        /// <summary>
        /// Lấy số lần phát thuốc theo đơn thuốc tháng nội trú
        /// </summary>
        /// <returns></returns>
        public MonthlySummaryDO GetFyCountVoByYfbm(List<FyCountVoByYfbm> val)
        {
            var monthlySummary = new MonthlySummaryDO();
            if (val != null && val.Count > 0)
            {
                val.ForEach(p =>
                {
                    if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-01"))
                    {
                        monthlySummary.January = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-02"))
                    {
                        monthlySummary.February = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-03"))
                    {
                        monthlySummary.March = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-04"))
                    {
                        monthlySummary.April = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-05"))
                    {
                        monthlySummary.May = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-06"))
                    {
                        monthlySummary.June = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-07"))
                    {
                        monthlySummary.July = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-08"))
                    {
                        monthlySummary.August = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-09"))
                    {
                        monthlySummary.September = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-10"))
                    {
                        monthlySummary.October = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-11"))
                    {
                        monthlySummary.November = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-12"))
                    {
                        monthlySummary.December = p.fyCount;
                    }
                });
            }
            return monthlySummary;
        }

        /// <summary>
        /// Lấy số lần y lệnh năm nay|Lần thuốc trong đơn thuốc
        /// </summary>
        /// <returns></returns>
        public List<FyClassificationStatistics> GetFyCountBydl()
        {
            var fyCount = _pyDmnService.GetFyCountBydl();
            if (fyCount == null || fyCount.Count <= 0) return null;
            return (from item in fyCount
                    select new FyClassificationStatistics
                    {
                        name = item.dlmc,
                        y = item.ypCount
                    }).ToList();
        }
        #endregion


        /// <summary>
        /// Tra cứu chi tiết kiểm kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        public IList<InventoryQureyinfoVO> InventoryQureyInfo(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk)
        {
            return _medicineDmnService.GetInventoryQureyInfo(pagination, startTime, endTime, inputCode, syyy, syqk);
        }
    }
}
