using Newtouch.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// yêu cầu chung
    /// </summary>
    public class ComController : ControllerBase
    {
        private readonly IItemDmnService _itemDmnService;

        /// <summary>
        /// lấy nội dung hộp thả xuống
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetSelectItemsDetailListByItemCode(string code)
        {
            var data = _itemDmnService.GetItemsDetailListByOrgIdAndItemCode(this.OrganizeId, code, "1");
            var treeList = data.Select(item => new TreeSelectModel
            {
                id = item.Code,
                text = item.Name,
                parentId = null
            }).ToList();
            return Content(treeList.TreeSelectJson(null));
        }
    }
}