using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.DTO.InputDto
{
    public class SysChargeTemplateDto
    {
        public string sfmbbh { get; set; }

        /// <summary>
        /// Tổ chứcId(bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfmb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfmbmc { get; set; }

        /// <summary>
        /// 0  Tất cả  1 Ngoại trú 2 Nội trú
        /// </summary>
        public byte mzzybz { get; set; }

        /// <summary>
        /// 0 Mẫu toàn viện   1 Mẫu khoa   2 Mẫu cá nhân   
        /// </summary>
        public byte kffw { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
