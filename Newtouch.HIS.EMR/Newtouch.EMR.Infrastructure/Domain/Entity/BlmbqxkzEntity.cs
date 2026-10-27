using System;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2018-09-20 14:48
    /// Mô tả：Bảng kiểm soát quyền mẫu
    /// </summary>
    [SugarTable("bl_mbqxkz")]
    public class BlmbqxkzEntity 
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// mbId
        /// </summary>
        /// <returns></returns>
        public string mbId { get; set; }
        /// <summary>
        /// dutyId
        /// </summary>
        /// <returns></returns>
        public string dutyCode { get; set; }
        /// <summary>
        /// Tên vị trí
        /// </summary>
        /// <returns></returns>
        public string dutyName { get; set; }
        /// <summary>
        /// 0 Không có quyền 1 Quyền chỉ đọc 2 Quyền đọc/ghi 3 Kiểm soát hoàn toàn
        /// </summary>
        /// <returns></returns>
        public int ctrlLevel { get; set; }
        /// <summary>
        /// Mô tả cấp độ kiểm soát quyền
        /// </summary>
        /// <returns></returns>
        public string ctrlLevelDesc { get; set; }
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
    }
}
