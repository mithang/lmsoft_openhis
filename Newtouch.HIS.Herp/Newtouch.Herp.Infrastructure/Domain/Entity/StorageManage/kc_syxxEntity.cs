using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Chênh lệch tồn kho
    /// </summary>
    [SugarTable("kc_syxx")]
    public class KcSyxxEntity : IEntity<KcSyxxEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức(Bệnh viện)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Lô hàng
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// Giá trị mặc định Getdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// Số dương:Báo thừa,Số âm:Báo hỏng Số đơn vị nhỏ nhất
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// Đơn vị ID  sử dụng phối hợp với giá,và zhyz thành bộ
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// Giá bán lẻ  Giá nhập tương ứng với hệ số chuyển đổi
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// Nguyên nhân chênh lệch  kc_syyy Khóa chính của bảng
        /// </summary>
        public string Syyy { get; set; }

        /// <summary>
        /// Người chịu trách nhiệm
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// Tồn kho còn lại Tồn kho hiện có Đơn vị nhỏ nhất kcsl
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Jj { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
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
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
