using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Extension;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// 出入库单据管理
    /// </summary>
    [Area("BillManage")]
    public class OutOrInStorageBillController : ControllerBase
    {
        private readonly IApplyDmnService applyDmnService;

        #region Duyệt

        /// <summary>
        /// 单据审核 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult Approval()
        {
            return View();
        }
        public IActionResult DrugPreparation()
        {
            return View();
        }
        public IActionResult OutOrder()
		{
			return View();
		}

		/// <summary>
		/// Submit Receipt Approval
		/// </summary>
		/// <param name="param"></param>
		/// <returns></returns>
		public IActionResult SubmitReceiptApproval(OutOrInStorageBillApprovalDTO param)
        {
            param.auditor = UserIdentity.UserCode;
            var result = new OutOrInStorageBillApprovalProcess(param).Process();
            return result.IsSucceed ? Success("Thao tác thành công") : Error(result.ResultMsg);
        }

        /// <summary>
        /// Submit Receipt Approval
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IActionResult SubmitReceiptApprovalBatch(List<OutOrInStorageBillApprovalDTO> param)
        {
            var auditor = UserIdentity.UserCode;
            Parallel.ForEach(param, item => { item.auditor = auditor; });
            var result = new StringBuilder();
            param.ForEach(p =>
            {
                var processResult = new OutOrInStorageBillApprovalProcess(p).Process();
                if (!processResult.IsSucceed)
                {
                    result.AppendLine(processResult.ResultMsg);
                }

            });
            return result.Length <= 0 ? Success("Thao tác thành công") : Error(result.ToString());
        }
        #endregion

        #region Tra cứu

        /// <summary>
        /// 单据查询 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult Query()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 出入库单据明细查询
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        [HttpGet]
        [Core.Attributes.HandlerAuthorizeIgnore]
        public IActionResult QueryOutOrInStorageBillDetail(string crkId, int djlx)
        {
            var result = applyDmnService.SelectOutOrInStorageBillDetail(crkId, djlx);
            return Content(result.ToJson());
        }
        public IActionResult SelectDrupreparationInfoMX(string byid)
        {
            var result = applyDmnService.SelectDrupreparationInfoMX(byid);
            return Content(result.ToJson());
        }
        #endregion
    }
}