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
        /// Khoa xin
        /// </summary>
        public string ApplyDeptName { get; set; }
        /// <summary>
        /// Bác sĩ xin
        /// </summary>
        public string ApplyDoctorName { get; set; }
        /// <summary>
        /// Mã khoa yêu cầu
        /// </summary>
        public string ApplyDept { get; set; }
        /// <summary>
        /// Mã bác sĩ yêu cầu
        /// </summary>
        public string ApplyDoctor { get; set; }
        /// <summary>
        /// Loại đơn 1：Sửa bệnh án 0：Tạo mới bệnh án
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
        /// Thời gian hoàn thành đơn
        /// </summary>
        public DateTime ApplyCompletionDate { get; set; }
        /// <summary>
        /// Lý do xin
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
