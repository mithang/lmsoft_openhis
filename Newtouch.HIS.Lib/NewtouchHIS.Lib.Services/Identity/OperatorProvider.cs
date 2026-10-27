using Microsoft.AspNetCore.Http;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Lib.Services.Identity
{
    /// <summary>
    /// Lớp thao tác thông tin danh tính người dùng đăng nhập
    /// </summary>
    public class OperatorProvider
    {
        /// <summary>
        /// Id ứng dụng (cấu hình AppId trong config)
        /// </summary>
        public static string _appId { get; private set; }


        private static string _loginUserKeyPart1 = "newtouch_loginuserkey_2016";

        /// <summary>
        /// Liên kết key, key có thể đổi nhómModel
        /// </summary>
        private static string _loginUserKey = _loginUserKeyPart1;

        /// <summary>
        /// Lắp ráp Cookie cục bộ
        /// </summary>
        public static string LocalhostCookieKey
        {
            get
            {
                return _loginUserKey;
            }
        }

        /// <summary>
        /// Thời gian duy trì trạng thái đăng nhập
        /// </summary>
        private static int _LoginStatusKeepMinute = 30;

        /// <summary>
        /// Bật‘Vô hiệu hóa đăng nhập nhiều nơi’của giới hạn
        /// </summary>
        private static bool _enableMultipleLoginLimit = false;

        /// <summary>
        /// Bật‘Vô hiệu hóa đăng nhập nhiều nơi’của giới hạn - số lượng
        /// </summary>
        private static int _multipleLoginLimit_Count = 1;

        /// <summary>
        /// Hàm khởi tạo tĩnh
        /// </summary>
        static OperatorProvider()
        {
            _appId = ConfigurationHelper.GetAppConfigValue("AppId");

            if (!string.IsNullOrWhiteSpace(_appId))
            {
                _loginUserKey = _appId + "_" + _loginUserKey;
            }

            //
            _LoginStatusKeepMinute = ConfigurationHelper.GetAppConfigIntValue("LoginStatusKeepMinute", defaultValue: _LoginStatusKeepMinute);

            //
            _enableMultipleLoginLimit = ConfigurationHelper.GetAppConfigBoolValue("EnableMultipleLoginLimit", defaultValue: _enableMultipleLoginLimit) ?? false;

            //
            _multipleLoginLimit_Count = ConfigurationHelper.GetAppConfigIntValue("MultipleLoginLimit_Count", defaultValue: _multipleLoginLimit_Count);
        }

        /// <summary>
        /// Lấy danh tính đăng nhập hiện tại
        /// </summary>
        /// <returns></returns>
        public static OperatorModel GetCurrent(bool withInnerCatch = false)
        {
            string exception = null;
            return GetCurrent(ref exception, withInnerCatch);
        }

        /// <summary>
        /// Lấy danh tính đăng nhập hiện tại (loại ngoại lệ: SIDELINED - đã bị loại ra)
        /// </summary>
        /// <param name="exception">Loại ngoại lệ: SIDELINED - đã bị loại ra</param>
        /// <param name="withInnerCatch">Có không ném ngoại lệ ra ngoài không</param>
        /// <returns></returns>
        public static OperatorModel GetCurrent(ref string exception, bool withInnerCatch = false)
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
        /// Lấy danh tính đăng nhập hiện tại
        /// </summary>
        /// <param name="exception">Loại ngoại lệ: SIDELINED - đã bị loại ra</param>
        /// <returns></returns>
        private static OperatorModel GetCurrent(ref string exception)
        {
            if (HttpContext.Current != null && HttpContext.Current.Items["OPERATOR_GETCURRENT_EXCEPTION"] != null)
            {
                exception = HttpContext.Current.Items["OPERATOR_GETCURRENT_EXCEPTION"].ToString();
                return null;
            }

            string token = null;
            if (HttpContext.Current != null)
            {
                token = WebHelper.GetCookie(_loginUserKey);
            }
            else
            {
                //Không phải BS
                throw new NotImplementedException();
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            //Lấy kết quả sau khi mã hóa
            string encryptedResult = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                encryptedResult = RedisHelper.StringGet(token);
            }

            if (string.IsNullOrWhiteSpace(encryptedResult))
            {
                return null;
            }

            OperatorModel operatorModel = null;

            if (_enableMultipleLoginLimit)
            {
                //Đã vô hiệu hóa đăng nhập nhiều nơi
                if (encryptedResult == "SIDELINED")
                {
                    //Đã bị loại ra
                    exception = "SIDELINED";
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.Items["OPERATOR_GETCURRENT_EXCEPTION"] = exception;
                    }
                    RedisHelper.Remove(token);  //Mã thông báo đã không còn hiệu lực
                    return null;
                }
            }

            //Nếu lấy được, giải tuần tự hóa thành đối tượng
            operatorModel = encryptedResult.ToObject<OperatorModel>();

            if (operatorModel != null &&
                (!string.IsNullOrWhiteSpace(operatorModel.UserId) || !string.IsNullOrWhiteSpace(operatorModel.UserCode))
                )
            {
                //Cập nhật thời gian hết hạn
                RedisHelper.KeyExpire(token, new TimeSpan(0, _LoginStatusKeepMinute, 0));

                return operatorModel;
            }
            else
            {
                return null;    //được coi là danh tính không hợp lệ
            }
        }

        /// <summary>
        /// Lấy danh tính đăng nhập hiện tại
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static OperatorModel GetCurrent(HttpContext context)
        {
            string token;
            if (context != null)
            {
                var v = context.Request.Cookies[_loginUserKey];
                token = v != null ? v.Value : "";
            }
            else
            {
                //Không phải BS
                throw new NotImplementedException();
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            //Lấy kết quả sau khi mã hóa
            string encryptedResult = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                encryptedResult = RedisHelper.StringGet(token);
            }

            if (string.IsNullOrWhiteSpace(encryptedResult))
            {
                return null;
            }

            //Nếu lấy được, giải tuần tự hóa thành đối tượng
            var operatorModel = encryptedResult.ToObject<OperatorModel>();

            if (operatorModel == null || string.IsNullOrWhiteSpace(operatorModel.UserId) &&
                string.IsNullOrWhiteSpace(operatorModel.UserCode)) return null; //được coi là danh tính không hợp lệ
            //Cập nhật thời gian hết hạn
            RedisHelper.KeyExpire(token, new TimeSpan(0, _LoginStatusKeepMinute, 0));

            return operatorModel;

        }

        /// <summary>
        /// Ghi đè trạng thái đăng nhập
        /// </summary>
        /// <param name="operatorModel"></param>
        public static void AddCurrent(OperatorModel operatorModel)
        {
            if (operatorModel == null ||
                (string.IsNullOrWhiteSpace(operatorModel.UserId) && string.IsNullOrWhiteSpace(operatorModel.UserCode))
                )
            {
                return;     //được coi là danh tính không hợp lệ
            }

            //tokenTương ứng trực tiếpUserInfo Model
            var token = Guid.NewGuid().ToString();

            if (string.IsNullOrWhiteSpace(operatorModel.token))
            {
                operatorModel.token = token;
            }

            if (_enableMultipleLoginLimit)
            {
                //Đã vô hiệu hóa đăng nhập nhiều nơi

                //ứng dụng+UserId+Tài khoản đăng nhập duy nhất của người dùng này trong hệ thống, đã đăng nhậpTokenList
                var keyUserTokenList = string.Format("list:Token_WithUser_{0}_{1}_{2}"
                    , _appId, operatorModel.UserId, operatorModel.UserCode);
                var existTokenList = RedisHelper.Get<List<string>>(keyUserTokenList)
                    ?? new List<string>();
                var limitCount = _multipleLoginLimit_Count;  //Số lượng máy khách trực tuyến có hiệu lực đồng thời tối đa của cùng một người dùng
                if (existTokenList != null && existTokenList.Count >= limitCount) //Có thể kiểm soát số lượng trạng thái đăng nhập đồng thời
                {
                    var availablyOnlineCount = 0;    //Máy khách đăng nhập trực tuyến có hiệu lực đã thống kê được của cùng một người dùng
                    existTokenList.Reverse();   //Đảo ngược trước, để mục đăng nhập sau lên phía trước
                    //Loại bỏ phần thừa
                    for (var i = 0; i < existTokenList.Count; i++)
                    {
                        var itemToken = existTokenList[i];
                        var itemEncryptedResult = RedisHelper.StringGet(itemToken);
                        if (string.IsNullOrWhiteSpace(itemEncryptedResult))
                        {
                            existTokenList.RemoveAt(i); //Đã thực hiện thao tác thoát hoặc đã hết hạn
                            i--;    //Lần vào vòng for tiếp theo vẫn phải lài
                            continue;
                        }

                        if (itemToken != "SIDELINED")
                        {
                            //availablyOnlineCount++;
                            if ((availablyOnlineCount + 1) < limitCount)    //+1là vì suất cuối cùng hiện tạitoken
                            {
                                //Đây là một máy khách trực tuyến có hiệu lực
                                availablyOnlineCount++;
                                continue;   //Chưa đạt đến giới hạn số lượng máy khách trực tuyến đồng thời
                            }
                            else
                            {
                                //Đã đạt số lượng có hiệu lực tối đa
                                //Thực hiện xử lý loại bỏ

                                //Cập nhật thành đã bị loại ra
                                RedisHelper.StringSet(itemToken, "SIDELINED", new TimeSpan(0, 60, 0));
                                continue;
                            }
                        }
                    }
                    if (existTokenList.Count > limitCount + 20)
                    {
                        //Sau khi bị loại ra, không có GetCurrent, thao tác thoát. Dẫn đến existTokenList không thểRemoveAt
                        //existTokenList.RemoveRange(5, limitCount + 20 - 5);  //Xóa các mục phía trước (mục đăng nhập mới nhất đã bị reverse lên phía trước)
                        existTokenList.RemoveRange(limitCount, 20 - 1);  //Xóa các mục phía trước (mục đăng nhập mới nhất đã bị reverse lên phía trước)
                    }
                }

                existTokenList.Reverse();   //Đảo ngược lại, để mục đăng nhập sau trở về phía sau list
                existTokenList.Add(token);
                RedisHelper.Set<List<string>>(keyUserTokenList, existTokenList);

                if (HttpContext.Current != null)
                {
                    WebHelper.WriteCookie(_loginUserKey, token);
                }
                else
                {
                    //Không phải BS
                    throw new NotImplementedException();
                }

                RedisHelper.StringSet(token, operatorModel.ToJson(), new TimeSpan(0, 60, 0));
            }
            else
            {
                //Có thể đăng nhập nhiều nơi

                if (HttpContext.Current != null)
                {
                    WebHelper.WriteCookie(_loginUserKey, token);
                }
                else
                {
                    //Không phải BS
                    throw new NotImplementedException();
                }

                RedisHelper.StringSet(token, operatorModel.ToJson(), new TimeSpan(0, 60, 0));
            }
        }

        /// <summary>
        /// Xóa trạng thái đăng nhập
        /// </summary>
        public static void RemoveCurrent()
        {
            string token = null;
            if (HttpContext.Current != null)
            {
                token = WebHelper.GetCookie(_loginUserKey);
            }
            else
            {
                //Không phải BS
                throw new NotImplementedException();
            }

            if (!string.IsNullOrWhiteSpace(token))
            {
                RedisHelper.Remove(token);
            }
        }

        /// <summary>
        /// Lấy ... của mã thông báo danh tính hiện tạiKey
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentToken()
        {
            return WebHelper.GetCookie(_loginUserKey);
        }

        /// <summary>
        /// Bổ sung cookie đăng nhập, áp dụng cho chuyển trang qua các hệ thống con
        /// </summary>
        public static void WriteLoginUserCookie()
        {
            try
            {
                var former = HttpContext.Current.Request.QueryString["appId"];
                if (string.IsNullOrWhiteSpace(former)) return;
                former = former + "_" + _loginUserKeyPart1;
                var cookie = WebHelper.GetCookie(former);
                if (string.IsNullOrWhiteSpace(cookie)) return;
                var json = RedisHelper.StringGet(cookie);
                if (string.IsNullOrWhiteSpace(json)) return;
                var operatorModel = json.ToObject<OperatorModel>();
                if (operatorModel == null || string.IsNullOrWhiteSpace(operatorModel.UserId) && string.IsNullOrWhiteSpace(operatorModel.UserCode)) return;

                var redisKey = Guid.NewGuid().ToString();
                WebHelper.WriteCookie(_loginUserKey, redisKey);
                RedisHelper.StringSet(redisKey, json, new TimeSpan(0, 60, 0));
            }
            catch (Exception e)
            {
                AppLogger.Instance.Error("WriteLoginUserCookie error", e);
            }
        }
    }
}
