using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.Entity.QcItemManage
{
    [Table("Mr_Qc_Score")]
    public class MrQcScoreEntity : IEntity<MrQcScoreEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Zyh { get; set; }

        /// <summary>
        /// Bệnh ánID
        /// </summary>
        //public string WritId { get; set; }

        /// <summary>
        /// loại kiểm soát chất lượng 
        /// </summary>
        public string BllxId { get; set; }

        /// <summary>
        /// Mẫu bệnh ánId
        /// </summary>
        //public string BlmbId { get; set; }

        /// <summary>
        /// Tên bệnh án
        /// </summary>
        public string Blmc { get; set; }
        /// <summary>
        /// hạng mụcCODE
        /// </summary>
        public string ScoreCode { get; set; }
        /// <summary>
        /// tên hạng mục
        /// </summary>
        public string ScoreMc { get; set; }
        /// <summary>
        /// điểm số
        /// </summary>
        public decimal? ScoreMcValue { get; set; }
        /// <summary>
        /// số lượng lỗi
        /// </summary>
        public int? sl { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? Px { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

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
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
