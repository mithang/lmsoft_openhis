using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using System;

namespace Newtouch.HIS.Application.Interface.HospitalizationManage
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInpatientRefundApp
    {
        /// <summary>
        /// Lấy(tính phí và đã hoàn tổng cộng của) tính phí chi tiết(bao gồm hạng mục điều trị và không phải hạng mục điều trị)
        /// </summary>
        InpatientSettPatStatusDetailDto InpatientRefundQuery(string zyh, DateTime? kssj, DateTime? jssj, string ks = null, string xmlb = null, string xmmc = null);
     }
}
