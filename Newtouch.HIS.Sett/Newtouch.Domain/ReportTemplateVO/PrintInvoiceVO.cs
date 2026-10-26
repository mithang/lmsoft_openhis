using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    public class PrintInvoiceVO
    {
        /// <summary>
        /// Cửa sổ 
        /// </summary>
        public string Xwindow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fph { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DepartmentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string ItemSubReport { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TotalStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Cash { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Pay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SocialPay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ExtraPay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PayForClass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PayForSelf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PrivateExpense { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CurrentRemain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PastRemain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Cashier { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// Phương thức thanh toán như:Tiền mặt:xxđồng,pos:yyđồng
        /// </summary>
        public string PayFuc { get; set; }
        /// <summary>
        /// Sai số tiền xu
        /// </summary>
        public string XjWc { get; set; }
        /// <summary>
        /// Tổng cộng như:Phải thuxxxThu trướcyyyTiền thối lạizzz
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Khoa như:Khoa đau
        /// </summary>
        public string Ks { get; set; }
        /// <summary>
        /// TrueĐể in:Tên hạng mục của phần biên lai này,Nội dung đã được đơn giản hóa.Để biết thông tin chi tiết, vui lòng tra cứu qua các kênh công khai chính thức như màn hình cảm ứng.
        /// </summary>
        public string IsFull { get; set; }

    }
}
