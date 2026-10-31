using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Thông tin lãi lỗ thuốc hệ thống
    /// </summary>
    [Table("xt_yp_syxx")]
    [SugarTable("xt_yp_syxx")]
    public class SysMedicineProfitLossEntity : IEntity<SysMedicineProfitLossEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string syId { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số hiệu mã nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// Thời gian báo cáo Giá trị mặc địnhGetdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// Số dương:Báo thừa,Số âm:Báo hỏng  Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// Giá bán buôn
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// Lý do lãi lỗ
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
        /// Tồn kho còn lại
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Jj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }

    }
}
