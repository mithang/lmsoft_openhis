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
    /// <summary>
    /// Mua hàng
    /// </summary>
    [Table("xt_yp_cg")]
    [SugarTable("xt_yp_cg")]
    public class PurchaseEntity : IEntity<PurchaseEntity>
    {
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        
        
        public string cgId { get; set; }
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Thời gian đơn hàng
        /// </summary>
        public string ddsj { get; set; }
        /// <summary>
        /// Loại thao tác
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// Mã BHYT
        /// </summary>
        public string yybm { get; set; }
        /// <summary>
        /// Mã điểm giao hàng
        /// </summary>
        public string psdbm { get; set; }
        /// <summary>
        /// Loại đơn hàng
        /// </summary>
        public string ddlx { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string ddbh { get; set; }
        /// <summary>
        /// Số đơn kế hoạch bệnh viện
        /// </summary>
        public string yyjhdh { get; set; }
        /// <summary>
        /// Ngày nhận hàng muộn nhất
        /// </summary>
        public string zwdhrq { get; set; }
        /// <summary>
        /// Số bản ghi
        /// </summary>
        public int jls { get; set; }
        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public int ddzt { get; set; }
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
        public string gysCode { get; set;}
        public string gysName { get; set;}
    }
}
