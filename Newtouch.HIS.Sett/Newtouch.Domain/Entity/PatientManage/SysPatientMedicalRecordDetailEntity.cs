using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết bệnh án lịch sử bệnh nhân hệ thống
    /// </summary>
    [Table("xt_brlsblmx")]
    public class SysPatientMedicalRecordDetailEntity : IEntity<SysPatientMedicalRecordDetailEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Loại tệp đính kèm(Word,Excelv.v.) EnumEnumFileType
        /// </summary>
        public int attachType { get; set; }

        /// <summary>
        /// Bảng chính bệnh ánId
        /// </summary>
        public string blId { get; set; }

        /// <summary>
        /// Tên tệp đính kèm
        /// </summary>
        public string attachName { get; set; }

        /// <summary>
        /// Đường dẫn tệp đính kèm
        /// </summary>
        public string attachPath { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Người tạoID
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
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

    }
}
