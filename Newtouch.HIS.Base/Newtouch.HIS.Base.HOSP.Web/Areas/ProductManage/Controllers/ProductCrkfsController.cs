using Newtouch.HIS.Application.Interface;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    [Area("ProductManage")]
    public class ProductCrkfsController : ControllerBase
    {
        private readonly IWzCrkfsRepo _wzCrkfsRepo;
        private readonly IWzCrkfsApp _wzCrkfsApp;

        public ProductCrkfsController(IWzCrkfsRepo wzCrkfsRepo, IWzCrkfsApp wzCrkfsApp)
        {
            _wzCrkfsRepo = wzCrkfsRepo;
            _wzCrkfsApp = wzCrkfsApp;
        }

        /// <summary>
        /// 获取出入库方式列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetCrkfsGridJson(Pagination pagination, string keyWord = "")
        {
            pagination.sidx = "crkfsmc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _wzCrkfsRepo.FindList(p => "" == keyWord || p.crkfsmc.Contains(keyWord.Trim()), pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            return _wzCrkfsRepo.DeleteCrkfsById(keyValue) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        /// <summary>
        /// 获取出入库方式信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzCrkfsRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        /// <summary>
        /// 物资单位维护 表单提交
        /// </summary>
        /// <param name="wzCrkfsEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(WzCrkfsEntity wzCrkfsEntity, string keyWord)
        {
            wzCrkfsEntity.zt = wzCrkfsEntity.zt == "true" ? "1" : "0";
            return _wzCrkfsApp.SubmitForm(wzCrkfsEntity, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }
    }
}