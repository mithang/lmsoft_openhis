using Newtouch.HIS.Domain.ValueObjects.ReportManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.ReportManage
{
    public interface IEchartsDmnService
    {
        /// <summary>
        /// Tra cứu hiệu quả Biểu tượng
        /// </summary>
        List<PerformanceIndicatorVO> SelectPerformanceIndicator(string orgId, string year);

        /// <summary>
        /// Biểu đồ tháng lượt người ngoại trú
        /// </summary>
        List<PerformanceIndicatorVO> PerformanceMonthIndicator(string orgId, string year, string month);
    }
}
