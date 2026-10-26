using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("WF_ProcessInstance")]
    public class WF_ProcessInstanceEntity : IEntity<WF_ProcessInstanceEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemeInfoCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemeInfoVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CustomName { get; set; }

        /// <summary>
        /// Enum 1Thông thường 9Quan trọng
        /// </summary>
        public int wfLevel { get; set; }

        /// <summary>
        /// Dấu nút hiện tại
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PreviousId { get; set; }

        /// <summary>
        /// Nút hiện tại(ActivityId)Người xử lý của
        /// </summary>
        public string MarkerList { get; set; }

        /// <summary>
        /// 0Đang chạy,1Kết thúc chạy,2Bị thu hồi,3Không đồng ý,4Bị từ chối
        /// </summary>
        public int IsFinish { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

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
