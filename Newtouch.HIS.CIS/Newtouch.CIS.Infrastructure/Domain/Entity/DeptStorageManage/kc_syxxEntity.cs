using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.CIS.Compat;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Tồn kholãi lỗ
    /// </summary>
    [SugarTable("dept_syxx")]
    public class KcSyxxEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức(yviện)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// KhoMã
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// vật tưID
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
        /// Mặc địnhGiá trịGetdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// đangsố:báo thừa,âmsố:Báo hỏng Đơn vị nhỏ nhấtsố
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// Đơn vịID  vàgiáôphốihợpSử dụng,vàzhyzthànhbộ
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// Giá bán lẻ  chuyểnhóavìcontương ứngvàogiá
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// nguyên nhân lãi lỗ  kc_syyybảngKhóa chính
        /// </summary>
        public string Syyy { get; set; }

        /// <summary>
        /// Trách nhiệm người
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// phiếusố phiếu
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// còn lạiTồn kho hiệncóTồn kho Đơn vị nhỏ nhất kcsl
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// vàogiá
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
