using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.PharmacyDrugStorage
{
    [Table("xt_yp_cgmx")]
    public class PurchaseDetailEntity : IEntity <PurchaseDetailEntity>
    {
        [Key]
        public string cgmxId { get; set; }
        /// <summary>
        /// mua hàng số hiệu
        /// </summary>
        public string cgId { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }
        public string ypName { get; set; }
        /// <summary>
        /// số thứ tự
        /// </summary>
        public int sxh { get; set; }
        /// <summary>
        /// mua hàng mô hình
        /// </summary>
        public string cglx { get; set; }
        /// <summary>
        /// loại hàng hóa
        /// </summary>
        public string splx { get; set; }
        /// <summary>
        /// thống nhất mã
        /// </summary>
        public string zxspbm { get; set; }
        /// <summary>
        /// đơn vị tính mua hàng
        /// </summary>
        public string cgjldw { get; set; }
        /// <summary>
        /// quy cách đóng gói
        /// </summary>
        public string ggbz { get; set; }
        /// <summary>
        /// số lượng mua
        /// </summary>
        public decimal cgsl { get; set; }
        /// <summary>
        /// đơn giá mua hàng
        /// </summary>
        public decimal cgdj { get; set; }
        /// <summary>
        /// mã doanh nghiệp dược/nhà cung cấp
        /// </summary>
        public string yqbm { get; set; }
        /// <summary>
        /// nhiều lần phân phối dấu hiệu (0 không cho phép  1 cho phép)
        /// </summary>
        public string dcpsbs { get; set; }
        /// <summary>
        /// ghi chú giải thích
        /// </summary>
        public string bzsm { get; set; }
        /// <summary>
        /// đơn vị
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
