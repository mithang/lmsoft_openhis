using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// cơ sở cơ sở thông tin
    /// </summary>
    public interface IBaseDataDmnService
    {
        /// <summary>
        /// Cách dùng thuốc Tìm kiếm
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysMedicineUsageVEntity> GetMedicineUsageList();
        /// <summary>
        /// sắc hộ phương kiểu
        /// </summary>
        /// <returns></returns>
        IList<SysAuxiliaryDictionaryEntity> GetDjfsUsageList(string OrganizeId);
        /// <summary>
        /// Lấy Thuốc Đông y sắc hộ phí
        /// </summary>
        /// <returns></returns>
        decimal? GetDjFee(string orgId);

        /// <summary>
        /// Vị trí Nhóm lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyBwFl(string orgId);
        /// <summary>
        /// Kiểm tra Vị trí Nhóm lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyJcBwFl(string orgId);

        /// <summary>
        /// Lấy Hệ thống Vị trí
        /// </summary>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyParts(string orgId);


        /// <summary>
        /// Lấy Hệ thống Vị trí
        /// </summary>
        /// <returns></returns>
        IList<GetYxBodyPartsVO> GetYxBodyParts(string orgId);

        /// <summary>
        /// Lấy Hình ảnh Phương pháp
        /// </summary>
        /// <returns></returns>
        IList<string> GetYxBodyMethod(string orgId, string jcbw);

        IList<GetSysJcBodyPartsVo> GetJcListByOrg(string orgId);
        /// <summary>
        /// Lấy Hệ thống Dặn dò
        /// </summary>
        /// <returns></returns>
        IList<SysDoctorRemarkVO2> GetSysDoctorRemark(string orgId, string ksCode);

        /// <summary>
        /// Lấy Bác sĩ Khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string GetDoctorDepartCode(string orgId, string ysgh);

        void UpdatebrxzInfo(string orgId, string mzh, string zyh, string brxzCode, string brxzmc);

        /// <summary>
        /// Lấy bệnh nhân Thông tin cơ bản
        /// </summary>
        /// <param name="blh">Số bệnh án</param>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPatientBasicInfoVO> SelectXtBrjbxx(string blh, string mzh, string organizeId);

        /// <summary>
        /// Lấy bệnh nhân thẻ thông tin
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPatientCardDetail> SelectCardDetail(string blh, string organizeId);
        /// <summary>
        /// KhoaKhu bệnh quan hệ danh sách
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDeptWardRelVO> GetDeptWardRel(string orgId, string ks, string bq);

		/// <summary>
		/// Lấy Bác sĩ mở thuốc Quyền
		/// </summary>
		/// <param name="orgid"></param>
		/// <param name="gh"></param>
		/// <param name="ypzl"></param>
		/// <returns></returns>
		 int GetPermissions(string orgid, string gh, string tsypzl, string dlcode, string kssqxjb);
        System.Collections.Generic.IList<Newtouch.Domain.Entity.SysMedicineUsageVEntity> GetMediUsageList();
		System.Collections.Generic.IList<Newtouch.Domain.Entity.Inpatient.InpatientMedicinejxCodeComparedEntity> GetMediFormlUsageList();
		System.Collections.Generic.IList<Newtouch.Domain.ValueObjects.WardVO> GetWardListByStaffGh(string gh, string orgId);
	}
}
