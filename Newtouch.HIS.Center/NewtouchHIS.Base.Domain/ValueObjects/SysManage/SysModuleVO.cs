namespace NewtouchHIS.Base.Domain.ValueObjects
{
    /// <summary>
    /// Menu hệ thống
    /// </summary>
    public class SysModuleVO
    {
        public string? Id { get; set; }
        /// <summary>
        /// Tổ chứcId(Tùy chọn,nullthì là dùng chung cho mọiTổ chứctổ chức)
        /// </summary>
        public string? OrganizeId { get; set; }
        /// <summary>
        /// Cấp cha
        /// </summary>
        /// <returns></returns>
        public string? ParentId { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        /// <returns></returns>
        public string? Name { get; set; }
        /// <summary>
        /// EnName
        /// </summary>
        /// <returns></returns>
        public string? EnName { get; set; }
        /// <summary>
        /// Mã
        /// </summary>
        /// <returns></returns>
        public string? Code { get; set; }
        /// <summary>
        /// Ứng dụngId,NhưCIS,Sett
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// Biểu tượng
        /// </summary>
        /// <returns></returns>
        public string? Icon { get; set; }
        /// <summary>
        /// nối
        /// </summary>
        /// <returns></returns>
        public string? UrlAddress { get; set; }
        /// <summary>
        /// Mục tiêu
        /// </summary>
        /// <returns></returns>
        public string? Target { get; set; }
        /// <summary>
        /// Mã sắp xếp
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// Mô tả
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
        /// Đã đồng bộ hay chưa
        /// </summary>
        public bool? IsSync { get; set; }
        /// <summary>
        /// Sắp xếp tùy chỉnh
        /// </summary>
        public string? CustomOrder { get; set; }

    }

}
