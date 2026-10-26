using System.Linq;
using System.Text;
using Newtouch.Herp.Application.Interface;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Điều chỉnh giá
    /// </summary>
    public class AdjustPriceApp : AppBase, IAdjustPriceApp
    {
        /// <summary>
        /// Thực hiện lãi/lỗ điều chỉnh giá
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string AdjustPriceExecute(string ids)
        {
            var result = new StringBuilder();
            var idList = ids.TrimEnd(',').Split(',').ToList();
            if (idList.Count > 0)
            {
                idList.ForEach(p =>
                {
                    var exRes = new AdjustPriceExecuteProcess(p).Process();
                    if (!exRes.IsSucceed) result.AppendLine(exRes.ResultMsg);
                });
            }
            return result.ToString();
        }
    }
}
