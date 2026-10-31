using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Khung nhìn kho
    /// </summary>
    public class VKfWarehouseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Cấp bậc
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// Nút cha
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Tên quản trị viên
        /// </summary>
        public string admins { get; set; }

        /// <summary>
        /// Tên khoa phòng
        /// </summary>
        public string deptNames { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Có đồng bộ mặc định không
        /// </summary>
        public string isDefSyn { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
