using System;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    /// <summary>
    /// truyền dịch thông tin
    /// </summary>
    public class MzsyypxxVO
    {
        /// <summary>
        /// Khóa chính ID
        /// </summary>
        public long Id { get; set; }
        public string syzxId { get; set; }
        public string cfId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Bệnh nhân Khám bệnh Số thẻ
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// Hóa đơn số
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Đơn thuốc chi tiết ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Tính toán chi tiết trong mã
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// Số giường/ghế số
        /// </summary>
        public string seatNum { get; set; }

        /// <summary>
        /// Thu phí Thời gian  quyết toán Thời gian
        /// </summary>
        public DateTime sfsj { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// dùng lượng+dùng lượng Đơn vị
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// Số lượng+Số lượng Đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Liều lượng+Đơn vị liều
        /// </summary>
        public string jlStr { get; set; }
        public string yfcode { get; set; }
        public string yfmc { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
        public string groupNo { get; set; }

        /// <summary>
        /// phối Dược sĩ Mã nhân viên
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// phối Dược sĩ Tên
        /// </summary>
        public string dispenserName { get; set; }

        /// <summary>
        /// Thực hiện người Mã nhân viên
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// Thực hiện người Tên
        /// </summary>
        public string executorName { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// truyền dịch Thời gian bắt đầu
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// truyền dịch Thời gian kết thúc
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạo người
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa người
        /// </summary>
        public string LastModifierCode { get; set; }
        public int zcs { get; set; }
        public int yzxcs { get; set; }
        /// <summary>
        /// da thử kết nếu
        /// </summary>
        public string psjg { get; set; }
        /// <summary>
        /// da thử Cờ 0:Không 1:là
        /// </summary>
        public string ispsbz { get; set; }
        /// <summary>
        /// giữ xem Cờ
        /// </summary>

        public string islgbz { get; set; }
    }
}
