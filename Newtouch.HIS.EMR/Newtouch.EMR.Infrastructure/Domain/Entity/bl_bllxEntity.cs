using System;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{

    [SugarTable("bl_bllx")]
    public  class bl_bllxEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// Định danh loại bệnh án
        /// </summary>
        public string bllx { get; set; }
        public string bllxmc { get; set; }       
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Chức vụ liên kết
        /// </summary>
        public string RelDutys { get; set; }
        /// <summary>
        /// Mã ngắn loại bệnh án
        /// </summary>
        public string bllxcode { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// Bảng dữ liệu liên kết, mặc định khi trống
        /// </summary>
        public string relTB { get; set; }
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
        /// Định danh mẫu ngoại trú, 1 là ngoại trú
        /// </summary>
        public string mzbz { get; set; }
    }
}
