using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure.EF;
using Newtouch.Core.Common.Utils;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.Tools.Excel;

namespace Newtouch.HIS.Web.Controllers
{
	/// <summary>
	/// xt_yp_ls_syxx ThuốcController
	/// </summary>
	public class MedicineController : ControllerBase
	{
		private readonly IMedicineInfoDmnService _medicineInfoDmnService;
		private readonly IHandOutMedicineDmnService _handOutMedicineDmnService;
		private readonly IMedicineApp _medicineApp;
		private readonly IMedicineDmnService _medicineDmnService;
		private readonly IPharmacyDrugStorageApp _pharmacyDrugStorageApp;
		private readonly IfyDmnService _fyDmnService;
		private readonly ISysStaffRepo _sysStaffRepo;
		private readonly ISysUserDmnService sysuserDmnService;
		private readonly ISysMedicineSupplierRepo isyMedicineSupplierRepo;
		private readonly IDrugStorageApp _drugStorageApp;

		/// <summary>
		/// Thuốc hệ thống
		/// </summary>
		/// <returns></returns>
		public IActionResult SysMedicine()
		{
			return View();
		}

		public IActionResult MedicineInfo()
		{
			return View();
		}

		public IActionResult ExpiredDrugs()
		{
			return View();
		}

		/// <summary>
		/// Thông tin thuốc của bộ phận này
		/// </summary>
		/// <returns></returns>
		public IActionResult GetypGridJson(Pagination pagination, MedicineInfoParam model, string keyword, string organizeId)
		{
			ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
			model.ypzt = model.ypzt ?? "";
			model.syzt = model.syzt ?? "";
			var data = new
			{
				rows = _medicineInfoDmnService.GetMedicineInfoListV2(pagination, model, this.OrganizeId),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// Đồng bộ thuốc bộ phận
		/// </summary>
		/// <returns></returns>
		public IActionResult SyncDeptDrug()
		{
			return View();
		}

		/// <summary>
		/// Truy vấn lãi lỗ
		/// </summary>
		/// <returns></returns>
		public IActionResult PriceAdjustmentProfitAndLossQuery()
		{
			return View();
		}

		/// <summary>
		///Đơn xin trả hàng
		/// </summary>
		/// <returns></returns>
		public IActionResult RequestOfReturn()
		{
			return View();
		}

		/// <summary>
		/// Đồng bộ
		/// </summary>
		/// <param name="yp"></param>
		/// <param name="opereateType">0:Thêm  1：Xóa</param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult FreshList(string[] yp, int opereateType)
		{
			var result = _medicineInfoDmnService.FreshList(yp, opereateType, OrganizeId, Constants.CurrentYfbm.yfbmCode);
			return string.IsNullOrWhiteSpace(result) ? Success("Đồng bộ thành công") : Error(result);
		}

		/// <summary>
		/// Lấy thuốc ứng viên đồng bộ
		/// </summary>
		/// <returns></returns>
		public IActionResult GettbypGridJson(Pagination pagination, MedicineInfoParam model)
		{
			var data = new
			{
				rows = _medicineInfoDmnService.GetTbMedicineInfoList(pagination, model, OrganizeId),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// Kiểm soát,Hủy kiểm soát
		/// </summary>
		/// <param name="ypCode"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public IActionResult ControlMedicine(string ypCode, string type)
		{
			_medicineInfoDmnService.ControlMedicine(ypCode, type, OrganizeId, Constants.CurrentYfbm.yfbmCode);
			return Success("Thao tác thành công");
		}

		/// <summary>
		/// Bộ phận trả thuốc
		/// </summary>
		/// <param name="xtYpLsNbfymxk"></param>
		/// <param name="tybm"></param>
		/// <param name="tydh"></param>
		/// <param name="fyfs"></param>
		/// <returns></returns>
		public IActionResult ExecHandOutMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string tybm, string tydh, string fyfs)
		{
			var data = _handOutMedicineDmnService.RequestOfReturnMedicine(xtYpLsNbfymxk, tybm, tydh, fyfs, 5);
			return string.IsNullOrWhiteSpace(data) ? Success() : Error(data);
		}

		/// <summary>
		/// Nhà cung cấp thuốc hệ thống Nguồn dữ liệu
		/// </summary>
		/// <returns></returns>
		public IActionResult MedicineSupplierList(string keyword)
		{
			var result = isyMedicineSupplierRepo.GetGysList(keyword, OrganizeId);
			return Content(result.ToJson());
		}

		/// <summary>
		/// Lấy thuộc tính đặc biệt của thuốc
		/// </summary>
		/// <returns></returns>
		public IActionResult GetMedicineTSSXList()
		{
			var list = _pharmacyDrugStorageApp.GetDrugSpecialPropertiesList();
			return Content(list.ToJson());
		}

		/// <summary>
		/// Lấy bộ phận nhận hàng
		/// </summary>
		/// <returns></returns>
		public IActionResult GetMeidicineSHBMList()
		{
			var list = _fyDmnService.GetMeidicineSHBMList();
			return Content(list.ToJson());
		}

		#region Báo hỏng báo thừa

		/// <summary>
		/// Báo hỏng báo thừa
		/// </summary>
		/// <returns></returns>
		public IActionResult ReportLossAndProfit()
		{
			return View();
		}

		/// <summary>
		/// Lấy số lãi lỗ mới
		/// </summary>
		/// <returns></returns>
		public IActionResult initialSYDH()
		{
			var sydh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("报损报溢单", Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
			return Content(sydh);
		}

		/// <summary>
		/// Tải lý do lãi lỗ theo loại
		/// </summary>
		/// <param name="sylx"></param>
		/// <returns></returns>
		public IActionResult GetLossProfitReasonListByType(string sylx)
		{
			var list = _medicineApp.GetLossProfitReasonListByType(sylx);
			var obj = list.Select(a => new { syyyId = a.syyyId, syyy = a.syyy }).ToJson();
			return Content(obj);
		}

		/// <summary>
		/// Lấy người chịu trách nhiệmlist
		/// </summary>
		/// <param name="inputCode"></param>
		/// <returns></returns>
		public IActionResult GetZRRList(string inputCode)
		{
			var list = _medicineInfoDmnService.GetStaffListByOrg(OrganizeId);
			return Content(list.ToJson());
		}

		/// <summary>
		/// Truy vấn thuốc lãi lỗlist
		/// </summary>
		/// <param name="inputCode"></param>
		/// <returns></returns>
		public IActionResult SelectLossAndProfitMedicineList(string inputCode)
		{
			return Content(_medicineApp.SelectLossAndProfitMedicineList(inputCode).ToJson());
		}

		/// <summary>
		/// Gửi báo hỏng báo thừa
		/// </summary>
		/// <param name="syxx"></param>
		/// <returns></returns>
		public IActionResult SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
		{
			if (syxx == null || syxx.Length <= 0) return Error("Vui lòng truyền chi tiết lãi lỗ");
			var organizeId = OrganizeId;
			var yfbmCode = Constants.CurrentYfbm.yfbmCode;
			var userCode = OperatorProvider.GetCurrent().UserCode;
			var syIds = "";
			Parallel.ForEach(syxx, item =>
			{
				item.syId = Guid.NewGuid().ToString();
				item.OrganizeId = organizeId;
				item.yfbmCode = yfbmCode;
				item.CreatorCode = userCode;
				item.CreateTime = DateTime.Now;
				item.zt = "1";
				item.Bgsj = DateTime.Now;
				syIds += item.syId + ",";
			});
			var result = _drugStorageApp.SubmitReportLossAndProfit(syxx);
			return string.IsNullOrWhiteSpace(result) ? Success(syIds) : Error(result);
		}

		/// <summary>
		/// Báo hỏng báo thừa （Lưu）
		/// </summary>
		/// <param name="syxx"></param>
		/// <returns></returns>
		public IActionResult SaveReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
		{
			var syxxList = syxx != null && syxx.Length > 0 ? syxx.ToList() : new List<SysMedicineProfitLossEntity>();
			var profitLossEntityList = new List<YpSyxxVo>();
			if (syxxList.Count > 0)
			{
				syxxList.ForEach(p =>
				{
					profitLossEntityList.Add(new YpSyxxVo
					{
						Bgsj = DateTime.Now,
						Djh = p.Djh,
						Jj = p.Jj,
						Lsj = p.Lsj,
						OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
						pc = p.pc,
						Pfj = p.Pfj,
						Ph = p.Ph,
						remark = p.remark,
						Sykc = p.Sykc,
						Sysl = p.Sysl,
						Syyy = p.Syyy,
						yfbmCode = Constants.CurrentYfbm.yfbmCode,
						Yklsj = p.Yklsj,
						Ykpfj = p.Ykpfj,
						Ypdm = p.Ypdm,
						Yxq = p.Yxq,
						Zhyz = p.Zhyz,
						Zrr = p.Zrr
					});
				});
			}
			else
			{
				throw new FailedException("Thông tin lãi lỗ không được để trống");
			}
			var result = _medicineDmnService.SaveReportLossAndProfit(profitLossEntityList);
			return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
		}

		#endregion

		#region Truy vấn báo hỏng báo thừa
		/// <summary>
		/// Truy vấn báo hỏng báo thừa
		/// </summary>
		/// <returns></returns>
		public IActionResult ReportLossAndProfitQuery()
		{
			return View();
		}

		/// <summary>
		/// Truy vấn báo hỏng báo thừa
		/// </summary>
		/// <returns></returns>
		public IActionResult SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string syyy, string inputCode, int syqk)
		{
			var list = new
			{
				rows = _medicineApp.SelectLossAndProditInfoList(pagination, startTime, endTime, inputCode, syyy, syqk),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(list.ToJson());
		}
		/// <summary>
		/// Truy vấn chi tiết kiểm kê
		/// </summary>
		/// <returns></returns>
		public IActionResult InventoryQureyInfoList(Pagination pagination, string startTime, string endTime, string syyy, string inputCode, int syqk)
		{
			var list = new
			{
				rows = _medicineApp.InventoryQureyInfo(pagination, startTime, endTime, inputCode, syyy, syqk),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(list.ToJson());
		}
		/// <summary>
		/// Tổng số tiền giá bán buôn、Truy vấn tổng số tiền giá bán lẻ
		/// </summary>
		/// <param name="startTime">Thời gian bắt đầu</param>
		/// <param name="endTime">Thời gian kết thúc</param>
		/// <param name="syyy">Lý do lãi lỗ</param>
		/// <param name="inputCode">Từ khóa</param>
		/// <param name="syqk">Tình hình lãi lỗ</param>
		/// <returns></returns>
		public IActionResult ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk)
		{
			return Content(_medicineApp.ComputePjzeAndLjze(startTime, endTime, syyy, inputCode, syqk).ToJson());
		}


		#endregion
		/// <summary>
		/// Truy vấn thừa thiếu kiểm kê
		/// </summary>
		/// <returns></returns>
		public IActionResult InventoryQureyReport()
		{
			return View();
		}
		#region Truy vấn lãi lỗ điều chỉnh giá

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="Srm"></param>
		/// <param name="yfbmCode"></param>
		/// <param name="Lkc"></param>
		/// <returns></returns>
		public IActionResult SelectPriceAdjustmentProfitLossList(Pagination pagination, DateTime? startTime, DateTime? endTime, string Srm, string yfbmCode, string Lkc)
		{
			var boolLkc = Lkc == "1";
			var allUseableYfbmCodes = string.Join(",", OperatorProvider.GetCurrent().yfbmCodeList.ToArray());
			//const string allUseableYfbmCodes = "'yjk','mzyf','zyyf'";
			var list = new
			{
				rows = _medicineDmnService.SelectPriceAdjustmentProfitLossList(pagination, startTime, endTime, Srm, yfbmCode, boolLkc, allUseableYfbmCodes),
				tatal = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(list.ToJson());
		}
		#endregion
		public IActionResult ExcelGet(bool? isContainFilter, int colStanWidth, string cols, DateTime? startTime, DateTime? endTime, string Srm, string yfbmCode, string Lkc, string yfykmc)
		{
			var orgId = this.OrganizeId;
			if (string.IsNullOrEmpty(orgId))
			{
				throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
			}
			if (string.IsNullOrWhiteSpace(cols))
			{
				cols = Request.Cookies["ExportExcelCols"];
				if (!string.IsNullOrWhiteSpace(cols))
				{
					cols = System.Net.WebUtility.UrlDecode(cols);
					Response.Cookies.Delete("ExportExcelCols");
				}
			}
			if (string.IsNullOrWhiteSpace(cols))
			{
				throw new FailedException("Chưa chỉ định cột xuất");
			}
			var pagination = new Pagination();
			pagination.sidx = "Tjsj desc"; //Thời gian tăng dần
			pagination.rows = 65536 - 1;    //ExcelSố dòng tối đa
			pagination.page = 1;    //Trang đầu tiên truy vấn tất cả
			var boolLkc = Lkc == "1";
			var allUseableYfbmCodes = string.Join(",", OperatorProvider.GetCurrent().yfbmCodeList.ToArray());
			var list = _medicineDmnService.SelectPriceAdjustmentProfitLossList(pagination, startTime, endTime, Srm, yfbmCode, boolLkc, allUseableYfbmCodes);
			var colList = cols.ToObject<IList<ExcelColumn>>();
			var sheet = new ExcelSheet()
			{
				Title = "Truy vấn lãi lỗ điều chỉnh giá",
				columns = colList,
			};
			sheet.columns.Where(p => p.Name == "mzzybz").ToList().ForEach(t => t.FuncGetCellValue = (obj) =>
			{
				return obj.ToString() == "0" ? "Dùng chung" : obj.ToString() == "1" ? "Ngoại trú" : "Nội trú";
			});
			sheet.columns.Where(p => p.Name == "zt").ToList().ForEach(t => t.FuncGetCellValue = (obj) =>
			 {
				 return obj.ToString() == "0" ? "Không hiệu lực" : "Hiệu lực";
			 });
			//sheet.columns.Where(p => p.Name == "tjsyze").ToList().ForEach(t =>
			//{
			//    t.NumberDigits = 2;
			//});
			sheet.columns.ToList().ForEach(p =>
			{
				p.WidthTimes = (double)p.Width / colStanWidth;
				p.Width = 0;    //Widthđều đặt thành0
			});

			var path = DateTime.Now.ToString("\\\\yyyyMMdd\\\\HHmmssfff") + "Truy vấn lãi lỗ điều chỉnh giá" + ".xls";

			var filePath = CommmHelper.GetLocalFilePath("\\ExcelXuất\\Xuất truy vấn lãi lỗ điều chỉnh giá" + path, "D:\\");

			if (isContainFilter == true)
			{
				//Điều kiện lọc
				var filterDict = new Dictionary<string, string>();
				if (startTime.HasValue)
				{
					filterDict.Add("Ngày bắt đầu", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
				}
				if (endTime.HasValue)
				{
					filterDict.Add("Ngày kết thúc", endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
				}
				if (!string.IsNullOrWhiteSpace(Srm))
				{
					filterDict.Add("Từ khóa", Srm);
				}
				if (!string.IsNullOrWhiteSpace(yfykmc))
				{
					filterDict.Add("Nhà thuốc kho thuốc", yfykmc);
				}
				if (!string.IsNullOrWhiteSpace(Lkc))
				{
					filterDict.Add("Hiển thị bản ghi tồn kho bằng 0", Lkc == "0" ? "Hiển thị" : "Không hiển thị");
				}
				if (filterDict.Count > 0)
				{
					sheet.filters = filterDict;
				}
			}

			var rest = list.ToExcel(filePath, sheet);

			if (rest)
			{
				return File(filePath, "application/x-xls", path.Replace("\\", ""));
			}
			else
			{
				return Content("Xuất tệp thất bại，Vui lòng quay lại trang danh sách thử lại");
			}
		}


		#region Lấy thuốc hết hạn
		public IActionResult GetExpiredDrugsGridJson(Pagination pagination, MedicineInfoParam model)
		{
			var data = new
			{
				rows = _medicineInfoDmnService.GetExpiredDrugsData(pagination, model, this.OrganizeId),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}
		#endregion
	}
}