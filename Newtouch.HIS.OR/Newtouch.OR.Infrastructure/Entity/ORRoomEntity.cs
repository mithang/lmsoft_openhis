using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-11-06 10:22
    /// Mô tả:Phòng phẫu thuật
    /// </summary>
    [SugarTable("OR_Room")]
    public class ORRoomEntity : IEntityBase
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>
        /// Addr
        /// </summary>
        /// <returns></returns>
        public string Addr { get; set; }

        /// <summary>
        /// Trạng thái     1:Có hiệu lực  0:Không hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

    }
}