using Newtonsoft.Json;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Redis;
using Newtouch.Tools;
using Newtouch.Tools.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Infrastructure
{
    public class AuthenManageHelper
    {
        public readonly static string SiteTokenAPIHost = ConfigurationManager.AppSettings["SiteTokenAPIHost"];//Trạm token
        public readonly static string SiteConmonAPIHost = ConfigurationManager.AppSettings["SiteConmonAPIHost"]; //Trạm trung tâm api
        public readonly static string appId = ConfigurationManager.AppSettings["AppId"];//appid truy cập trạm trung tâm api
        public readonly static string accessAppId = ConfigurationManager.AppSettings["Token_ApiManage"];//appid truy cập trạm trung tâm api
        //Token đệm khi người dùng đăng nhập
        public static string userCacheToken = "";

        /// <summary>
        /// Lấy api token của giao diện trung tâm
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
                    //AppId = appId,
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
                    //Xác thực quyền thất bại, làm mới token
                    token = GetUserTokenRefesh(user, token);
                    //Yêu cầu lại giao diện
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
        /// Sau khi người dùng đăng nhập, lấy token chung của api
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
        ///// <summary>
        ///// Yêu cầu giao diện
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="uri"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public static string HttpPost(string data, string uri, string token)
        //{
        //    //Trước tiên, dựa vào uri yêu cầu của người dùng để tạo địa chỉ yêu cầu
        //    string serviceUrl = string.Format(uri);
        //    //Tạo đối tượng truy cập Web
        //    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
        //    //Chuyển dữ liệu người dùng truyền đến thành luồng byte “UTF-8”
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
        //    //Gửi yêu cầu
        //    Stream stream = myRequest.GetRequestStream();
        //    stream.Write(buf, 0, buf.Length);
        //    stream.Close();

        //    //Lấy giá trị trả về của giao diện
        //    //Lấy nội dung phản hồi qua đối tượng truy cập Web
        //    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
        //    //Tạo đối tượng StreamReader qua luồng nội dung phản hồi, vì StreamReader cao cấp hơn và nhanh hơn
        //    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
        //    //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//Nếu có vấn đề về mã hóa thì dùng phương pháp này
        // string result = reader.ReadToEnd();//Dùng StreamReader là có thể đọc nội dung phản hồi từ đầu đến cuối
        //    reader.Close();
        //    myResponse.Close();
        //    return result;

        //}
    }
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

}
