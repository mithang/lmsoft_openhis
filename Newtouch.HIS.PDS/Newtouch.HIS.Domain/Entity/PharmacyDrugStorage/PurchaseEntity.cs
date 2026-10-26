using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.PharmacyDrugStorage
{
    /// <summary>
    /// mua hàng
    /// </summary>
    [Table("xt_yp_cg")]
    public class PurchaseEntity : IEntity<PurchaseEntity>
    {
        [Key]
        
        
        public string cgId { get; set; }
        /// <summary>
        /// tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// thời gian đơn hàng
        /// </summary>
        public string ddsj { get; set; }
        /// <summary>
        /// loại thao tác
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// BHYT mã
        /// </summary>
        public string yybm { get; set; }
        /// <summary>
        /// mã điểm phân phối
        /// </summary>
        public string psdbm { get; set; }
        /// <summary>
        /// loại đơn hàng
        /// </summary>
        public string ddlx { get; set; }
        /// <summary>
        /// số đơn hàng
        /// </summary>
        public string ddbh { get; set; }
        /// <summary>
        /// số phiếu kế hoạch bệnh viện
        /// </summary>
        public string yyjhdh { get; set; }
        /// <summary>
        /// ngày giao hàng muộn nhất
        /// </summary>
        public string zwdhrq { get; set; }
        /// <summary>
        /// số bản ghi
        /// </summary>
        public int jls { get; set; }
        /// <summary>
        /// trạng thái đơn hàng
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
