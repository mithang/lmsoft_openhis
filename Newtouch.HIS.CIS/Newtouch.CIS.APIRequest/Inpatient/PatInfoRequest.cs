using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Inpatient
{
    public class PatInfoRequest: JSONRequestBase
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        [Required]
        public string zyh { get; set; }

        /// <summary>
        /// Cờ đang nằm viện
        /// </summary>
        public string zybz { get; set; }
        /// <summary>
        /// Chẩn đoán nhập viện
        /// </summary>
        public string ryzd { get; set; }  
        public string ryzdmc { get; set; }
        //Mã tính chất bệnh nhân 
        public string brxz { get; set; }
        /// <summary>
        /// Mô tả tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        ///  Tên người liên hệ
        /// </summary>
        public string lxr { get; set; }

        /// <summary>
        /// Quan hệ người liên hệ
        /// </summary>
        public string lxrgx { get; set; }

        /// <summary>
        /// Điện thoại người liên hệ
        /// </summary>
        public string lxrdh { get; set; }
    }
}
