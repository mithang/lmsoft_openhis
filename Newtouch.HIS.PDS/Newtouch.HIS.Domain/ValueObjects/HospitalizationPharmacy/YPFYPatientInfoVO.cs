using System;


namespace Newtouch.HIS.Domain.ValueObjects
{
    public class YPFYPatientInfoVO
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// y lệnh số
        /// </summary>
        public string yzh { get; set; }

        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// lĩnh thuốc số thứ tự
        /// </summary>
        public long lyxh { get; set; }

        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// số nội trú
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// nhóm lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// số lượng+đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// uống, truyền tĩnh mạch...
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// thời gian thực hiện: 04,06,08...
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// và thực hiện số lượng tương ứng
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// liều lượng
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// đơn vị liều lượng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// đơn vị phát thực tế
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// bác sĩ công số
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// ngày bắt đầu
        /// </summary>
        public DateTime ksrq { get; set; }

        /// <summary>
        /// ngày kết thúc
        /// </summary>
        public DateTime jsrq { get; set; }

        /// <summary>
        /// nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 1: tạm thời; 2: dài hạn
        /// </summary>
        public string yzxzmc { get; set; }

        /// <summary>
        /// lời dặn
        /// </summary>
        public string yzbz { get; set; }

        /// <summary>
        /// tần suất
        /// </summary>
        public int zxsl { get; set; }

        /// <summary>
        /// đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// tỷ lệ tự chi trả
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// tính chất tự chi trả
        /// </summary>
        public char zfxz { get; set; }

        /// <summary>
        /// tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// khu bệnh mã
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// khu bệnh tên
        /// </summary>
        public string bqmc { get; set; }

        /// <summary>
        /// giường
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// cờ phát thuốc  0: chưa phát; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// loại thao tác tên  1-phát thuốc 2-đã trả
        /// </summary>
        public string czlxbz { get; set; }

        /// <summary>
        /// người đề nghị thực hiện y lệnh
        /// </summary>
        public string yzzxsqr { get; set; }

        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// quy cách
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// tần suất
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }
        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }
        /// <summary>
        /// xếp thuốc nhân viên
        /// </summary>
        public string pyry { get; set; }
        /// <summary>
        /// xếp thuốc ngày
        /// </summary>
        public DateTime? pyrq { get; set; }
        /// <summary>
        /// dạng bào chế
        /// </summary>
        public string jxmc { get; set; }
        /// <summary>
        /// đề nghị trả thuốc cờ
        /// </summary>
        public string sqtyzt { get; set; }
        /// <summary>
        /// số nhóm
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// đơn giá+đơn vị
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// thang số
        /// </summary>
        public decimal ts { get; set; }
        /// <summary>
        /// mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
    }

    /// <summary>
    /// bệnh nhân thông tin
    /// </summary>
    public class HospitalizationPatientInfo
    {

        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// giường
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// số nội trú
        /// </summary>
        public string zyh { get; set; }
    }

    /// <summary>
    /// nội trú y lệnh thông tin
    /// </summary>
    public class HospitalizationRpDetail : HospitalizationPatientInfo
    {
        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// số nhóm
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// tính chất y lệnh tên
        /// </summary>
        public string yzxzmc { get; set; }

        /// <summary>
        /// tên nhà máy dược
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 治理方法
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// dạng bào chế tên
        /// </summary>
        public string jxmc { get; set; }

        /// <summary>
        /// tần suất tên
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// liều lượng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// đơn vị liều lượng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// cờ phát thuốc  0: chưa phát; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string fybz { get; set; }
    }

    /// <summary>
    /// chi tiết phát thuốc nội trú
    /// </summary>
    public class HospitalizationDispenseDetail : HospitalizationRpDetail
    {
        public Int64 Id { get; set; }
        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lượng+đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// loại thao tác   trả thuốc/phát thuốc
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// đơn vị liều lượng
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// đơn giá+đơn vị
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// thời gian thao tác
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người thao tác
        /// </summary>
        public string CreatorCode { get; set; }

        public string zytyapplyno { get; set; }

    }

    /// <summary>
    /// chi tiết phát thuốc nội trú
    /// </summary>
    public class HospitalizationDispenseDetailV2 : HospitalizationRpDetail
    {
        public Int64 Id { get; set; }
        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lượng+đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// loại thao tác   trả thuốc/phát thuốc
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// đơn vị liều lượng
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// đơn giá+đơn vị
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// thời gian thao tác
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người thao tác
        /// </summary>
        public string CreatorCode { get; set; }

        public Int64? lyxh { get; set; }

    }

    public class FybdComboboxList
    {
        public string operatetime { get; set; }
    }

    /// <summary>
    /// trả thuốc nội trú
    /// </summary>
    public class HospitalizationReturnDispenseDetail : HospitalizationRpDetail
    {

        /// <summary>
        /// số phiếu đề nghị trả thuốc
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// chi tiết đề nghị trả thuốc ID
        /// </summary>
        public string rdbdId { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lượng trả thuốc  mặc định 0
        /// </summary>
        public int tysl { get; set; }

        /// <summary>
        /// đề nghị trả thuốc số (kèm đơn vị)  
        /// </summary>
        public string tyslStr { get; set; }

        /// <summary>
        /// số lượng trả tối đa số lượng đơn vị bộ phận
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// số lượng trả tối đa số lượng đơn vị nhỏ nhất
        /// </summary>
        public int zxdwsl { get; set; }

        /// <summary>
        /// đơn vị bộ phận
        /// </summary>
        public string bmdw { get; set; }

        /// <summary>
        /// số lượng+đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// đơn vị liều lượng
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// đơn giá+đơn vị
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// tổ chức
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// thời gian phát thuốc
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
