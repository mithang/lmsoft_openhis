using Newtouch.HIS.Domain.Entity.V;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutOrInStoredOperate
{
    /// <summary>
    /// Tham Số quá trình xử lý đóng băng tồn kho và tạo thông tin chứng từ
    /// </summary>
    public class FrozenStockAndGenerateCrkdjDTO
    {

        /// <summary>
        /// Bộ phận nhà thuốc hiện tại Bộ phận xuất kho
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Nhân viên thao tác
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Bộ phận nhập kho
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// Thông tin thuốc cần đóng băng
        /// </summary>
        public List<FrozenedMedicineInfoDTO> frozenseMedicines { get; set; }
    }

    /// <summary>
    /// Thông tin thuốc cần đóng băng
    /// </summary>
    public class FrozenedMedicineInfoDTO
    {

        /// <summary>
        /// Tổng Số lượng cần đóng băng  Đơn vị nhỏ nhất
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Chi tiết đơn lĩnhID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// Hệ số quy đổi Hiện là hệ số quy đổi đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Số lượng đóng gói
        /// </summary>
        public int bzs { get; set; }

        /// <summary>
        /// Tồn kho khả dụng Đơn vị nhỏ nhất
        /// </summary>
        public int kykc { get; set; }

        /// <summary>
        /// Giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal zxdwjj { get; set; }

        /// <summary>
        /// Giá bán buôn đơn vị nhỏ nhất
        /// </summary>
        public decimal zxdwpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal zxdwlsj { get; set; }

        /// <summary>
        /// Thông tin lô thuốc đóng băng
        /// </summary>
        public List<FrozenBatchesDetailVEntity> frozenedMedicineBatchs { get; set; }
    }

}
