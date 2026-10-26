using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;

namespace Newtouch.OR.ManageSystem.Domain.ValueObjects
{
    public class SysChargeTemplateInfoVM
    {
        /// <summary>
        /// Thực thể mẫu
        /// </summary>
        public SysChargeTemplateDto TemplateEntity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<SysChargeItemTemplateVO> SysList { get; set; }
    }
}
