using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Mua sắm_Kế hoạch mua sắm
    /// </summary>
    [SugarTable("cg_purchaseOrder")]
    public class CgPurchaseOrderEntity : IEntity<CgPurchaseOrderEntity>
    {
        /// <summary>
        /// Khóa chính ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số phiếu kế hoạch mua sắm
        /// </summary>
        public string cgdh { get; set; }

        /// <summary>
        /// Khoa nhập kho Code
        /// </summary>
        public string rkbmCode { get; set; }

        /// <summary>
        /// Trạng thái duyệt 0-Chờ duyệt； 1-Đã duyệt； 2-Không duyệt；3-Đã hủy bỏ；4-Lưu tạm
        /// </summary>
        public int auditState { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Trạng thái 0:Hủy bỏ；1.Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người dùng tạo ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người dùng sửa đổi cuối ID
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}