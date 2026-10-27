using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("mz_cyxm")]
    public class CommonChargeItemEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cyxmId { get; set; }

        /// <summary>
        /// Liệt kê 1 Khoathường dùng 2 Cá nhânthường dùng
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// liên kếtbasekhocủaKhoabảng
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// liên kếtbasekhocủa ngườiviênbảng
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Liệt kê 1 Phục hồi chức năngMục 2 Xét nghiệmMục 3Kiểm traMục
        /// </summary>
        public int xmlx { get; set; }

        /// <summary>
        /// liên kếtbasekhocủaMục thu phíbảng
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Dư thừachữđoạn
        /// </summary>
        public string xmmc { get; set; }

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
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
