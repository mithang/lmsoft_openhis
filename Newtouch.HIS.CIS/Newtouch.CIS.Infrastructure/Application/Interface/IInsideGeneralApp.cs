using Newtouch.CIS.Compat;
using Newtouch.CIS.APIRequest.Dto;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// trongbộgiao diệngọinghiệp vụ
    /// </summary>
    public interface IInsideGeneralApp
    {
        /// <summary>
        /// công cộngvàomiệng
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        void PublicPortal(MqGeneralTaskRequestDto requestDto);

    }
}