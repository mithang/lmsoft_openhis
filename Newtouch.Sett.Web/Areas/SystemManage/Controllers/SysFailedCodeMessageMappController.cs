using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    /// <summary>
    /// tạo lập：Trương Đông Lâm
    /// ngày kỳ：2017-09-30 14:54
    /// mô tả kể：saisainêuhiển thịphốiđặt
    /// </summary>
    [Area("SystemManage")]

    public class SysFailedCodeMessageMappController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysFailedCodeMessageMappRepo _sysFailedCodeMessageMappRepo;
        public SysFailedCodeMessageMappController(ISysFailedCodeMessageMappRepo sysFailedCodeMessageMappRepo)
        {
            _sysFailedCodeMessageMappRepo = sysFailedCodeMessageMappRepo;
        }


        /// <summary>
        /// đượclấyphântrangthựcthểcộtbảng
        /// </summary>
        /// <param name="pagination">phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        /// <returns></returns>
        public IActionResult GetPagintionGridJson(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _sysFailedCodeMessageMappRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _sysFailedCodeMessageMappRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysFailedCodeMessageMappEntity entity, string keyValue)
        {
            _sysFailedCodeMessageMappRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _sysFailedCodeMessageMappRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }

    }
}
