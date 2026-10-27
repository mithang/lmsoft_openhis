using System;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{

    [SugarTable("bl_bllx")]
    public  class bl_bllxEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// ͱʶ
        /// </summary>
        public string bllx { get; set; }
        public string bllxmc { get; set; }       
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// λ
        /// </summary>
        public string RelDutys { get; set; }
        /// <summary>
        /// ͼ
        /// </summary>
        public string bllxcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// ݱ ΪʱĬ
        /// </summary>
        public string relTB { get; set; }
        /// <summary>
        /// Ŀ¼㼶 ʼ㼶1
        /// </summary>
        public string MenuLev { get; set; }
        /// <summary>
        /// Ŀ¼㼶 Ĭһʱ ԡָ
        /// </summary>
        public string MenuLevName { get; set; }
        public string ParentId { get; set; }
        public string IsRoot { get; set; }
        /// <summary>
        /// ģʶ 1  
        /// </summary>
        public string mzbz { get; set; }
    }
}
