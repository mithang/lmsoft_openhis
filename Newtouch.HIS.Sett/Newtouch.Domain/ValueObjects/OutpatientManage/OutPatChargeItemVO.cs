/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description:Thực thể hạng mục đăng ký khám ngoại trú 
// Author:HLF
// CreateDate: 2016/12/26 16:07:31 
//**********************************************************/
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thực thể hạng mục đăng ký khám ngoại trú
    /// </summary>
    public class OutPatChargeItemVO
    {
        /// <summary>
        /// Mã nội bộ đăng ký khám
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public int ksbh { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Mã bác sĩ
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Cờ khám ngoại trú/cấp cứu
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// Hạng mục bệnh nặng
        /// </summary>
        public string dbxm { get; set; }

        /// <summary>
        /// Cờ tái khám
        /// </summary>
        public Byte fzbz { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Khoa đăng ký khám  Trường tổ hợp
        /// </summary>
        public string gh_ks { get; set; }

        /// <summary>
        /// Tên hiển thị đăng ký khám
        /// </summary>
        public string ghName { get; set; }

        /// <summary>
        /// Tên hiển thị khoa
        /// </summary>
        public string ksName { get; set; }


        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ysName { get; set; }
         

    }
}
