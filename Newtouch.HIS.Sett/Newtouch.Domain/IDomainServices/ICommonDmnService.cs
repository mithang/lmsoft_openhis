using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Một số không biết để đâusqlTra cứu,Đặt tại đây
    /// </summary>
    public interface ICommonDmnService
    {
        /// <summary>
        /// Lấy số người khám bệnh(Ngoại trú và nội trú)
        /// </summary>
        VisitNumBO GetVisitNum(string configmethod, string orgId = null);

        /// <summary>
        /// Lấy trước hôm nay10Chi tiết lượt khám bệnh trong tuần
        /// </summary>
        /// <param name="month"></param>
        /// <param name="isAdministrator"></param>
        /// <param name="orgId"></param>
        /// <param name="topOrgId"></param>
        MonthVisitNumBO GetWeekNum(string configmethod, string orgId);

        /// <summary>
        /// Lấy cuối cùng10Ngày trong tuầnlist
        /// </summary>
        /// <returns></returns>
        List<LastWeekInfo> GetLastWeek();

        /// <summary>
        /// BHYT Quý Châu,Lấy mã xác minh giao dịch qua số giao dịch
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        string Gzyb_GetJyyzmByJylsh(string jylsh, string OrgId);

        /// <summary>
        /// Lấy phân loại thuốclist(Loại lớn không thu phí)
        /// </summary>
        /// <returns></returns>
        IList<SysMedicineClassificationVEntity> GetMedicineClassificationList();

        /// <summary>
        /// Lấy phân loại hạng mụclist
        /// </summary>
        /// <returns></returns>
        IList<SysChargeCategoryVEntity> GetChargetItemClassificationList(string orgId);
        /// <summary>
        /// Lấy khu bệnh liên kết khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>

        IList<SysDepartmentWardRelationVO> GetWardbyDept(string orgId, string ks, string keyword);

    }
}
