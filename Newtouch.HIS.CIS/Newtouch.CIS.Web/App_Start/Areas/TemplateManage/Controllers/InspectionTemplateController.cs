using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    public class InspectionTemplateController : OrgControllerBase
    {
        private readonly IInspectionTemplateRepo _inspectionTemplateRepo;
        private readonly IInspectionCategoryRepo _inspectionCategoryRepo;
        private readonly IInspectionTemplateDmnService _inspectionTemplateDmnService;
        // GET: TemplateManage/Inspection
        
        /// <summary>
        /// Nhóm lớn xét nghiệm kiểm tra Thả xuống
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCategoryList()
        {
            var data = _inspectionCategoryRepo.IQueryable().Where(a => a.OrganizeId == this.OrganizeId && a.zt == "1").ToList();
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// Danh sách mẫu (tra cứu)
        /// </summary>
        /// <param name="zutaoType"></param>
        /// <returns></returns>
        public ActionResult SelectGridList(int type)
        {
            var data = _inspectionTemplateRepo.GetList(type, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Chi tiết mẫu
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        public ActionResult GetTemplateDetail(string mbId)
        {
            var data = _inspectionTemplateDmnService.GetTemplateDetail(mbId, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="ztobj"></param>
        /// <param name="ztxmlist"></param>
        /// <returns></returns>
        public ActionResult SaveData(InspectionTemplateEntity mbobj, List<TemplateGroupPackageEntity> mbztlist)
        {
            mbobj.zt = mbobj.zt == "on" ? "1" : "0";
            //Bảng mẫu
            mbobj.OrganizeId = this.OrganizeId;

            _inspectionTemplateDmnService.SaveData(mbobj, mbztlist);
            return Success();
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        public ActionResult DeleteData(string mbId)
        {
            _inspectionTemplateDmnService.DeleteData(mbId);
            return Success();
        }

        /// <summary>
        /// Cây mẫu xét nghiệm kiểm tra của trang đơn thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeList(int jyjcmbLx)
        {
            var treeList = new List<TreeViewModel>();
            var alldldata = _inspectionTemplateDmnService.GetTemplateListByType(this.OrganizeId, jyjcmbLx);
            var dldata = alldldata.Select(a => new { a.dlmc }).Distinct().ToList();
            foreach (var item in dldata)
            {
                //Nhóm lớn xét nghiệm kiểm tra
                treeList.Add(new TreeViewModel()
                {
                    id = item.dlmc,
                    value = item.dlmc,
                    text = item.dlmc,
                    parentId = null,
                    hasChildren = true,
                    isexpand = true,
                });
                var mbdata = alldldata.Where(a => a.dlmc == item.dlmc).Select(a => new { a.mbId, a.mbmc }).ToList();
                foreach (var mb in mbdata)
                {
                    //Mẫu
                    var ztdata = _inspectionTemplateDmnService.GetTemplateDetailByMbId(this.OrganizeId, mb.mbId).Select(a => new { a.ztId, a.ztmc }).Distinct();
                    //bool hasChildren = ztdata.Count() > 0 ? true : false;
                    bool hasChildren = true;
                    treeList.Add(new TreeViewModel()
                    {
                        id = mb.mbId,
                        value = mb.mbId,
                        text = mb.mbmc,
                        parentId = item.dlmc,
                        hasChildren = hasChildren,
                        isexpand = hasChildren,
                        complete = true
                    });
                    //Bộ mẫu
                    foreach (var zt in ztdata)
                    {
                        treeList.Add(new TreeViewModel()
                        {
                            id = zt.ztId,
                            value = zt.ztId,
                            text = zt.ztmc,
                            parentId = mb.mbId,
                            hasChildren = false,
                            isexpand = false,
                            complete = true
                        });
                    }
                }
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// TheoztIdTra cứu hạng mục thu phí thuộc bộ mẫu
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGPackageDetailByZtId(string ztId)
        {
            var sfxmdata = _inspectionTemplateDmnService.GetGPackageDetailByZtId(this.OrganizeId, ztId);
            return Content(sfxmdata.ToJson());
        }

    }
}