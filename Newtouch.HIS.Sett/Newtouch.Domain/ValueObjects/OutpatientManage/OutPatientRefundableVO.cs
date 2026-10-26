using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Hoàn phí ngoại trú Query
    /// </summary>
    public class OutPatientRefundableVO
    {

        /// <summary>
        /// 
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? sl { get; set; }

        /// <summary>
        /// Số lượng có thể trả lại
        /// </summary>
        public decimal? ktsl { get; set; }
        public decimal? tsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jsmxje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sqdzt { get; set; }

        public string cflxmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Loại tính phí 1Thuốc 2Hạng mục
        /// </summary>
        public int feeType { get; set; }

        /// <summary>
        /// Số nhóm của thuốc
        /// </summary>
        public string czh { get; set; }


        public int? cfnm { get; set; }
        public string sfxmCode { get; set; }
        public string sfdlCode { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }

        /// <summary>
        /// Mã nội bộ hạng mục(Chưa liên kết đơn thuốc)
        /// </summary>
        public int? xmnm { get; set; }

        /// <summary>
        /// Khóa chính chi tiết thuốc
        /// </summary>
        public int? cfmxId { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Mã bác sĩ
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        public DateTime klsj { get; set; }

        /// <summary>
        /// Là kế hoạch điều trị,Và trạng thái chưa thực hiện
        /// </summary>
        public bool? zljhwzx { get; set; }

        public string jslx { get; set; }
        public int ghnm { get; set; }
        /// <summary>
        /// Bộ thu phíID
        /// </summary>
        public string ztId { get; set; }
        /// <summary>
        /// Số lượng bộ
        /// </summary>
        public int? ztsl { get; set; }
        /// <summary>
        /// Tên bộ
        /// </summary>
        public string ztmc { get; set; }
        /// <summary>
        /// Mã mẫu thu phí
        /// </summary>
        public string sfmb { get; set; }
    }
}
