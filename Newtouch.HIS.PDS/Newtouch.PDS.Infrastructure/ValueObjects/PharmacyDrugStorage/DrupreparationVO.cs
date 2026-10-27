using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Bản ghi chính truy vấn chứng từ
    /// </summary>
    public class DrupreparationVO
    {
        public string Id { get; set; }
        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Khu bệnh xin
        /// </summary>
        public string bqmc { get; set; }

        /// <summary>
        /// Tên bộ phận xuất kho
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// Tên bộ phận nhập kho
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// Bảng chính chứng từ xuất nhập khoID
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string CreateTime { get; set; }
    }
}
