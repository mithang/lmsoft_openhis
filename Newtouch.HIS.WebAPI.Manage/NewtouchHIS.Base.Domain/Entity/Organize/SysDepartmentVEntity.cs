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
    ///khoa hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_Sys_Department", "系统科室视图")]
    public class SysDepartmentVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// cấp chaId
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// cờ kỹ thuật y tế（có phải là khoa kỹ thuật y tế hay không，như khoa X-quang）
        /// </summary>
        public bool yjbz { get; set; }

        /// <summary>
        /// cờ ngoại trú – nội trú
        /// </summary>
        public byte mzzybz { get; set; }

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
