using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtouch.Herp.API.Common;
using Newtouch.Herp.API.Models;

namespace Newtouch.Herp.API
{
    /// <summary>
    /// Minimal ASP.NET Core port of FrameworkBase.API.ApiControllerBase, following
    /// the PDS source mirror (Newtouch.HIS.PDS/Newtouch.PDS.API/App_Start/
    /// ApiControllerBase.cs) as the surface reference. Differences from the
    /// Framework base, all deliberate:
    /// - Autofac IComponentContext is replaced by constructor-injected
    ///   IUserIdentityResolver (MS DI); the private-readonly-field auto-resolve
    ///   (PRFAutoResolve) is unnecessary because Core controllers declare their
    ///   dependencies in the constructor.
    /// - HttpResponseMessage is replaced by ContentResult carrying the same
    ///   indented JSON (Newtonsoft, date format yyyy-MM-dd HH:mm:ss).
    /// - The AppLogger monitor log is not reproduced (AddLibLogging still
    ///   provides request logging); error mapping is identical.
    /// - Generic-exception sub_msg carries only the message, not the stack
    ///   trace the original appended (avoids leaking internals).
    /// All user-visible token messages are translated to Vietnamese.
    /// </summary>
    public abstract class ApiControllerBase<TControllerType> : ControllerBase
    {
        private readonly IUserIdentityResolver<Identity> _userIdentityResolver;

        protected ApiControllerBase(IUserIdentityResolver<Identity> userIdentityResolver)
        {
            _userIdentityResolver = userIdentityResolver;
        }

        /// <summary>Authenticated identity for this request (null when no valid token).</summary>
        protected Identity Identity { get; private set; }

        /// <summary>Herp identity view (cast of Identity), as in the original Herp base.</summary>
        protected UserIdentity UserIdentity => Identity as UserIdentity;

        /// <summary>True when the controller or the named action carries DefaultAuthorizeAttribute.</summary>
        protected bool RequiresAuthorize(string actionName)
        {
            var controllerType = GetType();
            if (controllerType.GetCustomAttributes(typeof(DefaultAuthorizeAttribute), inherit: true).Any())
            {
                return true;
            }
            var method = controllerType.GetMethod(actionName);
            return method != null
                && method.GetCustomAttributes(typeof(DefaultAuthorizeAttribute), inherit: true).Any();
        }

        protected void Authorize(string token, string tokenType, bool needAuthorize)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                // QA PHASE 2 (API-11c): token có mà TokenType trống phải là
                // TOKEN_TYPE_IS_REQUIRED như các API anh em — trước đây resolver
                // trả null và rơi về TOKEN_IS_EXPIRED, sai ngữ nghĩa.
                if (string.IsNullOrWhiteSpace(tokenType))
                    throw new TokenTypeRequiredException();
                Identity = _userIdentityResolver.GetIdentity(token, tokenType);
                if (Identity == null || string.IsNullOrWhiteSpace(Identity.Account)
                    || string.IsNullOrWhiteSpace(Identity.UserId))
                {
                    throw new TokenExpiredException();
                }
            }
            if (Identity == null && needAuthorize)
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new TokenRequiredException();
                }
                throw new TokenExpiredException();
            }
        }

        protected TResponseModel CommonExecute<TRequestModel, TResponseModel>(
            Action<TRequestModel, TResponseModel> action,
            TRequestModel request,
            string actionName)
            where TRequestModel : RequestBase
            where TResponseModel : ResponseBase, new()
        {
            var response = new TResponseModel();
            try
            {
                if (request == null)
                {
                    response.code = ResponseResultCode.FAIL;
                    response.sub_code = "SOMEARGUMENTS_IS_REQUIRED";
                }
                else if (request.Timestamp == null)
                {
                    response.code = ResponseResultCode.FAIL;
                    response.sub_code = "TIMESTAMP_IS_REQUIRED";
                }
                else
                {
                    // Authorize first so identity values can be mapped onto the
                    // request, exactly as the original orders these steps.
                    Authorize(request.Token, request.TokenType, RequiresAuthorize(actionName));
                    if (Identity != null)
                    {
                        MapIdentityToRequest(Identity, request);
                    }
                    request.Validate();
                    action(request, response);
                }
            }
            catch (TokenTypeRequiredException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_TYPE_IS_REQUIRED";
                response.sub_msg = "Loại token truy cập trống";
            }
            catch (TokenRequiredException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_IS_REQUIRED";
                response.sub_msg = "Token truy cập trống";
            }
            catch (TokenExpiredException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_IS_EXPIRED";
                response.sub_msg = "Token truy cập đã hết hạn";
            }
            catch (TokenTypeNotMatchException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_TYPE_NOT_MATCH";
                response.sub_msg = "Loại token truy cập không khớp";
            }
            catch (TokenIllegalException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_IS_ILLEGAL";
                response.sub_msg = "Không đủ quyền truy cập giao diện";
            }
            catch (ArgumentsRequiredException ex)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = string.Format("{0}_IS_REQUIRED",
                    !string.IsNullOrWhiteSpace(ex.Fields) ? ex.Fields.ToUpperInvariant() : "SOMEARGUMENTS");
            }
            catch (FailedException ex)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = (ex.Code ?? "").ToUpperInvariant();
                response.sub_msg = ex.Msg;
            }
            catch (Exception ex)
            {
                var inner = ex;
                while (inner.InnerException != null) inner = inner.InnerException;
                response.code = ResponseResultCode.ERROR;
                response.sub_code = "INTERNAL_SERVER_ERROR";
                response.sub_msg = SanitizeDbError(ex, inner);
            }
            return response;
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
        private static void MapIdentityToRequest(Identity identity, RequestBase request)
        {
            var requestProps = request.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite).ToDictionary(p => p.Name, p => p);
            foreach (var prop in identity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!requestProps.TryGetValue(prop.Name, out var target)) continue;
                var value = prop.GetValue(identity);
                if (value != null) target.SetValue(request, value);
            }
        }

        protected string GetObjectJsonStr(object obj)
        {
            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        }

        /// <summary>Same wire shape as the original CreateResponse(response).</summary>
        protected ContentResult CreateResponse(object obj)
        {
            return Content(GetObjectJsonStr(obj), "application/json");
        }

        /// <summary>JSONP-aware variant (original CreateResponse(request, response)).</summary>
        protected ContentResult CreateResponse<TRequestModel>(TRequestModel request, object response)
            where TRequestModel : JSONRequestBase
        {
            if (request != null && !string.IsNullOrWhiteSpace(request.jsonpCallback))
            {
                var json = GetObjectJsonStr(response);
                return Content(string.Format("{0}({1})", request.jsonpCallback, json), "application/json");
            }
            return CreateResponse(response);
        }
    }
}
