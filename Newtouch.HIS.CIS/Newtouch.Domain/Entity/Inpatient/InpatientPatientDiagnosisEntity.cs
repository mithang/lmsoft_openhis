using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Thông tin chẩn đoán bệnh nhân nội trú
    /// </summary>
    [Table("zy_PatDxInfo")]
    public class InpatientPatientDiagnosisEntity : IEntity<InpatientPatientDiagnosisEntity>
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
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// WM Tây y  TCM Đông y
        /// </summary>
        public string zddl { get; set; }
        /// <summary>
        /// 1Chẩn đoán nhập viện2Chẩn đoán xuất viện
        /// </summary>
        /// <returns></returns>
        public string zdlb { get; set; }
        /// <summary>
        /// 0Chẩn đoán chính1Chẩn đoán phụ thứ nhất2Chẩn đoán phụ thứ hai3Chẩn đoán phụ thứ ba9Chẩn đoán tùy chỉnh
        /// </summary>
        /// <returns></returns>
        public string zdlx { get; set; }
        /// <summary>
        /// zddm
        /// </summary>
        /// <returns></returns>
        public string zddm { get; set; }
        /// <summary>
        /// zdmc
        /// </summary>
        /// <returns></returns>
        public string zdmc { get; set; }
        /// <summary>
        /// zdyzdmc
        /// </summary>
        /// <returns></returns>
        public string zdyzdmc { get; set; }
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
        /// Tình hình xuất viện
        /// </summary>
        public int? cyqk { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }
    }
}