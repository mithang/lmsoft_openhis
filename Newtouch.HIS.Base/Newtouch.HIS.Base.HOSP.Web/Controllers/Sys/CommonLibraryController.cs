using System;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.DomainServices.SystemManage;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class CommonLibraryController : ControllerBase
    {
        
        
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysMedicineRepo _SysMedicineRepository;
        private readonly ISysMedicinePropertyRepo _sysMedicinePropertyRepo;
        private readonly ISysMedicineDmnService _sysMedicineDmnService;
        private readonly ISysMedicalOrderFrequencyRepo _sysMedicalOrderFrequencyRepo;
        private readonly ISysMedicineAntibioticInfoRepo _sysMedicineAntibioticInfoRepo;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly ISysMedicineUsageRepo _sysMedicineUsageRepo;
        private readonly ICommonLibraryDmnService _iCommonLibraryDmnService;
        private readonly ISysMedicineBaseRepo _sysMedicineBaseRepo;
        private readonly ISysMedicinePropertyBaseRepo _sysMedicinePropertyBaseRepo;
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;
        private readonly ISysChargeCategoryBaseRepo _sysChargeCategoryBaseRepo;
        private readonly ISysChargeItemRepo _sysChargeItemRepo;
        private readonly ISysChargeItemBaseRepo _sysChargeItemBaseRepo;
        private readonly ISysMedicalOrderFrequencyRepo _sysMedicalOrderFrequencyApp;
        private readonly ISysChargeCategoryTypeRelationRepo _sysChargeCategoryTypeRelationRepo;
        
        


         public CommonLibraryController(ISysOrganizeDmnService sysOrganizeDmnService
             , ISysMedicineRepo SysMedicineRepository, ISysMedicineDmnService sysMedicineDmnService
             , ISysMedicalOrderFrequencyRepo SysMedicalOrderFrequencyRepo
             , ISysMedicineAntibioticInfoRepo MedicineAntibioticInfoRepo,
             ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo,
             ISysPharmacyDepartmentApp sysPharmacyDepartmentApp, ICommonLibraryDmnService iCommonLibraryDmnService,
             ISysMedicineBaseRepo SysMedicineBaseRepo, ISysMedicinePropertyBaseRepo SysMedicinePropertyBaseRepo,ISysChargeCategoryRepo SysChargeCategoryRepo
             ,ISysChargeCategoryBaseRepo SysChargeCategoryBaseRepo,
             ISysChargeItemRepo SysChargeItemRepo,
             ISysChargeItemBaseRepo SysChargeItemBaseRepo,
             ISysMedicalOrderFrequencyRepo sysMedicalOrderFrequencyApp,
             ISysChargeCategoryTypeRelationRepo sysChargeCategoryTypeRelationRepo,
             ISysMedicinePropertyRepo sysMedicinePropertyRepo
             )
         {
             _sysOrganizeDmnService = sysOrganizeDmnService;
             _SysMedicineRepository = SysMedicineRepository;
             _sysMedicineDmnService = sysMedicineDmnService;
             _sysMedicalOrderFrequencyRepo = SysMedicalOrderFrequencyRepo;
             _sysMedicineAntibioticInfoRepo = MedicineAntibioticInfoRepo;
             _sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
             _sysPharmacyDepartmentApp = sysPharmacyDepartmentApp;
             _iCommonLibraryDmnService = iCommonLibraryDmnService;
             _sysMedicineBaseRepo = SysMedicineBaseRepo;
             _sysMedicinePropertyBaseRepo = SysMedicinePropertyBaseRepo;
             _sysChargeCategoryRepo = SysChargeCategoryRepo;
             _sysChargeCategoryBaseRepo = SysChargeCategoryBaseRepo;
             _sysChargeItemRepo = SysChargeItemRepo;
             _sysChargeItemBaseRepo = SysChargeItemBaseRepo;
             _sysMedicalOrderFrequencyApp = sysMedicalOrderFrequencyApp;
             _sysChargeCategoryTypeRelationRepo = sysChargeCategoryTypeRelationRepo;
             _sysMedicinePropertyRepo = sysMedicinePropertyRepo;
         }
        
        
        
        //grid json Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(Pagination pagination, string keyword, string organizeId,string zxyfl, string zt,string ypflCode,string ypgg ,string ycmc,string dlCode)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            if (!string.IsNullOrEmpty(zxyfl))
            {
                dlCode = zxyfl;
            }
            var data = new
            {
                rows = _iCommonLibraryDmnService.GetPaginationList(organizeId, pagination, zt, ypflCode, keyword,ypgg, ycmc, dlCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        
        
        
        
        
        
        
        
        // /**
        //  * Đồng bộ tất cả thuốc
        //  */
        // [HttpGet]
        // [HandlerAjaxOnly]
        // public IActionResult SyncAllDrug(string organizeId)
        // {
        //     //Mặc định lấy tổ chức của người dùng đang đăng nhập
        //     var userIdentityOrganizeId = UserIdentity.OrganizeId;
        //     var validListByOrg = _sysMedicineBaseRepo.GetValidListByOrg("*");
        //     
        //     foreach (var item in validListByOrg)
        //     {
        //         //Xem đã đồng bộ chưa
        //         var sysMedicineEntity = _SysMedicineRepository.FindEntity(p => p.ypCode == item.ypCode && p.OrganizeId == userIdentityOrganizeId && p.ypmc == item.ypmc);
        //         if (sysMedicineEntity != null)
        //         {
        //             //Đã đồng bộ thì bỏ qua
        //             continue;
        //         }
        //         _iCommonLibraryDmnService.SyncCommonDrug(item,userIdentityOrganizeId);
        //     }
        //     return Success("Đồng bộ thành công!");
        // }
        
        
        
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult Sync(List<int> idList,string organizeId,string Type)
        {
            //Mặc định lấy tổ chức của người dùng đang đăng nhập
            // var userIdentityOrganizeId = UserIdentity.OrganizeId;
                //Thuốc
                foreach (var id in idList)
                {
                    if ("yp".Equals(Type))
                    {   
                        //Lấy thông tin thuốc
                        var s = _sysMedicineBaseRepo.FindEntity(p=>p.ypId == id);
                        var s1 = _sysMedicinePropertyBaseRepo.FindEntity(p => p.ypId == id);
                        //Xem đã đồng bộ chưa
                        var sysMedicineEntity = _SysMedicineRepository.FindEntity(p => p.ycmc == s.ycmc && p.OrganizeId == organizeId && p.ypmc == s.ypmc);
                        var sysMedicineEntityProperty = _sysMedicinePropertyRepo.FindEntity(p =>p.OrganizeId == organizeId && p.gjybdm == s1.gjybdm);
                        if (sysMedicineEntity == null && sysMedicineEntityProperty == null)
                        {
                            _iCommonLibraryDmnService.SyncCommonDrug(s,organizeId);
                        }
                        else
                        {
                            return Error("["+s.ypmc+"]Đã đồng bộ rồi!Vui lòng không tích chọn");
                        }  
                    }else if ("sfdl".Equals(Type))
                    {
                        var s = _sysChargeCategoryBaseRepo.FindEntity(p => p.dlId == id);
                        var sysChargeCategoryEntity = _sysChargeCategoryRepo.FindEntity(p => p.dlCode == s.dlCode && p.OrganizeId == organizeId && p.dlmc == s.dlmc);
                        if (sysChargeCategoryEntity == null)
                        {
                            _iCommonLibraryDmnService.SyncCommonSfdl(s,organizeId);
                            try
                            {
                                //Đồng bộ loại danh mục thu phí lớn thuộc hạng mục đó
                                var sysChargeCategoryTypeRelationEntity = _sysChargeCategoryTypeRelationRepo.FindEntity(p =>
                                    p.dlCode == s.dlCode && p.OrganizeId == "*");
                                sysChargeCategoryTypeRelationEntity.OrganizeId = organizeId;
                                sysChargeCategoryTypeRelationEntity.Id = Guid.NewGuid().ToString();
                                _sysChargeCategoryTypeRelationRepo.Insert(sysChargeCategoryTypeRelationEntity);
                            }
                            catch (Exception e)
                            {
                               continue;
                            }
                        }else
                        {
                            return Error("["+s.dlmc+"]Đã đồng bộ rồi!Vui lòng không tích chọn");
                        }
                        //Nhóm thu phí lớn
                        
                    }else if ("hc".Equals(Type))
                    {
                        //Tạm dùng phí vật tư thuốc dùng chung
                        //Lấy thông tin vật tư
                        var s = _sysMedicineBaseRepo.FindEntity(p=>p.ypId == id);
                        //Xem đã đồng bộ chưa
                        var sysMedicineEntity = _SysMedicineRepository.FindEntity(p => p.ycmc == s.ycmc && p.OrganizeId == organizeId && p.ypmc == s.ypmc);
                        if (sysMedicineEntity == null)
                        {
                            _iCommonLibraryDmnService.SyncCommonDrug(s,organizeId);
                        }
                        else
                        {
                            return Error("["+s.ypmc+"]Đã đồng bộ rồi!Vui lòng không tích chọn");
                        }  
                        
                    }else if ("sfxm".Equals(Type))
                    {
                        //sfxm 
                        var s = _sysChargeItemBaseRepo.FindEntity(p => p.sfxmId == id);
                        //Xem đã đồng bộ chưa
                        var sysChargeItem = _sysChargeItemRepo.FindEntity(p => p.sfxmmc == s.sfxmmc && p.OrganizeId == organizeId && p.sfdlCode == s.sfdlCode);
                        if (sysChargeItem == null)
                        {
                            _iCommonLibraryDmnService.SyncCommonSfxm(s,organizeId);
                        }
                        else
                        {
                            return Error("["+s.sfxmmc+"]Đã đồng bộ rồi!Vui lòng không tích chọn");
                        }  
                    }
                    else if ("yzpc".Equals(Type))
                    {
                        //yzpc
                        var s = _sysMedicalOrderFrequencyApp.FindEntity(p => p.yzpcId == id);
                        //Xem đã đồng bộ chưa
                        var sysChargeItem = _sysMedicalOrderFrequencyApp.FindEntity(p => p.yzpcCode == s.yzpcCode && p.OrganizeId == organizeId && p.yzpcmc == s.yzpcmc);
                        if (sysChargeItem == null)
                        {
                            s.OrganizeId = organizeId;
                            _sysMedicalOrderFrequencyApp.Insert(s);
                        }
                        else
                        {
                            return Error("["+s.yzpcmc+"]Đã đồng bộ rồi!Vui lòng không tích chọn");
                        }  
                    }
                    
                }
            
            return Success("Đồng bộ thành công!");
        }
        /**
         * Đồng bộ danh mục thuốc BHYT
         */
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult SyncYbyp()
        {
            _iCommonLibraryDmnService.SyncYbyp(UserIdentity.OrganizeId);
            
            return Success("Đồng bộ danh mục thuốc BHYT thành công!");
        }
        
        
        public IActionResult SysCommonMedicineAdd()
        {
            return View();
        }
        
        public IActionResult SysCommonStuffAdd()
        {
            return View();
        }
        
        public IActionResult ChargeItemForm()
        {
            return View();
        }
        
        public IActionResult SysCommonChargeCategoryForm()
        {
            return View();
        }
        
        public IActionResult SysMedicalOrderFrequencyForm()
        {
            return View();
        }

        
        
        
        
        public IActionResult GetPcSelectJson()
        {
            var data = new List<object>();
            var entityList = _sysMedicalOrderFrequencyRepo.GetOrderFrequencyList(this.OrganizeId);
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
        /// Khi sửa thông tin,Mang thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(int keyValue)
        {
            var data = _iCommonLibraryDmnService.GetFormJson(keyValue);
            return Content(data.ToJson());
        }
        
        
    }
}