using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtouch.EMR.API.Models;
using Newtouch.EMR.Infrastructure;
using Newtouch.HIS.API.Common;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace Newtouch.EMR.API
{
    /// <summary>
    /// Local re-implementation of the binary FrameworkBase.API.ApiControllerBase surface
    /// used by the three EMR.API controllers. The vendor base is not available on .NET Core.
    /// Documented reconstruction (behavior preserved where the originals were observable):
    /// - CommonExecute runs the action inside try/catch and folds exceptions into
    ///   DefaultResponse (code ERROR, message = exception message).
    /// - Token validation: unless the action carries [IgnoreTokenDecrypt], the request
    ///   must carry a token previously issued by SaveIdentity (header "token" or the
    ///   request's Token field when present); an unknown/missing token yields FAIL.
    ///   The original token transport (encrypted header vs field) is not recoverable
    ///   from source, so this port accepts the plain token in either place.
    /// - SaveIdentity stores the identity JSON in Redis with a 2-hour expiry and
    ///   returns a fresh Guid token. The original expiry/key format is not in source;
    ///   key "EMR:ApiIdentity:{token}" is this port's own convention.
    /// - CreateResponse serializes DefaultResponse as JSON (code/msg/data).
    /// </summary>
    public abstract class ApiControllerBase<TControllerType> : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;

        protected ApiControllerBase(IConnectionMultiplexer redis = null)
        {
            _redis = redis;
        }

        /// <summary>Identity resolved from the current request's token, if any.</summary>
        public UserIdentity Identity
        {
            get
            {
                var token = Request?.Headers["token"].ToString();
                if (string.IsNullOrWhiteSpace(token))
                    token = Request?.Query["token"].ToString();
                if (string.IsNullOrWhiteSpace(token) || _redis == null)
                    return null;
                try
                {
                    var json = _redis.GetDatabase().StringGet("EMR:ApiIdentity:" + token);
                    return json.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<UserIdentity>(json);
                }
                catch
                {
                    return null;
                }
            }
        }

        public UserIdentity UserIdentity => Identity;

        protected string SaveIdentity(UserIdentity identity)
        {
            var token = Guid.NewGuid().ToString("N");
            identity.Token = token;
            if (_redis != null)
            {
                try
                {
                    _redis.GetDatabase().StringSet("EMR:ApiIdentity:" + token,
                        JsonConvert.SerializeObject(identity), TimeSpan.FromHours(2));
                }
                catch
                {
                    // Token still returned; a Redis outage must not turn GetToken into a 500.
                }
            }
            return token;
        }

        protected DefaultResponse CommonExecute<TReq>(Action<TReq, DefaultResponse> action, TReq request,
            bool ignoreTokenDecrypt = false)
        {
            var resp = new DefaultResponse();
            try
            {
                if (!ignoreTokenDecrypt && Identity == null)
                {
                    resp.code = APIRequestHelper.ResponseResultCode.FAIL;
                    resp.msg = "Token không hợp lệ hoặc đã hết hạn";
                    resp.sub_code = "TOKEN_IS_EXPIRED";
                    resp.sub_msg = resp.msg;
                    return resp;
                }
                action(request, resp);
                if (resp.code == 0)
                    resp.code = APIRequestHelper.ResponseResultCode.SUCCESS;
            }
            catch (Exception ex)
            {
                var inner = ex;
                while (inner.InnerException != null) inner = inner.InnerException;
                resp.code = APIRequestHelper.ResponseResultCode.ERROR;
                resp.msg = SanitizeDbError(ex, inner);
                resp.sub_code = "INTERNAL_SERVER_ERROR";
                resp.sub_msg = resp.msg;
            }
            return resp;
        }


        /// <summary>QA PHASE 2 (API-13): lỗi mất kết nối DB không được rò ra client
        /// (provider error + ConfigId + tiếng Trung của lớp DB). Chỉ lỗi DB mới bị
        /// thay bằng thông điệp chung; các lỗi khác giữ nguyên message. Chi tiết
        /// đầy đủ được ghi vào log server qua ILoggerFactory khi resolve được.</summary>
        private string SanitizeDbError(System.Exception ex, System.Exception inner)
        {
            for (var e = ex; e != null; e = e.InnerException)
            {
                var typeName = e.GetType().FullName ?? "";
                var msg = e.Message ?? "";
                if (typeName.Contains("Sql") || e is System.Data.Common.DbException
                    || msg.Contains("ConfigId=") || msg.Contains("network-related")
                    || msg.Contains("instance-specific") || msg.Contains("连接数据库"))
                {
                    try
                    {
                        var lf = HttpContext?.RequestServices?.GetService(typeof(Microsoft.Extensions.Logging.ILoggerFactory))
                            as Microsoft.Extensions.Logging.ILoggerFactory;
                        lf?.CreateLogger(GetType().FullName ?? "ApiControllerBase")
                            ?.LogError(ex, "DB error sanitized for client response");
                    }
                    catch
                    {
                    }
                    return "Không kết nối được cơ sở dữ liệu, vui lòng liên hệ quản trị viên";
                }
            }
            return inner.Message;
        }
        protected ContentResult CreateResponse(DefaultResponse response)
        {
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }
    }
}
