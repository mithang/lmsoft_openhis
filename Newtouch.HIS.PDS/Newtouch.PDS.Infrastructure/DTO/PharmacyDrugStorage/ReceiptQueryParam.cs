using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
using System;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Điều kiện truy vấn chứng từ
    /// </summary>
    public class ReceiptQueryParam
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
        /// Số chứng từ
        /// </summary>
        public string pdh { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public int? djlx { get; set; }

        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Loại chứng từ sở hữu quyền
        /// </summary>
        public string[] alldjlx { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc hiện tại
        /// </summary>
        public string curYfbmCode { get; set; }

        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        public string gys { get; set; }
    }
}