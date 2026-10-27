using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Kho
    /// </summary>
    [SugarTable("gys_supplier")]
    public class GysSupplierEntity : IEntity<GysSupplierEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức Id（Bệnh viện có nghiệp vụ cụ thể）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 0：Khác     1：Nhà sản xuất      2：Nhà phân phối
        /// </summary>
        public int? supplierType { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Mã bưu điện
        /// </summary>
        public string zipCode { get; set; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// Fax
        /// </summary>
        public string fax { get; set; }

        /// <summary>
        /// Ngân hàng mở tài khoản
        /// </summary>
        public string khh { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string khhzh { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string sh { get; set; }

        /// <summary>
        /// Chu kỳ thanh toán
        /// </summary>
        public string jszq { get; set; }

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
