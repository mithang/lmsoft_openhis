using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Redis;
using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Id tổ chức (cấp cao nhất, cấu hình hệ thống)
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                return ConstantsBase.TopOrganizeId;
            }
        }

        /// <summary>
        /// Id tổ chức (cấp cao nhất, cấu hình hệ thống)
        /// </summary>
        public static string AppId
        {
            get
            {
                return ConstantsBase.AppId;
            }
        }

        /// <summary>
        /// Lấy khoa dược hiện tại (nhà thuốc hoặc kho thuốc)
        /// </summary>
        public static LoginUserCurrentYfbmModel CurrentYfbm
        {
            get
            {
                var operate = OperatorProvider.GetCurrent();
                return GetCurrentYfbm(operate.UserId);
            }
        }

        /// <summary>
        /// Lấy khoa dược hiện tại (nhà thuốc hoặc kho thuốc)
        /// </summary>
        /// <param name="userId"></param>
        public static LoginUserCurrentYfbmModel GetCurrentYfbm(string userId)
        {
            return RedisHelper.Get<LoginUserCurrentYfbmModel>(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId)) ?? new LoginUserCurrentYfbmModel();
        }
        /// <summary>
        /// Định dạng thời gian
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        
        /// <summary>
        /// Trả về thời gian nhỏ nhất 1970-01-01 00:00:000
        /// </summary>
        public static DateTime MinDateTime
        {
            get { return Convert.ToDateTime("1970-01-01"); }
        }
        
        public static string DeptCode
        {
            get { 
                var operate = OperatorProvider.GetCurrent();
                return operate.DepartmentCode;


            }
        }

    }
}
