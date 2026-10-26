using Newtouch.Infrastructure.EF.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc chi tiết nhập/xuất kho
    /// </summary>
    [Table("xt_yp_crkmx")]
    public class SysMedicineStorageIOReceiptDetailEntity : IEntity<SysMedicineStorageIOReceiptDetailEntity>
    {
        /// <summary>
        /// chi tiết nhập/xuất kho ID
        /// </summary>
        [Key]
        public string crkmxId { get; set; }

        /// <summary>
        /// từ bảng XT_YP_CRKDJK của khóa chính
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// chi tiết phiếu lĩnh ID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// ngày lập hóa đơn
        /// </summary>
        public DateTime? Kprq { get; set; }

        /// <summary>
        /// ngày nhận hóa đơn
        /// </summary>
        public DateTime? Dprq { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// giá bán buôn và zhyz và sl tương ứng
        /// </summary>
        public decimal Pfj { get; set; }

        /// <summary>
        /// giá bán lẻ và zhyz và sl tương ứng
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// tổng số tiền
        /// </summary>
        public decimal Zje { get; set; }

        /// <summary>
        /// số lượng số lượng đơn vị bộ phận
        /// </summary>
        public int Sl { get; set; }

        /// <summary>
        /// nhập kho hệ số chuyển đổi
        /// </summary>
        public int Rkzhyz { get; set; }

        /// <summary>
        /// bộ phận nhập kho tồn kho
        /// </summary>
        public int Rkbmkc { get; set; }

        /// <summary>
        /// nhập kho đơn vị (tên đơn vị)
        /// </summary>
        public string rkdw { get; set; }

        /// <summary>
        /// xuất kho hệ số chuyển đổi
        /// </summary>
        public int Ckzhyz { get; set; }

        /// <summary>
        /// bộ phận xuất kho tồn kho
        /// </summary>
        public int Ckbmkc { get; set; }

        /// <summary>
        /// đơn vị xuất kho (tên đơn vị)
        /// </summary>
        public string ckdw { get; set; }

        /// <summary>
        /// giao diện
        /// </summary>
        public string Wg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zbbz { get; set; }

        /// <summary>
        /// giấy chứng nhận đăng ký nhập kho
        /// </summary>
        public string jkzcz { get; set; }

        /// <summary>
        /// giấy chứng nhận đạt chuẩn
        /// </summary>
        public string hgzm { get; set; }

        /// <summary>
        /// nghiệm thu kết quả
        /// </summary>
        public string ysjg { get; set; }

        /// <summary>
        /// lý do trả hàng
        /// </summary>
        public string Thyy { get; set; }

        /// <summary>
        /// xử lý kết quả
        /// </summary>
        public string Cljg { get; set; }

        /// <summary>
        /// ngày sản xuất
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// tỷ lệ chiết khấu
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        /// giá nhập giá nhập theo đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// nơi sản xuất
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// sắp xếp
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
        /// tổng tiền giá nhập giá nhập theo đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? pfjze { get; set; }

        /// <summary>
        /// mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// có tách lẻ không
        /// 1: có
        /// 2: không
        /// </summary>
        public int? sfcl { get; set; }

    }
}
