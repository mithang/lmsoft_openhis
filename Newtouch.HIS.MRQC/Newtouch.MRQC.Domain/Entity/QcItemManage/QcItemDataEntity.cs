using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.Entity.QcItemManage
{
    /// <summary>
    /// 
    /// </summary>
    [Table("MR_Qc_ItemData")]
    public class QcItemDataEntity : IEntity<QcItemDataEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///erm Loại bệnh án、mẫuId
        /// </summary>
        public string BlmbId { get; set; }

        /// <summary>
        /// Loại bệnh án、tên mẫu
        /// </summary>
        public string Bllmbmc { get; set; }

        /// <summary>
        /// loại kiểm soát chất lượng 1:Loại bệnh án  2：Mẫu bệnh án
        /// </summary>
        public string zklx { get; set; }

        /// <summary>
        /// mã kiểm soát chất lượng
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// tên hạng mục kiểm soát chất lượng
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// điểm kiểm soát chất lượng
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// Giải thích chấm điểm
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? Px { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
