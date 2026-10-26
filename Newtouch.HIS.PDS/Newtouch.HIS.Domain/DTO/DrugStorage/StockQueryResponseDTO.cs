using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.DrugStorage
{
    /// <summary>
    /// 查询药品库存返回报文
    /// </summary>
    public class StockQueryResponseDTO
    {
        /// <summary>
        /// thuốc thông tin tồn kho
        /// </summary>
        public List<DrugStockInfo> drugStockInfos { get; set; }
    }

    /// <summary>
    /// tồn kho thông tin thuốc
    /// </summary>
    public class DrugStockInfo
    {

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// nhà thuốc lĩnh thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// nhà thuốc lĩnh thuốc tên
        /// </summary>
        public string lyyfmc { get; set; }

        /// <summary>
        /// 做小单位库存数量
        /// </summary>
        public int kscl { get; set; }

        /// <summary>
        /// 做小单位冻结数量
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 做小单位可用数量
        /// </summary>
        public int kysl { get; set; }
    }
}
