using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Extension;

using NewtouchHIS.Lib.Base.Model;
using Newtouch.Infrastructure.EF;
using Newtouch.Core.Common.Utils;
using Newtouch.Common;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.PDS.Requset.PharmacyDepartment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class SysMedicineController : ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysMedicineDmnService _sysMedicineDmnService;
        private readonly ISysMedicineBaseDmnService _sysMedicineBaseDmnService;
        private readonly ISysPharmacyDepartmentBaseDmnService _sysPharmacyDepartmentBaseDmnService;
        private readonly ISysPharmacyDepartmentDmnService _sysPharmacyDepartmentDmnService;
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacyDepartmentMedicineRepo;


        public SysMedicineController(
            ISysOrganizeDmnService sysOrganizeDmnService
            , ISysMedicineDmnService sysMedicineDmnService
            ,ISysMedicineBaseDmnService sysMedicineBaseDmnService
            , ISysPharmacyDepartmentBaseDmnService sysPharmacyDepartmentBaseDmnService
            ,ISysPharmacyDepartmentDmnService sysPharmacyDepartmentDmnService
            , ISysPharmacyDepartmentApp sysPharmacyDepartmentApp
            , ISysPharmacyDepartmentMedicineRepo sysPharmacyDepartmentMedicineRepo
            )
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysMedicineDmnService = sysMedicineDmnService;
            this._sysMedicineBaseDmnService = sysMedicineBaseDmnService;
            this._sysPharmacyDepartmentBaseDmnService = sysPharmacyDepartmentBaseDmnService;
            this._sysPharmacyDepartmentDmnService = sysPharmacyDepartmentDmnService;
            this._sysPharmacyDepartmentApp = sysPharmacyDepartmentApp;
            this._sysPharmacyDepartmentMedicineRepo = sysPharmacyDepartmentMedicineRepo;
        }

        public IActionResult SysMedicineAdd()
        {
            return View();
        }

        //grid json Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(Pagination pagination, string keyword, string organizeId, string zt, string ypflCode)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysMedicineDmnService.GetPaginationList(organizeId, pagination, zt, ypflCode, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        #region Gắn giá trị điều khiển thả xuống

        /// <summary>
        /// Lấy danh sách thông tin thuốc theo tổ chức
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetFloatingYPXXJson(string organizeId, string keyword)
        {
            var list = _sysMedicineDmnService.GetYbMedicineList(organizeId, keyword);
            return Json(list);
        }
        public JsonResult GetYbName(string organizeId, string keyword, string lx)
        {
            var list = _sysMedicineDmnService.GetYbName(organizeId, lx, keyword);
            return Json(list);
        }

        public JsonResult GetFloatingYPXNHXXJson(string organizeId, string keyword)
        {
            var list = _sysMedicineDmnService.GetYbXNHMedicineList(keyword);
            return Json(list);
        }

        #endregion

        /// <summary>
        /// Gửi thông tin dữ liệu
        /// </summary>
        /// <param name="model"></param>
        /// <param name="p"></param>
        /// <param name="ypCode"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult submitForm(SysMedicineVO model, List<string> p, string ypCode, int? keyValue)
        {
            model.zt = model.zt == "true" ? "1" : "0";
            model.ycmc = model.ycmc ?? "";
            //model.ybbz = model.ybbz == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(model.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }

            if (!_sysOrganizeDmnService.IsMedicalOrganize(model.OrganizeId) && !model.OrganizeId.Equals("*"))
            {
                throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
            }
            //G39-F10: base: field rỗng → binder map thành null → lỗi SQL thô "Cannot insert NULL into
            //column 'zfxz'/'djdw'/'ypmc'". Validate server-side 8 field string NOT NULL của xt_yp
            //(nhãn đúng form Views/SysMedicine/SysMedicineAdd.cshtml; ycmc đã default "" ở trên,
            //mzzybz default "1" ở UpdateEnityProperties, số bzs/lsj/zfbl non-nullable nên không null được).
            if (string.IsNullOrWhiteSpace(model.ypCode))
            {
                throw new FailedException("Vui lòng nhập mã thuốc.");
            }
            if (string.IsNullOrWhiteSpace(model.ypmc))
            {
                throw new FailedException("Vui lòng nhập tên thuốc.");
            }
            if (string.IsNullOrWhiteSpace(model.py))
            {
                throw new FailedException("Vui lòng nhập chữ đầu pinyin.");
            }
            if (string.IsNullOrWhiteSpace(model.dlCode))
            {
                throw new FailedException("Vui lòng chọn nhóm lớn.");
            }
            if (string.IsNullOrWhiteSpace(model.zfxz))
            {
                throw new FailedException("Vui lòng chọn tính chất tự trả.");
            }
            if (string.IsNullOrWhiteSpace(model.djdw))
            {
                throw new FailedException("Vui lòng nhập đơn vị định giá.");
            }
            if (string.IsNullOrWhiteSpace(model.bzdw))
            {
                throw new FailedException("Vui lòng nhập kho thuốc.");
            }
            if (string.IsNullOrWhiteSpace(model.zxdw))
            {
                throw new FailedException("Vui lòng nhập đơn vị nhỏ nhất.");
            }
            _sysMedicineDmnService.SubmitMedicine(model, keyValue);
            var t = SubmitEmpowermentPharmacyDepartment(p, ypCode, model.OrganizeId, keyValue);
            var msg = string.IsNullOrWhiteSpace(t) ? "Thao tác thành công." : ("Lưu thông tin thuốc thành công,nhưng ủy quyền nhà thuốc/kho thuốc thất bại," + t);
            return Success(msg);
        }

        /// <summary>
        /// Ủy quyền nhà thuốc/kho thuốc cho thuốc vừa lưu (PDS DTO API nội bộ).
        /// Khác HOSP: HOSP gọi SubmitEmpowermentPharmacyDepartment qua PDS API từ xa
        /// (SitePdsApiHelper); PDS gọi thẳng ISysPharmacyDepartmentApp nội bộ —
        /// cùng impl mà PDS.API/PharmacyDepartmentController đang dùng live.
        /// Thêm mới (keyValue null): EmpowermentPharmacyDepartment từng kho;
        /// sửa (keyValue có): EmpowermentPharmacyDepartmentAndRemoveOld (xóa hết rồi cấp lại).
        /// View trả false khi không chọn kho nào (AssembleEmpowermentPharmacyDepartment)
        /// nên phải lọc bỏ "false" — model binder chuyển thành ["false"].
        /// </summary>
        private string SubmitEmpowermentPharmacyDepartment(List<string> p, string ypCode, string organizeId, int? keyValue)
        {
            var epds = (p ?? new List<string>())
                .Where(x => !string.IsNullOrWhiteSpace(x) && !x.Equals("false", StringComparison.OrdinalIgnoreCase))
                .Distinct().ToList();
            if (epds.Count == 0 || string.IsNullOrWhiteSpace(ypCode))
            {
                return "";
            }
            if (keyValue == null)
            {
                var result = new StringBuilder();
                foreach (var code in epds)
                {
                    result.Append(_sysPharmacyDepartmentApp.EmpowermentPharmacyDepartment(new EmpowermentPharmacyDepartmentRequestDto
                    {
                        bmypId = Guid.NewGuid().ToString(),
                        yfbmCode = code,
                        Ypdm = ypCode,
                        OrganizeId = organizeId,
                        Ypkw = "",
                        Zcxh = "",
                        px = null,
                        Pxfs1 = "",
                        Pxfs2 = "",
                        Kcsx = 0,
                        Kcxx = 0,
                        Jhd = 0,
                        Jhl = 0,
                        Ypsxdm = null,
                        Sysx = 0,
                        Ylsx = 0,
                        zt = "1",
                        CreateTime = DateTime.Now,
                        CreatorCode = UserIdentity.UserCode,
                        LastModifierCode = "",
                        LastModifyTime = null,
                        Timestamp = DateTime.Now
                    }));
                }
                return result.ToString();
            }
            var request = new EmpowermentPharmacyDepartmentAndRemoveOldRequestDto
            {
                epds = epds.Select(code => new EmpowermentPharmacyDepartment
                {
                    bmypId = Guid.NewGuid().ToString(),
                    yfbmCode = code,
                    Ypdm = ypCode,
                    OrganizeId = organizeId,
                    Ypkw = "",
                    Zcxh = "",
                    px = null,
                    Pxfs1 = "",
                    Pxfs2 = "",
                    Kcsx = 0,
                    Kcxx = 0,
                    Jhd = 0,
                    Jhl = 0,
                    Ypsxdm = null,
                    Sysx = 0,
                    Ylsx = 0,
                    zt = "1",
                    CreateTime = DateTime.Now,
                    CreatorCode = UserIdentity.UserCode,
                    LastModifierCode = "",
                    LastModifyTime = null
                }).ToList(),
                Timestamp = DateTime.Now
            };
            return _sysPharmacyDepartmentApp.EmpowermentPharmacyDepartmentAndRemoveOld(request);
        }

        /// <summary>
        /// Khi sửa thông tin,Mang thông tin tới trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(int keyValue)
        {
            var data = _sysMedicineDmnService.GetFormJson(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Đồng bộ BHYT thông tin thuốc
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        //[HttpPost]
        //[HandlerAjaxOnly]
        //public IActionResult YibaoUploadApi(int ypId, string flag)
        //{
        //    if (ypId <= 0)
        //    {
        //        return Success("Vui lòng chọn thuốc cần đồng bộ!");
        //    }
        //    var ypEntity = _sysMedicineDmnService.GetFormJson(ypId);
        //    if (!string.IsNullOrWhiteSpace(ypEntity.ybdm))
        //    {
        //        var Result = newtouchyibao.YibaoUniteInterface.Mcatalogrl("Z092", "0", ypEntity.ybdm, ypEntity.ypCode, ypEntity.ypmc, ypEntity.jxmc, ypEntity.ycmc, ypEntity.ypgg, ypEntity.bzdw, ypEntity.lsj, flag);
        //        if (Result.Code == 0 && Result.Data.Count > 0 && Result.Data[0].TPCODE == 0)
        //        {
        //            string error = "";
        //            if (!_sysMedicineDmnService.YibaoUpload(ypId, out error))
        //            {
        //                return Error(error);
        //            }
        //            return Success(string.Format("[{0}]Đồng bộ BHYT thành công!", ypEntity.ypmc));
        //        }
        //        else
        //        {
        //            return Error(string.Format("[{0}]Đồng bộ BHYT thất bại:{1}", ypEntity.ypmc, Result.ErrorMsg));
        //        }
        //    }
        //    else
        //    {
        //        return Success("Thiếu mã BHYT!");
        //    }
        //}

        public IActionResult GetPcSelectJson()
        {
            var data = new List<object>();
            List<SysMedicalOrderFrequencyVO> entityList = _sysMedicineBaseDmnService.GetOrderFrequencyList(this.OrganizeId);
            foreach (var item in entityList)
            {
                var obj = new
                {
                    id = item.yzpcCode,
                    text = item.yzpcmc
                };
                data.Add(obj);
            }
            return Content(data.ToJson());
        }
        /// <summary>
        /// Lấy thông tin kháng sinh
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult GetKssById(string Id)
        {
            SysMedicineAntibioticInfoVO entity = _sysMedicineBaseDmnService.GetKssInfo(Id, OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu thông tin kháng sinh
        /// </summary>
        /// <param name="AntibioticInfo"></param>
        /// <returns></returns>
        //public IActionResult SubmitKssForm(SysMedicineAntibioticInfoEntity AntibioticInfo)
        //{
        //    AntibioticInfo.OrganizeId = OrganizeId;
        //    AntibioticInfo.zt = "1";
        //    AntibioticInfo.LastModifierCode = UserIdentity.UserCode;
        //    AntibioticInfo.LastModifyTime = DateTime.Now;
        //    var getId = _sysMedicineAntibioticInfoRepo.SubmitForm(AntibioticInfo);
        //    return string.IsNullOrWhiteSpace(getId) ? Error("Lưu kháng sinh thất bại") : Success("", getId);
        //}

        #region Tự động ủy quyền nhà thuốc kho thuốc


        /// <summary>
        /// Lấy tất cả nhà thuốc kho thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllYfbm()
        {
            var result = _sysPharmacyDepartmentBaseDmnService.GetEffectiveList(OrganizeId, null);
            return Success("", result.ToJson());
        }

        /// <summary>
        /// Lấy nhà thuốc kho thuốc thuốc này đã ủy quyền
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        public IActionResult GetEmpowermentPharmacyDepartment(string ypId)
        {
            var result = _sysPharmacyDepartmentDmnService.SelectEmpowermentYfbmByYp(ypId, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Gửi ủy quyền nhà thuốc kho thuốc
        /// </summary>
        /// <param name="p"></param>
        /// <param name="ypCode">Mã thuốc</param>
        /// <param name="keyValue">Thuốc ID</param>
        /// <returns></returns>
        //public IActionResult SubmitEmpowermentPharmacyDepartment(List<string> p, string ypCode, int? keyValue)
        //{
        //    var result = _sysPharmacyDepartmentApp.SubmitEmpowermentPharmacyDepartment(keyValue, ypCode, OrganizeId, UserIdentity.UserCode, p);
        //    return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        //}

        public IActionResult GetItemDetailsByCode(string name)
        {
            string result = _sysMedicineDmnService.GetTradePricePlus(name);
            return Content(result); ;
        }

        #endregion


        //Lớp nổi thả xuống cách dùng thuốc
        public IActionResult GetUsageFloat(string keyword = null)
        {
            var list = _sysMedicineDmnService.GetUsageFloat(keyword);
            return Content(list.ToJson());
        }


        #region baseDữ liệu cơ bản

        /// <summary>
        /// Truy vấn phân loại thuốc theo từ khóa
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetListSelectJson(string keyword)
        {
            var list = _sysMedicineBaseDmnService.GetValidList(keyword);
            return Json(list);
        }

        //Gắn dữ liệu hộp thả xuống
        public IActionResult GetSelectJson(string code)
        {
            var data = _sysMedicineBaseDmnService.GetValidListByItemCode(code, "");
            var list = new List<object>();
            foreach (SysItemsDetailVO item in data)
            {
                list.Add(new { id = item.Code, text = item.Name });
            }
            return Content(list.ToJson());
        }

        //Cây loại thu phí lớn(Bệnh viện) Thả xuống Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeSelectJson(string organizeId, string treeidFieldName = "Code")
        {
            var data = _sysMedicineBaseDmnService.GetsfdlValidList(organizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                if (treeidFieldName == "Code")
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlCode;
                    treeModel.text = item.dlmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                        ? data.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault()
                        : null;
                    treeList.Add(treeModel);
                }
                else
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlId.ToString();
                    treeModel.text = item.dlmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0) ? item.ParentId.ToString() : null;
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeSelectJson(null));
        }

        public JsonResult GetypjxListSelectJson(string keyword)
        {
            var list = _sysMedicineBaseDmnService.GetypjxValidList(keyword);
            return Json(list);
        }

        /// <summary>
        /// Lấy danh sách đơn vị thuốc
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IActionResult GetypdwListSelectJson(string keyword)
        {
            var data = _sysMedicineBaseDmnService.GetypdwValidList(keyword);
            return Json(data);
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GettsypbzSelectJson(string code, string keyword, string OrganizeId)
        {
            List<object> list = new List<object>();
            if (code == "tsypbz")
            {
                foreach (var item in System.Enum.GetValues(typeof(EnumYpsx)))
                {
                    object name = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                    list.Add(new { id = (int)item, text = (name as DescriptionAttribute).Description });
                }
                return Content(list.ToJson());
            }
            var data = _sysMedicineBaseDmnService.GetValidListByItemCode(code, keyword, OrganizeId);
            foreach (SysItemsDetailVO item in data)
            {
                list.Add(new { id = item.Code, text = item.Name });
            }
            return Content(list.ToJson());
        }

        #endregion
        
    }
}