using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysItemsDetailVO : IEntity<SysItemsDetailVO>
    {
        /// <summary>
        /// khóa chính chi tiết
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// chính bảng khóa chính
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// cấp cha
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// khóa chính người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// mã sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
