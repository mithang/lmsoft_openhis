using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.Settlement
{
    public class GetSfDlMc
    {
        public string Id { get; set; }

        /// <summary>
        /// Cơ sở y tế
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Loại, liên kết với mục từ điển ChargeCateType
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Mã nhóm thu phí lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        public string dlmc { get; set; }
    }
}
