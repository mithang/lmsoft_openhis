using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Herp.Web;

namespace Newtouch.Herp.Web.Areas.SupplierManage.Controllers
{
    /// <summary>
    /// Supplier maintenance. Port of the Framework SupplierController.
    /// View actions Index/Form are inherited from the vendor OrgControllerBase
    /// (virtual Index/Form returning View()), as in the Warehouse port.
    /// The original relied on binary field injection; this port takes every
    /// dependency through an explicit constructor (MS DI).
    /// </summary>
    [Area("SupplierManage")]
    public class SupplierController : ControllerBase
    {
        private readonly IGysSupplierRepo _gysSupplierRepo;
        private readonly IGysContactsRepo _gysContactsRepo;
        private readonly IGysSupplierDmnService _gysSupplierDmnService;
        private readonly ISupplierApp _supplierApp;

        public SupplierController(IGysSupplierRepo gysSupplierRepo, IGysContactsRepo gysContactsRepo,
            IGysSupplierDmnService gysSupplierDmnService, ISupplierApp supplierApp)
        {
            _gysSupplierRepo = gysSupplierRepo;
            _gysContactsRepo = gysContactsRepo;
            _gysSupplierDmnService = gysSupplierDmnService;
            _supplierApp = supplierApp;
        }

        /// <summary>
        /// Get supplier list (paged grid)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetSupplierGridJson(Pagination pagination, string keyWord)
        {
            var data = new
            {
                rows = _gysSupplierRepo.GetList(pagination, OrganizeId, keyWord),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// get enabled suppliers (select list)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSupplierSelectJson()
        {
            var list = _gysSupplierRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString()).ToList();
            return Content(list.ToJson());
        }

        /// <summary>
        /// Delete supplier (plus its contacts)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                _gysSupplierDmnService.DeleteSupplier(keyValue);
            }
            return Success("Xóa thành công");
        }

        /// <summary>
        /// get supplier information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyWord)
        {
            var supplier = _gysSupplierRepo.FindEntity(p => p.Id == keyWord && p.OrganizeId == OrganizeId);
            return Content(supplier.ToJson());
        }

        /// <summary>
        /// get supplier contacts
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetSupplierContactsGridJson(string keyWord)
        {
            var contacts = _gysContactsRepo.IQueryable(p => p.supplierId == keyWord && p.OrganizeId == OrganizeId).ToList();
            return Content(contacts.ToJson());
        }

        /// <summary>
        /// Supplier maintenance form submit
        /// </summary>
        /// <param name="supplierEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(GysSupplierEntity supplierEntity, string keyWord)
        {
            supplierEntity.zt = supplierEntity.zt == "true" ? "1" : "0";
            supplierEntity.OrganizeId = OrganizeId;
            _supplierApp.SubmitForm(supplierEntity, keyWord);
            return Success("Thao tác thành công", null);
        }

        /// <summary>
        /// Add / update a contact
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitContacts(SupplierContactVO contactInfo)
        {
            if (contactInfo != null && !string.IsNullOrWhiteSpace(contactInfo.id))
            {
                return _gysContactsRepo.UpdateContact(contactInfo) > 0 ? Success("Sửa người liên hệ thành công") : Error("Sửa người liên hệ thất bại");
            }
            return _gysContactsRepo.InserContact(contactInfo, OrganizeId) > 0 ? Success("Thêm người liên hệ thành công") : Error("Thêm người liên hệ thất bại");
        }

        /// <summary>
        /// delete a contact
        /// </summary>
        /// <param name="contactsId"></param>
        /// <returns></returns>
        public IActionResult DeleteContacts(string contactsId)
        {
            var entity = _gysContactsRepo.FindEntity(p => p.Id == contactsId && p.OrganizeId == OrganizeId);
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id)) _gysContactsRepo.Delete(entity);
            return Success("Xóa người liên hệ thành công");
        }

        /// <summary>
        /// Get producers (enabled)
        /// </summary>
        /// <returns></returns>
        public IActionResult GetProducers(string keyword)
        {
            var result = _gysSupplierRepo.GetList(OrganizeId, keyword, (int)EnumSupplierType.Producer, ((int)Enumzt.Enable).ToString());
            return Content(result.ToJson());
        }

        /// <summary>
        /// Get distributors (enabled)
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSuppliers(string keyword)
        {
            var result = _gysSupplierRepo.GetList(OrganizeId, keyword, (int)EnumSupplierType.Distributor, ((int)Enumzt.Enable).ToString());
            return Content(result.ToJson());
        }
    }
}
