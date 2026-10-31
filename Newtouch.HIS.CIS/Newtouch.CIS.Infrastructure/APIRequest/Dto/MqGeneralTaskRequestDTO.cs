using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Chunghủytinyêu cầubáovăn
    /// </summary>
    public class MqGeneralTaskRequestDto : RequestBase
    {
        /// <summary>
        /// hủytinchínhnội dung
        /// </summary>
        public string body { get; set; }
    }

    /// <summary>
    /// MqGeneralTaskRequestDto.bodyphảnthứ tựcộthóanội dung
    /// </summary>
    public class MqGeneralTaskBody
    {
        /// <summary>
        /// Thao tácLoại
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// nội dung,actiontruyềntham số
        /// </summary>
        public string Content { get; set; }
    }
}