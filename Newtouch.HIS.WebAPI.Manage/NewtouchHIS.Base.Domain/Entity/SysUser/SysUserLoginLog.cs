using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///nhật ký đăng nhập hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Log")]
    public class SysUserLoginLog
    {
        /// <summary>
        /// khóa chính nhật ký
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// tổ chức cấp cao nhấtId（có thể xác định duy nhấtAccount）
        /// </summary>
        public string TopOrganizeId { get; set; }
        /// <summary>
        /// cơ sở y tế
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// ngày
        /// </summary>
        /// <returns></returns>
        public DateTime? Date { get; set; }
        /// <summary>
        /// tên người dùng
        /// </summary>
        /// <returns></returns>
        public string? Account { get; set; }
        /// <summary>
        /// họ tên
        /// </summary>
        /// <returns></returns>
        public string? NickName { get; set; }
        /// <summary>
        /// loại
        /// </summary>
        /// <returns></returns>
        public string? Type { get; set; }
        /// <summary>
        /// IPđịa chỉ
        /// </summary>
        /// <returns></returns>
        public string? IPAddress { get; set; }
        /// <summary>
        /// IPthành phố nơi ở
        /// </summary>
        /// <returns></returns>
        public string? IPAddressName { get; set; }
        /// <summary>
        /// mô-đun hệ thốngId
        /// </summary>
        /// <returns></returns>
        public string? ModuleId { get; set; }
        /// <summary>
        /// mô-đun hệ thống
        /// </summary>
        /// <returns></returns>
        public string? ModuleName { get; set; }
        /// <summary>
        /// kết quả
        /// </summary>
        /// <returns></returns>
        public bool? Result { get; set; }
        /// <summary>
        /// mô tả
        /// </summary>
        /// <returns></returns>
        public string? Description { get; set; }
        /// <summary>
        /// thời gian tạo
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// px
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
    }
}
