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
    /// trícó thểPhê duyệtngàychí
    /// </summary>
    [SugarTable("qhd_znshsqtx")]
    public class QhdZnshSqtxEntity
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// giao dịchMã
        /// </summary>
        public string jydm { get; set; }
        /// <summary>
        /// Ngoại trúSố nhập viện
        /// </summary>
        public string mzzyh { get; set; }
        /// <summary>
        /// 1：Ngoại trú 2：Nhập viện
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Tham số đầu vàoxml
        /// </summary>
        public string XmlRequest { get; set; }
        /// <summary>
        /// ratham sốxml
        /// </summary>
        public string XmlResponse { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
