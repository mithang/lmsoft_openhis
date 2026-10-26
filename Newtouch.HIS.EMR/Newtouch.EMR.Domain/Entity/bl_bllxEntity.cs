using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.EMR.Domain.Entity
{

    [Table("bl_bllx")]
    public  class bl_bllxEntity : IEntity<bl_bllxEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Dinh danh loai benh an
        /// </summary>
        public string bllx { get; set; }
        public string bllxmc { get; set; }       
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Chuc vu lien ket
        /// </summary>
        public string RelDutys { get; set; }
        /// <summary>
        /// Ma ngan loai benh an
        /// </summary>
        public string bllxcode { get; set; }
        /// <summary>
        /// Sap xep
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// Bang du lieu lien ket, mac dinh khi trong
        /// </summary>
        public string relTB { get; set; }
        /// <summary>
        /// Cap thu muc, cap ban dau la 1
        /// </summary>
        public string MenuLev { get; set; }
        /// <summary>
        /// Ten cap thu muc, mac dinh cap mot la thoi gian, phan tach bang ¡ù
        /// </summary>
        public string MenuLevName { get; set; }
        public string ParentId { get; set; }
        public string IsRoot { get; set; }
        /// <summary>
        /// Dinh danh mau ngoai tru, 1 la ngoai tru 
        /// </summary>
        public string mzbz { get; set; }
    }
}
