using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_cfmbmx")]
    public class PresTemplateDetailEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string mxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// liên kếtbảng mẫu
        /// </summary>
        public string mbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Dư thừachữđoạn
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Dư thừaMã
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// kimđốiPhục hồi chức năngĐơn thuốc
        /// </summary>
        public int? mczll { get; set; }

        /// <summary>
        /// kimđốiThuốc
        /// </summary>
        public decimal? mcjl { get; set; }

        /// <summary>
        /// kimđốiThuốc
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// kimđốiThuốc
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// kimđốiThuốc
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// kimđốiPhục hồi chức năngĐơn thuốc
        /// </summary>
        public int? zl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

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
        public string zh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Khoa thực hiện（ThuốccủalĩnhthuốcKhoa phòng yfbmCode）
        /// </summary>
        public string zxks { get; set; }
        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }
        
    }
}
