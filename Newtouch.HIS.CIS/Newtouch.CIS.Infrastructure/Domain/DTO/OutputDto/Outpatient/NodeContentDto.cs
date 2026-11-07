using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class NodeContentDto
    {
        #region Bệnh nhân Khám bệnhthông tin

        public string jzysmc { get; set; }
        public string jzys { get; set; }
        public string xb { get; set; }
        public string nlshow { get; set; }
        
        public int mjzbz { get; set; }
        public string xm { get; set; }
        public string jzks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// đăng ký khám Bác sĩ
        /// </summary>
        public string ghys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? zlkssj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ghksmc { get; set; }
        
   

        /// <summary>
        /// 
        /// </summary>
        public decimal? tizhong { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tiwen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? maibo { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public decimal? xueya { get; set; }

        /// <summary>
        /// đường huyếtphương thức đo
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xuetang { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fzbz { get; set; }

        public decimal? shengao { get; set; }
        public decimal? shousuoya { get; set; }
        public decimal? shuzhangya { get; set; }
        public decimal? huxi { get; set; }
        public string cfzbz { get; set; }
        /// <summary>
        /// xãbảo Số hiệu
        /// </summary>
        public string sbbh { get; set; }
        public lastzcfDto lastzcfDto { get; set; }
        #endregion

        #region Hồ sơnội dung
        /// <summary>
        /// chínhbảo
        /// </summary>
        public string zs { get; set; }

        /// <summary>
        /// phátbệnh Thời gian
        /// </summary>
        public DateTime? fbsj { get; set; }

        /// <summary>
        /// hiệnbệnhsử
        /// </summary>
        public string xbs { get; set; }

        /// <summary>
        /// Tiền sử
        /// </summary>
        public string jws { get; set; }
        /// <summary>
        /// Thángtiền sử bệnh
        /// </summary>
        public string yjs { get; set; }
        /// <summary>
        /// qua Tiền sử dị ứng
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// Hôn nhân
        /// </summary>
        public string hy { get; set; }
        /// <summary>
        /// Hôn nhân
        /// </summary>
        public string hymc { get; set; }
        /// <summary>
        /// trathể
        /// </summary>
        public string ct { get; set; }

        public string clfa { get; set; }

        public string fzjc { get; set; }
        #endregion

        #region Hồ sơ Chẩn đoán
        /// <summary>
        /// 
        /// </summary>
        public List<WMDiagnosisHtmlVO> xyzdList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TCMDiagnosisHtmlVO> zyzdList { get; set; }
        #endregion

        #region Đơn thuốcnội dung
        /// <summary>
        /// 
        /// </summary>
        public List<NodeContentPresBO> cfBoList { get; set; }

        #endregion


    }

    public class lastzcfDto {
        /// <summary>
        /// cuối cùngmột lầnthulấykhámtraphí Thời gian
        /// </summary>
        public string zcfsj { get; set; }
        /// <summary>
        /// cuối cùngmột lầnthulấykhámtraphí Mục thu phí Chi tiết
        /// </summary>
        public string zcfsfxm { get; set; }
    }
}
