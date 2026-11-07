using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo：毛浩泽
    /// Ngày:2021-07-02 15:37
    /// Mô tả: Nhập viện y lệnh chẩn đoán thông tin
    /// </summary>
    [SugarTable("zy_clyzzd")]
    public class InpatientDiagnosisEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// yzId
        /// </summary>
        /// <returns></returns>
        public string yzId { get; set; }
        /// <summary>
        /// yzh
        /// </summary>
        /// <returns></returns>
        public string yzh { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// Thời gian chẩn đoán
        /// </summary>
        /// <returns></returns>
        public DateTime? zdsj { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        /// <returns></returns>
        public string icd10 { get; set; }
        /// <summary>
        /// Tên chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string zdmc { get; set; }
        /// <summary>
        /// Là không loại y lệnh thuốc gây nghiện hướng thần
        /// </summary>
        /// <returns></returns>
        public string yztag { get; set; }
        /// <summary>
        /// Thứ tự y lệnh
        /// </summary>
        /// <returns></returns>
        public string yzpx { get; set; }
        /// <summary>
        /// Kiểu y lệnh
        /// </summary>
        /// <returns></returns>
        public int? yzlx { get; set; }
        /// <summary>
        /// Tính chất y lệnh
        /// </summary>
        /// <returns></returns>
        public string yzxz { get; set; }
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
        /// 0 Vô hiệu 1 Hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// Id duy nhất của bệnh viện
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
    }
}
