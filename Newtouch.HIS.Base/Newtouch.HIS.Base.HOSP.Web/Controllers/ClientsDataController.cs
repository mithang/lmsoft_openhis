using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    [HandlerLogin]
    public class ClientsDataController : Controller
    {
        private readonly ISysItemsRepository _sysItemsRepository;
        private readonly ISysItemsDetailRepository _sysItemsDetailRepository;
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        private readonly ISysDepartmentRepository _sysDepartRepo;

        public ClientsDataController(ISysItemsRepository sysItemsRepository
            , ISysItemsDetailRepository sysItemsDetailRepository, ISysModuleDmnService sysModuleDmnService, ISysOrganizeRepository sysOrganizeRepository, ISysDepartmentRepository sysDepartRepo, IUserRoleAuthDmnService userRoleAuthDmnService)
        {
            this._sysItemsRepository = sysItemsRepository;
            this._sysItemsDetailRepository = sysItemsDetailRepository;
            this._sysModuleDmnService = sysModuleDmnService;
            this._sysOrganizeRepository = sysOrganizeRepository;
            this._sysDepartRepo = sysDepartRepo;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetClientsDataJson()
        {
            // Controller mang [HandlerLogin] nên request ẩn danh vẫn vào đây:
            // không có session thì trả dữ liệu rỗng hợp lệ thay vì NRE kèm
            // stack trace ở GetCurrent().IsRoot như trước.
            if (OperatorProvider.GetCurrent() == null)
            {
                return Content(new
                {
                    dataItems = new Dictionary<string, object>(),
                    authorizeMenu = (object)null,
                }.ToJson());
            }
            var data = new
            {
                //Từ điển Bao gồm cả vô hiệu
                dataItems = OperatorProvider.GetCurrent().IsRoot==true?this.GetDataItemListRoot(): this.GetDataItemList(),
                ////Lấy tất cả tổ chức Bao gồm cả vô hiệu
                //organize = _sysOrganizeRepository.GetListByTopOrg(Constants.TopOrganizeId).ToList(),
                ////Lấy tất cả khoa Bao gồm cả vô hiệu
                //department = _sysDepartRepo.GetListByTopOrg(Constants.TopOrganizeId),
                authorizeMenu = this.GetMenuList(),
                //authorizeButton = this.GetMenuButtonList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Từ điển Bao gồm cả vô hiệu (Không root)
        /// </summary>
        /// <returns></returns>
        private object GetDataItemList()
        {
            var opr = OperatorProvider.GetCurrent();
            string orgId = null;
            if (!opr.IsRoot && !opr.IsAdministrator)
            {
                orgId = opr.OrganizeId;
            }
            var itemdata = _sysItemsDetailRepository.GetListByOrgId(orgId).ToList();
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


        /// <summary>
        /// Từ điển root
        /// </summary>
        /// <returns></returns>
        private object GetDataItemListRoot()
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

        private object GetMenuList()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr.IsRoot)
            {
                return null;
            }
            // Thu session RoleIdList truoc (khong doi hanh vi cu).
            var menuList = _sysModuleDmnService.GetMenuListByTopOrg(opr.OrganizeId, opr.RoleIdList, opr.IsRoot, opr.IsAdministrator);
            if (menuList == null || menuList.Count == 0)
            {
                // BHOSP-001: RoleIdList trong session duoc xay tu MainDB=UnionDb,
                // con menu query chay tren Base -> GUID Union khong khop quyen
                // Base nen menu rong. Re-resolve quyen tren chinh DB menu cua
                // user nay (chi doc, chi quyen Base that cua chinh user) roi thu lai.
                var baseRoleIds = _userRoleAuthDmnService.GetUserRoleList(opr.UserId, opr.OrganizeId)
                    ?.Select(p => p.Id).ToList();
                if (baseRoleIds != null && baseRoleIds.Count > 0)
                {
                    menuList = _sysModuleDmnService.GetMenuListByTopOrg(opr.OrganizeId, baseRoleIds, opr.IsRoot, opr.IsAdministrator);
                }
            }
            if (menuList == null)
            {
                return "[]";
            }
            return ToMenuJson(menuList, null);
        }

        private string ToMenuJson(IList<SysModuleEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<SysModuleEntity> entitys = data.Where(t => t.ParentId == parentId).ToList();
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.Id) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }
    }
}
