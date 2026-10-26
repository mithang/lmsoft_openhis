using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InterfaceSync
{
    public class ResponseAPIBase
    {
        /// <summary>
        /// Mã trả về nghiệp vụ
        /// </summary>
        public ResponseResultCodeAPI code { get; set; }

        /// <summary>
        /// Mô tả mã trả về nghiệp vụ
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// Dữ liệu phản hồi(ResponseResultCode.SUCCESSDữ liệu trả về khi)
        /// </summary>
        public object data { get; set; }
    }
    /// <summary>
    /// Loại chuyển tiếp Chỉ dùng để gọihisNội bộApi
    /// </summary>
    public class ResponseBaseOld : ResponseBase
    {

        public string sub_code { get; set; }
        public string sub_msg { get; set; }
    }


    public enum ResponseResultCodeAPI
    {
        /// <summary>
        /// Giá trị mặc định
        /// </summary>
        Default = -1,
        /// <summary>
        /// Thành công
        /// </summary>
        SUCCESS = 10000,

        /// <summary>
        /// Lỗi nội bộ máy chủ(Chương trình gặp ngoại lệ)
        /// </summary>
        ERROR = 20000,

        /// <summary>
        /// Xử lý nghiệp vụ thất bại:	Lý do thất bại cụ thể xem mã lỗi giao diện trả về    
        /// </summary>
        FAIL = 40004
    }
}
