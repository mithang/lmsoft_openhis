using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutOrInStoredOperate
{
    /// <summary>
    /// đơn chứng nội dung
    /// </summary>
    public class DjInfoDTO
    {
        /// <summary>
        /// loại chứng từ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// số chứng từ
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// phương thức phát thuốc
        /// </summary>
        public string fyfs { get; set; }

        /// <summary>
        /// bộ phận xuất kho
        /// </summary>
        public string ckbm { get; set; }

        /// <summary>
        /// bộ phận nhập kho
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// chi tiết
        /// </summary>
        public List<DjDetailDTO> mx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }
    }

    /// <summary>
    /// chi tiết chứng từ
    /// </summary>
    public class DjDetailDTO
    {
        /// <summary>
        /// chi tiết phiếu lĩnh id
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// giá bán buôn  đơn vị bộ phận
        /// </summary>
        public decimal pfj { get; set; }

        /// <summary>
        /// giá bán lẻ  đơn vị bộ phận
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// giá nhập  đơn vị kho thuốc
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// đơn vị kho thuốc giá bán buôn
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// đơn vị kho thuốc giá bán lẻ
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// tổng số tiền  tổng tiền giá nhập
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// số lượng số lượng đơn vị bộ phận và xuất kho hệ số chuyển đổi tương ứng
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// bộ phận xuất kho tồn kho  đơn vị nhỏ nhất
        /// </summary>
        public int? ckbmkc { get; set; }

        /// <summary>
        /// đơn vị xuất kho (tên đơn vị)
        /// </summary>
        public string ckdw { get; set; }

        /// <summary>
        /// bộ phận nhập kho tồn kho  đơn vị nhỏ nhất
        /// </summary>
        public int? rkbmkc { get; set; }

        /// <summary>
        /// bộ phận nhập kho hệ số chuyển đổi
        /// </summary>
        public int? rkzhyz { get; set; }

        /// <summary>
        /// nhập kho đơn vị (tên đơn vị)
        /// </summary>
        public string rkdw { get; set; }

        /// <summary>
        /// bộ phận xuất kho hệ số chuyển đổi
        /// </summary>
        public int? ckzhyz { get; set; }

        /// <summary>
        /// nơi sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// tỷ lệ chiết khấu
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        /// ngày lập hóa đơn
        /// </summary>
        public DateTime? kprq { get; set; }

        /// <summary>
        /// ngày sản xuất
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// lý do trả hàng
        /// </summary>
        public string thyy { get; set; }
        /// <summary>
        /// đơn vị kho thuốc giá bán buôn
        /// </summary>
        public decimal pfjze { get; set; }

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
