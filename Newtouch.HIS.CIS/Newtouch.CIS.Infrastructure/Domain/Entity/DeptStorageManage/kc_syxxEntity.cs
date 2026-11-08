using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.CIS.Compat;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Tồn kho lãi lỗ
    /// </summary>
    [SugarTable("dept_syxx")]
    public class KcSyxxEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức(y viện)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Kho Mã
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// Mặc định Giá trị Getdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// đang số:báo thừa,âm số:Báo hỏng Đơn vị nhỏ nhất số
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// Đơn vị ID  vàgiáôphốihợp Sử dụng,và zhyz thành bộ
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// Giá bán lẻ  chuyển hóa vì con tương ứng vào giá
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// chuyển hóa vì con
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// nguyên nhân lãi lỗ  kc_syyybảng Khóa chính
        /// </summary>
        public string Syyy { get; set; }

        /// <summary>
        /// Trách nhiệm người
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// phiếu số phiếu
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// còn lại Tồn kho hiện có Tồn kho Đơn vị nhỏ nhất kcsl
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// vào giá
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
        /// Thứ tự
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
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa cuối người
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
