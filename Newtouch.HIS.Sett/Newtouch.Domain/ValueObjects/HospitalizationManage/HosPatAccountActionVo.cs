/*********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: Quản lý nội trú》Quản lý tài khoản》Thông tin cơ bản bệnh nhân
// Author:HLF
// CreateDate: 2016/12/7 17:01:42 
//**********************************************************/
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class HosPatAccountActionVO
    { 
        /// <summary>
        /// Số nhập viện zy_brjbxx
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Mã pinyin xt_brjbxx  từzy_brjbxxĐọc bảng  Thêm trường giống nhau
        /// </summary>
        //public string py { get; set; }
        /// <summary>
        /// Mã nội bộ bệnh nhân 
        /// </summary>
        public Int32 patid { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
    
        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// Tuổi
        /// </summary>
        //public Int16 nl { get; set; }

        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Tính chất tài khoản xt_brzh
        /// </summary>
        public int? zhxz { get; set; }

        /// <summary>
        /// Số dư tài khoản
        /// </summary>
        public decimal zhye { get; set; }
        /// <summary>
        /// Chủ tài khoản
        /// </summary>
        public string zhsyr { get; set; }
      
        /// <summary>
        /// Ngày kích hoạt
        /// </summary>
        public DateTime qyrq { get; set; }
        /// <summary>
        /// Ngày chấm dứt
        /// </summary>
        public DateTime zzrq { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Ngày lập hồ sơ
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Nhân viên lập hồ sơ
        /// </summary>
        public string CreatorUserCode { get; set; }
        /// <summary>
        /// Tài khoản
        /// </summary>
        public int zh { get; set; }

        //Thêm trường
        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Khu bệnh
        /// </summary>
        public string bq { get; set; }

        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        public DateTime ryrq { get; set; }


        /// <summary>
        /// Mã tài khoản
        /// </summary>
        public int zhbh { get; set; }

        /// <summary>
        /// Tên tính chất tài khoản
        /// </summary>
        public string zhxzMC { get; set; }
    }
}
