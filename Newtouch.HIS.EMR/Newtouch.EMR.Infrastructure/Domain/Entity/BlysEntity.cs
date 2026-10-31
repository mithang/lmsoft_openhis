using System;
using SqlSugar;
namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo:hyj
    /// Ngày:2018-10-30 11:24
    /// Mô tả:Phần tử
    /// </summary>
    [SugarTable("bl_ys")]
    public class BlysEntity 
    {
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
        public string zt { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// yssjid
        /// </summary>
        /// <returns></returns>
        public string yssjid { get; set; }
        /// <summary>
        /// ysmc
        /// </summary>
        /// <returns></returns>
        public string ysmc { get; set; }
        /// <summary>
        /// Mã phần tử là dấu hiệu duy nhất
        /// </summary>
        public string yscode { get; set; }
        /// <summary>
        /// Văn bản nền
        /// </summary>
        /// <returns></returns>
        public string BackgroundText { get; set; }
        /// <summary>
        /// Nguồn dữ liệu
        /// </summary>
        /// <returns></returns>
        public string DataSource { get; set; }
        /// <summary>
        /// Đường dẫn
        /// </summary>
        /// <returns></returns>
        public string BindingPath { get; set; }
        /// <summary>
        /// AutoUpdate
        /// </summary>
        /// <returns></returns>
        public string AutoUpdate { get; set; }
        /// <summary>
        /// Có chỉ đọc hay không
        /// </summary>
        /// <returns></returns>
        public string Readonly { get; set; }
        /// <summary>
        /// Loại dữ liệu
        /// </summary>
        /// <returns></returns>
        public string TypeName { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// Có phải là danh sách thả xuống hay không
        /// </summary>
        /// <returns></returns>
        public int? ListSource { get; set; }
        /// <summary>
        /// UserEditable
        /// </summary>
        /// <returns></returns>
        public string UserEditable { get; set; }
        /// <summary>
        /// DropdownList
        /// </summary>
        /// <returns></returns>
        public string EditStyle { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        /// <returns></returns>
        public string Value { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// Style
        /// </summary>
        /// <returns></returns>
        public string Style { get; set; }
        /// <summary>
        /// Format
        /// </summary>
        /// <returns></returns>
        public string Format { get; set; }
        public int? yslx { get; set; }
        public int? px { get; set; }
    }
}
