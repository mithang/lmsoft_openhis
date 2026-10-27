using System;
using Newtouch.HIS.Sett.Request;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

using Newtouch.Application.Interface;
using Newtouch.CIS.Compat;
using Newtouch.Common.Web;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;

using Newtouch.Infrastructure;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Bệnh nhânChẩn đoán nhập viện
    /// </summary>
    public class PatientRyDiagnosisApp : AppBase, IPatientRyDiagnosisApp
    {
        private readonly IInpatientPatientDiagnosisRepo _inpatientPatientDiagnosisRepo;
        private readonly IInpatientPatientInfoRepo _inpatientPatientInfoRepo;

        /// <summary>
        /// LấyChẩn đoán nhập việnthông tin
        /// </summary>
        /// <param name="zhy"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<PatientRyDiagnosisDto> PatientRyDiagnosisQuery(string zhy, string organizeId)
        {
            var patInfoReq = new InPatientDetailQueryRequest
            {
                zyh = zhy,
                OrganizeId = organizeId
            };
            var apiRespzd = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<List<PatientRyDiagnosisDto>>>("/api/Patient/PatientRyDiagnosisQuery", patInfoReq);
            if (apiRespzd.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                throw new Exception(apiRespzd.sub_msg);
            }

            return apiRespzd.data;
        }

        /// <summary>
        /// LưuChẩn đoán nhập viện
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string SavaInPatDiagnosis(PatInAreaVO patInAreaVo, string organizeId, string userCode)
        {
            if (patInAreaVo == null) return "Chẩn đoánnội dungkhông thểđể trống！";
            if (string.IsNullOrWhiteSpace(patInAreaVo.zyh)) return "Số nhập việnkhông thểlàtrống！";
            if (string.IsNullOrWhiteSpace(patInAreaVo.ryzddm1) || string.IsNullOrWhiteSpace(patInAreaVo.ryzdmc1)) return "Chẩn đoán nhập viện1không thểlàtrống！";

            #region Đồng bộđếnSett
            var syncSettResult = SyncInPatDiagnosisToSett(patInAreaVo, organizeId, userCode);
            if (!string.IsNullOrWhiteSpace(syncSettResult)) return syncSettResult;
            #endregion

            var patOutDiaList = _inpatientPatientDiagnosisRepo.SelectData(patInAreaVo.zyh, organizeId, "1");//Lấylịch sửXuất việnChẩn đoán
            var patInfo = _inpatientPatientInfoRepo.GetByZyh(organizeId, patInAreaVo.zyh);//LấyThông tin bệnh nhân
            var operateTime = DateTime.Now;
            try
            {

                using (var db = new EFDbTransaction().BeginTrans())
                {
                    #region sửaThông tin bệnh nhânkho
                    if (patInfo != null)
                    {
                        patInfo.LastModifierCode = userCode;
                        patInfo.LastModifyTime = operateTime;
                        patInfo.zddm = patInAreaVo.ryzddm1;
                        patInfo.zdmc = patInAreaVo.ryzdmc1;
                        db.Update(patInfo);
                    }
                    #endregion

                    #region sửaNhập việnbệnh nhânChẩn đoánthông tin
                    if (patOutDiaList != null && patOutDiaList.Count > 0)
                    {
                        patOutDiaList.ForEach(p =>
                        {
                            p.zt = "0";
                            p.LastModifierCode = userCode;
                            p.LastModifyTime = operateTime;
                            db.Update(p);
                        });
                    }

                    var diaEntity = new InpatientPatientDiagnosisEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        OrganizeId = organizeId,
                        zyh = patInAreaVo.zyh,
                        zdlb = "1",
                        zdlx = "0",
                        zddm = patInAreaVo.ryzddm1,
                        zdmc = patInAreaVo.ryzdmc1,
                        zt = "1",
                        CreateTime = operateTime,
                        CreatorCode = userCode
                    };
                    db.Insert(diaEntity);
                    #endregion

                    db.Commit();
                    return "";
                }

            }
            catch (Exception)
            {
                return "sửaChẩn đoán nhập việnthất bại";
            }
        }

        /// <summary>
        /// Đồng bộChẩn đoán nhập việnđếnquyết toánHệ thống
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        private string SyncInPatDiagnosisToSett(PatInAreaVO patInAreaVo, string organizeId, string userCode)
        {
            var zdxx = new ModifyRyDiagnosisRequestDTO
            {
                zyh = patInAreaVo.zyh,
                OrganizeId = organizeId,
                userCode = userCode,
                RyDiagnosisDetails = new List<RyDiagnosisDetail>
                {
                    new RyDiagnosisDetail
                    {
                        ryzdmc = patInAreaVo.ryzdmc1,
                        ryzdICD10 = patInAreaVo.ryzdICD101,
                        ryzddm = patInAreaVo.ryzddm1,
                        zdpx = "1"
                    },
                    new RyDiagnosisDetail
                    {
                        ryzddm = patInAreaVo.ryzddm2,
                        ryzdICD10 = patInAreaVo.ryzdICD102,
                        ryzdmc = patInAreaVo.ryzdmc2,
                        zdpx = "2"
                    },
                    new RyDiagnosisDetail
                    {
                        ryzddm = patInAreaVo.ryzddm3,
                        ryzdICD10 = patInAreaVo.ryzdICD103,
                        ryzdmc = patInAreaVo.ryzdmc3,
                        zdpx = "3"
                    }
                }
            };
            var apiRespzd = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/patient/ModifyRyDiagnosis", zdxx);
            return apiRespzd.code != APIRequestHelper.ResponseResultCode.SUCCESS ? apiRespzd.sub_msg : "";
        }
    }
}