using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.SSO.Domain.SysManage
{
    /// <summary>
    /// menu hệ thống
    /// </summary>
    public class SysModuleVO
    {
        public string? Id { get; set; }
        /// <summary>
        /// tổ chứcId（tùy chọn，nullkhi đó dùng chung cho tất cả các tổ chức）
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
        /// mã hóa
        /// </summary>
        /// <returns></returns>
        public string? Code { get; set; }
        /// <summary>
        /// ứng dụngId，ví dụCIS、Sett
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
    }
}
