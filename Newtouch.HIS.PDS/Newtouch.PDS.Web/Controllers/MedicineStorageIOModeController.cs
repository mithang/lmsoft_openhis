using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Phương thức xuất nhập kho thuốc
    /// </summary>
    public class MedicineStorageIOModeController : ControllerBase
    {
        private readonly IXtypcrkfsRepo iXtypcrkfsRepo;

        /// <summary>
        /// Lấy phương thức xuất nhập kho(Nguồn dữ liệu thả xuống)
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