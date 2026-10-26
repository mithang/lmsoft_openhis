using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("mz_cyxm")]
    public class CommonChargeItemEntity : IEntity<CommonChargeItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cyxmId { get; set; }

        /// <summary>
        /// Enum 1 Thường dùng của khoa 2 Thường dùng cá nhân
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng khoa của CSDL
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng nhân viên của CSDL
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Enum 1 Hạng mục phục hồi chức năng 2 Hạng mục xét nghiệm 3Hạng mục kiểm tra
        /// </summary>
        public int xmlx { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng hạng mục thu phí của CSDL
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Trường dư thừa
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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
        public string zt { get; set; }

    }
}
