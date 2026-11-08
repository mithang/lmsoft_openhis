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
        /// định điểm y tế Cơ sở Mã
        /// </summary>
        public string AKB020 { get; set; }
        /// <summary>
        /// định điểm y tế Tên cơ sở
        /// </summary>
        public string AKB021 { get; set; }
        /// <summary>
        /// giao dịch Mã
        /// </summary>
        public string MSGNO { get; set; }
        /// <summary>
        /// gửi phương giao dịch dòng chảy số
        /// </summary>
        public string MSGID { get; set; }
        /// <summary>
        /// ủy quyền mã
        /// </summary>
        public string GRANTID { get; set; }
        /// <summary>
        /// Thao tác viên Số hiệu
        /// </summary>
        public string OPERID { get; set; }
        /// <summary>
        /// Thao tác viên Họ tên
        /// </summary>
        public string OPERNAME { get; set; }
        public KC21XML KC21XML { get; set; }
        public List<KC22ROW> KC22XML { get; set; }
        public List<KC33ROW> KC33XML { get; set; }
    }
    public class KC21XML
    {
        /// <summary>
        /// Khám bệnh dòng chảy số
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// y tế Loại
        /// </summary>
        public string AKA130 { get; set; }
        /// <summary>
        /// Ngoại trúNgàyYYYYMMDDHH24MISS
        /// </summary>
        public string AKC192 { get; set; }
        /// <summary>
        /// Chẩn đoán nhập viện bệnh bệnh Mã/Ngoại trú Chẩn đoánbệnhbệnh Mã
        /// </summary>
        public string AKC193 { get; set; }
        /// <summary>
        /// Chẩn đoán nhập viện bệnh bệnh Tên/Ngoại trú Chẩn đoánbệnhbệnh Tên
        /// </summary>
        public string ZKC274 { get; set; }
        /// <summary>
        /// Ngày xuất viện YYYYMMDDHH24MISS
        /// </summary>
        public string AKC194 { get; set; }
        /// <summary>
        /// Xuất viện nguyên nhân
        /// </summary>
        public string AKC195 { get; set; }
        /// <summary>
        /// Xuất viện Chẩn đoánbệnhbệnh Mã
        /// </summary>
        public string AKC196 { get; set; }
        /// <summary>
        /// Xuất viện Chẩn đoánbệnhbệnh Tên
        /// </summary>
        public string ZKC275 { get; set; }
        /// <summary>
        /// Cờ bệnh tâm thần nặng
        /// </summary>
        public string ZKC285 { get; set; }
        /// <summary>
        /// yTrong viện bộ đăng ký khám phiếu số
        /// </summary>
        public string ZKC286 { get; set; }
        /// <summary>
        /// Số hiệu khoa
        /// </summary>
        public string BKF040 { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ZKC272 { get; set; }
        /// <summary>
        /// Bác sĩ điều trị Mã
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// Bác sĩ điều trị Họ tên
        /// </summary>
        public string ZKC271 { get; set; }
        /// <summary>
        /// Bảo hiểm y tế Số hiệu
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
        /// công dân thân phần số mã
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
        /// qua làm người
        /// </summary>
        public string AAE011 { get; set; }
        /// <summary>
        /// qua làm Thời gianYYYYMMDDHH24MISS
        /// </summary>
        public string AAE036 { get; set; }
    }

    public class KC22ROW
    {
        /// <summary>
        /// Khám bệnh dòng chảy số
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string AKC220 { get; set; }
        /// <summary>
        /// Đơn thuốc Ngày YYYYMMDDHH24MISS
        /// </summary>
        public string AKC221 { get; set; }
        /// <summary>
        /// Bảo hiểm y tế Thuốc/Mã hạng mục
        /// </summary>
        public string AKE001 { get; set; }
        /// <summary>
        /// Bảo hiểm y tế Thuốc/Tên mục
        /// </summary>
        public string AKE002 { get; set; }
        /// <summary>
        /// y viện Mục thu phí Mã
        /// </summary>
        public string AKC515 { get; set; }
        /// <summary>
        /// y viện Tên hạng mục thu phí
        /// </summary>
        public string AKC223 { get; set; }
        /// <summary>
        /// Mục Loại1:Thuốc 2:Hạng mục chẩn trị 3:phục vụ thiết thi
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
        /// Thuốc Đơn vị liều
        /// </summary>
        public string CKC132 { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string AKA074 { get; set; }
        /// <summary>
        /// mỗi lần dùng lượng
        /// </summary>
        public decimal? AKA071 { get; set; }
        /// <summary>
        /// Thuốc mỗi ngày nhất lớn Liều lượng
        /// </summary>
        public decimal? AKA075 { get; set; }
        /// <summary>
        /// Sử dụng Tần suất
        /// </summary>
        public string AKA072 { get; set; }
        /// <summary>
        /// Cách dùng
        /// </summary>
        public string AKA073 { get; set; }
        /// <summary>
        /// bán bán Đơn vị
        /// </summary>
        public string ZKA101 { get; set; }
        /// <summary>
        /// hiện tại Đơn vị dấu ghi
        /// </summary>
        public string AKA067 { get; set; }
        /// <summary>
        /// Thực hiện Số ngày
        /// </summary>
        public decimal AKC229 { get; set; }
        /// <summary>
        /// thảo dược phiếu lại phương Cờ
        /// </summary>
        public string BKC127 { get; set; }
        /// <summary>
        /// Đơn thuốc Bác sĩ Mã
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// Đơn thuốc Bác sĩ Họ tên
        /// </summary>
        public string ZKC271 { get; set; }
        /// <summary>
        /// qua làm người
        /// </summary>
        public string AAE011 { get; set; }
        /// <summary>
        /// qua làm Thời gian YYYYMMDDHH24MISS
        /// </summary>
        public string AAE036 { get; set; }
    }

    public class KC33ROW
    {
        /// <summary>
        /// Khám bệnh dòng chảy số
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// Chẩn đoánthuậnthứ tự
        /// </summary>
        public int BKE150 { get; set; }
        /// <summary>
        /// là Không chính Chẩn đoán
        /// </summary>
        public string CKC305 { get; set; }
        /// <summary>
        /// Chẩn đoánNgàyYYYYMMDDHH24MISS
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
        /// Chẩn đoán Bác sĩ Mã
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// Chẩn đoán Bác sĩ Họ tên
        /// </summary>
        public string ZKC271 { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string AAE013 { get; set; }
    }
}
