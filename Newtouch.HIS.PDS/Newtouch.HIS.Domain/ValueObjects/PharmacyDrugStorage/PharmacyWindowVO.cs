using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class PharmacyWindowVO
    {
        /// <summary>
        /// nhà thuốc cửa sổ Id
        /// </summary>
        public int yfckId { get; set; }

        /// <summary>
        /// nhà thuốc cửa sổ mã
        /// </summary>
        public string yfckCode { get; set; }

        /// <summary>
        /// nhà thuốc cửa sổ tên
        /// </summary>
        public string yfckmc { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tổ chức cấp cao nhất Id
        /// </summary>
        public string TopOrganizeId { get; set; }
    }
}
