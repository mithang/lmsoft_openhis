using System;

namespace Newtouch.HIS.Domain.DO
{
    /// <summary>
    /// đặt trước tài nguyên
    /// </summary>
    public class BookItemDo
    {
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// số lượng đơn vị nhỏ nhất
        /// </summary>
        public int Sl { get; set; }

        /// <summary>
        /// bộ phận nhà thuốc
        /// </summary>
        public string Yfbm { get; set; }

        /// <summary>
        /// số nhóm
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string Cfh { get; set; }
        /// <summary>
        /// chi tiết đơn thuốc Id
        /// </summary>
        public string CfmxId { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string Yzzxid { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// cơ cấu tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// người thao tác
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// ngày rút hồ sơ/(hoặc CreateTime)
        /// </summary>
        public DateTime AdtTdrq { get; set; }

        /// <summary>
        /// quay lại
        /// </summary>
        public string Res { get; set; }

        /// <summary>
        /// số nhóm
        /// </summary>
        public int? zh { get; set; }
    }

    /// <summary>
    /// hủy xếp thuốc
    /// </summary>
    public class CancelArrangement
    {
        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// người dùng đăng nhập
        /// </summary>
        public string userCode { get; set; }
    }
}
