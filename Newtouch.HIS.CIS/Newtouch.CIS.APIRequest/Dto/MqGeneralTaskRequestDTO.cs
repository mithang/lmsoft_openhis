using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Bản tin yêu cầu thông báo chung
    /// </summary>
    public class MqGeneralTaskRequestDto : RequestBase
    {
        /// <summary>
        /// Nội dung chính của thông báo
        /// </summary>
        public string body { get; set; }
    }

    /// <summary>
    /// Nội dung giải tuần tự hóa của MqGeneralTaskRequestDto.body
    /// </summary>
    public class MqGeneralTaskBody
    {
        /// <summary>
        /// Loại thao tác
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Nội dung, tham số truyền action
        /// </summary>
        public string Content { get; set; }
    }
}