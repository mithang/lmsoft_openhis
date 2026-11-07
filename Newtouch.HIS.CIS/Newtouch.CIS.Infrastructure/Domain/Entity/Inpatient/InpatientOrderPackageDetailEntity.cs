using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả: Nhập viện y lệnh gói chi tiết
    /// </summary>
    [SugarTable("zy_yztcmx")]
    public class InpatientOrderPackageDetailEntity
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
        /// Số thứ tự nhóm y lệnh tại chỗ
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
        /// Đơn vị chu kỳ thực hiện -1: không theo chu kỳ, 0: ngày, 1: giờ, 2: phút. Lưu ý: khi là -1, mã tuần (zdm) là vài chữ số bất kỳ trong (1234567)
        /// </summary>
        /// <returns></returns>
        public int? zxzqdw { get; set; }
        /// <summary>
        /// Mã tuần
        /// </summary>
        /// <returns></returns>
        public string zdm { get; set; }
        /// <summary>
        /// Idm thuốc
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
        /// Đơn vị liều
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
        /// <summary>
        /// sl
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// Loại đơn vị 0 đơn vị liều, 1 đơn vị kho thuốc, 2 đơn vị ngoại trú, 3 đơn vị nhập viện, 4 đơn vị nhập kho (chỉ hiển thị khi loại đơn vị y lệnh là 0)
        /// </summary>
        /// <returns></returns>
        public int? dwlb { get; set; }
        /// <summary>
        /// Loại y lệnh tại chỗ 0: y lệnh thuốc, 1: y lệnh kháng sinh, 2: y lệnh kiểm tra, 3: y lệnh xét nghiệm, 4: y lệnh chỉ hiển thị, 5: y lệnh thông thường, 6: y lệnh dừng, 7: xuất viện mang thuốc, 8: y lệnh ăn uống, 9: y lệnh phẫu thuật, 10: y lệnh thảo dược Trung y  
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
        /// 0 chung 1 tự chuẩn bị
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
