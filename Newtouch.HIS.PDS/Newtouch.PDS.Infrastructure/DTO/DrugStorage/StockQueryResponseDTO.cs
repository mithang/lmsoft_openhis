using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.DrugStorage
{
    /// <summary>
    /// Bản tin trả về truy vấn tồn kho thuốc
    /// </summary>
    public class StockQueryResponseDTO
    {
        /// <summary>
        /// Thông tin tồn kho thuốc
        /// </summary>
        public List<DrugStockInfo> drugStockInfos { get; set; }
    }

    /// <summary>
    /// Thông tin thuốc tồn kho
    /// </summary>
    public class DrugStockInfo
    {

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Nhà thuốc nhận thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Tên nhà thuốc nhận thuốc
        /// </summary>
        public string lyyfmc { get; set; }

        /// <summary>
        /// Làm Số lượng tồn kho đơn vị nhỏ
        /// </summary>
        public int kscl { get; set; }

        /// <summary>
        /// Làm Số lượng đóng băng đơn vị nhỏ
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// Làm Số lượng khả dụng đơn vị nhỏ
        /// </summary>
        public int kysl { get; set; }
    }
}
