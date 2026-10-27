using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Khoa hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_Sys_Department", "View khoa hệ thống")]
    public class SysDepartmentVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Cấp chaId
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Cờ y kỹ thuật(Có phải khoa y kỹ thuật hay không,như khoa X-quang)
        /// </summary>
        public bool yjbz { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú
        /// </summary>
        public byte mzzybz { get; set; }

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
