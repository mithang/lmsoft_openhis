using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Đồng bộ CSDL trung gian về cục bộ
    /// </summary>
    public class SysIDBVO
    {

        public string OrganizeId { get; set; }

        public string jzId { get; set; }


        /// <summary>
        /// Thông tin đăng ký khám dư thừa  Enum 1 Khám thường ngoại trú 2 Cấp cứu 3Chuyên gia
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// Tương ứngbillingSố bệnh án trong giao diện
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Liên kếtbillingBảng đăng ký khám của hệ thống
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa Enum
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Trường bảng đăng ký khám dư thừa Tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Trường bảng đăng ký khám dư thừa Tên bác sĩ
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// Trường bảng đăng ký khám dư thừa
        /// </summary>
        public DateTime ghsj { get; set; }

        public DateTime ghczsj { get; set; }

        public int jzzt { get; set; }
        public string brxzCode { get; set; }


    }
}
