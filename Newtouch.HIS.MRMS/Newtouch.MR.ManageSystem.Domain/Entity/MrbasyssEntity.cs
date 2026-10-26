using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 11:05
    /// Mô tả: Bảng ghi phẫu thuật trang đầu bệnh án
    /// </summary>
    [Table("mr_basy_ss")]
    public class MrbasyssEntity : IEntity<MrbasyssEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// BAH
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }

        /// <summary>
        /// SSOrder
        /// </summary>
        /// <returns></returns>
        public int SSOrder { get; set; }

        /// <summary>
        /// Mã phẫu thuật và thao tác
        /// </summary>
        /// <returns></returns>
        public string SSJCZBM { get; set; }

        /// <summary>
        /// Ngày phẫu thuật và thao tác
        /// </summary>
        /// <returns></returns>
        public DateTime SSJCZRQ { get; set; }

        /// <summary>
        /// Cấp độ phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string SSJB { get; set; }

        /// <summary>
        /// Tên phẫu thuật và thao tác
        /// </summary>
        /// <returns></returns>
        public string SSJCZMC { get; set; }

        /// <summary>
        /// Người phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string SZ { get; set; }

        /// <summary>
        /// IPhụ
        /// </summary>
        /// <returns></returns>
        public string YZ { get; set; }

        /// <summary>
        /// IIPhụ
        /// </summary>
        /// <returns></returns>
        public string EZ { get; set; }

        /// <summary>
        /// Cấp độ vết mổ
        /// </summary>
        /// <returns></returns>
        public string QKDJ { get; set; }

        /// <summary>
        /// Loại liền vết mổ
        /// </summary>
        /// <returns></returns>
        public string QKYHLB { get; set; }
        /// <summary>
        /// Mô tả liền vết mổ
        /// </summary>
        public string QKYHDJ { get; set; }

        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        /// <returns></returns>
        public string MZFS { get; set; }

        /// <summary>
        /// Bác sĩ gây mê
        /// </summary>
        /// <returns></returns>
        public string MZYS { get; set; }
        public string OrganizeId { get; set; }

        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

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

    }
}