using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 13:45
    /// Mô tả: Chẩn đoán bệnh lý
    /// </summary>
    [Table("mr_dic_blzd")]
    public class MrdicblzdEntity : IEntity<MrdicblzdEntity>
    {
        /// <summary>
        /// zdbm
        /// </summary>
        /// <returns></returns>
        public string zdbm { get; set; }

        /// <summary>
        /// zdmc
        /// </summary>
        /// <returns></returns>
        public string zdmc { get; set; }

        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Trạng thái     1:Có hiệu lực, 0: Không hiệu lực
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
        /// Thời gian sửa đổi
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

    }
}