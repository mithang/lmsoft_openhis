using Newtouch.Domain.Entity;
using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Chuẩn bị thuốc
    /// </summary>
    public class PreparationdrugsVO 
    {
        public string Id { get; set; }

        public string OrganizeId { get; set; }
        public string yfbm { get; set; }
        public string bqbm { get; set; }
        public string ksbm { get; set; }
        public string djh { get; set; }
        public string shzt { get; set; }
        public string tjsj { get; set; }
        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
        public string yfmc { get; set; }
        public string ksmc { get; set; }
        public string bqmc { get; set; }
    }
}
