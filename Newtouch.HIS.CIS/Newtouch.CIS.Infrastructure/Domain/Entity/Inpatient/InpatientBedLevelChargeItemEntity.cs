using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo：hoàng杉杉
    /// Ngày:2018-12-12 15:37
    /// Mô tả:Nhập việnPhí giường bệnhdùngtương ứngbảng
    /// </summary>
    [SugarTable("zy_LevelChargedy")]
    public class InpatientBedLevelChargeItemEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// BedCode
        /// </summary>
        /// <returns></returns>
        public string LevelCode { get; set; }

        public string sfxmCode { get; set; }
        /// <summary>
        /// ChargeNum
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
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
        /// 0Vô hiệu1Hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}
