using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Gói y lệnh nội trú
    /// </summary>
    [Table("zy_yztc")]
    public class InpatientOrderPackageEntity : IEntity<InpatientOrderPackageEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// tcmc
        /// </summary>
        /// <returns></returns>
        public string tcmc { get; set; }
        /// <summary>
        /// Loại y lệnh: 0:Y lệnh thuốc,1:Y lệnh kháng sinh,2:Y lệnh kiểm tra,3:Y lệnh xét nghiệm,4:Y lệnh chỉ thị,5:Y lệnh thường , 6 Y lệnh dừng, 7  Thuốc mang về khi xuất viện, 8 Y lệnh chế độ ăn, 9 Y lệnh phẫu thuật   10  Thuốc thảo dược 
        /// </summary>
        /// <returns></returns>
        public int tclx { get; set; }
        /// <summary>
        /// 0 Cá nhân 1 Khoa 2 Toàn viện
        /// </summary>
        /// <returns></returns>
        public int tcfw { get; set; }
        /// <summary>
        /// Mã khu bệnh
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// Mã bác sĩ
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}