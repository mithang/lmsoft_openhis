using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    public class SysCashPaymenVEntity
    {
        public int xjzffsbh { get; set; }

        /// <summary>
        /// hiệnkimPhương thức thanh toán
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// hiệnkimPhương thức thanh toánTên
        /// </summary>
        public string xjzffsmc { get; set; }

        /// <summary>
        /// Tài khoảnTính chất
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// tạoNgười dùngID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa cuốiNgười dùngID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }
    }
}
