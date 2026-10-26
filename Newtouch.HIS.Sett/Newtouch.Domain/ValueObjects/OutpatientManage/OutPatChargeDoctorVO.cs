/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: 
// Author:
// CreateDate: 2016/12/26 19:07:26 
//**********************************************************/

using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeDoctorVO
    {
        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string rymc { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string ry { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public Int32 ksbh { get; set; }
    }
}
