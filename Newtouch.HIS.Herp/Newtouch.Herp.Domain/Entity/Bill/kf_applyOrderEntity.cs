using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Chứng từ nhập/xuất kho
    /// </summary>
    [Table("kf_applyOrder")]
    public class KfApplyOrderEntity : IEntity<KfApplyOrderEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Loại chứng từ 1-Lĩnh vật tư khoa;2-Lĩnh vật tư kho
        /// </summary>
        public int applyType { get; set; }

        /// <summary>
        /// Số phiếu lĩnh
        /// </summary>
        public string sldh { get; set; }

        /// <summary>
        /// Khoa nhập kho
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// Khoa xuất kho
        /// </summary>
        public string ckbm { get; set; }

        /// <summary>
        /// 0:Chờ xử lý;1:Duyệt đạt;2:Duyệt không đạt;3:Giao hàng;4:Hoàn thành một phần;5:Hoàn thành;6:Từ chối nhận
        /// </summary>
        public int applyProcess { get; set; }

        /// <summary>
        /// Trạng thái 0:Hủy bỏ;1.Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
