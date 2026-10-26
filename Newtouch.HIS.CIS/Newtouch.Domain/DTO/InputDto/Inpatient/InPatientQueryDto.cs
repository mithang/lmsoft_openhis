using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    public class InPatientQueryDto
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string nl { get; set; }
        public string nlshow { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }
        public string blh { get; set; }
        public string zzdCode { get; set; }
        public string zzdicd10 { get; set; }
        public string zzdmc { get; set; }
        public DateTime? csny { get; set; }
        public DateTime ryrq { get; set; }
        public DateTime? cyrq { get; set; }
        public string bqCode { get; set; }
        public string bqmc { get; set; }
        public int zybz { get; set; }
        public string zybzmc { get; set; }
        public string UpdateTime { get; set; }
        public string sexValue { get; set; }
        public string ys { get; set; }
        public string ysxm { get; set; }
    }

    public class InPatientDetailQueryDto: InPatientQueryDto
    {
        public string OrganizeId { get; set; }
        public string py { get; set; }
        public string wb { get; set; }
        public string zjlx { get; set; }
        public string zjlxValue { get; set; }
        public string zjh { get; set; }
        public string idCardNo { get; set; }
        public string sex { get; set; }
        public string ks { get; set; }
        public string ksmc { get; set; }
        
        public string kh { get; set; }
        public string CardType { get; set; }
        public string CardTypeName { get; set; }
        public string contPerName { get; set; }
        public string contPerPhoneNum { get; set; }
        public string contPerRel { get; set; }
        public string contPerRelValue { get; set; }
        public DateTime rqrq { get; set; }
        public string wzjb { get; set; }
        public string BedCode { get; set; }
        public string cwmc { get; set; }
        public string ryfs { get; set; }
        public string grbh { get; set; }
    }
    public class InAreapatientInfo:InPatientDetailQueryDto
    {
        public string cyzdmc { get; set; }    }
    public class OutAreapatientInfo : InPatientDetailQueryDto
    {
        public DateTime? cqrq { get; set; }
    }

    public class RequestMsgDto
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public string data { get; set; }
    }

    public class RequestOrderExecMsgDto
    {
        /// <summary>
        /// Có thành công hay không true:Tất cả thành công/Một phần thành công  false:Tất cả thất bại/Xác thực thất bại
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Mã trạng thái Tham chiếu enumResultCode
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Mô tả lỗi
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// Thông tin trả về
        /// </summary>
        public dynamic Data { get; set; }
    }

    /// <summary>
    /// Chi tiết y lệnh   DataNội dung trả về,Trình bày dưới dạng tập hợp
    /// </summary>
    //public class YzDetail
    //{
    //    /// <summary>
    //    /// Y lệnhID
    //    /// </summary>
    //    public string yzId { get; set; }

    //    /// <summary>
    //    /// Số thứ tự lĩnh thuốc
    //    /// </summary>
    //    public long lyxh { get; set; }

    //    /// <summary>
    //    /// Họ tên bệnh nhân
    //    /// </summary>
    //    public string patientName { get; set; }

    //    /// <summary>
    //    /// Số nội trú
    //    /// </summary>
    //    public string zyh { get; set; }

    //    /// <summary>
    //    /// Mã thuốc
    //    /// </summary>
    //    public string ypCode { get; set; }

    //    /// <summary>
    //    /// Nhóm lớn
    //    /// </summary>
    //    public string dl { get; set; }

    //    /// <summary>
    //    /// Số lượng
    //    /// </summary>
    //    public decimal sl { get; set; }

    //    /// <summary>
    //    /// Uống,Truyền tĩnh mạch...
    //    /// </summary>
    //    public string zlff { get; set; }

    //    /// <summary>
    //    /// Thời gian thực hiện:04,06,08...
    //    /// </summary>
    //    public string sjap { get; set; }

    //    /// <summary>
    //    /// Tương ứng với số lượng thực hiện
    //    /// </summary>
    //    public string pcmc { get; set; }

    //    /// <summary>
    //    /// Liều dùng
    //    /// </summary>
    //    public decimal yl { get; set; }

    //    /// <summary>
    //    /// Đơn vị liều dùng
    //    /// </summary>
    //    public string yldw { get; set; }

    //    /// <summary>
    //    /// Mã nhân viên bác sĩ
    //    /// </summary>
    //    public string ysgh { get; set; }

    //    /// <summary>
    //    /// Ngày bắt đầu
    //    /// </summary>
    //    public DateTime ksrq { get; set; }

    //    /// <summary>
    //    /// Ngày kết thúc
    //    /// </summary>
    //    public DateTime jsrq { get; set; }

    //    /// <summary>
    //    /// Nhà thuốc phát thuốc
    //    /// </summary>
    //    public string fyyf { get; set; }

    //    /// <summary>
    //    /// 1:Tạm thời;2:Dài hạn
    //    /// </summary>
    //    public string yzxz { get; set; }

    //    /// <summary>
    //    /// Dặn dò
    //    /// </summary>
    //    public string yzbz { get; set; }

    //    /// <summary>
    //    /// Tần suất
    //    /// </summary>
    //    public int zxsl { get; set; }

    //    /// <summary>
    //    /// Đơn giá
    //    /// </summary>
    //    public decimal dj { get; set; }

    //    /// <summary>
    //    /// Số tiền
    //    /// </summary>
    //    public decimal je { get; set; }

    //    /// <summary>
    //    /// Tỷ lệ tự chi trả
    //    /// </summary>
    //    public decimal? zfbl { get; set; }

    //    /// <summary>
    //    /// Tính chất tự chi trả
    //    /// </summary>
    //    public char zfxz { get; set; }

    //    /// <summary>
    //    /// Tên khoa
    //    /// </summary>
    //    public string ksmc { get; set; }

    //    /// <summary>
    //    /// Tên khu bệnh
    //    /// </summary>
    //    public string bqmc { get; set; }

    //    /// <summary>
    //    /// Giường bệnh
    //    /// </summary>
    //    public string cw { get; set; }

    //    /// <summary>
    //    /// Cờ phát thuốc  0:Chưa phát;1:Đã xếp;2:Đã phát;3:Đã trả
    //    /// </summary>
    //    public char fybz { get; set; }

    //    /// <summary>
    //    /// Người yêu cầu thực hiện y lệnh
    //    /// </summary>
    //    public string yzzxsqr { get; set; }

    //    /// <summary>
    //    /// Ngày thực hiện
    //    /// </summary>
    //    public DateTime zxrq { get; set; }
    //}
}
