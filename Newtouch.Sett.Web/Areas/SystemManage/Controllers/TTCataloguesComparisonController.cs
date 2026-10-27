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
    /// 三方目录对照
    /// </summary>
    [Area("SystemManage")]

    public class TTCataloguesComparisonController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ITTCataloguesComparisonDmnService _ttCataloguesComparisonDmnService;
        private readonly ITTCataloguesComparisonDetailRepo _ttCataloguesComparisonDetailRepo;
        private readonly ITTCataloguesComparisonMainRepo _ttCataloguesComparisonMainRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="ttCataloguesComparisonDmnService"></param>
        public TTCataloguesComparisonController(ITTCataloguesComparisonDmnService ttCataloguesComparisonDmnService, ITTCataloguesComparisonDetailRepo ttCataloguesComparisonDetailRepo, ITTCataloguesComparisonMainRepo ttCataloguesComparisonMainRepo)
        {
            this._ttCataloguesComparisonDmnService = ttCataloguesComparisonDmnService;
            this._ttCataloguesComparisonDetailRepo = ttCataloguesComparisonDetailRepo;
            this._ttCataloguesComparisonMainRepo = ttCataloguesComparisonMainRepo;
        }

        #region chi tiết

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult DetailIndex()
        {
            return View();
        }

        /// <summary>
        /// detail grid json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetDetailGridJson(string keyword, string mainType)
        {
            if (string.IsNullOrWhiteSpace(mainType))
            {
                return Content(new List<TTCataloguesComparisonDetailEntity>().ToJson());
            }
            var data = _ttCataloguesComparisonDetailRepo.GetListByMainId(keyword, mainType);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult DetailFrom()
        {
            return View();
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetDetailFormJson(string keyValue)
        {
            var entity = _ttCataloguesComparisonDetailRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DetailSubmitForm(TTCataloguesComparisonDetailEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _ttCataloguesComparisonDetailRepo.SubmitForm(keyValue, entity);
            return Success("Gửi thành công");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DetailDeleteForm(string keyValue)
        {
            var entity = _ttCataloguesComparisonDetailRepo.FindEntity(keyValue);
            if(entity != null)
            {
                _ttCataloguesComparisonDetailRepo.Delete(entity);
            }
            return Success("Xóa thành công。");
        }

        #endregion chi tiết

        #region Main

        /// <summary>
        /// 下拉 by Code 获取字典项
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetMainSelectJson()
        {
            var list = _ttCataloguesComparisonDmnService.GetValidMainList(this.OrganizeId);
            var data = list.Select(p => new
            {
                id = p.Id,
                text = p.Code + "-" + p.TTCode + "-" + p.TTMark,
            });
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult MainIndex()
        {
            return View();
        }

        /// <summary>
        /// main grid json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetMainGridJson(string keyword)
        {
            var data = _ttCataloguesComparisonMainRepo.GetList(this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult MainFrom()
        {
            return View();
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetMainFormJson(string keyValue)
        {
            var entity = _ttCataloguesComparisonMainRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult MainSubmitForm(TTCataloguesComparisonMainEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _ttCataloguesComparisonMainRepo.SubmitForm(keyValue, entity);
            return Success("Gửi thành công");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult MainDeleteForm(string keyValue)
        {
            var entity = _ttCataloguesComparisonMainRepo.FindEntity(keyValue);
            if (entity != null)
            {
                _ttCataloguesComparisonMainRepo.Delete(entity);
            }
            return Success("Xóa thành công。");
        }

        #endregion Main

    }
}
