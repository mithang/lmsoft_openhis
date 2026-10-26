namespace NewtouchHIS.Base.Domain.ValueObjects
{
    /// <summary>
    /// menu hệ thống
    /// </summary>
    public class SysModuleVO
    {
        public string? Id { get; set; }
        /// <summary>
        /// tổ chứcId（có thể chọn，nullkhi là dùng chung cho tất cả tổ chức）
        /// </summary>
        public string? OrganizeId { get; set; }
        /// <summary>
        /// cấp cha
        /// </summary>
        /// <returns></returns>
        public string? ParentId { get; set; }
        /// <summary>
        /// tên
        /// </summary>
        /// <returns></returns>
        public string? Name { get; set; }
        /// <summary>
        /// EnName
        /// </summary>
        /// <returns></returns>
        public string? EnName { get; set; }
        /// <summary>
        /// mã
        /// </summary>
        /// <returns></returns>
        public string? Code { get; set; }
        /// <summary>
        /// ứng dụngId，Ví dụCIS、Sett
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// biểu tượng
        /// </summary>
        /// <returns></returns>
        public string? Icon { get; set; }
        /// <summary>
        /// kết nối
        /// </summary>
        /// <returns></returns>
        public string? UrlAddress { get; set; }
        /// <summary>
        /// mục tiêu
        /// </summary>
        /// <returns></returns>
        public string? Target { get; set; }
        /// <summary>
        /// mã sắp xếp
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// mô tả
        /// </summary>
        /// <returns></returns>
        public string? Description { get; set; }
        public string? zt { get; set; }
    }

    public class SysModuleWithChildVO : SysModuleVO
    {
        public List<SysModuleWithChildVO>? ChildNodes
        {
            get; set;
        }
    }

    public class SysModuleExtendVO : SysModuleVO
    {
        /// <summary>
        /// đã đồng bộ hay chưa
        /// </summary>
        public bool? IsSync { get; set; }
        /// <summary>
        /// sắp xếp tùy chỉnh
        /// </summary>
        public string? CustomOrder { get; set; }

    }

}
