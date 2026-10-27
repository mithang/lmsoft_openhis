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
        /// Khóa chínhId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Số hiệu bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tổ chức cấp cao nhấtId
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Số hiệu loại lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Tên bộ phận nhà thuốc
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }
    }
}