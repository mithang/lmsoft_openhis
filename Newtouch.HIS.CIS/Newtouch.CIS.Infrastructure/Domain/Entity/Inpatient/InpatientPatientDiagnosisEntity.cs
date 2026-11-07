using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả: Nhập viện bệnh nhân chẩn đoán thông tin
    /// </summary>
    [SugarTable("zy_PatDxInfo")]
    public class InpatientPatientDiagnosisEntity
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
        /// WM Tây y, TCM Trung y
        /// </summary>
        public string zddl { get; set; }
        /// <summary>
        /// 1 chẩn đoán nhập viện 2 chẩn đoán xuất viện
        /// </summary>
        /// <returns></returns>
        public string zdlb { get; set; }
        /// <summary>
        /// 0 chẩn đoán chính 1 chẩn đoán hỗ trợ thứ nhất 2 chẩn đoán hỗ trợ thứ hai 3 chẩn đoán hỗ trợ thứ ba 9 chẩn đoán tự định nghĩa
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
        /// 0 Vô hiệu 1 Hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// Tình hình xuất viện
        /// </summary>
        public int? cyqk { get; set; }
        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }
    }
}
