using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class BaseBllxVO
    {
        public string Id { get; set; }
        public string bllx { get; set; }
        public string bllxmc { get; set; }
        public string RelDutys { get; set; }
        public string RelDutysDesc { get; set; }
        /// <summary>
        /// Mã ngắn loại bệnh án
        /// </summary>
        public string bllxcode { get; set; }
        /// <summary>
        /// Khi bảng dữ liệu liên quan trống thì mặc định
        /// </summary>
        public string relTB { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        /// <summary>
        /// Cấp ban đầu của cấp thư mục1
        /// </summary>
        public string MenuLev { get; set; }
        /// <summary>
        /// Tên cấp thư mục mặc định cấp một là thời gian, phân tách bằng※phân tách
        /// </summary>
        public string MenuLevName { get; set; }
        public string ParentId { get; set; }
        public string IsRoot { get; set; }
        /// <summary>
        /// Dấu hiệu mẫu ngoại trú 1 Ngoại trú 
        /// </summary>
        public string mzbz { get; set; }
    }
}
