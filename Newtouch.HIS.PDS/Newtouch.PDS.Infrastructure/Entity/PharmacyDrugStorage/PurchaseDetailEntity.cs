using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.PharmacyDrugStorage
{
    [Table("xt_yp_cgmx")]
    [SugarTable("xt_yp_cgmx")]
    public class PurchaseDetailEntity : IEntity <PurchaseDetailEntity>
    {
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string cgmxId { get; set; }
        /// <summary>
        /// Số mua hàng
        /// </summary>
        public string cgId { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }
        public string ypName { get; set; }
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int sxh { get; set; }
        /// <summary>
        /// Phương thức mua hàng
        /// </summary>
        public string cglx { get; set; }
        /// <summary>
        /// Loại hàng hóa
        /// </summary>
        public string splx { get; set; }
        /// <summary>
        /// Mã thống nhất
        /// </summary>
        public string zxspbm { get; set; }
        /// <summary>
        /// Đơn vị đo lường mua hàng
        /// </summary>
        public string cgjldw { get; set; }
        /// <summary>
        /// Quy cách đóng gói
        /// </summary>
        public string ggbz { get; set; }
        /// <summary>
        /// Số lượng mua hàng
        /// </summary>
        public decimal cgsl { get; set; }
        /// <summary>
        /// Đơn giá mua hàng
        /// </summary>
        public decimal cgdj { get; set; }
        /// <summary>
        /// Mã doanh nghiệp dược/Nhà cung cấp
        /// </summary>
        public string yqbm { get; set; }
        /// <summary>
        /// Cờ giao hàng nhiều lần（0Không cho phép  1Cho phép）
        /// </summary>
        public string dcpsbs { get; set; }
        /// <summary>
        /// Giải thích ghi chú
        /// </summary>
        public string bzsm { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

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
        public string zt { get; set; }
        public string py { get; set; }
        public decimal? zje { get; set; }
    }
    
}
