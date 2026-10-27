using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thuốc bộ phận hệ thống
    /// </summary>
    public class DepartmentMedicineVO
    {
        #region Thuốc hệ thốngxt_yp
        /// <summary>
        /// Loại thuốc
        /// </summary>
        public string yplb { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ypmc { get; set; }
        
        /// <summary>
        /// Số lượng đóng gói
        /// </summary>
        public decimal bzs { get; set; }

        /// <summary>
        /// Đơn vị kho thuốc Đơn vị đóng gói
        /// </summary>
        public string ykdw { get; set; }
        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }
        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }
        #endregion

        #region Tồn kho thuốc hệ thống xt_yp_kcxx
        /// <summary>
        /// Tồn khoId
        /// </summary>
        public string kcId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int xykc { get; set; }
        /// <summary>
        /// Tồn kho khả dụng
        /// </summary>
        public int kykc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xykcstr { get; set; }
        /// <summary>
        /// Đơn vị lớn nhỏ tồn kho khả dụng
        /// </summary>
        public string kykcstr { get; set; }
        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        public int kcsl { get; set; }
        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }
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
        public DateTime yxq { get; set; }
        #endregion

        #region Chi tiết xuất nhập khoxt_yp_crkmx 
        /// <summary>
        /// Ngày sản xuất
        /// </summary>
        public DateTime scrq { get; set; }
        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal jj { get; set; }
        /// <summary>
        /// Giá bán buôn
        /// </summary>
        public decimal pfj { get; set; }
        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }
        /// <summary>
        /// Tỷ lệ khấu trừ
        /// </summary>
        public decimal kl { get; set; }
        #endregion

        #region Thuộc tính thuốcxt_ypsx
        /// <summary>
        /// Số văn bản phê duyệt
        /// </summary>
        public string pzwh { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }
        #endregion

    }
}
