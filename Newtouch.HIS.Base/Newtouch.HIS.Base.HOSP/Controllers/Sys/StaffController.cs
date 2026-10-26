using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System.Text;
using Newtouch.Tools.Excel;
using System.Text.RegularExpressions;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.HIS.Domain.Entity.SystemManage;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
	public class StaffController : ControllerBase
	{
		//private readonly ISysUserApp _sysUserApp;
		//private readonly ISysUserRepo _sysUserRepo;
		//private readonly ISysUserRoleRepository _sysUserRoleRepository;
		//private readonly ISysUserDmnService _sysUserDmnService;
		private readonly ISysStaffRepo _sysStaffRepo;
		private readonly ISysWardRepo _sysWardRepo;
		private readonly ISysStaffWardRepo _sysStaffWardRepo;
		private readonly ISysStaffDmnService _sysStaffDmnService;
		private readonly ISysOrganizeRepository _sysOrganizeRepository;
		private readonly ISysDepartmentRepository _sysDepartmentRepository;
		private readonly ISysStaffSignatureRepo _sysStaffSignatureRepo;
        private readonly ISysConsultRepo _sysConsultRepo;
        private readonly ISysStaffConsultRepo _sysStaffConsultRepo;

        public StaffController(ISysStaffRepo sysStaffRepo, ISysStaffDmnService sysStaffDmnService
			, ISysOrganizeRepository sysOrganizeRepositor
			, ISysDepartmentRepository sysDepartmentRepository
			, ISysWardRepo sysWardRepo, ISysStaffWardRepo sysStaffWardRepo, ISysStaffSignatureRepo sysStaffSignatureRepo
            , ISysConsultRepo sysConsultRepo
            , ISysStaffConsultRepo sysStaffConsultRepo
            )
		{
			this._sysStaffRepo = sysStaffRepo;
			this._sysWardRepo = sysWardRepo;
			this._sysStaffDmnService = sysStaffDmnService;
			this._sysOrganizeRepository = sysOrganizeRepositor;
			this._sysDepartmentRepository = sysDepartmentRepository;
			this._sysStaffWardRepo = sysStaffWardRepo;
			this._sysStaffSignatureRepo = sysStaffSignatureRepo;
            this._sysConsultRepo = sysConsultRepo;
            this._sysStaffConsultRepo = sysStaffConsultRepo;

        }

		//grid json Nguồn dữ liệu
		[HttpGet]
		[HandlerAjaxOnly]
		public ActionResult GetGridJson(Pagination pagination, string keyword, string OrganizeId)
		{
			pagination.sidx = "DepartmentCode asc,CreateTime desc";
			pagination.sord = "asc";
			if (string.IsNullOrWhiteSpace(OrganizeId))
			{
				throw new FailedException("定位当前权限内的组织机构失败");
			}
			var data = new
			{
				rows = _sysStaffDmnService.GetPaginationList(pagination, OrganizeId, keyword),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// Xuất nhân viên
		/// </summary>
		/// <param name="key"></param>
		/// <param name="kpzt"></param>
		/// <param name="createTimestart"></param>
		/// <param name="createTimeEnd"></param>
		/// <param name="cols"></param>
		/// <param name="colStanWidth"></param>
		/// <param name="isContainFilter"></param>
		/// <returns></returns>
		public ActionResult OutpatientExport(string keyword, string cols, int colStanWidth, bool? isContainFilter)
		{
			var orgId = this.OrganizeId;
			if (string.IsNullOrEmpty(orgId))
			{
				throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
			}
			if (string.IsNullOrWhiteSpace(cols))
			{
				cols = WebHelper.GetCookie("ExportExcelCols");
				if (!string.IsNullOrWhiteSpace(cols))
				{
					cols = System.Web.HttpUtility.UrlDecode(cols);
					WebHelper.RemoveCookie("ExportExcelCols");
				}
			}
			if (string.IsNullOrWhiteSpace(cols))
			{
				throw new FailedException("未指定导出列");
			}

			var pagination = new Pagination();
			pagination.sidx = "CreateTime desc"; //Thời gian tăng dần
			pagination.rows = 65536 - 1;    //ExcelSố dòng tối đa
			pagination.page = 1;    //Trang đầu tiên tra cứu tất cả

			var list = _sysStaffDmnService.GetPaginationList(pagination, OrganizeId, keyword);

			var data = list.Select(r => new StaffExport
			{
				gh = r.gh,
				Name = r.Name,
				Gender = r.Gender == true ? "男" : "女" ?? "",
				zc = r.zc,
				kflb = r.kflb == "RTM_OT" ? "作业治疗" : r.kflb == "RTM_PT" ? "物理治疗" : r.kflb == "RTM_ST" ? "语言治疗" : "",
				MobilePhone = r.MobilePhone,
				gjybdm = r.gjybdm,
				staffNames = r.staffNames,
				OrganizeName = r.OrganizeName,
				DepartmentName = r.DepartmentName,
				Description = r.Description,
				zt = r.zt == "1" ? "有效" : "无效",
				CreatorCode = r.CreatorCode,
				sj1 = r.CreateTime,
				LastModifierCode = r.LastModifierCode,
				sj2 = r.LastModifyTime
			}).ToList();

			var colList = cols.ToObject<IList<ExcelColumn>>();
			var sheet = new ExcelSheet()
			{
				Title = "工作人员",
				columns = colList,
			};
			sheet.columns.ToList().ForEach(p =>
				{
					p.WidthTimes = (double)p.Width / colStanWidth;
					p.Width = 0;    //WidthĐều đặt thành0
				});

			var path = DateTime.Now.ToString("\\\\yyyyMMdd\\\\HHmmssfff") + "工作人员.xls";

			var filePath = CommmHelper.GetLocalFilePath("\\Excel导出\\工作人员导出" + path);

			if (isContainFilter == true)
			{
				//Điều kiện lọc
				var filterDict = new Dictionary<string, string>();
				if (!string.IsNullOrWhiteSpace(keyword))
				{
					filterDict.Add("关键字", keyword);
				}
				if (filterDict.Count > 0)
				{
					sheet.filters = filterDict;
				}
			}

			var rest = data.ToExcel(filePath, sheet);

			if (rest)
			{
				return File(filePath, "application/x-xls", path.Replace("\\", ""));
			}
			else
			{
				return Content("文件导出失败，请返回列表页重试");
			}
		}



		[HttpGet]
		[HandlerAjaxOnly]
		public ActionResult GetFormJson(string keyValue)
		{
			var data = _sysStaffRepo.FindEntity(keyValue);
			return Content(data.ToJson());
		}
		[HttpGet]
		[HandlerAjaxOnly]
		public ActionResult GetFormPicture(string keyValue, string orgId)
		{
			var data = _sysStaffSignatureRepo.IQueryable().Where(p => p.StaffId == keyValue && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
			if (data != null)
			{
				var result = data.ImagePrefix + data.ImageData;
				return Content(result);
			}
			return Content(null);
		}

		[HttpPost]
		[HandlerAjaxOnly]
		//[ValidateAntiForgeryToken]
		public ActionResult SubmitForm(SysStaffEntity entity, string keyValue, bool asLoginUser, string dutyList)
		{
			var staffId = "";
			if (string.IsNullOrWhiteSpace(entity.Name) || string.IsNullOrWhiteSpace(entity.OrganizeId))
			{
				throw new FailedException("数据不完整，请重新填写提交");
			}
			entity.zt = entity.zt == "true" ? "1" : "0";
			entity.TopOrganizeId = Constants.TopOrganizeId;
			_sysStaffDmnService.SubmitForm(entity, keyValue, asLoginUser, out staffId);
			_sysStaffDmnService.UpdateStaffDuty(entity.Id, (dutyList ?? "").Split(','));
			return Success("操作成功。", staffId);
		}
		[HttpPost]
		[HandlerAjaxOnly]
		public ActionResult FileUpLoad(string staffId, string orgId)
		{
			HttpPostedFileBase pUpImage = Request.Files["file"];
			//Xác định đã tải lên hình ảnh chưa
			if (pUpImage != null)
			{
				var maxbyte = 10 * 1024;
				int sFileLength = pUpImage.ContentLength;
				if (sFileLength > maxbyte)
				{
					throw new FailedException("上传图片不能超过10kb");
				}
				//Lấy tên tệp
				string sFilename = System.IO.Path.GetFileName(pUpImage.FileName).ToLower();
				//LấyupImagePhần mở rộng của tệp
				string extendName = System.IO.Path.GetExtension(sFilename);
				//Xác định có phải định dạng hình ảnh không 
				if (extendName != ".jpg" && extendName != ".png")
				{
					throw new FailedException("上传图片格式只能是.jpg/.png格式");
				}
				byte[] image = new Byte[sFileLength];
				pUpImage.InputStream.Read(image, 0, sFileLength);

				var savepath = "/PictureUploadFile/";
				var dirpath = Server.MapPath(savepath);
				if (!Directory.Exists(dirpath))
				{
					Directory.CreateDirectory(dirpath);
				}
				var picNewName = DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetFileName(pUpImage.FileName);
				var picUrl = dirpath + picNewName;
				pUpImage.SaveAs(picUrl);

				SysStaffSignatureEntity entity = new SysStaffSignatureEntity();
				entity.StaffId = staffId;
				entity.OrganizeId = orgId;
				entity.ImageName = sFilename;
				entity.ImageTpye = extendName;
				entity.ImageUrl = picUrl;
				entity.ImagePrefix = "data:" + pUpImage.ContentType + ";base64,";
				entity.ImageData = Convert.ToBase64String(image);
				_sysStaffSignatureRepo.SubmitFor(entity);

			}
			return Success();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		//[HandlerAuthorize]
		public virtual ActionResult Duties()
		{
			return View();
		}

		public virtual ActionResult WardInfo()
		{
			return View();
		}
        public virtual ActionResult ConsultInfo()
        {
            return View();
        }
        /// <summary>
        /// Xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelExportChooseColumns()
		{
			return View();
		}

		[HttpPost]
		[HandlerAjaxOnly]
		//[ValidateAntiForgeryToken]
		public ActionResult UpdateStaffDuty(string keyValue, string dutyList)
		{
			_sysStaffDmnService.UpdateStaffDuty(keyValue, (dutyList ?? "").Split(','));
			return Success("操作成功。");
		}

		/// <summary>
		/// Người dùng liên quan
		/// </summary>
		/// <returns></returns>
		public ActionResult Selector()
		{
			return View();
		}

		/// <summary>
		/// Lấy cây nhân sự cơ quan Ba cấp（Cơ quan+Khoa+Nhân sự）
		/// </summary>
		/// <param name="keyValue"></param>
		/// <returns></returns>
		public ActionResult GetStaffSelecotrTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, string initIdSelected = null)
		{
			if (string.IsNullOrWhiteSpace(organizeId))
			{
				organizeId = base.GetAuthOrganizeId();  //Mặc định QuyềnOrganizeId
			}
			//Bộ nhớ đệmstart
			var organizedata = _sysOrganizeRepository.GetValidListByParentOrg(organizeId);
			var treeList = new List<TreeViewModel>();
			foreach (SysOrganizeEntity orgItem in organizedata)
			{
				//Tập hợp khoa của cơ quan
				var orgDetpData = _sysDepartmentRepository.GetListByOrg(orgItem.Id);
				//Tập hợp nhân sự của cơ quan
				var orgStaffData = _sysStaffRepo.GetsatffListByOrg(orgItem.Id);
				#region 机构下所有科室
				foreach (SysDepartmentEntity deptItem in orgDetpData)
				{
					var deptStaffData = orgStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
					#region 机构下所有用户
					foreach (SysStaffEntity staffItem in deptStaffData)
					{
						TreeViewModel staffTree = new TreeViewModel();
						staffTree.id = staffItem.Id;
						staffTree.text = staffItem.Name;
						staffTree.value = staffItem.gh;
						staffTree.parentId = deptItem.Id;
						staffTree.hasChildren = false;
						staffTree.showcheck = true;
						staffTree.checkstate = 0;   //Gán từ bên ngoài，Thuận tiện cho bộ nhớ đệm
						treeList.Add(staffTree);
					}
					#endregion
					TreeViewModel deptTree = new TreeViewModel();
					deptTree.id = deptItem.Id;
					deptTree.text = deptItem.Name;
					deptTree.value = deptItem.Code;
					deptTree.parentId = deptItem.ParentId == null ? orgItem.Id : deptItem.ParentId;
					deptTree.isexpand = isExpand;  //Mặc định không mở rộng，Quá nhiều người
					deptTree.complete = true;
					deptTree.showcheck = false;
					deptTree.hasChildren = deptStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
					treeList.Add(deptTree);
				}
				#endregion
				TreeViewModel tree = new TreeViewModel();
				tree.id = orgItem.Id;
				tree.text = orgItem.Name;
				tree.value = orgItem.Code;
				tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //Đặc biệt
				tree.isexpand = true;
				tree.complete = true;
				tree.showcheck = false;
				//tree.hasChildren = orgDetpData.Count > 0 || orgStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
				tree.hasChildren = true;
				treeList.Add(tree);
			}
			//Bộ nhớ đệmend
			IList<string> checkedStaffIdList = new List<string>();
			if (from == "userrelatioinstaff" && !string.IsNullOrWhiteSpace(keyValue))   //Người dùng đã liên kết nhân sự
			{
				checkedStaffIdList = _sysStaffRepo.GetCurStaffIdListByUserId(keyValue);
			}

			var initIdSelectedArr = (initIdSelected ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			checkedStaffIdList = checkedStaffIdList.Union(initIdSelectedArr).Distinct().ToList();

			if (checkedStaffIdList != null && checkedStaffIdList.Count > 0)
			{
				foreach (var treeItem in treeList)
				{
					if (treeItem.showcheck && checkedStaffIdList.Contains(treeItem.id))
					{
						treeItem.checkstate = 1;
						//Đồng thời mở rộng cấp trên
						var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
						if (thisParentNode != null)
						{
							thisParentNode.isexpand = true;  //Cấp dưới có mục đã chọn，Ở đây nhất định mở rộng
						}
					}
				}
			}
			if (!isShowEmpty)   //Không hiển thị phần thừa
			{
				var count = 1;  //Số dòng đã loại bỏ
				while (count > 0)
				{
					count = treeList.RemoveAll(p => !p.showcheck && !p.hasChildren);
					//Điều chỉnh lại cây Loại bỏ Không có cấp dưới（Cấp dưới vừa bị di chuyển） Lại Không hiển thịshowcheckcủa
					treeList.RemoveAll(p => !p.showcheck && !treeList.Any(sub => sub.parentId == p.id));
				}
			}
			return Content(treeList.TreeViewJson(null));
		}

		public ActionResult GetWardInfo(string staffId)
		{
			var treeList = new List<TreeViewModel>();
			SysStaffEntity staffInfo = _sysStaffRepo.FindEntity(staffId);
			if (staffInfo == null || staffInfo.OrganizeId == "")
			{
				return Content(treeList.TreeViewJson(null));
			}

			string OrganizeId = staffInfo.OrganizeId;
			//Danh sách khu bệnh hiệu lực của hệ thống
			var wardList = _sysWardRepo.SelectWardList(OrganizeId);
			//Thông tin gắn khu bệnh của nhân viên
			var staffwards = new List<SysStaffWardEntity>();

			if (!string.IsNullOrWhiteSpace(staffId))
			{
				staffwards = _sysStaffWardRepo.GetStaffWardList(staffId).ToList();
			}
			foreach (var item in wardList)
			{
				TreeViewModel tree = new TreeViewModel();
				int i = staffwards.Count(t => t.bqCode == item.bqCode);

				tree.id = item.bqCode;
				tree.text = item.bqmc;
				tree.value = item.bqCode;
				tree.parentId = null;
				tree.isexpand = true;
				tree.complete = true;
				tree.showcheck = true;
				tree.checkstate = i;
				tree.hasChildren = false;
				treeList.Add(tree);
			}
			return Content(treeList.TreeViewJson(null));
		}

		[HttpPost]
		[HandlerAjaxOnly]
		//[ValidateAntiForgeryToken]
		public ActionResult UpdateStaffWard(string keyValue, string wardList)
		{
			SysStaffEntity staffInfo = _sysStaffRepo.FindEntity(keyValue);
			if (staffInfo != null || staffInfo.OrganizeId != "")
			{
				string OrganizeId = staffInfo.OrganizeId;
				_sysStaffDmnService.UpdateStaffWard(keyValue, (wardList ?? "").Split(','), OrganizeId);
				return Success("操作成功。");
			}
			else
			{
				return Error("操作失败，人员信息有误");
			}

		}



		public int checkEamil(string MsEmail)
		{
			var checkEmail = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
			Regex regex = new Regex(checkEmail);
			if (!regex.IsMatch(MsEmail))
			{
				return 0;
			}
			else
			{
				return 1;
			}


		}


        public ActionResult GetConsultInfo(string staffId,string ksCode)
        {
            var treeList = new List<TreeViewModel>();
            SysStaffEntity staffInfo = _sysStaffRepo.FindEntity(staffId);
            if (staffInfo == null || staffInfo.OrganizeId == "")
            {
                return Content(treeList.TreeViewJson(null));
            }

            string OrganizeId = staffInfo.OrganizeId;
            //Danh sách phòng khám hiệu lực hệ thống
            var consultList = _sysConsultRepo.GetValidConsultListByDept(OrganizeId,ksCode);
            //Thông tin gắn phòng khám của nhân viên
            var staffconsult = new List<SysStaffConsultEntity>();

            if (!string.IsNullOrWhiteSpace(staffId))
            {
                staffconsult = _sysStaffConsultRepo.GetStaffConsultList(staffId).ToList();
            }
            foreach (var item in consultList)
            {
                TreeViewModel tree = new TreeViewModel();
                int i = staffconsult.Count(t => t.zsCode == item.zsCode);

                tree.id = item.zsCode;
                tree.text = item.zsmc;
                tree.value = item.zsCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = i;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateStaffConsult(string keyValue, string zsCode)
        {
            SysStaffEntity staffInfo = _sysStaffRepo.FindEntity(keyValue);
            if (staffInfo != null || staffInfo.OrganizeId != "")
            {
                string OrganizeId = staffInfo.OrganizeId;
                _sysStaffDmnService.UpdateStaffConsult(keyValue, zsCode , OrganizeId);
                return Success("操作成功。");
            }
            else
            {
                return Error("操作失败，人员信息有误");
            }

        }


    }
}