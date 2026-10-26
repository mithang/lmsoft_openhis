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
    /// đăng ký bệnh án
    ///</summary>
    [Tenant(DBEnum.MrQcDb)]
    [SugarTable("Mr_Apply", "MrApplyEntity")]
    public class MrApplyEntity: IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// khoa đăng ký
        /// </summary>
        [Required(ErrorMessage = "ApplyDept不可为空")]
        public string ApplyDept { get; set; }
        /// <summary>
        /// số nội trú
        /// </summary>
        [Required(ErrorMessage = "Zyh不可为空")]
        public string Zyh { get; set; }
        /// <summary>
        /// giường bệnh
        /// </summary>
        [Required(ErrorMessage = "Bed不可为空")]
        public string Bed { get; set; }
        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        [Required(ErrorMessage = "PatName不可为空")]
        public string PatName { get; set; }
        /// <summary>
        /// bác sĩ đăng ký
        /// </summary>
        [Required(ErrorMessage = "ApplyDoctor不可为空")]
        public string ApplyDoctor { get; set; }
        /// <summary>
        /// loại đăng ký 0：tạo bệnh án mới 1：sửa đổi bệnh án
        /// </summary>
        [Required(ErrorMessage = "ApplyType不可为空")]
        public int ApplyType { get; set; }
        /// <summary>
        /// tên bệnh án
        /// </summary>
        [Required(ErrorMessage = "MedicalName不可为空")]
        public string MedicalName { get; set; }
        /// <summary>
        /// thời gian hoàn thành kiểm soát chất lượng
        /// </summary>
        [Required(ErrorMessage = "CompletionDate不可为空")]
        public DateTime? CompletionDate { get; set; }
        /// <summary>
        /// thời gian đăng ký
        /// </summary>
        [Required(ErrorMessage = "ApplyDate不可为空")]
        public DateTime? ApplyDate { get; set; }
        /// <summary>
        /// thời gian hoàn thành đăng ký
        /// </summary>
        [Required(ErrorMessage = "ApplyCompletionDate不可为空")]
        public DateTime? ApplyCompletionDate { get; set; }
        /// <summary>
        /// nguyên nhân đăng ký
        /// </summary>
        [Required(ErrorMessage = "ApplyReason不可为空")]
        public string ApplyReason { get; set; }
        /// <summary>
        /// trạng thái đăng ký 0：chưa phê duyệt 1：đã phê duyệt
        /// </summary>
        [Required(ErrorMessage = "ApplyStatus不可为空")]
        public int ApplyStatus { get; set; }
        /// <summary>
        /// người phê duyệt
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// khoa phê duyệt
        /// </summary>
        public string ApproverDept { get; set; }
        /// <summary>
        /// ngày phê duyệt
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        /// <summary>
        /// ngày nhập viện
        /// </summary>
        public DateTime? Ryrq { get; set; }
        /// <summary>
        /// ngày xuất viện
        /// </summary>
        public DateTime? Cyrq { get; set; }
        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? Px { get; set; }

    }
}
