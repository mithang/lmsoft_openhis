using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.MRQC.Infrastructure
{
    /// <summary>
    /// hằng số hệ thống
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// tổ chứcId（cấp cao nhất，cấu hình hệ thống）
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                return ConstantsBase.TopOrganizeId;
            }
        }
        /// <summary>
        /// tổ chứcId（cấp cao nhất，cấu hình hệ thống）
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