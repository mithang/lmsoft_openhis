using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày：2018-06-28 15:37
    /// Mô tả：Y lệnh dài hạn
    /// </summary>
    [SugarTable("zy_bqksby")]
    public class PreparationdrugsEntity
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
        /// Sửa người dùng
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// Sửa cuốiNgười dùng
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
