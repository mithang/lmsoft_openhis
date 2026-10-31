using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;

namespace NewtouchHIS.Lib.Framework.Operator
{
    /// <summary>
    /// người dùng đăng nhập thông tin danh tính lớp thao tác
    /// </summary>
    public class OperatorProvider
    {
        /// <summary>
        /// ứng dụngId(trongconfigcấu hình AppId)
        /// </summary>
        public static string _appId { get; private set; }


        private static string _loginUserKeyPart1 = "newtouch_loginuserkey_2016";

        /// <summary>
        /// liên kếtkey,keycó thể đổi nhómModel
        /// </summary>
        private static string _loginUserKey = _loginUserKeyPart1;

        /// <summary>
        /// lắp ráp cục bộCookiecủa
        /// </summary>
        public static string LocalhostCookieKey
        {
            get
            {
                return _loginUserKey;
            }
        }

        /// <summary>
        /// trạng thái đăng nhập của duy trì thời gian
        /// </summary>
        private static int _LoginStatusKeepMinute = 30;

        /// <summary>
        /// bật‘vô hiệu hóa đăng nhập nhiều nơi’giới hạn của
        /// </summary>
        private static bool _enableMultipleLoginLimit = false;

        /// <summary>
        /// bật‘vô hiệu hóa đăng nhập nhiều nơi’giới hạn của - số lượng
        /// </summary>
        private static int _multipleLoginLimit_Count = 1;

        /// <summary>
        /// hàm khởi tạo tĩnh
        /// </summary>
        static OperatorProvider()
        {
            _appId = ConfigInitHelper.SysConfig.AppId ?? AppSettings.GetConfig("AppId");

            if (!string.IsNullOrWhiteSpace(_appId))
            {
                _loginUserKey = _appId + "_" + _loginUserKey;
            }

            //thời lượng trực tuyến
            _LoginStatusKeepMinute = ConfigInitHelper.SysConfig.LoginStatusKeepMinute ?? _LoginStatusKeepMinute;

            //bật giới hạn đăng nhập
            _enableMultipleLoginLimit = ConfigInitHelper.SysConfig.EnableMultipleLoginLimit ?? _enableMultipleLoginLimit;

            //
            _multipleLoginLimit_Count = ConfigInitHelper.SysConfig.MultipleLoginLimit_Count ?? _multipleLoginLimit_Count;
        }

        /// <summary>
        /// lấy danh tính đăng nhập hiện tại
        /// </summary>
        /// <returns></returns>
        public static OperatorModel? GetCurrent(bool withInnerCatch = false)
        {
            string? exception = string.Empty;
            return GetCurrent(ref exception, withInnerCatch);
        }

        /// <summary>
        /// lấy danh tính đăng nhập hiện tại(loại ngoại lệ:SIDELINED-đã bị đẩy ra)
        /// </summary>
        /// <param name="exception">loại ngoại lệ:SIDELINED-đã bị đẩy ra</param>
        /// <param name="withInnerCatch">có không ném ngoại lệ ra ngoài hay không</param>
        /// <returns></returns>
        public static OperatorModel? GetCurrent(ref string? exception, bool withInnerCatch = false)
        {
            if (withInnerCatch)
            {
                try
                {
                    return GetCurrent(ref exception);
                }
                catch
                {
                    exception = "INNER_EXCEPTION";
                    return null;
                }
            }
            else
            {
                return GetCurrent(ref exception);
            }
        }

