using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Nhập việnY lệnhgói
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
        /// Kiểu y lệnh: 0:ThuốcY lệnh,1:Kháng sinhY lệnh,2:Kiểm traY lệnh,3:Xét nghiệmY lệnh,4:chỉhiển thịY lệnh,5:thông thườngY lệnh , 6 DừngY lệnh, 7  Xuất việnmang thuốc, 8 ăn uốngY lệnh, 9 Phẫu thuậtY lệnh   10  trongthảo dược 
        /// </summary>
        /// <returns></returns>
        public int tclx { get; set; }
        /// <summary>
        /// 0 Cá nhân 1 Khoa 2 Toàn viện
        /// </summary>
        /// <returns></returns>
        public int tcfw { get; set; }
        /// <summary>
        /// Khu bệnhMã
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// KhoaMã
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// Bác sĩMã
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
