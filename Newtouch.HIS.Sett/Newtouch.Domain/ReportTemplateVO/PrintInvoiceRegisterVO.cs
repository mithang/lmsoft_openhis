using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    public class PrintInvoiceRegisterVO
    {
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

        public string KsNo { get; set; }



        #region 挂号显示内容
        /// <summary>
        /// 
        /// </summary>
        public string P_Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_GH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_KS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_BKS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_YsName { get; set; }
        #endregion

    }
}
