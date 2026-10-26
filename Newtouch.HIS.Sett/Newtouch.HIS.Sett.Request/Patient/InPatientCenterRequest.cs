using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    public class InPatientCenterRequest: RequestBase
    {
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Từ khóa:Họ tên, Phiên âm
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// nghiệp vụ loại:mz, zy
        /// </summary>
        public string ywlx { get; set; }
    }
}
