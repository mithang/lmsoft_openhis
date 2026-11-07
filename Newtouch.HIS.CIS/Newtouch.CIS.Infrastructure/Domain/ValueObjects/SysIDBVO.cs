using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// tronggiữakho Đồng bộđếnbảnđịa
    /// </summary>
    public class SysIDBVO
    {

        public string OrganizeId { get; set; }

        public string jzId { get; set; }


        /// <summary>
        /// Dư thừađăng ký khámthông tin  Liệt kê 1 thông thường Ngoại trú 2 gấpkhám 3chuyên gia
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// tương ứngbillinggiao diệntrongcủa Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// liên kếtbilling Hệ thốngcủađăng ký khámbảng
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn Liệt kê
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámbảngchữđoạn Tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámbảngchữđoạn Bác sĩ Tên
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámbảngchữđoạn
        /// </summary>
        public DateTime ghsj { get; set; }

        public DateTime ghczsj { get; set; }

        public int jzzt { get; set; }
        public string brxzCode { get; set; }


    }
}
