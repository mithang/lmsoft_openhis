
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Khoa hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_xt_bq", "SysWardVEntity")]
    public class SysWardVEntity
    {
        public int bqId { get; set; }
        public string bqCode { get; set; }
        public string bqmc { get; set; }
        public string OrganizeId { get; set; }
        public string py { get; set; }
        public string zt { get; set; }
    }
}
