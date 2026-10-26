using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Hạng mục ngoại trú
    /// </summary>
    [Table("mz_jzjhmxzx")]
    public class OutpatientItemExeEntity : IEntity<OutpatientItemExeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Liên kếtmz_jzjhmxKhóa chính bảng
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime zxsj { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng(Từ điểnRTM)
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// Liều điều trị
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Mã nhân viên kỹ thuật viên điều trị
        /// </summary>
        public string zlsgh { get; set; }

    }
}
