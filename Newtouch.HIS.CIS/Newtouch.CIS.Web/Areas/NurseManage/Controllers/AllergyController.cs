using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{

    [Area("NurseManage")]

    public class AllergyController : OrgControllerBase
    {
        private readonly IAllergyManageDmnService _IAllergyManageDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        public AllergyController(IAllergyManageDmnService iAllergyManageDmnService, ISysConfigRepo sysConfigRepo)
        {
            this._IAllergyManageDmnService = iAllergyManageDmnService;
            this._sysConfigRepo = sysConfigRepo;
        }

        #region Chế độ xem
        public IActionResult OutHosAllergyManage()
        {
            return View();
        }

        public IActionResult AllergyForm()
        {
            return View();
        }

        public IActionResult AllergyInfoQuery()
        {
            return View();
        }

        public IActionResult InHosAllergyManage()
        {
            return View();
        }
        #endregion

        #region LấyDữ liệu
        /// <summary>
        /// LấyNgoại trúDanh sách bệnh nhân (chờlàmTest da)
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMzPatientListJson(Pagination pagination, string keyword, string ks, string kssj, string jssj, string zxzt)
        {
            var PsItmeCode = _sysConfigRepo.GetValueByCode("AllergyItemSetting", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(PsItmeCode)) throw new Exception("Vui lòng liên hệ quản trị viên cấu hình mục dị ứng và tham số động, mã tham số: AllergyItemSetting");
            var patList = _IAllergyManageDmnService.GetOutPatientPaginationList(pagination, keyword, ks, kssj, jssj, this.OrganizeId, PsItmeCode, zxzt);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// LấyNhập việnDanh sách bệnh nhân (chờlàmTest da)
        /// </summary>
        /// <returns></returns>
        public IActionResult GetZyPatientListJson(Pagination pagination, string keyword, string ks, string kssj, string jssj, string zxzt)
        {
            var PsItmeCode = _sysConfigRepo.GetValueByCode("AllergyItemSetting", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(PsItmeCode)) throw new Exception("Vui lòng liên hệ quản trị viên cấu hình mục dị ứng và tham số động, mã tham số: AllergyItemSetting");
            var patList = _IAllergyManageDmnService.GetInPatientPaginationList(pagination, keyword, ks, kssj, jssj, this.OrganizeId, PsItmeCode, zxzt);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// LấyNgoại trúBệnh nhânTest daMục
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="mzzybz">1Ngoại trú,2Nhập viện</param>
        /// <returns></returns>
        public IActionResult GetMzPatientPsItemsJson(Pagination pagination, string Id)
        {
            var PsItmeCode = _sysConfigRepo.GetValueByCode("AllergyItemSetting", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(PsItmeCode)) throw new Exception("Vui lòng liên hệ quản trị viên cấu hình mục dị ứng và tham số động, mã tham số: AllergyItemSetting");
            var patList = _IAllergyManageDmnService.GetMzPatientPsItemsList(pagination, Id, this.OrganizeId, PsItmeCode);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// LấyBệnh nhân nội trúTest daMục
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="mzzybz">1Ngoại trú,2Nhập viện</param>
        /// <returns></returns>
        public IActionResult GetZyPatientPsItemsJson(Pagination pagination, string zyh)
        {
            var PsItmeCode = _sysConfigRepo.GetValueByCode("AllergyItemSetting", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(PsItmeCode)) throw new Exception("Vui lòng liên hệ quản trị viên cấu hình mục dị ứng và tham số động, mã tham số: AllergyItemSetting");
            var patList = _IAllergyManageDmnService.GetZyPatientPsItemsList(pagination, zyh, this.OrganizeId, PsItmeCode);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấyquamẫnthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IActionResult GetPatientGmxxListJson(Pagination pagination, string keyword)
        {
            var patList = _IAllergyManageDmnService.GetPatientGmxxList(pagination, keyword, this.OrganizeId);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// LấyKháng sinhThuốcthông tin
        /// </summary>
        /// <returns></returns>
        public IActionResult GetKssYpListJson()
        {
            var list = _IAllergyManageDmnService.GetKssYpListData(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// LấyKháng sinhThuốcLoại
        /// </summary>
        /// <returns></returns>
        public IActionResult GetKssYpLbJson(string ypCode)
        {
            if (string.IsNullOrEmpty(ypCode))
            {
                return Success(null, null);
            }
            var list = _IAllergyManageDmnService.GetKssYpDlData(ypCode, this.OrganizeId);
            return Success(null, list);
        }
        #endregion

        #region Lưu dữ liệu
        public IActionResult SaveAllergyInfo(AllergyEntity postData)
        {
            postData.Id = Guid.NewGuid().ToString();
            postData.OrganizeId = this.OrganizeId; 
            postData.CreatorCode = UserIdentity.UserCode;
            postData.CreatorName = UserIdentity.UserName;
            postData.CreateTime = DateTime.Now;
            postData.zt = "1";

            _IAllergyManageDmnService.SaveAllergyInfo(postData);
            return Success();
        }


        /// <summary>
        /// XóaThực hiệncủaquamẫnthông tin
        /// </summary>
        /// <param name="xmCode"></param>
        /// <returns></returns>
        public IActionResult DeleteExecutedGmxx(string gmxxId)
        {
            int ret = _IAllergyManageDmnService.DeleteAllergyInfo(gmxxId, this.OrganizeId, UserIdentity.UserCode, UserIdentity.UserName);
            return ret > 0 ? Success("Thao tác thành công") : Error("HủyThực hiệnthất bại,Vui lòng liên hệQuản trị viên!");
        }
        #endregion
    }
}