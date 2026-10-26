using System;
using System.Collections.Generic;
using Microsoft.Build.Framework;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Trả thuốc nội trú
    /// </summary>
    public class HospitalizatiionReturnDispensingMedicineRequestDTO : RequestBase
    {
        /// <summary>
        /// ID tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Chi tiết thuốc trả
        /// </summary>
        public List<ReturnDispensingDrug> ReturnDrugDetail { get; set; }

        /// <summary>
        /// Số thứ tự gọi giao diện
        /// </summary>
        public string ClientNo { get; set; }
    }

    /// <summary>
    /// Trả thuốc
    /// </summary>
    public class ReturnDispensingDrug
    {

        /// <summary>
        /// ID y lệnh
        /// </summary>
        [Required]
        public string yzId { get; set; }

        /// <summary>
        /// Code thuốc
        /// </summary>
        [Required]
        public string ypCode { get; set; }

        /// <summary>
        /// Số lượng trả thuốc đơn vị bộ phận
        /// </summary>
        [Required]
        public int tysl { get; set; }

        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi（tysl*zhyz=số lượng đơn vị nhỏ nhất）  Mặc định số lẻ tách nội trú
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Mã người xin trả thuốc
        /// </summary>
        [Required]
        public string tysqr { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// Ngày thực hiện
        /// </summary>
        public DateTime? zxrq { get; set; }

        /// <summary>
        /// Mã khu bệnh
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// Nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// Số thứ tự lĩnh thuốc
        /// </summary>
        public long lyxh { get; set; }
    }
}