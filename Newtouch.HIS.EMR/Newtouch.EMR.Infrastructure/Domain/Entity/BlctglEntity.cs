using System;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo:hyj
    /// Ngày:2018-09-07 15:46
    /// Mô tả:Quản lý từ mục
    /// </summary>
    [SugarTable("bl_ctgl")]
    public class BlctglEntity 
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string ID { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 0 Chung 1 Cá nhân 2 Khoa
        /// </summary>
        /// <returns></returns>
        public int? qx { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        /// <returns></returns>
        public string mc { get; set; }

        /// <summary>
        /// ksbm
        /// </summary>
        /// <returns></returns>
        public string ksbm { get; set; }
        /// <summary>
        /// Nội dung từ
        /// </summary>
        /// <returns></returns>
        public string ctnr { get; set; }
        /// <summary>
        /// py
        /// </summary>
        /// <returns></returns>
        public string py { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        /// 
        public string parentId { get; set; }
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
        public string zt { get; set; }

    }
}
