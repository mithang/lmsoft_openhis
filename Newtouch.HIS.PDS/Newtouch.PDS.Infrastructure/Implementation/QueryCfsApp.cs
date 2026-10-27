using Newtouch.HIS.Domain.Entity;
using System;
using Newtouch.Tools;

using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Lấy đơn thuốc chờ xếp thuốc
    /// </summary>
    public class QueryCfsApp : ProcessorFun<string>
    {
        private readonly IMzCfRepo mzCfRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        public QueryCfsApp(string request) : base(request)
        {
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var ago = -7;
            var td = ConfigurationHelper.GetAppConfigValue("pyCfBeforDaysAgo");
            int.TryParse(td, out ago);
            var cfList = mzCfRepo.GetNoArrangedCfList(DateTime.Now.AddDays(ago), DateTime.Now);
            if (cfList != null && cfList.Count > 0)
            {
                actResult.Data = cfList;
            }
            else
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = "Không lấy được đơn thuốc chưa xếp lịch，Vui lòng đồng bộ đơn thuốc！";
            }
        }
    }
}
