using Newtouch.Infrastructure.EF;
using Newtouch.HIS.Application.Implementation;
using Newtouch.Core.Common.Utils;
using FrameworkBase.MultiOrg.DmnService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Common.Web;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.Tools;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// Trả thuốc ngoại trú
    /// </summary>
    public class OutpatientReturnDrugsProcess : ProcessorFun<tyInfo>
    {
        private readonly ItyDmnService _tyDmnService;

        public OutpatientReturnDrugsProcess(tyInfo request) : base(request)
        {
        }

        protected override ActResult Validata()
        {
            if (Request == null || Request.tyDrugDetail == null || Request.tyDrugDetail.Count == 0)
            {
                throw new FailedException("Vui lòng truyền thuốc cần trả");
            }

            return base.Validata();
        }

        protected override void BeforeAction(ActResult actResult)
        {
            Parallel.ForEach(Request.tyDrugDetail, p =>
            {
                if (p.sl <= 0)
                {
                    Request.tyDrugDetail.Remove(p);
                }
            });
            if (Request.tyDrugDetail.Count != 0)
            {
                return;
            }

            actResult.IsSucceed = false;
            actResult.ResultMsg = "Số lượng trả thuốc phải lớn hơn 0";
        }

        protected override void Action(ActResult actResult)
        {
            var returnDrugBillNo = "";
            var tyResult = _tyDmnService.ReturnDrugSingleThread(Request, out returnDrugBillNo);
            if (string.IsNullOrWhiteSpace(tyResult))
            {
                actResult.Data = returnDrugBillNo;
                return;
            }
            actResult.IsSucceed = false;
            actResult.ResultMsg = tyResult;
        }

        protected override void AfterAction(ActResult actResult)
        {
            var cfxx = new MzCfRepo(new DefaultDatabaseFactory()).FindEntity(p =>
                p.cfh == Request.cfh && p.zt == "1" && p.OrganizeId == OrganizeId);
            if (cfxx == null)
            {
                return;
            }
            var errorMsg = new StringBuilder();
            Request.tyDrugDetail.ForEach(p =>
            {
                try
                {
                    var cfmx = new MzCfmxRepo(new DefaultDatabaseFactory()).FindEntity(d => d.cfh == Request.cfh && d.ypCode == p.ypCode && d.zt == "1" && d.OrganizeId == OrganizeId);
                    if (cfmx == null)
                    {
                        return;
                    }
                    var request = new
                    {
                        cfnm = (int)cfxx.cfnm,
                        yp = p.ypCode,
                        sl = (decimal)p.sl * p.zhyz / cfmx.zhyz,
                        czh = p.czh,
                        OrganizeId = Request.organizeId
                    };
                    var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/OutpatientDrugWithdrawalNotify", request);
                    if (apiResp == null || apiResp.data == null)
                    {
                        errorMsg.Append(string.Format("Đơn thuốc【{0}】Giao diện trả thuốc ngoại trú bất thường，Vui lòng liên hệ quản trị viên；", p.cfh));
                    }
                }
                catch (Exception e)
                {
                    return;
                }
            });

        }
    }
}