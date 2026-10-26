using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.DrugStorage
{
    /// <summary>
    /// Bản tin trả về khi truy vấn tồn kho thuốc
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
        /// Số lượng tồn kho theo đơn vị nhỏ
        /// </summary>
        public int kscl { get; set; }

        /// <summary>
        /// Số lượng đóng băng theo đơn vị nhỏ
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// Số lượng khả dụng theo đơn vị nhỏ
        /// </summary>
        public int kysl { get; set; }
    }
}
