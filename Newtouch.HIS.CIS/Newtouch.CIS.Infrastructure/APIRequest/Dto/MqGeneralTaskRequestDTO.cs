using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Chung hủy tin yêu cầu báo văn
    /// </summary>
    public class MqGeneralTaskRequestDto : RequestBase
    {
        /// <summary>
        /// hủy tin chính nội dung
        /// </summary>
        public string body { get; set; }
    }

    /// <summary>
    /// MqGeneralTaskRequestDto.body phản thứ tự cột hóa nội dung
    /// </summary>
    public class MqGeneralTaskBody
    {
        /// <summary>
        /// Thao tác Loại
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// nội dung,action truyền tham số
        /// </summary>
        public string Content { get; set; }
    }
}