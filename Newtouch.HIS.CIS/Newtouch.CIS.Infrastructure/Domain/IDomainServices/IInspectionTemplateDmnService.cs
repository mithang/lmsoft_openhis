using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface IInspectionTemplateDmnService
    {
        /// <summary>
        /// Theotype Tra cứuXét nghiệmKiểm tramẫu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jyjcmbLx"></param>
        List<InspectionTemplateTreeVO> GetTemplateListByType(string orgId, int jyjcmbLx);

        /// <summary>
        /// TheombId Tra cứumẫudướiBộ mẫuMục
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mbId"></param>
        List<GPackageTreeDetailVO> GetTemplateDetailByMbId(string orgId, string mbId, string ztKeyword);

        /// <summary>
        /// TheoztIdTra cứuBộ mẫudướicủaMục thu phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ztId"></param>
        /// <returns></returns>
        List<GPackageZTTreeDetailVO> GetGPackageDetailByZtId(string orgId, string ztId,string mbId=null);
        /// <summary>
        /// LấyBộ mẫuthông tin
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ztId"></param>
        /// <returns></returns>
        List<GPackageZTTreeDetailVO> GetGPackageInfoByZtId(string orgId, string ztId, string mbId);

        /// <summary>
        /// mẫuChi tiết
        /// </summary>
        /// <param name="ztId"></param>
        InspectionTemplateDetailBO GetTemplateDetail(string mbId, string orgId);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="ztobj"></param>
        /// <param name="ztxmlist"></param>
        void SaveData(InspectionTemplateEntity mbobj, List<TemplateGroupPackageEntity> mbztlist);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="ztId"></param>
        void DeleteData(string mbId);
    }
}
