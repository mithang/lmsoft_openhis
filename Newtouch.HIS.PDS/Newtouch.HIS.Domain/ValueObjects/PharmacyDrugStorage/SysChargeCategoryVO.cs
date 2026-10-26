using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysChargeCategoryVO : IEntity<SysChargeCategoryVO>
    {
        /// <summary>
        /// 
        /// </summary>
        public int dlId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 0 chung 1 ngoại trú 2 nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// ngoại trú in báo cáo thuộc về chi phí (比饮食费归'khác')
        /// </summary>
        public string mzprintreportcode { get; set; }

        /// <summary>
        /// ngoại trú quyết toán thuộc về chi phí (比饮食费归'khác')
        /// </summary>
        public string mzprintbillcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// chung báo cáo nhóm lớn
        /// </summary>
        public string reportdlcode { get; set; }

        /// <summary>
        /// nhóm lớn loại, enum tương ứng EnumSfdlDllb 1 thuốc 2 điều trị hạng mục 3 hạng mục không điều trị
        /// </summary>
        public int? dllb { get; set; }

        public string fjCode { get; set; }
        public string fjmc { get; set; }

        public decimal? sn { get; set; }
    }
}
