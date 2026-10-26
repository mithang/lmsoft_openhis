using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Hằng số hệ thống
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
    }
}