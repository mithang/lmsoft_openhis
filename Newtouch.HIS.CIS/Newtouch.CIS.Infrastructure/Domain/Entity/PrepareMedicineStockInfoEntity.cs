using Newtouch.CIS.Compat;
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
    [SugarTable("xt_ksby_kcxx")]
    public class PrepareMedicineStockInfoEntity
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string kcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string ypkw { get; set; }

        /// <summary>
        /// 0：khôngđiều khiển1：điều khiển
        /// </summary>
        //public short kzbz { get; set; }

        /// <summary>
        /// đãphốithuốcchưaphát（lĩnh）thuốccủaSố lượng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 0：Kích hoạt1：Vô hiệu
        /// </summary>
        public short tybz { get; set; }

        /// <summary>
        /// đếntự bảng XT_YP_CRKMXK 
        /// </summary>
        //public string crkmxId { get; set; }

        /// <summary>
        /// vàogiá  Đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// tương ứngNhà thuốcNhà thuốccủatháokhôngsố
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Nơi sản xuấtDanh mục
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string pc { get; set; }

        /// <summary>
        /// làKhôngđãkhóa  0：chưakhóa ；    1/null：đãkhóa
        /// </summary>
        public string locked { get; set; }

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


        public string Updating { get; set; }
        public string ksbm { get; set; }
        public string bqbm { get; set; }
    }
}
