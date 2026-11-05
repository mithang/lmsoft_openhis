using Newtouch.HIS.Domain.IDomainServices;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Base.Web.Controllers
{
    // BA-001 (FA-017-B2): KHÔNG mang [HandlerLogin] nữa — attribute đó miễn
    // controller khỏi FrameworkAuthorizationFilter toàn cục (Program.cs) nên
    // request ẩn danh vẫn nhận đủ từ điển + top org (13KB). Endpoint này chỉ
    // được framework-clientdata.js gọi, mà file đó chỉ Home/Index load, và
    // Home/Index đã redirect ẩn danh về /Login/Index — nên bắt auth ở đây
    // không phá luồng bootstrap của client đã đăng nhập; request ẩn danh
    // (AJAX) nhận SESSION_TIMEOUT như mọi controller khác, không còn dữ liệu.
    public class ClientsDataController : Controller
    {
        private readonly ISysItemsRepository _sysItemsRepository;
        private readonly ISysItemsDetailRepository _sysItemsDetailRepository;
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        private readonly ISysDepartmentRepository _sysDepartRepo;

        public ClientsDataController(ISysItemsRepository sysItemsRepository
            , ISysItemsDetailRepository sysItemsDetailRepository, ISysModuleDmnService sysModuleDmnService, ISysOrganizeRepository sysOrganizeRepository, ISysDepartmentRepository sysDepartRepo)
        {
            this._sysItemsRepository = sysItemsRepository;
            this._sysItemsDetailRepository = sysItemsDetailRepository;
            this._sysModuleDmnService = sysModuleDmnService;
            this._sysOrganizeRepository = sysOrganizeRepository;
            this._sysDepartRepo = sysDepartRepo;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetClientsDataJson()
        {
            var data = new
            {
                //Từ điển Bao gồm cả vô hiệu
                dataItems = this.GetDataItemList(),
                //Lấy tổ chức cấp cao nhất Bao gồm cả vô hiệu
                topOrganize = _sysOrganizeRepository.GetTopOrgList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Từ điển Bao gồm cả vô hiệu
        /// </summary>
        /// <returns></returns>
        private object GetDataItemList()
        {
            var itemdata = _sysItemsDetailRepository.GetCommonList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in _sysItemsRepository.GetList())
            {
                var dataItemList = itemdata.Where(t => t.ItemId.Equals(item.Id)).ToList();
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.Code, itemList.Name);
                }
                dictionaryItem.Add(item.Code, dictionaryItemList);
            }
            return dictionaryItem;
        }

    }
}
