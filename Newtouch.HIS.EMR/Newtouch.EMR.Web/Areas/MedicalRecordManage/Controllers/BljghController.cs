using NewtouchHIS.Framework.Web.Controllers;

using NewtouchHIS.Lib.Framework.Operator;

using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;

using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage
{
    [Area("MedicalRecordManage")]
    public class BljghController : OrgControllerBase
    {
        private readonly Ibl_ElementDomainRepo _bljsRepo;
        private readonly Ibl_ElementDomain_DetailRepo _bljsmxRepo;
        private readonly Ibl_bllxRepo _bllxRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="blysRepo"></param>
        public BljghController(Ibl_ElementDomainRepo bljsRepo, Ibl_ElementDomain_DetailRepo bljsmxRepo, Ibl_bllxRepo bllxRepo)
        {
            this._bljsRepo = bljsRepo;
            this._bljsmxRepo = bljsmxRepo;
            this._bllxRepo = bllxRepo;
        }
        // GET: MedicalRecordManage/Bljgh
        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult BlmlAdd()
        {
            return View();
        }
        public override IActionResult Form()
        {
            return View();
        }
        public IActionResult GetElementTree()
        {

            var treeList = new List<TreeViewModel>();
            var dataList = _bljsRepo.GetElementTree(OrganizeId);

            foreach (var data in dataList)
            {

                treeList.Add(new TreeViewModel()
                {
                    id = data.Id.ToString(),
                    value = data.Table_Name,
                    text = data.Table_Name,
                    Ex1 = data.Table_EngLish_Name,
                    Ex2=data.Regex,
                    Ex3=data.Bllx,
                    parentId = null,
                    hasChildren = false,
                    isexpand = false,
                });
            }
            var show = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));

        }

        public IActionResult GetGridJson(Pagination pagination, string ElementId)
        {
            OperatorModel user = this.UserIdentity;
            var data = new
            {
                rows = _bljsRepo.GetElementDetail(pagination, this.OrganizeId, ElementId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult GetYsList(string keyword)
        {
            var list = _bljsRepo.GetBlys(keyword,this.OrganizeId);
            foreach (var item in list)
            {
                item.yslxmc = ((EnumBlys)item.yslx).GetDescription();
            }
            return Content(list.ToJson());
        }

        public IActionResult SubmitForm(bl_ElementDomain_DetailEntity entity, string keyValue)
        {
            if (entity != null)
            {
                entity.OrganizeId = this.OrganizeId;
                entity.Element_Type_Name = ((EnumBlys)(entity.Element_Type)).GetDescription();
                _bljsmxRepo.SubmitForm(entity, keyValue);

                return Success("Thao tác thành công。");
            }
            return Error("Thao tác thất bại，Thông tin quan trọng không được để trống。");
           
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string delBljgmxId)
        {
            _bljsmxRepo.DeleteForm(delBljgmxId);
            return Success("Thao tác thành công。");
        }

        public IActionResult GetTableColumnList(string TableName, string keyword)
        {
            var list = _bljsRepo.GetTabColumnList(TableName,keyword);
            return Content(list.ToJson());
        }
        public IActionResult GetElementMx(string keyword)
        {
            var list = _bljsRepo.GetBlysmx(keyword,this.OrganizeId);
            return Content(list.ToJson());
        }

        public IActionResult GetBlTable(string keyword)
        {
            var list = _bllxRepo.IQueryable(p=> p.zt=="1" && p.OrganizeId==this.OrganizeId && p.IsRoot=="0" && p.bllxmc.Contains(keyword)).ToList();
            return Content(list.ToJson());
        }

        public IActionResult GetBljgMain(int keyValue)
        {
            var data = _bljsRepo.FindEntity(p => p.OrganizeId == OrganizeId && (p.Id == keyValue) && p.Zt == 1);
            return Content(data.ToJson());
        }

        public IActionResult BljgMainSubmitForm(bl_ElementDomainEntity ety, string keyValue)
        {
            ety.OrganizeId = OrganizeId;
            _bljsRepo.SubmitForm(ety, keyValue);
            return Success("Lưu thành công");
        }
        public IActionResult DeleteBljgMainForm(string keyValue)
        {
            _bljsRepo.DeleteForm(keyValue, OrganizeId, this.UserIdentity.rygh);
            return Success("Xóa thành công");
        }
    }
}