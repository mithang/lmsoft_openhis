using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.PatientCenter
{
    ///<summary>
    /// Thẻ khám
    ///</summary>
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_card", "SysPatCardEntity")]
    public class SysPatCardEntity : IEntity
    {
        public string OrganizeId { get; set; }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string CardId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "CardNo giới hạn độ dài là 500")]
        public string CardNo { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CardType không được để trống")]
        [StringLength(50, ErrorMessage = "CardType giới hạn độ dài là 50")]
        public string CardType { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CardTypeName không được để trống")]
        [StringLength(50, ErrorMessage = "CardTypeName giới hạn độ dài là 50")]
        public string CardTypeName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "cbdbm giới hạn độ dài là 20")]
        public string cbdbm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "cblb giới hạn độ dài là 50")]
        public string cblb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "grbh giới hạn độ dài là 200")]
        public string grbh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "xzlx giới hạn độ dài là 20")]
        public string xzlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "patid không được để trống")]
        public int patid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "hzxm không được để trống")]
        [StringLength(50, ErrorMessage = "hzxm giới hạn độ dài là 50")]
        public string hzxm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(5, ErrorMessage = "brxz giới hạn độ dài là 5")]
        public string brxz { get; set; }
    }

    public class SysPatCardIndexVO
    {
        public int patid { get; set; }
        public string hzxm { get; set; }
        public string CardNo { get; set; }
        public string CardTypeName { get; set; }
        public string brxz { get; set; }
    }
}
