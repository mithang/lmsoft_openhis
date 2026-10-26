using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// phát thuốc thông tin thuốc
    /// </summary>
    public class fyMeidicneInfo
    {
        /// <summary>
        /// chi tiết đơn thuốc số lô Id
        /// </summary>
        public string cfmxphId { get; set; }

        /// <summary>
        /// mã nội bộ đơn thuốc
        /// </summary>
        public string cfnm { get; set; }
        
        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// số lượng
        /// </summary>
        public decimal sl { get; set; }
        
        /// <summary>
        /// đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// đơn vị số lượng
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// nhà sản xuất
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// liều dùng
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// đơn vị liều dùng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// cách dùng
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// lời dặn của bác sĩ(ghi chú)
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// nhóm thuốc
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 1-phát thuốc 2-trả thuốc
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// thời gian thao tác
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
