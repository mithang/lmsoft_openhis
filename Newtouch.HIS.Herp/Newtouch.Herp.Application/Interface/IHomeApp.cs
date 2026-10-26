using System.Collections.Generic;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// home app
    /// </summary>
    public interface IHomeApp
    {
        /// <summary>
        /// Lấy việc cần làm HTML
        /// </summary>
        /// <returns></returns>
        string AssembleNeedDealHtml();

        /// <summary>
        /// Lấy tổng Số việc cần làm
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetPendingCount();

        /// <summary>
        /// Lấy lượt phát thuốc đơn nội trú theo tháng
        /// </summary>
        /// <returns></returns>
        MonthlySummaryDTO GetPsiCountVoByKf(List<VPsiStatisticsByDateEntity> val);

        /// <summary>
        /// Tên loại chuyển đổi
        /// </summary>
        /// <param name="source"></param>
        void TransformRkCount(List<ClassificationStatisticsEntity> source);
    }
}