using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// tra cứu chứng từ chính bản ghi
    /// </summary>
    public class DrupreparationVO
    {
        public string Id { get; set; }
        /// <summary>
        /// trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// đề nghị khu bệnh
        /// </summary>
        public string bqmc { get; set; }

        /// <summary>
        /// bộ phận xuất kho tên
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// bộ phận nhập kho tên
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// tên nhà cung cấp
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// chứng từ nhập/xuất kho chính bảng ID
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// số chứng từ
        /// </summary>
        public string CreateTime { get; set; }
    }
}
