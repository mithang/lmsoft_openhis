using NewtouchHIS.Base.Domain.Model;

namespace NewtouchHIS.Framework.Web.ServiceModels
{
    public class QueryParamsRequest : QueryParamsBase
    {
        /// <summary>
        /// chỉ giới hạn có hiệu lực
        /// </summary>
        public bool? validLimit { get; set; }

    }
}
