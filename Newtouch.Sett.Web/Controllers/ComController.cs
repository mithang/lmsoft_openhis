using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>
    /// Port of the original Framework ComController's data actions (parity audit 2026-10-27).
    /// These endpoints are called by live ported views (accounting, discharge settle,
    /// reports, site statistics) but the controller was missing from the port, so every
    /// call returned 404. Data comes from the Base DB dictionary views, exactly like the
    /// original CommonDmnService/ItemDmnService SQL.
    /// NOT ported (same stub class as the rest of the GuiAn insurance surface):
    /// XnhInterfaceRequest / XnhInterfaceSend / CqYbInterfaceRequest — the GuiAn
    /// XnhInterfaceSend method does not exist on the ported IGuiAnOutpatientXnhApp
    /// (its ported surface is Sett/CancelSett/Simulation only, all stubs), and the
    /// Chongqing page posts raw XML to a local 127.0.0.1:33333 service that does not
    /// exist in this deployment.
    /// </summary>
    public class ComController : ControllerBase
    {
        public IActionResult GetSelectItemsDetailListByItemCode(string code)
        {
            var db = SettDb.GetBaseConnection();
            var data = db.SqlQueryable<ItemsDetailRow>(
                @"select Code, Name from V_S_Sys_ItemsDetail with (nolock)
                  where zt = '1' and (TopOrganizeId = @orgId or TopOrganizeId = '*')
                  and ItemId in (select Id from V_S_Sys_Items with (nolock) where Code = @code and zt = '1')")
                .AddParameters(new[]
                {
                    new SugarParameter("@orgId", this.OrganizeId),
                    new SugarParameter("@code", code),
                })
                .ToList();
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                treeList.Add(new TreeSelectModel
                {
                    id = item.Code,
                    text = item.Name,
                    parentId = null,
                });
            }
            return Content(treeList.TreeSelectJson(null));
        }

        public IActionResult GetSysNowDate(string format)
        {
            try
            {
                return Content(DateTime.Now.ToString(format));
            }
            catch
            {
                return Content(DateTime.Now.ToString());
            }
        }

        public IActionResult GetMedicineClassificationList()
        {
            var db = SettDb.GetBaseConnection();
            var list = db.SqlQueryable<MedicineClassificationRow>(
                "select ypflId, ypflmc, ypflCode, py, zt from V_S_xt_ypfl with (nolock) where zt = '1'")
                .ToList();
            return Content(list.ToJson());
        }

        public IActionResult GetChargetItemClassificationList()
        {
            var db = SettDb.GetBaseConnection();
            var list = db.SqlQueryable<ChargeCategoryRow>(
                @"select dlId, dlCode, dlmc, OrganizeId, py, ParentId, dllb, zt
                  from V_S_xt_sfdl with (nolock)
                  where dllb = 2 and OrganizeId = @orgId and zt = '1'")
                .AddParameters(new[] { new SugarParameter("@orgId", this.OrganizeId) })
                .ToList();
            return Content(list.ToJson());
        }

        public class ItemsDetailRow
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class MedicineClassificationRow
        {
            public int? ypflId { get; set; }
            public string ypflmc { get; set; }
            public string ypflCode { get; set; }
            public string py { get; set; }
            public string zt { get; set; }
        }

        public class ChargeCategoryRow
        {
            public int? dlId { get; set; }
            public string dlCode { get; set; }
            public string dlmc { get; set; }
            public string OrganizeId { get; set; }
            public string py { get; set; }
            public int? ParentId { get; set; }
            public int? dllb { get; set; }
            public string zt { get; set; }
        }
    }
}
