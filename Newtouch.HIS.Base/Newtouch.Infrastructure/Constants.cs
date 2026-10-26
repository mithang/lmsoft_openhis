using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Id tổ chức cấp cao nhất
        /// </summary>
        private static string _TopOrganizeId;

        /// <summary>
        /// Id tổ chức (cấp cao nhất, cấu hình hệ thống)
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                if (_TopOrganizeId == null)
                {
                    _TopOrganizeId = System.Configuration.ConfigurationManager.AppSettings["Top_OrganizeId"] as string;
                    if (string.IsNullOrWhiteSpace(_TopOrganizeId))
                    {
                        throw new Exception("Top_OrganizeId未配置");
                    }
                }
                return _TopOrganizeId;
            }
        }

        /// <summary>
        /// Id ứng dụng
        /// </summary>
        private static string _AppId;

        /// <summary>
        /// Id tổ chức (cấp cao nhất, cấu hình hệ thống)
        /// </summary>
        public static string AppId
        {
            get
            {
                if (_AppId == null)
                {
                    _AppId = System.Configuration.ConfigurationManager.AppSettings["AppId"] as string;
                    if (string.IsNullOrWhiteSpace(_AppId))
                    {
                        //throw new Exception("AppId chưa được cấu hình");
                    }
                }
                return _AppId;
            }
        }

        private static string _LogonDefaultPassword;

        /// <summary>
        /// Mật khẩu mặc định khi đăng nhập (mật khẩu mặc định khi tạo người dùng mới)
        /// </summary>
        public static string LogonDefaultPassword
        {
            get
            {
                if (_LogonDefaultPassword == null)
                {
                    _LogonDefaultPassword = System.Configuration.ConfigurationManager.AppSettings["LoginUser_Default_Password"] as string;
                    if (string.IsNullOrWhiteSpace(_LogonDefaultPassword))
                    {
                        throw new System.ApplicationException("LoginUser_Default_Password未配置");
                    }
                }
                return _LogonDefaultPassword;
            }
        }

    }
}
