using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Quyết toán giao dịch nội trú
    /// </summary>
    [Table("zy_jyjs")]
    public class HospTransactionSettlementEntity : IEntity<HospTransactionSettlementEntity>
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
        /// Về nghiệp vụ đảm bảo quyết toán nhiều tính chất bệnh nhân,Mã nội bộ quyết toán liên kết duy nhất.Đến từ mã nội bộ quyết toán bảng quyết toán nội trú
        /// </summary>
        public int gljsnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân tổ hợp và tính chất bệnh nhân con
        /// </summary>
        public string sbrxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zyts { get; set; }

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
        /// 
        /// </summary>
        public decimal xjzf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal xjwc { get; set; }

        /// <summary>
        /// ########CSHệ thống không dùng đến
        /// </summary>
        public decimal zhjz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 1 Đã quyết toán 2 Đã trả lại
        /// </summary>
        public string jszt { get; set; }

        /// <summary>
        /// Hủy quyết toán lần này(Số tiền là số âm)Mã nội bộ đang sử dụng
        /// </summary>
        public int cxjsnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cxjsyy { get; set; }

        /// <summary>
        /// ########CSHệ thống không dùng đến
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 1 Quyết toán 2 Quyết toán giữa chừng
        /// </summary>
        public string jsxz { get; set; }

        /// <summary>
        /// Chỉ có ý nghĩa đối với quyết toán giữa chừng
        /// </summary>
        public DateTime? jsksrq { get; set; }

        /// <summary>
        /// Chỉ có ý nghĩa đối với quyết toán giữa chừng
        /// </summary>
        public DateTime? jsjsrq { get; set; }

        /// <summary>
        /// Thuế địa phương,Mã hóa đơn trên hóa đơn thuế quốc gia,Do cần xuất hóa đơn để báo cáo,Vì vậy phải   2013-8-7 Hủy trường này:Áp dụng quản lý hóa đơn tài chính(cw_fp)chương trình nhập số hóa đơn và số bắt đầu của mã hóa đơn,Sau đó tính mã hóa đơn của mỗi hóa đơn qua hiệu số của hai số này,Đồng thời tính ra danh sách hóa đơn đã hủy
        /// </summary>
        public string fpdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        ///             yb_deal_wjy = 0,//Không có giao dịch BHYT               yb_deal_mzgh = 1,//Đăng ký khám ngoại trú               yb_deal_mzsf = 2,//Thu phí ngoại trú               yb_deal_dbgh = 3,//Đăng ký khám bệnh nặng               yb_deal_dbsf = 4,//Thu phí bệnh nặng               yb_deal_jcsf = 5,//Thu phí giường gia đình                yb_deal_zysf = 6,    //Thu phí nội trú               yb_deal_gsgh = 7,    //Đăng ký khám tai nạn lao động               yb_deal_gsmz = 8,   //Thu phí tai nạn lao động               yb_deal_sssf = 9 //Thu phí thời gian thực đối với hạng mục chi tiết              
        /// </summary>
        public string jylx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ysk { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zl { get; set; }

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

    }
}
