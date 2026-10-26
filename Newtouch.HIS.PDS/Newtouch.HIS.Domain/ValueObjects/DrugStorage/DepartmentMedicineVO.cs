using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// hệ thống bộ phận thuốc
    /// </summary>
    public class DepartmentMedicineVO
    {
        #region 系统药品xt_yp
        /// <summary>
        /// loại thuốc
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
        /// số lượng đóng gói
        /// </summary>
        public decimal bzs { get; set; }

        /// <summary>
        /// đơn vị kho thuốc đơn vị đóng gói
        /// </summary>
        public string ykdw { get; set; }
        /// <summary>
        /// đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }
        /// <summary>
        /// nhà sản xuất
        /// </summary>
        public string sccj { get; set; }
        #endregion

        #region 系统药品库存 xt_yp_kcxx
        /// <summary>
        /// tồn kho Id
        /// </summary>
        public string kcId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int xykc { get; set; }
        /// <summary>
        /// tồn kho khả dụng
        /// </summary>
        public int kykc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xykcstr { get; set; }
        /// <summary>
        /// 可用库存大小单位
        /// </summary>
        public string kykcstr { get; set; }
        /// <summary>
        /// số lượng tồn kho
        /// </summary>
        public int kcsl { get; set; }
        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }
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
        public DateTime yxq { get; set; }
        #endregion

        #region 出入库明细xt_yp_crkmx 
        /// <summary>
        /// ngày sản xuất
        /// </summary>
        public DateTime scrq { get; set; }
        /// <summary>
        /// giá nhập
        /// </summary>
        public decimal jj { get; set; }
        /// <summary>
        /// giá bán buôn
        /// </summary>
        public decimal pfj { get; set; }
        /// <summary>
        /// giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }
        /// <summary>
        /// tỷ lệ chiết khấu
        /// </summary>
        public decimal kl { get; set; }
        #endregion

        #region 药品属性xt_ypsx
        /// <summary>
        /// số giấy phép
        /// </summary>
        public string pzwh { get; set; }
        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }
        #endregion

    }
}
