using Newtouch.Infrastructure.EF.Attributes;
using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết xuất nhập kho thuốc hệ thống
    /// </summary>
    [Table("xt_yp_crkmx")]
    [SugarTable("xt_yp_crkmx")]
    public class SysMedicineStorageIOReceiptDetailEntity : IEntity<SysMedicineStorageIOReceiptDetailEntity>
    {
        /// <summary>
        /// Chi tiết xuất nhập khoID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string crkmxId { get; set; }

        /// <summary>
        /// Từ bảng XT_YP_CRKDJKkhóa chính của
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// Chi tiết đơn lĩnhID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// Ngày lập hóa đơn
        /// </summary>
        public DateTime? Kprq { get; set; }

        /// <summary>
        /// Ngày nhận phiếu
        /// </summary>
        public DateTime? Dprq { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// Giá bán buôn vàzhyzvàslTương ứng
        /// </summary>
        public decimal Pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ vàzhyzvàslTương ứng
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal Zje { get; set; }

        /// <summary>
        /// Số lượng Số lượng đơn vị bộ phận
        /// </summary>
        public int Sl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi nhập kho
        /// </summary>
        public int Rkzhyz { get; set; }

        /// <summary>
        /// Tồn kho bộ phận nhập kho
        /// </summary>
        public int Rkbmkc { get; set; }

        /// <summary>
        /// Đơn vị nhập kho(Tên đơn vị)
        /// </summary>
        public string rkdw { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi xuất kho
        /// </summary>
        public int Ckzhyz { get; set; }

        /// <summary>
        /// Tồn kho bộ phận xuất kho
        /// </summary>
        public int Ckbmkc { get; set; }

        /// <summary>
        /// Đơn vị xuất kho(Tên đơn vị)
        /// </summary>
        public string ckdw { get; set; }

        /// <summary>
        /// Bề ngoài
        /// </summary>
        public string Wg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zbbz { get; set; }

        /// <summary>
        /// Giấy đăng ký nhập kho
        /// </summary>
        public string jkzcz { get; set; }

        /// <summary>
        /// Giấy chứng nhận đạt chuẩn
        /// </summary>
        public string hgzm { get; set; }

        /// <summary>
        /// Kết quả nghiệm thu
        /// </summary>
        public string ysjg { get; set; }

        /// <summary>
        /// Lý do trả hàng
        /// </summary>
        public string Thyy { get; set; }

        /// <summary>
        /// Kết quả xử lý
        /// </summary>
        public string Cljg { get; set; }

        /// <summary>
        /// Ngày sản xuất
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// Tỷ lệ khấu trừ
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        /// Giá nhập Giá nhập đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// Nơi sản xuất
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Tổng số tiền giá nhập Giá nhập đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? pfjze { get; set; }

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1: Có
        /// 2: Không
        /// </summary>
        public int? sfcl { get; set; }

    }
}
