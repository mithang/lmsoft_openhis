using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
   /// <summary>
   /// Đối tượng trả về tra cứu chi tiết BHYT nội trú chưa phê duyệt
   /// </summary>
    public class RequestApprovalVO
    {
        /// <summary>
        /// Trạng thái chi phí
        /// </summary>
        public string fyzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }

        public int jfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Số ngoại trú/nội trú
        /// </summary>
        public DateTime createtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }

        public decimal? zje { get; set; }

        public string zyh { get; set; }

        public string jylsh { get; set; }

        public string cfh { get; set; }
    }
}
