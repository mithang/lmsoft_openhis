using Newtonsoft.Json;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Redis;
using Newtouch.Tools;
using Newtouch.Tools.Net;
using System;
using System.Configuration;
using System.Net;

namespace Newtouch.MRQC.Infrastructure
{
    public class AuthenManageHelper
    {
        public readonly static string SiteTokenAPIHost = ConfigurationManager.AppSettings["SiteAuthCenterHost"];//token trang
        public readonly static string SiteConmonAPIHost = ConfigurationManager.AppSettings["SiteApiManageHost"]; //api trang trung tâm
        public readonly static string appId = ConfigurationManager.AppSettings["AppId"];//api truy cập trang trung tâm appid
        public readonly static string accessAppId = ConfigurationManager.AppSettings["Token_ApiManage"];//Apimanage AppId
        //bộ nhớ đệm đăng nhập của người dùng token
        public static string userCacheToken = "";

        /// <summary>
        /// lấy giao diện trung tâmapi token
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            string tokenstr = RedisHelper.StringGet("AuthenGetTokenKey");
            string tokenstrs = "";
            if (!string.IsNullOrWhiteSpace(tokenstr))
            {
                tokenstrs = tokenstr;
            }
            else
            {
                var reqObj = new
                {
                    AppId = appId,
                    Data = accessAppId
                };
                tokenstrs = HttpMethods.HttpPost(SiteTokenAPIHost + "api/Auth/GetAppToken", reqObj.ToJson(), null);
                AuthenTokenHelper Token = JsonConvert.DeserializeObject<AuthenTokenHelper>(tokenstrs);
                Tokendata data = Token.BusData.data.ToString().ToObject<Tokendata>();
                RedisHelper.StringSet("AuthenGetTokenKey", data.Token, new TimeSpan(0, 20, 0));
                tokenstrs = data.Token;
            }
            return tokenstrs;
        }

