using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:毛浩泽
    /// Ngày:2021-07-02 15:37
    /// Mô tả:Thông tin chẩn đoán y lệnh nội trú
    /// </summary>
    [Table("zy_clyzzd")]
    public class InpatientDiagnosisEntity : IEntity<InpatientDiagnosisEntity>
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
        /// Có phải y lệnh thuốc hướng thần/gây mê hay không
        /// </summary>
        /// <returns></returns>
        public string yztag { get; set; }
        /// <summary>
        /// Sắp xếp y lệnh
        /// </summary>
        /// <returns></returns>
        public string yzpx { get; set; }
        /// <summary>
        /// Loại y lệnh
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
        /// 0Vô hiệu1Có hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// duy nhất của bệnh việnid
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
    }
}