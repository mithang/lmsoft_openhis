using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Domain.Entity.RemoteTreated;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Common.EnumExternal;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.Services;
using NewtouchHIS.WebAPI.Manage.Controllers;
using static NewtouchHIS.Lib.Common.EnumExternal.ClinicEnum;
using static NewtouchHIS.Lib.Common.HisEnum;

namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic.Controllers
{
    /// <summary>
    /// Phòng khám điện toán đám mây
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ApiBaseController
    {
        private readonly IRemoteTreatedDmnService _remoteTreatedDmn;
        private readonly IOutpMedicalRecordService _outpMedical;
        private readonly ISysMedicineService _sysMedicine;
        readonly ILogger<ClinicController> _logger;
        public ClinicController(IHttpClientHelper httpClient,
            IRemoteTreatedDmnService remoteTreatedDmn, IOutpMedicalRecordService outpMedical, ISysMedicineService sysMedicine, ILogger<ClinicController> logger) : base(httpClient)
        {
            _remoteTreatedDmn = remoteTreatedDmn;
            _outpMedical = outpMedical;
            _sysMedicine = sysMedicine;
            _logger = logger;
        }
        #region Token
        /// <summary>
        /// Lấy phòng khám Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClinicToken")]
        public async Task<BusResult<ClinicTokenDTO>> GetClinicTokenAsync(Request<string> request)
        {
            var tokenResp = await _httpClient.GetAsync<ClinicResponseBase<ClinicTokenDTO>>(ClinicApiHelper.GetTokenRoute);
            return new BusResult<ClinicTokenDTO> { code = ResponseResultCode.SUCCESS, Data = tokenResp.data };
        }
        #endregion

        #region Thông tin bệnh nhân
        /// <summary>
        /// Lấy phòng khám điện toán đám mâyBệnh nhânBệnh án
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClinicPatMedicalRecord")]
        public async Task<BusResult<PatMedicalRecordResponse>> GetClinicPatMedicalRecordAsync(Request<PatMedicalRecordRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<PatMedicalRecordResponse> { code = ResponseResultCode.FAIL, msg = "Tổ chứcId,HISĐăng kýId không được để trống" };
            }
            var clinicMr = await ClinicHttpGet<List<ClinicPatMedicalRecordDTO>>($"{ClinicApiHelper.GetPatMedicalRecord}{request.Data.ApplyId}");
            if (clinicMr == null || clinicMr.code != ((int)ClinicResponseCode.success).ToString())
            {
                return new BusResult<PatMedicalRecordResponse> { code = ResponseResultCode.FAIL, msg = $"Lấy bệnh án từ bên thứ ba thất bại:{clinicMr?.msg}" };
            }
            var resp = clinicMr.data.Where(p => !string.IsNullOrWhiteSpace(p.patientTell)).FirstOrDefault();
            return new BusResult<PatMedicalRecordResponse> { code = ResponseResultCode.SUCCESS, Data = resp.Adapt<PatMedicalRecordResponse>() };
        }

        /// <summary>
        /// ĐẩyHISBệnh nhânBệnh án
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendHisMedicalRecord")]
        public async Task<BusResult<string>> SendHisMedicalRecordAsync(Request<SendPatMedicalRecordRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Tổ chứcId,HISĐăng kýId không được để trống" };
            }
            var applyInfo = await _remoteTreatedDmn.TreatedApplyExtendInfo(request.Data.ApplyId, request.OrganizeId);
            if (applyInfo == null || applyInfo.sqzt == (int)EmunRemoteTreatedStu.yth || applyInfo.sqzt == (int)EmunRemoteTreatedStu.ycx)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Đăng ký khám chữa bệnh không hợp lệ hoặc trạng thái bất thường, không thể đẩy bệnh án" };
            }
            ClinicPatMedicalRecordDTO clinicRequest = request.Data.Adapt<ClinicPatMedicalRecordDTO>();
            clinicRequest.doctor = new ClinicDicDTO { id = applyInfo.ysgh };
            clinicRequest.diagnosisId = clinicRequest.diagnosisId ?? applyInfo.sqlsh;
            var sendMr = await ClinicHttpPost<object, ClinicPatMedicalRecordDTO>($"{ClinicApiHelper.SendHisPatMedicalRecord}", clinicRequest);
            if (sendMr == null || sendMr.code != ((int)ClinicResponseCode.success).ToString())
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = $"Bên thứ ba trả về thất bại:{sendMr?.msg}" };
            }
            return new BusResult<string> { code = ResponseResultCode.SUCCESS, Data = sendMr.data.ToJson() };
        }
        /// <summary>
        /// ĐẩyHISBệnh nhânĐơn thuốc lần khám này
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendHisOutpPrescriptionData")]
        public async Task<BusResult<string>> SendHisOutpPrescriptionDataAsync(Request<SendPatMedicalRecordRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Tổ chứcId,HISĐăng kýId không được để trống" };
            }
            var applyInfo = await _remoteTreatedDmn.TreatedApplyExtendInfo(request.Data.ApplyId, request.OrganizeId);
            if (applyInfo == null || applyInfo.sqzt == (int)EmunRemoteTreatedStu.yth || applyInfo.sqzt == (int)EmunRemoteTreatedStu.ycx)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Đăng ký khám chữa bệnh không hợp lệ hoặc trạng thái bất thường, không thể đẩy đơn thuốc" };
            }
            var presData = await _outpMedical.OutpPrescriptionDataByMzh(applyInfo.mzh ?? request.Data.mzh, request.OrganizeId);
            var drugPres = presData.Where(p => p.cflx == (int)EnumCflx.WMPres || p.cflx == (int)EnumCflx.TCMPres);
            if (!drugPres.Any())
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy đơn thuốc có thể đẩy" };
            }
            var hisypdw = await _sysMedicine.DrugUnitDic();
            //Đơn thuốc chính
            List<ClinicPatRecipelData> cfList = new List<ClinicPatRecipelData>();
            foreach (var p in drugPres)
            {
                ClinicRecipelDTO cfData = new ClinicRecipelDTO
                {
                    name = p.cfh,//((EnumCflx)p.cflx).GetDescription(),
                    recipelType = new ClinicDicDTO { value = p.cflx == (int)EnumCflx.WMPres ? EnumRecipelType.recipelType_0.ToString() : EnumRecipelType.recipelType_1.ToString() },
                    takeFrequency = new ClinicDicDTO { value = "" },
                    singleDosage = null,
                    fee = p.zje,
                    remarks = p.cfh,
                    entrust = p.cfzt,
                    chinessNotes = null,
                };
                List<ClinicRecipelDetailDTO> cfmxList = new List<ClinicRecipelDetailDTO>();
                if (p.cflx == (int)EnumCflx.TCMPres)
                {
                    cfData.dosage = p.tieshu.ToString();
                    cfData.recipelUse = new ClinicDicDTO { value = !string.IsNullOrWhiteSpace(p.cfyf) ? Enum.GetName((EnumChineseMedicineRecipelUse)Convert.ToInt32(p.cfyf)) : null };
                    cfData.frequency = new ClinicDicDTO { value = "" };
                    cfmxList = (from mx in p.cfmx
                                join jldw in hisypdw on mx.mcjldw equals jldw.ypdwmc
                                join dw in hisypdw on mx.dw equals dw.ypdwmc
                                select new ClinicRecipelDetailDTO
                                {
                                    stuffType = 1,
                                    unitPrice = mx.dj,
                                    allFee = mx.je,
                                    chineseMedicineUse = cfData.recipelUse,
                                    minTotal = mx.sl,
                                    singleDosage = mx.mcjl,
                                    total = mx.sl,
                                    drugStuffId = new ClinicDrugInfoDTO
                                    {
                                        drugStuffId = mx.ypCode,
                                        name = mx.ypmc,
                                        price = mx.dj,
                                        retailPrice = mx.dj,
                                        dosisUnit = new ClinicDicDTO { value = Enum.GetName((EnumMedicalDosisUnit)Convert.ToInt32(jldw.ypdwCode)) ?? mx.mcjldw, },
                                        preparationUnit = new ClinicDicDTO { value = Enum.GetName((EnumMedicalPreparationUnit)Convert.ToInt32(dw.ypdwCode)) ?? mx.dw, },
                                        pack = new ClinicDicDTO { },
                                    }
                                }).ToList();
                }
                else
                {
                    cfmxList = (from mx in p.cfmx
                                join jldw in hisypdw on mx.mcjldw equals jldw.ypdwmc
                                join dw in hisypdw on mx.dw equals dw.ypdwmc
                                select new ClinicRecipelDetailDTO
                                {
                                    stuffType = 2,
                                    unitPrice = mx.dj,
                                    allFee = mx.je,
                                    westernMedicineUse = new ClinicDicDTO { value = "" },
                                    frequency = new ClinicDicDTO { value = "" },
                                    days = new ClinicDicDTO { name = mx.ts.ToString() },
                                    minTotal = null,
                                    singleDosage = mx.mcjl,
                                    total = mx.sl,
                                    drugStuffId = new ClinicDrugInfoDTO
                                    {
                                        drugStuffId = mx.ypCode,
                                        name = mx.ypmc,
                                        price = mx.dj,
                                        retailPrice = mx.dj,
                                        dosisUnit = new ClinicDicDTO { value = Enum.GetName((EnumMedicalDosisUnit)Convert.ToInt32(jldw.ypdwCode)) ?? mx.mcjldw, },
                                        preparationUnit = new ClinicDicDTO { value = Enum.GetName((EnumMedicalPreparationUnit)Convert.ToInt32(dw.ypdwCode)) ?? mx.dw, },
                                        pack = new ClinicDicDTO { },
                                    }
                                }).ToList();

                }
                cfList.Add(new ClinicPatRecipelData { recipelInfo = cfData, recipelDetailEvtList = cfmxList });
            }

            var clinicRequest = new ClinicPatRecipelDataSync
            {
                recipelInfoEvtList = cfList,
                id = applyInfo.sqlsh
            };
