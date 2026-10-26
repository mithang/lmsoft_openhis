using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    [Table("xt_dzcfmx")]
    public class DzcfmxEntity : IEntity<DzcfmxEntity>
    {
        /// <summary>
        /// Chi tiết đơn thuốcid
        /// </summary>
        [Key]
        public string cfmxId { get; set; }

        /// <summary>
        /// Tổ chứcid
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Đơn thuốcid
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// Mã hạng mục
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Tên hạng mục
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// Liều lượng mỗi lần điều trị
        /// </summary>
        public int? mczll { get; set; }

        /// <summary>
        /// Liều mỗi lần
        /// </summary>
        public decimal? mcjl { get; set; }

        /// <summary>
        /// Đơn vị liều mỗi lần
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// Mã cách dùng
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// Mã tần suất
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// Tổng lượng
        /// </summary>
        public int? zl { get; set; }

        /// <summary>
        /// Đơn vị tổng lượng
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal? je { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Người tạoid
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiid
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// Số thang
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// Khẩn
        /// </summary>
        public string urgent { get; set; }

        /// <summary>
        /// Mục đích
        /// </summary>
        public string purpose { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Khoa thực hiện
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime? zxsj { get; set; }

        /// <summary>
        /// Mã duy nhất BHYT
        /// </summary>
        public string ybwym { get; set; }

        /// <summary>
        /// Cờ hạn chế sử dụng
        /// </summary>
        public string xzsybz { get; set; }

        /// <summary>
        /// Số ngày
        /// </summary>
        public decimal? ts { get; set; }

        /// <summary>
        /// Lý do sử dụng kháng sinh
        /// </summary>
        public string kssReason { get; set; }

        /// <summary>
        /// Góiid
        /// </summary>
        public string ztId { get; set; }

        /// <summary>
        /// Tên gói
        /// </summary>
        public string ztmc { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public string bwff { get; set; }

        /// <summary>
        /// Loại yêu cầu
        /// </summary>
        public string sqlx { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Tốc độ truyền
        /// </summary>
        public int? ds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zzfbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ghlybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string syncfbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ispsbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string islgbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ztsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? sfzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zysm { get; set; }

    }
}
