using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.OutpatientManage
{
    [Table("mz_ghpb_schedule")]
    public class OutBookScheduleEntity : IEntity<OutBookScheduleEntity>
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
        /// Số lịch trực
        /// </summary>
        public string ghpbId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutDate { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ysgh { get; set; }
        /// <summary>
        /// Tên bác sĩ
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
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Khoảng thời gian
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
        /// Khoảng thời gian bắt đầu
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
        /// Có thể hẹn khám hay không
        /// </summary>
        public string IsBook { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ghlx { get; set; }
        /// <summary>
        /// Hạng mục khám chữa bệnh
        /// </summary>
        public string zlxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int weekdd { get; set; }
        /// <summary>
        /// Có hủy hay không
        /// </summary>
        public string IsCancel { get; set; }
        /// <summary>
        /// Lý do hủy
        /// </summary>
        public string CancelReason { get; set; }
        /// <summary>
        /// Thời gian hủy
        /// </summary>
        public string CancelTime { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Số hẹn khám
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
        /// Thời gian sửa đổi lần trước
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// Người sửa đổi lần trước
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Có ngừng khám hay không 0Khám bệnh bình thường  1Trạng thái ngừng khám
        /// </summary>
        public string istz { get; set; }
        /// <summary>
        /// Lý do ngừng khám
        /// </summary>
        public string tzyy { get; set; }
        /// <summary>
        /// Thời gian ngừng khám
        /// </summary>
        public string tzsj { get; set; }
    }
}
