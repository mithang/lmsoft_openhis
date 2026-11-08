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
        /// Dư thừa của chữ đoạn,trang mặt trên ở cho select điền options của khi chờ dùng đến
        /// </summary>
        public string redundant_jldw { get; set; }
        /// <summary>
        /// là Không Bảo hiểm y tế add by sunny Giai đoạn 3 Tần Hoàng Đảo dẫn dùng lịch sử Đơn thuốc khi,Bảo hiểm y tế bệnh nhân không phương pháp chọn mục tự chi trả
        /// </summary>
        public string sfyb { get; set; }
        /// <summary>
        /// add by huangshanshan Ngoại trú mẫu sao chép Cách dùng và Dạng bào chế liên kết,Cách dùng không phương pháp Bình thường Hiển thị
        /// </summary>
        public string jxCode { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
    }
}
