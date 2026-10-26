using Newtouch.Domain.Api;
using Newtouch.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices.Inpatient
{
    public interface IDeptMedicineApplyNoDmnService
    {
        /// <summary>
        /// Tra cứu trạng thái phiếu yêu cầu thuốc dự trữ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>

        List<DeptApplyNoResp> SelectApplyNoStatus(string orgId, string sqdArr, string UserCode);
        /// <summary>
        /// Cập nhật trạng thái phiếu yêu cầu thuốc dự trữ khoa
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="SqdArray"></param>
        /// <returns></returns>
        int UpdateApplyNoStatus(string OrganizeId, string SqdArray,string UserCode);

        /// <summary>
        /// Xác thực hoàn trả phiếu yêu cầu trả tồn kho
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        string SelectKcthApplyNoStatus(string orgId, string sqdArr, string UserCode);
        /// <summary>
        /// Cập nhật trạng thái yêu cầu trả tồn kho khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        int UpdateReturnApplyNoStatus(string orgId, string sqdArr, string UserCode);
    }
}
