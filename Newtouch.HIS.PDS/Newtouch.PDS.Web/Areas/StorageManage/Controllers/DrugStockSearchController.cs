using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Areas.StorageManage.Controllers
{
	/// <summary>
	/// Truy vấn tồn kho thuốc
	/// </summary>
	[Area("StorageManage")]
	public class DrugStockSearchController : ControllerBase
	{
		private readonly IDrugStorageDmnService drugStorageDmnService;
		private readonly ISysMedicineStockInfoRepo _kcxxRepo;
        private readonly ISysMedicineStockInfoRepo _sysMedicineStockInfoRepo;


        /// <summary>
        /// Truy vấn thuốc Bộ phận xuất kho mặc định là bộ phận hiện tại
        /// </summary>
        /// <param name="rkbm"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [Core.Attributes.HandlerAuthorizeIgnore]
		public IActionResult DrugAndStockSearch(string rkbm, string keyWord)
		{
			var result = drugStorageDmnService.GetDrugAndStock(Constants.CurrentYfbm.yfbmCode, rkbm, keyWord, OrganizeId);
			return Content(result.ToJson());
		}

		/// <summary>
		/// Lấy thông tin thuốc và tồn kho của bộ phận hiện tại  （Nhập kho từ ngoài）
		/// </summary>
		/// <param name="rkbm"></param>
		/// <param name="keyWord"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult DrugAndStockSearch(string keyword)
		{
			var result = drugStorageDmnService.GetDrugAndStock(Constants.CurrentYfbm.yfbmCode, keyword, OrganizeId);
			return Content(result.ToJson());
		}

		/// <summary>
		/// Lấy thông tin thuốc và tồn kho của bộ phận hiện tại  （Nhập kho từ ngoài）
		/// </summary>
		/// <param name="keyword"></param>
		/// <param name="fph"></param>
		/// <param name="gysCode"></param>
		/// <returns></returns>
		[HttpGet]
		[Core.Attributes.HandlerAuthorizeIgnore]
		public IActionResult DrugAndStockSearchByFph(string keyword, string fph, string gysCode)
		{
			var result = drugStorageDmnService.GetDrugAndStockByFph(Constants.CurrentYfbm.yfbmCode, keyword, OrganizeId, fph, gysCode);
			return Content(result.ToJson());
		}

		/// <summary>
		/// Lấy thông tin tồn kho  Dùng để truy vấn lượng tồn kho
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="tybz">Cờ ngừng dùng  dbo.xt_yp_bmypxx.zt  1-Khả dụng 0-Ngừng dùng</param>
		/// <param name="kczt">Cờ tồn kho trống-Tất cả 0-Tồn kho không hiệu lực  1-Tồn kho hiệu lực</param>
		/// <param name="show0kc">Có hiển thị tồn kho bằng 0 không</param>
		/// <returns></returns>
		[HttpGet]
		[Core.Attributes.HandlerAuthorizeIgnore]
		public IActionResult DrugAndStockSearchByPage(Pagination pagination, string keyword, string tybz, string kczt, string show0kc,string kcyjcode,string iskcyjz=null)
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
		/// Lấy số lượng tồn kho nhóm theo lô theo mã thuốc Bộ phận nhà thuốc đang đăng nhập
		/// </summary>
		/// <param name="ypdm"></param>
		/// <returns></returns>
		[Core.Attributes.HandlerAuthorizeIgnore]
		public IActionResult StockGroupByBatchSearch(string ypdm)
		{
			var result = drugStorageDmnService.GetStockGroupByBatch(ypdm, Constants.CurrentYfbm.yfbmCode, OrganizeId);
			return Content(result.ToJson());
		}

		/// <summary>
		/// Lấy số lượng tồn kho nhóm theo lô theo mã thuốc Bộ phận nhà thuốc đang đăng nhập
		/// </summary>
		/// <param name="ypdm"></param>
		/// <param name="fph"></param>
		/// <param name="gysCode"></param>
		/// <returns></returns>
		[HttpGet]
		[Core.Attributes.HandlerAuthorizeIgnore]
		public IActionResult StockGroupByBatchSearchByFph(string ypdm, string fph, string gysCode, string pc = null)
		{
			var result = drugStorageDmnService.GetStockGroupByBatchByFph(ypdm, Constants.CurrentYfbm.yfbmCode, OrganizeId, fph, gysCode, pc);
			return Content(result.ToJson());
		}

		/// <summary>
		/// Lấy số lượng tồn kho nhóm theo lô theo mã thuốc Bộ phận nhà thuốc đang đăng nhập
		/// </summary>
		/// <param name="ypdm"></param>
		/// <returns></returns>
		[HttpGet]
		[Core.Attributes.HandlerAuthorizeIgnore]
		public IActionResult StockGroupByBatchSearchByPage(Pagination pagination, string ypdm, string kczt)
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

		#region Trang chức năng truy vấn tồn kho

		/// <summary>
		/// Truy vấn tồn kho Khung nhìn
		/// </summary>
		/// <returns></returns>
		public IActionResult StockQuery()
		{
			ViewBag.OrganizeId = OrganizeId;
			ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;
			return View();
		}

		/// <summary>
		/// Sửa trạng thái tồn kho
		/// </summary>
		/// <param name="ypdm"></param>
		/// <param name="ph"></param>
		/// <param name="pc"></param>
		/// <param name="zt"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult UpdateKcxxZt(string ypdm, string ph, string pc, string zt)
		{
			return _kcxxRepo.UpdateZt(ypdm, ph, pc, zt, Constants.CurrentYfbm.yfbmCode, OrganizeId) > 0 ? Success() : Error("Sửa trạng thái kiểm tra tồn kho thất bại");
		}
		#endregion

		#region Tra cứu thuốc hết hạn

		/// <summary>
		/// Truy vấn thuốc hết hạn Khung nhìn
		/// </summary>
		/// <returns></returns>
		public IActionResult ExpiredDrugsView()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
		}

		/// <summary>
		/// Truy vấn thuốc hết hạn
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="gpyf"></param>
		/// <returns></returns>
		public IActionResult ExpiredDrugsQuery(Pagination pagination, string keyword, int gpyf,string gqyjcode,string noShow0Kc=null)
		{
            int gqyjz = -SysConfigReader.Int("GET_YFGQYG");//Giá trị cảnh báo hết hạn
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


		#region Truy vấn tất cả dữ liệu chi tiết chứng từ hiện tại

		/// <summary>
		/// Truy vấn dữ liệu chứng từ
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		public IActionResult GetCrkMxAll(string crkId)
		{
			var data = drugStorageDmnService.GetCrkMxAll(crkId,this.OrganizeId);
			return Content(data.ToJson());
		}

		/// <summary>
		/// Xóa dữ liệu chứng từ
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		public int DeleteCrkDj(string crkId)
		{
			var data = drugStorageDmnService.DeleteCrkDj(crkId);
			return data;
		}

		/// <summary>
		/// Sửa dữ liệu chứng từ
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		public int ReviseCrkDj(string crkId)
		{
			var data = drugStorageDmnService.ReviseCrkDj(crkId);
			return data;
		}

		/// <summary>
		/// Truy vấn dữ liệu chứng từ
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		public IActionResult GetCrkDjh(string crkId,int? djlx)
		{
			var data = drugStorageDmnService.GetCrkDjh(crkId, djlx);
			return Content(data.ToJson());
		}
        #endregion


        #region Trang quản lý hạn dùng thuốc
        public IActionResult ExpiredDateManage()
        {
            return View();
        }

        public IActionResult ExpiredDateForm()
        {
            return View();
        }

        /// <summary>
        /// Lấy thông tin tồn kho hạn dùng thuốc Bộ phận nhà thuốc đang đăng nhập
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        [HttpGet]
        [Core.Attributes.HandlerAuthorizeIgnore]
        public IActionResult GetStockExpiredSearchByPage(Pagination pagination, string keyword,string show0kc)
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
        /// Cập nhật hạn dùng thuốc
        /// </summary>
        /// <param name="kcId"></param>
        /// <param name="yxq"></param>
        /// <returns></returns>
        public IActionResult UpdateExpired(string kcId, string yxq) {
            return _sysMedicineStockInfoRepo.UpdateExpired(kcId, yxq) > 0 ? Success() : Error("Sửa hạn dùng thất bại");
        } 
        

        #endregion
    }
}