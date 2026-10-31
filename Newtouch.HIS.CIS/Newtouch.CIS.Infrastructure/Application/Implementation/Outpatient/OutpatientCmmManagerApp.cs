using System;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Newtouch.CIS.Repository;

using Newtouch.Application.Interface;
using Newtouch.CIS.Proxy;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.AERequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.HEALRequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.KRequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;
using Newtouch.CIS.Proxy.Dapper.CMMPlatform;
using Newtouch.Domain.DTO;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.CIS.Compat;
using Newtouch.Repository;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Ngoại trútrongyquánQuản lý
    /// </summary>
    public class OutpatientCmmManagerApp : AppBase, IOutpatientCmmManagerApp
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ITreatmentRepo _treatmentRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly ICmmHis01Repo _cmmHis01Repo;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ICmmHis02RecordDmnService _comCmmHis02RecordDmnService;

        #region TCM_HIS_01

        /// <summary>
        /// Thông tin bệnh nhânTải lên
        /// </summary>
        /// <param name="jzxx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public Result TcmHis01(TreatEntityObj jzxx, string organizeId, string userCode)
        {
            var result = new Result
            {
                code = "1",
                desc = ""
            };
            try
            {
                // QA PHASE 2 (API-06): thiếu jzxx trước đây NRE ở jzxx.mzh và
                // catch trả desc = text NullReference với code "0" (không phải
                // envelope chuẩn — contract {code,desc} của nền tảng GuiAn giữ
                // nguyên, chỉ sửa validation/thông điệp, không đổi envelope).
                if (jzxx == null) return new Result { code = "0", desc = "Thông tin khám bệnh (jzxx) không được để trống" };
                if (CheckIsUpload(jzxx.mzh, organizeId)) return result;
                var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
                if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("vui lòngcấu hìnhtrongyquándòngchínhCơ sởthông tin，Mãlàcmm_configuration");
                var objConfig = confInfo.ToObject<ConfigurationInfo>();
                if (objConfig == null) throw new Exception("Thông tin cơ quan hành chính y quán Trung y cấu hình sai，Vui lòng chú ý cấu trúc！");
                if (string.IsNullOrWhiteSpace(objConfig.areaCode) || string.IsNullOrWhiteSpace(objConfig.jgdm)) throw new Exception("trongyquándòngchínhCơ sởthông tincấu hìnhCơ sởMãvàkhulĩnh vựcMãkhông thểlàtrống！");
                var brxx = _baseDataDmnService.SelectXtBrjbxx(jzxx.blh, jzxx.mzh, organizeId);

                var patientInfo = new Patient
                {
                    id = jzxx.blh,
                    name = jzxx.xm,
                    genderCode = CIS.Proxy.Tools.GetGenderCode(jzxx.xb ?? ""),
                    gender = jzxx.xb ?? "",
                    birthday = jzxx.csny == null ? "" : ((DateTime)jzxx.csny).ToString("yyyy-MM-dd"),
                    cardTypeCode = CIS.Proxy.Tools.CardTypeCodeChange(jzxx.zjlx ?? 0) ?? "",
                    cardType = CIS.Proxy.Tools.GetCardType(jzxx.zjlx ?? 0) ?? "",
                    cardNo = jzxx.zjh ?? "",
                    outpatientNo = jzxx.mzh
                };
                if (brxx != null && brxx.Count > 0)
                {
                    var item = brxx.FirstOrDefault();
                    patientInfo.marriedCode = CIS.Proxy.Tools.ChangeMarriedCode(item.hf == null ? 9 : (int)item.hf);
                    patientInfo.married = CIS.Proxy.Tools.GetMarried(item.hf == null ? 9 : (int)item.hf);
                    patientInfo.patiType = item.brxzmc ?? "";
                    patientInfo.patiTypeCode = "";
                    patientInfo.telephone = item.phone ?? "";

                    if (new[] { "99", "" }.Contains(patientInfo.cardTypeCode) && string.IsNullOrWhiteSpace(patientInfo.cardNo))
                    {
                        patientInfo.cardNo = item.CardNo;
                    }
                }
                patientInfo.countryCode = "";
                patientInfo.country = "";
                patientInfo.nationalityCode = "";
                patientInfo.nationality = "";
                patientInfo.provinceCode = "";
                patientInfo.province = "";
                patientInfo.cityCode = "";
                patientInfo.city = "";
                patientInfo.area = "";
                patientInfo.ctRoleId = "";
                patientInfo.orgCode = objConfig.jgdm ?? "";
                patientInfo.areaCode = objConfig.areaCode;
                var dapper = new TcmHis01(patientInfo);
                var execResult = dapper.Execute();
                result = execResult as Result;
                InsertCmmHis01(dapper.Request, organizeId, userCode, result);
            }
            catch (Exception e)
            {
                // QA PHASE 2 (API-06): NullReference (thường do field null trong
                // jzxx) không được rò text exception ra desc; lỗi nghiệp vụ khác
                // (thiếu cấu hình cmm_configuration...) giữ nguyên message để
                // nền tảng đối tác còn chẩn đoán được.
                // QA PHASE 2 (API-13): lỗi DB (SqlSugar/SqlException, message chứa
                // ConfigId/provider/tiếng Trung) cũng không được rò ra desc —
                // đường này không qua CommonExecute nên sanitize tại chỗ với
                // cùng marker set như DbErrorSanitizer ở tầng API.
                var fullMsg = e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
                var isDbError = e.GetType().Name.Contains("Sql")
                    || (e.InnerException != null && e.InnerException.GetType().Name.Contains("Sql"))
                    || fullMsg.Contains("ConfigId=")
                    || fullMsg.Contains("network-related")
                    || fullMsg.Contains("instance-specific")
                    || fullMsg.Contains("连接数据库");
                var desc = e is NullReferenceException
                    ? "Dữ liệu khám bệnh không đầy đủ, vui lòng kiểm tra và thử lại"
                    : isDbError
                        ? "Không kết nối được cơ sở dữ liệu, vui lòng liên hệ quản trị viên"
                        : fullMsg;
                result = new Result
                {
                    code = "0",
                    desc = desc
                };
                LogCore.Error("OutpatientCmmManagerApp TcmHis01 error", e, result.ToJson());
            }
            return result;
        }

        /// <summary>
        /// kiểm traBệnh nhânlàKhôngĐã tải lên
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        private bool CheckIsUpload(string mzh, string organizeId)
        {
            var entity = _cmmHis01Repo.SelectDataByMzh(mzh, organizeId);
            return entity != null && !string.IsNullOrWhiteSpace(entity.Id);
        }

        /// <summary>
        /// rơiđịayêu cầuDữ liệu
        /// </summary>
        /// <param name="request"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="result"></param>
        private void InsertCmmHis01(PushPatientRequestEntity request, string organizeId, string userCode, Result result)
        {
            result = result ?? new Result();
            var entity = new CmmHis01Entity
            {
                Id = Guid.NewGuid().ToString(),
                cmmId = request.Patient.id,
                name = request.Patient.name,
                gender = request.Patient.gender,
                genderCode = request.Patient.genderCode,
                birthday = request.Patient.birthday,
                cardTypeCode = request.Patient.cardTypeCode,
                cardType = request.Patient.cardType,
                cardNo = request.Patient.cardNo,
                outpatientNo = request.Patient.outpatientNo,
                occupation = request.Patient.occupation,
                occupationCode = request.Patient.occupationCode,
                marriedCode = request.Patient.marriedCode,
                married = request.Patient.married,
                countryCode = request.Patient.countryCode,
                country = request.Patient.country,
                nationality = request.Patient.nationality,
                nationalityCode = request.Patient.nationalityCode,
                province = request.Patient.province,
                provinceCode = request.Patient.provinceCode,
                city = request.Patient.city,
                cityCode = request.Patient.cityCode,
                areaCode = request.Patient.areaCode,
                area = request.Patient.area,
                streetInfo = request.Patient.streetInfo,
                postcode = request.Patient.postcode,
                ctRoleId = request.Patient.ctRoleId,
                ctAddr = request.Patient.ctAddr,
                ctName = request.Patient.ctName,
                ctTelephone = request.Patient.ctTelephone,
                patiTypeCode = request.Patient.patiTypeCode,
                patiType = request.Patient.patiType,
                telephone = request.Patient.telephone,
                orgCode = request.Patient.orgCode,
                sender = request.Header.sender,
                receiver = request.Header.receiver,
                sendTime = request.Header.sendTime,
                msgType = request.Header.msgType,
                msgID = request.Header.msgID,
                OrganizeId = organizeId,
                resultCode = result.code,
                resultDesc = result.desc,
                zt = "1",
                CreatorCode = userCode,
                CreateTime = DateTime.Now
            };
            _cmmHis01Repo.Insert(entity);
        }
        #endregion

        #region TCM_HIS_02

        /// <summary>
        /// Đồng bộ thuốcphẩm
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string PushMedicineInfo(string organizeId, string userCode)
        {
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("vui lòngcấu hìnhtrongyquándòngchínhCơ sởthông tin，Mãlàcmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("Thông tin cơ quan hành chính y quán Trung y cấu hình sai，Vui lòng chú ý cấu trúc！");

            var record = _comCmmHis02RecordDmnService.SelectNoSyncMedicines(30, organizeId);
            if (record == null || record.Count <= 0) return "";

            var cts = new CancellationTokenSource();
            var parent = new Task(() =>
              {
                  var childFactory = new TaskFactory(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                  record.ForEach(p =>
                  {
                      childFactory.StartNew(() =>
                      {
                          SyncMedicines(p, organizeId, userCode, objConfig);
                      }, cts.Token);

                  });
              });
            parent.Start();
            parent.Wait(cts.Token);
            if (!cts.IsCancellationRequested) return "";
            LogCore.Fatal("PushMedicineInfo IsCancellationRequested", message: record.ToJson());
            return "đẩyThảo dược Trung Hoathất bại";
        }

        /// <summary>
        /// Đồng bộ thuốcphẩm,vàLưuĐồng bộghi chépbảng
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="config"></param>
        private void SyncMedicines(SysMedicineExVEntity yp, string organizeId, string userCode, ConfigurationInfo config)
        {
            var cmmHis02Entity = new CmmHis02RecordEntity
            {
                activeFlg = "Y",
                code = yp.ypCode,
                cmmId = yp.ypCode,
                manufacturer = yp.ycmc,
                name = yp.ypmc,
                orgCode = config.jgdm,
                specification = yp.gg,
                OrganizeId = organizeId,
                zt = "1",
                CreatorCode = userCode,
                CreateTime = DateTime.Now
            };
            var medicine = new Medicine
            {
                activeFlg = "Y",
                code = yp.ypCode,
                id = yp.ypCode,
                manufacturer = yp.ycmc,
                name = yp.ypmc,
                orgCode = config.jgdm,
                specification = yp.gg
            };
            var response = new TcmHis02(medicine).Execute();
            var result = response as Result;
            if (result != null)
            {
                cmmHis02Entity.resultCode = result.code;
                cmmHis02Entity.resultDesc = result.desc;
            }

            new CmmHis02RecordRepo().Insert(cmmHis02Entity);
        }

        #endregion

        #region TCM_HIS_07

        /// <summary>
        /// nhắclấyđiện tửHồ sơ
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public Record ExtractIntegrateEmr(string mzh, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(mzh)) return null;
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("vui lòngcấu hìnhtrongyquándòngchínhCơ sởthông tin，Mãlàcmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("Thông tin cơ quan hành chính y quán Trung y cấu hình sai，Vui lòng chú ý cấu trúc！");
            var request = new Receive
            {
                serialNo = mzh,
                orgCode = objConfig.jgdm
            };
            var response = new TcmHis07(request).Execute();
            var record = response as Record;
            return record;
        }
        #endregion

        #region TCM_HIS_08

        /// <summary>
        /// nhắclấyĐơn thuốc
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <param name="mzzybz">Dấu hiệu ngoại trú/nội trú 0-Kho thuốc  1-Ngoại trú  2-Nhập viện  3-Chung(lấyĐơn vị ngoại trú) </param>
        /// <returns></returns>
        public List<DrugDataEx> ExtractIntegrateRp(string mzh, string organizeId, string mzzybz)
        {
            if (string.IsNullOrWhiteSpace(mzh)) return null;
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("vui lòngcấu hìnhtrongyquándòngchínhCơ sởthông tin，Mãlàcmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("Thông tin cơ quan hành chính y quán Trung y cấu hình sai，Vui lòng chú ý cấu trúc！");
            var request = new Receive
            {
                serialNo = mzh,
                orgCode = objConfig.jgdm
            };
            var response = new TcmHis08(request).Execute();
            var prescription = response as Prescription;
            return FormatRpDetail(prescription, organizeId, mzzybz);
        }

        /// <summary>
        /// Chuẩn hóa thông tin đơn thuốc
        /// </summary>
        /// <returns></returns>
        private List<DrugDataEx> FormatRpDetail(Prescription s, string organizeId, string mzzybz)
        {
            var result = new List<DrugDataEx>();
            if (s == null || s.drugDataList == null || s.drugDataList.Count == 0) return result;

            var errorMsg = "";
            var cts = new CancellationTokenSource();
            var parent = new Task(() =>
            {
                var cLocker = new object();
                var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                s.drugDataList.ForEach(p =>
                {
                    var childTask = childFactory.StartNew(() =>
                    {
                        if (cts.IsCancellationRequested)
                        {
                            return false;
                        }

                        try
                        {

                            var reqDto = new SelectSfxmYpFilterDTO
                            {
                                topCount = 50,
                                orgId = organizeId,
                                mzzybz = mzzybz,
                                dllb = "1",
                                sfdllx = "TCM",
                                keyword = p.drugCode,
                                dlCode = "",
                                isContansChildDl = true,
                                useypckflag = true,
                                ypyfbmCode = null,
                                containyp0ck = true,
                                onlyybflag = false,
                                isQyKssKZ = false,
                                qxjb = null
                            };
                            var yp = Newtouch.CIS.Compat.CmmStub.SelectSfxmYp(reqDto);
                            if (yp != null && yp.Count > 0)
                            {
                                var item = yp.FirstOrDefault().ToJson().ToObject<DrugDataEx>();
                                if (item == null)
                                {
                                    errorMsg = "Thuốcthông tinchuyểnhóathất bại";
                                    return false;
                                }

                                if (!"g".Equals((item.dw ?? "").Trim()))
                                {
                                    errorMsg = string.Format("【{0}】Thiếu đơn vịg", item.sfxmmc);
                                    cts.Cancel();
                                    return false;
                                }
                                item.drugQuantity = p.drugQuantity;
                                item.drugUnit = p.drugUnit;
                                item.note = p.note;
                                item.drugName = p.drugName;
                                item.drugCode = p.drugCode;
                                lock (cLocker)
                                {
                                    result.Add(item);
                                }

                                return true;
                            }
                            errorMsg = "khớpđốibảnđịaThuốcthông tinthất bại";
                            cts.Cancel();
                            return false;

                        }
                        catch (Exception e)
                        {
                            errorMsg = e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
                            cts.Cancel();
                            return false;
                        }
                    }, cts.Token);

                });
            });
            parent.Start();
            parent.Wait(cts.Token);
            if (cts.IsCancellationRequested)
            {
                LogCore.Fatal("FormatRpDetail  fail", message: s.ToJson());
                throw new Exception(errorMsg);
            }
            return result;
        }
        #endregion

        #region TCM_HIS_09

        /// <summary>
        /// nhắclấyChẩn đoánthông tin
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public DiagInfo ExtractIntegrateDiagnosis(string mzh, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(mzh)) return null;
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("vui lòngcấu hìnhtrongyquándòngchínhCơ sởthông tin，Mãlàcmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("Thông tin cơ quan hành chính y quán Trung y cấu hình sai，Vui lòng chú ý cấu trúc！");
            var request = new Receive
            {
                serialNo = mzh,
                orgCode = objConfig.jgdm
            };
            var response = new TcmHis09(request).Execute();
            var diagInfo = response as DiagInfo;
            AddLocalZddm(diagInfo, organizeId);
            return diagInfo;
        }

        /// <summary>
        /// kèmthêmbảnđịaChẩn đoánMã
        /// </summary>
        /// <param name="diagInfo"></param>
        /// <param name="organizeId"></param>
        private void AddLocalZddm(DiagInfo diagInfo, string organizeId)
        {
            if (diagInfo == null || diagInfo.diagnosislist == null || diagInfo.diagnosislist.Count <= 0) return;
            var cts = new CancellationTokenSource();
            var parent = new Task(() =>
            {
                var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                diagInfo.diagnosislist.ForEach(p =>
                {
                    var childTask = childFactory.StartNew(() =>
                    {
                        var zdRepo =
                            new SysDiagnosisRepo().SelectDataByICD10(organizeId, p.code);
                        if (zdRepo == null) return false;
                        p.localCode = zdRepo.zdCode;
                        return true;
                    }, cts.Token);
                });
            });
            parent.Start();
            parent.Wait();
            if (cts.IsCancellationRequested)
            {
                LogCore.Fatal("AddLocalZddm CancellationRequested", message: diagInfo.ToJson());
            }
        }
        #endregion

        #region Integrated Module

        /// <summary>
        /// điện tửHồ sơtậpthànhmẫukhối
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public string GetIntegrateEmrUrl(string mzh, string jzys, string organizeId, out string response)
        {
            response = "";
            var entity = _cmmHis01Repo.SelectDataByMzh(mzh, organizeId);
            if (entity == null || string.IsNullOrWhiteSpace(entity.Id)) return "Chưa đẩy thông tin bệnh nhân tới y quán Trung y，Vui lòng chọn bệnh nhân trước";
            var emrUrlRequest = new EMRRequestEntity
            {
                resource = "EMR_WRITE",
                doctorId = jzys,
                patientId = entity.cmmId
            };
            response = CmmProxyCenter<EMRRequestEntity, string>.Execute(emrUrlRequest);
            return "";
        }

        /// <summary>
        /// biệnchứngluậnđiều trị
        /// </summary>
        /// <param name="aeRequest"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public string GetIntegrateAeUrl(GetAeRequestDto aeRequest, out string response)
        {
            response = "";
            if (aeRequest == null || string.IsNullOrWhiteSpace(aeRequest.mzh)) return "Số khám ngoại trú không được để trống";
            if (string.IsNullOrWhiteSpace(aeRequest.organizeId)) return "Tổ chứckhông thểđể trống";
            if (string.IsNullOrWhiteSpace(aeRequest.jzys)) return "Khám bệnhBác sĩMã nhân viênkhông thểđể trống";
            var entity = _cmmHis01Repo.SelectDataByMzh(aeRequest.mzh, aeRequest.organizeId);
            if (entity == null || string.IsNullOrWhiteSpace(entity.Id)) return "Chưa đẩy thông tin bệnh nhân tới y quán Trung y，Vui lòng chọn bệnh nhân trước";

            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", aeRequest.organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) return "vui lòngcấu hìnhtrongyquándòngchínhCơ sởthông tin，Mãlàcmm_configuration";
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) return "Thông tin cơ quan hành chính y quán Trung y cấu hình sai，Vui lòng chú ý cấu trúc！";
            var staff = _sysStaffRepo.GetValidStaffByGh(aeRequest.jzys, aeRequest.organizeId);

            var returnHisUrl = Newtouch.CIS.Compat.AppConfig.Get("returnHisUrl");
            if (string.IsNullOrWhiteSpace(returnHisUrl)) return "vui lòngcấu hìnhQuay lạiDữ liệucho HIS củathaylýtrangmặtĐịa chỉ——key：returnHisUrl";
            var aeUrlRequest = new AERequestEntity
            {
                resource = "AE_EDIT",
                orgCode = objConfig.jgdm,
                chisZggh = aeRequest.jzys,
                chisEmpName = staff.Name,
                clinicId = aeRequest.mzh,
                brxm = entity.name,
                sex = entity.genderCode,
                patientid = entity.cmmId,
                returnHisUrl = returnHisUrl
            };
            response = CmmProxyCenter<AERequestEntity, string>.Execute(aeUrlRequest);
            return "";
        }

        /// <summary>
        /// biếtbiếtkho
        /// </summary>
        /// <returns></returns>
        public string GetIntegrateKUrl(out string response)
        {
            var kRequest = new KRequestEntity
            {
                resource = "K_LOOK"
            };
            response = CmmProxyCenter<KRequestEntity, string>.Execute(kRequest);
            return "";
        }

        /// <summary>
        /// tậpthànhđiều trịchưabệnh
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="organizeId"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public string GetIntegrateHEALUrl(string blh, string mzh, string zjlx, string zjh, string organizeId, out string response)
        {
            response = "";
            if (string.IsNullOrWhiteSpace(mzh)) return "thông tincósai，Số khám ngoại trú không được để trống";
            var entity = _cmmHis01Repo.SelectDataByMzh(mzh, organizeId);
            if (entity == null || string.IsNullOrWhiteSpace(entity.cardTypeCode) ||
                string.IsNullOrWhiteSpace(entity.cardNo))
            {
                return "thông tinkhôngtoàn，đẩyBệnh nhânkhithiếuchứngmụcLoạivàSố giấy tờ";
            }

            int tzjlx = Convert.ToInt32(entity.cardTypeCode);
            var request = new HEALRequestEntity
            {
                resource = "HEAL_CHECKBODY",
                CertificatesType = tzjlx.ToString(),
                CertificatesNumber = entity.cardNo
            };
            response = CmmProxyCenter<HEALRequestEntity, string>.Execute(request);
            return "";
        }

        /// <summary>
        /// lắp rápxatrìnhy tếtham số
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IntegrateRcDto GetIntegrateRCRequsetParam(string blh, string mzh, string jzys, string organizeId)
        {
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("vui lòngcấu hìnhtrongyquándòngchínhCơ sởthông tin，Mãlàcmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("Thông tin cơ quan hành chính y quán Trung y cấu hình sai，Vui lòng chú ý cấu trúc！");
            if (string.IsNullOrWhiteSpace(objConfig.areaCode) || string.IsNullOrWhiteSpace(objConfig.jgdm)) throw new Exception("trongyquándòngchínhCơ sởthông tincấu hìnhCơ sởMãvàkhulĩnh vựcMãkhông thểlàtrống！");

            if (string.IsNullOrWhiteSpace(blh)) throw new Exception("Vui lòng chọnBệnh nhân");
            if (string.IsNullOrWhiteSpace(mzh)) throw new Exception("Dữ liệu bất thường，Số khám ngoại trúđánhmất");
            var brxx = _baseDataDmnService.SelectXtBrjbxx(blh, mzh, organizeId);
            if (brxx == null || brxx.Count <= 0) throw new Exception(string.Format("theoSố bệnh án【{0}】Lấybệnh nhânThông tin cơ bảnthất bại", blh));
            var patientBaseInfo = brxx.FirstOrDefault();
            var staff = _sysStaffRepo.GetValidStaffByGh(jzys, organizeId);

            var result = new IntegrateRcDto
            {
                organCode = objConfig.jgdm,
                doctorId = jzys,
                doctorName = staff.Name,
                patientId = blh,
                patientName = patientBaseInfo.xm,
                mobile = patientBaseInfo.phone,
                gender = patientBaseInfo.xb,
                birthday = patientBaseInfo.csny != null ? ((DateTime)patientBaseInfo.csny).ToString("yyyy-MM-dd") : "",
                patientType = CIS.Proxy.Tools.ChangeBrxzCode(patientBaseInfo.brxz ?? ""),
                series = patientBaseInfo.CardNo,
                certid = "",
            };
            if ("1".Equals((patientBaseInfo.zjlx ?? "").Trim()))
            {
                result.certid = patientBaseInfo.zjh;
            }
            return result;
        }
        #endregion
    }
}