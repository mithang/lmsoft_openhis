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
    /// Hệ thốngThuốclãi lỗthông tin
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
        /// Nhà thuốcMãSố hiệu
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
        /// báo cáoThời gian Mặc địnhGiá trịGetdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// đangsố:báo thừa,âmsố:Báo hỏng  Đơn vị nhỏ nhấtSố lượng
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
        /// Kho thuốcGiá bán buôn
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// Kho thuốcGiá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
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
        /// phiếusố phiếu
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// còn lạiTồn kho
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// vàogiá
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
