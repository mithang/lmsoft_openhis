using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("WF_ProcessTransitionHistory")]
    public class WF_ProcessTransitionHistoryEntity : IEntity<WF_ProcessTransitionHistoryEntity>
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
        public string FromNodeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ToNodeId { get; set; }

        /// <summary>
        /// Trạng thái chuyển đổi 0Bình thường,1Không đồng ý   2Từ chối
        /// </summary>
        public int TransitionSate { get; set; }

        /// <summary>
        /// 0Đang chạy,1Kết thúc chạy,2Bị thu hồi,3Không đồng ý,4Bị từ chối
        /// </summary>
        public int IsFinish { get; set; }

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
