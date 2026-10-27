using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.IRepository;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IZybrjbxxDmnService
    {

        void Sync_HisPatinfo(DateTime lastUpdate, OperatorModel user);
        void Sync_HisPatinfo(string OrgId, DateTime Bdate, DateTime Edate);
        List<PatientzbqResponseDto> GetzbqPatientList(PatientzbqRequestDto req,string OrgId);
        List<PatientycqResponseDto> GetycqPatientList(PatientycqRequestDto req, string OrgId);
        List<PatientmyResponseDto> GetmyPatientList(PatientmyRequestDto req, string OrgId);
        /// <summary>
        /// Lấy thông tin bệnh nhân theo từ khóa
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<PatientmyResponseDto> GetPatInfoBykeyword(string keyword, string orgId);
        /// <summary>
        /// Lấy cây bệnh án của bệnh nhân
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<PatMedRecordTreeVO> GetPatMedRecordTree(string OrgId, string zyh, string rygh);
        /// <summary>
        /// Lấy từ điển bệnh án
        /// </summary>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        IList<MedRecordTypeVO> GetSysItemDic(string OrgId, string Code,string bllxId);
        /// <summary>
        /// Danh sách thông tin bệnh nhân(Đang nằm viện/Ra viện)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<PatListVO> GetPatList(Pagination pagination, string keyword, string zyh, string cyts, string blzt, string ysgh, string OrgId, int type, string bq = null, string appId = null);
        /// <summary>
        /// Danh sách bệnh nhân của bác sĩ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ysgh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        IList<PatListVO> GetMyPatList(Pagination pagination, string keyword, string ysgh, string OrgId);

        void UpdtPatMedRecord(string blId, string blgxId, string OrgId, OperatorModel user);
        /// <summary>
        /// Lấy thông tin cơ bản của bệnh nhân trang đầu bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        BabasyVO GetPatBasicInfo_basy(string orgId, string zyh);

        string GetCwNameByCode(string cw,string orgId,string bq);

        string selectBJQX(string blId, string blgxId, string OrganizeId, OperatorModel user);
    }

}
