using System;
using SqlSugar;
namespace Newtouch.EMR.Domain.Entity
{

    [SugarTable("bl_hljl_yb")]
    public  class bl_hljl_ybEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rq { get; set; }
        /// <summary>
        /// ʱ
        /// </summary>
        public string sj { get; set; }
        /// <summary>
        /// סԺ
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// ȼ 1 ؼ 2 һ 3  4 
        /// </summary>
        public string hldj { get; set; }
        /// <summary>
        ///  1 2⻤ 3֤ʩ 4
        /// </summary>
        public string hllx { get; set; }
        /// <summary>
        /// ʷ
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tw { get; set; }
        /// <summary>
        /// Ƶ
        /// </summary>
        public string hxpl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ml { get; set; }
        /// <summary>
        /// ѹ
        /// </summary>
        public string ssy { get; set; }
        /// <summary>
        /// ѹ
        /// </summary>
        public string szy { get; set; }
        /// <summary>
        /// ѪͶ
        /// </summary>
        public string xybhd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xy { get; set; }
        /// <summary>
        /// 㱳
        /// </summary>
        public string zbdmbz { get; set; }
        /// <summary>
        /// ʳ 1 2һ 3ϲ
        /// </summary>
        public string ysqk { get; set; }
        /// <summary>
        /// ܻ
        /// </summary>
        public string dghl { get; set; }
        /// <summary>
        /// ܻ
        /// </summary>
        public string qghl { get; set; }
        /// <summary>
        /// λ
        /// </summary>
        public string twhl { get; set; }
        /// <summary>
        /// Ƥ
        /// </summary>
        public string pfhl { get; set; }
        /// <summary>
        /// Ӫ
        /// </summary>
        public string yyhl { get; set; }
        /// <summary>
        /// ʳָ 01ͨʳ 02ʳ 03ʳ 04ʳ 05ʳ 06ʳˮ 07ʳ 08ε֬ʳ 09ʳ 99
        /// </summary>
        public string yszd { get; set; }
        /// <summary>
        ///  1ݲ״ʵʩ 2֧
        /// </summary>
        public string xlhl { get; set; }
        /// <summary>
        /// ȫ 1ѲӲ 2Ӵ 3Լ֫
        /// </summary>
        public string aqhl { get; set; }
        /// <summary>
        /// Ҫ
        /// </summary>
        public string jybq { get; set; }
        /// <summary>
        /// ۲Ŀ
        /// </summary>
        public string hlgcxm { get; set; }
        /// <summary>
        /// ۲
        /// </summary>
        public string hlgcjg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hlczmc { get; set; }
        /// <summary>
        /// ĿĿ
        /// </summary>
        public string hlczxmmc { get; set; }
        /// <summary>
        /// ۲
        /// </summary>
        public string hlczjg { get; set; }
        /// <summary>
        /// ȫ˶Ա
        /// </summary>
        public string fcssaqb { get; set; }
        /// <summary>
        /// ջȫ˶Ա
        /// </summary>
        public string shssaqb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fcssfxpgb { get; set; }
        /// <summary>
        /// ջ
        /// </summary>
        public string shssfxpgb { get; set; }
        /// <summary>
        /// ־
        /// </summary>
        public string glbz { get; set; }
        /// <summary>
        ///  1 2 3Ӵ 4ѪҺ-Һ 5ܸ 6 7Ը
        /// </summary>
        public string glzl { get; set; }
        /// <summary>
        /// ʿǩ
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// ʿǩʱ 
        /// </summary>
        public string hsqmrqsj { get; set; }
        /// <summary>
        /// ʶ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// ͫ-
        /// </summary>
        public string tk_z { get; set; }
        /// <summary>
        /// ͫ-
        /// </summary>
        public string tk_y { get; set; }
        /// <summary>
        /// ԹⷴӦ-
        /// </summary>
        public string dgfs_z { get; set; }
        /// <summary>
        /// ԹⷴӦ-
        /// </summary>
        public string dgfs_y { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
    }
}
