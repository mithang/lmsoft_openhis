using Newtonsoft.Json;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    ///<summary>
    ///thông tin nhân viênVO
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
        /// Desc:khóa chính tổ chức
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
        /// Desc:họ tên
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
        /// Desc:chức danh
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("Title")]
        public string zc { get; set; }

        /// <summary>
        /// Desc:ảnh đại diện
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? HeadIcon { get; set; }

        /// <summary>
        /// Desc:giới tính
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? Gender { get; set; }

        /// <summary>
        /// Desc:sinh nhật
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Desc:điện thoại di động
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? MobilePhone { get; set; }

        /// <summary>
        /// Desc:hộp thư
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
        /// Desc:khóa chính cấp chủ quản
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? ManagerCode { get; set; }

        /// <summary>
        /// Desc:mã sắp xếp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

        /// <summary>
        /// Desc:mô tả
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? Description { get; set; }

        /// <summary>
        /// Desc:thời gian tạo
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:người tạo
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? CreatorCode { get; set; }

        /// <summary>
        /// Desc:thời gian sửa đổi lần cuối
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:người sửa đổi lần cuối
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
