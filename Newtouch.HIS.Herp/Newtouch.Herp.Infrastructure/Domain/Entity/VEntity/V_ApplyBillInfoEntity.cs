using System;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Thông tin chính phiếu lĩnh
    /// </summary>
    public class VApplyBillInfoEntity
    {
        /// <summary>
        /// Loại chứng từ 1-Khoa phòng lĩnh；2-Kho lĩnh
        /// </summary>
        public int applyType { get; set; }

        /// <summary>
        /// Quá trình xử lý   0:Chờ xử lý；1:Đã duyệt；2:Không duyệt；3:Phân phối；4:Hoàn thành một phần；5:Hoàn thành；6:Từ chối nhận
        /// </summary>
        public int applyProcess { get; set; }

        /// <summary>
        /// Số phiếu lĩnh
        /// </summary>
        public string sldh { get; set; }

        /// <summary>
        /// Tên khoa xuất kho
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// Khoa xuất kho ID
        /// </summary>
        public string ckbmId { get; set; }

        /// <summary>
        /// Tên khoa nhập kho
        /// </summary>
        public string rkbmmc { get; set; }

        /// <summary>
        /// Khoa nhập kho ID
        /// </summary>
        public string rkbmId { get; set; }

        /// <summary>
        /// Tên khoa nhập kho
        /// </summary>
        public string rkksmc { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorName { get; set; }
    }
}