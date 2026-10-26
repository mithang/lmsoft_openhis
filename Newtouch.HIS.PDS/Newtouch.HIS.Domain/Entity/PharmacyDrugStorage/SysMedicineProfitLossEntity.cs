using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc thông tin lãi/lỗ
    /// </summary>
    [Table("xt_yp_syxx")]
    public class SysMedicineProfitLossEntity : IEntity<SysMedicineProfitLossEntity>
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        [Key]
        public string syId { get; set; }

        /// <summary>
        /// tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// nhà thuốc mã số hiệu
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// thời gian báo cáo giá trị mặc định Getdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// đang số: báo thừa, số âm: báo hỏng  số lượng đơn vị nhỏ nhất
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// giá bán buôn
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Pfj { get; set; }

        /// <summary>
        /// giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// nguyên nhân lãi/lỗ
        /// </summary>
        public string Syyy { get; set; }

        /// <summary>
        /// người chịu trách nhiệm
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// số chứng từ
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// tồn kho còn lại
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// giá nhập
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Jj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// lô
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
