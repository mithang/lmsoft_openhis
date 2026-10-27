using System;


namespace Newtouch.HIS.Domain.ValueObjects
{
    public class YPFYPatientInfoVO
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số y lệnh
        /// </summary>
        public string yzh { get; set; }

        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Số thứ tự nhận thuốc
        /// </summary>
        public long lyxh { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Số lượng+Đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Uống、Truyền tĩnh mạch...
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// Thời gian thực hiện：04,06,08...
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// tương ứng với số lượng thực hiện
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// Liều dùng
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Đơn vị phát thực tế
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// Số hiệu công tác bác sĩ
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime ksrq { get; set; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime jsrq { get; set; }

        /// <summary>
        /// Nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 1：Tạm thời；2：Dài hạn
        /// </summary>
        public string yzxzmc { get; set; }

        /// <summary>
        /// Dặn dò
        /// </summary>
        public string yzbz { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public int zxsl { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Tỷ lệ tự trả
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// Tính chất tự trả
        /// </summary>
        public char zfxz { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Mã khu bệnh
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// Tên khu bệnh
        /// </summary>
        public string bqmc { get; set; }

        /// <summary>
        /// Giường bệnh
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// Cờ phát thuốc  0：Chưa phát；1：Đã xếp；2：Đã phát；3：Đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// Tên loại thao tác  1-Phát thuốc 2-Đã trả
        /// </summary>
        public string czlxbz { get; set; }

        /// <summary>
        /// Người xin thực hiện y lệnh
        /// </summary>
        public string yzzxsqr { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// Tần suất
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }
        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }
        /// <summary>
        /// Nhân viên xếp thuốc
        /// </summary>
        public string pyry { get; set; }
        /// <summary>
        /// Ngày xếp thuốc
        /// </summary>
        public DateTime? pyrq { get; set; }
        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string jxmc { get; set; }
        /// <summary>
        /// Cờ xin trả thuốc
        /// </summary>
        public string sqtyzt { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// Đơn giá+Đơn vị
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// Số miếng dán
        /// </summary>
        public decimal ts { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
    }

    /// <summary>
    /// Thông tin bệnh nhân
    /// </summary>
    public class HospitalizationPatientInfo
    {

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Giường bệnh
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }
    }

    /// <summary>
    /// Thông tin y lệnh nội trú
    /// </summary>
    public class HospitalizationRpDetail : HospitalizationPatientInfo
    {
        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string zxId { get; set; }

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
        /// Số nhóm
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// Tên tính chất y lệnh
        /// </summary>
        public string yzxzmc { get; set; }

        /// <summary>
        /// Tên hãng thuốc
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Phương pháp quản lý
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Tên dạng bào chế
        /// </summary>
        public string jxmc { get; set; }

        /// <summary>
        /// Tên tần suất
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// Liều dùng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Cờ phát thuốc  0：Chưa phát；1：Đã xếp；2：Đã phát；3：Đã trả
        /// </summary>
        public string fybz { get; set; }
    }

    /// <summary>
    /// Chi tiết phát thuốc nội trú
    /// </summary>
    public class HospitalizationDispenseDetail : HospitalizationRpDetail
    {
        public Int64 Id { get; set; }
        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lượng+Đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Loại thao tác   Trả thuốc/Phát thuốc
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// Đơn giá+Đơn vị
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người thao tác
        /// </summary>
        public string CreatorCode { get; set; }

        public string zytyapplyno { get; set; }

    }

    /// <summary>
    /// Chi tiết phát thuốc nội trú
    /// </summary>
    public class HospitalizationDispenseDetailV2 : HospitalizationRpDetail
    {
        public Int64 Id { get; set; }
        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lượng+Đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Loại thao tác   Trả thuốc/Phát thuốc
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// Đơn giá+Đơn vị
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người thao tác
        /// </summary>
        public string CreatorCode { get; set; }

        public Int64? lyxh { get; set; }

    }

    public class FybdComboboxList
    {
        public string operatetime { get; set; }
    }

    /// <summary>
    /// Trả thuốc nội trú
    /// </summary>
    public class HospitalizationReturnDispenseDetail : HospitalizationRpDetail
    {

        /// <summary>
        /// Số đơn xin trả thuốc
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// Chi tiết đơn xin trả thuốcID
        /// </summary>
        public string rdbdId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lượng trả thuốc  Mặc định0
        /// </summary>
        public int tysl { get; set; }

        /// <summary>
        /// Số lượng xin trả thuốc（kèm đơn vị）  
        /// </summary>
        public string tyslStr { get; set; }

        /// <summary>
        /// Số lượng có thể trả tối đa Số lượng đơn vị bộ phận
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Số lượng có thể trả tối đa Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int zxdwsl { get; set; }

        /// <summary>
        /// Đơn vị bộ phận
        /// </summary>
        public string bmdw { get; set; }

        /// <summary>
        /// Số lượng+Đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// Đơn giá+Đơn vị
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Thời gian phát thuốc
        /// </summary>
        public DateTime? fysj { get; set; }
        
        public string zytyapplyno { get; set; }
    }

    public class KSBYSQDInfoVO
    {
        public string djh { get; set; }
        public string bqbm { get; set; }
        public string bqmc { get; set; }
        public string thyy { get; set; }
    }
}
