using Newtouch.Domain.Entity;
using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// chuẩn bị thuốc
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
        /// Sửa người dùng
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// Sửa cuốiNgười dùng
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Thời gian sửa cuối
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
