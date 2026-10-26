using Newtouch.HIS.Sett.Request;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// nội bộ giao diện gọi nghiệp vụ 
    /// </summary>
    public interface IInsideGeneralApp
    {
        /// <summary>
        /// 公tổng cộng入口
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        void PublicPortal(MqGeneralTaskRequestDto requestDto);

    }
}