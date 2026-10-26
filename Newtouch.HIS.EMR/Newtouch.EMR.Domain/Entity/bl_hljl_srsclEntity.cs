using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Newtouch.EMR.Domain.Entity
{

    [Table("bl_hljl_srscl")]
    public class bl_hljl_srsclEntity : IEntity<bl_hljl_srsclEntity>
    {
        [Key]
        public string Id { get; set; }
        public string rq { get; set; }
        public string sj { get; set; }
        public string zyh { get; set; }
        /// <summary>
        /// Loai dieu duong: 0 Thong thuong, 1 Nguy kich, 2 Phau thuat
        /// </summary>
        public string bllx { get; set; }
        /// <summary>
        /// Loai luong dua vao/thai ra: 0 Dua vao, 1 Thai ra
        /// </summary>
        public string srsclx { get; set; }
        public string mc { get; set; }
        public string ml { get; set; }
        public string dw { get; set; }
        /// <summary>
        /// Duong dung
        /// </summary>
        public string tj { get; set; }
        /// <summary>
        /// Mau sac va hinh dang
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
