using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    /// <summary>
    /// Bảng kê chi phí trong ngày
    /// </summary>
    public class InpatientDayFeeDTO
    {
        /// <summary>
        /// Ngày tháng
        /// </summary>
        public string rq { get; set; }
        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// Số tiền loại lớn
        /// </summary>
        public decimal dlje { get; set; }
        /// <summary>
        /// Số tiền trong ngày
        /// </summary>
        public decimal drje { get; set; }
        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal zje { get; set; }
    }

    public class InpatientDayFeeResp {
        public string zyh { get; set; }
        public decimal zje { get; set; }
        public List<InpatientDayFeeRQ> data { get; set; }
    }

    public class InpatientDayFeeRQ
    {
        /// <summary>
        /// Ngày tháng
        /// </summary>
        public string rq { get; set; }
      
        /// <summary>
        /// Số tiền trong ngày
        /// </summary>
        public decimal drzje { get; set; }
        public List<InpatientDayFeeDL> data { get; set; }
    }

    public class InpatientDayFeeDL{
        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// Số tiền loại lớn
        /// </summary>
        public decimal dlje { get; set; }
    }
}
