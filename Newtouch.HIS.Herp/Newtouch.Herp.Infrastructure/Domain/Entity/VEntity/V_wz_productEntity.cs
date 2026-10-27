using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Khung nhìn vật tư
    /// </summary>
    public class VWzProductEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức Id（Bệnh viện có nghiệp vụ cụ thể）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên vật tư(Bắt buộc)
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Loại、Nhóm lớn ID(Bắt buộc)
        /// </summary>
        public string typeId { get; set; }

        /// <summary>
        /// Loại、Nhóm lớn(Bắt buộc)
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Số giấy đăng ký
        /// </summary>
        public string zczh { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// Đường dẫn hình ảnh
        /// </summary>
        public string imageUrl { get; set; }

        /// <summary>
        /// Thương hiệu
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// Model và quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string minUnit { get; set; }

        /// <summary>
        /// Số lượng đặt tối thiểu
        /// </summary>
        public int? zxqds { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Có phải tồn kho bằng 0 không
        /// </summary>
        public string sflkc { get; set; }

        /// <summary>
        /// Có tái sử dụng không
        /// </summary>
        public string sffy { get; set; }

        /// <summary>
        /// Có theo ca mổ không
        /// </summary>
        public string sfgt { get; set; }

        /// <summary>
        /// Trạng thái  0:Hủy bỏ；1.Hiệu lực
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

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
        /// <summary>
        /// Giá trị cảnh báo tồn kho
        /// </summary>
        public int? kcyjz { get; set; }
    }
}
