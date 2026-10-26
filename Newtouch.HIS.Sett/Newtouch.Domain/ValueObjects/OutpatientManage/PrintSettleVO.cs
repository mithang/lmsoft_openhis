
using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    public class PrintSettleVO
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string danwei { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? sl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int xmnm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? fwfdj { get; set; }
        /// <summary>
        /// Đơn giá và đơn giá phí dịch vụ
        /// </summary>
        public decimal? djandfwfdj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        public string kh { get; set; }
    }
}
