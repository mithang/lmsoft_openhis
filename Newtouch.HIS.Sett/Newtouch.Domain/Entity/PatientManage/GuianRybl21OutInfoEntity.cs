using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Gzyb_OutRYBL_21")]
    public class GuianRybl21OutInfoEntity : IEntity<GuianRybl21OutInfoEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string prm_ykc010 { get; set; }
        /// <summary>
        /// Số khám bệnh
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// Mã trung tâm chi nhánh(HISPhải lưu trường này,Làm đầu vào cho giao dịch sau này)
        /// </summary>
        public string prm_yab003 { get; set; }
        /// <summary>
        /// Loại thanh toán
        /// </summary>
        public string prm_aka130 { get; set; }
        /// <summary>
        /// Số cá nhân
        /// </summary>
        public string prm_aac001 { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string prm_aac003 { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string prm_aac004 { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string prm_aac002 { get; set; }
        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string prm_aab001 { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string prm_aab004 { get; set; }
        /// <summary>
        /// Trạng thái nhân viên
        /// </summary>
        public string prm_akc021 { get; set; }
        /// <summary>
        /// Tuổi thực
        /// </summary>
        public int prm_akc023 { get; set; }
        /// <summary>
        /// Phương pháp BHXH(HISPhải lưu trường này,Làm đầu vào cho giao dịch sau này)
        /// </summary>
        public string prm_ykb065 { get; set; }
        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số giao dịch
        /// </summary>
        public string jylsh { get; set; }
        /// <summary>
        /// Mã xác minh giao dịch
        /// </summary>
        public string jyyzm { get; set; }
    }
}
