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
    /// Tra cứuThuốcTồn khoQuay lạibáovăn
    /// </summary>
    public class StockQueryResponseDTO
    {
        /// <summary>
        /// ThuốcTồn khothông tin
        /// </summary>
        public List<DrugStockInfo> drugStockInfos { get; set; }

    }

    /// <summary>
    /// Tồn khoThuốcthông tin
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
        /// lĩnhthuốcNhà thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// làmnhỏĐơn vịTồn khoSố lượng
        /// </summary>
        public int kscl { get; set; }

        /// <summary>
        /// làmnhỏĐơn vịđôngkếtSố lượng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// làmnhỏĐơn vịcó thểdùngSố lượng
        /// </summary>
        public int kysl { get; set; }
    }
}
