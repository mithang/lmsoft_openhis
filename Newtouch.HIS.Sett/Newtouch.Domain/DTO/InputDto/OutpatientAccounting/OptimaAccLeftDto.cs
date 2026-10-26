
using System;

namespace Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting
{
    public class OptimaAccLeftDto
    {
        public string patientId { get; set; }
        public string admsNum { get; set; }
        public string patientName { get; set; }
        public string patientType { get; set; }
        /// <summary>
        /// Cờ chưa xác nhận
        /// </summary>
        public int? wqrzt { get; set; }
        /// <summary>
        /// Dữ liệu đánh dấu hiển thị trạng thái đang nằm viện
        /// </summary>
        public int? hidden { get; set; }
    }

    public class ChargeLeftDto
    {
        public string cfnm { get; set; }
        public string cfh { get; set; }
        public decimal zje { get; set; }
        public string ksmc { get; set; }
        public string ysmc { get; set; }
        public string cflx { get; set; }
        public string cflxmc { get; set; }
        public string klsj { get; set; }
    }

    public class ChargeRightDto
    {
        public string cfh { get; set; }
        public int? cfnm { get; set; }
        public string sfxmmc { get; set; }
        public string sfxmCode { get; set; }
        public string sfdlCode { get; set; }
        public string sfdlmc { get; set; }
        public string dlmc { get; set; }
        public int sl { get; set; }
        public string dw { get; set; }
        public decimal dj { get; set; }
        public decimal zje { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }
        /// <summary>
        /// Liều điều trị mỗi lần
        /// </summary>
        public int? dczll { get; set; }
        /// <summary>
        /// Số lần thực hiện
        /// </summary>
        public int? zxcs { get; set; }

        /// <summary>
        /// Mã nội bộ hạng mục(Chưa liên kết đơn thuốc)
        /// </summary>
        public int? xmnm { get; set; }

        /// <summary>
        /// Khóa chính chi tiết thuốc
        /// </summary>
        public int? cfmxId { get; set; }

        /// <summary>
        /// Loại(1Thuốc 2Hạng mục)
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Mã bác sĩ
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        public DateTime klsj { get; set; }

        /// <summary>
        /// Mã BHYT
        /// </summary>
        public string ybdm { get; set; }
        /// <summary>
        /// Cờ BHYT 1:BHYT  0Tự chi trả
        /// </summary>
        public string ybbz { get; set; }

        /// <summary>
        /// Chi tiếtID cfmxIdhoặcxmnm
        /// </summary>
        public int mxId { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Mã BHYT Bảo hiểm nông thôn mới
        /// </summary>
        public string xnhybdm { get; set; }
        public int? cflx { get; set; }
        
        ///Nhắc nhở giới hạn dùng thuốc BHYT
        public string yybz { get; set; }
    }

    public class GuiAnChargeRightDto : ChargeRightDto
    {
        public int? jsmxnm { get; set; }

    }
    public class TbbzmlDto
    {
        /// <summary>
        /// Mã danh mục loại bệnh
        /// </summary>
        public string mtbbzmldm { get; set; }
        /// <summary>
        /// Tên loại lớn danh mục loại bệnh
        /// </summary>
        public string mtbbzdlmc { get; set; }
        /// <summary>
        /// Tên phân loại loại bệnh
        /// </summary>
        public string mtbbzflmc { get; set; }
    }
}
