using Newtouch.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.ValueObjects
{
    [NotMapped]
    public class PresTemplateDetailVO : PresTemplateDetailEntity
    {

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? dwjls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? cls { get; set; }

        public string zxksmc { get; set; }

        /// <summary>
        /// Trường dư thừa,Trên trang đang choselectĐiền vàooptionsdùng khi
        /// </summary>
        public string redundant_jldw { get; set; }
        /// <summary>
        /// Có phải BHYT hay không add by sunny Tần Hoàng Đảo giai đoạn 3 Khi trích dẫn đơn thuốc lịch sử,Bệnh nhân BHYT không thể chọn hạng mục tự chi trả
        /// </summary>
        public string sfyb { get; set; }
        /// <summary>
        /// add by huangshanshan Mẫu ngoại trú sao chép liên kết cách dùng và dạng bào chế,Cách dùng không hiển thị bình thường
        /// </summary>
        public string jxCode { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
    }
}
