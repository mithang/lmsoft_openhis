using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtouch.HIS.API.Common;
using Newtouch.PDS.API.Services;
using Newtonsoft.Json;

namespace Newtouch.PDS.API.Envelope
{
    /// <summary>
    /// Tái tạo CommonExecute / CreateResponse / Authorize của
    /// FrameworkBase.API.ApiControllerBase theo bản mirror source trong chính cây PDS
    /// (Newtouch.PDS.API/App_Start/ApiControllerBase.cs).
    ///
    /// ĐIỂM THÍCH ỨNG VỚI BẢN SHIM ĐÃ PORT (không phải khác biệt tùy ý):
    /// - RequestBase shim của PDS.Infrastructure có Timestamp là DateTime KHÔNG nullable
    ///   (gốc là DateTime?). JSON thiếu Timestamp sẽ deserialize thành DateTime.MinValue,
    ///   nên điều kiện TIMESTAMP_IS_REQUIRED ở đây là Timestamp == default — tương đương
    ///   ngữ nghĩa "client không gửi Timestamp".
    /// - Shim không có Validate(): bản này tự kiểm tra các property [Required] bằng
    ///   reflection, đúng ngữ nghĩa Validate() gốc (CommonExecute map thành
    ///   {FIELD}_IS_REQUIRED). Các DTO PDS có [Required] thật (DeptCode, các trường
    ///   OutpatientBook...) nên bước này có tác dụng thật, không phải code chết.
    /// - Shim không có jsonpCallback và mọi controller gốc của PDS đều gọi overload
    ///   CreateResponse(response) một tham số (không dùng jsonp). Overload nhận request
    ///   ở đây vẫn được giữ cho đúng hình thức envelope anh em, nhưng nó chỉ bọc jsonp
    ///   khi request có property jsonpCallback — DTO PDS không có nên thực tế luôn trả
    ///   JSON trần, đúng hành vi gốc của PDS.
    /// - Không ghi monitor log qua AppLogger (host API không wiring NLog monitor);
    ///   log mặc định của ASP.NET Core vẫn ghi request/error.
    /// - sub_msg của lỗi hệ thống chỉ chứa Exception.Message, KHÔNG kèm StackTrace
    ///   (gốc kèm cả stacktrace — rò rỉ nội bộ ra ngoài API công khai).
    /// - Token được dùng thô làm key Redis (xem ghi chú TokenStore về TokenDecryptFilter).
    /// - Không có controller/action nào của PDS mang [DefaultAuthorize] (đã grep toàn bộ
    ///   cây API gốc) nên request không token vẫn qua Authorize khi có Timestamp — đúng
    ///   hành vi gốc, giống Query của OR.API/MRMS.API. Token khi được gửi thì vẫn bị kiểm
    ///   tra đầy đủ (type bắt buộc, hết hạn, type không khớp).
    /// </summary>
    public abstract class ApiControllerBase : ControllerBase
    {
        protected Identity Identity { get; private set; }

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
                else if (request.Timestamp == default(DateTime))
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
                    Validate(request);
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
            catch (NewtouchHIS.Lib.Base.Exceptions.FailedException ex)
            {
                // QA PHASE 2 (API-07b): app tầng Infrastructure ném FailedException
                // của Lib (ctor (code, msg), KHÔNG gọi base(message)) — trước đây
                // nó rơi vào catch Exception chung và sub_msg chỉ còn text mặc
                // định của .NET ("Exception of type '...FailedException' was
                // thrown."), code 20000. Nay map đúng: FAIL + Msg thật của nghiệp
                // vụ (vd. lý do hủy đặt trước), sub_code lấy Code nếu có.
                response.code = ResponseResultCode.FAIL;
                response.sub_code = !string.IsNullOrWhiteSpace(ex.Code)
                    ? ex.Code.ToUpper() : "FAILED";
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
        /// đầy đủ được ghi vào log server qua ILoggerFactory (đã wiring
        /// AddLibLogging ở Program.cs) khi resolve được.</summary>
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
                        // Log thất bại cũng không được làm hỏng response.
                    }
                    return "Không kết nối được cơ sở dữ liệu, vui lòng liên hệ quản trị viên";
                }
            }
            return inner.Message;
        }

        /// <summary>
        /// Ngữ nghĩa Validate() gốc: property nào mang [Required] mà trống thì ném
        /// ArgumentsRequiredException mang tên field đó.
        /// </summary>
        private static void Validate(RequestBase request)
        {
            foreach (var prop in request.GetType().GetProperties())
            {
                if (!Attribute.IsDefined(prop, typeof(RequiredAttribute))) continue;
                var val = prop.GetValue(request);
                if (val == null || (val is string s && string.IsNullOrWhiteSpace(s)))
                    throw new ArgumentsRequiredException(prop.Name);
            }
        }

        /// <summary>
        /// Map tối thiểu từ identity sang request (gốc dùng Ext.MapperTo):
        /// chỉ điền OrganizeId khi request đang trống.
        /// </summary>
        private static void MapIdentityToRequest(Identity identity, RequestBase request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) && !string.IsNullOrWhiteSpace(identity.OrganizeId))
                request.OrganizeId = identity.OrganizeId;
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

        protected ContentResult CreateResponse(ResponseBase response)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
            };
            return Content(JsonConvert.SerializeObject(response, settings), "application/json");
        }

        protected ContentResult CreateResponse(object request, ResponseBase response)
        {
            if (request != null)
            {
                var cbProp = request.GetType().GetProperty("jsonpCallback");
                var cb = cbProp?.GetValue(request) as string;
                if (!string.IsNullOrWhiteSpace(cb))
                {
                    var settings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        DateFormatString = "yyyy-MM-dd HH:mm:ss",
                    };
                    var json = JsonConvert.SerializeObject(response, settings);
                    return Content(string.Format("{0}({1})", cb, json), "application/json");
                }
            }
            return CreateResponse(response);
        }
    }
}
