using Newtouch.Herp.Domain.DTO.OutputDto;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Phương pháp xử lý
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Quá trình xử lý
        /// </summary>
        /// <returns></returns>
        ActResult Process();
    }
}