using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysRecipeDrugVO
    {
        /// <summary>
        /// Số chi tiết đơn thuốc
        /// </summary>
        public int cfmxId { get; set; }
        /// <summary>
        /// Đơn thuốcID
        /// </summary>
        public int cfnm { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
        public string czh { get; set; }
        /// <summary>
        /// Số thuốc
        /// </summary>
        public string yp { get; set; }
        /// <summary>
        /// Tên chung của thuốc
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Tên thương mại thuốc
        /// </summary>
        public string spm { get; set; }
        /// <summary>
        /// Số lượng quy cách đóng gói
        /// </summary>
        public Decimal bzs { get; set; }
        /// <summary>
        /// Phiếu quy cách đóng gói
        /// </summary>
        public string bzdw { get; set; }
        /// <summary>
        /// Tên nhà sản xuất
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// Số dạng bào chế
        /// </summary>
        public string jxCode { get; set; }
        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string jx { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public Decimal dj { get; set; }
        /// <summary>
        /// Số lượng phát thuốc
        /// </summary>
        public Decimal sl { get; set; }
        /// <summary>
        /// Tổng giá
        /// </summary>
        public Decimal je { get; set; }
        /// <summary>
        /// Có phải là cờ trả thuốc hay không
        /// </summary>
        public string tybz { get; set; }
        /// <summary>
        /// Cấp BHYT
        /// </summary>
        public string zfxz { get; set; }
    }
}
