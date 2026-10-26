
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    public class PrintInvoiceInfoVO
    {
        /// <summary>
        /// Có đăng ký khám hay không
        /// </summary>
        public bool IsGh;
        public PrintInvoiceInfoVO(bool isGh = false)
        {
            this.IsGh = isGh;
        }
        /// <summary>
        /// Cửa sổ 
        /// </summary>
        public List<string> Xwindow { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string Fph { get; set; }
        /// <summary>
        /// Số giao dịch nghiệp vụ
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// Loại cơ sở y tế
        /// </summary>
        public string DepartmentType { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// Loại BHYT
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Số BHXH
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Hạng mục Số tiền
        /// </summary>
        public List<KeyValuePair<string, string>> ItemPrice = new List<KeyValuePair<string, string>>();
        /// <summary>
        /// Mã hạng mục Tên Quy cách Số lượng Đơn giá Số tiền(đồng)
        /// </summary>
        public List<Dictionary<string, string>> ItemDetails = new List<Dictionary<string, string>>();
        /// <summary>
        /// Tổng cộng như:Phải thuxxxThu trướcyyyTiền thối lạizzz
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// Chi tiết đăng ký khám
        /// Tên bệnh nhân:Name Mã nội bộ bệnh nhân:ID Thông thường hoặc chuyên gia:YS Khoa:KS Số giao dịch khoa:KsNo Ngày tháng:dateTime Tên bác sĩ:YsName
        /// </summary>
        public Dictionary<string, string> GhItemDetails = new Dictionary<string, string>(){
            {"Name" ,string.Empty},
            {"ID",string.Empty},
            {"YS",string.Empty},
            {"KS",string.Empty},
            {"KsNo",string.Empty},
            {"dateTime",string.Empty},
            {"YsName",string.Empty}
        };
        /// <summary>
        /// Khoa như:Khoa đau
        /// </summary>
        public string Ks { get; set; }
        /// <summary>
        /// Tổng cộng(Chữ hoa)
        /// </summary>
        public decimal TotalStr { get; set; }
        /// <summary>
        /// Thanh toán tiền mặt
        /// </summary>
        public string Cash { get; set; }
        /// <summary>
        /// Thanh toán từ tài khoản cá nhân
        /// </summary>
        public string Pay { get; set; }
        /// <summary>
        /// Chi trả từ quỹ chung BHYT
        /// </summary>
        public string SocialPay { get; set; }
        /// <summary>
        /// Thanh toán bổ sung
        /// </summary>
        public string ExtraPay { get; set; }
        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public string PayForClass { get; set; }
        /// <summary>
        /// Tự chi trả
        /// </summary>
        public string PayForSelf { get; set; }
        /// <summary>
        /// Tự chi trả
        /// </summary>
        public string PrivateExpense { get; set; }
        /// <summary>
        /// Số dư tài khoản trong năm
        /// </summary>
        public string CurrentRemain { get; set; }
        /// <summary>
        /// Số dư tài khoản các năm
        /// </summary>
        public string PastRemain { get; set; }
        /// <summary>
        /// Phương thức thanh toán như:Tiền mặt:xxđồng,pos:yyđồng
        /// </summary>
        public string PayFuc { get; set; }
        /// <summary>
        /// Sai số tiền xu
        /// </summary>
        public string XjWc { get; set; }
        /// <summary>
        /// Đơn vị thu tiền
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Nhân viên thu tiền
        /// </summary>
        public string Cashier { get; set; }
        /// <summary>
        /// Ngày tháng
        /// </summary>
        public string Date;
        /// <summary>
        /// TrueĐể in:Tên hạng mục của phần biên lai này,Nội dung đã được đơn giản hóa.Để biết thông tin chi tiết, vui lòng tra cứu qua các kênh công khai chính thức như màn hình cảm ứng.
        /// </summary>
        public bool IsFull { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// Đường dẫn mẫu hóa đơn
        /// </summary>
        public string FpdymbPath { get; set; }
        /// <summary>
        /// Đường dẫn mẫu hóa đơn đăng ký khám
        /// </summary>
        public string GhFpdymbPath { get; set; }
        /// <summary>
        /// Có in bổ sung hay không
        /// </summary>
        public bool IsBd { get; set; }

    }
}
