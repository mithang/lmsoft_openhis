using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newtouch.HIS.Web.Areas.HospitalizationManage.Models
{
    /// <summary>
    /// Thong tin chinh tra cuu chi phi
    /// </summary>
    public class HospFeeSearchMainModel
    {

        /// <summary>
        /// Ngay thu phi
        /// </summary>
        public string Sfrq { get; set; }

        /// <summary>
        /// Ma nhom lon phi giuong benh
        /// </summary>
        public string CWDlCode { get; set; }

        /// <summary>
        /// Phi giuong benh
        /// </summary>
        public decimal CWJe { get; set; }

        /// <summary>
        /// Ma nhom lon phi cham soc
        /// </summary>
        public string HLDlCode { get; set; }

        /// <summary>
        /// Phi cham soc
        /// </summary>
        public decimal HLJe { get; set; }

        /// <summary>
        /// Ma nhom lon phi dieu tri
        /// </summary>
        public string ZLDlCode { get; set; }

        /// <summary>
        /// Phi dieu tri
        /// </summary>
        public decimal ZLJe { get; set; }

        /// <summary>
        /// Ma nhom lon phi khac
        /// </summary>
        public string QTDlCode { get; set; }

        /// <summary>
        /// Phi khac
        /// </summary>
        public decimal QTJe { get; set; }

        /// <summary>
        /// Ma nhom lon phi thuoc Tay
        /// </summary>
        public string XYDlCode { get; set; }

        /// <summary>
        /// Phi thuoc Tay
        /// </summary>
        public decimal XYJe { get; set; }

        /// <summary>
        ///Ma nhom lon thuoc thanh pham Dong y
        /// </summary>
        public string ZCYDlCode { get; set; }

        /// <summary>
        /// Thuoc thanh pham Dong y
        /// </summary>
        public decimal ZCYJe { get; set; }

        /// <summary>
        /// Ma nhom lon phi xet nghiem
        /// </summary>
        public string HYDlCode { get; set; }

        /// <summary>
        /// Phi xet nghiem
        /// </summary>
        public decimal HYJe { get; set; }

        /// <summary>
        /// Ma nhom lon phi kiem tra
        /// </summary>
        public string JCDlCode { get; set; }

        /// <summary>
        /// Phi kiem tra
        /// </summary>
        public decimal JCJe { get; set; }

        /// <summary>
        /// Ma nhom lon phi vat tu
        /// </summary>
        public string CLDlCode { get; set; }

        /// <summary>
        /// Phi vat tu
        /// </summary>
        public decimal CLJe { get; set; }

        /// <summary>
        /// Tong cong
        /// </summary>
        public decimal HJ { get; set; }

    }
}