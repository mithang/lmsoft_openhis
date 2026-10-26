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
        /// Bảng dữ liệu liên kết, mặc định khi trống
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
        /// Cấp thư mục, cấp ban đầu là 1
        /// </summary>
        public string MenuLev { get; set; }
        /// <summary>
        /// Tên cấp thư mục, mặc định cấp một là thời gian, phân tách bằng ※
        /// </summary>
        public string MenuLevName { get; set; }
        public string ParentId { get; set; }
        public string IsRoot { get; set; }
        /// <summary>
        /// Cờ mẫu ngoại trú: 1 Ngoại trú 
        /// </summary>
        public string mzbz { get; set; }
    }
}
