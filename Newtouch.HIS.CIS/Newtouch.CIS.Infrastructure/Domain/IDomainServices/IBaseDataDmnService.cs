using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// cơ sởcơ sởthông tin
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
        /// sắc hộphươngkiểu
        /// </summary>
        /// <returns></returns>
        IList<SysAuxiliaryDictionaryEntity> GetDjfsUsageList(string OrganizeId);
        /// <summary>
        /// LấyThuốc Đông ysắc hộphí
        /// </summary>
        /// <returns></returns>
        decimal? GetDjFee(string orgId);

        /// <summary>
        /// Vị tríNhóm lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyBwFl(string orgId);
        /// <summary>
        /// Kiểm traVị tríNhóm lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyJcBwFl(string orgId);

        /// <summary>
        /// LấyHệ thốngVị trí
        /// </summary>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyParts(string orgId);


        /// <summary>
        /// LấyHệ thốngVị trí
        /// </summary>
        /// <returns></returns>
        IList<GetYxBodyPartsVO> GetYxBodyParts(string orgId);

        /// <summary>
        /// LấyHình ảnhPhương pháp
        /// </summary>
        /// <returns></returns>
        IList<string> GetYxBodyMethod(string orgId, string jcbw);

        IList<GetSysJcBodyPartsVo> GetJcListByOrg(string orgId);
        /// <summary>
        /// LấyHệ thốngDặn dò
        /// </summary>
        /// <returns></returns>
        IList<SysDoctorRemarkVO2> GetSysDoctorRemark(string orgId, string ksCode);

        /// <summary>
        /// LấyBác sĩKhoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string GetDoctorDepartCode(string orgId, string ysgh);

        void UpdatebrxzInfo(string orgId, string mzh, string zyh, string brxzCode, string brxzmc);

        /// <summary>
        /// Lấybệnh nhânThông tin cơ bản
        /// </summary>
        /// <param name="blh">Số bệnh án</param>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPatientBasicInfoVO> SelectXtBrjbxx(string blh, string mzh, string organizeId);

        /// <summary>
        /// Lấybệnh nhânthẻthông tin
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPatientCardDetail> SelectCardDetail(string blh, string organizeId);
        /// <summary>
        /// KhoaKhu bệnhquan hệdanh sách
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDeptWardRelVO> GetDeptWardRel(string orgId, string ks, string bq);

		/// <summary>
		/// LấyBác sĩmởthuốcQuyền
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
