using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Thông tin thực hiện truyền dịch ngoại trú
    /// </summary>
    [Table("mz_syzxxx")]
    public class MzsyzxxxEntity : IEntity<MzsyzxxxEntity>
    {

        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// mz_syypxxKhóa chính của bảng
        /// </summary>
        public long syypxxId { get; set; }

        /// <summary>
        /// Số ghế
        /// </summary>
        public string seatNum { get; set; }

        /// <summary>
        /// Thời gian bắt đầu truyền dịch
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// Thời gian kết thúc truyền dịch
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// Mã nhân viên dược sĩ pha thuốc
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// Mã nhân viên người thực hiện
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Trạng thái 0:Vô hiệu  1:Có hiệu lực
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
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
