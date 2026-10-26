using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.VO;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class DictionariesController : ControllerBase
    {
        private readonly ISysMedicineProfitLossReasonRepo _ISysMedicineProfitLossReasonRepo;
        private readonly ISysDispensingaConfigRepo _ISysDispensingaConfigRepo;
        private readonly ISysYpksfypzEntityRepo _ISysYpksfypzEntityRepo;

        #region 损益原因
        /// <summary>
        /// nguyên nhân lãi/lỗ khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult syyy()
        {
            return View();
        }
        public ActionResult syyyForm()
        {
            return View();
        }
        /// <summary>
        /// lấy phần trang thực thể danh sách
        /// </summary>
        /// <param name="pagination">phần trang</param>
        /// <param name="keyword">lọc từ khóa</param>
        /// <returns></returns>
        public ActionResult GetPagintionGridJson(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _ISysMedicineProfitLossReasonRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// lấy thực thể
        /// </summary>
        /// <param name="keyValue">giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetSyyyFormJson(string keyValue)
        {
            return Content(_ISysMedicineProfitLossReasonRepo.FindEntity(keyValue).ToJson());
        }

        /// <summary>
        /// lưu bảng đơn (thêm mới, sửa)
        /// </summary>
        /// <param name="warningDTO">thực thể</param>
        /// <param name="keyValue">Id giá trị</param>
        /// <returns></returns>
        public ActionResult SubmitSyyyForm(SysMedicineProfitLossReasonEntity SyyyDTO, string keyValue)
        {
            _ISysMedicineProfitLossReasonRepo.SubmitForm(SyyyDTO, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteSyyyForm(string keyValue)
        {
            _ISysMedicineProfitLossReasonRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
        #endregion

        #region 发药配置
        /// <summary>
        /// phát thuốc cấu hình khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult FypzIndex()
        {
            return View();
        }

        public ActionResult FypzForm()
        {
            return View();
        }

        /// <summary>
        /// lấy phần trang thực thể danh sách
        /// </summary>
        /// <param name="pagination">phần trang</param>
        /// <param name="keyword">lọc từ khóa</param>
        /// <returns></returns>
        public ActionResult GetFypzPagintionGridJson(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _ISysDispensingaConfigRepo.GetPagintionList(pagination, keyword, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        /// <summary>
        /// lấy thực thể
        /// </summary>
        /// <param name="keyValue">giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFypzFormJson(string keyValue)
        {
            return Content(_ISysDispensingaConfigRepo.FindEntity(keyValue).ToJson());
        }
        /// <summary>
        /// lưu bảng đơn (thêm mới, sửa)
        /// </summary>
        /// <param name="warningDTO">thực thể</param>
        /// <param name="keyValue">Id giá trị</param>
        /// <returns></returns>
        public ActionResult SubmitFypzForm(SysDispensingaConfigEntity FypzDTO, string keyValue)
        {
            FypzDTO.OrganizeId = OrganizeId;
            _ISysDispensingaConfigRepo.SubmitForm(FypzDTO, keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteFypzForm(string keyValue)
        {
            _ISysDispensingaConfigRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// lấy kho thuốc hộp thả xuống dữ liệu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCodeSelectJson(string yjbmjb)
        {
            var data = _ISysDispensingaConfigRepo.GetCodeName(OrganizeId, Convert.ToInt32(yjbmjb) );
            var treeList = new List<TreeSelectModel>
            {
                new TreeSelectModel{
                id="",
                text="未选择"}
            };
            if (data != null)
            {
                foreach (CodeNameVO item in data)
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.code;
                    treeModel.text = item.name;
                    treeList.Add(treeModel);

                }
                return Content(treeList.TreeSelectJson(null));
            }
            else
            {
                return Content("");
            }
        }

        /// <summary>
        /// lấy kho thuốc nhà thuốc hộp thả xuống dữ liệu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetYFYKCodeSelectJson(string yjbmjb)
        {
            var data = _ISysDispensingaConfigRepo.GetYFYKCodeName(OrganizeId);
            var treeList = new List<TreeSelectModel>
            {
                new TreeSelectModel{
                id="",
                text="未选择"}
            };
            if (data != null)
            {
                foreach (CodeNameVO item in data)
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.code;
                    treeModel.text = item.name;
                    treeList.Add(treeModel);

                }
                return Content(treeList.TreeSelectJson(null));
            }
            else
            {
                return Content("");
            }
        }
        #endregion

        #region 科室发药配置
        /// <summary>
        /// phát thuốc cấu hình khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult KsFypzIndex()
        {
            return View();
        }

        public ActionResult KsFypzForm()
        {
            return View();
        }

        /// <summary>
        /// lấy khoa
        /// </summary>
        /// <param name="pagination">phần trang</param>
        /// <param name="keyword">lọc từ khóa</param>
        /// <returns></returns>
        public ActionResult GetKsFypzGridJson(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _ISysYpksfypzEntityRepo.GetPagintionList(pagination, keyword, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        /// <summary>
        /// lấy thực thể
        /// </summary>
        /// <param name="keyValue">giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetKsFypzFormJson(string keyValue)
        {
            return Content(_ISysYpksfypzEntityRepo.FindEntity(keyValue).ToJson());
        }
        /// <summary>
        /// lưu bảng đơn (thêm mới, sửa)
        /// </summary>
        /// <param name="warningDTO">thực thể</param>
        /// <param name="keyValue">Id giá trị</param>
        /// <returns></returns>
        public ActionResult SubmitKsFypzForm(SysYpksfypzEntity FypzDTO, string keyValue)
        {
            FypzDTO.OrganizeId = OrganizeId;
            _ISysYpksfypzEntityRepo.SubmitForm(FypzDTO, keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// xóa khoa phát thuốc cấu hình
        /// </summary>
        /// <param name="keyValue">giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteKsFypzForm(string keyValue)
        {
            _ISysYpksfypzEntityRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// lấy khoa thả xuống dữ liệu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetKsCodeSelectJson(string yjbmjb)
        {
            var data = _ISysYpksfypzEntityRepo.GetCodeName(OrganizeId, Convert.ToInt32(yjbmjb));
            var treeList = new List<TreeSelectModel>
            {
                new TreeSelectModel{
                id="",
                text="未选择"}
            };
            if (data != null)
            {
                foreach (CodeNameVO item in data)
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.code;
                    treeModel.text = item.name;
                    treeList.Add(treeModel);

                }
                return Content(treeList.TreeSelectJson(null));
            }
            else
            {
                return Content("");
            }
        }
        #endregion
    }
}