using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.WebAPI.Manage.Areas.HisJob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticeJobController : ApiBaseController
    {
        private readonly IMsgQueueDmnService _msgQueueDmn;
        private readonly ISysOrgDmnService _sysOrgVDmn;
        public NoticeJobController(IHttpClientHelper httpClient,
            IMsgQueueDmnService msgQueueDmn, ISysOrgDmnService sysOrgVDmn) : base(httpClient)
        {
            _msgQueueDmn = msgQueueDmn;
            _sysOrgVDmn = sysOrgVDmn;
        }
        /// <summary>
        /// 未处理的即时Tin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("NoticeImmediatelyWaitProc")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<List<MsgNoticeQueueVO>>> NoticeImmediatelyWaitProcAsync(Request<string> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MsgNoticeQueueVO>> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức không được để trống" };
            }
            var orgList = await _sysOrgVDmn.GetOrganizeList(request.OrganizeId);
            List<MsgNoticeQueueVO> msg = new List<MsgNoticeQueueVO>();
            if (orgList.Count > 0)
            {
                foreach (var org in orgList)
                {
                    var msgList = await _msgQueueDmn.NoticeInfoQuery(new MsgNoticeQueueBasicVO
                    {
                        NoticeStu = (int)NoticeStuEnum.Wait,
                        QueueExecType = (int)MsgQueueExecTypeEnum.Immediately,
                        RecipientType = (int)RecipientTypeEnum.user,
                        OrganizeId = org.OrganizeId
                    });
                    if (msgList.Count > 0)
                    {
                        msg.AddRange(msgList);
                    }
                }
            }
            return new BusResult<List<MsgNoticeQueueVO>> { code = ResponseResultCode.SUCCESS, Data = msg.Count == 0 ? null : msg };
        }
        /// <summary>
        /// 未读Tin nhắn
        /// 当天时间间隔超过1h
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("NoticeUnReadProc")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<List<MsgNoticeQueueVO>>> NoticeUnReadProcAsync(Request<string> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MsgNoticeQueueVO>> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức không được để trống" };
            }
            var orgList = await _sysOrgVDmn.GetOrganizeList(request.OrganizeId);
            List<MsgNoticeQueueVO> msg = new List<MsgNoticeQueueVO>();
            if (orgList.Count > 0)
            {
                foreach (var org in orgList)
                {
                    var msgList = await _msgQueueDmn.NoticeInfoQuery(new MsgNoticeQueueBasicVO
                    {
                        NoticeStu = (int)NoticeStuEnum.Send,
                        QueueExecType = (int)MsgQueueExecTypeEnum.Immediately,
                        RecipientType = (int)RecipientTypeEnum.user,
                        OrganizeId = org.OrganizeId
                    });
                    if (msgList.Count > 0)
                    {
                        msgList = msgList.Where(x => DateTime.Compare(Convert.ToDateTime(x.LastModifyTime), DateTime.Now) < 0).ToList();
                        msg.AddRange(msgList);
                    }
                }
            }
            return new BusResult<List<MsgNoticeQueueVO>> { code = ResponseResultCode.SUCCESS, Data = msg };
        }
        /// <summary>
        /// Tin nhắn处理成功
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("NoticeProcSuccess")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<bool>> NoticeProcSuccessAsync(Request<NoticeJobResult> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.QueueId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức、Tin nhắnId không được để trống" };
            }
            //更新Tin nhắn处理状态
            var msgModify = await _msgQueueDmn.NoticeStuModify(request.Data.QueueId, request.AppId, NoticeStuEnum.UnSend);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = msgModify };
        }
        /// <summary>
        /// Tin nhắn处理失败
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("NoticeProcFailed")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<bool>> NoticeProcFailedAsync(Request<NoticeJobResult> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.QueueId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức、Tin nhắnId không được để trống" };
            }
            //更新Tin nhắn处理状态
            var msgModify = await _msgQueueDmn.NoticeStuModify(request.Data.QueueId, request.AppId, NoticeStuEnum.Wait, false);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = msgModify };
        }
        /// <summary>
        /// Tin nhắn发送成功
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("NoticeSendSuccess")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<bool>> NoticeSendSuccessAsync(Request<NoticeJobResult> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.QueueId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức、Tin nhắnId không được để trống" };
            }
            //更新Tin nhắn处理状态
            var msgModify = await _msgQueueDmn.NoticeStuModify(request.Data.QueueId, request.AppId, NoticeStuEnum.Send);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = msgModify };
        }
        /// <summary>
        /// Tin nhắn发送失败
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("NoticeSendFailed")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<bool>> NoticeSendFailedAsync(Request<NoticeJobResult> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.QueueId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức、Tin nhắnId không được để trống" };
            }
            //更新Tin nhắn处理状态
            var msgModify = await _msgQueueDmn.NoticeStuModify(request.Data.QueueId, request.AppId, NoticeStuEnum.UnSend, false);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = msgModify };
        }
        /// <summary>
        /// Tin nhắn đã đọc
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("NoticeRead")]
        [HttpPost]
        public async Task<BusResult<bool>> NoticeReadAsync(Request<NoticeJobResult> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.QueueId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức、Tin nhắnId không được để trống" };
            }
            //更新Tin nhắn处理状态
            var msgModify = await _msgQueueDmn.NoticeStuModify(request.Data.QueueId, request.AppId, NoticeStuEnum.Read);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = msgModify };
        }
        /// <summary>
        /// Tin nhắn đã đọc（批量）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("NoticeListRead")]
        [HttpPost]
        public async Task<BusResult<bool>> NoticeListReadAsync(Request<NoticeJobResult> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || request.Data?.QueueIds?.Length == 0)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức、Tin nhắnId không được để trống" };
            }
            //更新Tin nhắn处理状态
            var msgModify = await _msgQueueDmn.NoticeStuModify(request.Data.QueueIds, request.AppId, NoticeStuEnum.Read);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = msgModify };
        }

    }
}
