using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using Newtouch.Tools.Excel;
using System.Text.RegularExpressions;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.HIS.Domain.Entity.SystemManage;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
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

		//grid json 数据源
		[HttpGet]
		[HandlerAjaxOnly]
		public IActionResult GetGridJson(Pagination pagination, string keyword, string OrganizeId)
		{
			pagination.sidx = "DepartmentCode asc,CreateTime desc";
			pagination.sord = "asc";
			if (string.IsNullOrWhiteSpace(OrganizeId))
			{
				throw new FailedException("Không xác định được cơ sở trong phạm vi quyền hiện tại");
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
		/// 工作人员导出
		/// </summary>
		/// <param name="key"></param>
		/// <param name="kpzt"></param>
		/// <param name="createTimestart"></param>
		/// <param name="createTimeEnd"></param>
		/// <param name="cols"></param>
		/// <param name="colStanWidth"></param>
		/// <param name="isContainFilter"></param>
		/// <returns></returns>
		public IActionResult OutpatientExport(string keyword, string cols, int colStanWidth, bool? isContainFilter)
		{
			var orgId = this.OrganizeId;
			if (string.IsNullOrEmpty(orgId))
			{
				throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
			}
			if (string.IsNullOrWhiteSpace(cols))
			{
				cols = HttpWebHelper.GetCookie("ExportExcelCols");
				if (!string.IsNullOrWhiteSpace(cols))
				{
					cols = System.Net.WebUtility.UrlDecode(cols);
					HttpWebHelper.RemoveCookie("ExportExcelCols");
				}
			}
			if (string.IsNullOrWhiteSpace(cols))
			{
				throw new FailedException("Chưa chỉ định cột xuất");
			}

			var pagination = new Pagination();
			pagination.sidx = "CreateTime desc"; //时间升序
			pagination.rows = 65536 - 1;    //Excel最大行数
			pagination.page = 1;    //第一页把所有都查出来

			var list = _sysStaffDmnService.GetPaginationList(pagination, OrganizeId, keyword);

			var data = list.Select(r => new StaffExport
			{
				gh = r.gh,
				Name = r.Name,
				Gender = r.Gender == true ? "Nam" : "Nữ" ?? "",
				zc = r.zc,
				kflb = r.kflb == "RTM_OT" ? "Trị liệu nghề nghiệp" : r.kflb == "RTM_PT" ? "Vật lý trị liệu" : r.kflb == "RTM_ST" ? "Trị liệu ngôn ngữ" : "",
				MobilePhone = r.MobilePhone,
				gjybdm = r.gjybdm,
				staffNames = r.staffNames,
				OrganizeName = r.OrganizeName,
				DepartmentName = r.DepartmentName,
				Description = r.Description,
				zt = r.zt == "1" ? "Hiệu lực" : "Vô hiệu",
				CreatorCode = r.CreatorCode,
				sj1 = r.CreateTime,
				LastModifierCode = r.LastModifierCode,
				sj2 = r.LastModifyTime
			}).ToList();

			var colList = cols.ToObject<IList<ExcelColumn>>();
			var sheet = new ExcelSheet()
			{
				Title = "Nhân viên",
				columns = colList,
			};
			sheet.columns.ToList().ForEach(p =>
				{
					p.WidthTimes = (double)p.Width / colStanWidth;
					p.Width = 0;    //Width都置为0
				});

			var path = DateTime.Now.ToString("\\\\yyyyMMdd\\\\HHmmssfff") + "Nhân viên.xls";

			var filePath = CommmHelper.GetLocalFilePath("\\ExcelXuất ra\\Xuất nhân viên" + path);

			if (isContainFilter == true)
			{
				//筛选条件
				var filterDict = new Dictionary<string, string>();
				if (!string.IsNullOrWhiteSpace(keyword))
				{
					filterDict.Add("Từ khóa", keyword);
				}
				if (filterDict.Count > 0)
				{
					sheet.filters = filterDict;
				}
			}

			var rest = data.ToExcel(filePath, sheet);

			if (rest)
			{
				return PhysicalFile(filePath, "application/x-xls", path.Replace("\\", ""));
			}
			else
			{
				return Content("Xuất tệp thất bại,Vui lòng quay lại trang danh sách và thử lại");
			}
		}



		[HttpGet]
		[HandlerAjaxOnly]
		public IActionResult GetFormJson(string keyValue)
		{
			var data = _sysStaffRepo.FindEntity(keyValue);
			return Content(data.ToJson());
		}
		[HttpGet]
		[HandlerAjaxOnly]
		public IActionResult GetFormPicture(string keyValue, string orgId)
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
		public IActionResult SubmitForm(SysStaffEntity entity, string keyValue, bool asLoginUser, string dutyList)
		{
			var staffId = "";
			if (string.IsNullOrWhiteSpace(entity.Name) || string.IsNullOrWhiteSpace(entity.OrganizeId))
			{
				throw new FailedException("Dữ liệu chưa đầy đủ,Vui lòng nhập lại và gửi");
			}
			entity.zt = entity.zt == "true" ? "1" : "0";
			entity.TopOrganizeId = Constants.TopOrganizeId;
			_sysStaffDmnService.SubmitForm(entity, keyValue, asLoginUser, out staffId);
			_sysStaffDmnService.UpdateStaffDuty(entity.Id, (dutyList ?? "").Split(','));
			return Success("Thao tác thành công.", staffId);
		}
		[HttpPost]
		[HandlerAjaxOnly]
		public IActionResult FileUpLoad(string staffId, string orgId)
		{
			var pUpImage = Request.Form.Files["file"];
			//判断是否上传了图片
			if (pUpImage != null)
			{
				var maxbyte = 10 * 1024;
				long sFileLength = pUpImage.Length;
				if (sFileLength > maxbyte)
				{
					throw new FailedException("Ảnh tải lên không được vượt quá 10kb");
				}
				//获取文件名
				string sFilename = System.IO.Path.GetFileName(pUpImage.FileName).ToLower();
				//获取upImage文件的扩展名
				string extendName = System.IO.Path.GetExtension(sFilename);
				//判断是否为图片格式
				if (extendName != ".jpg" && extendName != ".png")
				{
					throw new FailedException("Ảnh tải lên chỉ hỗ trợ định dạng .jpg/.png");
				}
				byte[] image;
				using (var ms = new System.IO.MemoryStream())
				{
					pUpImage.CopyTo(ms);
					image = ms.ToArray();
				}

				var savepath = "/PictureUploadFile/";
				var dirpath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", savepath.TrimStart('/'));
				if (!Directory.Exists(dirpath))
				{
					Directory.CreateDirectory(dirpath);
				}
				var picNewName = DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetFileName(pUpImage.FileName);
				var picUrl = System.IO.Path.Combine(dirpath, picNewName);
				using (var fs = new System.IO.FileStream(picUrl, System.IO.FileMode.Create))
				{
					pUpImage.CopyTo(fs);
				}

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
		public virtual IActionResult Duties()
		{
			return View();
		}

		public virtual IActionResult WardInfo()
		{
			return View();
		}
        public virtual IActionResult ConsultInfo()
        {
            return View();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public IActionResult ExcelExportChooseColumns()
		{
			return View();
		}

		[HttpPost]
		[HandlerAjaxOnly]
		//[ValidateAntiForgeryToken]
		public IActionResult UpdateStaffDuty(string keyValue, string dutyList)
		{
			_sysStaffDmnService.UpdateStaffDuty(keyValue, (dutyList ?? "").Split(','));
			return Success("Thao tác thành công.");
		}

		/// <summary>
		/// 关联用户
		/// </summary>
		/// <returns></returns>
		public IActionResult Selector()
		{
			return View();
		}

		/// <summary>
		/// 获取机构人员树 三级（机构+科室+人员）
		/// </summary>
		/// <param name="keyValue"></param>
		/// <returns></returns>
		public IActionResult GetStaffSelecotrTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, string initIdSelected = null)
		{
			if (string.IsNullOrWhiteSpace(organizeId))
			{
				organizeId = base.GetAuthOrganizeId();  //默认 QuyềnOrganizeId
			}
			//缓存start
			var organizedata = _sysOrganizeRepository.GetValidListByParentOrg(organizeId);
			var treeList = new List<TreeViewModel>();
			foreach (SysOrganizeEntity orgItem in organizedata)
			{
				//机构科室集合
				var orgDetpData = _sysDepartmentRepository.GetListByOrg(orgItem.Id);
				//机构人员集合
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
						staffTree.checkstate = 0;   //外面赋值，方便缓存
						treeList.Add(staffTree);
					}
					#endregion
					TreeViewModel deptTree = new TreeViewModel();
					deptTree.id = deptItem.Id;
					deptTree.text = deptItem.Name;
					deptTree.value = deptItem.Code;
					deptTree.parentId = deptItem.ParentId == null ? orgItem.Id : deptItem.ParentId;
					deptTree.isexpand = isExpand;  //默认不展开，人太多
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
				tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //特殊
				tree.isexpand = true;
				tree.complete = true;
				tree.showcheck = false;
				//tree.hasChildren = orgDetpData.Count > 0 || orgStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
				tree.hasChildren = true;
				treeList.Add(tree);
			}
			//缓存end
			IList<string> checkedStaffIdList = new List<string>();
			if (from == "userrelatioinstaff" && !string.IsNullOrWhiteSpace(keyValue))   //用户已关联人员
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
						//同时让上级展开
						var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
						if (thisParentNode != null)
						{
							thisParentNode.isexpand = true;  //下级有选中的，这里一定展开之
						}
					}
				}
			}
			if (!isShowEmpty)   //不显示多余的
			{
				var count = 1;  //移除的行数
				while (count > 0)
				{
					count = treeList.RemoveAll(p => !p.showcheck && !p.hasChildren);
					//重新调整下树 移除 没有下级（下级刚被移了） 又 不显示showcheck的
					treeList.RemoveAll(p => !p.showcheck && !treeList.Any(sub => sub.parentId == p.id));
				}
			}
			return Content(treeList.TreeViewJson(null));
		}

		public IActionResult GetWardInfo(string staffId)
		{
			var treeList = new List<TreeViewModel>();
			SysStaffEntity staffInfo = _sysStaffRepo.FindEntity(staffId);
			if (staffInfo == null || staffInfo.OrganizeId == "")
			{
				return Content(treeList.TreeViewJson(null));
			}

			string OrganizeId = staffInfo.OrganizeId;
			//系统有效病区列表
			var wardList = _sysWardRepo.SelectWardList(OrganizeId);
			//员工病区绑定信息
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
		public IActionResult UpdateStaffWard(string keyValue, string wardList)
		{
			SysStaffEntity staffInfo = _sysStaffRepo.FindEntity(keyValue);
			if (staffInfo != null || staffInfo.OrganizeId != "")
			{
				string OrganizeId = staffInfo.OrganizeId;
				_sysStaffDmnService.UpdateStaffWard(keyValue, (wardList ?? "").Split(','), OrganizeId);
				return Success("Thao tác thành công.");
			}
			else
			{
				return Error("Thao tác thất bại,Thông tin nhân viên không đúng");
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


        public IActionResult GetConsultInfo(string staffId,string ksCode)
        {
            var treeList = new List<TreeViewModel>();
            SysStaffEntity staffInfo = _sysStaffRepo.FindEntity(staffId);
            if (staffInfo == null || staffInfo.OrganizeId == "")
            {
                return Content(treeList.TreeViewJson(null));
            }

            string OrganizeId = staffInfo.OrganizeId;
            //系统有效诊室列表
            var consultList = _sysConsultRepo.GetValidConsultListByDept(OrganizeId,ksCode);
            //员工诊室绑定信息
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
        public IActionResult UpdateStaffConsult(string keyValue, string zsCode)
        {
            SysStaffEntity staffInfo = _sysStaffRepo.FindEntity(keyValue);
            if (staffInfo != null || staffInfo.OrganizeId != "")
            {
                string OrganizeId = staffInfo.OrganizeId;
                _sysStaffDmnService.UpdateStaffConsult(keyValue, zsCode , OrganizeId);
                return Success("Thao tác thành công.");
            }
            else
            {
                return Error("Thao tác thất bại,Thông tin nhân viên không đúng");
            }

        }


    }
}