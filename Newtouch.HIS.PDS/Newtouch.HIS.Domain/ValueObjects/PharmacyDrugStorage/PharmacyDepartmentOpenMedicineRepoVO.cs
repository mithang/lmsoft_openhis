using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class PharmacyDepartmentOpenMedicineRepoVO
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
        /// tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// tổ chức cấp cao nhất Id
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// nhóm lớn số hiệu
        /// </summary>
        public string dlCode { get; set; }

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
        /// trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// tên bộ phận nhà thuốc
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// tên nhóm lớn
        /// </summary>
        public string dlmc { get; set; }
    }
}