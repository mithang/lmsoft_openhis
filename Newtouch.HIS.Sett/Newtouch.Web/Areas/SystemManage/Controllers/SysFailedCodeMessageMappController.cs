using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Người tạo：Trương Đông Lâm
    /// Ngày ghi：2017-09-30 14:54
    /// Mô tả：Cấu hình thông báo lỗi
    /// </summary>
    public class SysFailedCodeMessageMappController : ControllerBase
    {
        private readonly ISysFailedCodeMessageMappRepo _sysFailedCodeMessageMappRepo;

        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        /// <returns></returns>
        public ActionResult GetPagintionGridJson(Pagination pagination, string keyword)
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
        public ActionResult GetFormJson(string keyValue)
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
        public ActionResult SubmitForm(SysFailedCodeMessageMappEntity entity, string keyValue)
        {
            _sysFailedCodeMessageMappRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _sysFailedCodeMessageMappRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}