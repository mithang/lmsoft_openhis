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
        /// Theo tổ chức,Cấp,Lấy danh sách phân loại kháng sinh
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Level"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeVO> GetValidListByOrg(string OrganizeId, string Level, string parentId);
        /// <summary>
        /// Theo tổ chức,Cấp trênId,Lấy danh sách phân loại kháng sinh
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeVO> GetListByParentId(string OrganizeId, string parentId);
        /// <summary>
        /// Gửi thông tin phân loại kháng sinh
        /// </summary>
        /// <param name="entity"></param>
        void SubmitForm(SysMedicineAntibioticTypeVO entity);
    }
}
