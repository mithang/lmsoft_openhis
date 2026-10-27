using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;

namespace Newtouch.Herp.Web.Areas.WarehouseManage.Controllers
{
    /// <summary>
    /// Quản lý kho
    /// </summary>
    [Area("WarehouseManage")]
    public class WarehouseController : ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IRelWarehouseDeptRepo _relWarehouseDeptRepo;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly IRelWarehouseUserRepo _relWarehouseUserRepo;
        private readonly IWarehouseApp _warehouseApp;
        private readonly IWarehouseDmnService _warehouseDmnService;
        private readonly IKfWarehouseRepo _kfWarehouseRepo;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IRelProductWarehouseRepo _relProductWarehouseRepe;
        private readonly IWzProductDmnService _wzProductDmnService;

        public WarehouseController(ISysDepartmentRepo sysDepartmentRepo, IRelWarehouseDeptRepo relWarehouseDeptRepo,
            ISysStaffRepo sysStaffRepo, IRelWarehouseUserRepo relWarehouseUserRepo, IWarehouseApp warehouseApp,
            IWarehouseDmnService warehouseDmnService, IKfWarehouseRepo kfWarehouseRepo, IWzProductRepo wzProductRepo,
            IRelProductWarehouseRepo relProductWarehouseRepo, IWzProductDmnService wzProductDmnService)
        {
            _sysDepartmentRepo = sysDepartmentRepo;
            _relWarehouseDeptRepo = relWarehouseDeptRepo;
            _sysStaffRepo = sysStaffRepo;
            _relWarehouseUserRepo = relWarehouseUserRepo;
            _warehouseApp = warehouseApp;
            _warehouseDmnService = warehouseDmnService;
            _kfWarehouseRepo = kfWarehouseRepo;
            _wzProductRepo = wzProductRepo;
            _relProductWarehouseRepe = relProductWarehouseRepo;
            _wzProductDmnService = wzProductDmnService;
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                _warehouseDmnService.DeleteWarehouse(keyValue);
            }
            return Success("Xóa thành công.");
        }

        /// <summary>
        /// Lấy thông tin kho
        /// </summary>
        /// <returns></returns>
        public IActionResult GetWarehouseGridJson(string keyWord)
        {
            var data = _warehouseDmnService.QueryWarehouseInfo("", OrganizeId);
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                data = data.ToList().TreeWhere(t => t.name.Contains(keyWord), parentId: "ParentId");
            }
            var treeList = (from item in data
                            let hasChildren = data.Count(t => t.parentId == item.Id) != 0
                            select new TreeGridModel
                            {
                                id = item.Id,
                                isLeaf = hasChildren,
                                parentId = item.parentId,
                                expanded = hasChildren,
                                entityJson = item.ToJson()
                            }).ToList();
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// Lấy thông tin kho
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyWord)
        {
            var warehouse = _kfWarehouseRepo.FindEntity(p => p.Id == keyWord && p.OrganizeId == OrganizeId);
            return Content(warehouse.ToJson());
        }

        /// <summary>
        /// Lấy khoa
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetDeptmentSelecotrTree(string keyWord)
        {
            var depts = _sysDepartmentRepo.GetList(OrganizeId, ((int)Enumzt.Enable).ToString());
            var treeList = new List<TreeViewModel>();
            if (depts == null || depts.Count <= 0) return Content(treeList.TreeViewJson(null));
            var deptWareRel = _relWarehouseDeptRepo.GetListByWarehouseId(keyWord, OrganizeId) ?? new List<RelWarehouseDeptEntity>();
            depts.ToList().ForEach(p =>
            {
                treeList.Add(new TreeViewModel
                {
                    id = p.Id,
                    text = p.Name,
                    value = p.Id,
                    parentId = p.ParentId,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    hasChildren = true,
                    checkstate = deptWareRel.Any(q => q.deptId == p.Id) ? 1 : 0
                });
            });
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Lấy nhân viên
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetStaffSelecotrTree(string keyWord)
        {
            var staff = _sysStaffRepo.GetValidStaffListByOrganizeId(OrganizeId);
            var treeList = new List<TreeViewModel>();
            if (staff == null || staff.Count <= 0) return Content(treeList.TreeViewJson(null));
            var staffWareRel = _relWarehouseUserRepo.GetListByWarehouseId(keyWord, OrganizeId) ?? new List<RelWarehouseUserEntity>();
            staff.ToList().ForEach(p =>
            {
                treeList.Add(new TreeViewModel
                {
                    id = p.gh,
                    text = p.Name + "(" + p.gh + ")",
                    value = p.gh,
                    parentId = null,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    hasChildren = true,
                    checkstate = staffWareRel.Any(q => q.gh == p.gh) ? 1 : 0
                });
            });
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Lấy cấp trên
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPatientTreeSelectJson()
        {
            var warehouse = _kfWarehouseRepo.IQueryable(p => p.OrganizeId == OrganizeId && p.zt == ((int)Enumzt.Enable).ToString()).ToList();
            var treeList = new List<TreeSelectModel>();
            foreach (var item in warehouse)
            {
                var treeModel = new TreeSelectModel
                {
                    id = item.Id,
                    text = item.name,
                    parentId = item.parentId
                };
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// Bảo trì kho Gửi biểu mẫu
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="staffghs"></param>
        /// <param name="departmentIds"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(KfWarehouseEntity wzEntity, string staffghs, string departmentIds, string keyWord)
        {
            wzEntity.zt = wzEntity.zt == "true" ? "1" : "0";
            wzEntity.isDefSyn = wzEntity.isDefSyn == "true" ? "1" : "0";
            wzEntity.OrganizeId = OrganizeId;
            _warehouseApp.SubmitForm(wzEntity, staffghs.Split(','), departmentIds.Split(','), keyWord);
            return Success("Thao tác thành công", null);
        }

        #region Đồng bộ vật tư

        /// <summary>
        /// Đồng bộ vật tư
        /// </summary>
        /// <returns></returns>
        public IActionResult SynceProduct()
        {
            return View();
        }

        /// <summary>
        /// Lấy thông tin vật tư kho đồng bộ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="wId">ID kho</param>
        /// <param name="keyWord"></param>
        /// <param name="wzlb"></param>
        /// <returns></returns>
        public IActionResult GettbwzGridJson(Pagination pagination, string wId, string keyWord, string wzlb)
        {
            var data = new
            {
                rows = _warehouseDmnService.GetSyncProductList(pagination, wId, wzlb, keyWord, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Kiểm soát vật tư
        /// </summary>
        /// <param name="relId"></param>
        /// <param name="wId">khoID</param>
        /// <param name="op">Thao tác 1：Hủy kiểm soát  0：Kiểm soát </param>
        /// <returns></returns>
        public IActionResult ControlWz(string relId, string wId, string op = "")
        {
            var rel = _relProductWarehouseRepe.FindEntity(p => p.Id == relId && p.OrganizeId == OrganizeId && p.warehouseId == wId);
            rel.zt = op == ((int)Enumzt.Enable).ToString() ? ((int)Enumzt.Enable).ToString() : ((int)Enumzt.Disable).ToString();
            rel.Modify();
            return _relProductWarehouseRepe.Update(rel) > 0 ? Success("Kiểm soát thành công") : Error("Kiểm soát thất bại");
        }

        /// <summary>
        /// Cập nhật quan hệ liên kết vật tư kho
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="opereateType">0:Thêm  1：Xóa</param>
        /// <param name="keyWord">Kho đích</param>
        /// <returns></returns>
        public IActionResult FreshWhAndwzRelList(string[] productIds, int opereateType, string keyWord)
        {
            var list = productIds.ToList();
            var result = _warehouseApp.FreshWhAndwzRelList(list, opereateType, OrganizeId, keyWord);
            //return result ? Success("Thao tác thành công") : Error("Thao tác thất bại");
            return Success("Thao tác thành công");
        }

        #endregion

        #region Bảo trì đơn vị vật tư của bộ phận

        public IActionResult ProductUnit(string relId)
        {
            return View();
        }

        /// <summary>
        /// Lấy thông tin phiếu vật tư chỉ định của kho
        /// </summary>
        /// <param name="relId"></param>
        /// <returns></returns>
        public IActionResult GetKfWzUnit(string relId)
        {
            return Content(_relProductWarehouseRepe.FindEntity(p => p.Id == relId && p.OrganizeId == OrganizeId).ToJson());
        }

        /// <summary>
        /// Lấy tất cả đơn vị của vật tư này
        /// </summary>
        /// <param name="relId"></param>
        /// <returns></returns>
        public IActionResult GetUnitByProId(string relId)
        {
            return Content(_wzProductDmnService.GetProductAndUnitByProId(relId, OrganizeId).ToJson());
        }

        /// <summary>
        /// Sửa đơn vị vật tư kho
        /// </summary>
        /// <param name="relId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public IActionResult UpdateKfWzUnit(string relId, string unitId)
        {
            var entity = _relProductWarehouseRepe.FindEntity(p => p.Id == relId && p.OrganizeId == OrganizeId);
            if (entity == null) return Error("Không tìm thấy thông tin vật tư kho đã chỉ định");
            entity.unitId = unitId;
            entity.Modify();
            return _relProductWarehouseRepe.Update(entity) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }
        #endregion

        /// <summary>
        /// Lấy loại vật tư của bộ phận
        /// </summary>
        /// <returns></returns>
        public IActionResult GetBmWzLb()
        {
            var result = _wzProductDmnService.GetBmWzLb(Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(result.ToJson());
        }


        #region Truy vấn vật tư của bộ phận

        /// <summary>
        /// Chế độ xem vật tư bộ phận
        /// </summary>
        /// <returns></returns>
        public IActionResult WarehouseProductQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Lấy vật tư kho
        /// </summary>
        /// <returns></returns>
        public IActionResult GetWarehouseProducts(Pagination pagination, string keyWord, string lb, string kzbz, string wzzt)
        {
            var data = new
            {
                rows = _warehouseDmnService.GetWarehouseProducts(pagination, keyWord, lb, kzbz, wzzt, OrganizeId, Constants.CurrentKf.currentKfId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        #endregion
    }
}