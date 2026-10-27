using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 药品出入库方式
    /// </summary>
    public class MedicineStorageIOModeController : ControllerBase
    {
        private readonly IXtypcrkfsRepo iXtypcrkfsRepo;

        /// <summary>
        /// 获取出入库方式（下拉数据源）
        /// </summary>
        /// <param name="crkbz"></param>
        /// <returns></returns>
        public IActionResult MedicineStorageIOModeList(string crkbz)
        {
            var result = iXtypcrkfsRepo.GetCrkfsList(crkbz).Select(i => new { id = i.crkfsCode, text = i.crkfsmc });
            return Content(result.ToJson());
        }

    }
}