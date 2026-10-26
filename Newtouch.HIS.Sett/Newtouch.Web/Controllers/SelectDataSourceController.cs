using Newtouch.HIS.Domain.IRepository;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// （Thả xuống）Chọn Nguồn dữ liệu
    /// </summary>
    public class SelectDataSourceController : ControllerBase
    {
        private readonly ISysPatientNatureRepo _sysPatiNatureRepo;

        /// <summary>
        /// Lấy danh sách hợp lệ tính chất bệnh nhân
        /// </summary>
        /// <returns></returns>
        public ActionResult getEffectPatiNatureList()
        {
            var treeList = _sysPatiNatureRepo.getEffectPatiNatureList(this.OrganizeId);
            return Content(treeList);
        }

    }
}