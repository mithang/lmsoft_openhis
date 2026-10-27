using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Bảng chính có cấu trúc
    /// </summary>
    [SugarTable("bl_ElementDomain")]
    public class bl_ElementDomainEntity 
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Mã định danh loại bệnh án
        /// </summary>
        public string Bllx { get; set; }
        public string Table_EngLish_Name { get; set; }
        public string Table_Name { get; set; }
        public string Regex { get; set; }
        public string Px { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int Zt { get; set; }
    }
}
