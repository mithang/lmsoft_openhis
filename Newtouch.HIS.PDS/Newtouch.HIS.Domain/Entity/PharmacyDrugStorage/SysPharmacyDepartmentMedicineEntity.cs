using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc này bộ phận thông tin thuốc
    /// </summary>
    [Table("xt_yp_bmypxx")]
    public class SysPharmacyDepartmentMedicineEntity : IEntity<SysPharmacyDepartmentMedicineEntity>
    {
        /// <summary>
        /// bộ phận thuốc Id
        /// </summary>
        [Key]
        public string bmypId { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// thuốc vị trí kho
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// số thứ tự sổ sách
        /// </summary>
        public string Zcxh { get; set; }

        /// <summary>
        /// sắp xếp phương thức 1
        /// </summary>
        public string Pxfs1 { get; set; }

        /// <summary>
        /// sắp xếp phương thức 2
        /// </summary>
        public string Pxfs2 { get; set; }

        /// <summary>
        /// 库存上限
        /// </summary>
        public int? Kcsx { get; set; }

        /// <summary>
        /// 库存下限
        /// </summary>
        public int? Kcxx { get; set; }

        /// <summary>
        /// điểm nhập hàng 低于进货点则自动生成内部申领单
        /// </summary>
        public int? Jhd { get; set; }

        /// <summary>
        /// lượng kế hoạch tương ứng điểm nhập hàng, 指定内部申领的数量
        /// </summary>
        public int? Jhl { get; set; }

        /// <summary>
        /// thuộc tính thuốc từ bảng xt_yptssx
        /// </summary>
        public string Ypsxdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Ylsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sysx { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// trạng thái  1: bình thường 0: kiểm soát
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建试驾
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// sửa người
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
