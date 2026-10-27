using Newtouch.MR.ManageSystem.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MRMS.Web.Models;
using Newtouch.MR.ManageSystem.Domain.IRepository;

namespace Newtouch.MRMS.Web.Controllers
{
    /// <summary>
    /// 创 建：chl
    ///  ngày 期：2019-11-20 11:05
    /// 描 述：Trang đầu bệnh án
    /// </summary>
    public class MrbasyController : OrgControllerBase
    {
        private readonly IMrbasyRepo _mrbasyRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="mrbasyRepo"></param>
        public MrbasyController(IMrbasyRepo mrbasyRepo)
        {
            this._mrbasyRepo = mrbasyRepo;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _mrbasyRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu表单（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult SubmitForm(MrbasyEntity entity, string keyValue)
        {
            _mrbasyRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Xóa数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _mrbasyRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }

    }
}