using System;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{

    [SugarTable("bl_ysjgnr")]
    public  class bl_ysjgnrEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string blid { get; set; }
        public string zyh { get; set; }
        public string bllx { get; set; }
        public string yscode { get; set; }
        public string ysvalue { get; set; }
        public string ystext { get; set; }
        public string ysid { get; set; }
        public string ysmc { get; set; }
        public string OrganizeId { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
    }
}
