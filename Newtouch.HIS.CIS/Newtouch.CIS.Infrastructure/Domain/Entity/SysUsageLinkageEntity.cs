using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{

    /// <summary>
    /// Hệ thốngCách dùngliênđộng
    /// </summary>
    [SugarTable("xt_usageLinkage")]
    public class SysUsageLinkageEntity
    {
        
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúc
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã cách dùng
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// Mã hạng mục
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// MụcMã nhóm lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạongười
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửangười
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0：Vô hiệu
        /// </summary>
        public string zt { get; set; }
    }
}
