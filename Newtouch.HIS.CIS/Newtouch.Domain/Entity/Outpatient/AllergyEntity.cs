using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Thông tin dị ứng
    /// </summary>
    [Table("xt_gmxx")]
    public class AllergyEntity : IEntity<AllergyEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcid
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// Hạng mục dị ứngCode
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Tên hạng mục dị ứng
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorName { get; set; }

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
        public string LastModifierName { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Y lệnhid
        /// </summary>
        public string yzid { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốcid
        /// </summary>
        public string cfmxid { get; set; }

        /// <summary>
        /// Trạng thái thực hiện
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Loại dị ứng
        /// </summary>
        public string gmlx { get; set; }

        /// <summary>
        /// Thuốc gây dị ứngCode
        /// </summary>
        public string ypCode { get; set; }
    }
}
