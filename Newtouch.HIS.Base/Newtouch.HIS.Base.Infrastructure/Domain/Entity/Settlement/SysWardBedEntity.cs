using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Giường bệnh
    /// </summary>
    [SugarTable("xt_cw")]
    public class SysWardBedEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int cwId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 0 Không thêm giường 1 Thêm giường
        /// </summary>
        public string jcbz { get; set; }

        /// <summary>
        /// from xt_sfxm
        /// </summary>
        public string cwf { get; set; }

        /// <summary>
        /// Chi phí giường theo kho hạng mục BHYT giai đoạn 4from xt_sfxm
        /// </summary>
        public string djyyjsf { get; set; }

        /// <summary>
        /// from xt_sfxm
        /// </summary>
        public string bszlf { get; set; }

        /// <summary>
        /// Chi phí giường theo kho hạng mục BHYT giai đoạn 4from xt_sfxm
        /// </summary>
        public string yymjkqjhf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

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
        /// Phòng bệnh
        /// </summary>
        public string bfCode { get; set; }

        /// <summary>
        /// Loại giường bệnh Enum tương ứngEnumWardBedType
        /// </summary>
        public string cwlx { get; set; }

        /// <summary>
        /// Có bị chiếm không
        /// </summary>
        public bool? sfzy { get; set; }

        public string cwdj { get; set; }

    }
}
