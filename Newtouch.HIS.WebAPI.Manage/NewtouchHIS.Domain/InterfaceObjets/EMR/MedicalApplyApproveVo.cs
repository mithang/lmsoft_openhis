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
        /// số đăng ký phê duyệt
        /// </summary>
        [Required(ErrorMessage = "ApplyNos不可为空")]
        
        public string ApplyNos { get; set; }
        /// <summary>
        /// người phê duyệt
        /// </summary>
        [Required(ErrorMessage = "ApprovePerson不可为空")]
        public string ApprovePerson { get; set; }
        /// <summary>
        /// khoa của người phê duyệt/khoa
        /// </summary>
        [Required(ErrorMessage = "ApproveDept不可为空")]
        public string ApproveDept { get; set; }
        /// <summary>
        /// trạng thái phê duyệt
        /// </summary>
        [Required(ErrorMessage = "ApproveStatus不可为空")]
        public int ApproveStatus { get; set; }
    }
}
