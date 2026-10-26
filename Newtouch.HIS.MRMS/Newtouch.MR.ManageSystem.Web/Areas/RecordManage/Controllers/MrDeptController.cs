using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Newtouch.MR.ManageSystem.Web.Areas.RecordManage.Controllers
{
    public class MrDeptController :  OrgControllerBase
    {

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
        public ActionResult Index()
#pragma warning restore CS0114 // 'MrDeptController.Index()' hides inherited member 'BaseController.Index()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        {
            return View();
        }
#pragma warning disable CS0114 // 'MrDeptController.Form()' hides inherited member 'BaseController.Form()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        public ActionResult Form()
#pragma warning restore CS0114 // 'MrDeptController.Form()' hides inherited member 'BaseController.Form()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        {
            return View();
        }

        public ActionResult GetPagintionList(Pagination pagination, string keyword)
        {
            //object[] oplist = new[]{
            //    new{
            //        group="",
            //        name="Phòng X-quang",
            //        Code="07"
            //    },
            //    new {
            //        group="Khoa bệnh án1",
            //        name="Khoa Sản phụ",
            //        Code="00000005"
            //    },
            //    new{
            //        group="Khoa bệnh án2",
            //        name="Phòng thu phí",
            //        Code="00000009"
            //    },
            //    new{
            //        group="Khoa bệnh án3",
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

        //LấyKhoa bệnh ánNội dung hộp thả xuống
        public ActionResult GetDicDeptList(Pagination pagination, string keyword)
        {
            var oplist = _MrdicdeptRepo.GetDicDeptList(OrganizeId);
            var groupOptions = "";
            groupOptions += "<select>";
            groupOptions += "<option value=0>==请选择==</option>";
            foreach (var op in oplist)
            {
                groupOptions += "<option value="+op.ksbm + ">" + op.ksmc + "</option>";
            }
            groupOptions+= "</select>";
            return Content(groupOptions);

        }

        //Thêm mới Cập nhật Khoa bệnh ánhisKhoaQuan hệBảng
        public ActionResult SubmitForm(string hisdept, string hisdeptname, string baksId) {
            MrreldeptEntity entity= new MrreldeptEntity();
            entity.hisdept = hisdept;
            entity.hisdeptname = hisdeptname;
            entity.baksId = baksId;
            entity.OrganizeId = OrganizeId;
            entity.zt = "1";
            entity.CreateTime = Convert.ToDateTime(DateTime.Now);
            entity.CreatorCode= UserIdentity.UserName;
            _MrreldeptRepo.Save(entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// Lấy danh sách bệnh án theo trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetPagintionDicDeptList(Pagination pagination,string keyword) {
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
        /// Thêm mới/Sửa đổi Thông tin bệnh án
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitDicDept(MrdicdeptEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _MrdicdeptRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }  

        /// <summary>
        /// Tìm kiếmThông tin bệnh án đơn lẻ
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _MrdicdeptRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// XóaThông tin bệnh án
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteData(string keyValue)
        {
            _MrdicdeptRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
    }
}