#if DEBUG
            _logger.LogInformation(clinicRequest.ToJson());
#endif
            var sendMr = await ClinicHttpPost<object, ClinicPatRecipelDataSync>($"{ClinicApiHelper.SendHisRecipelData}", clinicRequest);
            if (sendMr == null || sendMr.code != ((int)ClinicResponseCode.success).ToString())
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = $"Bên thứ ba trả về thất bại:{sendMr?.msg}" };
            }
            return new BusResult<string> { code = ResponseResultCode.SUCCESS, Data = sendMr.data.ToJson() };
        }

        #endregion

        #region Loại thông báo
        /// <summary>
        /// Đăng ký khám chữa bệnhThông báo kết quả
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TreatedApplyResult")]
        public async Task<BusResult<bool>> TreatedApplyResultAsync(Request<TreatedApplyResultRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Tổ chứcId,HISĐăng kýId không được để trống" };
            }
            var applyInfo = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.ApplyId, request.OrganizeId);
            if (applyInfo == null || applyInfo.sqzt == (int)EmunRemoteTreatedStu.ycx)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy đăng ký khám chữa bệnh hợp lệ" };
            }
            if (request.Data.ApplyStu == (int)EmunRemoteTreatedStu.yfy)
            {
                applyInfo.sqzt = (int)EmunRemoteTreatedStu.yfy;
                await _remoteTreatedDmn.ModifiedApplyStu(applyInfo.Adapt<TreatedApplyEntity>(), request.AppId);
            }
            var sqztEx = ClinicApiHelper.ApplyStuExChange(request.Data.ApplyStu);
            if (sqztEx < 0)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Trạng thái đăng ký bất thường" };
            }
            var Resp = await ClinicHttpPost<int, TreatedApplyResultDTO>(ClinicApiHelper.TreatedApplyResultNotice,
                new TreatedApplyResultDTO
                {
                    id = request.Data.Sqlsh,
                    applicationResults = request.Data.IsConfirm ? "0" : "1",
                    conferenceId = request.Data?.roomid,
                    status = sqztEx.ToString()
                });
            if (Resp == null || Resp.code != ((int)ClinicResponseCode.success).ToString())
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = Resp?.msg ?? "Hệ thống bên thứ ba không trả kết quả", Data = false };
            }
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = true };

        }

        #endregion


        #region http
        private async Task<ClinicResponseBase<TResponse>> ClinicHttpGet<TResponse>(string url)
        {
            var header = ClinicApiHelper.HeaderItems(null);
            var resp = await _httpClient.GetAsync<ClinicResponseBase<TResponse>>(url, header);
            //Xác thực thất bại thì lấy lạitoken
            if (resp != null && resp.code == ((int)ClinicResponseCode.tokenfailed).ToString())
            {
                var tokenResp = await _httpClient.GetAsync<ClinicResponseBase<ClinicTokenDTO>>(ClinicApiHelper.GetTokenRoute);
                if (tokenResp != null && tokenResp.code == ((int)ClinicResponseCode.success).ToString())
                {
                    header = ClinicApiHelper.HeaderItems(tokenResp.data.token);
                    resp = await _httpClient.GetAsync<ClinicResponseBase<TResponse>>(url, header);
                }
                else
                {
                    throw new Exception($"Lấy Token từ bên thứ ba bất thường: Phản hồi: {tokenResp?.ToJson()}");
                }
            }
            return resp;
        }
        private async Task<ClinicResponseBase<TResponse>> ClinicHttpPost<TResponse, TRequest>(string url, TRequest? request)
        {
            var header = ClinicApiHelper.HeaderItems(null);
            var resp = await _httpClient.PostAsync<ClinicResponseBase<TResponse>>(url, request!.ToJson(), header);
            //Xác thực thất bại thì lấy lạitoken
            if (resp != null && resp.code == ((int)ClinicResponseCode.tokenfailed).ToString())
            {
                var tokenResp = await _httpClient.GetAsync<ClinicResponseBase<ClinicTokenDTO>>(ClinicApiHelper.GetTokenRoute);
                if (tokenResp != null && tokenResp.code == ((int)ClinicResponseCode.success).ToString())
                {
                    header = ClinicApiHelper.HeaderItems(tokenResp.data.token);
                    resp = await _httpClient.PostAsync<ClinicResponseBase<TResponse>>(url, request!.ToJson(), header);
                }
                else
                {
                    throw new Exception($"Lấy Token từ bên thứ ba bất thường: Phản hồi: {tokenResp?.ToJson()}");
                }
            }
            return resp;
        }
        #endregion

        #region private

        #endregion
    }
}
