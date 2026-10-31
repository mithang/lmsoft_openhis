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
        /// làKhôngthành công true:Tất cảthành công/bộphútthành công  false:Tất cảthất bại/kiểm trathất bại
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Trạng tháimã tham sốchiếuLiệt kêResultCode
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// LỗiMô tả
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// Quay lạithông tin
        /// </summary>
        public dynamic Data { get; set; }
    }

    /// <summary>
    /// Y lệnhchi tiết   DataQuay lạinội dung,lấytậphợphiển thịhiện
    /// </summary>
    //public class YzDetail
    //{
    //    /// <summary>
    //    /// Y lệnhID
    //    /// </summary>
    //    public string yzId { get; set; }

    //    /// <summary>
    //    /// lĩnhthuốcthứ tựsố
    //    /// </summary>
    //    public long lyxh { get; set; }

    //    /// <summary>
    //    /// Tên bệnh nhân
    //    /// </summary>
    //    public string patientName { get; set; }

    //    /// <summary>
    //    /// Số nhập viện
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
    //    /// uống,tĩnhgiọt...
    //    /// </summary>
    //    public string zlff { get; set; }

    //    /// <summary>
    //    /// Thời gian thực hiện:04,06,08...
    //    /// </summary>
    //    public string sjap { get; set; }

    //    /// <summary>
    //    /// vàThực hiệnSố lượngtương ứng
    //    /// </summary>
    //    public string pcmc { get; set; }

    //    /// <summary>
    //    /// dùnglượng
    //    /// </summary>
    //    public decimal yl { get; set; }

    //    /// <summary>
    //    /// dùnglượngĐơn vị
    //    /// </summary>
    //    public string yldw { get; set; }

    //    /// <summary>
    //    /// Bác sĩMã nhân viên
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
    //    /// phát thuốcNhà thuốc
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
    //    /// Tỷ lệ tự trả
    //    /// </summary>
    //    public decimal? zfbl { get; set; }

    //    /// <summary>
    //    /// Tính chất tự trả
    //    /// </summary>
    //    public char zfxz { get; set; }

    //    /// <summary>
    //    /// Tên khoa
    //    /// </summary>
    //    public string ksmc { get; set; }

    //    /// <summary>
    //    /// Khu bệnhTên
    //    /// </summary>
    //    public string bqmc { get; set; }

    //    /// <summary>
    //    /// giường ký tự
    //    /// </summary>
    //    public string cw { get; set; }

    //    /// <summary>
    //    /// Cờ phát thuốc  0:chưaphát;1:đãxếp;2:đãphát;3:đãtrả
    //    /// </summary>
    //    public char fybz { get; set; }

    //    /// <summary>
    //    /// Y lệnhThực hiệnĐăng ký người
    //    /// </summary>
    //    public string yzzxsqr { get; set; }

    //    /// <summary>
    //    /// Thực hiệnNgày
    //    /// </summary>
    //    public DateTime zxrq { get; set; }
    //}
}
