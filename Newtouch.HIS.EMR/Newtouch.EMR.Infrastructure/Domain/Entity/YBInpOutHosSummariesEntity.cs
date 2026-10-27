using System;
using System.ComponentModel;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2021-03-01 15:24
    /// Mô tả：Tóm tắt ra viện
    /// </summary>
    [SugarTable("YB_Inp_OutHosSummaries")]
    public class YBInpOutHosSummariesEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// Số thứ tự khám
        /// </summary>
        /// <returns></returns>
        [Description("Số thứ tự khám")]
        [Required]
        public string AKC190 { get; set; }

        /// <summary>
        /// BKF636
        /// </summary>
        /// <returns></returns>
        public string BKF636 { get; set; }

        /// <summary>
        /// Số thứ tự nội trú
        /// </summary>
        /// <returns></returns>
        [Description("Số thứ tự nội trú")]
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
        [Description("Tên khoa")]
        [Required]
        public string AKF002 { get; set; }

        /// <summary>
        /// Tên khu bệnh
        /// </summary>
        /// <returns></returns>
        [Description("Tên khu bệnh")]
        [Required]
        public string AKE021 { get; set; }

        /// <summary>
        /// Số giường bệnh
        /// </summary>
        /// <returns></returns>
        [Description("Số giường bệnh")]
        [Required]
        public string BKB410 { get; set; }

        /// <summary>
        /// Định dạng ngày giờ nhập viện：yyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        [Description("Ngày giờ nhập viện")]
        [Required]
        public string BKC192 { get; set; }

        /// <summary>
        /// Định dạng ngày giờ ra viện：yyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        [Description("Ngày giờ ra viện")]
        [Required]
        public string AKC194 { get; set; }

        /// <summary>
        /// Số ngày nằm viện thực tế
        /// </summary>
        /// <returns></returns>
        [Description("Số ngày nằm viện thực tế")]
        [Required]
        public string BKF668 { get; set; }

        /// <summary>
        /// Tình trạng nhập viện
        /// </summary>
        /// <returns></returns>
        [Description("Tình trạng nhập viện")]
        [Required]
        public string BKF264 { get; set; }

        /// <summary>
        /// Kết quả kiểm tra bổ sung dương tính
        /// </summary>
        /// <returns></returns>
        public string BKF378 { get; set; }

        /// <summary>
        /// Mô tả quá trình chẩn đoán và điều trị
        /// </summary>
        /// <returns></returns>
        public string BKF420 { get; set; }

        /// <summary>
        /// Tình hình ra viện
        /// </summary>
        /// <returns></returns>
        [Description("Tình hình ra viện")]
        [Required]
        public string BKF758 { get; set; }

        /// <summary>
        /// Triệu chứng và dấu hiệu khi ra viện
        /// </summary>
        /// <returns></returns>
        public string BKF139 { get; set; }

        /// <summary>
        /// Y lệnh ra viện
        /// </summary>
        /// <returns></returns>
        public string BKF759 { get; set; }

        /// <summary>
        /// Mã kết quả điều trị
        /// </summary>
        /// <returns></returns>
        public string BKF439 { get; set; }

        /// <summary>
        /// Mã bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        public string BKF050 { get; set; }

        /// <summary>
        /// Tên bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        [Description("Tên bác sĩ nội trú")]
        [Required]
        public string BKF692 { get; set; }

        /// <summary>
        /// Tên bác sĩ cấp trên
        /// </summary>
        /// <returns></returns>
        [Description("Tên bác sĩ cấp trên")]
        [Required]
        public string BKF267 { get; set; }

        /// <summary>
        /// Ngày giờ ký ( Bác sĩ nội trú) Định dạng：yyyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        public string BKF254 { get; set; }

        /// <summary>
        /// Mô tả kết quả quan sát tứ chẩn Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF473 { get; set; }

        /// <summary>
        /// Nguyên tắc và phương pháp điều trị
        /// </summary>
        /// <returns></returns>
        public string BKF440 { get; set; }

        /// <summary>
        /// Phương pháp sắc thuốc Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF451 { get; set; }

        /// <summary>
        /// Cách dùng thuốc Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF457 { get; set; }
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
