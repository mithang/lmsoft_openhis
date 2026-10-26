using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// Bản tin trả về của giao diện tra cứu chi tiết đơn thuốc đã quyết toán
    /// </summary>
    public class OutpatientSettledRpQueryResponseDTO
    {
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        public string mzh { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốcID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết quyết toán
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// Thời gian thu phí
        /// </summary>
        public DateTime sfsj { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Liều dùng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Liều dùng+Đơn vị liều lượng
        /// </summary>
        public string ylstr { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Đơn vị số lượng
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Số lượng+Đơn vị số lượng
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// Liều lượng+Đơn vị liều dùng
        /// </summary>
        public string jlstr { get; set; }
        public string yfcode { get; set; }
        public string ycCode { get; set; }
        public int? zxcs { get; set; }
    }
}
