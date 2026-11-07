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
        /// Tra cứuchuẩn bị thuốc Phiếu Trạng thái
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>

        List<DeptApplyNoResp> SelectApplyNoStatus(string orgId, string sqdArr, string UserCode);
        /// <summary>
        /// Cập nhật Khoachuẩn bị thuốc Phiếu Trạng thái
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="SqdArray"></param>
        /// <returns></returns>
        int UpdateApplyNoStatus(string OrganizeId, string SqdArray,string UserCode);

        /// <summary>
        /// Tồn kho Trả lại Phiếuvềtrảkiểm tra
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        string SelectKcthApplyNoStatus(string orgId, string sqdArr, string UserCode);
        /// <summary>
        /// Cập nhật Khoatrả Tồn kho Trạng thái đăng ký
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        int UpdateReturnApplyNoStatus(string orgId, string sqdArr, string UserCode);
    }
}
