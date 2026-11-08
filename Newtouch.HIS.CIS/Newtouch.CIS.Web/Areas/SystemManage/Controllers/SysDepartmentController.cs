using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// A6-NX31-002: controller mới — 2 view (BespeakRegisterManage/BespeakRegister.cshtml
    /// và VisitDeptSet/Form.cshtml) gọi /SystemManage/SysDepartment/GetSelectJson nhưng
    /// repo không có SysDepartmentController ở bất kỳ portal nào nên route 404
    /// (D-NX30-CARRY-004). Không sửa 2 view (URL đã đúng mẫu area SystemManage).
    /// Action trả mảng JSON thuần [{Code, Name}] để phục vụ cả 2 caller:
    /// - VisitDeptSet bindSelect: duyệt mảng, lấy data[i][id=Code]/[text=Name]
    ///   (framework-ui.js bindSelect).
    /// - BespeakRegister dataGrid (jqGrid, không pager): jqGrid chấp nhận mảng JSON
    ///   ở cấp cao nhất (jqgrid.source.js addJSONData: drows==null && isArray(data)
    ///   thì drows=data), cột Code/Name.
    /// Không gắn [HttpGet]/[HttpPost]: bindSelect gọi $.najax (mặc định GET),
    /// jqGrid dataGrid mặc định POST — action phải nhận cả 2 verb như các
    /// GetGridJson/GetFormJson anh em. Read-only nên không cần antiforgery token
    /// (đồng nhất với các GetSelectJson khác trong repo).
    /// </summary>
    [Area("SystemManage")]
    public class SysDepartmentController : OrgControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;

        public SysDepartmentController(ISysDepartmentRepo sysDepartmentRepo)
        {
            _sysDepartmentRepo = sysDepartmentRepo;
        }

        /// <summary>
        /// Danh sách khoa của tổ chức (dùng cho dropdown/grid chọn khoa).
        /// </summary>
        /// <param name="organizeId">Mã tổ chức; trống thì lấy org của user hiện tại.</param>
        /// <returns>Mảng JSON [{Code, Name}].</returns>
        public IActionResult GetSelectJson(string organizeId)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = OrganizeId;
            }
            // flag "1" = Khoa (mẫu ClientsDataController: GetList(orgId, "1")).
            // Repo hiện tại bỏ qua flag/keyword, trả mọi khoa zt='1' của org (hoặc '*').
            var data = _sysDepartmentRepo.GetList(organizeId ?? "", "1");
            var list = new List<object>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(new { Code = item.Code, Name = item.Name });
                }
            }
            return Content(list.ToJson());
        }
    }
}
