
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysPharmacyDepartmentOpenMedicineVO : IEntity<SysPharmacyDepartmentOpenMedicineVO>
    {
        /// <summary>
        /// khóa chính Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// bộ phận nhà thuốc số hiệu
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// nhóm lớn số hiệu
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// trạng thái 0-không hiệu lực  1-còn hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }

    }
}