using System;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Domain.Entity.SystemManage;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class SysChargeItemController : ControllerBase
    {
        private readonly ISysChargeItemRepo _sysChargeItemRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysChargeItemDmnService _sysChargeItemDmnService;
        private readonly ISysChargeItemBaseRepo _sysChargeItemBaseRepo;

        public SysChargeItemController(ISysChargeItemRepo _sysChargeItemRepo
            , ISysOrganizeDmnService sysOrganizeDmnService
            , ISysChargeItemDmnService sysChargeItemDmnService,
                ISysChargeItemBaseRepo sysChargeItemBaseRepo
            )
        {
            this._sysChargeItemRepo = _sysChargeItemRepo;
            this._SysOrganizeDmnService = sysOrganizeDmnService;
            this._sysChargeItemDmnService = sysChargeItemDmnService;
            this._sysChargeItemBaseRepo = sysChargeItemBaseRepo;
            _sysChargeItemBaseRepo = sysChargeItemBaseRepo;

        }

        public IActionResult YbbxblForm()
        {
            return View();
        }
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(Pagination pagination, string organizeId, string keyword,string sfdl,string zt)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            if (!_SysOrganizeDmnService.IsMedicalOrganize(organizeId) && !"*".Equals(organizeId))
            {
                return Content(new
                {
                    rows = new List<SysChargeItemVO>(),
                    total = 0,
                    page = pagination.page,
                    records = 0,
                }.ToJson());
            }

            if ("*".Equals(organizeId))
            {
                var list = _sysChargeItemDmnService.GetPagintionList(organizeId, pagination, sfdl, zt,keyword);
                var data = new
                {
                    rows = list,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records
                };
                return Content(data.ToJson());
                
            }
            else
            {
                var list = _sysChargeItemDmnService.GetPagintionList(organizeId, pagination, sfdl, zt,keyword);
                var data = new
                {
                    rows = list,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records
                };
                return Content(data.ToJson());
            }
        }
        
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _sysChargeItemRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysChargeItemEntity entity, int? keyValue)
        {
            entity.ssbz = entity.ssbz == "true" ? "1" : "0";
            entity.tsbz = entity.tsbz == "true" ? "1" : "0";
            entity.sfbz = entity.sfbz == "true" ? "1" : "0";
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId) && !"*".Equals(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId)&& !"*".Equals(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
            }

            if ("*".Equals(entity.OrganizeId))
            {
                _sysChargeItemBaseRepo.SubmitForm(entity.ToJson().ToObject<SysChargeItemBaseEntity>(), keyValue);
            }
            else
            {
                _sysChargeItemRepo.SubmitForm(entity, keyValue);
            }
            return Success("Thao tác thành công.");
        }

        
        public IActionResult UpdateZt(int sfxmId,string zt,string organizeId)
        {
            
            if (string.IsNullOrWhiteSpace(OrganizeId) && !"*".Equals( organizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(OrganizeId)&& !"*".Equals(organizeId))
            {
                throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
            }
            if ("*".Equals( organizeId))
            {
                var entity = _sysChargeItemBaseRepo.FindEntity(p => p.sfxmId == sfxmId);
                entity.zt= zt;
                entity.LastModifyTime = DateTime.Now;
                _sysChargeItemBaseRepo.Update(entity.ToJson().ToObject<SysChargeItemBaseEntity>());
            }
            else
            {
                var entity = _sysChargeItemRepo.FindEntity(p => p.sfxmId == sfxmId);
                entity.zt= zt;
                entity.LastModifyTime = DateTime.Now;
                _sysChargeItemRepo.Update(entity);
            }
            return Success("Thao tác thành công.");
        }
        
        
        /// <summary>
        /// Đồng bộ BHYT hạng mục thu phí
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult YibaoUploadApi(int sfxmId, string flag)
        {
            if (sfxmId <= 0)
            {
                return Success("Vui lòng chọn hạng mục cần đồng bộ!");
            }
            var sfxmEntity = _sysChargeItemRepo.GetForm(sfxmId);
            if (!string.IsNullOrWhiteSpace(sfxmEntity.ybdm))
            {
                var Result = newtouchyibao.YibaoUniteInterface.Mcatalogrl("Z092", "1", sfxmEntity.ybdm, sfxmEntity.sfxmCode, sfxmEntity.sfxmmc, null, null, null, null, sfxmEntity.dj, flag);
                if (Result.Code == 0 && Result.Data.Count > 0 && Result.Data[0].TPCODE == 0)
                {
                    string error = "";
                    if (!_sysChargeItemRepo.YibaoUpload(sfxmId, out error))
                    {
                        return Error(error);
                    }
                    return Success(string.Format("[{0}]Đồng bộ BHYT thành công!", sfxmEntity.sfxmmc));
                }
                else
                {
                    return Error(string.Format("[{0}]Đồng bộ BHYT thất bại:{1}", sfxmEntity.sfxmmc, Result.ErrorMsg));
                }
            }
            else
            {
                return Success("Thiếu mã BHYT!");
            }
        }
        /// <summary>
        /// Lấy tập hợp tỷ lệ hoàn trả BHYT
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult Getybbxbldata(string keyValue)
        {
            var list = _sysChargeItemDmnService.Getybbxbldata(keyValue, this.OrganizeId);
            return Content(list.ToJson());
        }
        /// <summary>
        /// Lưu cài đặt tỷ lệ hoàn trả BHYT
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="xmbm"></param>
        /// <param name="xmmc"></param>
        /// <returns></returns>
        public IActionResult SaveYbblValue(List<Sh_YbfyxzblEntity> entity, string xmbm,string xmmc)
        {
            _sysChargeItemDmnService.SaveYbblValue(entity, xmbm, xmmc, this.OrganizeId,this.UserIdentity.rygh);
            return Success();
        }
    }
}