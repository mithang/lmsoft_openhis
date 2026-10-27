using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.MRQC
{
    ///<summary>
    /// 病历Đăng ký
    ///</summary>
    [Tenant(DBEnum.MrQcDb)]
    [SugarTable("Mr_Apply", "MrApplyEntity")]
    public class MrApplyEntity: IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
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
        /// Đăng ký状态 0：未批准 1：已审批
        /// </summary>
        [Required(ErrorMessage = "ApplyStatus không được để trống")]
        public int ApplyStatus { get; set; }
        /// <summary>
        /// 批准人
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// 批准科室
        /// </summary>
        public string ApproverDept { get; set; }
        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime? Ryrq { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? Cyrq { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Px { get; set; }

    }
}
