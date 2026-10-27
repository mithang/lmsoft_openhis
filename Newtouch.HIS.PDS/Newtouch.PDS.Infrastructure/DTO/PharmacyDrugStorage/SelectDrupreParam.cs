using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
using System;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Điều kiện truy vấn chứng từ
    /// </summary>
    public class SelectDrupreParam
    {
        /// <summary>
        /// Lật trang
        /// </summary>
        public Pagination pagination { get; set; }

        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime? qsrj { get; set; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? jsrj { get; set; }

       
        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

       
        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc hiện tại
        /// </summary>
        public string curYfbmCode { get; set; }

    }
}