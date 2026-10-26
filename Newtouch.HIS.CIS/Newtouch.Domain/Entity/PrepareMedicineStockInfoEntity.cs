using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    [Table("xt_ksby_kcxx")]
    public class PrepareMedicineStockInfoEntity : IEntity<PrepareMedicineStockInfoEntity>
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
        /// 0:Không kiểm soát1:Kiểm soát
        /// </summary>
        //public short kzbz { get; set; }

        /// <summary>
        /// Đã pha thuốc chưa phát(lĩnh)Số lượng thuốc
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 0:Bật1:Tắt
        /// </summary>
        public short tybz { get; set; }

        /// <summary>
        /// Đến từ bảng XT_YP_CRKMXK 
        /// </summary>
        //public string crkmxId { get; set; }

        /// <summary>
        /// Giá nhập  Đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// Số lượng bán lẻ tương ứng của nhà thuốc
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Danh mục xuất xứ
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string pc { get; set; }

        /// <summary>
        /// Đã khóa hay chưa  0:Chưa khóa ;    1/null:Đã khóa
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
