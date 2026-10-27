using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-09-30 14:54
    /// 描 述：错误提示配置
    /// </summary>
    [Area("SystemManage")]

    public class SysFailedCodeMessageMappController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysFailedCodeMessageMappRepo _sysFailedCodeMessageMappRepo;
        public SysFailedCodeMessageMappController(ISysFailedCodeMessageMappRepo sysFailedCodeMessageMappRepo)
        {
            _sysFailedCodeMessageMappRepo = sysFailedCodeMessageMappRepo;
        }


        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        /// <returns></returns>
        public IActionResult GetPagintionGridJson(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _sysFailedCodeMessageMappRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _sysFailedCodeMessageMappRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysFailedCodeMessageMappEntity entity, string keyValue)
        {
            _sysFailedCodeMessageMappRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _sysFailedCodeMessageMappRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }

    }
}
