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
        /// 0:không điều khiển1:điều khiển
        /// </summary>
        //public short kzbz { get; set; }

        /// <summary>
        /// đã phối thuốc chưa phát(lĩnh)thuốc của Số lượng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 0:Kích hoạt1:Vô hiệu
        /// </summary>
        public short tybz { get; set; }

        /// <summary>
        /// đến tự bảng XT_YP_CRKMXK 
        /// </summary>
        //public string crkmxId { get; set; }

        /// <summary>
        /// vào giá  Đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// tương ứng Nhà thuốc Nhà thuốc của tháo không số
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Nơi sản xuất Danh mục
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string pc { get; set; }

        /// <summary>
        /// là Không đã khóa  0:chưa khóa ;    1/null:đã khóa
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
