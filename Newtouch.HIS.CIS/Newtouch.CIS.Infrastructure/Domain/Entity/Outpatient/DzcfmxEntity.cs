using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    [SugarTable("xt_dzcfmx")]
    public class DzcfmxEntity
    {
        /// <summary>
        /// Đơn thuốc chi tiết id
        /// </summary>
        [Key]
        public string cfmxId { get; set; }

        /// <summary>
        /// Tổ chức id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Đơn thuốc id
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// Mục Mã
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Tên mục
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
        /// Liều mỗi lần điều trị
        /// </summary>
        public int? mczll { get; set; }

        /// <summary>
        /// mỗi lần Liều lượng
        /// </summary>
        public decimal? mcjl { get; set; }

        /// <summary>
        /// mỗi lần Đơn vị liều
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// Cách dùng Mã
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// Tần suất Mã
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
        /// Tổng lượng Đơn vị
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
        /// tạo Người dùng id
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa cuối Người dùng id
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
        /// dán số
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// thêm gấp
        /// </summary>
        public string urgent { get; set; }

        /// <summary>
        /// mắt của
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
        /// Bảo hiểm y tế duy nhất một mã
        /// </summary>
        public string ybwym { get; set; }

        /// <summary>
        /// hạn chế Sử dụng Cờ
        /// </summary>
        public string xzsybz { get; set; }

        /// <summary>
        /// Số ngày
        /// </summary>
        public decimal? ts { get; set; }

        /// <summary>
        /// Kháng sinh Sử dụng nguyên nhân
        /// </summary>
        public string kssReason { get; set; }

        /// <summary>
        /// Bộ mẫu id
        /// </summary>
        public string ztId { get; set; }

        /// <summary>
        /// Tên bộ mẫu
        /// </summary>
        public string ztmc { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public string bwff { get; set; }

        /// <summary>
        /// Loại đăng ký
        /// </summary>
        public string sqlx { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// tốc độ truyền
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
