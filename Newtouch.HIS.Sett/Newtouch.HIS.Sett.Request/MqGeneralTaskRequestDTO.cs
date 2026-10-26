using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Yêu cầu bản tin tin nhắn chung
    /// </summary>
    public class MqGeneralTaskRequestDto : RequestBase
    {
        /// <summary>
        /// Nội dung chính tin nhắn 
        /// </summary>
        public string body { get; set; }
    }

    /// <summary>
    /// Nội dung giải tuần tự hóa MqGeneralTaskRequestDto.body 
    /// </summary>
    public class MqGeneralTaskBody
    {
        /// <summary>
        /// Thao tác loại xnhS27: gọi BHYT nông thôn mới S27giao diện, 
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// nội dung, tham số truyền action
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// Tra cứuQuyết toánKết quả
    /// </summary>
    public class QuerySettDetail
    {
        /// <summary>
        /// ID tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string outpId { get; set; }
    }
}