        public static string HttpPost(string data, string uri, string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                token = $"Bearer {token}";
            }
            return HttpMethods.HttpPostWithToken(uri, data, token);
        }

        /// <summary>
        /// Post yêu cầu
        /// xác thực thất bại thì tự động làm mới
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="uri"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static BusData<T> HttpPost<T>(string data, string uri, OperatorModel user)
        {
            string token = "";
            try
            {
                token = GetUserToken(user);
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new FailedException("[鉴权]未获取到 Api 身份令牌，请联系管理员");
                }
                token = $"Bearer {token}";
                string response = HttpMethods.HttpPostWithToken(uri, data, token);
                ApiManageResponse<T> apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                if (apiResp.HttpStatus == (int)HttpStatusCode.Unauthorized)
                {
                    //xác thực thất bại làm mới token
                    token = GetUserTokenRefesh(user, token);
                    //yêu cầu lại giao diện
                    response = HttpMethods.HttpPostWithToken(uri, data, token);
                    apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                }
                if (apiResp.HttpStatus != (int)HttpStatusCode.OK)
                {
                    throw new FailedException($"[Http]请求异常：{apiResp.HttpStatus}=>{apiResp.Message}");
                }
                return apiResp.BusData;
            }
            catch (Exception ex)
            {
                throw new FailedException($"[鉴权]获取到 Api 身份令牌异常：{ex.Message}");
            }
        }
        /// <summary>
        /// token hết hạn buộc làm mới
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserTokenRefesh(OperatorModel user, string token)
        {
            var key = GetUserCacheKey(user.OrganizeId, user.UserCode);
            var reqObj = new
            {
                AppId = appId,
                OrganizeId = user.OrganizeId,
                Data = new
                {
                    TopOrganizeId = user.TopOrganizeId,
                    UserCode = user.UserCode,
                    UserName = user.UserName,
                    Account = user.UserCode,
                    Timestamp = DateTime.Now,
                    KeyLicense = accessAppId
                }
            };
            string tokenstrs = HttpMethods.HttpPostWithToken(SiteTokenAPIHost + "api/Auth/UserTokenRefresh", reqObj.ToJson(), token);
            var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
            RedisHelper.StringSet(key, Token.BusData.data.Token, new TimeSpan(0, 20, 0));
            return Token.BusData.data.Token;
        }
        /// <summary>
        /// lấy sau khi người dùng đăng nhậpapichung token
        /// </summary>
        /// <returns></returns>
        public static string GetUserToken(OperatorModel user)
        {
            var key = GetUserCacheKey(user.OrganizeId, user.UserCode);
            string tokenstr = RedisHelper.StringGet(key);
            string tokenstrs = "";
            if (!string.IsNullOrWhiteSpace(tokenstr))
            {
                tokenstrs = tokenstr;
            }
            else
            {
                var reqObj = new
                {
                    AppId = appId,
                    OrganizeId = user.OrganizeId,
                    Data = new
                    {
                        TopOrganizeId = user.TopOrganizeId,
                        UserCode = user.UserCode,
                        UserName = user.UserName,
                        Account = user.UserCode,
                        Timestamp = DateTime.Now,
                        KeyLicense = accessAppId
                    }
                };
                tokenstrs = HttpMethods.HttpPost(SiteTokenAPIHost + "api/Auth/GetUserToken", reqObj.ToJson(), null);
                var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
                RedisHelper.StringSet(key, Token.BusData.data.Token, new TimeSpan(0, 20, 0));
                tokenstrs = Token.BusData.data.Token;
            }
            return tokenstrs;
        }

        public static string GetUserCacheKey(string orgId, string user = null)
        {
            if (string.IsNullOrWhiteSpace(accessAppId))
            {
                throw new FailedException("[鉴权]未读取到接口AppId配置（Token_ApiManage）信息");
            }
            if (string.IsNullOrWhiteSpace(userCacheToken))
            {
                userCacheToken = string.Format(CacheKey.AccessToken, appId, orgId, accessAppId) + $"_{user}";
            }
            return userCacheToken;
        }

        #region Tạm thời chưa bật
        ///// <summary>
        ///// yêu cầu giao diện
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="uri"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public static string HttpPost(string data, string uri, string token)
        //{
        //    //trước tiên căn cứ vào yêu cầu của người dùngurixây dựng địa chỉ yêu cầu
        //    string serviceUrl = string.Format(uri);
        //    //tạo Web đối tượng truy cập
        //    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
        //    //chuyển dữ liệu người dùng truyền tới thành“UTF-8”luồng byte của
        //    byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);

        //    myRequest.Method = "POST";
        //    myRequest.ContentLength = buf.Length;
        //    myRequest.ContentType = "application/json";
        //    myRequest.MaximumAutomaticRedirections = 1;
        //    myRequest.AllowAutoRedirect = true;
        //    if (!string.IsNullOrWhiteSpace(token))
        //    {
        //        myRequest.Headers.Add("Authorization", "Bearer " + token);
        //    }
        //    //gửi yêu cầu
        //    Stream stream = myRequest.GetRequestStream();
        //    stream.Write(buf, 0, buf.Length);
        //    stream.Close();

        //    //lấy giá trị trả về của giao diện
        //    //thông qua Web đối tượng truy cập lấy nội dung phản hồi
        //    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
        //    //tạo qua luồng nội dung phản hồiStreamReaderđối tượng，vìStreamReadercao cấp hơn và nhanh hơn
        //    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
        //    //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//nếu có vấn đề về mã hóa thì dùng phương thức này
        //    string result = reader.ReadToEnd();//tận dụngStreamReaderlà có thể đọc nội dung phản hồi từ đầu đến cuối
        //    reader.Close();
        //    myResponse.Close();
        //    return result;

        //}

        #endregion
    }

    #region ResponseDTO
    public class ApiManageResponse<T>
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData<T> BusData { get; set; }
    }
    public class BusData<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }


    public class AuthenTokenHelper
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData BusData { get; set; }

    }
    public class BusData
    {
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
    public class Tokendata
    {
        public string Token { get; set; }
    }

    #endregion

}
