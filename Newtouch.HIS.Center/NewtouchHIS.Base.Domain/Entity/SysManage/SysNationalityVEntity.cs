using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// Vị trí hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_xt_gj", "Quốc tịch")]
    public partial class SysNationalityVEntity : ISysEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int gjId { get; set; }

        /// <summary>
        /// Mã
        /// </summary>
        public string gjCode { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string gjmc { get; set; }

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
