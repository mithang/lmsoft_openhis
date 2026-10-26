using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.KPI;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Chia lợi nhuận tháng
    /// </summary>
    public interface IMonthProfitShareConfigDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TherapeutistProfitShareConfigVO> GetTherapeutistPSConfigList(string orgId, string keyword);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        TherapeutistProfitShareConfigVO GetTherapeutistPSConfig(string keyValue);

        /// <summary>
        /// Gửi cấu hình hoa hồng kỹ thuật viên điều trị
        /// </summary>
        /// <param name="entity"></param>
        void SubmitTherapeutistPSConfig(TherapeutistMonthProfitShareConfigEntity entity, string keyValue);

        /// <summary>
        /// Hoa hồng lợi nhuận kỹ thuật viên điều trị Báo cáo cố định
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="curUserCode"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isReGene"></param>
        void DoGenerateTherapeutistPS(string orgId, string curUserCode, int year, int month, bool? isReGene);

        /// <summary>
        /// CheckBáo cáo đã được tạo hay chưa
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        bool TherapeutistPSCheckIsGenerated(string orgId, int year, int month);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<MedicalOrgProfitShareConfigVO> GetMedicalOrgPSConfigList(string orgId, string keyword);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        MedicalOrgProfitShareConfigVO GetMedicalOrgPSConfig(string keyValue);

        /// <summary>
        /// Gửi cấu hình hoa hồng cơ sở y tế
        /// </summary>
        /// <param name="entity"></param>
        void SubmitMedicalOrgPSConfig(MedicalOrgMonthProfitShareConfigEntity entity, string keyValue);

        /// <summary>
        /// Chia lợi nhuận cơ sở y tế Báo cáo cố định
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="curUserCode"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isReGene"></param>
        void DoGenerateMedicalOrgPS(string orgId, string curUserCode, int year, int month, bool? isReGene);

        /// <summary>
        /// CheckBáo cáo đã được tạo hay chưa
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        bool MedicalOrgPSCheckIsGenerated(string orgId, int year, int month);

    }
}
