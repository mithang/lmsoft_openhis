using NewtouchHIS.Base.Domain.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.MRQC
{
    public class MedicalBlApplyVo
    {
        public string? Id { get; set; }
        [StringLength(50, ErrorMessage = "OrganizeId giới hạn độ dài là 50")]
        public string? OrganizeId { get; set; }
        /// <summary>
        /// Đăng kýKhoa
        /// </summary>
        [Required(ErrorMessage = "ApplyDept không được để trống")]
        public string ApplyDept { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        [Required(ErrorMessage = "Zyh không được để trống")]
        public string Zyh { get; set; }
        /// <summary>
        /// Giường bệnh
        /// </summary>
        [Required(ErrorMessage = "Bed không được để trống")]
        public string Bed { get; set; }
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        [Required(ErrorMessage = "PatName không được để trống")]
        public string PatName { get; set; }
        /// <summary>
        /// Đăng kýBác sĩ
        /// </summary>
        [Required(ErrorMessage = "ApplyDoctor không được để trống")]
        public string ApplyDoctor { get; set; }
        /// <summary>
        /// Đăng kýLoại 0:Lập bệnh án mới 1:Sửa bệnh án
        /// </summary>
        [Required(ErrorMessage = "ApplyType không được để trống")]
        public int ApplyType { get; set; }
        /// <summary>
        /// Tên bệnh án
        /// </summary>
        [Required(ErrorMessage = "MedicalName không được để trống")]
        public string MedicalName { get; set; }
        /// <summary>
        /// Thời gian hoàn thành kiểm soát chất lượng
        /// </summary>
        [Required(ErrorMessage = "CompletionDate không được để trống")]
        public DateTime? CompletionDate { get; set; }
        /// <summary>
        /// Đăng kýThời gian
        /// </summary>
        [Required(ErrorMessage = "ApplyDate không được để trống")]
        public DateTime? ApplyDate { get; set; }
        /// <summary>
        /// Đăng kýThời gian hoàn thành
        /// </summary>
        [Required(ErrorMessage = "ApplyCompletionDate không được để trống")]
        public DateTime? ApplyCompletionDate { get; set; }
        /// <summary>
        /// Đăng kýNguyên nhân
        /// </summary>
        [Required(ErrorMessage = "ApplyReason không được để trống")]
        public string ApplyReason { get; set; }

        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        public DateTime? Ryrq { get; set; }
        /// <summary>
        /// Ngày xuất viện
        /// </summary>
        public DateTime? Cyrq { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatorCode { get; set; }
        //public DateTime? LastModifyTime { get; set; }
        //public string? LastModifierCode { get; set; }
        //public string? zt { get; set; }
    }

    public class MedicalBlApplyResponse
    { 
        public string Id { get; set; }
        public int ApplyStatus { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproverDept { get; set; }
        public string Approver { get; set; }
    }

}
