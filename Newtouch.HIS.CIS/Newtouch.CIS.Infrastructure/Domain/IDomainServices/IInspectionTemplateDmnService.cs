using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface IInspectionTemplateDmnService
    {
        /// <summary>
        /// Theotype Tra cứu Xét nghiệm Kiểm tramẫu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jyjcmbLx"></param>
        List<InspectionTemplateTreeVO> GetTemplateListByType(string orgId, int jyjcmbLx);

        /// <summary>
        /// TheombId Tra cứumẫudưới Bộ mẫu Mục
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mbId"></param>
        List<GPackageTreeDetailVO> GetTemplateDetailByMbId(string orgId, string mbId, string ztKeyword);

        /// <summary>
        /// TheoztIdTra cứu Bộ mẫudướicủa Mục thu phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ztId"></param>
        /// <returns></returns>
        List<GPackageZTTreeDetailVO> GetGPackageDetailByZtId(string orgId, string ztId,string mbId=null);
        /// <summary>
        /// Lấy Bộ mẫuthông tin
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ztId"></param>
        /// <returns></returns>
        List<GPackageZTTreeDetailVO> GetGPackageInfoByZtId(string orgId, string ztId, string mbId);

        /// <summary>
        /// mẫu Chi tiết
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
