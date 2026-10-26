using Newtouch.HIS.Sett.Request;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// nội bộ giao diện gọi nghiệp vụ 
    /// </summary>
    public interface IInsideGeneralApp
    {
        /// <summary>
        /// cổng vào chung
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        void PublicPortal(MqGeneralTaskRequestDto requestDto);

    }
}