using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Mua sắm_Đơn hàng
    /// </summary>
    [SugarTable("cg_order")]
    public class CgOrderEntity : IEntity<CgOrderEntity>
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
        /// Số đơn mua sắm
        /// </summary>
        public string orderNo { get; set; }

        /// <summary>
        /// Loại đơn hàng 0:Phiếu lưu tạm;1:Phiếu chính thức
        /// </summary>
        public int orderType { get; set; }

        /// <summary>
        /// Quy trình xử lý đơn mua sắm -1:Từ chối xử lý; 0:Chờ xử lý; 1:Chuẩn bị hàng; 2:Phân phối; 3:Ký nhận; 4:Hoàn thành; 5:Từ chối ký nhận 
        /// </summary>
        public int orderProcess { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Trạng thái 0:Hủy bỏ;1.Hiệu lực
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