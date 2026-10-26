using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc chứng từ nhập/xuất kho
    /// </summary>
    [Table("xt_yp_crkdj")]
    public class SysMedicineStorageIOReceiptEntity : IEntity<SysMedicineStorageIOReceiptEntity>
    {
        /// <summary>
        /// ra nhập kho ID
        /// </summary>
        [Key]
        public string crkId { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// loại chứng từ 
        /// 1: thuốc nhập kho  số chứng từ YPRKD111111111111
        /// 2: xuất kho ra bên ngoài  số chứng từ WBCKD111111111111
        /// 3: xuất kho trực tiếp  số chứng từ NBFYD111111111111
        /// 4: xuất kho theo phiếu lĩnh  số chứng từ SLCKD111111111111
        /// 5: trả lại thuốc phát nội bộ    số chứng từ NBFYTHD111111111111
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// số chứng từ
        /// </summary>
        public string Pdh { get; set; }

        /// <summary>
        /// bộ phận nhập kho
        /// </summary>
        public string Rkbm { get; set; }

        /// <summary>
        /// bộ phận xuất kho
        /// </summary>
        public string Ckbm { get; set; }

        /// <summary>
        /// thời gian nhập kho
        /// </summary>
        public DateTime? Rksj { get; set; }

        /// <summary>
        /// thời gian xuất kho
        /// </summary>
        public DateTime? Cksj { get; set; }

        /// <summary>
        /// nhập kho người thao tác
        /// </summary>
        public string Rkczy { get; set; }

        /// <summary>
        /// xuất kho người thao tác
        /// </summary>
        public string Ckczy { get; set; }

        /// <summary>
        /// từ NewtouchHIS_Base.dbo.xt_ypcrkfs bảng của phương thức mã 
        /// </summary>
        public string Crkfsdm { get; set; }

        /// <summary>
        /// thời gian thao tác
        /// </summary>
        public DateTime? Czsj { get; set; }

        /// <summary>
        /// thời gian đề nghị
        /// </summary>
        public DateTime? Sqsj { get; set; }

        /// <summary>
        /// duyệt người thao tác
        /// </summary>
        public string Shczy { get; set; }

        /// <summary>
        /// 0:chưa duyệt 1:đã đạt 2: chưa đạt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// trạng thái 1-còn hiệu lực  0-không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// tạo người
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