        /// <summary>
        /// lấy danh tính đăng nhập hiện tại
        /// </summary>
        /// <param name="exception">loại ngoại lệ:SIDELINED-đã bị đẩy ra</param>
        /// <returns></returns>
        private static OperatorModel? GetCurrent(ref string? exception)
        {

            HttpContext httpContext = HttpWebHelper.HttpCurrent;
            if (httpContext == null)
            {
                return null;
            }
            if (httpContext != null && httpContext.Items["OPERATOR_GETCURRENT_EXCEPTION"] != null)
            {
                exception = httpContext.Items["OPERATOR_GETCURRENT_EXCEPTION"].ToString();
                return null;
            }

            string? token = string.Empty;
            if (httpContext != null)
            {
                token = HttpWebHelper.GetCookie(_loginUserKey);
            }
            else
            {
                //khôngBS
                throw new NotImplementedException();
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            //lấy kết quả sau khi mã hóa
            string? encryptedResult = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                encryptedResult = RedisHelper.GetString(token);
            }

            if (string.IsNullOrWhiteSpace(encryptedResult))
            {
                HttpWebHelper.RemoveCookie(_loginUserKey);
                return null;
            }

            OperatorModel? operatorModel = null;

            if (_enableMultipleLoginLimit)
            {
                //đã vô hiệu hóa đăng nhập nhiều nơi
                if (encryptedResult == "SIDELINED")
                {
                    //đã bị đẩy ra
                    exception = "SIDELINED";
                    if (httpContext != null)
                    {
                        httpContext.Items["OPERATOR_GETCURRENT_EXCEPTION"] = exception;
                    }
                    RedisHelper.Remove(token);  //mã thông báo đã không còn hiệu lực
                    return null;
                }
            }

            //nếu lấy được,giải tuần tự hóa thành đối tượng
            operatorModel = JsonConvert.DeserializeObject<OperatorModel>(encryptedResult); // encryptedResult.ToObject<OperatorModel>();

            if (operatorModel != null &&
                (!string.IsNullOrWhiteSpace(operatorModel.UserId) || !string.IsNullOrWhiteSpace(operatorModel.UserCode))
                )
            {
                //cập nhật thời gian hết hạn
                RedisHelper.KeyExpire(token, _LoginStatusKeepMinute);

                return operatorModel;
            }
            else
            {
                return null;    //được coi là không hợp lệ danh tính
            }
        }

        /// <summary>
        /// lấy danh tính đăng nhập hiện tại
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static OperatorModel? GetCurrent(HttpContext context)
        {
            string? token;
            if (context != null)
            {
                var v = context.Request.Cookies[_loginUserKey];
                token = v != null ? v : "";
            }
            else
            {
                //khôngBS
                throw new NotImplementedException();
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            //lấy kết quả sau khi mã hóa
            string? encryptedResult = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                encryptedResult = RedisHelper.GetString(token);
            }

            if (string.IsNullOrWhiteSpace(encryptedResult))
            {
                return null;
            }

            //nếu lấy được,giải tuần tự hóa thành đối tượng
            var operatorModel = JsonConvert.DeserializeObject<OperatorModel>(encryptedResult);// encryptedResult.ToObject<OperatorModel>();

            if (operatorModel == null || string.IsNullOrWhiteSpace(operatorModel.UserId) &&
                string.IsNullOrWhiteSpace(operatorModel.UserCode)) return null; //được coi là không hợp lệ danh tính
            //cập nhật thời gian hết hạn
            RedisHelper.KeyExpire(token, _LoginStatusKeepMinute);

            return operatorModel;

        }

