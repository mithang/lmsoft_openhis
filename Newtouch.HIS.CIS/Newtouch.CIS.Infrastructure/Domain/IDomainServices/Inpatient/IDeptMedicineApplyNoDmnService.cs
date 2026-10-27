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
        /// Tra cứuchuẩn bị thuốcPhiếuTrạng thái
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>

        List<DeptApplyNoResp> SelectApplyNoStatus(string orgId, string sqdArr, string UserCode);
        /// <summary>
        /// Cập nhậtKhoachuẩn bị thuốcPhiếuTrạng thái
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="SqdArray"></param>
        /// <returns></returns>
        int UpdateApplyNoStatus(string OrganizeId, string SqdArray,string UserCode);

        /// <summary>
        /// Tồn khoTrả lạiPhiếuvềtrảkiểm tra
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        string SelectKcthApplyNoStatus(string orgId, string sqdArr, string UserCode);
        /// <summary>
        /// Cập nhậtKhoatrảTồn khoTrạng thái đăng ký
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        int UpdateReturnApplyNoStatus(string orgId, string sqdArr, string UserCode);
    }
}
