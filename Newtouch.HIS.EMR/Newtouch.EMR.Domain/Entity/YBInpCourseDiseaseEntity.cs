using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2021-03-01 15:24
    /// Mô tả: Ghi chép diễn biến bệnh
    /// </summary>
    [Table("YB_Inp_CourseDisease")]
    public class YBInpCourseDiseaseEntity : IEntity<YBInpCourseDiseaseEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Số thứ tự khám bệnh
        /// </summary>
        /// <returns></returns>
        [Description("就诊流水号")]
        [Required]
        public string AKC190 { get; set; }

        /// <summary>
        /// Số thứ tự nhập viện
        /// </summary>
        /// <returns></returns>
        [Description("住院流水号")]
        [Required]
        public string BKC191 { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        /// <returns></returns>
        public string AKF001 { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        /// <returns></returns>
        [Description("科室名称")]
        [Required]
        public string AKF002 { get; set; }

        /// <summary>
        /// Tên khu bệnh
        /// </summary>
        /// <returns></returns>
        [Description("病区名称")]
        [Required]
        public string AKE021 { get; set; }

        /// <summary>
        /// Số giường bệnh
        /// </summary>
        /// <returns></returns>
        [Description("病床号")]
        [Required]
        public string BKB410 { get; set; }

        /// <summary>
        /// Ngày giờ ghi chép, định dạng: yyyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        [Description("记录日期时间")]
        [Required]
        public string BKF737 { get; set; }

        /// <summary>
        /// Lý do vào viện chính
        /// </summary>
        /// <returns></returns>
        [Description("主诉")]
        [Required]
        public string BKF476 { get; set; }

        /// <summary>
        /// Đặc điểm ca bệnh
        /// </summary>
        /// <returns></returns>
        [Description("病例特点")]
        [Required]
        public string BKF108 { get; set; }

        /// <summary>
        /// Kết quả quan sát “tứ chẩn” Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF458 { get; set; }

        /// <summary>
        /// Căn cứ chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string BKF419 { get; set; }

        /// <summary>
        /// Chẩn đoán sơ bộ - Mã chẩn đoán Tây y
        /// </summary>
        /// <returns></returns>
        public string BKF748 { get; set; }

        /// <summary>
        /// Chẩn đoán sơ bộ - Tên chẩn đoán Tây y
        /// </summary>
        /// <returns></returns>
        public string BKF126 { get; set; }

        /// <summary>
        /// Chẩn đoán sơ bộ - Mã tên bệnh Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF128 { get; set; }

        /// <summary>
        /// Chẩn đoán sơ bộ - Tên bệnh Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF127 { get; set; }

        /// <summary>
        /// Chẩn đoán sơ bộ - Mã chứng hậu Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF130 { get; set; }

        /// <summary>
        /// Chẩn đoán sơ bộ - Chứng hậu Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF129 { get; set; }

        /// <summary>
        /// Chẩn đoán phân biệt - Mã chẩn đoán Tây y
        /// </summary>
        /// <returns></returns>
        public string BKF856 { get; set; }

        /// <summary>
        /// Chẩn đoán phân biệt - Tên chẩn đoán Tây y
        /// </summary>
        /// <returns></returns>
        public string BKF857 { get; set; }

        /// <summary>
        /// Chẩn đoán phân biệt - Mã tên bệnh Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF855 { get; set; }

        /// <summary>
        /// Chẩn đoán phân biệt - Tên bệnh Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF854 { get; set; }

        /// <summary>
        /// Chẩn đoán phân biệt - Mã chứng hậu Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF204 { get; set; }

        /// <summary>
        /// Chẩn đoán phân biệt - Chứng hậu Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF203 { get; set; }

        /// <summary>
        /// Kế hoạch chẩn trị
        /// </summary>
        /// <returns></returns>
        public string BKF421 { get; set; }

        /// <summary>
        /// Nguyên tắc và phương pháp điều trị
        /// </summary>
        /// <returns></returns>
        public string BKF440 { get; set; }

        /// <summary>
        /// Mã bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        [Description("住院医师编号")]
        [Required]
        public string BKF693 { get; set; }

        /// <summary>
        /// Họ tên bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        [Description("住院医师姓名")]
        [Required]
        public string BKF692 { get; set; }

        /// <summary>
        /// Họ tên bác sĩ cấp trên
        /// </summary>
        /// <returns></returns>
        [Description("上级医师姓名")]
        [Required]
        public string BKF267 { get; set; }
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

    }
}