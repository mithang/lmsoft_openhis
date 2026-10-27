using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.EMR.APIRequest
{
    /// <summary>
    /// 医疗机构API请求基类
    /// </summary>
    public class OrgJSONRequestBase : JSONRequestBase
    {
        /// <summary>
        /// 医疗机构Id
        /// </summary>
        [Required]
        public new string OrganizeId { get; set; }

    }
}
