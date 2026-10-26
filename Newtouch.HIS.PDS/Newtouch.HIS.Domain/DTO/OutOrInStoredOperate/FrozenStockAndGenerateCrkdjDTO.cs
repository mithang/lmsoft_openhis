using Newtouch.HIS.Domain.Entity.V;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutOrInStoredOperate
{
    /// <summary>
    /// 冻结库存并生成单据信息处理过程参数
    /// </summary>
    public class FrozenStockAndGenerateCrkdjDTO
    {

        /// <summary>
        /// hiện tại bộ phận nhà thuốc bộ phận xuất kho
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// tổ chức
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// người thao tác
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// bộ phận nhập kho
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// thông tin thuốc cần đóng băng
        /// </summary>
        public List<FrozenedMedicineInfoDTO> frozenseMedicines { get; set; }
    }

    /// <summary>
    /// thông tin thuốc cần đóng băng
    /// </summary>
    public class FrozenedMedicineInfoDTO
    {

        /// <summary>
        /// 总共需要冻结的数量  đơn vị nhỏ nhất
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// chi tiết phiếu lĩnh ID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// hệ số chuyển đổi hiện là hệ số chuyển đổi đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// số lượng đóng gói
        /// </summary>
        public int bzs { get; set; }

        /// <summary>
        /// tồn kho khả dụng đơn vị nhỏ nhất
        /// </summary>
        public int kykc { get; set; }

        /// <summary>
        /// giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal zxdwjj { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất giá bán buôn
        /// </summary>
        public decimal zxdwpfj { get; set; }

        /// <summary>
        /// giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal zxdwlsj { get; set; }

        /// <summary>
        /// 冻结药品批次信息
        /// </summary>
        public List<FrozenBatchesDetailVEntity> frozenedMedicineBatchs { get; set; }
    }

}
