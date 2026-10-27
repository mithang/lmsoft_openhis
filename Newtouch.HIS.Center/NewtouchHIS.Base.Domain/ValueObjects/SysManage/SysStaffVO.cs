using Newtonsoft.Json;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    ///<summary>
    ///Thông tin công nhân viên chứcVO
    ///</summary>
    public partial class SysStaffVO
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? Id { get; set; }

        /// <summary>
        /// Desc:Tổ chứcKhóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? OrganizeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? TopOrganizeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? gh { get; set; }

        /// <summary>
        /// Desc:Họ tên
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? Name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Desc:Chức danh
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("Title")]
        public string zc { get; set; }

        /// <summary>
        /// Desc:Ảnh đại diện
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? HeadIcon { get; set; }

        /// <summary>
        /// Desc:Giới tính
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? Gender { get; set; }

        /// <summary>
        /// Desc:Ngày sinh
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Desc:Điện thoại di động
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? MobilePhone { get; set; }

        /// <summary>
        /// Desc:Email
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? Email { get; set; }

        /// <summary>
        /// Desc:WeChat
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? WeChat { get; set; }

        /// <summary>
        /// Desc:Quản lýKhóa chính
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? ManagerCode { get; set; }

        /// <summary>
        /// Desc:Mã sắp xếp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

        /// <summary>
        /// Desc:Mô tả
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? Description { get; set; }

        /// <summary>
        /// Desc:Thời gian tạo
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:Người tạo
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? CreatorCode { get; set; }

        /// <summary>
        /// Desc:Thời gian sửa đổi cuối
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:Người sửa đổi cuối
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? zt { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? kflb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? mbqx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? zjlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? zjh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? gjybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? MsEmail { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? yszydm { get; set; }

    }
}
