using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// tronggiữakho Đồng bộ đến bản địa
    /// </summary>
    public class SysIDBVO
    {

        public string OrganizeId { get; set; }

        public string jzId { get; set; }


        /// <summary>
        /// Dư thừa đăng ký khám thông tin  Liệt kê 1 thông thường Ngoại trú 2 gấp khám 3chuyên gia
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// tương ứng billing giao diện trong của Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// liên kết billing Hệ thống của đăng ký khám bảng
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn Liệt kê
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Dư thừa đăng ký khám bảng chữ đoạn Tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Dư thừa đăng ký khám bảng chữ đoạn Bác sĩ Tên
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// Dư thừa đăng ký khám bảng chữ đoạn
        /// </summary>
        public DateTime ghsj { get; set; }

        public DateTime ghczsj { get; set; }

        public int jzzt { get; set; }
        public string brxzCode { get; set; }


    }
}
