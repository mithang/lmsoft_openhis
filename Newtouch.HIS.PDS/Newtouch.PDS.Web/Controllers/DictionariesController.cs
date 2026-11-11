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
using System.Linq;
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
        /// Khung nhìn lý do lãi lỗ
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
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Lọc từ khóa</param>
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
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult GetSyyyFormJson(string keyValue)
        {
            return Content(_ISysMedicineProfitLossReasonRepo.FindEntity(p => p.syyyId == keyValue).ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="warningDTO">Thực thể</param>
        /// <param name="keyValue">Id giá trị</param>
        /// <returns></returns>
        // G35-F1: chan ghi qua GET + CSRF; F3: validate loi TV; F4: guard edit; F5: repo tra so dong.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSyyyForm(SysMedicineProfitLossReasonEntity SyyyDTO, string keyValue)
        {
            if (!ModelState.IsValid) return Error("Dữ liệu nhập không hợp lệ, vui lòng kiểm tra lại");
            var isCreate = string.IsNullOrEmpty(keyValue);
            if (isCreate || SyyyDTO.syyy != null)
            {
                if (string.IsNullOrWhiteSpace(SyyyDTO.syyy)) return Error("Vui lòng nhập lý do lãi lỗ");
                if (SyyyDTO.syyy.Length > 100) return Error("Lý do lãi lỗ tối đa 100 ký tự");
            }
            if (isCreate || SyyyDTO.sybz != null)
            {
                if (string.IsNullOrWhiteSpace(SyyyDTO.sybz)) return Error("Vui lòng chọn loại lãi lỗ");
                if (SyyyDTO.sybz != "0" && SyyyDTO.sybz != "1") return Error("Loại lãi lỗ không hợp lệ");
            }
            if (!isCreate && _ISysMedicineProfitLossReasonRepo.FindEntity(p => p.syyyId == keyValue) == null)
                return Error("Không tìm thấy bản ghi cần sửa");
            var rows = _ISysMedicineProfitLossReasonRepo.SubmitForm(SyyyDTO, keyValue);
            if (rows <= 0) return Error("Không tìm thấy bản ghi cần sửa");
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSyyyForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue)) return Error("Vui lòng chọn một bản ghi");
            var rows = _ISysMedicineProfitLossReasonRepo.DeleteForm(keyValue);
            if (rows <= 0) return Error("Không tìm thấy bản ghi cần xóa");
            return Success("Thao tác thành công。");
        }
        #endregion

        #region Cấu hình phát thuốc
        /// <summary>
        /// Khung nhìn cấu hình phát thuốc
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
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Lọc từ khóa</param>
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
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult GetFypzFormJson(string keyValue)
        {
            return Content(_ISysDispensingaConfigRepo.FindEntity(p => p.Id == keyValue).ToJson());
        }
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="warningDTO">Thực thể</param>
        /// <param name="keyValue">Id giá trị</param>
        /// <returns></returns>
        // G35-F1/F3/F4/F5: nhu SubmitSyyyForm + validate ma kho + chong trung cap + guard org.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitFypzForm(SysDispensingaConfigEntity FypzDTO, string keyValue)
        {
            if (!ModelState.IsValid) return Error("Dữ liệu nhập không hợp lệ, vui lòng kiểm tra lại");
            var isCreate = string.IsNullOrEmpty(keyValue);
            if (isCreate || FypzDTO.ykCode != null)
            {
                if (string.IsNullOrWhiteSpace(FypzDTO.ykCode)) return Error("Vui lòng chọn kho thuốc");
                if (FypzDTO.ykCode.Length > 50) return Error("Mã kho thuốc tối đa 50 ký tự");
                if (!IsYfbmCode(FypzDTO.ykCode)) return Error("Kho thuốc không tồn tại");
            }
            if (isCreate || FypzDTO.yfCode != null)
            {
                if (string.IsNullOrWhiteSpace(FypzDTO.yfCode)) return Error("Vui lòng chọn nhà thuốc");
                if (FypzDTO.yfCode.Length > 50) return Error("Mã nhà thuốc tối đa 50 ký tự");
                if (!IsYfbmCode(FypzDTO.yfCode)) return Error("Nhà thuốc không tồn tại");
            }
            string ykCode = FypzDTO.ykCode, yfCode = FypzDTO.yfCode;
            if (isCreate)
            {
                FypzDTO.OrganizeId = OrganizeId;
            }
            else
            {
                var existing = _ISysDispensingaConfigRepo.FindEntity(p => p.Id == keyValue);
                if (existing == null) return Error("Không tìm thấy bản ghi cần sửa");
                if (existing.OrganizeId != OrganizeId) return Error("Không có quyền sửa bản ghi của tổ chức khác");
                if (ykCode == null) ykCode = existing.ykCode;
                if (yfCode == null) yfCode = existing.yfCode;
                FypzDTO.OrganizeId = existing.OrganizeId; // giu nguyen org goc, khong viet lai (G35 3h)
            }
            var dup = isCreate
                ? _ISysDispensingaConfigRepo.FindEntity(p => p.ykCode == ykCode && p.yfCode == yfCode && p.OrganizeId == FypzDTO.OrganizeId)
                : _ISysDispensingaConfigRepo.FindEntity(p => p.ykCode == ykCode && p.yfCode == yfCode && p.OrganizeId == FypzDTO.OrganizeId && p.Id != keyValue);
            if (dup != null) return Error("Cấu hình phát thuốc đã tồn tại");
            var rows = _ISysDispensingaConfigRepo.SubmitForm(FypzDTO, keyValue);
            if (rows <= 0) return Error("Không tìm thấy bản ghi cần sửa");
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFypzForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue)) return Error("Vui lòng chọn một bản ghi");
            var rows = _ISysDispensingaConfigRepo.DeleteForm(keyValue, OrganizeId);
            if (rows <= 0) return Error("Không tìm thấy bản ghi cần xóa");
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// Lấy dữ liệu hộp thả xuống kho thuốc
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
                text="Chưa chọn"}
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
        /// Lấy dữ liệu hộp thả xuống kho thuốc nhà thuốc
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
                text="Chưa chọn"}
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

        #region Cấu hình phát thuốc cho khoa
        /// <summary>
        /// Khung nhìn cấu hình phát thuốc
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
        /// Lấy khoa
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Lọc từ khóa</param>
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
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult GetKsFypzFormJson(string keyValue)
        {
            return Content(_ISysYpksfypzEntityRepo.FindEntity(p => p.Id == keyValue).ToJson());
        }
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="warningDTO">Thực thể</param>
        /// <param name="keyValue">Id giá trị</param>
        /// <returns></returns>
        // G35-F1/F3/F4/F5: nhu SubmitFypzForm (ma khoa qua GetCodeName, ma nha thuoc qua GetYFYKCodeName).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitKsFypzForm(SysYpksfypzEntity FypzDTO, string keyValue)
        {
            if (!ModelState.IsValid) return Error("Dữ liệu nhập không hợp lệ, vui lòng kiểm tra lại");
            var isCreate = string.IsNullOrEmpty(keyValue);
            if (isCreate || FypzDTO.ksCode != null)
            {
                if (string.IsNullOrWhiteSpace(FypzDTO.ksCode)) return Error("Vui lòng chọn khoa");
                if (FypzDTO.ksCode.Length > 50) return Error("Mã khoa tối đa 50 ký tự");
                if (!IsDepartmentCode(FypzDTO.ksCode)) return Error("Khoa không tồn tại");
            }
            if (isCreate || FypzDTO.yfCode != null)
            {
                if (string.IsNullOrWhiteSpace(FypzDTO.yfCode)) return Error("Vui lòng chọn nhà thuốc");
                if (FypzDTO.yfCode.Length > 50) return Error("Mã nhà thuốc tối đa 50 ký tự");
                if (!IsYfbmCode(FypzDTO.yfCode)) return Error("Nhà thuốc không tồn tại");
            }
            string ksCode = FypzDTO.ksCode, yfCode = FypzDTO.yfCode;
            if (isCreate)
            {
                FypzDTO.OrganizeId = OrganizeId;
            }
            else
            {
                var existing = _ISysYpksfypzEntityRepo.FindEntity(p => p.Id == keyValue);
                if (existing == null) return Error("Không tìm thấy bản ghi cần sửa");
                if (existing.OrganizeId != OrganizeId) return Error("Không có quyền sửa bản ghi của tổ chức khác");
                if (ksCode == null) ksCode = existing.ksCode;
                if (yfCode == null) yfCode = existing.yfCode;
                FypzDTO.OrganizeId = existing.OrganizeId; // giu nguyen org goc, khong viet lai (G35 5h)
            }
            var dup = isCreate
                ? _ISysYpksfypzEntityRepo.FindEntity(p => p.ksCode == ksCode && p.yfCode == yfCode && p.OrganizeId == FypzDTO.OrganizeId)
                : _ISysYpksfypzEntityRepo.FindEntity(p => p.ksCode == ksCode && p.yfCode == yfCode && p.OrganizeId == FypzDTO.OrganizeId && p.Id != keyValue);
            if (dup != null) return Error("Cấu hình phát thuốc cho khoa đã tồn tại");
            var rows = _ISysYpksfypzEntityRepo.SubmitForm(FypzDTO, keyValue);
            if (rows <= 0) return Error("Không tìm thấy bản ghi cần sửa");
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// Xóa cấu hình phát thuốc cho khoa
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteKsFypzForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue)) return Error("Vui lòng chọn một bản ghi");
            var rows = _ISysYpksfypzEntityRepo.DeleteForm(keyValue, OrganizeId);
            if (rows <= 0) return Error("Không tìm thấy bản ghi cần xóa");
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// G35-F3: ma kho/nha thuoc phai ton tai trong xt_yfbm (zt=1) cua org hien tai.
        /// </summary>
        private bool IsYfbmCode(string code)
        {
            var list = _ISysDispensingaConfigRepo.GetYFYKCodeName(OrganizeId);
            return list != null && list.Any(x => x.code == code);
        }

        /// <summary>
        /// G35-F3: ma khoa phai ton tai trong Sys_Department (zt=1) cua org hien tai.
        /// </summary>
        private bool IsDepartmentCode(string code)
        {
            var list = _ISysYpksfypzEntityRepo.GetCodeName(OrganizeId, 0);
            return list != null && list.Any(x => x.code == code);
        }
        /// <summary>
        /// Lấy dữ liệu thả xuống khoa
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
                text="Chưa chọn"}
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