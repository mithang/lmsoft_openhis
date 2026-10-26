/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: 
// Author:
// CreateDate: 2016/12/15 21:21:53 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    /// <summary>
    /// Truyền tham số nạp tiền
    /// </summary>
    public class DeposDto
    {
        /// <summary>
        /// Mã phương thức thanh toán
        /// </summary>
        public int zffsbh { get; set; }

        /// <summary>
        /// Tên phương thức thanh toán
        /// </summary>
        public string zffsmc { get; set; }

        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal zfje { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public int zh { get; set; }
        /// <summary>
        /// Tính chất thu chi
        /// </summary>
        public int szxz { get; set; }

        /// <summary>
        /// Tính chất tài khoản
        /// </summary>
        public int? zhxz { get; set; }

        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// </summary>
        public int patid { get; set; }
        public string zyh { get; set; }
        public string memo { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string outTradeNo { get; set; }
    }
}
