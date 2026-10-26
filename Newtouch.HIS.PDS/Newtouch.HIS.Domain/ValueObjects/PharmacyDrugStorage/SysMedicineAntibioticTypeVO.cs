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
        /// loại tên
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Cấp bậc loại
        /// </summary>
        public string typelevel { get; set; }

        /// <summary>
        /// cấp quyền(0 không hạn chế sử dụng thuốc,1 hạn chế sử dụng thuốc,2 đặc biệt sử dụng thuốc)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// cấp quyền tên
        /// </summary>
        public string qxjbmc { get; set; }

        /// <summary>
        /// cấp trên loại Id
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
        /// 1: còn hiệu lực 0: không hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
