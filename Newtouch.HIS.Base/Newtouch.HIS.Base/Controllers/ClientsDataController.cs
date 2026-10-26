using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Base.Controllers
{
    [HandlerLogin]
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
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                //Từ điển, bao gồm cả các mục không hiệu lực
                dataItems = this.GetDataItemList(),
                //Lấy tổ chức cấp cao nhất, bao gồm cả các mục không hiệu lực
                topOrganize = _sysOrganizeRepository.GetTopOrgList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Từ điển, bao gồm cả các mục không hiệu lực
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
