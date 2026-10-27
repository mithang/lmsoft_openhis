using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Common;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.VO;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class DictionariesController : ControllerBase
    {
        private readonly ISysMedicineProfitLossReasonRepo _ISysMedicineProfitLossReasonRepo;
        private readonly ISysDispensingaConfigRepo _ISysDispensingaConfigRepo;
        private readonly ISysYpksfypzEntityRepo _ISysYpksfypzEntityRepo;

        #region Nguyên nhân lãi/lỗ
        /// <summary>
        /// 损益原因视图
        /// </summary>
        /// <returns></returns>
        public IActionResult syyy()
        {
            return View();
        }
        public IActionResult syyyForm()
        {
            return View();
        }
        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        /// <returns></returns>
        public IActionResult GetPagintionGridJson(Pagination pagination, string keyword)
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
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult GetSyyyFormJson(string keyValue)
        {
            return Content(_ISysMedicineProfitLossReasonRepo.FindEntity(keyValue).ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="warningDTO">实体</param>
        /// <param name="keyValue">Id值</param>
        /// <returns></returns>
        public IActionResult SubmitSyyyForm(SysMedicineProfitLossReasonEntity SyyyDTO, string keyValue)
        {
            _ISysMedicineProfitLossReasonRepo.SubmitForm(SyyyDTO, keyValue);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult DeleteSyyyForm(string keyValue)
        {
            _ISysMedicineProfitLossReasonRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }
        #endregion

        #region 发药配置
        /// <summary>
        /// 发药配置视图
        /// </summary>
        /// <returns></returns>
        public IActionResult FypzIndex()
        {
            return View();
        }

        public IActionResult FypzForm()
        {
            return View();
        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        /// <returns></returns>
        public IActionResult GetFypzPagintionGridJson(Pagination pagination, string keyword)
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
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult GetFypzFormJson(string keyValue)
        {
            return Content(_ISysDispensingaConfigRepo.FindEntity(keyValue).ToJson());
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="warningDTO">实体</param>
        /// <param name="keyValue">Id值</param>
        /// <returns></returns>
        public IActionResult SubmitFypzForm(SysDispensingaConfigEntity FypzDTO, string keyValue)
        {
            FypzDTO.OrganizeId = OrganizeId;
            _ISysDispensingaConfigRepo.SubmitForm(FypzDTO, keyValue);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult DeleteFypzForm(string keyValue)
        {
            _ISysDispensingaConfigRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// 获取药库下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetCodeSelectJson(string yjbmjb)
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
        /// 获取药库药房下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetYFYKCodeSelectJson(string yjbmjb)
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
        /// 发药配置视图
        /// </summary>
        /// <returns></returns>
        public IActionResult KsFypzIndex()
        {
            return View();
        }

        public IActionResult KsFypzForm()
        {
            return View();
        }

        /// <summary>
        /// 获取科室
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        /// <returns></returns>
        public IActionResult GetKsFypzGridJson(Pagination pagination, string keyword)
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
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult GetKsFypzFormJson(string keyValue)
        {
            return Content(_ISysYpksfypzEntityRepo.FindEntity(keyValue).ToJson());
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="warningDTO">实体</param>
        /// <param name="keyValue">Id值</param>
        /// <returns></returns>
        public IActionResult SubmitKsFypzForm(SysYpksfypzEntity FypzDTO, string keyValue)
        {
            FypzDTO.OrganizeId = OrganizeId;
            _ISysYpksfypzEntityRepo.SubmitForm(FypzDTO, keyValue);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// 删除科室发药配置
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult DeleteKsFypzForm(string keyValue)
        {
            _ISysYpksfypzEntityRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// 获取科室下拉数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetKsCodeSelectJson(string yjbmjb)
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