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
        /// Dư thừacủachữđoạn，trangmặttrênởchoselectđiềnoptionscủakhichờdùngđến
        /// </summary>
        public string redundant_jldw { get; set; }
        /// <summary>
        /// làKhôngBảo hiểm y tế add by sunny Giai đoạn 3 Tần Hoàng Đảo dẫndùnglịch sửĐơn thuốckhi，Bảo hiểm y tếbệnh nhânkhôngphương phápchọnmục tự chi trả
        /// </summary>
        public string sfyb { get; set; }
        /// <summary>
        /// add by huangshanshan Ngoại trúmẫusao chépCách dùngvàDạng bào chếliên kết，Cách dùngkhôngphương phápBình thườngHiển thị
        /// </summary>
        public string jxCode { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
    }
}
