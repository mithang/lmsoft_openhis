using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class MrDeptVO
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string organizeId { get; set; }
        /// <summary>
        /// Khoa bệnh ánID
        /// </summary>
        public string baksId { get; set; }
        /// <summary>
        /// Tên khoa bệnh án
        /// </summary>
        public string baksName { get; set; }
        /// <summary>
        /// hisMã số khoa
        /// </summary>
        public string hisdept { get; set; }
        /// <summary>
        /// hisTên khoa
        /// </summary>
        public string hisdeptname { get; set; }
    }
}
