using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 周珏琦
    /// Ngày:2019-02-11 15:37
    /// Mô tả:Sắp xếp phẫu thuật
    /// </summary>
    [SugarTable("zy_OperationArrangement")]
    public class InpatientOperationArrangementEntity
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
        /// Y lệnh tạm thờiid
        /// </summary>
        public string lsyzid { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// anxếpNgày
        /// </summary>
        /// <returns></returns>
        public DateTime aprq { get; set; }
        /// <summary>
        /// Phẫu thuậtĐịa chỉ
        /// </summary>
        /// <returns></returns>
        public string ssAddr { get; set; }
        /// <summary>
        /// gấpgấp
        /// </summary>
        /// <returns></returns>
        public string urgent { get; set; }
        /// <summary>
        /// Phẫu thuậtBác sĩID
        /// </summary>
        /// <returns></returns>
        public string surgeonId { get; set; }
        /// <summary>
        /// Phẫu thuậtBác sĩHọ tên
        /// </summary>
        public string surgeonName { get; set; }
        /// <summary>
        /// giúptayID
        /// </summary>
        /// <returns></returns>
        public string assistant { get; set; }
        /// <summary>
        /// giúptayHọ tên
        /// </summary>
        public string assistantName { get; set; }
        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        /// <returns></returns>
        public string anesthesiaType { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <returns></returns>
        public string remark { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
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
        /// zt
        /// </summary>
        public string zt { get; set; }
    }
}
