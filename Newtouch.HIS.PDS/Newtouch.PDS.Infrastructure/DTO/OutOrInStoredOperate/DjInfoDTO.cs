using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutOrInStoredOperate
{
    /// <summary>
    /// Nội dung chứng từ
    /// </summary>
    public class DjInfoDTO
    {
        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// Phương thức phát thuốc
        /// </summary>
        public string fyfs { get; set; }

        /// <summary>
        /// Bộ phận xuất kho
        /// </summary>
        public string ckbm { get; set; }

        /// <summary>
        /// Bộ phận nhập kho
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// Chi tiết
        /// </summary>
        public List<DjDetailDTO> mx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }
    }

    /// <summary>
    /// Chi tiết chứng từ
    /// </summary>
    public class DjDetailDTO
    {
        /// <summary>
        /// Chi tiết đơn lĩnhid
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Giá bán buôn  Đơn vị bộ phận
        /// </summary>
        public decimal pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ  Đơn vị bộ phận
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá nhập  Đơn vị kho thuốc
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// Giá bán buôn đơn vị kho thuốc
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị kho thuốc
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// Tổng số tiền  Tổng số tiền giá nhập
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// Số lượng Số lượng đơn vị bộ phận tương ứng với hệ số quy đổi xuất kho
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Tồn kho bộ phận xuất kho  Đơn vị nhỏ nhất
        /// </summary>
        public int? ckbmkc { get; set; }

        /// <summary>
        /// Đơn vị xuất kho(Tên đơn vị)
        /// </summary>
        public string ckdw { get; set; }

        /// <summary>
        /// Tồn kho bộ phận nhập kho  Đơn vị nhỏ nhất
        /// </summary>
        public int? rkbmkc { get; set; }

        /// <summary>
        /// Hệ số quy đổi bộ phận nhập kho
        /// </summary>
        public int? rkzhyz { get; set; }

        /// <summary>
        /// Đơn vị nhập kho(Tên đơn vị)
        /// </summary>
        public string rkdw { get; set; }

        /// <summary>
        /// Hệ số quy đổi bộ phận xuất kho
        /// </summary>
        public int? ckzhyz { get; set; }

        /// <summary>
        /// Nơi sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Tỷ lệ khấu trừ
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        /// Ngày lập hóa đơn
        /// </summary>
        public DateTime? kprq { get; set; }

        /// <summary>
        /// Ngày sản xuất
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// Lý do trả hàng
        /// </summary>
        public string thyy { get; set; }
        /// <summary>
        /// Giá bán buôn đơn vị kho thuốc
        /// </summary>
        public decimal pfjze { get; set; }

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
