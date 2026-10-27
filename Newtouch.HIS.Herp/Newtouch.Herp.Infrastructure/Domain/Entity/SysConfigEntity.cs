using System;
using SqlSugar;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Hệ thống cấu hình (bảng Sys_Config trong CSDL Herp NewtouchHIS_herp).
    /// Đây là bản port mỏng của entity SysConfig thuộc FrameworkBase.MultiOrg
    /// (chỉ có dạng binary, không có source): entity này là nguồn dữ liệu mà
    /// API Herp Default/Query trả về và HomeController.SyncSysConfigParams đồng bộ.
    /// CSDL Base (NewtouchHIS_Base) KHÔNG có bảng Sys_Config nên repo này đọc
    /// đúng CSDL Herp (kết nối HerpDb mặc định), không phải kết nối Base.
    /// </summary>
    [SugarTable("Sys_Config")]
    public class SysConfigEntity
    {
        /// <summary>Khóa chính (chuỗi GUID)</summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>Id tổ chức sở hữu dòng cấu hình ("*" = dữ liệu gốc dùng chung)</summary>
        public string OrganizeId { get; set; }

        /// <summary>Id tổ chức cấp trên</summary>
        public string TopOrganizeId { get; set; }

        /// <summary>Mã cấu hình (ví dụ: sfdlCode)</summary>
        public string Code { get; set; }

        /// <summary>Tên cấu hình</summary>
        public string Name { get; set; }

        /// <summary>Giá trị cấu hình</summary>
        public string Value { get; set; }

        /// <summary>Ghi chú</summary>
        public string Memo { get; set; }

        /// <summary>Thời gian tạo</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>Mã người tạo</summary>
        public string CreatorCode { get; set; }

        /// <summary>Thời gian sửa lần cuối</summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>Mã người sửa lần cuối</summary>
        public string LastModifierCode { get; set; }

        /// <summary>Trạng thái (1 = hiệu lực)</summary>
        public string zt { get; set; }

        /// <summary>Thứ tự hiển thị</summary>
        public int? px { get; set; }
    }
}
