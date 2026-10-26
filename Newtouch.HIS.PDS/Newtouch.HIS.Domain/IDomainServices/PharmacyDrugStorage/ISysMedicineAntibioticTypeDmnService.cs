using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface ISysMedicineAntibioticTypeDmnService
    {
        SysMedicineAntibioticTypeVO GetById(string orgId, string Id);
        /// <summary>
        /// theo tổ chức, cấp bậc, lấy danh sách phân loại kháng sinh
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Level"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeVO> GetValidListByOrg(string OrganizeId, string Level, string parentId);
        /// <summary>
        /// theo tổ chức,cấp trên Id,lấy danh sách phân loại kháng sinh
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeVO> GetListByParentId(string OrganizeId, string parentId);
        /// <summary>
        /// gửi kháng sinh phần loại thông tin
        /// </summary>
        /// <param name="entity"></param>
        void SubmitForm(SysMedicineAntibioticTypeVO entity);
    }
}
