using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtouch.HIS.Base.HOSP.API.Services;
using Newtonsoft.Json;

namespace Newtouch.HIS.Base.HOSP.API.Envelope
{
    /// <summary>
    /// Tái tạo CommonExecute / CreateResponse / Authorize / FilterObjectProperty /
    /// SaveIdentity của FrameworkBase.API.ApiControllerBase theo bản mirror source
    /// bên PDS. Điểm khác biệt có chủ đích so với gốc (ghi rõ, không giấu):
    /// - Không ghi monitor log qua AppLogger (host API không wiring NLog monitor).
    /// - sub_msg của lỗi hệ thống chỉ chứa Exception.Message, KHÔNG kèm StackTrace
    ///   (gốc kèm cả stacktrace — rò rỉ nội bộ ra ngoài API công khai).
    /// - Token được dùng thô làm key Redis (thuật toán giải mã DES của TokenDecrypt
    ///   nằm trong DLL API.Common, không có source). Client gửi token đã mã hóa DES
    ///   sẽ nhận TOKEN_IS_EXPIRED — giới hạn đã biết, không tự bịa thuật toán.
    /// </summary>
    public abstract class ApiControllerBase : ControllerBase
    {
        protected Identity Identity { get; private set; }

        /// <summary>Identity của request hiện tại (dạng UserIdentity gốc).</summary>
        protected Identity UserIdentity => Identity;

        private readonly TokenStore _tokenStore;

        protected ApiControllerBase(TokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        protected TResponse CommonExecute<TRequest, TResponse>(Action<TRequest, TResponse> ac, TRequest request)
            where TRequest : RequestBase
            where TResponse : ResponseBase, new()
        {
            var response = new TResponse();
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
                    Authorize(request.Token, request.TokenType);
                    if (Identity != null)
                    {
                        MapIdentityToRequest(Identity, request);
                    }
                    request.Validate();
                    ac(request, response);
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
                response.sub_msg = "Token truy cập đã hết hạn hoặc không hợp lệ";
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
                response.sub_msg = "Không đủ quyền truy cập API";
            }
            catch (ArgumentsRequiredException ex)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = string.Format("{0}_IS_REQUIRED",
                    !string.IsNullOrWhiteSpace(ex.Fields) ? ex.Fields.ToUpper() : "SOMEARGUMENTS");
            }
            catch (FailedException ex)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = (ex.Code ?? "").ToUpper();
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
            var prop = request.GetType().GetProperty("OrganizeId");
            if (prop != null && prop.CanWrite)
            {
                var cur = prop.GetValue(request) as string;
                if (string.IsNullOrWhiteSpace(cur) && !string.IsNullOrWhiteSpace(identity.OrganizeId))
                    prop.SetValue(request, identity.OrganizeId);
            }
        }

        protected void Authorize(string token, string tokenType)
        {
            Identity = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                if (string.IsNullOrWhiteSpace(tokenType))
                    throw new TokenTypeRequiredException();
                var resolved = _tokenStore.GetIdentity(token, tokenType);
                if (resolved != null
                    && !string.IsNullOrWhiteSpace(resolved.Account)
                    && !string.IsNullOrWhiteSpace(resolved.UserId ?? resolved.UserCode))
                {
                    if (!string.IsNullOrWhiteSpace(resolved.TokenType)
                        && !string.IsNullOrWhiteSpace(tokenType)
                        && !string.Equals(resolved.TokenType, tokenType, StringComparison.OrdinalIgnoreCase))
                        throw new TokenTypeNotMatchException();
                    Identity = resolved;
                }
                else
                {
                    throw new TokenExpiredException();
                }
            }
            if (Identity == null && NeedsAuthorize())
            {
                if (string.IsNullOrWhiteSpace(token))
                    throw new TokenRequiredException();
                throw new TokenExpiredException();
            }
        }

        private bool NeedsAuthorize()
        {
            var controllerType = GetType();
            if (controllerType.GetCustomAttribute<DefaultAuthorizeAttribute>() != null)
                return true;
            var action = ControllerContext?.ActionDescriptor?.ActionName;
            if (!string.IsNullOrWhiteSpace(action))
            {
                foreach (var m in controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (string.Equals(m.Name, action, StringComparison.OrdinalIgnoreCase)
                        && m.GetCustomAttribute<DefaultAuthorizeAttribute>() != null)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Lưu identity xuống Redis, trả về token (key). Gốc: "có thời hạn nhất định"
        /// nhưng không ghi rõ số phút trong source mở; bản port dùng 120 phút.
        /// </summary>
        protected string SaveIdentity(Identity identity)
        {
            var token = Guid.NewGuid().ToString("N");
            _tokenStore.SaveIdentity(token, identity, TimeSpan.FromMinutes(120));
            return token;
        }

        /// <summary>
        /// Lọc thuộc tính của danh sách entity theo ResponseColumns của request,
        /// cộng/trừ includeProps/excludeProps (đúng thứ tự gốc: include rồi trừ exclude,
        /// rồi giao với ResponseColumns nếu request có chỉ định).
        /// </summary>
        protected object FilterObjectProperty<T>(IEnumerable<T> list, string[] responseColumns,
            string[] includeProps = null, string[] excludeProps = null)
        {
            var result = new List<Dictionary<string, object>>();
            if (list == null) return result;
            var allProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead).ToArray();
            foreach (var item in list)
            {
                var dict = new Dictionary<string, object>();
                IEnumerable<PropertyInfo> props = includeProps != null && includeProps.Length > 0
                    ? allProps.Where(p => includeProps.Contains(p.Name))
                    : allProps;
                if (excludeProps != null && excludeProps.Length > 0)
                    props = props.Where(p => !excludeProps.Contains(p.Name));
                if (responseColumns != null && responseColumns.Length > 0)
                    props = props.Where(p => responseColumns.Contains(p.Name));
                foreach (var p in props)
                {
                    try { dict[p.Name] = p.GetValue(item); } catch { dict[p.Name] = null; }
                }
                result.Add(dict);
            }
            return result;
        }

        protected ContentResult CreateResponse(ResponseBase response)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
            };
            return Content(JsonConvert.SerializeObject(response, settings), "application/json");
        }

        protected ContentResult CreateResponse(JSONRequestBase request, ResponseBase response)
        {
            if (request != null && !string.IsNullOrWhiteSpace(request.jsonpCallback))
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                };
                var json = JsonConvert.SerializeObject(response, settings);
                return Content(string.Format("{0}({1})", request.jsonpCallback, json), "application/json");
            }
            return CreateResponse(response);
        }
    }
}
