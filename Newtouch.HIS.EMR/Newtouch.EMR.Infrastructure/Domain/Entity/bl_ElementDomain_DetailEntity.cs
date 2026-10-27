using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Bảng chi tiết có cấu trúc
    /// </summary>
    [SugarTable("bl_ElementDomain_Detail")]
    public class bl_ElementDomain_DetailEntity 
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string OrganizeId { get; set; }

        public int ElementId { get; set; }
        public string Table_Name { get; set; }
        public int Table_Column_No { get; set; }

        public string Table_Column_Code { get; set; }
        public string Table_Colunn_Name { get; set; }
       
        public string Table_Column_Type { get; set; }

        public string Element_ID { get; set; }
        public string Element_Name { get; set; }

        public int Element_Type { get; set; }
        public string Element_Type_Name { get; set; }
        public int Sybz { get; set; }
        public string ysmxId { get; set; }
        public string AreValue { get; set; }
        public int Px { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int Zt { get; set; }

    }
}
