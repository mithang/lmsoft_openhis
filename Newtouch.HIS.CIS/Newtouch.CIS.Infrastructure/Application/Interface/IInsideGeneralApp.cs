using Newtouch.CIS.Compat;
using Newtouch.CIS.APIRequest.Dto;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// trong bộ giao diện gọi nghiệp vụ
    /// </summary>
    public interface IInsideGeneralApp
    {
        /// <summary>
        /// công cộng vào miệng
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        void PublicPortal(MqGeneralTaskRequestDto requestDto);

    }
}