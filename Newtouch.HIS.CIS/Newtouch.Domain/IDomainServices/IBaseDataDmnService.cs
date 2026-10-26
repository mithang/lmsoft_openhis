using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Thông tin cơ bản
    /// </summary>
    public interface IBaseDataDmnService
    {
        /// <summary>
        /// Cách dùng thuốc Tra cứu
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysMedicineUsageVEntity> GetMedicineUsageList();
        /// <summary>
        /// Cách sắc hộ
        /// </summary>
        /// <returns></returns>
        IList<SysAuxiliaryDictionaryEntity> GetDjfsUsageList(string OrganizeId);
        /// <summary>
        /// Lấy phí sắc hộ thuốc đông y
        /// </summary>
        /// <returns></returns>
        decimal? GetDjFee(string orgId);

        /// <summary>
        /// Nhóm lớn vị trí
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyBwFl(string orgId);
        /// <summary>
        /// Nhóm lớn vị trí kiểm tra
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyJcBwFl(string orgId);

        /// <summary>
        /// Lấy vị trí hệ thống
        /// </summary>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyParts(string orgId);


        /// <summary>
        /// Lấy vị trí hệ thống
        /// </summary>
        /// <returns></returns>
        IList<GetYxBodyPartsVO> GetYxBodyParts(string orgId);

        /// <summary>
        /// Lấy phương pháp hình ảnh
        /// </summary>
        /// <returns></returns>
        IList<string> GetYxBodyMethod(string orgId, string jcbw);

        IList<GetSysJcBodyPartsVo> GetJcListByOrg(string orgId);
        /// <summary>
        /// Lấy dặn dò hệ thống
        /// </summary>
        /// <returns></returns>
        IList<SysDoctorRemarkVO2> GetSysDoctorRemark(string orgId, string ksCode);

        /// <summary>
        /// Lấy khoa của bác sĩ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string GetDoctorDepartCode(string orgId, string ysgh);

        void UpdatebrxzInfo(string orgId, string mzh, string zyh, string brxzCode, string brxzmc);

        /// <summary>
        /// Lấy thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="blh">Số bệnh án</param>
        /// <param name="mzh">Số ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPatientBasicInfoVO> SelectXtBrjbxx(string blh, string mzh, string organizeId);

        /// <summary>
        /// Lấy thông tin thẻ bệnh nhân
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPatientCardDetail> SelectCardDetail(string blh, string organizeId);
        /// <summary>
        /// Danh sách quan hệ khoa - khu bệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDeptWardRelVO> GetDeptWardRel(string orgId, string ks, string bq);

		/// <summary>
		/// Lấy quyền kê thuốc của bác sĩ
		/// </summary>
		/// <param name="orgid"></param>
		/// <param name="gh"></param>
		/// <param name="ypzl"></param>
		/// <returns></returns>
		 int GetPermissions(string orgid, string gh, string tsypzl, string dlcode, string kssqxjb);
	}
}
