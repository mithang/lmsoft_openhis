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
    /// Mô tả: Tóm tắt ra viện
    /// </summary>
    [Table("YB_Inp_OutHosSummaries")]
    public class YBInpOutHosSummariesEntity : IEntity<YBInpOutHosSummariesEntity>
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
        /// BKF636
        /// </summary>
        /// <returns></returns>
        public string BKF636 { get; set; }

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
        /// Ngày giờ vào viện, định dạng: yyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        [Description("入院日期时间")]
        [Required]
        public string BKC192 { get; set; }

        /// <summary>
        /// Ngày giờ ra viện, định dạng: yyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        [Description("出院日期时间")]
        [Required]
        public string AKC194 { get; set; }

        /// <summary>
        /// Số ngày nằm viện thực tế
        /// </summary>
        /// <returns></returns>
        [Description("实际住院天数")]
        [Required]
        public string BKF668 { get; set; }

        /// <summary>
        /// Tình hình lúc vào viện
        /// </summary>
        /// <returns></returns>
        [Description("入院情况")]
        [Required]
        public string BKF264 { get; set; }

        /// <summary>
        /// Kết quả kiểm tra bổ trợ dương tính
        /// </summary>
        /// <returns></returns>
        public string BKF378 { get; set; }

        /// <summary>
        /// Mô tả quá trình chẩn trị
        /// </summary>
        /// <returns></returns>
        public string BKF420 { get; set; }

        /// <summary>
        /// Tình hình ra viện
        /// </summary>
        /// <returns></returns>
        [Description("出院情况")]
        [Required]
        public string BKF758 { get; set; }

        /// <summary>
        /// Triệu chứng và dấu hiệu lúc ra viện
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
        /// Họ tên bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        [Description("住院医生姓名")]
        [Required]
        public string BKF692 { get; set; }

        /// <summary>
        /// Họ tên bác sĩ cấp trên
        /// </summary>
        /// <returns></returns>
        [Description("上级医生姓名")]
        [Required]
        public string BKF267 { get; set; }

        /// <summary>
        /// Ngày giờ ký (bác sĩ nội trú), định dạng: yyyymmddhhmiss
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