using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure.EF;
using Newtouch.HIS.Application.Implementation;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.DrugStorage;
using Newtouch.PDS.Requset;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// Gửi phát thuốc nội bộ
    /// </summary>
    public class SubmitApplyOutStockProcess : ProcessorFun<List<ApplyOutStockVEntity>>
    {
        private readonly IApplyDmnService applyDmnService;
        private readonly ISysMedicineStockInfoDmnService kcxxDmnService;

        public SubmitApplyOutStockProcess(List<ApplyOutStockVEntity> request) : base(request)
        {
        }

        /// <summary>
        /// Kiểm tra
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null || Request.Count <= 0)
            {
                throw new FailedException("Chi tiết phát thuốc không được để trống");
            }
            return new ActResult();
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var group = new List<ApplyOutStockVEntity>();
            var source = Request.ToJson().ToObject<List<ApplyOutStockVEntity>>();
            var tmpCount = source.Count;
            int i = 0;
            var errorMsg = new StringBuilder();
            while (source.Count > 0 && (++i) <= tmpCount)
            {
                var curSlId = source[0].sldId;
                group = source.Where(p => p.sldId == curSlId).ToList();
                var result = applyDmnService.SubmitAppOutStock(group, source, OrganizeId, UserIdentity.UserCode);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    errorMsg.Append(result + ";");
                }
                if (source.Any(p => p.sldId == curSlId))
                {
                    source.Where(p => p.sldId == curSlId).ToList().ForEach(p => source.Remove(p));
                }
            }
            if (!string.IsNullOrWhiteSpace(errorMsg.ToString()))
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = errorMsg.ToString();
            }
        }

    }
}
