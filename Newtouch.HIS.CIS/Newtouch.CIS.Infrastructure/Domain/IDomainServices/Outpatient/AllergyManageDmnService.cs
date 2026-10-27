using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.Outpatient;

namespace Newtouch.Domain.IDomainServices
{
    public interface IAllergyManageDmnService
    {
        /// <summary>
        /// Ngoại trúbệnh nhânLọc
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
        /// Nhập việnbệnh nhânLọc
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
        /// LưuBệnh nhânquamẫnthông tin
        /// </summary>
        /// <param name="entity"></param>
        void SaveAllergyInfo(AllergyEntity entity);

        /// <summary>
        /// XóaThực hiệnquamẫnthông tin
        /// </summary>
        /// <param name="entity"></param>
        int DeleteAllergyInfo(string gmxxId, string orgId, string UserCode, string UserName);

        /// <summary>
        /// LấyNgoại trúBệnh nhânmởcủadathửMục
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <param name="mzzybz">Dấu hiệu ngoại trú/nội trú 1Ngoại trú 2Nhập viện</param>
        /// <returns></returns>
        IList<PsItemVO> GetMzPatientPsItemsList(Pagination pagination, string Id, string orgId, string PsItemCode);

        /// <summary>
        /// LấyBệnh nhân nội trúmởcủadathửMục
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <param name="mzzybz">Dấu hiệu ngoại trú/nội trú 1Ngoại trú 2Nhập viện</param>
        /// <returns></returns>
        IList<PsItemVO> GetZyPatientPsItemsList(Pagination pagination, string zyh, string orgId, string PsItemCode);

        /// <summary>
        /// LấyBệnh nhânquamẫnthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<PatientGmxxDetailVO> GetPatientGmxxList(Pagination pagination, string keyword, string orgId);

        /// <summary>
        /// LấyKháng sinhThuốcthông tin
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<KssYpVO> GetKssYpListData(string orgId);

        /// <summary>
        /// LấyKháng sinhLoại
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<KssYpLbVO> GetKssYpDlData(string ypCode, string orgId);

        string GetPsItemsSetting(string PsItemCode);
    }
}
