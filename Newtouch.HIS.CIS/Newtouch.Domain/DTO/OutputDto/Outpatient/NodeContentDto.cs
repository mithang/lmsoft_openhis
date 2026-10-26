using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class NodeContentDto
    {
        #region 患者就诊信息

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
        /// Bác sĩ đăng ký khám
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
        /// Cách đo đường huyết
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
        /// Số bảo hiểm xã hội
        /// </summary>
        public string sbbh { get; set; }
        public lastzcfDto lastzcfDto { get; set; }
        #endregion

        #region 病历内容
        /// <summary>
        /// Triệu chứng chính
        /// </summary>
        public string zs { get; set; }

        /// <summary>
        /// Thời gian phát bệnh
        /// </summary>
        public DateTime? fbsj { get; set; }

        /// <summary>
        /// Bệnh sử hiện tại
        /// </summary>
        public string xbs { get; set; }

        /// <summary>
        /// Tiền sử
        /// </summary>
        public string jws { get; set; }
        /// <summary>
        /// Tiền sử kinh nguyệt
        /// </summary>
        public string yjs { get; set; }
        /// <summary>
        /// Tiền sử dị ứng
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
        /// Khám thực thể
        /// </summary>
        public string ct { get; set; }

        public string clfa { get; set; }

        public string fzjc { get; set; }
        #endregion

        #region 病历诊断
        /// <summary>
        /// 
        /// </summary>
        public List<WMDiagnosisHtmlVO> xyzdList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TCMDiagnosisHtmlVO> zyzdList { get; set; }
        #endregion

        #region 处方内容
        /// <summary>
        /// 
        /// </summary>
        public List<NodeContentPresBO> cfBoList { get; set; }

        #endregion


    }

    public class lastzcfDto {
        /// <summary>
        /// Thời gian thu phí khám chữa bệnh lần cuối
        /// </summary>
        public string zcfsj { get; set; }
        /// <summary>
        /// Chi tiết hạng mục thu phí khám chữa bệnh thu lần cuối
        /// </summary>
        public string zcfsfxm { get; set; }
    }
}
