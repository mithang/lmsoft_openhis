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
        /// Phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// Tên phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffsmc { get; set; }

        /// <summary>
        /// Tính chất tài khoản
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }
    }
}
