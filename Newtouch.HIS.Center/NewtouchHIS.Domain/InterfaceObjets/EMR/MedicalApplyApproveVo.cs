using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.EMR
{
    public class MedicalApplyApproveVo
    {
        /// <summary>
        /// Phê duyệtĐăng kýSố
        /// </summary>
        [Required(ErrorMessage = "ApplyNos không được để trống")]
        
        public string ApplyNos { get; set; }
        /// <summary>
        /// Người phê duyệt
        /// </summary>
        [Required(ErrorMessage = "ApprovePerson không được để trống")]
        public string ApprovePerson { get; set; }
        /// <summary>
        /// Bộ phận/khoa của người phê duyệt/Khoa
        /// </summary>
        [Required(ErrorMessage = "ApproveDept không được để trống")]
        public string ApproveDept { get; set; }
        /// <summary>
        /// Trạng thái phê duyệt
        /// </summary>
        [Required(ErrorMessage = "ApproveStatus không được để trống")]
        public int ApproveStatus { get; set; }
    }
}
