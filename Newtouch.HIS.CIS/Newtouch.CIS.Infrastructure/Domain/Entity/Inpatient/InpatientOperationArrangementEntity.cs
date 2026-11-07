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
        /// Id y lệnh tạm thời
        /// </summary>
        public string lsyzid { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// Ngày sắp xếp
        /// </summary>
        /// <returns></returns>
        public DateTime aprq { get; set; }
        /// <summary>
        /// Địa chỉ phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssAddr { get; set; }
        /// <summary>
        /// Khẩn cấp
        /// </summary>
        /// <returns></returns>
        public string urgent { get; set; }
        /// <summary>
        /// ID bác sĩ phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string surgeonId { get; set; }
        /// <summary>
        /// Họ tên bác sĩ phẫu thuật
        /// </summary>
        public string surgeonName { get; set; }
        /// <summary>
        /// ID trợ thủ
        /// </summary>
        /// <returns></returns>
        public string assistant { get; set; }
        /// <summary>
        /// Họ tên trợ thủ
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