        /// <summary>
        /// ghi đè trạng thái đăng nhập
        /// </summary>
        /// <param name="operatorModel"></param>
        public static void AddCurrent(OperatorModel operatorModel)
        {
            HttpContext httpContext = HttpWebHelper.HttpCurrent;
            if (operatorModel == null ||
                (string.IsNullOrWhiteSpace(operatorModel.UserId) && string.IsNullOrWhiteSpace(operatorModel.UserCode))
                )
            {
                return;     //được coi là không hợp lệ danh tính
            }

            //tokentương ứng trực tiếpUserInfo Model
            var token = Guid.NewGuid().ToString();

            if (string.IsNullOrWhiteSpace(operatorModel.token))
            {
                operatorModel.token = token;
            }

            if (_enableMultipleLoginLimit)
            {
                //đã vô hiệu hóa đăng nhập nhiều nơi

                //ứng dụng+UserId+tài khoản đăng nhập duy nhất người dùng đó của hệ thống,những cái đã đăng nhậpTokenList
                var keyUserTokenList = string.Format("list:Token_WithUser_{0}_{1}_{2}"
                    , _appId, operatorModel.UserId, operatorModel.UserCode);
                var existTokenList = RedisHelper.GetList<string>(keyUserTokenList)
                    ?? new List<string>();
                var limitCount = _multipleLoginLimit_Count;  //cùng một người dùng số lượng máy khách trực tuyến có hiệu lực đồng thời tối đa
                if (existTokenList != null && existTokenList.Count >= limitCount) //có thể kiểm soát số lượng trạng thái đăng nhập đồng thời
                {
                    var availablyOnlineCount = 0;    //cùng một người dùng máy khách đăng nhập trực tuyến có hiệu lực đã thống kê được
                    existTokenList.Reverse();   //đảo ngược trước đã,khiến người đăng nhập sau lên phía trước
                    //đẩy ra những cái thừa
                    for (var i = 0; i < existTokenList.Count; i++)
                    {
                        var itemToken = existTokenList[i];
                        var itemEncryptedResult = RedisHelper.GetString(itemToken);
                        if (string.IsNullOrWhiteSpace(itemEncryptedResult))
                        {
                            existTokenList.RemoveAt(i); //đã thực hiện rồi thao tác thoát hoặc đã hết hạn rồi
                            i--;    //lần tới vàoforvẫn phải lài
                            continue;
                        }

                        if (itemToken != "SIDELINED")
                        {
                            if ((availablyOnlineCount + 1) < limitCount)    //+1là vì suất cuối cùng hiện tạitoken
                            {
                                //đây là một máy khách trực tuyến có hiệu lực
                                availablyOnlineCount++;
                                continue;   //chưa đạt số lượng máy khách trực tuyến đồng thời giới hạn của
                            }
                            else
                            {
                                //đã đạt số lượng có hiệu lực tối đa
                                //xử lý việc đẩy ra

                                //cập nhật thành đã bị đẩy ra
                                RedisHelper.SetString(itemToken, "SIDELINED", new TimeSpan(0, 60, 0));
                                continue;
                            }
                        }
                    }
                    if (existTokenList.Count > limitCount + 20)
                    {
                        //sau khi bị đẩy ra không cóGetCurrent,thao tác thoát. dẫn đếnexistTokenListkhông thểRemoveAt
                        //existTokenList.RemoveRange(5, limitCount + 20 - 5);  //xóa những cái phía trước(lần đăng nhập mới nhất bịreverseđã lên phía trước)
                        existTokenList.RemoveRange(limitCount, 20 - 1);  //xóa những cái phía trước(lần đăng nhập mới nhất bịreverseđã lên phía trước)
                    }
                }
                if (existTokenList != null && existTokenList.Count > 0)
                {
                    existTokenList.Reverse();   //đảo ngược lại,khiến người đăng nhập sau quay lạilistphía sau
                    existTokenList.Add(token);
                    RedisHelper.SetList<string>(keyUserTokenList, existTokenList);
                }

                if (httpContext != null)
                {
                    HttpWebHelper.WriteCookie(_loginUserKey, token);
                }
                else
                {
                    //khôngBS
                    throw new NotImplementedException();
                }

                RedisHelper.SetString(token, operatorModel.ToJson(), new TimeSpan(0, _LoginStatusKeepMinute, 0));
            }
            else
            {
                //có thể đăng nhập nhiều nơi

                if (httpContext != null)
                {
                    HttpWebHelper.WriteCookie(_loginUserKey, token);
                    RedisHelper.SetString(token, operatorModel.ToJson(), new TimeSpan(0, _LoginStatusKeepMinute, 0));
                }
                else
                {
                    //khôngBS
                    throw new NotImplementedException();
                }

            }
        }

        /// <summary>
        /// xóa trạng thái đăng nhập
        /// </summary>
        public static void RemoveCurrent()
        {
            HttpContext httpContext = HttpWebHelper.HttpCurrent;
            string? token = null;
            if (httpContext != null)
            {
                token = HttpWebHelper.GetCookie(_loginUserKey);
            }
            else
            {
                //khôngBS
                throw new NotImplementedException();
            }

            if (!string.IsNullOrWhiteSpace(token))
            {
                HttpWebHelper.RemoveCookie(_loginUserKey);
                RedisHelper.Remove(token);
            }
        }

        /// <summary>
        /// lấy ... của mã thông báo danh tính hiện tạiKey
        /// </summary>
        /// <returns></returns>
        public static string? GetCurrentToken()
        {
            return HttpWebHelper.GetCookie(_loginUserKey);
        }

        /// <summary>
        /// bổ sung đăng nhậpcookie áp dụng cho việc chuyển trang giữa các hệ thống con
        /// </summary>
        public static void WriteLoginUserCookie()
        {
            HttpContext httpContext = HttpWebHelper.HttpCurrent;
            var former = httpContext.Request.Query.FirstOrDefault(p => p.Key == "appId").Value;
            if (string.IsNullOrWhiteSpace(former)) return;
            former = former + "_" + _loginUserKeyPart1;
            var cookie = HttpWebHelper.GetCookie(former);
            if (string.IsNullOrWhiteSpace(cookie)) return;
            var json = RedisHelper.GetString(cookie);
            if (string.IsNullOrWhiteSpace(json)) return;
            var operatorModel = json.ToObject<OperatorModel>();
            if (operatorModel == null || string.IsNullOrWhiteSpace(operatorModel.UserId) && string.IsNullOrWhiteSpace(operatorModel.UserCode)) return;

            var redisKey = Guid.NewGuid().ToString();
            HttpWebHelper.WriteCookie(_loginUserKey, redisKey);
            RedisHelper.SetString(redisKey, json, new TimeSpan(0, _LoginStatusKeepMinute, 0));

        }
    }

}
