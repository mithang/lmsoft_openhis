using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Giường bệnh
    /// </summary>
    [Table("xt_cw")]
    public class SysWardBedEntity : IEntity<SysWardBedEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int cwId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 0 Không phải giường kê thêm 1 Giường kê thêm
        /// </summary>
        public string jcbz { get; set; }

        /// <summary>
        /// from xt_sfxm
        /// </summary>
        public string cwf { get; set; }

        /// <summary>
        /// Chi phí giường bệnh theo quy định của kho hạng mục giai đoạn bốn bảo hiểm y tế from xt_sfxm
        /// </summary>
        public string djyyjsf { get; set; }

        /// <summary>
        /// from xt_sfxm
        /// </summary>
        public string bszlf { get; set; }

        /// <summary>
        /// Chi phí giường bệnh theo quy định của kho hạng mục giai đoạn bốn bảo hiểm y tế from xt_sfxm
        /// </summary>
        public string yymjkqjhf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Phòng bệnh
        /// </summary>
        public string bfCode { get; set; }

        /// <summary>
        /// Loại giường bệnh, tương ứng với enum EnumWardBedType
        /// </summary>
        public string cwlx { get; set; }

        /// <summary>
        /// Có đang sử dụng hay không
        /// </summary>
        public bool? sfzy { get; set; }

        public string cwdj { get; set; }

    }
}
