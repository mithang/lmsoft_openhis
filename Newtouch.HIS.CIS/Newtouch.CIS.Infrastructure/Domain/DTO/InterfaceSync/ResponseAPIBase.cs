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
        /// nghiệp vụ Quay lạimã
        /// </summary>
        public ResponseResultCodeAPI code { get; set; }

        /// <summary>
        /// nghiệp vụ Quay lạimã Mô tả
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// hưởngứng Dữ liệu(ResponseResultCode.SUCCESS khi của Quay lại Dữ liệu)
        /// </summary>
        public object data { get; set; }
    }
    /// <summary>
    /// quavượtloại chỉdùngtạigọihistrongbộ Api
    /// </summary>
    public class ResponseBaseOld : ResponseBase
    {

        public string sub_code { get; set; }
        public string sub_msg { get; set; }
    }


    public enum ResponseResultCodeAPI
    {
        /// <summary>
        /// Mặc định Giá trị
        /// </summary>
        Default = -1,
        /// <summary>
        /// thành công
        /// </summary>
        SUCCESS = 10000,

        /// <summary>
        /// phụcvụdụng cụtrongbộ Lỗi(trìnhthứ tựphátsinh Ngoại lệ)
        /// </summary>
        ERROR = 20000,

        /// <summary>
        /// Xử lý nghiệp vụ thất bại: nguyên nhân thất bại cụ thể xem mã lỗi trả về của giao diện
        /// </summary>
        FAIL = 40004
    }
}
