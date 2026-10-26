using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.Outpatient;

namespace Newtouch.Domain.IDomainServices
{
    public interface IAllergyManageDmnService
    {
        /// <summary>
        /// Lọc bệnh nhân ngoại trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OutHosPatientVO> GetOutPatientPaginationList(Pagination pagination, string keyword, string ks, string kssj, string jssj, string orgId, string PsItemCode, string Status);

        /// <summary>
        /// Lọc bệnh nhân nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<InHosPatientVO> GetInPatientPaginationList(Pagination pagination, string keyword, string ks, string kssj, string jssj, string orgId, string PsItemCode, string Status);


        /// <summary>
        /// Lưu thông tin dị ứng bệnh nhân
        /// </summary>
        /// <param name="entity"></param>
        void SaveAllergyInfo(AllergyEntity entity);

        /// <summary>
        /// Xóa thông tin dị ứng đã thực hiện
        /// </summary>
        /// <param name="entity"></param>
        int DeleteAllergyInfo(string gmxxId, string orgId, string UserCode, string UserName);

        /// <summary>
        /// Lấy hạng mục thử da bệnh nhân ngoại trú đã kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <param name="mzzybz">Cờ ngoại trú/nội trú 1Ngoại trú 2Nội trú</param>
        /// <returns></returns>
        IList<PsItemVO> GetMzPatientPsItemsList(Pagination pagination, string Id, string orgId, string PsItemCode);

        /// <summary>
        /// Lấy hạng mục thử da bệnh nhân nội trú đã kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <param name="mzzybz">Cờ ngoại trú/nội trú 1Ngoại trú 2Nội trú</param>
        /// <returns></returns>
        IList<PsItemVO> GetZyPatientPsItemsList(Pagination pagination, string zyh, string orgId, string PsItemCode);

        /// <summary>
        /// Lấy thông tin dị ứng bệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<PatientGmxxDetailVO> GetPatientGmxxList(Pagination pagination, string keyword, string orgId);

        /// <summary>
        /// Lấy thông tin thuốc kháng sinh
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<KssYpVO> GetKssYpListData(string orgId);

        /// <summary>
        /// Lấy loại kháng sinh
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<KssYpLbVO> GetKssYpDlData(string ypCode, string orgId);

        string GetPsItemsSetting(string PsItemCode);
    }
}