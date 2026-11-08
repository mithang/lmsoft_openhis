using Newtouch.CIS.Compat;
using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Hệ thống Thuốc lãi lỗ thông tin
    /// </summary>
    [SugarTable("zy_ksby_syxx")]
    public class SysMedicineProfitLossEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string syId { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Nhà thuốc Mã Số hiệu
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
        /// Hạn hiệu lực
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// báo cáo Thời gian Mặc định Giá trị Getdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// đang số:báo thừa,âm số:Báo hỏng  Đơn vị nhỏ nhất Số lượng
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
        /// Kho thuốc Giá bán buôn
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// Kho thuốc Giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// chuyển hóa vì con
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// nguyên nhân lãi lỗ
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
        /// còn lại Tồn kho
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// vào giá
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
