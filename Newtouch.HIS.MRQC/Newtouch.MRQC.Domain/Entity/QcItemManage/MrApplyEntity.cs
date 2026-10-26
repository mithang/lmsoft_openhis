using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.Entity.QcItemManage
{
    /// <summary>
    /// đề nghị bệnh án kiểm soát chất lượng
    /// </summary>
    [Table("Mr_Apply")]
    public class MrApplyEntity : IEntity<MrApplyEntity>
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Khoa đề nghị
        /// </summary>
        public string ApplyDept { get; set; }
        /// <summary>
        /// giường bệnh
        /// </summary>
        public string Bed { get; set; }
        /// <summary>
        /// tên bệnh nhân
        /// </summary>
        public string PatName { get; set; }
        /// <summary>
        /// Bác sĩ đề nghị
        /// </summary>
        public string ApplyDoctor { get; set; }
        /// <summary>
        /// Loại đề nghị 0：tạo bệnh án mới 1：sửa bệnh án
        /// </summary>
        public int ApplyType { get; set; }
        /// <summary>
        /// Tên bệnh án
        /// </summary>
        public string MedicalName { get; set; }
        /// <summary>
        /// thời gian hoàn thành kiểm soát chất lượng
        /// </summary>
        public DateTime CompletionDate { get; set; }
        /// <summary>
        /// Thời gian đề nghị
        /// </summary>
        public DateTime ApplyDate { get; set; }
        /// <summary>
        /// Thời gian hoàn thành đề nghị
        /// </summary>
        public DateTime ApplyCompletionDate { get; set; }
        /// <summary>
        /// Lý do đề nghị
        /// </summary>
        public string ApplyReason { get; set; }
        /// <summary>
        /// Trạng thái đề nghị 0：chưa phê duyệt 1：Đã phê duyệt
        /// </summary>
        public int ApplyStatus { get; set; }
        /// <summary>
        /// Người phê duyệt
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// Khoa phê duyệt
        /// </summary>
        public string ApproverDept { get; set; }
        /// <summary>
        /// Ngày phê duyệt
        /// </summary>
        public string ApprovalDate { get; set; }
        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        public DateTime? Ryrq { get; set; }
        /// <summary>
        /// Ngày xuất viện
        /// </summary>
        public DateTime? Cyrq { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? Px { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
