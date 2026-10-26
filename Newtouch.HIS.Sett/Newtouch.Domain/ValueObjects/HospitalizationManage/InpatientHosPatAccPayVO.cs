using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class InpatientHosPatAccPayVO
    {
        /// <summary>
        /// Số tiền thu chi xt_brzhszjl
        /// </summary>
        public decimal szje { get; set; }
        /// <summary>
        /// Số dư tài khoản
        /// </summary>
        public decimal zhye { get; set; }
        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string pzh { get; set; }
        /// <summary>
        /// Nhân viên thu chi
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// Ngày thu chi-Tức là ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Tính chất thu chi
        /// </summary>
        public string szxz { get; set; }

        /// <summary>
        /// Phương thức thanh toán  xt_xjzffs
        /// </summary>
        public string xjzffsmc { get; set; }

        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public string xjzffs { get; set; }
    }

    /// <summary>
    /// Bản ghi thu chi tài khoản
    /// </summary>
    public class InpatientPatAccPayVO
    {
        public string Id { get; set; }
        /// <summary>
        /// Số tiền thu chi xt_brzhszjl
        /// </summary>
        public decimal szje { get; set; }
        /// <summary>
        /// Số dư tài khoản
        /// </summary>
        public decimal zhye { get; set; }
        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string pzh { get; set; }
        /// <summary>
        /// Nhân viên thu chi
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Ngày thu chi-Tức là ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Tính chất thu chi
        /// </summary>
        public int szxz { get; set; }

        public string szxzmc { get; set; }

        /// <summary>
        /// Phương thức thanh toán  xt_xjzffs
        /// </summary>
        public string xjzffsmc { get; set; }

        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public string xjzffs { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string outTradeNo { get; set; }
    }
}
