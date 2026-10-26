using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2018-09-11 10:46
    /// Mô tả: Bảng quan hệ văn bản bệnh án của bệnh nhân - ngoại trú
    /// </summary>
    [Table("mz_meddocs_relation")]
    public class MzmeddocsrelationEntity : IEntity<MzmeddocsrelationEntity>
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
        /// Định danh bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string patid { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        /// <returns></returns>
        public string mzh { get; set; }
        /// <summary>
        /// blId
        /// </summary>
        /// <returns></returns>
        public string blId { get; set; }
        /// <summary>
        /// Id loại bệnh án
        /// </summary>
        /// <returns></returns>
        public string mbId { get; set; }
        /// <summary>
        /// Tên bệnh án
        /// </summary>
        /// <returns></returns>
        public string blmc { get; set; }
        /// <summary>
        /// Tiêu đề bệnh án
        /// </summary>
        /// <returns></returns>
        public string blbt { get; set; }
        /// <summary>
        /// Đã ký hay chưa: 0 Chưa ký, 1 Đã ký
        /// </summary>
        /// <returns></returns>
        public int? blzt { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        /// <returns></returns>
        public string ks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ksmc { get; set; }
        /// <summary>
        /// ysgh
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// ysxm
        /// </summary>
        /// <returns></returns>
        public string ysxm { get; set; }
        /// <summary>
        /// blrq
        /// </summary>
        /// <returns></returns>
        public DateTime? blrq { get; set; }
        /// <summary>
        /// Nút cha
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// Có phải nút cha hay không: 0 Không, 1 Có
        /// </summary>
        /// <returns></returns>
        public int? IsParent { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
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
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        public string bllx { get; set; }
        /// <summary>
        /// Cờ tải lên bảo hiểm y tế: 0 Chưa tải lên, 1 Đã tải lên
        /// </summary>
        public string YbUploadFlag { get; set; }
        /// <summary>
        /// Loại định dạng văn bản - xml/html
        /// </summary>
        public string doctype { get; set; }
    }
}