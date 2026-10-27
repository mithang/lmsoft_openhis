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
        /// ởviệnCờ
        /// </summary>
        public string zybz { get; set; }
        /// <summary>
        /// Chẩn đoán nhập viện
        /// </summary>
        public string ryzd { get; set; }  
        public string ryzdmc { get; set; }
        //bệnh nhânTính chấtMã 
        public string brxz { get; set; }
        /// <summary>
        /// bệnh nhânTính chấtMô tả
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        ///  Người liên hệHọ tên
        /// </summary>
        public string lxr { get; set; }

        /// <summary>
        /// Người liên hệquan hệ
        /// </summary>
        public string lxrgx { get; set; }

        /// <summary>
        /// Người liên hệĐiện thoại
        /// </summary>
        public string lxrdh { get; set; }
    }
}
