using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRHomePage
{
    public class SysAnesListDto
    {
        public string OrganizeId { get; set; }
        /// <summary>
        /// Mã gây mê
        /// </summary>
        public string AnesCode { get; set; }
        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        public string AnesName { get; set; }
        /// <summary>
        /// Mã ghi nhớ
        /// </summary>
        public string Aneszjm { get; set; }
    }
}
