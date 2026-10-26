using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common.Operator;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeLogicController : ControllerBase
    {
        private readonly ISysPatiChargeLogicApp _SysPatiChargeLogicApp;
        private readonly ISysPatiNatureApp _SysPatiNatureApp;
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;

        #region 病人收费算法

        /// <summary>
        /// Lấy thuật toán thu phí bệnh nhân grid Giá trị
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ogrId"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetsfGridJson(Pagination pagination, string keyword, string ogrId)
        {
            var data = new
            {
                rows = _SysPatiChargeLogicApp.GetList(pagination, keyword, ogrId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public ActionResult SysPatiChargeLogic()
        {
            return View();
        }


        public ActionResult SubmitForm(PatiChargeLogicVO PatiChargeLogicVO, string keyValue)
        {
            _SysPatiChargeLogicApp.SubmitForm(PatiChargeLogicVO, keyValue,OrganizeId);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(int keyValue)
        {
            _SysPatiChargeLogicApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// Khi sửa thông tin，Mang đối tượng dòng đã chọn sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _SysPatiChargeLogicApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }


        /// <summary>
        /// Hộp thả xuống tính chất bệnh nhân 
        /// Hộp thả xuống tính chất bệnh nhân của thuật toán thu phí bệnh nhân đã không còn sử dụng，
        /// Phương thức này cũng được sử dụng tại nơi miễn giảm thu phí，Nếu miễn giảm sửa đổi cách hiển thị tính chất bệnh nhân，Không sử dụng phương thức này nữa，Vui lòng nhớ xóa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetbrxzSelect()
        {
            var treeList = _SysPatiNatureApp.GetbrxzSelect("");
            return Content(treeList);
        }

        /// <summary>
        /// Hộp thả xuống loại lớn
        /// </summary>
        /// <returns></returns>
        public ActionResult GetdlSelect()
        {
            var data = _sysChargeCategoryRepo.GetList(this.OrganizeId, zt: "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dlCode;
                treeModel.text = item.dlmc;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// Lấy khoản mục theo loại lớn code
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        public ActionResult GetSFXMItemInfoByDlCode(string keyword, string dlCode)
        {
            var data = _SysPatiChargeLogicApp.GetSFXMItemInfoByDlCode(keyword, dlCode, this.OrganizeId);
            return Content(data.ToJson());
        }

        #endregion
    }
}
