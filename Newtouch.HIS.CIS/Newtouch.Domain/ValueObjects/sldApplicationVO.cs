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
        /// Mã khoa yêu cầu
        /// </summary>
        public string sqkscode { get; set; }
        /// <summary>
        /// Tên khoa yêu cầu
        /// </summary>
        public string sqksname { get; set; }
        /// <summary>
        /// Mã khu bệnh yêu cầu
        /// </summary>
        public string sqbqcode { get; set; }

        /// <summary>
        /// Tên khu bệnh yêu cầu
        /// </summary>
        public string sqbqname { get; set; }

        /// <summary>
        /// Mã bác sĩ yêu cầu
        /// </summary>
        public string sqyscode { get; set; }

        /// <summary>
        /// Tên bác sĩ yêu cầu
        /// </summary>
        public string sqysname { get; set; }

        /// <summary>
        /// Số phiếu yêu cầu
        /// </summary>
        public string sldh { get; set; }


        /// <summary>
        /// Tổng số tiền phiếu yêu cầu
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// Ghi chú phiếu yêu cầu
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// Trạng thái phiếu yêu cầu  
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
