using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// Dân tộc
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_xt_mz", "Dân tộc")]
    public partial class SysNationVEntity : ISysEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int mzId { get; set; }

        /// <summary>
        /// Mã
        /// </summary>
        public string mzCode { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string mzmc { get; set; }

        /// <summary>
        /// Chữ cái đầu phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Cờ hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
