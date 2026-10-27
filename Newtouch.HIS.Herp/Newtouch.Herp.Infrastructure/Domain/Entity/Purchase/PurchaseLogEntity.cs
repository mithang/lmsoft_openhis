
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
    /// Bảng nhật ký trả về của API mua sắm vật tư
    /// </summary>
    [SugarTable("xt_wz_cg_log")]
    public class PurchaseLogEntity : IEntity<PurchaseLogEntity>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Mã giao dịch
        /// </summary>
        public string jydm { get; set; }
        /// <summary>
        /// Tên giao dịch
        /// </summary>
        public string jymc { get; set; }
        public string param { get; set; }
        /// <summary>
        /// Tham số đầu vào xml
        /// </summary>
        public string XmlRequest { get; set; }
        /// <summary>
        /// Tham số đầu ra xml
        /// </summary>
        public string XmlResponse { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
