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
    [Table("zy_bqksbymx")]
    public class PreparationdrugsMXEntity : IEntity<PreparationdrugsMXEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public string Id { get; set; }
        public string byId { get; set; }

        public string OrganizeId { get; set; }
        public string ypdm { get; set; }
        public string sl { get; set; }
        public string dw { get; set; }
        public string zhyz { get; set; }
        public string pfj { get; set; }
        public string lsj { get; set; }
        public string zje { get; set; }
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
        public string gg { get; set; }
        public string yplb { get; set; }
        public string yxq { get; set; }
        public string sccj { get; set; }
        public string ypmc { get; set; }
        public string pc { get; set; }
        public string ph { get; set; }
    }
}