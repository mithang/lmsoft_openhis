using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-10-31 14:15
    /// Mô tả:Bảng từ điển phẫu thuật
    /// </summary>
    [SugarTable("OR_Operation")]
    public class OROperationEntity : IEntityBase
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// ssdm
        /// </summary>
        /// <returns></returns>
        public string ssdm { get; set; }

        /// <summary>
        /// ssmc
        /// </summary>
        /// <returns></returns>
        public string ssmc { get; set; }

        /// <summary>
        /// zjm
        /// </summary>
        /// <returns></returns>
        public string zjm { get; set; }

        /// <summary>
        /// ssjb
        /// </summary>
        /// <returns></returns>
        public string ssjb { get; set; }

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