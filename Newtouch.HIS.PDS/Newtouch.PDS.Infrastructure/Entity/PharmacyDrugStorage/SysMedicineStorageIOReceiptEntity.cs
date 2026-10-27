using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chứng từ xuất nhập kho thuốc hệ thống
    /// </summary>
    [Table("xt_yp_crkdj")]
    [SugarTable("xt_yp_crkdj")]
    public class SysMedicineStorageIOReceiptEntity : IEntity<SysMedicineStorageIOReceiptEntity>
    {
        /// <summary>
        /// Xuất nhập khoID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string crkId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Loại chứng từ 
        /// 1：Nhập kho thuốc  Số chứng từYPRKD111111111111
        /// 2：Xuất kho ra ngoài  Số chứng từWBCKD111111111111
        /// 3：Xuất kho trực tiếp  Số chứng từNBFYD111111111111
        /// 4：Xuất kho theo đơn lĩnh  Số chứng từSLCKD111111111111
        /// 5：Trả lại phát thuốc nội bộ    Số chứng từNBFYTHD111111111111
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string Pdh { get; set; }

        /// <summary>
        /// Bộ phận nhập kho
        /// </summary>
        public string Rkbm { get; set; }

        /// <summary>
        /// Bộ phận xuất kho
        /// </summary>
        public string Ckbm { get; set; }

        /// <summary>
        /// Thời gian nhập kho
        /// </summary>
        public DateTime? Rksj { get; set; }

        /// <summary>
        /// Thời gian xuất kho
        /// </summary>
        public DateTime? Cksj { get; set; }

        /// <summary>
        /// Nhân viên thao tác nhập kho
        /// </summary>
        public string Rkczy { get; set; }

        /// <summary>
        /// Nhân viên thao tác xuất kho
        /// </summary>
        public string Ckczy { get; set; }

        /// <summary>
        /// Từ NewtouchHIS_Base.dbo.xt_ypcrkfsMã phương thức bảng 
        /// </summary>
        public string Crkfsdm { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        public DateTime? Czsj { get; set; }

        /// <summary>
        /// Thời gian xin
        /// </summary>
        public DateTime? Sqsj { get; set; }

        /// <summary>
        /// Nhân viên thao tác duyệt
        /// </summary>
        public string Shczy { get; set; }

        /// <summary>
        /// 0:Chưa duyệt 1:Đã đạt 2：Không đạt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Trạng thái 1-Hiệu lực  0-Không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

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
