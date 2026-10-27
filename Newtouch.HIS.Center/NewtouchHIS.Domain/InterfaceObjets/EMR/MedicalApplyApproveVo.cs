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
        /// 审批Đăng ký号
        /// </summary>
        [Required(ErrorMessage = "ApplyNos không được để trống")]
        
        public string ApplyNos { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        [Required(ErrorMessage = "ApprovePerson không được để trống")]
        public string ApprovePerson { get; set; }
        /// <summary>
        /// 审批人部门/科室
        /// </summary>
        [Required(ErrorMessage = "ApproveDept không được để trống")]
        public string ApproveDept { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        [Required(ErrorMessage = "ApproveStatus không được để trống")]
        public int ApproveStatus { get; set; }
    }
}
