using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术排班记录
    /// </summary>
    public class ORArrangementController : OrgControllerBase
    {
        private readonly IORArrangementRepo _oRArrangementRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="oRArrangementRepo"></param>
        public ORArrangementController(IORArrangementRepo oRArrangementRepo)
        {
            this._oRArrangementRepo = oRArrangementRepo;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _oRArrangementRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        //public IActionResult SubmitForm(ORArrangementEntity entity, string keyValue)
        //{
        //    _oRArrangementRepo.SubmitForm(entity, keyValue);
        //    return Success("操作成功。");
        //}

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _oRArrangementRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }

    }
}