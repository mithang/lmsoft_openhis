using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    /// <summary>
    /// Đăng ký khám ngoại trú
    /// </summary>
    public class BespeakRegisterParamDTO
    {
        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// Khoa đăng ký khám ngoại trú
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Loại đăng ký khám ngoại trú 1:Khám ngoại trú thông thường  2:Cấp cứu  3:Khám chuyên gia
        /// </summary>
        public int mzlx { get; set; }

        /// <summary>
        /// Đăng ký khám chuyên gia Mã nhân viên bác sĩ
        /// </summary>
        public string ysgh { get; set; }
    }
}
