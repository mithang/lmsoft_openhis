using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.API
{
    public class MrApplyWritingVo
    {
        /// <summary>
        /// Họ tên
        /// </summary>
        public string PatName { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string Zyh { get; set; }
        /// <summary>
        /// Số giường
        /// </summary>
        public string Bed { get; set; }
        /// <summary>
        /// Khoa đề nghị
        /// </summary>
        public string ApplyDeptName { get; set; }
        /// <summary>
        /// Bác sĩ đề nghị
        /// </summary>
        public string ApplyDoctorName { get; set; }
        /// <summary>
        /// Mã khoa đề nghị
        /// </summary>
        public string ApplyDept { get; set; }
        /// <summary>
        /// Mã bác sĩ đề nghị
        /// </summary>
        public string ApplyDoctor { get; set; }
        /// <summary>
        /// Loại đề nghị: 1 Sửa bệnh án, 0 Tạo bệnh án mới
        /// </summary>
        public string ApplyType { get; set; }
        /// <summary>
        /// Tên bệnh án
        /// </summary>
        public string MedicalName { get; set; }
        /// <summary>
        /// Thời gian hoàn thành kiểm soát chất lượng
        /// </summary>
        public DateTime CompletionDate { get; set; }
        /// <summary>
        /// Thời gian hoàn thành đề nghị
        /// </summary>
        public DateTime ApplyCompletionDate { get; set; }
        /// <summary>
        /// Lý do đề nghị
        /// </summary>
        public string ApplyReason { get; set; }
        public DateTime? Ryrq { get; set; }
        public DateTime? Cyrq { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string bllx { get; set; }
    }
    public class ApplyWritingResp
    {
        public string Id { get; set; }
        public int ApplyStatus { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproverDept { get; set; }
        public string Approver { get; set; }
    }
}
