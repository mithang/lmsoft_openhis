using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Domain.Entity.RemoteTreated;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Domain.IDomainService.PatientCenter;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.Services;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models;
using System.Text;
using static NewtouchHIS.Lib.Common.HisEnum;

namespace NewtouchHIS.WebAPI.Manage.Areas.CIS.Controllers
{
    /// <summary>
    /// Khám chữa bệnh từ xa
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RemoteTreatedController : ApiBaseController
    {
        private readonly IMsgQueueDmnService _msgQueue;
        private readonly IPatientInfoDmnService _patientInfoDmn;
        private readonly ISysStaffDmnService _sysStaffDmn;
        private readonly IRemoteTreatedDmnService _remoteTreatedDmn;
        public RemoteTreatedController(IHttpClientHelper httpClient,
            IMsgQueueDmnService msgQueue, IPatientInfoDmnService patientInfoDmn, ISysStaffDmnService sysStaffDmn, IRemoteTreatedDmnService remoteTreatedDmn)
            : base(httpClient)
        {
            _msgQueue = msgQueue;
            _patientInfoDmn = patientInfoDmn;
            _sysStaffDmn = sysStaffDmn;
            _remoteTreatedDmn = remoteTreatedDmn;
        }
        /// <summary>
        /// Khám chữa bệnh từ xaStep1:Bên thứ baĐăng kýHISKhám chữa bệnh từ xa(Chỉ tự chi trả)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoteTreatedApplyZfOnly")]
        public async Task<BusResult<TreatedApplyReponse>> RemoteTreatedApplyZfOnlyAsync(Request<TreatedApplyBaseVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyOrg) || string.IsNullOrWhiteSpace(request.Data.ApplyOrgName))
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức đăng ký khám chữa bệnh không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.xm) || string.IsNullOrWhiteSpace(request.Data.kh) || request.Data.patid == null)
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng của bệnh nhân:Mã định danh bệnh nhân,Họ tên bệnh nhân,Thẻ khám bệnh không được để trống" };
            }
            //Kiểm tra thông tin bệnh nhân,Bác sĩ khoa
            var patInfo = await _patientInfoDmn.GetPatientbyPatid(Convert.ToInt32(request.Data.patid), request.OrganizeId);
            var card = await _patientInfoDmn.GetPatientCard(Convert.ToInt32(request.Data.patid), request.OrganizeId, request.Data.kh);
            if (card.Count == 0)
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "Thông tin thẻ bệnh nhân bất thường" };
            }
            //Tìm thông tin bác sĩ
            var doc = await _sysStaffDmn.GetStaffDeptByGh(request.Data.ysgh, request.OrganizeId);
            //Tạm thời không kiểm tra khoa
            if (doc.Count == 0)
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy thông tin bác sĩ liên quan" };
            }
            var applyInfo = request.Data.Adapt<TreatedApplyVO>();
            applyInfo.brxz = ((int)EnumBrxz.zf).ToString();
            applyInfo.xb = patInfo.xb;
            applyInfo.birth = patInfo.csny;
            applyInfo.OrganizeId = request.OrganizeId;
            applyInfo.AppId = request.AppId;
            var applyId = await _remoteTreatedDmn.AddTreatedApply(applyInfo, request.AppId);
            return new BusResult<TreatedApplyReponse>
            {
                code = ResponseResultCode.SUCCESS,
                msg = $"Gửi đăng ký khám chữa bệnh thành công",
                Data = new TreatedApplyReponse
                {
                    ApplyId = applyId,
                    sqr = request.Data.sqr,
                    sqlsh = request.Data.sqlsh,
                    sqrgh = request.Data.sqrgh,
                    ApplyOrg = request.Data.ApplyOrg,
                    ApplyOrgName = request.Data.ApplyOrgName,
                }
            };
        }
        #region HIS Nội bộ
        /// <summary>
        /// Khám chữa bệnh từ xaStep1:HISBác sĩ đồng ýĐăng kýKhởi xướng khám chữa bệnhCuộc họp
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Cuộc họpSố,Người dùng khám chữa bệnhToken,Cuộc họpĐịa chỉ</returns>
        [HttpPost]
        [Route("DoctorMeetingApply")]
        public async Task<BusResult<MeettingResponse>> DoctorMeetingApplyAsync(Request<TreatedUserRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            //Truy vấnĐăng kýCó bình thường hay không 
            var apply = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.applyId, request.OrganizeId);
            if (apply == null || (apply.sqzt != (int)EmunRemoteTreatedStu.dqr && apply.sqzt != (int)EmunRemoteTreatedStu.jzz))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "Trạng thái đăng ký khám chữa bệnh bất thường, vui lòng xác nhận đã kết thúc hoặc thu hồi hay chưa" };
            }
            if (DateTime.Compare(apply.sqsj ?? DateTime.Now, DateTime.Now.Date) < 0)
            {
                throw new FailedException($"Thời gian đăng ký khám chữa bệnh: {apply.sqsj} đã hết hạn");
            }
            //Cả hai bên đều có thể khởi xướngCuộc họp
            if (!(apply.sqr == request.Data.usercode || apply.ysgh == request.Data.usercode))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "Cuộc họp khám chữa bệnh phải do bác sĩ khám chữa bệnh hoặc người đăng ký khám chữa bệnh khởi tạo" };
            }
            //Bản đăng ký nàyĐăng kýchưa tạoCuộc họpSố Tạo số phòng trò chuyện
            if (string.IsNullOrWhiteSpace(apply.mettingId) || request.Data.roomReset)
            {
                ChatRoom cr = new ChatRoom();
                apply.mettingId = cr.GetNewRoomId(request.OrganizeId).ToString();
            }
            var tokenReq = new MeettingTokenRequest
            {
                roomid = apply.mettingId,
                device = MediaTerminalType.Browser.ToString(),
                username = $"{request.Data.username}({request.Data.usercode})"
            };
            var tokenResp = await TreatedMeetingToken(tokenReq);
            if (apply.sqzt != (int)EmunRemoteTreatedStu.jzz || !string.IsNullOrWhiteSpace(request.Data.mzh))
            {
                //Ghi lại từ xaĐăng ký
                apply.sqzt = (int)EmunRemoteTreatedStu.jzz;
                apply.mzh = request.Data.mzh;
                var modifyApply = await _remoteTreatedDmn.ModifiedApplyStu(apply.Adapt<TreatedApplyEntity>(), request.AppId);
                if (!modifyApply)
                {
                    return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "Ghi lại số cuộc họp thất bại,Vui lòng đăng ký lại cuộc họp" };
                }
            }
            //Thông báo phòng khám điện toán đám mây 
            //Đăng kýId  Đồng ý khám chữa bệnh :True Cuộc họpSố:roomid
            return new BusResult<MeettingResponse> { code = ResponseResultCode.SUCCESS, Data = tokenResp };
        }
        #endregion

        /// <summary>
        /// Khám chữa bệnh từ xaStep3: Người dùng thông thường tham giaCuộc họp
        /// Giá trị trả về dùng choCuộc họpkiểm chứng trang phòng
        /// To:Bên thứ ba,HIS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserJoinMeetingApply")]
        public async Task<BusResult<MeettingResponse>> UserJoinMeetingApplyAsync(Request<TreatedUserRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.roomid))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "Cuộc họpId không được để trống" };
            }
            var tokenReq = new MeettingTokenRequest
            {
                roomid = request.Data.roomid,
                device = MediaTerminalType.Browser.ToString(),
                username = $"{request.Data.username}({request.Data.usercode})"
            };
            var tokenResp = await TreatedMeetingToken(tokenReq);

            return new BusResult<MeettingResponse> { code = ResponseResultCode.SUCCESS, Data = tokenResp };
        }

        /// <summary>
        /// Truy vấn thông tin khám chữa bệnh by HISĐăng ký khám chữa bệnhId
        /// To:Bên thứ ba,HIS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TreatedApplyInfo")]
        public async Task<BusResult<TreatedApplyVO>> TreatedApplyInfoAsync(Request<TreatedApplyInfoRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<TreatedApplyVO> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyId) && string.IsNullOrWhiteSpace(request.Data.sqlsh))
            {
                return new BusResult<TreatedApplyVO> { code = ResponseResultCode.FAIL, msg = "His đăng ký Id, số thứ tự đăng ký của bên thứ ba không được đồng thời để trống" };
            }
            var response = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.ApplyId, request.OrganizeId, request.Data.sqlsh);
            return new BusResult<TreatedApplyVO> { code = ResponseResultCode.SUCCESS, Data = response };
        }
        /// <summary>
        /// Từ chốiĐăng ký khám chữa bệnh 
        /// To:HIS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TreatedApplyRefused")]
        public async Task<BusResult<bool>> TreatedApplyRefusedAsync(Request<TreatedApplyInfoRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "His đăng ký Id, số thứ tự đăng ký của bên thứ ba không được đồng thời để trống" };
            }
            var response = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.ApplyId, request.OrganizeId);
            if (response == null)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy thông tin đăng ký khám chữa bệnh hợp lệ" };
            }
            if (response.sqzt == (int)EmunRemoteTreatedStu.yth)
            {
                return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "Đăng ký khám chữa bệnh đã được xử lý từ chối" };
            }
            if (response.sqzt != (int)EmunRemoteTreatedStu.dqr)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Trạng thái đăng ký đã thay đổi,Không thể từ chối" };
            }
            response.sqzt = (int)EmunRemoteTreatedStu.yth;
            var cancel = await _remoteTreatedDmn.ModifiedApplyStu(response.Adapt<TreatedApplyEntity>(), request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "Từ chối đăng ký khám chữa bệnh thành công", Data = cancel };
        }
        //[HttpPost]
        //[Route("GotoMeeting")]
        //public async Task<HttpResponseMessage> GotoMeeting(Request<MeettingResponse> request)
        //{
        //    HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.Moved);
        //    resp.Headers.Location = new Uri(RemoteTreatedHelper.RemoteTreatedMeettingRoute);
        //    return resp;
        //}

        #region Hệ thống ngoài Only
        /// <summary>
        /// Thu hồiĐăng ký khám chữa bệnh 
        /// To:Bên thứ ba
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TreatedApplyCancel")]
        public async Task<BusResult<bool>> TreatedApplyCancelAsync(Request<TreatedApplyInfoRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "His đăng ký Id, số thứ tự đăng ký của bên thứ ba không được đồng thời để trống" };
            }
            var response = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.ApplyId, request.OrganizeId);
            if (response == null)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy thông tin đăng ký khám chữa bệnh hợp lệ" };
            }
            if (response.sqzt == (int)EmunRemoteTreatedStu.ycx)
            {
                return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "Đăng ký khám chữa bệnh đã bị hủy" };
            }
            if (response.sqzt != (int)EmunRemoteTreatedStu.dqr)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Trạng thái đăng ký đã thay đổi,Không thể thu hồi" };
            }
            response.sqzt = (int)EmunRemoteTreatedStu.ycx;
            var cancel = await _remoteTreatedDmn.ModifiedApplyStu(response.Adapt<TreatedApplyEntity>(), request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "Hủy đăng ký khám chữa bệnh thành công", Data = cancel };
        }

        /// <summary>
        /// Thông báo phát thuốc 
        /// To:Bên thứ ba
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DrugDispensing")]
        public async Task<BusResult<bool>> DrugDispensingAsync(Request<TreatedApplyInfoRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "His đăng ký Id, số thứ tự đăng ký của bên thứ ba không được đồng thời để trống" };
            }
            var response = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.ApplyId, request.OrganizeId);
            if (response == null)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy thông tin đăng ký khám chữa bệnh hợp lệ" };
            }
            response.sqzt = (int)EmunRemoteTreatedStu.yfy;
            var fy = await _remoteTreatedDmn.ModifiedApplyStu(response.Adapt<TreatedApplyEntity>(), request.AppId);
            //Gọi bên thứ ba Thông báo kết quả khám chữa bệnh Giao diện
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "Cập nhật trạng thái phát thuốc thành công", Data = fy };
        }

        #endregion

        /// <summary>
        /// Giải mã thông tin danh tính người dùng khám chữa bệnh
        /// To:Bên thứ ba,HIS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserIdDecode")]
        public async Task<BusResult<string>> UserIdDecodeAsync(Request<string> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            string data = await Task.Run(() => RemoteTreatedHelper.GetRtcuser(request.Data));
            return new BusResult<string> { code = ResponseResultCode.SUCCESS, Data = data };
        }



        #region private
        /// <summary>
        /// Đóng gói giao diện dịch vụ âm thanh/hình ảnh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        private async Task<MeettingResponse> TreatedMeetingToken(MeettingTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.roomid) || string.IsNullOrWhiteSpace(request.username))
            {
                throw new FailedException("Số phòng họp âm thanh/hình ảnh và tên người dùng không được để trống");
            }
            MeettingTokenRequest tokenReq = new MeettingTokenRequest
            {
                roomid = request.roomid,
                username = request.username,
                device = request.device ?? MediaTerminalType.Browser.ToString()
            };
            var tokenResp = await MediaTokenHttpPost(tokenReq);

            //var tokenResp = new MeettingTokenResponse
            //{
            //    username = request.username,
            //    rtcuserid = RemoteTreatedHelper.SetRtcuser(request.username),
            //    roomid = request.roomid,
            //    status = "Success"
            //};
            if (tokenResp.status != "Success" || !string.IsNullOrWhiteSpace(tokenResp.errorCode))
            {
                throw new FailedException("Giao diện cuộc họp âm thanh/hình ảnh trả về bất thường:" + tokenResp.errorCode);
            }
            MeettingResponse resp = tokenResp.Adapt<MeettingResponse>();
            resp.organization = RemoteTreatedHelper.Organization;
            resp.device = tokenReq.device;
            resp.roompath = RemoteTreatedHelper.RemoteTreatedMeettingRoute;
            //Tự động giải mã
            resp.rtcuserid = await Task.Run(() => RemoteTreatedHelper.GetRtcuser(tokenResp.rtcuserid));
            return resp;
        }

        private async Task<MeettingTokenResponse> MediaTokenHttpPost(MeettingTokenRequest request)
        {
            var header = RemoteTreatedHelper.HeaderItems();
            return await _httpClient.PostAsync<MeettingTokenResponse>($"{RemoteTreatedHelper.OuterMediaTokenRoute}", request.ToJson(), header, Encoding.UTF8);
        }

        #endregion
    }
}
