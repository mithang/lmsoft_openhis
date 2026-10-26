using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// dân tộc
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_xt_mz", "民族")]
    public partial class SysNationVEntity : ISysEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int mzId { get; set; }

        /// <summary>
        /// mã
        /// </summary>
        public string mzCode { get; set; }

        /// <summary>
        /// tên
        /// </summary>
        public string mzmc { get; set; }

        /// <summary>
        /// chữ cái đầu của phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// cờ hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
