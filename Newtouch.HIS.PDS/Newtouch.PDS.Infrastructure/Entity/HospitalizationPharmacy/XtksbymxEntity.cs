using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bản ghi thao tác y lệnh thuốc nội trú
    /// </summary>
    [Table("xt_bqksbymx")]
    [SugarTable("xt_bqksbymx")]
    public class XtksbymxEntity : IEntity<XtksbymxEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
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
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Thời gian sửa đổi cuối
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
