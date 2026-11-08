using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class OutBookScheduleVO
    {
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// ScheduId
        /// </summary>
        public Decimal ScheduId { get; set; }
        /// <summary>
        /// Xếp lịch Số hiệu
        /// </summary>
        public string ghpbId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutDate { get; set; }
        /// <summary>
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }
        /// <summary>
        /// Bác sĩ Họ tên
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string czks { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string czksmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// dấu đề
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Thời gian đoạn
        /// </summary>
        public string Period { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PeriodDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// Thời gian bắt đầu đoạn
        /// </summary>
        public string PeriodStart { get; set; }
        /// <summary>
        /// Đoạn thời gian kết thúc
        /// </summary>
        public string PeriodEnd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal RegFee { get; set; }
        /// <summary>
        /// là Không có thể đặt hẹn
        /// </summary>
        public string IsBook { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ghlx { get; set; }
        /// <summary>
        /// Hạng mục chẩn trị
        /// </summary>
        public string zlxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int weekdd { get; set; }
        /// <summary>
        /// là Không Hủy
        /// </summary>
        public string IsCancel { get; set; }
        /// <summary>
        /// Hủy nguyên nhân
        /// </summary>
        public string CancelReason { get; set; }
        /// <summary>
        /// Hủy Thời gian
        /// </summary>
        public string CancelTime { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// đặt hẹn số
        /// </summary>
        public int YYNum { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// trên lần Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// trên lần Sửa người
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// là Không Dừng khám 0Bình thường ra khám  1Dừng khám Trạng thái
        /// </summary>
        public string istz { get; set; }
        /// <summary>
        /// Dừng khám nguyên nhân
        /// </summary>
        public string tzyy { get; set; }
        /// <summary>
        /// Dừng khám Thời gian
        /// </summary>
        public string tzsj { get; set; }
    }
}
