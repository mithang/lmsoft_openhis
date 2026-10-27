using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Nhật ký đăng nhập hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Log")]
    public class SysUserLoginLog
    {
        /// <summary>
        /// Nhật kýKhóa chính
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// Cấp cao nhấtTổ chứcId(có thể xác định duy nhấtAccount)
        /// </summary>
        public string TopOrganizeId { get; set; }
        /// <summary>
        /// Y tếTổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Ngày
        /// </summary>
        /// <returns></returns>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Tên người dùng
        /// </summary>
        /// <returns></returns>
        public string? Account { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        /// <returns></returns>
        public string? NickName { get; set; }
        /// <summary>
        /// Loại
        /// </summary>
        /// <returns></returns>
        public string? Type { get; set; }
        /// <summary>
        /// IPĐịa chỉ
        /// </summary>
        /// <returns></returns>
        public string? IPAddress { get; set; }
        /// <summary>
        /// IPThành phố nơi đặt IP
        /// </summary>
        /// <returns></returns>
        public string? IPAddressName { get; set; }
        /// <summary>
        /// Mô-đun hệ thốngId
        /// </summary>
        /// <returns></returns>
        public string? ModuleId { get; set; }
        /// <summary>
        /// Mô-đun hệ thống
        /// </summary>
        /// <returns></returns>
        public string? ModuleName { get; set; }
        /// <summary>
        /// Kết quả
        /// </summary>
        /// <returns></returns>
        public bool? Result { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        /// <returns></returns>
        public string? Description { get; set; }
        /// <summary>
        /// Thời gian tạo
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
