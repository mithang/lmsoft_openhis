using Newtouch.Core.Common;
using System;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// điều kiện tra cứu chứng từ
    /// </summary>
    public class SelectDrupreParam
    {
        /// <summary>
        /// chuyển trang
        /// </summary>
        public Pagination pagination { get; set; }

        /// <summary>
        /// ngày bắt đầu
        /// </summary>
        public DateTime? qsrj { get; set; }

        /// <summary>
        /// ngày kết thúc
        /// </summary>
        public DateTime? jsrj { get; set; }

       
        /// <summary>
        /// trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

       
        /// <summary>
        /// cơ cấu tổ chức
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        /// hiện tại mã bộ phận nhà thuốc
        /// </summary>
        public string curYfbmCode { get; set; }

    }
}