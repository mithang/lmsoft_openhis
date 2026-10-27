using System;

namespace Newtouch.HIS.Domain.DO
{
    /// <summary>
    /// Đặt trước tài nguyên
    /// </summary>
    public class BookItemDo
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int Sl { get; set; }

        /// <summary>
        /// Bộ phận nhà thuốc
        /// </summary>
        public string Yfbm { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string Cfh { get; set; }
        /// <summary>
        /// Chi tiết đơn thuốcId
        /// </summary>
        public string CfmxId { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string Yzzxid { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Cơ cấu tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Người thao tác
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Ngày lấy hồ sơ/(hoặcCreateTime)
        /// </summary>
        public DateTime AdtTdrq { get; set; }

        /// <summary>
        /// Trả về
        /// </summary>
        public string Res { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public int? zh { get; set; }
    }

    /// <summary>
    /// Hủy xếp thuốc
    /// </summary>
    public class CancelArrangement
    {
        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Người dùng đăng nhập
        /// </summary>
        public string userCode { get; set; }
    }
}
