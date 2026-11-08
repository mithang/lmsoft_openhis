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
    /// Tra cứu Thuốc Tồn khoQuay lại báo văn
    /// </summary>
    public class StockQueryResponseDTO
    {
        /// <summary>
        /// Thuốc Tồn kho thông tin
        /// </summary>
        public List<DrugStockInfo> drugStockInfos { get; set; }

    }

    /// <summary>
    /// Tồn kho Thuốc thông tin
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
        /// lĩnh thuốc Nhà thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// làm nhỏ Đơn vị Tồn kho Số lượng
        /// </summary>
        public int kscl { get; set; }

        /// <summary>
        /// làm nhỏ Đơn vị đông kết Số lượng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// làm nhỏ Đơn vị có thể dùng Số lượng
        /// </summary>
        public int kysl { get; set; }
    }
}
