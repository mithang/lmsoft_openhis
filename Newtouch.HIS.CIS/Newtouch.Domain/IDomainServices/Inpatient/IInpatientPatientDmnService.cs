using Newtouch.Core.Common;
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
        /// Lấy tập hợp đối tượng bệnh nhân tại khu bệnh
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatientzbqResponseDto> GetzbqPatientList(PatientzbqRequestDto req, string OrganizeId);
        /// <summary>
        /// Lấy tập hợp đối tượng bệnh nhân đã ra khỏi khu
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatientycqResponseDto> GetycqPatientList(PatientycqRequestDto req, string OrganizeId);

        /// <summary>
        /// Lọc bệnh nhân nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="bq"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="zybz">Cờ đang ở viện,Nhiều cái ngăn cách bằng dấu phẩy</param>
        /// <returns></returns>
        IList<InPatientPatientSearchVO> GetInPatSearchPaginationList(Pagination pagination, string orgId, string bq, string zyh, string xm, string ch, string bqCode,string zybz = null);

        /// <summary>
        /// Số nội trú,Họ tên,Lấy thông tin cơ bản bệnh nhân theo số thẻ Họ tên,Giới tính,Tuổi,Tính chất bệnh nhân,Ngày nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<patientInfoDto> GetInfoBykeyword(string keyword, string orgId);

        /// <summary>
        /// Lấy thông tin cơ bản bệnh nhân theo số nội trú Họ tên,Giới tính,Tuổi,Tính chất bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        patientInfoDto GetInfoByZyh(string zyh, string orgId);

        /// <summary>
        /// Lọc bệnh nhân nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="bq"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="zybz">Cờ đang ở viện,Nhiều cái ngăn cách bằng dấu phẩy</param>
        /// <returns></returns>
        IList<InPatientNursingInputVO> GetInPatSearchList(string orgId, string bq, DateTime rq, string sj, string zyh, string zybz = null);
        IList<InpWardPatTreeVO> GetPatTree(string orgId, string zyzt, string keyword);
        /// <summary>
        /// Nhập điều dưỡng nhiều bệnh nhân nội trú
        /// </summary>
        /// <param name="entity"></param>
        void submitmutiple(List<InpatientVitalSignsEntity> entity, string orgId, int? sjd, DateTime rq,string flag);
        InpatientBasicInfoVO GetInpatientBasicInfo(string zyh, string orgId, string zhxz);
		List<String> GetWardCodeByStaffId(string StaffId, string orgId);
        InpWardPatTreeVO GetPatList(string orgId, string zyh);

        /// <summary>
        /// Tra cứu nút chi phí trong quản lý y lệnh
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        PatMedInsurSettVO PreSettbyId(string zyh, string orgId);
        List<PatQfWarnVo> PatYjjWarn(string patzyh, string orgId);

		RefSuccess inserghjz(jzdjbrxx brxx);

	}
}