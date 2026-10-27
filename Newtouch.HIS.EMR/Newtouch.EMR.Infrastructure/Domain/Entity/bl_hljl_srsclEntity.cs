using System;
using SqlSugar;
namespace Newtouch.EMR.Domain.Entity
{

    [SugarTable("bl_hljl_srscl")]
    public class bl_hljl_srsclEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string rq { get; set; }
        public string sj { get; set; }
        public string zyh { get; set; }
        /// <summary>
        ///  0һ 1Σ 2
        /// </summary>
        public string bllx { get; set; }
        /// <summary>
        ///  0 1
        /// </summary>
        public string srsclx { get; set; }
        public string mc { get; set; }
        public string ml { get; set; }
        public string dw { get; set; }
        /// <summary>
        /// ;
        /// </summary>
        public string tj { get; set; }
        /// <summary>
        /// ɫ״
        /// </summary>
        public string ysxz { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
    }
}
