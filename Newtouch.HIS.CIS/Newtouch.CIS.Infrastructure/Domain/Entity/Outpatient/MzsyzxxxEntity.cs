using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Ngoại trútruyền dịchThực hiệnthông tin
    /// </summary>
    [SugarTable("mz_syzxxx")]
    public class MzsyzxxxEntity
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
        /// mz_syypxxbảngKhóa chính
        /// </summary>
        public long syypxxId { get; set; }

        /// <summary>
        /// ghế ký tựsố
        /// </summary>
        public string seatNum { get; set; }

        /// <summary>
        /// truyền dịchThời gian bắt đầu
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// truyền dịchThời gian kết thúc
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// phốiDược sĩMã nhân viên
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// Thực hiện ngườiviênMã nhân viên
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Trạng thái 0:Vô hiệu  1:Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạongười
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửangười
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
