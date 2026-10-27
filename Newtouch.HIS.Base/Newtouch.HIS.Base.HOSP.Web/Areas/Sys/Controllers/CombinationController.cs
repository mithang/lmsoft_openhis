using Newtouch.HIS.Domain.IDomainServices;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Areas.Sys.Controllers
{
    [Area("Sys")]
    public class CombinationController : ControllerBase
	{

		private readonly ISysMedicineDmnService _sysMedicineDmnService;


		public CombinationController(ISysOrganizeDmnService sysOrganizeDmnService, ISysMedicineDmnService sysMedicineDmnService)
		{

			this._sysMedicineDmnService = sysMedicineDmnService;
		}
		/// <summary>
		/// 药品对码视图
		/// </summary>
		/// <returns></returns>
		// GET: Sys/Combination
		public IActionResult Index()
        {
            return View();
        }

        public IActionResult CatalogComparison()
        {
            return View();
        }
        /// <summary>
        /// 耗材对码视图
        /// </summary>
        /// <returns></returns>
        public IActionResult materialIndex()
		{
			return View();
		}
		/// <summary>
		/// 项目对码视图
		/// </summary>
		/// <returns></returns>
		public IActionResult projectIndex()
		{
			return View();
		}
		
		/// <summary>
		/// 查询系统药品信息
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		[HandlerAjaxOnly]
		public IActionResult Getypxxlist(Pagination pagination, string keyword, string type)
		{
			pagination.sidx = "CreateTime desc";
			pagination.sord = "asc";
			var data = new
			{
				rows = _sysMedicineDmnService.GetPaginationListdm(this.OrganizeId, pagination, type, keyword),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}
        /// <summary>
		/// 查询系统药品信息
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		[HandlerAjaxOnly]
        public IActionResult GetMldzxxlist(Pagination pagination, string keyword, string type)
        {
            pagination.sidx = "Id desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysMedicineDmnService.GetPaginationListMldzxx(this.OrganizeId, pagination, type, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 根据系统药品信息查询医保对应药品信息 
        /// </summary>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        public IActionResult GetYpypxxlist( string ypmc, string py,string gjybdm,string pzwh,string dataSource)
		{

			var data = _sysMedicineDmnService.GetYpypxxlist(this.OrganizeId, ypmc, py, gjybdm, pzwh,dataSource);
			
			return Content(data.ToJson());
		}

		/// <summary>
		/// 药品对码操作
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <returns></returns>
		public IActionResult SaveYpYb(G_yb_ypxxVO ybxx, int? ypid)
		{
			var data = new
			{
				updatecount = _sysMedicineDmnService.SaveYpYb(ybxx, ypid, OrganizeId),
			};
			return Content(data.ToJson());
		}
		/// <summary>
		/// 系统材料信息
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public IActionResult Getclxxlist(Pagination pagination, string keyword, string type)
		{
			pagination.sidx = "CreateTime desc";
			pagination.sord = "asc";
			var data = new
			{
				rows = _sysMedicineDmnService.GetclxxList(this.OrganizeId, pagination, type, keyword),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// 医保材料信息
		/// </summary>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		public IActionResult GetYbclxxlist(string ypmc, string py, string gjybdm, string pzwh,string dataSource)
		{
			var data = _sysMedicineDmnService.GetYbclxxlist(this.OrganizeId, ypmc, py, gjybdm, pzwh, dataSource);

			return Content(data.ToJson());
		}

		/// <summary>
		/// 材料对码操作
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <returns></returns>
		public IActionResult SaveYpcl(G_yb_clxxVO ybxx, int? ypid)
		{
			var data = new
			{
				updatecount = _sysMedicineDmnService.SaveYpcl(ybxx, ypid, OrganizeId),
			};
			return Content(data.ToJson());
		}
        /// <summary>
        /// 获取项目信息集合
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="type"></param>
        /// <returns></returns>
		public IActionResult Getxmxxlist(Pagination pagination, string keyword, string type)
		{
			pagination.sidx = "CreateTime desc";
			pagination.sord = "asc";
			var data = new
			{
				rows = _sysMedicineDmnService.GetxmxxList(this.OrganizeId, pagination, type, keyword),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// 医保项目信息
		/// </summary>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		public IActionResult GetYbxmxxlist(string ypmc, string py, string gjybdm, string pzwh,string dataSource)
		{
			var data = _sysMedicineDmnService.GetYbxmxxlist(this.OrganizeId, ypmc, py, gjybdm, pzwh, dataSource);

			return Content(data.ToJson());
		}

		/// <summary>
		/// 项目对码操作
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <returns></returns>
		public IActionResult SaveYpxm(G_yb_xmxxVO ybxx, int? ypid)
		{
			var data = new
			{
				updatecount = _sysMedicineDmnService.SaveYpxm(ybxx, ypid, OrganizeId),
			};
			return Content(data.ToJson());
		}
	}
}