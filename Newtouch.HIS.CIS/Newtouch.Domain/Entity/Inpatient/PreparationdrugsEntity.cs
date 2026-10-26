using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Y lệnh dài hạn
    /// </summary>
    [Table("zy_bqksby")]
    public class PreparationdrugsEntity : IEntity<PreparationdrugsEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public string Id { get; set; }

        public string OrganizeId { get; set; }
        public string yfbm { get; set; }
        public string bqbm { get; set; }
        public string ksbm { get; set; }
        public string djh { get; set; }
        public string shzt { get; set; }
        public string tjsj { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }


        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}