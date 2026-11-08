using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class NodeContentDto
    {
        #region Bệnh nhân Khám bệnh thông tin

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
        /// đường huyết phương thức đo
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
        /// xã bảo Số hiệu
        /// </summary>
        public string sbbh { get; set; }
        public lastzcfDto lastzcfDto { get; set; }
        #endregion

        #region Hồ sơ nội dung
        /// <summary>
        /// chính bảo
        /// </summary>
        public string zs { get; set; }

        /// <summary>
        /// phát bệnh Thời gian
        /// </summary>
        public DateTime? fbsj { get; set; }

        /// <summary>
        /// hiện bệnh sử
        /// </summary>
        public string xbs { get; set; }

        /// <summary>
        /// Tiền sử
        /// </summary>
        public string jws { get; set; }
        /// <summary>
        /// Tháng tiền sử bệnh
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
        /// tra thể
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

        #region Đơn thuốc nội dung
        /// <summary>
        /// 
        /// </summary>
        public List<NodeContentPresBO> cfBoList { get; set; }

        #endregion


    }

    public class lastzcfDto {
        /// <summary>
        /// cuối cùng một lần thu lấy khám tra phí Thời gian
        /// </summary>
        public string zcfsj { get; set; }
        /// <summary>
        /// cuối cùng một lần thu lấy khám tra phí Mục thu phí Chi tiết
        /// </summary>
        public string zcfsfxm { get; set; }
    }
}
