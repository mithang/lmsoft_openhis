using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface IInpatientPatientDmnService
    {
        /// <summary>
        /// LấyởKhu bệnhcủabệnh nhânđối tượngtậphợp
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatientzbqResponseDto> GetzbqPatientList(PatientzbqRequestDto req, string OrganizeId);
        /// <summary>
        /// Lấyđãra khỏi khucủabệnh nhânđối tượngtậphợp
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatientycqResponseDto> GetycqPatientList(PatientycqRequestDto req, string OrganizeId);

        /// <summary>
        /// Nhập việnbệnh nhânLọc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="bq"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="zybz">Cờ nội trú, nhiều giá trị phân tách bằng dấu phẩy</param>
        /// <returns></returns>
        IList<InPatientPatientSearchVO> GetInPatSearchPaginationList(Pagination pagination, string orgId, string bq, string zyh, string xm, string ch, string bqCode,string zybz = null);

        /// <summary>
        /// Số nhập viện、Họ tên、Số thẻLấybệnh nhânThông tin cơ bản Họ tên，Giới tính，Tuổi，bệnh nhânTính chất、Ngày nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<patientInfoDto> GetInfoBykeyword(string keyword, string orgId);

        /// <summary>
        /// Số nhập việnLấybệnh nhânThông tin cơ bản Họ tên，Giới tính，Tuổi，bệnh nhânTính chất
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        patientInfoDto GetInfoByZyh(string zyh, string orgId);

        /// <summary>
        /// Nhập việnbệnh nhânLọc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="bq"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="zybz">Cờ nội trú, nhiều giá trị phân tách bằng dấu phẩy</param>
        /// <returns></returns>
        IList<InPatientNursingInputVO> GetInPatSearchList(string orgId, string bq, DateTime rq, string sj, string zyh, string zybz = null);
        IList<InpWardPatTreeVO> GetPatTree(string orgId, string zyzt, string keyword);
        /// <summary>
        /// Nhập việnnhiềubệnh nhânđiều dưỡngnhập
        /// </summary>
        /// <param name="entity"></param>
        void submitmutiple(List<InpatientVitalSignsEntity> entity, string orgId, int? sjd, DateTime rq,string flag);
        InpatientBasicInfoVO GetInpatientBasicInfo(string zyh, string orgId, string zhxz);
		List<String> GetWardCodeByStaffId(string StaffId, string orgId);
        InpWardPatTreeVO GetPatList(string orgId, string zyh);

        /// <summary>
        /// Y lệnhQuản lýtrongcủaChi phíNútTra cứu
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        PatMedInsurSettVO PreSettbyId(string zyh, string orgId);
        List<PatQfWarnVo> PatYjjWarn(string patzyh, string orgId);

		RefSuccess inserghjz(jzdjbrxx brxx);

	}
}
