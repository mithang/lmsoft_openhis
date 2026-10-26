using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2021-03-01 15:24
    /// Mô tả: Tóm tắt ra viện - Chẩn đoán
    /// </summary>
    [Table("YB_Inp_OutHosSummaries_Diag")]
    public class YBInpOutHosSummariesDiagEntity : IEntity<YBInpOutHosSummariesDiagEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Số thứ tự khám bệnh
        /// </summary>
        /// <returns></returns>
        public string AKC190 { get; set; }

        /// <summary>
        /// Số thẻ sức khỏe cư dân
        /// </summary>
        /// <returns></returns>
        public string BKF636 { get; set; }

        /// <summary>
        /// Số thứ tự nhập viện
        /// </summary>
        /// <returns></returns>
        public string BKC191 { get; set; }

        /// <summary>
        /// Phân loại chẩn đoán: 1 Chẩn đoán lúc vào viện; 2 Chẩn đoán lúc ra viện
        /// </summary>
        /// <returns></returns>
        public string BKF417 { get; set; }

        /// <summary>
        /// Cờ chẩn đoán chính/phụ: 1 Chẩn đoán chính; 2 Chẩn đoán khác; 3 Chẩn đoán bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BKF718 { get; set; }

        /// <summary>
        /// Thứ tự chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string BKF512 { get; set; }

        /// <summary>
        /// Mã chẩn đoán Tây y
        /// </summary>
        /// <returns></returns>
        public string BKF857 { get; set; }

        /// <summary>
        /// Tên chẩn đoán Tây y
        /// </summary>
        /// <returns></returns>
        public string BKF856 { get; set; }

        /// <summary>
        /// Mã tên bệnh Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF460 { get; set; }

        /// <summary>
        /// Tên bệnh Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF459 { get; set; }

        /// <summary>
        /// Mã chứng hậu Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF475 { get; set; }

        /// <summary>
        /// Chứng hậu Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF474 { get; set; }

        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

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
        /// <summary>
        /// Id chính của tóm tắt ra viện
        /// </summary>
        public string OutId { get; set; }

    }
}