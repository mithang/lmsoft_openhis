using System;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    /// <summary>
    /// Thông tin truyền dịch
    /// </summary>
    public class MzsyypxxVO
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        public long Id { get; set; }
        public string syzxId { get; set; }
        public string cfId { get; set; }

        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số thẻ khám bệnh của bệnh nhân
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốcID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết tính toán
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// Số giường/Số ghế
        /// </summary>
        public string seatNum { get; set; }

        /// <summary>
        /// Thời gian thu phí  Thời gian quyết toán
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
        /// Liều dùng+Đơn vị liều dùng
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// Số lượng+Đơn vị số lượng
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Liều lượng+Đơn vị liều lượng
        /// </summary>
        public string jlStr { get; set; }
        public string yfcode { get; set; }
        public string yfmc { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
        public string groupNo { get; set; }

        /// <summary>
        /// Mã nhân viên dược sĩ pha thuốc
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// Tên dược sĩ pha thuốc
        /// </summary>
        public string dispenserName { get; set; }

        /// <summary>
        /// Mã nhân viên người thực hiện
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// Tên người thực hiện
        /// </summary>
        public string executorName { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Thời gian bắt đầu truyền dịch
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// Thời gian kết thúc truyền dịch
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }
        public int zcs { get; set; }
        public int yzxcs { get; set; }
        /// <summary>
        /// Kết quả thử da
        /// </summary>
        public string psjg { get; set; }
        /// <summary>
        /// Cờ thử da 0:Không 1:Có
        /// </summary>
        public string ispsbz { get; set; }
        /// <summary>
        /// Cờ lưu lại theo dõi
        /// </summary>

        public string islgbz { get; set; }
    }
}
