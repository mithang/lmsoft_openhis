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
    /// Nhóm thu phí lớn
    /// </summary>
    [Area("SystemManage")]

    public class SysChargeCategoryController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        public SysChargeCategoryController(ISysChargeCategoryRepo sysChargeCategoryRepo, ISysConfigRepo sysConfigRepo)
        {
            _sysChargeCategoryRepo = sysChargeCategoryRepo;
            _sysConfigRepo = sysConfigRepo;
        }


        /// <summary>
        /// đượclấythuphílớnloạicộtbảng
        /// </summary>
        /// <param name="dllbs"></param>
        /// <returns></returns>
        public IActionResult GetListJson(string dllbs)
        {
            var list = _sysChargeCategoryRepo.GetList(this.OrganizeId, dllbs, zt: "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in list)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dlCode;
                treeModel.text = item.dlmc;
                treeModel.parentId = item.ParentId == null ? null :
                    list.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }
        /// <summary>
        /// đượclấythuphílớnloạinổilớplist
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetsfdlJson(string keyword)
        {
            var iszytfyp = _sysConfigRepo.GetBoolValueByCode("YXZ_SETT_1010", OrganizeId);//độngtrạng tháithamsố：nhập việnviệnhoànphílàkhônghoànthuốcphẩmphídùng
            var sysDls = new List<SysChargeCategoryVEntity>();
            if (iszytfyp.Value == false)
            {//hiển thịhiển thịthuốcphẩm
                sysDls = _sysChargeCategoryRepo.GetLazyList(this.OrganizeId).ToList();
            }
            else {//khônghiển thịhiển thịthuốcphẩm
                sysDls = _sysChargeCategoryRepo.GetLazyList(this.OrganizeId).Where(p => p.dlCode != "01" && p.dlCode != "02" && p.dlCode != "03").ToList();
            } 
            //sysDls = sysDls.Where(p=>p.py.Contains(keyword)||p.dlmc.Contains(keyword)).ToList();
            sysDls = sysDls.Where(p => p.py.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
            || p.dlmc.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            return Content(sysDls.ToJson());
        }
        
    }
}
