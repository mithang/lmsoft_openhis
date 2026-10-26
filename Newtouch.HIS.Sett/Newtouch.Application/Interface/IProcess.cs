using Newtouch.HIS.Domain.DTO.OutputDto;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Xử lý Phư ơng thức
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Xử lý quá trình 
        /// </summary>
        /// <returns></returns>
        ActResult Process();
    }
}
