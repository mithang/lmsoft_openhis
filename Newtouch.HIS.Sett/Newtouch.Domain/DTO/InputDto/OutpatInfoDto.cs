/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description:Thông tin cá nhân cần khi lưu thu phí 
// Author:HLF
// CreateDate: 2017/1/17 13:21:19 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO
{
    public class OutpatInfoDto
    {
        /// <summary>
        /// Có phải là tính chất bệnh nhân tổ hợp hay không
        /// </summary>
        public bool IsZhbrxz { get; set; }

        public string zh_sbrxz { get; set; }

        public string zh_mbrxz { get; set; }
        /// <summary>
        /// Có phải là bệnh nhân BHYT hay không
        /// </summary>
        public bool isYbbr { get; set; }
        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// </summary>
        public int patid { get; set; }



        /// <summary>
        /// Mã nội bộ đăng ký khám
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// Mã tính chất bệnh nhân
        /// </summary>
        public int brxzbh { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân-Tên
        /// </summary>
        public string brxzmc { get; set; }


        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public string zjlx { get; set; }
    }
}
