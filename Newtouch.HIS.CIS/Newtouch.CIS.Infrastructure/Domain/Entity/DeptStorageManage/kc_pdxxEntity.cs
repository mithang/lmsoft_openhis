using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Tồn khoKiểm kêbảng
    /// </summary>
    [SugarTable("dept_pdxx")]
    public class KcPdxxEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chứcId(cócụ thểthểnghiệp vụcủayviện)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// KhoID
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
        /// Kiểm kêphươngkiểu
        /// </summary>
        public short pdfs { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thứ tự
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
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa cuối người
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
