using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("V_C_Sys_User")]
    public class SysUserVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LanguageType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserSecretkey { get; set; }

        /// <summary>
        /// làKhôngbịkhóađịnh.truekhóađịnh,Khôngthìchưakhóađịnh
        /// </summary>
        public bool? Locked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
