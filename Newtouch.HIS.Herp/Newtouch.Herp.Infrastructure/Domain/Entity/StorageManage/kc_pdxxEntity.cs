using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Bảng kiểm kê tồn kho
    /// </summary>
    [SugarTable("kc_pdxx")]
    public class KcPdxxEntity : IEntity<KcPdxxEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Kho ID
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        ///  Thời gian bắt đầu
        /// </summary>
        public DateTime kssj { get; set; }

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime? jssj { get; set; }

        /// <summary>
        /// Phương thức kiểm kê
        /// </summary>
        public short pdfs { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

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
