using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request
{
    public class UpdateInpatientOutInfoRequest : RequestBase
    {
        //zyh,bq,cw,cyrq,cyzd,doctor
        [Required]
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Khu bệnh
        /// </summary>
        public string bq { get; set; }
        /// <summary>
        /// giư ờng bệnh 
        /// </summary>
        public string cw { get; set; }
        /// <summary>
        ///  Ngày xuất viện
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// Xuất việnChẩn đoán
        /// </summary>
        public string cyzd { get; set; }
        ///// <summary>
        ///// Xuất viện病情
        ///// </summary>
        //public string cybq { get; set; }
        /// <summary>
        /// 主治bác sĩ
        /// </summary>
        public string doctor { get; set; }
        ///// <summary>
        ///// Chẩn đoán nhập viện
        ///// </summary>
        //public short? ryzd { get; set; }
        ///// <summary>
        ///// Nhập viện病情
        ///// </summary>
        //public string rybq { get; set; }
        /// <summary>
        /// Dấu hiệu đang nằm viện
        /// </summary>
        public string zybz { get; set; }

    }
}
