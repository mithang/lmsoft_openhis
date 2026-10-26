using System;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Areas.StorageManage.Controllers
{
	/// <summary>
	/// thuốc tồn kho tra cứu
	/// </summary>
	public class DrugStockSearchController : ControllerBase
	{
		private readonly IDrugStorageDmnService drugStorageDmnService;
		private readonly ISysMedicineStockInfoRepo _kcxxRepo;
        private readonly ISysMedicineStockInfoRepo _sysMedicineStockInfoRepo;




        /// <summary>
        /// thuốc tra cứu bộ phận xuất kho mặc định bộ phận hiện tại
        /// </summary>
        /// <param name="rkbm"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [Core.Attributes.HandlerAuthorizeIgnore]
		public ActionResult DrugAndStockSearch(string rkbm, string keyWord)
		{
			var result = drugStorageDmnService.GetDrugAndStock(Constants.CurrentYfbm.yfbmCode, rkbm, keyWord, OrganizeId);
			return Content(result.ToJson());
		}

		/// <summary>
		/// lấy bộ phận hiện tại sở hữu của thuốc và thông tin tồn kho   (nhập kho từ bên ngoài)
		/// </summary>
		/// <param name="rkbm"></param>
		/// <param name="keyWord"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult DrugAndStockSearch(string keyword)
		{
			var result = drugStorageDmnService.GetDrugAndStock(Constants.CurrentYfbm.yfbmCode, keyword, OrganizeId);
			return Content(result.ToJson());
		}

		/// <summary>
		/// lấy bộ phận hiện tại sở hữu của thuốc và thông tin tồn kho   (nhập kho từ bên ngoài)
		/// </summary>
		/// <param name="keyword"></param>
		/// <param name="fph"></param>
		/// <param name="gysCode"></param>
		/// <returns></returns>
		[HttpGet]
		[Core.Attributes.HandlerAuthorizeIgnore]
		public ActionResult DrugAndStockSearchByFph(string keyword, string fph, string gysCode)
		{
			var result = drugStorageDmnService.GetDrugAndStockByFph(Constants.CurrentYfbm.yfbmCode, keyword, OrganizeId, fph, gysCode);
			return Content(result.ToJson());
		}

		/// <summary>
		/// lấy thông tin tồn kho  tra cứu lượng tồn kho dùng
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="tybz">cờ ngừng sử dụng  dbo.xt_yp_bmypxx.zt  1-khả dụng 0-ngừng sử dụng</param>
		/// <param name="kczt">tồn kho cờ trống-tất cả 0-không hiệu lực tồn kho  1-còn hiệu lực tồn kho</param>
		/// <param name="show0kc">có hiển thị tồn kho bằng 0 không</param>
		/// <returns></returns>
		[HttpGet]
		[Core.Attributes.HandlerAuthorizeIgnore]
		public ActionResult DrugAndStockSearchByPage(Pagination pagination, string keyword, string tybz, string kczt, string show0kc,string kcyjcode,string iskcyjz=null)
		{
			var stockTotalList = new
			{
				rows = drugStorageDmnService.GetDrugAndStock(pagination, Constants.CurrentYfbm.yfbmCode, keyword, tybz, kczt, show0kc, OrganizeId, kcyjcode, iskcyjz),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(stockTotalList.ToJson());
		}

		/// <summary>
		/// theo mã thuốc lấy theo lô nhóm của số lượng tồn kho hiện tại đăng nhập bộ phận nhà thuốc
		/// </summary>
		/// <param name="ypdm"></param>
		/// <returns></returns>
		[Core.Attributes.HandlerAuthorizeIgnore]
		public ActionResult StockGroupByBatchSearch(string ypdm)
		{
			var result = drugStorageDmnService.GetStockGroupByBatch(ypdm, Constants.CurrentYfbm.yfbmCode, OrganizeId);
			return Content(result.ToJson());
		}

		/// <summary>
		/// theo mã thuốc lấy theo lô nhóm của số lượng tồn kho hiện tại đăng nhập bộ phận nhà thuốc
		/// </summary>
		/// <param name="ypdm"></param>
		/// <param name="fph"></param>
		/// <param name="gysCode"></param>
		/// <returns></returns>
		[HttpGet]
		[Core.Attributes.HandlerAuthorizeIgnore]
		public ActionResult StockGroupByBatchSearchByFph(string ypdm, string fph, string gysCode, string pc = null)
		{
			var result = drugStorageDmnService.GetStockGroupByBatchByFph(ypdm, Constants.CurrentYfbm.yfbmCode, OrganizeId, fph, gysCode, pc);
			return Content(result.ToJson());
		}

		/// <summary>
		/// theo mã thuốc lấy theo lô nhóm của số lượng tồn kho hiện tại đăng nhập bộ phận nhà thuốc
		/// </summary>
		/// <param name="ypdm"></param>
		/// <returns></returns>
		[HttpGet]
		[Core.Attributes.HandlerAuthorizeIgnore]
		public ActionResult StockGroupByBatchSearchByPage(Pagination pagination, string ypdm, string kczt)
		{
			var result = new
			{
				rows = drugStorageDmnService.GetStockGroupByBatch(pagination, ypdm, kczt, Constants.CurrentYfbm.yfbmCode, OrganizeId),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(result.ToJson());
		}

		#region 库存查询功能页

		/// <summary>
		/// tồn kho tra cứu khung nhìn
		/// </summary>
		/// <returns></returns>
		public ActionResult StockQuery()
		{
			ViewBag.OrganizeId = OrganizeId;
			ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;
			return View();
		}

		/// <summary>
		/// sửa trạng thái tồn kho
		/// </summary>
		/// <param name="ypdm"></param>
		/// <param name="ph"></param>
		/// <param name="pc"></param>
		/// <param name="zt"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult UpdateKcxxZt(string ypdm, string ph, string pc, string zt)
		{
			return _kcxxRepo.UpdateZt(ypdm, ph, pc, zt, Constants.CurrentYfbm.yfbmCode, OrganizeId) > 0 ? Success() : Error("修改查库存状态失败");
		}
		#endregion

		#region 过期药品查询

		/// <summary>
		/// tra cứu thuốc hết hạn khung nhìn
		/// </summary>
		/// <returns></returns>
		public ActionResult ExpiredDrugsView()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
		}

		/// <summary>
		/// tra cứu thuốc hết hạn
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="gpyf"></param>
		/// <returns></returns>
		public ActionResult ExpiredDrugsQuery(Pagination pagination, string keyword, int gpyf,string gqyjcode,string noShow0Kc=null)
		{
            int gqyjz = -SysConfigReader.Int("GET_YFGQYG");//giá trị cảnh báo hết hạn
            var stockTotalList = new
			{
				rows = drugStorageDmnService.SelectExpiredDrugs(pagination, keyword.Trim(), gpyf, Constants.CurrentYfbm.yfbmCode, OrganizeId, gqyjcode, gqyjz, noShow0Kc),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(stockTotalList.ToJson());
		}
		#endregion


		#region 查询当前单据全部明细数据

		/// <summary>
		/// tra cứu đơn chứng dữ liệu
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		public ActionResult GetCrkMxAll(string crkId)
		{
			var data = drugStorageDmnService.GetCrkMxAll(crkId,this.OrganizeId);
			return Content(data.ToJson());
		}

		/// <summary>
		/// xóa đơn chứng dữ liệu
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		public int DeleteCrkDj(string crkId)
		{
			var data = drugStorageDmnService.DeleteCrkDj(crkId);
			return data;
		}

		/// <summary>
		/// sửa đơn chứng dữ liệu
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		public int ReviseCrkDj(string crkId)
		{
			var data = drugStorageDmnService.ReviseCrkDj(crkId);
			return data;
		}

		/// <summary>
		/// tra cứu đơn chứng dữ liệu
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		public ActionResult GetCrkDjh(string crkId,int? djlx)
		{
			var data = drugStorageDmnService.GetCrkDjh(crkId, djlx);
			return Content(data.ToJson());
		}
        #endregion


        #region 药品有效期管理页面
        public ActionResult ExpiredDateManage()
        {
            return View();
        }

        public ActionResult ExpiredDateForm()
        {
            return View();
        }

        /// <summary>
        /// lấy thuốc hạn sử dụng thông tin tồn kho hiện tại đăng nhập bộ phận nhà thuốc
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        [HttpGet]
        [Core.Attributes.HandlerAuthorizeIgnore]
        public ActionResult GetStockExpiredSearchByPage(Pagination pagination, string keyword,string show0kc)
        {
            var result = new
            {
                rows = drugStorageDmnService.GetStockExpiredSearch(pagination, keyword, show0kc,Constants.CurrentYfbm.yfbmCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(result.ToJson());
        }
        
        /// <summary>
        /// cập nhật thuốc hạn sử dụng
        /// </summary>
        /// <param name="kcId"></param>
        /// <param name="yxq"></param>
        /// <returns></returns>
        public ActionResult UpdateExpired(string kcId, string yxq) {
            return _sysMedicineStockInfoRepo.UpdateExpired(kcId, yxq) > 0 ? Success() : Error("修改有效期失败");
        } 
        

        #endregion
    }
}