using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// phiếu cứ nội dung
    /// </summary>
    public class BYDjInfoDTO
    {
        /// <summary>
        /// Nhập khoKhoa phòng
        /// </summary>
        public string rkbm { get; set; }
        public string yfbm { get; set; }
        public string ksbm { get; set; }
        public string djh { get; set; }
        public string issavesubmit { get; set; }
        public string bqbm { get; set; }
        public string shzt { get; set; }
        public string isdeldj { get; set; }
        /// <summary>
        /// chi tiết
        /// </summary>
        public List<DjDetailDTO> mx { get; set; }
    }

    /// <summary>
    /// phiếu cứ chi tiết
    /// </summary>
    public class DjDetailDTO
    {
        /// <summary>
        /// đăng ký lĩnh phiếu chi tiết id
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }
        

        /// <summary>
        /// Giá bán buôn  Khoa phòng Đơn vị
        /// </summary>
        public string pfj { get; set; }
        /// <summary>
        ///Quy cách
        /// </summary>
        public string  gg { get; set; }
        /// <summary>
        /// Giá bán lẻ  Khoa phòng Đơn vị
        /// </summary>
        public string lsj { get; set; }

        /// <summary>
        /// vào giá  Đơn vị kho thuốc
        /// </summary>
        public string jj { get; set; }

        /// <summary>
        /// Đơn vị kho thuốc Giá bán buôn
        /// </summary>
        public string ykpfj { get; set; }

        /// <summary>
        /// Đơn vị kho thuốc Giá bán lẻ
        /// </summary>
        public string yklsj { get; set; }

        /// <summary>
        /// Tổng số tiền  vào giá Tổng số tiền
        /// </summary>
        public string zje { get; set; }

        /// <summary>
        /// Số lượng Khoa phòng Đơn vị Số lượng và Xuất kho chuyển hóa vì con tương ứng
        /// </summary>
        public string sl { get; set; }

        /// <summary>
        /// Xuất khoKhoa phòng Tồn kho  Đơn vị nhỏ nhất
        /// </summary>
        public int? ckbmkc { get; set; }

        /// <summary>
        /// Xuất kho Đơn vị(Tên đơn vị)
        /// </summary>
        public string ckdw { get; set; }

        /// <summary>
        /// Nhập khoKhoa phòng Tồn kho  Đơn vị nhỏ nhất
        /// </summary>
        public int? rkbmkc { get; set; }

        /// <summary>
        /// Nhập khoKhoa phòng chuyển hóa vì con
        /// </summary>
        public int? rkzhyz { get; set; }

        /// <summary>
        /// Nhập kho Đơn vị(Tên đơn vị)
        /// </summary>
        public string rkdw { get; set; }
        public string dw { get; set; }
        /// <summary>
        /// Xuất khoKhoa phòng chuyển hóa vì con
        /// </summary>
        public int? ckzhyz { get; set; }

        /// <summary>
        /// Nơi sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// khấu tỷ lệ
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        /// Ngày lập phiếu
        /// </summary>
        public string kprq { get; set; }

        /// <summary>
        /// sinh sản xuất Ngày
        /// </summary>
        public string scrq { get; set; }

        /// <summary>
        /// trả hàng nguyên nhân
        /// </summary>
        public string thyy { get; set; }
        /// <summary>
        /// Đơn vị kho thuốc Giá bán buôn
        /// </summary>
        public decimal pfjze { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksbm { get; set; }
        /// <summary>
        /// Thuốc Loại
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public string yxq { get; set; }
        public string zhyz { get; set; }
        public string yplb { get; set; }
    }
}
