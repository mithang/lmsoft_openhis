using System;
using System.Collections.Generic;

namespace Newtouch.Domain.ValueObjects
{
    public class sldApplication 
    {


       public sldApplicationVO sld;
       public List<sldmxApplicationVO> sldmx;
    }
    public class sldApplicationVO
    {
        public string OrganizeId { get; set; }
        /// <summary>
        /// Khoa đăng ký Mã
        /// </summary>
        public string sqkscode { get; set; }
        /// <summary>
        /// Khoa đăng ký Tên
        /// </summary>
        public string sqksname { get; set; }
        /// <summary>
        /// Đăng ký Khu bệnh Mã
        /// </summary>
        public string sqbqcode { get; set; }

        /// <summary>
        /// Đăng ký Khu bệnh Tên
        /// </summary>
        public string sqbqname { get; set; }

        /// <summary>
        /// Bác sĩ đăng ký Mã
        /// </summary>
        public string sqyscode { get; set; }

        /// <summary>
        /// Bác sĩ đăng ký Tên
        /// </summary>
        public string sqysname { get; set; }

        /// <summary>
        /// Số phiếu
        /// </summary>
        public string sldh { get; set; }


        /// <summary>
        /// Tổng số tiền phiếu
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// Phiếu Ghi chú
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// Phiếu Trạng thái  
        /// </summary>
        public char sldzt { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreatorCode { get; set; }
    }
    public class sldmxApplicationVO
    {

        public string xmCode { get; set; }

        public string xmmc { get; set; }

        public decimal dj { get; set; }

        public int sl { get; set; }

        public string dw { get; set; }

        public string gg { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreatorCode { get; set; }

    }
}
