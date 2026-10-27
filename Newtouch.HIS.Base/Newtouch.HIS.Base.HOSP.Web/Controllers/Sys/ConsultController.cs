using System.Collections.Generic;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Linq;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.HIS.Domain.Entity.SystemManage;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers.Sys
{
    public class ConsultController : ControllerBase
    {
        private readonly ISysDepartmentRepository _sysDepartmentRepository;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysConsultDmnService _sysConsultDmnService;
        private readonly ISysConsultRepo _sysConsultRepo;
        private readonly ISysStaffRepo _sysStaffRepo;

        public ConsultController(ISysDepartmentRepository sysDepartmentRepository
            , ISysOrganizeDmnService sysOrganizeDmnService
            , ISysConsultDmnService sysConsultDmnService
            , ISysConsultRepo sysConsultRepo
            , ISysStaffRepo sysStaffRepo
            ) {
            this._sysDepartmentRepository = sysDepartmentRepository;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysConsultDmnService = sysConsultDmnService;
            this._sysConsultRepo = sysConsultRepo;
			this._sysStaffRepo = sysStaffRepo;
        }

        // GET: Consult
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExpertForm()
        {
            return View();
        }
        public IActionResult NormalForm()
        {
            return View();
        }

        /// <summary>
        /// Lấy danh sách khoa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetDeptList(string keyValue)
        {
            var entity = _sysDepartmentRepository.GetDeptListByKeyValue(this.OrganizeId,keyValue);
            return Content(entity.ToJson());
        }


        /// <summary>
        /// Lấy danh sách phòng khám của khoa
        /// </summary>
        /// <param name="ksCode">Số hiệu khoa</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetConsultListByDept(string ksCode)
        {
            var list = _sysConsultRepo.GetConsultListByDept(this.OrganizeId,ksCode);

            return Content(list.ToJson());
        }


        /// <summary>
        /// Phòng khám thường：Khi tạo phòng khám thông thường thì khởi tạo nội dung phòng khám
        /// </summary>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult InitConsultInfo(string ksCode)
        {
            var entity = new
            {
                orgId = this.OrganizeId,
                orgName = _sysOrganizeDmnService.GetNameByOrgId(this.OrganizeId),
                ksCode = ksCode,
                ksmc = _sysDepartmentRepository.GetNameByCode(ksCode, this.OrganizeId)
            };
            return Content(entity.ToJson());
        }


        /// <summary>
        /// Phòng khám chuyên gia：Lấy danh sách chuyên gia thuộc khoa
        /// </summary>
        /// <param name="ksCode">Số hiệu khoa</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetExpertListByDept(string ksCode)
        {
            var treeList = new List<TreeViewModel>();
            //Lấy bác sĩ chuyên gia
            var list = _sysConsultDmnService.GetExpertListByDept(this.OrganizeId, ksCode);
            if (list == null)
            {
                return Content(treeList.TreeViewJson(null));
            }
            foreach (var item in list)
            {
                TreeViewModel tree = new TreeViewModel();
                int i = list.Count(t => t.gh == item.gh);

                tree.id = item.gh;
                tree.text = item.name;
                tree.value = item.gh;
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

        /// <summary>
        /// Tạo phòng khám chuyên gia
        /// </summary>
        /// <param name="expert"></param>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        public IActionResult CreateExpertConsult(string expert, string ksCode)
        {
            var expertList = expert.Split(',');
            foreach (var gh in expertList) {
                var staff = _sysStaffRepo.IQueryable().Where(p => p.gh == gh && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault();
                SysConsultEntity entity= new SysConsultEntity();
                entity.zsmc = staff.Name + "Phòng khám chuyên gia";
                entity.zsCode = staff.py + "ZJZS";
                entity.ksCode = ksCode;
                entity.ys = gh;
                entity.OrganizeId = this.OrganizeId;
                entity.py = staff.py + "ZJZS";
                entity.xh = 0;//Phòng khám chuyên giaxhlà0
                
                //Phòng khám đã tồn tại thì không chèn nữa
                if (_sysConsultRepo.IQueryable().Any(p => p.zsCode == entity.zsCode && p.zt == "1"))
                {
                    continue;
                }

                entity.Create();
                _sysConsultRepo.Insert(entity);
            }
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Tạo phòng khám thường
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="zssl"></param>
        /// <returns></returns>
        public IActionResult CreateNormalConsult( string ksCode,int zssl)
        {
            //Tên khoa
            var ksmc=_sysDepartmentRepository.IQueryable().Where(p => p.Code == ksCode && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault().Name;
            //Số thứ tự phòng khám lớn nhất thuộc khoa
            var maxConsult = _sysConsultRepo.IQueryable().Where(p => p.ksCode == ksCode && p.OrganizeId == this.OrganizeId && p.zt == "1").OrderByDescending(p => p.xh).FirstOrDefault();
            var num = maxConsult == null ? 0 : maxConsult.xh;
            //Tự động tạo nhiều phòng khám
            for (var i=0;i<zssl;i++)
            {
                num++;
                SysConsultEntity entity = new SysConsultEntity();
                entity.zsmc = ksmc + "Phòng khám" + num;
                entity.zsCode = ksCode + "zs"+num;
                entity.ksCode = ksCode;
                entity.ys = null;
                entity.OrganizeId = this.OrganizeId;
                entity.py = ksCode + "zs" + num;
                entity.xh = num;

                //Phòng khám đã tồn tại thì không chèn nữa
                if (_sysConsultRepo.IQueryable().Any(p => p.zsCode == entity.zsCode && p.zt == "1"))
                {
                    continue;
                }

                entity.Create();
                _sysConsultRepo.Insert(entity);
            }
            return Success("Thao tác thành công.");
        }
    }
}