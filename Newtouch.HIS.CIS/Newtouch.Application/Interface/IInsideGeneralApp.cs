using Newtouch.CIS.APIRequest.Dto;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Nghiệp vụ gọi giao diện nội bộ
    /// </summary>
    public interface IInsideGeneralApp
    {
        /// <summary>
        /// Điểm vào chung
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        void PublicPortal(MqGeneralTaskRequestDto requestDto);

    }
}