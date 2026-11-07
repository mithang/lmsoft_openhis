using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả: Nhập viện y lệnh gói
    /// </summary>
    [SugarTable("zy_yztc")]
    public class InpatientOrderPackageEntity
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
        /// Kiểu y lệnh: 0: y lệnh thuốc, 1: y lệnh kháng sinh, 2: y lệnh kiểm tra, 3: y lệnh xét nghiệm, 4: y lệnh chỉ hiển thị, 5: y lệnh thông thường, 6: y lệnh dừng, 7: xuất viện mang thuốc, 8: y lệnh ăn uống, 9: y lệnh phẫu thuật, 10: y lệnh thảo dược Trung y 
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
