using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Chứng từ nhập/xuất kho
    /// </summary>
    [SugarTable("kf_crkdj")]
    public class KfCrkdjEntity : IEntity<KfCrkdjEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức Id（Bệnh viện có nghiệp vụ cụ thể）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 1.Nhập kho ngoài；2.Xuất kho ngoài；3.Xuất kho trực tiếp；4.Xuất kho theo phiếu lĩnh；5.Trả hàng nội bộ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// Số phiếu
        /// </summary>
        public string Pdh { get; set; }

        /// <summary>
        /// Khoa xuất kho
        /// </summary>
        public string ckbm { get; set; }

        /// <summary>
        /// Khoa nhập kho
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// Thời gian xuất kho
        /// </summary>
        public DateTime? cksj { get; set; }

        /// <summary>
        /// Thời gian nhập kho
        /// </summary>
        public DateTime? rksj { get; set; }

        /// <summary>
        /// Nhân viên nhập kho
        /// </summary>
        public string rkczy { get; set; }

        /// <summary>
        /// Nhân viên xuất kho
        /// </summary>
        public string ckczy { get; set; }

        /// <summary>
        /// Phương thức nhập/xuất kho
        /// </summary>
        public string crkfs { get; set; }

        /// <summary>
        /// Nhân viên duyệt
        /// </summary>
        public string shczy { get; set; }

        /// <summary>
        /// 0:Chờ duyệt；1.Thông qua；2.Không thông qua
        /// </summary>
        public string auditState { get; set; }

        /// <summary>
        /// Số phiếu phân phối
        /// </summary>
        public string deliveryNo { get; set; }

        /// <summary>
        /// Trạng thái
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
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
