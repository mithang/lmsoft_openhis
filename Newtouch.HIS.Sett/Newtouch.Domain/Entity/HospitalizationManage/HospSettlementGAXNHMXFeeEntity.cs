using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Tạo：李鑫
    /// Ngày tháng:2019-07-19 14:00
    /// Mô tả:Quyết toán nội trú-Chi phí BHYT-Quý An
    /// </summary>
    [Table("zy_js_gaxnhjyfy_details")]
    public class HospSettlementGAXNHMXFeeEntity : IEntity<HospSettlementGAXNHMXFeeEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public int Id { get; set; }
        public string inpId { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Liên kếtzy_js jsnm
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// Tên tính toán
        /// </summary>
        public string calcName { get; set; }
        /// <summary>
        /// Giải thích tính toán
        /// </summary>
        public string calcMemo { get; set; }
        /// <summary>
        /// Tính toán lần này
        /// </summary>
        public string calcBefore { get; set; }
        /// <summary>
        /// Kết quả
        /// </summary>
        public string calcAfter { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

    }
}