using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Chung消息bản tin yêu cầu
    /// </summary>
    public class MqGeneralTaskRequestDto : RequestBase
    {
        /// <summary>
        /// 消息主 nội dung 
        /// </summary>
        public string body { get; set; }
    }

    /// <summary>
    /// MqGeneralTaskRequestDto.body反 tuần tự hóa nội dung 
    /// </summary>
    public class MqGeneralTaskBody
    {
        /// <summary>
        /// Thao tác loại xnhS27: gọi BHYT nông thôn mới S27giao diện, 
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// nội dung,action truyền 参
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