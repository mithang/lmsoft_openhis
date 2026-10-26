using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    public class BookingRequestDto: RequestBase
    {
        public string OrgId { get; set; }
        /// <summary>
        /// Bệnh nhân duy nhất dấu hiệu 
        /// </summary>
        public string patid { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        public string xm { get; set; }
        public DateTime? csrq { get; set; }
        public string xb { get; set; }
        public string dh { get; set; }
        public string dz { get; set; }
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        public string ksmc { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// đ ặt lịch khám bệnh ngày 
        /// </summary>
        public  string outDate { get; set; }
        /// <summary>
        /// 起始khám bệnh ngày 
        /// </summary>
        public string FromoutDate { get; set; }
        public string ys { get; set; }
        /// <summary>
        /// Loại ngoại trú thông thư ờng chuyên gia副高V.v.
        /// </summary>
        public string regType { get; set; }
        /// <summary>
        /// mz_ghpb_config lịch trực id
        /// </summary>
        public string ghpbId { get; set; }
        /// <summary>
        /// mz_ghpb_schedule đ ặt lịch vừa 定ngày程id
        /// </summary>
        public string scheduId { get; set; }
        /// <summary>
        /// Đ ăng ký khám性质 tự trả BHYTV.v. brxz V.v.同
        /// </summary>
        public string ghxz { get; set; }
        /// <summary>
        /// số đ ặt lịch 
        /// </summary>
        public string bookId { get; set; }
        /// <summary>
        /// Ngoại trú đ ặt lịch trạng thái
        /// </summary>
        public int? yyzt { get; set; }
        /// <summary>
        /// Đ ã thanh toán chi phí
        /// </summary>
        public string PayFee { get; set; }
        /// <summary>
        /// thanh toán số thứ tự 
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// liên hệ phư ơng thức 
        /// </summary>
        public string lxdh { get; set; }
        /// <summary>
        /// Ngày bắt đ ầu
        /// </summary>
        public string ksrq { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public string jsrq { get; set; }
        /// <summary>
        /// Đ ăng ký khám ngày 
        /// </summary>
        public string ghrq { get; set; }
        /// <summary>
        /// Đ ăng ký khám来源辅助 dấu hiệu Quận分 cùng một 来源của khác nhau 标记bệnh nhân
        /// 体检 dùng 
        /// </summary>
        public string ghlybz { get; set; }
    }
}
