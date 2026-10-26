using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class CardInfoResp
    {
        /// <summary>
        /// Bệnh nhânID
        /// </summary>
        public int PatientNumber { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime Brithday { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }
        
        /// <summary>
        /// Loại thẻ0 tự trả thẻ,1BHYT thẻ 
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// Loại thẻ tên
        /// </summary>
        public string CardTypeName { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string PatType { get; set; }
        /// <summary>
        /// Tính chất bệnh nhânTên
        /// </summary>
        public string PatTypeName { get; set; }
        public string Zjh { get; set; }
    }
}
