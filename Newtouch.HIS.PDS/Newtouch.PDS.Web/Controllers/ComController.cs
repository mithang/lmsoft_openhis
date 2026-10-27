using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Common;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Yêu cầu chung
    /// </summary>
    public class ComController : ControllerBase
    {
        private readonly IItemDmnService _itemDmnService;

        /// <summary>
        /// Lấy nội dung hộp thả xuống
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IActionResult GetSelectItemsDetailListByItemCode(string code)
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