using Newtouch.HIS.Domain.DTO.OutputDto;
using System.Threading;

namespace Newtouch.HIS.Application.Strategy
{
    /// <summary>
    /// chiến lược
    /// </summary>
    public interface ICommitStrategy
    {
        /// <summary>
        /// thực hiện chiến lược
        /// </summary>
        /// <param name="cts"></param>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool Process(CancellationTokenSource cts, dynamic request, ActResult result);

        /// <summary>
        /// Hậu xử lý
        /// </summary>
        /// <param name="cts"></param>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool After(CancellationTokenSource cts, dynamic request, ActResult result);
    }
}
