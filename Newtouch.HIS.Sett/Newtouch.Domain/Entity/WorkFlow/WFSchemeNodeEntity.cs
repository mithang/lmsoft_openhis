using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("WF_SchemeNode")]
    public class WF_SchemeNodeEntity : IEntity<WF_SchemeNodeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemeInfoCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemeInfoVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 0Tất cả mọi người,1Chỉ định thành viên
        /// </summary>
        public bool AuthorizeType { get; set; }

        /// <summary>
        /// Enum Nút bắt đầu,Nút khởi tạo,Nút thông thường,Hoặc nút phân nhánh,Hoặc nút hợp nhất,Nút kết thúc
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? zdzxbz { get; set; }

        /// <summary>
        /// Enum 1 Không đồng ý(Kết thúc quy trình) 2 Từ chối về bước trước 3 Từ chối về nút khởi tạo
        /// </summary>
        public int? htlx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
