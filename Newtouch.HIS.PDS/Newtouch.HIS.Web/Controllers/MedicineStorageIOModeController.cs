using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// thuốc phương thức nhập/xuất kho
    /// </summary>
    public class MedicineStorageIOModeController : ControllerBase
    {
        private readonly IXtypcrkfsRepo iXtypcrkfsRepo;

        /// <summary>
        /// lấy phương thức nhập/xuất kho (thả xuống nguồn dữ liệu)
        /// </summary>
        /// <param name="crkbz"></param>
        /// <returns></returns>
        public ActionResult MedicineStorageIOModeList(string crkbz)
        {
            var result = iXtypcrkfsRepo.GetCrkfsList(crkbz).Select(i => new { id = i.crkfsCode, text = i.crkfsmc });
            return Content(result.ToJson());
        }

    }
}