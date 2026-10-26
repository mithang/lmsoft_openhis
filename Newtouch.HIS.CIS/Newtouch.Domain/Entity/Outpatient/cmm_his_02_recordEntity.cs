using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Phòng khám đông y_Đẩy thuốc thảo dược_Hồ sơ
    /// </summary>
    [Table("cmm_his_02_record")]
    public class CmmHis02RecordEntity : IEntity<CmmHis02RecordEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Số thuốc
        /// Bắt buộc nhập
        /// </summary>
        public string cmmId { get; set; }

        /// <summary>
        /// Tên thuốc
        /// Bắt buộc nhập
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Mã thuốc
        /// Bắt buộc nhập
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string specification { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string manufacturer { get; set; }

        /// <summary>
        /// Có bật hay không
        /// </summary>
        public string activeFlg { get; set; }

        /// <summary>
        /// Mã cơ sở
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã trạng thái kết quả trả về  1: Thành công; 0:Thất bại
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// Mô tả kết quả trả về  Thành công;Thất bại(Mô tả bổ sung lý do thất bại)
        /// </summary>
        public string resultDesc { get; set; }

        /// <summary>
        /// Trạng thái     1:Có hiệu lực  0:Vô hiệu
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