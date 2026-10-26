using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Chi tiết gói y lệnh nội trú
    /// </summary>
    [Table("zy_yztcmx")]
    public class InpatientOrderPackageDetailEntity : IEntity<InpatientOrderPackageDetailEntity>
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
        /// MainId
        /// </summary>
        /// <returns></returns>
        public string MainId { get; set; }
        /// <summary>
        /// Số thứ tự nhóm y lệnh cục bộ
        /// </summary>
        /// <returns></returns>
        public int? zh { get; set; }
        /// <summary>
        /// Mã tần suất
        /// </summary>
        /// <returns></returns>
        public string pcCode { get; set; }
        /// <summary>
        /// Số lần thực hiện
        /// </summary>
        /// <returns></returns>
        public int? zxcs { get; set; }
        /// <summary>
        /// Chu kỳ thực hiện
        /// </summary>
        /// <returns></returns>
        public int? zxzq { get; set; }
        /// <summary>
        /// Đơn vị chu kỳ thực hiện -1:Chu kỳ không đều,0:Ngày,1:Giờ,2:Phút.Lưu ý:Khi là-1khi,Mã tuần(zdm)là số(1234567)một vài số bất kỳ trong đó
        /// </summary>
        /// <returns></returns>
        public int? zxzqdw { get; set; }
        /// <summary>
        /// Mã tuần
        /// </summary>
        /// <returns></returns>
        public string zdm { get; set; }
        /// <summary>
        /// Thuốcidm
        /// </summary>
        /// <returns></returns>
        public decimal? idm { get; set; }
        /// <summary>
        /// xmdm
        /// </summary>
        /// <returns></returns>
        public string xmdm { get; set; }
        /// <summary>
        /// xmmc
        /// </summary>
        /// <returns></returns>
        public string xmmc { get; set; }
        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
        /// <summary>
        /// sl
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// Loại đơn vị 0Đơn vị liều lượng,1Đơn vị kho thuốc,2Đơn vị ngoại trú,3Đơn vị nội trú,4Đơn vị nhập hàng(Loại đơn vị của y lệnh chỉ thị là0)
        /// </summary>
        /// <returns></returns>
        public int? dwlb { get; set; }
        /// <summary>
        /// Loại y lệnh cục bộ 0:Y lệnh thuốc,1:Y lệnh kháng sinh,2:Y lệnh kiểm tra,3:Y lệnh xét nghiệm,4:Y lệnh chỉ thị,5:Y lệnh thường , 6 Y lệnh dừng, 7  Thuốc mang về khi xuất viện, 8 Y lệnh chế độ ăn, 9 Y lệnh phẫu thuật   10  Thuốc thảo dược  
        /// </summary>
        /// <returns></returns>
        public int yzlx { get; set; }
        /// <summary>
        /// Liều lượng thuốc
        /// </summary>
        /// <returns></returns>
        public decimal? ypjl { get; set; }
        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// Nội dung dặn dò
        /// </summary>
        /// <returns></returns>
        public string ztnr { get; set; }
        /// <summary>
        /// Nội dung y lệnh
        /// </summary>
        /// <returns></returns>
        public string yznr { get; set; }
        /// <summary>
        /// Cách dùng thuốc
        /// </summary>
        /// <returns></returns>
        public string ypyfdm { get; set; }
        /// <summary>
        /// 0 Chung 1 Tự chuẩn bị
        /// </summary>
        /// <returns></returns>
        public int zbbz { get; set; }
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
        public string bw { get; set; }
        public string zxksdm { get; set; }
        public string nlmd { get; set; }
        /// <summary>
        /// Cờ dài hạn/tạm thời
        /// </summary>
        public string yzlb { get; set; }
    }
}