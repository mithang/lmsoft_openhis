using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Newtouch.HIS.Repository;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// Danh mục thu phí hệ thống lớn
    /// </summary>
    public class ChargeCategoryController : ControllerBase
    {
        private readonly ISysChargeCategoryRepo _SysChargeCategoryRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysChargeCategoryBaseRepo _SysChargeCategoryBaseRepo;

        public ChargeCategoryController(ISysChargeCategoryRepo SysChargeCategoryRepo
            , ISysOrganizeDmnService sysOrganizeDmnService , ISysChargeCategoryBaseRepo SysChargeCategoryBaseRepo)
        {
            this._SysChargeCategoryRepo = SysChargeCategoryRepo;
            this._SysOrganizeDmnService = sysOrganizeDmnService;
            this._SysChargeCategoryBaseRepo = SysChargeCategoryBaseRepo;
        }

        //Cây danh mục thu phí lớn(Bệnh viện) Thả xuống Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeSelectJson(string organizeId, string treeidFieldName = "Code")
        {
            // FA-018: empty organizeId must behave like an unknown org (empty tree), not NRE on Equals.
            if (string.IsNullOrEmpty(organizeId))
            {
                return Content("[]");
            }
            var data = _SysChargeCategoryRepo.GetValidList(organizeId);
            var data1 = _SysChargeCategoryBaseRepo.GetValidList(organizeId);
            var treeList = new List<TreeSelectModel>();
           // Dữ liệu cơ bản
            if (organizeId .Equals("*") )
            {
                // Duyệt data1
                foreach (var item in data1)
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    if (treeidFieldName == "Code")
                    {
                        treeModel.id = item.dlCode;
                        treeModel.text = item.dlmc;
                        treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                            ? data1.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault()
                            : null;
                    }
                    else
                    {
                        treeModel.id = item.dlId.ToString();
                        treeModel.text = item.dlmc;
                        treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0) ? item.ParentId.ToString() : null;
                    }
                    treeList.Add(treeModel);
                }
            }
            else
            {
                // Duyệt data
                foreach (var item in data)
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    if (treeidFieldName == "Code")
                    {
                        treeModel.id = item.dlCode;
                        treeModel.text = item.dlmc;
                        treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                            ? data.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault()
                            : null;
                    }
                    else
                    {
                        treeModel.id = item.dlId.ToString();
                        treeModel.text = item.dlmc;
                        treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0) ? item.ParentId.ToString() : null;
                    }
                    treeList.Add(treeModel);
                }
            }

            return Content(treeList.TreeSelectJson(null));

        }

        /// <summary>
        /// Lấy danh sách dạng cây
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetTreeGridJson(string organizeId, string zt,string keyword)
        {
            // FA-018: empty organizeId must behave like an unknown org (empty tree), not NRE on Equals.
            if (string.IsNullOrEmpty(organizeId))
            {
                return Content("[]");
            }
            if (!organizeId.Equals("*"))
            {
                //Dữ liệu không cơ sở
                var data = _SysChargeCategoryRepo.GetList(organizeId,zt);
                if (!string.IsNullOrEmpty(keyword))
                {
                    data = data.ToList().TreeWhereForKeyInt(t => t.dlmc.Contains(keyword), keyValue: "dlId",
                        parentId: "ParentId");
                }

                var treeList = new List<TreeGridModel>();
                foreach (var item in data)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    treeModel.id = item.dlId.ToString();
                    bool hasChildren = data.Count(t => t.ParentId == item.dlId) == 0 ? false : true;
                    treeModel.id = item.dlId.ToString();
                    treeModel.isLeaf = hasChildren;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                        ? item.ParentId.ToString()
                        : null;
                    treeModel.expanded = hasChildren;
                    treeModel.entityJson = item.ToJson();
                    treeList.Add(treeModel);
                }

                return Content(treeList.TreeGridJson(null));
            }
            else
            {
                var data = _SysChargeCategoryBaseRepo.GetList(organizeId);
                if (!string.IsNullOrEmpty(keyword))
                {
                    data = data.ToList().TreeWhereForKeyInt(t => t.dlmc.Contains(keyword), keyValue: "dlId",
                        parentId: "ParentId");
                }

                var treeList = new List<TreeGridModel>();
                foreach (var item in data)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    treeModel.id = item.dlId.ToString();
                    bool hasChildren = data.Count(t => t.ParentId == item.dlId) == 0 ? false : true;
                    treeModel.id = item.dlId.ToString();
                    treeModel.isLeaf = hasChildren;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                        ? item.ParentId.ToString()
                        : null;
                    treeModel.expanded = hasChildren;
                    treeModel.entityJson = item.ToJson();
                    treeList.Add(treeModel);
                }

                return Content(treeList.TreeGridJson(null));
            }
        }

        /// <summary>
        /// Thêm mới hoặc sửa Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue, string orgId = null)
        {

            if ("*".Equals(orgId))
            {
                var entity = _SysChargeCategoryBaseRepo.GetForm(keyValue);
                return Content(entity.ToJson());
            }
            else
            {
                var entity = _SysChargeCategoryRepo.GetForm(keyValue);
                return Content(entity.ToJson());

            }
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysChargeCategoryEntity entity, int? keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.mzprintreportcode))
            {
                entity.mzprintreportcode = entity.dlCode;
            }
            if (string.IsNullOrWhiteSpace(entity.mzprintbillcode))
            {
                entity.mzprintbillcode = entity.dlCode;
            }
            if (keyValue.HasValue && keyValue.Value > 0 && keyValue.Value == entity.ParentId)
            {
                throw new FailedException("Chọn nhóm lớn cấp trên sai");
            }
            if (!string.IsNullOrWhiteSpace(entity.fjCode))
            {
                if (!int.TryParse(entity.fjCode, out var fjCodeVal))
                {
                    throw new FailedException("Phân loại sở thuộc không đúng định dạng.");
                }
                entity.fjmc = GetFjmcByCode(fjCodeVal, entity, keyValue);
            }
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId) && !"*".Equals(entity.OrganizeId) )
            {
                throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
            }

            if ("*".Equals(entity.OrganizeId))
            {
                _SysChargeCategoryBaseRepo.SubmitForm(entity.ToJson().ToObject<SysChargeCategoryBaseEntity>(), keyValue);
            }
            else
            {
                _SysChargeCategoryRepo.SubmitForm(entity, keyValue);
            }
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// V6-F1: map fjCode → fjmc theo từ vựng Scope A đã áp live trên STG (không ghi tên member enum thô).
        /// - 1002 → 'Phi kiem tra' (GIỮ theo QĐ V6-F1(1): 'Phí khám'/PhiKham là từ của 诊察费 'Phi kham benh', không phải 检查费).
        /// - 1001 → GIỮ 2 giá trị thuốc phân biệt ('Phi thuoc Tay y' / 'Phi thuoc thanh pham Dong y') khi sửa dòng đang mang giá trị đó (QĐ V6-F1(2)); dòng mới hoặc dòng chưa có giá trị phân biệt ghi 'PhiThuoc'.
        /// - 1003–1007 → tên member PascalCase (trùng giá trị đã chuẩn hoá trên STG); fjCode ngoài dải này giữ hành vi cũ (enum ToString).
        /// Lưu ý: form không có ô fjmc nên entity.fjmc post lên luôn rỗng — muốn giữ giá trị cũ phải đọc lại dòng đang lưu (GetStoredFjmc), không dựa vào entity post lên.
        /// </summary>
        private string GetFjmcByCode(int fjCodeVal, SysChargeCategoryEntity entity, int? keyValue)
        {
            switch (fjCodeVal)
            {
                case 1002:
                    return "Phi kiem tra";
                case 1001:
                    var storedFjmc = GetStoredFjmc(entity, keyValue);
                    if (storedFjmc == "Phi thuoc Tay y" || storedFjmc == "Phi thuoc thanh pham Dong y")
                    {
                        return storedFjmc;
                    }
                    return Newtouch.Infrastructure.EnumSffjdlmc.PhiThuoc.ToString();
                default:
                    return ((Newtouch.Infrastructure.EnumSffjdlmc)fjCodeVal).ToString();
            }
        }

        /// <summary>
        /// Đọc fjmc đang lưu của dòng sắp sửa (repo tương ứng với OrganizeId sẽ ghi).
        /// Chỉ dùng khi fjCode = 1001 để giữ 2 giá trị thuốc phân biệt; dòng mới (keyValue rỗng/0) trả null.
        /// </summary>
        private string GetStoredFjmc(SysChargeCategoryEntity entity, int? keyValue)
        {
            if (!keyValue.HasValue || keyValue.Value <= 0)
            {
                return null;
            }
            if ("*".Equals(entity.OrganizeId))
            {
                return _SysChargeCategoryBaseRepo.FindEntity(val => val.dlId == keyValue.Value)?.fjmc;
            }
            return _SysChargeCategoryRepo.FindEntity(val => val.dlId == keyValue.Value)?.fjmc;
        }

        /// <summary>
        /// Sửa trạng thái
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult UpdateZt(int dlId,string zt,string organizeId, int? keyValue)
        {
            if ("*".Equals(organizeId))
            {
                var entity = _SysChargeCategoryBaseRepo.FindEntity(val => val.dlId == dlId);
                // FA-018: unknown key must fail faithfully, not NRE on the null entity.
                if (entity == null)
                {
                    return Error("Thao tác thất bại.");
                }
                entity.zt = zt;
                _SysChargeCategoryBaseRepo.Update(entity);
            }
            else
            {
                var entity = _SysChargeCategoryRepo.FindEntity(val => val.dlId == dlId);
                // FA-018: unknown key must fail faithfully, not NRE on the null entity.
                if (entity == null)
                {
                    return Error("Thao tác thất bại.");
                }
                entity.zt = zt;
                _SysChargeCategoryRepo.Update(entity);
            }
            return Success("Thao tác thành công.");
        }

    }
}