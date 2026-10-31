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
    /// 
    /// </summary>
    [Area("SystemManage")]

    public class SysPatiChargeLogicController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysPatiChargeLogicApp _SysPatiChargeLogicApp;
        private readonly ISysPatiNatureApp _SysPatiNatureApp;
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;
        public SysPatiChargeLogicController(ISysPatiChargeLogicApp sysPatiChargeLogicApp, ISysPatiNatureApp sysPatiNatureApp, ISysChargeCategoryRepo sysChargeCategoryRepo)
        {
            _SysPatiChargeLogicApp = sysPatiChargeLogicApp;
            _SysPatiNatureApp = sysPatiNatureApp;
            _sysChargeCategoryRepo = sysChargeCategoryRepo;
        }


        #region bệnh nhân Thu phíThuật toán

        /// <summary>
        /// đượclấybệnhngườithuphíThuật toángridGiá trị
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ogrId"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetsfGridJson(Pagination pagination, string keyword, string ogrId)
        {
            var data = new
            {
                rows = _SysPatiChargeLogicApp.GetList(pagination, keyword, ogrId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public IActionResult SysPatiChargeLogic()
        {
            return View();
        }


        public IActionResult SubmitForm(PatiChargeLogicVO PatiChargeLogicVO, string keyValue)
        {
            _SysPatiChargeLogicApp.SubmitForm(PatiChargeLogicVO, keyValue,OrganizeId);
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(int keyValue)
        {
            _SysPatiChargeLogicApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// Khi sửa thông tin,mangchọntrongthực hiệnđốitượngmangđếnmớitrangmặt
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _SysPatiChargeLogicApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }


        /// <summary>
        /// bệnhngườitínhchấtdướikéokhung 
        /// bệnhngườithuphíThuật toánbệnhngườitínhchấtdướikéokhungđãkhônglạikhiếndùng,
        /// nàyphươngphương pháptạithuphígiảmmiễnnơicũngkhiếndùngđến,nhưkết quảgiảmmiễnsửasửarồibệnhngườitínhchấtmở rộnghiệnphươngkiểu,khônglạikhiếndùngnàyphươngphương pháp,vui lòngghiđượcxóatrừ
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetbrxzSelect()
        {
            var treeList = _SysPatiNatureApp.GetbrxzSelect("");
            return Content(treeList);
        }

        /// <summary>
        /// Hộp chọn loại lớn
        /// </summary>
        /// <returns></returns>
        public IActionResult GetdlSelect()
        {
            var data = _sysChargeCategoryRepo.GetList(this.OrganizeId, zt: "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dlCode;
                treeModel.text = item.dlmc;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// căndữ liệulớnloạiđượclấyhạngmụccode
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        public IActionResult GetSFXMItemInfoByDlCode(string keyword, string dlCode)
        {
            var data = _SysPatiChargeLogicApp.GetSFXMItemInfoByDlCode(keyword, dlCode, this.OrganizeId);
            return Content(data.ToJson());
        }

        #endregion
    }
}
