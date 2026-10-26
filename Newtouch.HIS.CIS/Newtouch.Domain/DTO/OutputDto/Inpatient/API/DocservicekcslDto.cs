using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto.Inpatient.API
{

    public class DocservicekcslRequestDto {
        public string ypCode { get; set; }
        public string lyyf { get; set; }
    }
    public class DocservicekcslResultDto
    {
        public string ResultCode { get; set; }
        public string ResultMsg { get; set; }
        public bool IsSucceed { get; set; }
        public StockQueryResponseDTO Data { get; set; }
    }

    /// <summary>
    /// Thông báo trả về khi tra cứu tồn kho thuốc
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
        /// Nhà thuốc lĩnh thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Làm số lượng tồn kho đơn vị nhỏ
        /// </summary>
        public int kscl { get; set; }

        /// <summary>
        /// Làm số lượng đóng băng đơn vị nhỏ
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// Làm số lượng khả dụng đơn vị nhỏ
        /// </summary>
        public int kysl { get; set; }
    }
}
