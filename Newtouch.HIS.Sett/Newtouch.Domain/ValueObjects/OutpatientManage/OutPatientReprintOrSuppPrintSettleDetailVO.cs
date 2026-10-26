
using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// In bổ sung và in lại hóa đơn  Đối tượng chi tiết quyết toán
    /// </summary>
    public class OutPatientReprintOrSuppPrintSettleDetailVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? fwfdj { get; set; }
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
        public decimal? zfbl { get; set; }
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
        public int ghnm { get; set; }
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
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Đơn vị đơn giá
        /// </summary>
        public string djdw { get; set; }
        /// <summary>
        /// Cửa sổ lĩnh thuốc
        /// </summary>
        public string lyck { get; set; }
        /// <summary>
        /// Số lẻ đã tách
        /// </summary>
        public short? cls { get; set; }   //Bảng làsmallint
        /// <summary>
        /// Số nhóm
        /// </summary>
        public string czh { get; set; }
        /// <summary>
        /// Mã cờ thuốc
        /// </summary>
        public string ypbzdm { get; set; }
        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public int cfnm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kh { get; set; }

    }
}
