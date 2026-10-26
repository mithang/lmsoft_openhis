using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.DTO.OutputDto
{
    [XmlRoot("YBJKDATA")]
    public class QhdPrescriptionDTO
    {
        public REQUESTDATA REQUESTDATA { get; set; }
    }

    public class REQUESTDATA
    {
        /// <summary>
        /// Mã cơ sở y tế chỉ định
        /// </summary>
        public string AKB020 { get; set; }
        /// <summary>
        /// Tên cơ sở y tế chỉ định
        /// </summary>
        public string AKB021 { get; set; }
        /// <summary>
        /// Mã giao dịch
        /// </summary>
        public string MSGNO { get; set; }
        /// <summary>
        /// Số thứ tự giao dịch của bên gửi
        /// </summary>
        public string MSGID { get; set; }
        /// <summary>
        /// Mã ủy quyền
        /// </summary>
        public string GRANTID { get; set; }
        /// <summary>
        /// Số nhân viên thao tác
        /// </summary>
        public string OPERID { get; set; }
        /// <summary>
        /// Họ tên nhân viên thao tác
        /// </summary>
        public string OPERNAME { get; set; }
        public KC21XML KC21XML { get; set; }
        public List<KC22ROW> KC22XML { get; set; }
        public List<KC33ROW> KC33XML { get; set; }
    }
    public class KC21XML
    {
        /// <summary>
        /// Số thứ tự khám bệnh
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// Loại hình y tế
        /// </summary>
        public string AKA130 { get; set; }
        /// <summary>
        /// Ngày ngoại trúYYYYMMDDHH24MISS
        /// </summary>
        public string AKC192 { get; set; }
        /// <summary>
        /// Mã bệnh chẩn đoán nhập viện/Mã bệnh chẩn đoán ngoại trú
        /// </summary>
        public string AKC193 { get; set; }
        /// <summary>
        /// Tên bệnh chẩn đoán nhập viện/Tên bệnh chẩn đoán ngoại trú
        /// </summary>
        public string ZKC274 { get; set; }
        /// <summary>
        /// Ngày xuất viện YYYYMMDDHH24MISS
        /// </summary>
        public string AKC194 { get; set; }
        /// <summary>
        /// Lý do xuất viện
        /// </summary>
        public string AKC195 { get; set; }
        /// <summary>
        /// Mã bệnh chẩn đoán xuất viện
        /// </summary>
        public string AKC196 { get; set; }
        /// <summary>
        /// Tên bệnh chẩn đoán xuất viện
        /// </summary>
        public string ZKC275 { get; set; }
        /// <summary>
        /// Cờ bệnh tâm thần nặng
        /// </summary>
        public string ZKC285 { get; set; }
        /// <summary>
        /// Số phiếu đăng ký khám nội bộ bệnh viện
        /// </summary>
        public string ZKC286 { get; set; }
        /// <summary>
        /// Số khoa
        /// </summary>
        public string BKF040 { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ZKC272 { get; set; }
        /// <summary>
        /// Mã bác sĩ điều trị chính
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// Họ tên bác sĩ điều trị chính
        /// </summary>
        public string ZKC271 { get; set; }
        /// <summary>
        /// Số BHYT
        /// </summary>
        public string AAC001 { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CKC502 { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string AAC003 { get; set; }
        /// <summary>
        /// Số CMND công dân
        /// </summary>
        public string AAE135 { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string AAC004 { get; set; }
        /// <summary>
        /// Tuổi
        /// </summary>
        public decimal BAE450 { get; set; }
        /// <summary>
        /// Người xử lý
        /// </summary>
        public string AAE011 { get; set; }
        /// <summary>
        /// Thời gian xử lýYYYYMMDDHH24MISS
        /// </summary>
        public string AAE036 { get; set; }
    }

    public class KC22ROW
    {
        /// <summary>
        /// Số thứ tự khám bệnh
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string AKC220 { get; set; }
        /// <summary>
        /// Ngày kê đơn YYYYMMDDHH24MISS
        /// </summary>
        public string AKC221 { get; set; }
        /// <summary>
        /// Thuốc BHYT/Mã hạng mục
        /// </summary>
        public string AKE001 { get; set; }
        /// <summary>
        /// Thuốc BHYT/Tên hạng mục
        /// </summary>
        public string AKE002 { get; set; }
        /// <summary>
        /// Mã hạng mục thu phí bệnh viện
        /// </summary>
        public string AKC515 { get; set; }
        /// <summary>
        /// Tên hạng mục thu phí bệnh viện
        /// </summary>
        public string AKC223 { get; set; }
        /// <summary>
        /// Loại hạng mục1:Thuốc 2:Hạng mục khám chữa bệnh 3:Cơ sở dịch vụ
        /// </summary>
        public string AKC224 { get; set; }
        /// <summary>
        /// Loại chi phí
        /// </summary>
        public string AKA063 { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal AKC225 { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal AKC226 { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal AKC227 { get; set; }
        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string AKA070 { get; set; }
        /// <summary>
        /// Đơn vị liều lượng thuốc
        /// </summary>
        public string CKC132 { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string AKA074 { get; set; }
        /// <summary>
        /// Liều dùng mỗi lần
        /// </summary>
        public decimal? AKA071 { get; set; }
        /// <summary>
        /// Liều tối đa mỗi ngày của thuốc
        /// </summary>
        public decimal? AKA075 { get; set; }
        /// <summary>
        /// Tần suất sử dụng
        /// </summary>
        public string AKA072 { get; set; }
        /// <summary>
        /// Cách dùng
        /// </summary>
        public string AKA073 { get; set; }
        /// <summary>
        /// Đơn vị bán
        /// </summary>
        public string ZKA101 { get; set; }
        /// <summary>
        /// Cờ đơn vị hiện tại
        /// </summary>
        public string AKA067 { get; set; }
        /// <summary>
        /// Số ngày thực hiện
        /// </summary>
        public decimal AKC229 { get; set; }
        /// <summary>
        /// Cờ đơn thuốc thảo dược đơn/phức
        /// </summary>
        public string BKC127 { get; set; }
        /// <summary>
        /// Mã bác sĩ kê đơn
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// Họ tên bác sĩ kê đơn
        /// </summary>
        public string ZKC271 { get; set; }
        /// <summary>
        /// Người xử lý
        /// </summary>
        public string AAE011 { get; set; }
        /// <summary>
        /// Thời gian xử lý YYYYMMDDHH24MISS
        /// </summary>
        public string AAE036 { get; set; }
    }

    public class KC33ROW
    {
        /// <summary>
        /// Số thứ tự khám bệnh
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// Thứ tự chẩn đoán
        /// </summary>
        public int BKE150 { get; set; }
        /// <summary>
        /// Có phải chẩn đoán chính hay không
        /// </summary>
        public string CKC305 { get; set; }
        /// <summary>
        /// Ngày chẩn đoánYYYYMMDDHH24MISS
        /// </summary>
        public string CKC304 { get; set; }
        /// <summary>
        /// Mã chẩn đoán
        /// </summary>
        public string CKC302 { get; set; }
        /// <summary>
        /// Tên chẩn đoán
        /// </summary>
        public string CKC303 { get; set; }
        /// <summary>
        /// Mã bác sĩ chẩn đoán
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// Họ tên bác sĩ chẩn đoán
        /// </summary>
        public string ZKC271 { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string AAE013 { get; set; }
    }
}
