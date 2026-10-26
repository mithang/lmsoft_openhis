using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysDrugVO
    {
        /// <summary>
        /// Số thuốc
        /// </summary>
        public  int ypId { get; set; }
        /// <summary>
        /// Tên chung của thuốc
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Tên thương mại thuốc
        /// </summary>
        public string spm { get; set; }
        /// <summary>
        /// Tên nhà sản xuất
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// Đơn vị quy cách hàm lượng
        /// </summary>
        public string jldw { get; set; }
        /// <summary>
        /// Quy cách hàm lượng
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// Mã dạng bào chế
        /// </summary>
        public string jxCode { get; set; }
        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string jx { get; set; }
        /// <summary>
        /// Đơn vị quy cách đóng gói
        /// </summary>
        public string bzdw { get; set; }
        /// <summary>
        /// Số lượng quy cách đóng gói
        /// </summary>
        public decimal bzs { get; set; }
        /// <summary>
        /// Dấu thuốc kháng khuẩn
        /// </summary>
        public string isKss { get; set; }
        /// <summary>
        /// Dấu thuốc cơ bản(Thuốc cơ bản giá trị cố định)
        /// </summary>
        public string jbywbj { get; set; }
        /// <summary>
        /// Đơn giá thuốc
        /// </summary>
        public decimal lsj { get; set; }
        /// <summary>
        /// Cấp BHYT
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// Mã thuốc BHYT
        /// </summary>
        public string ypCode { get; set; }
    }
}
