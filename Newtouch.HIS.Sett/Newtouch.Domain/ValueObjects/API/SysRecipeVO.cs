using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysRecipeVO
    {
        /// <summary>
        /// Đơn thuốcID
        /// </summary>
        public int cfnm { get; set; }
        /// <summary>
        /// Số giao dịch ngoại trú
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// Số thẻ khám bệnh
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Trạng thái đơn thuốc
        /// </summary>
        public string cfzt { get; set; }
        /// <summary>
        /// Loại đơn thuốc
        /// </summary>
        public int cflx { get; set; }
        /// <summary>
        /// Tên loại đơn thuốc
        /// </summary>
        public string cflxmc { get; set; }
        /// <summary>
        /// Mã khoa kê đơn
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Tên khoa kê đơn
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Mã chức danh bác sĩ kê đơn
        /// </summary>
        public string dutyId { get; set; }
        /// <summary>
        /// Chức danh bác sĩ kê đơn
        /// </summary>
        public string dutyName { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ kê đơn
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// Tên bác sĩ kê đơn
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// Thời gian kê đơn
        /// </summary>
        public DateTime? createTime { get; set; }
        /// <summary>
        /// Số tiền đơn thuốc
        /// </summary>
        public Decimal zje { get; set; }
        /// <summary>
        /// Cờ cấp cứu
        /// </summary>
        public string mjzbz { get; set; }
    }
}
