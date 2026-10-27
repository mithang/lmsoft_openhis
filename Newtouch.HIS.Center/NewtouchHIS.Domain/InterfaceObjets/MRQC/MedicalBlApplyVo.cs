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
        /// Đăng ký科室
        /// </summary>
        [Required(ErrorMessage = "ApplyDept không được để trống")]
        public string ApplyDept { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        [Required(ErrorMessage = "Zyh không được để trống")]
        public string Zyh { get; set; }
        /// <summary>
        /// 床位
        /// </summary>
        [Required(ErrorMessage = "Bed không được để trống")]
        public string Bed { get; set; }
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        [Required(ErrorMessage = "PatName không được để trống")]
        public string PatName { get; set; }
        /// <summary>
        /// Đăng ký医师
        /// </summary>
        [Required(ErrorMessage = "ApplyDoctor không được để trống")]
        public string ApplyDoctor { get; set; }
        /// <summary>
        /// Đăng ký类型 0：新建病历 1：修改病历
        /// </summary>
        [Required(ErrorMessage = "ApplyType không được để trống")]
        public int ApplyType { get; set; }
        /// <summary>
        /// 病历名称
        /// </summary>
        [Required(ErrorMessage = "MedicalName không được để trống")]
        public string MedicalName { get; set; }
        /// <summary>
        /// 质控完成时间
        /// </summary>
        [Required(ErrorMessage = "CompletionDate không được để trống")]
        public DateTime? CompletionDate { get; set; }
        /// <summary>
        /// Đăng ký时间
        /// </summary>
        [Required(ErrorMessage = "ApplyDate không được để trống")]
        public DateTime? ApplyDate { get; set; }
        /// <summary>
        /// Đăng ký完成时间
        /// </summary>
        [Required(ErrorMessage = "ApplyCompletionDate không được để trống")]
        public DateTime? ApplyCompletionDate { get; set; }
        /// <summary>
        /// Đăng ký原因
        /// </summary>
        [Required(ErrorMessage = "ApplyReason không được để trống")]
        public string ApplyReason { get; set; }

        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime? Ryrq { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? Cyrq { get; set; }
        /// <summary>
        /// 创建人
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
