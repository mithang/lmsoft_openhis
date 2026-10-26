using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// Đối tượng trả về tra cứu thông tin xếp thuốc
    /// </summary>
    public class OutPatientpyListDTO
    {
        /// <summary>
        /// Mã nội bộ quyết toán thôngjsnm
        /// </summary>
        public int sfxh { get; set; }
        public string sffyxh { get; set; }
        public string cfh { get; set; }
        public string xm { get; set; }
        public string kh { get; set; }
        public string brlxmc { get; set; }
        public DateTime sfsj { get; set; }
        public int? sfck { get; set; }
        public string ys { get; set; }
        public string Ysdm { get; set; }
        public string ksdm { get; set; }
        public string ksmc { get; set; }
        public int? nl { get; set; }
        public string gboy { get; set; }
        public string ggirl { get; set; }
        public string gh { get; set; }
        public string mzh { get; set; }
        public string lb { get; set; }
        public string yb { get; set; }
        
        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal Zje { get; set; }
        public string gyb { get; set; }
        public string gfyb { get; set; }
        public string gqt { get; set; }
        public string Fyczy { get; set; }
        public string Pyczy { get; set; }
        public string Tygyd { get; set; }
        public string Fyck { get; set; }
        public string Fyczygh { get; set; }
        public string Pyczygh { get; set; }
        public int? Ystd { get; set; }
        public int? Yszbh { get; set; }
        public string Fph { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public long pcfnm { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Nhà thuốc lĩnh thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Số tiền đơn thuốc
        /// </summary>
        public decimal je { get; set; }
    }
}
