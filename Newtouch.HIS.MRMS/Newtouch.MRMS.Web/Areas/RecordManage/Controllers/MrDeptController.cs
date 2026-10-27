using Newtouch.MR.ManageSystem.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MRMS.Web.Models;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Newtouch.MRMS.Web.Areas.RecordManage.Controllers
{
    [Area("RecordManage")]
    public class MrDeptController :  OrgControllerBase
    {
        public MrDeptController(IMrdicdeptRepo MrdicdeptRepo, IMrDeptDmnService MrDeptDmnService, IMrreldeptRepo MrreldeptRepo)
        {
            this._MrdicdeptRepo = MrdicdeptRepo;
            this._MrDeptDmnService = MrDeptDmnService;
            this._MrreldeptRepo = MrreldeptRepo;
        }


#pragma warning disable CS0649 // Field 'MrDeptController._MrdicdeptRepo' is never assigned to, and will always have its default value null
        private readonly IMrdicdeptRepo _MrdicdeptRepo;
#pragma warning restore CS0649 // Field 'MrDeptController._MrdicdeptRepo' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'MrDeptController._MrDeptDmnService' is never assigned to, and will always have its default value null
        private readonly IMrDeptDmnService _MrDeptDmnService;
#pragma warning restore CS0649 // Field 'MrDeptController._MrDeptDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'MrDeptController._MrreldeptRepo' is never assigned to, and will always have its default value null
        private readonly IMrreldeptRepo _MrreldeptRepo;
#pragma warning restore CS0649 // Field 'MrDeptController._MrreldeptRepo' is never assigned to, and will always have its default value null

        // GET: RecordManage/MrDept
#pragma warning disable CS0114 // 'MrDeptController.Index()' hides inherited member 'BaseController.Index()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        public override IActionResult Index()
#pragma warning restore CS0114 // 'MrDeptController.Index()' hides inherited member 'BaseController.Index()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        {
            return View();
        }
#pragma warning disable CS0114 // 'MrDeptController.Form()' hides inherited member 'BaseController.Form()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        public override IActionResult Form()
#pragma warning restore CS0114 // 'MrDeptController.Form()' hides inherited member 'BaseController.Form()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        {
            return View();
        }

        public IActionResult GetPagintionList(Pagination pagination, string keyword)
        {
            //object[] oplist = new[]{
            //    new{
            //        group="",
            //        name="Khoa X-quang",
            //        Code="07"
            //    },
            //    new {
            //        group="Khoa bệnh án 1",
            //        name="Khoa Phụ sản",
            //        Code="00000005"
            //    },
            //    new{
            //        group="Khoa bệnh án 2",
            //        name="Phòng Thu phí",
            //        Code="00000009"
            //    },
            //    new{
            //        group="Khoa bệnh án 3",
            //        name="Nhà thuốc",
            //        Code="00000003"
            //    },

            //};
            var oplist = _MrDeptDmnService.GetPaginationList(pagination, OrganizeId);

            var data = new
            {
                rows = oplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        //Lấy nội dung hộp thả xuống khoa bệnh án
        public IActionResult GetDicDeptList(Pagination pagination, string keyword)
        {
            var oplist = _MrdicdeptRepo.GetDicDeptList(OrganizeId);
            var groupOptions = "";
            groupOptions += "<select>";
            groupOptions += "<option value=0>==Vui lòng chọn==</option>";
            foreach (var op in oplist)
            {
                groupOptions += "<option value="+op.ksbm + ">" + op.ksmc + "</option>";
            }
            groupOptions+= "</select>";
            return Content(groupOptions);

        }

        //Thêm mới Cập nhật Khoa bệnh ánhisBảng quan hệ khoa
        public IActionResult SubmitForm(string hisdept, string hisdeptname, string baksId) {
            MrreldeptEntity entity= new MrreldeptEntity();
            entity.hisdept = hisdept;
            entity.hisdeptname = hisdeptname;
            entity.baksId = baksId;
            entity.OrganizeId = OrganizeId;
            entity.zt = "1";
            entity.CreateTime = Convert.ToDateTime(DateTime.Now);
            entity.CreatorCode= UserIdentity.UserName;
            _MrreldeptRepo.Save(entity);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Lấy danh sách bệnh án phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetPagintionDicDeptList(Pagination pagination,string keyword) {
            var orgId = OrganizeId;
            var oplist = _MrdicdeptRepo.GetPagintionDicDeptList(pagination,orgId, keyword);
            var data = new
            {
                rows = oplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới/Sửa Thông tin bệnh án
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitDicDept(MrdicdeptEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _MrdicdeptRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công。");
        }  

        /// <summary>
        /// Truy vấn thông tin một bệnh án
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _MrdicdeptRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// XóaThông tin bệnh án
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteData(string keyValue)
        {
            _MrdicdeptRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }
    }
}