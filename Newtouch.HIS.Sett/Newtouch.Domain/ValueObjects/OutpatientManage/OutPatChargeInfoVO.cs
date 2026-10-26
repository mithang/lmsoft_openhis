/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: Thông tin liên quan bệnh nhân thu phí đăng ký khám ngoại trú 
// Author:HLF
// CreateDate: 2016/12/23 11:22:53 
//**********************************************************/

using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeInfoVO
    {
        //[xt_brjbxx]
        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// </summary>
        public int patid { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime csny { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string dz { get; set; }
        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string pzh { get; set; }
        /// <summary>
        /// Hạng mục y tế
        /// </summary>
        public string ylxm { get; set; }
        /// <summary>
        /// Ngày bắt đầu chứng từ
        /// </summary>
        public DateTime? pzksrq { get; set; }
        /// <summary>
        /// Ngày kết thúc chứng từ
        /// </summary>
        public DateTime? pzzzrq { get; set; }
        /// <summary>
        /// Chẩn đoán chứng từ
        /// </summary>
        public string pzzd { get; set; }
        /// <summary>
        /// Tài khoản
        /// </summary>
        public int? zh { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Mã quận huyện
        /// </summary>
        public string qxdm { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string dwmc { get; set; }
        /// <summary>
        /// CMND/CCCD--Số giấy tờ
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// Loại thẻ
        /// </summary>
        public string zjlx { get; set; }


        // [xt_dy] Không cần nữa
        /// <summary>
        /// Mã khu vực
        /// </summary>
        public int? dybh { get; set; }
        /// <summary>
        /// Khu vực
        /// </summary>
        public string dy { get; set; }
        ///// <summary>
        ///// Tên khu vực
        ///// </summary>
        //public string dymc { get; set; }

        //[mz_gh]
        /// <summary>
        /// Mã nội bộ đăng ký khám
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Số thứ tự khám
        /// </summary>
        public Int16 jzxh { get; set; }

        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Cờ tái khám
        /// </summary>
        public Byte fzbz { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// Mã tính chất bệnh nhân
        /// </summary>
        public int brxzbh { get; set; }

        /// <summary>
        /// Tên tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// Loại giao dịch BHYT
        /// </summary>
        public string ybjylx { get; set; }



        /// <summary>
        /// Tập hợp tính chất bệnh nhân
        /// </summary>
        public List<OutPatChargeProperty> brxzList { get; set; }

        public short nl { get; set; }

    }
    public class GhJzInfo
    {
        public int ghnm { get; set; }
        public string mzh { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string zjh { get; set; }
        public string xb { get; set; }
        public string brxz { get; set; }
        public string ghksmc { get; set; }
        public string ghks { get; set; }
        public string ghysmc { get; set; }
        public string ghys { get; set; }
        public DateTime? ghrq { get; set; }
        public string ghzt { get; set; }
        public string jzzt { get; set; }
        public DateTime? jzrq { get; set; }
        public string jzksmc { get; set; }
        public string jzks { get; set; }
        public string jzysmc { get; set; }
        public string jzys { get; set; }
        public string fph { get; set; }
        public string syth { get; set; }
        public string thys { get; set; }
        public string thrq { get; set; }
    }


}
