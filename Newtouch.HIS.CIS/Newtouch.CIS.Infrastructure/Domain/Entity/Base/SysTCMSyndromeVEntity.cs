using SqlSugar;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>Row of Base view V_S_xt_zyzh (TCM syndrome dictionary).</summary>
    [SugarTable("V_S_xt_zyzh")]
    public class SysTCMSyndromeVEntity
    {
        public string zhId { get; set; }
        public string zhCode { get; set; }
        public string zhmc { get; set; }
        public string py { get; set; }
        public string OrganizeId { get; set; }
        public string wb { get; set; }
        public string zt { get; set; }
    }
}
