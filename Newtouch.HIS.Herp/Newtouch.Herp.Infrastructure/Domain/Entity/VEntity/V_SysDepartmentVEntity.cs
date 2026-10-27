using System.ComponentModel.DataAnnotations.Schema;

using SqlSugar;
namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Khoa phòng
    /// </summary>
    [SugarTable("V_S_Sys_Department")]
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
        /// Cấp cha Id
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
        /// Cờ kỹ thuật y tế（Có phải khoa kỹ thuật y tế không，như khoa X-quang）
        /// </summary>
        public bool yjbz { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú
        /// </summary>
        public byte mzzybz { get; set; }

        /// <summary>
        /// Chữ cái đầu pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Cờ hiệu lực
        /// </summary>
        public string zt { get; set; }

    }
}
