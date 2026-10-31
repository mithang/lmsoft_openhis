using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysMedicineAntibioticTypeVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Cấp loại
        /// </summary>
        public string typelevel { get; set; }

        /// <summary>
        /// Cấp quyền(0 Thuốc không hạn chế sử dụng,1 Thuốc hạn chế sử dụng,2 Thuốc sử dụng đặc biệt)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// Tên cấp quyền
        /// </summary>
        public string qxjbmc { get; set; }

        /// <summary>
        /// Loại cấp trênId
        /// </summary>
        public string parentId { get; set; }

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
        /// 1:Hiệu lực 0:Không hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
