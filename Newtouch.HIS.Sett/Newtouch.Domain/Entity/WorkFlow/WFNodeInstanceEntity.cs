using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("WF_NodeInstance")]
    public class WF_NodeInstanceEntity : IEntity<WF_NodeInstanceEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProcessId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemeNodeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemeNodeCode { get; set; }

        /// <summary>
        /// từ Mẫu nút Được dư thừa chuyển qua;hoặc Tên tùy chỉnh
        /// </summary>
        public string Name { get; set; }

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
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
