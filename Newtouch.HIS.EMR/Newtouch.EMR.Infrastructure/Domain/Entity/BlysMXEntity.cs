using System;
using SqlSugar;
namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo：mhz
    /// Ngày：2023-04-17 11:24
    /// Mô tả：Phần tử
    /// </summary>
    [SugarTable("bl_ysmx")]
    public class BlysMXEntity 
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string OrganizeId { get; set; }
        public string YsId { get; set; }
        public string YsmxCode { get; set; }
        public string YsmxName { get; set; }
        public int Px { get; set; }
        public string Py { get; set; }
        public int Sybz { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public int Zt { get; set; }
        

        
    }
}
