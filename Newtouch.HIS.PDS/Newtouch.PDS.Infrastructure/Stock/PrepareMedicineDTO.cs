using FrameworkBase.MultiOrg.DmnService;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// Nội dung chứng từ
    /// </summary>
    public class PrepareMedicineDTO : RequestBase
    {
        /// <summary>
        /// Bộ phận nhập kho
        /// </summary>
        public string OrganizeId { get; set; }
        public string yhgh { get; set; }
        /// <summary>
        /// Chi tiết
        /// </summary>
        public BYDjInfoDTO yplist { get; set; }
    }

    /// <summary>
    /// Nội dung chứng từ
    /// </summary>
    public class BYDjInfoDTO
    {
        /// <summary>
        /// Bộ phận nhập kho
        /// </summary>
        public string rkbm { get; set; }
        public string yfbm { get; set; }
        public string ksbm { get; set; }
        public string djh { get; set; }
        public string bqbm { get; set; }
        public string issavesubmit { get; set; }
        public string shzt { get; set; }
        /// <summary>
        /// Chi tiết
        /// </summary>
        public List<DjDetailDTO> mx { get; set; }
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
        /// Giá bán buôn  Đơn vị bộ phận
        /// </summary>
        public string pfj { get; set; }
        /// <summary>
        ///Quy cách
        /// </summary>
        public string gg { get; set; }
        /// <summary>
        /// Giá bán lẻ  Đơn vị bộ phận
        /// </summary>
        public string lsj { get; set; }

        /// <summary>
        /// Giá nhập  Đơn vị kho thuốc
        /// </summary>
        public string jj { get; set; }

        /// <summary>
        /// Giá bán buôn đơn vị kho thuốc
        /// </summary>
        public string ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị kho thuốc
        /// </summary>
        public string yklsj { get; set; }

        /// <summary>
        /// Tổng số tiền  Tổng số tiền giá nhập
        /// </summary>
        public string zje { get; set; }

        /// <summary>
        /// Số lượng Số lượng đơn vị bộ phận tương ứng với hệ số quy đổi xuất kho
        /// </summary>
        public string sl { get; set; }

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
        public string kprq { get; set; }

        /// <summary>
        /// Ngày sản xuất
        /// </summary>
        public string scrq { get; set; }

        /// <summary>
        /// Lý do trả hàng
        /// </summary>
        public string thyy { get; set; }
        /// <summary>
        /// Giá bán buôn đơn vị kho thuốc
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
        /// Loại thuốc
        /// </summary>
        public string yplb { get; set; }

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
        public string yxq { get; set; }
        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public string zhyz { get; set; }
    }
}
