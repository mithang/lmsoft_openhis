
using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.Entity.Purchase
{
    /// <summary>
    /// Mua sắm
    /// </summary>
    [SugarTable("xt_wz_cg")]
    public class PurchaseEntity : IEntity<PurchaseEntity>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string cgId { get; set; }
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Thời gian đặt hàng
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
        /// Mã điểm phân phối
        /// </summary>
        public string psdbm { get; set; }
        /// <summary>
        /// Loại đơn hàng
        /// </summary>
        public string ddlx { get; set; }
        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public string ddbh { get; set; }
        /// <summary>
        /// Số phiếu kế hoạch của bệnh viện
        /// </summary>
        public string yyjhdh { get; set; }
        /// <summary>
        /// Ngày giao hàng muộn nhất
        /// </summary>
        public string zwdhrq { get; set; }
        /// <summary>
        /// Phương thức mua sắm
        /// </summary>
        public string cgfs { get; set; }
        /// <summary>
        /// Mã hệ thống
        /// </summary>
        public string xtbm { get; set; }
        /// <summary>
        /// Có kèm dịch vụ đi kèm không
        /// </summary>
        public string sfhbsfw { get; set; }
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
    }
}
