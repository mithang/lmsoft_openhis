using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Quyết toán ngoại trú
    /// </summary>
    [Table("mz_js")]
    public class OutpatientSettlementEntity : IEntity<OutpatientSettlementEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jsnm { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// EnumEnumJslx 0 Đăng ký khám 1 Ghi sổ giường gia đình 2 Quyết toán ngoại trú(Bao gồm quyết toán giường gia đình) 3 Nhập viện 4 Thu chi tài khoản   -------   Khi hoàn phí,Phải biết có phải ghi sổ giường gia đình hay không,Xác định có thực hiện quyết toán BHYT hay không   －－－－   (gốc:0 Đăng ký khám 1 Đơn thuốc 2 Hạng mục  Vì đơn thuốc và hạng mục được quyết toán cùng lúc,Vì vậy không phân biệt)   
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zlfy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zffy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal flzffy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal jzfy { get; set; }

        /// <summary>
        /// Khoản phải thu khi quyết toán(Hiện trường quyết toán khác cần thanh toán)
        /// </summary>
        public decimal xjzf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal xjwc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zhjz { get; set; }

        /// <summary>
        /// from xt_xjzffs //20180829Hủy,Đổi phụ thuộcmz_jszffsBảng
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 1 Đã quyết toán(Chưa trả lại) 2 Đã trả lại 3 Thu bổ sung 4 Thu bổ sung rồi trả lại   --2006-06-17 Kích hoạt,Trường dư thừa để dễ nhận biết tình hình quyết toán      --2006.06.16 Tạm thời không dùng trường này   Hình như có thể thông qua   Đã quyết toán:Mã nội bộ quyết toán>0   Đã quyết toán(Chưa trả lại):Mã nội bộ quyết toán>0andMã nội bộ hủy>0   Đã trả lại:Mã nội bộ hủy quyết toán>0
        /// </summary>
        public int jszt { get; set; }

        /// <summary>
        /// Hủy quyết toán lần này(Số tiền là số âm)nơi???Mã nội bộ dùng
        /// </summary>
        public int cxjsnm { get; set; }

        /// <summary>
        /// 2012-11-24 Cần sửa phương pháp thanh toán tài khoản,   Thanh toán từ tài khoản:Cá nhân,Tài khoản đơn vị hoặc UnionPay v.v.,Có thể thanh toán riêng từ nhiều tài khoản,   	　do＂Khám ngoại trú－Tài khoản quyết toán＂Bảng dùng để ghi thông tin thanh toán tài khoản cụ thể(Chờ xây dựng)   	Đồng thời,Xóa"Tài khoản"Trường         Tài khoản dùng để khấu trừ chi phí ghi sổ
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// Thuế địa phương,Mã hóa đơn trên hóa đơn thuế quốc gia,Do cần xuất hóa đơn để báo cáo,Vì vậy phải   2013-8-7 Hủy trường này:Áp dụng quản lý hóa đơn tài chính(cw_fp)chương trình nhập số hóa đơn và số bắt đầu của mã hóa đơn,Sau đó tính mã hóa đơn của mỗi hóa đơn qua hiệu số của hai số này,Đồng thời tính ra danh sách hóa đơn đã hủy
        /// </summary>
        public string fpdm { get; set; }

        /// <summary>
        /// (Tỷ lệ chiết khấu tự chi trả)
        /// </summary>
        public decimal jmbl { get; set; }

        /// <summary>
        /// (Số tiền chiết khấu tự chi trả)
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        ///                Loại giao dịch BHYT yb_deal_wjy = 0,//Không có giao dịch BHYT               yb_deal_mzgh = 1,//Đăng ký khám ngoại trú               yb_deal_mzsf = 2,//Thu phí ngoại trú               yb_deal_dbgh = 3,//Đăng ký khám bệnh nặng               yb_deal_dbsf = 4,//Thu phí bệnh nặng               yb_deal_jcsf = 5,//Thu phí giường gia đình                yb_deal_zysf = 6,    //Thu phí nội trú               yb_deal_gsgh = 7,    //Đăng ký khám tai nạn lao động               yb_deal_gsmz = 8,   //Thu phí tai nạn lao động               yb_deal_sssf = 9 //Thu phí thời gian thực đối với hạng mục chi tiết                               nb_yb_fyjs_mz_T1 = 10,  //Quyết toán ngoại trú Bảo hiểm nông nghiệp              nb_yb_bc_ztjs = 11, //Thu phí quyết toán giữa chừng nội trú Bảo hiểm nông nghiệp              nb_yb_fyjs_zy_T1 = 12,  //Thu phí quyết toán xuất viện nội trú Bảo hiểm nông nghiệp              yb_deal_gszy = 13, //Thu phí tai nạn lao động nội trú              yb_deal_jcjz = 14,//Ghi sổ giường gia đình     
        /// </summary>
        public string jylx { get; set; }

        /// <summary>
        /// Khoản thu trước nên80 thu100 Tiền thối lại20, Ở đây100
        /// </summary>
        public decimal? ysk { get; set; }

        /// <summary>
        /// Tiền thối lại  nên80 thu100 Tiền thối lại20, Ở đây20
        /// </summary>
        public decimal? zl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? jch { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Giới tínhxb
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime? csny { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Loại giấy tờ 
        /// </summary>
        public string zjlx { get; set; }

        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// (Ngày thu phí)
        /// </summary>
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Dù trả hay bị trả đều vui lòng đặt thành1 Thuận tiện tra cứu
        /// </summary>
        public int? tbz { get; set; }

        /// <summary>
        /// Số tiền phải thu sau chiết khấu(Số tiền phải thu sau chiết khấu phần tự chi trả)
        /// (Hình như vàxjzfÝ nghĩa trường bị trùng lặp,Vì vậy20180829Hủy)
        /// </summary>
        public decimal? zkhzje { get; set; }

        /// <summary>
        /// Có phải là quyết toán trước khi nợ phí hay không
        /// </summary>
        public bool? isQfyj { get; set; }
        /// <summary>
        /// Số đơn hàng yêu cầu thanh toán
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// Số giao dịch quyết toán BHYT
        /// </summary>
        public string ybjslsh { get; set; }
        /// <summary>
        /// Quyết toán có dùng giảm miễn hay không
        /// </summary>
        public string isjm { get; set; }

    }
}
