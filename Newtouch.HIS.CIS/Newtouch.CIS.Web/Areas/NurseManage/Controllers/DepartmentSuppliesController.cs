using System;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.Common.Web;

using Newtouch.CIS.Compat;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset.Zyypyz;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    [Area("NurseManage")]
    public class DepartmentSuppliesController : OrgControllerBase
    {

        private readonly IWZsldDmnService _wZsldDmnService;

        public DepartmentSuppliesController(IWZsldDmnService wZsldDmnService)
        {
            this._wZsldDmnService = wZsldDmnService;
        }
        // GET: NurseManage/sldAPPlication
        public IActionResult WzSldIndex()
        {
            return View();
        }

        #region Mã đã bỏ
        /// <summary>
        /// đăng kýlĩnhphiếusố
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNewCkzksdh()
        {
            var result = "SLD" + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            return Content(result);
        }

        /// <summary>
        /// dướikéovật tưthông tin
        /// </summary>
        /// <param name="key"></param>
        /// <param name="slksbm"></param>
        /// <returns></returns>

        [HttpGet]
        public IActionResult DepartmentStockListQuery(string key, string slksbm)
        {
            var param = new DepartmentStockListQueryParamDTO
            {
                key = key,
                organizeId = OrganizeId,
                warehouseId = UserIdentity.DepartmentCode,
                zt = "1"
            };
            var result = _wZsldDmnService.DepartmentStockListQuery(param);
            return Content(result.ToJson());
        }


        /// <summary>
        /// dướikéovật tưSố lôLôthông tin
        /// </summary>
        /// <param name="proId">vật tưID</param>
        /// <param name="gysId">Nhà cung cấpID</param>
        /// <param name="deliveryNo">phốigửiphiếusố</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ProductBatchQuery(string proId, string slksbm, string keyword = "")
        {
            return Content(_wZsldDmnService.ProductBatchQuery(proId, UserIdentity.DepartmentCode, OrganizeId, keyword: keyword).ToJson());
        }

        /// <summary>
        /// LấyKho
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDeptByKf(string keyword)
        {
            var list = _wZsldDmnService.GetList(OrganizeId, keyword ?? "");
            return Content(list.ToJson());
        }

        /// <summary>
        /// LấyKhu bệnh
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDeptBy(string keyword)
        {
            var list = _wZsldDmnService.GetDeptList(OrganizeId, keyword ?? "");
            return Content(list.ToJson());
        }
        #endregion

        #region KhoaVật tư
        public IActionResult StorageQuery()
        {
            return View();
        }
        public IActionResult ExpiredStorageQuery()
        {
            ViewBag.OrgId = this.OrganizeId;
            return View();
        }
        
        public IActionResult ProfitAndLoss()
        {
            return View();
        }
        
        
        
        

        /// <summary>
        /// Lấyvật tưLoại
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPatientTreeSelectJson()
        {
            var warehouse = _wZsldDmnService.GetWzTreeSelectJson();
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

        public IActionResult GetWarehouseStorage(Pagination pagination, string ksCode, string keyWord, string lb, string xslkc)
        {
            var list = new
            {
                rows = _wZsldDmnService.GetProductStorage(pagination, ksCode, this.OrganizeId, (keyWord ?? "").Trim(), lb,  xslkc),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        /// <summary>
        /// Theovật tưIDLấymỗiLôTồn kho
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="zt">tạmkhiHiệu lựccủachi tiết  true：Có  false：Không</param>
        /// <returns></returns>
        public IActionResult GetWarehouseStorageDetail(string ks,string proId, string zt)
        {
            return Content(_wZsldDmnService.GetProductStorageDetail(ks, this.OrganizeId, proId, zt).ToJson());
        }

        /// <summary>
        /// SửaTồn khoTrạng thái
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IActionResult UpdateKcxxZt(string mxId, string zt)
        {
            return _wZsldDmnService.UpdateZt(mxId, this.OrganizeId, zt) > 0 ? Success() : Error("Sửa trạng thái tra cứu tồn kho thất bại");
        }
        /// <summary>
        /// Đồng bộvật tưHệ thốngVật tư tiêu hao
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public IActionResult UpdateSyncWz()
        {
            _wZsldDmnService.UpdateSyncWz(this.OrganizeId,this.UserIdentity.UserCode);
            return Success();
        }
        
        /// <summary>
        /// Lấyquakỳvật tư
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetExpiredWarehouseStorage(Pagination pagination, string ksCode, string keyWord, string lb, string xslkc)
        {
            var list = new
            {
                rows = _wZsldDmnService.GetExpiredProductStorage(pagination, ksCode, this.OrganizeId, (keyWord ?? "").Trim(), lb,  xslkc),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion
    }
}