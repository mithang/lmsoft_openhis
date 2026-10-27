using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày：2018-06-28 15:37
    /// Mô tả：Nhập việnY lệnhgóichi tiết
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
        /// bảnđịaY lệnhphútnhómthứ tựsố
        /// </summary>
        /// <returns></returns>
        public int? zh { get; set; }
        /// <summary>
        /// Tần suấtMã
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
        /// Chu kỳ thực hiệnĐơn vị -1:khôngquythìchu kỳ，0：ngày,1：giờ,2：Phút.Lưu ý：khilà-1khi，tuầnMã（zdm）làsốchữ（1234567）trongcủaBất kỳ vàicáisốchữ
        /// </summary>
        /// <returns></returns>
        public int? zxzqdw { get; set; }
        /// <summary>
        /// tuầnMã
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
        /// Đơn vịLoại 0Đơn vị liều，1Đơn vị kho thuốc，2Đơn vị ngoại trú，3Đơn vị nhập viện，4vàohàngĐơn vị(chỉhiển thịY lệnhĐơn vịLoạilà0)
        /// </summary>
        /// <returns></returns>
        public int? dwlb { get; set; }
        /// <summary>
        /// bảnđịaLoại y lệnh 0：ThuốcY lệnh，1：Kháng sinhY lệnh，2：Kiểm traY lệnh，3：Xét nghiệmY lệnh，4：chỉhiển thịY lệnh，5：thông thườngY lệnh ， 6 DừngY lệnh， 7  Xuất việnmang thuốc， 8 ăn uốngY lệnh， 9 Phẫu thuậtY lệnh   10  trongthảo dược  
        /// </summary>
        /// <returns></returns>
        public int yzlx { get; set; }
        /// <summary>
        /// ThuốcLiều lượng
        /// </summary>
        /// <returns></returns>
        public decimal? ypjl { get; set; }
        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// Dặn dònội dung
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
        /// 0 Chung 1 tựchuẩn bị
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
        /// dài hạn/tạm thờiCờ
        /// </summary>
        public string yzlb { get; set; }
    }
}
