using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FinancialInvoiceController : ControllerBase
    {
        private readonly IFinancialInvoiceRepo _financialInvoiceRepo;
        private readonly ISysFinancialDmnService _sysFinancialDmnService;

        public ActionResult FinanicalInvoiceIndex()
        {
            return View();
        }

        public ActionResult InvoiceQuery()
        {
            return View();
        }

        //[HandlerAuthorize]
        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// Lấy danh sách hợp lệ
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetFinancialInvoiceList(string keyword)
        {
            var data = _financialInvoiceRepo.GetFinancialInvoiceList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }
        public ActionResult GetCwfpList(string keyword)
        {
            var data = _sysFinancialDmnService.GetCwfpList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửa Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string fpdm)
        {
            var entity = _financialInvoiceRepo.GetFinancialInvoiceEntity(fpdm, this.OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(FinancialInvoiceEntity financialInvoiceEntity, string fpdm)
        {
            financialInvoiceEntity.zt = financialInvoiceEntity.zt == "true" ? "1" : "0";
            financialInvoiceEntity.OrganizeId = this.OrganizeId;
            _financialInvoiceRepo.VlidateRepeat(financialInvoiceEntity, OrganizeId,fpdm);
            _financialInvoiceRepo.SubmitForm(financialInvoiceEntity, fpdm);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string fpdm)
        {
            _financialInvoiceRepo.DeleteForm(fpdm, this.OrganizeId);
            return Success("操作成功。");
        }
    }
}