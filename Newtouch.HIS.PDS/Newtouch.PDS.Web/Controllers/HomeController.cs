using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.DomainServices.OutPatientPharmacy;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure.Model;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [HandlerLogin]
    public class HomeController : NewtouchHIS.Framework.Web.Controllers.HomeController
    {
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly IPyDmnService _pyDmnService;
        private readonly IMedicineApp _medicineApp;
        private readonly IHomePageStatisticsApp _homePageStatisticsApp;
        private readonly ISysConfigRepo _sysConfigRepo;

        public HomeController(IOrganizeAppService organizeApp,
            ISysUserExDmnService sysUserDmnService,
            ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo,
            IPyDmnService pyDmnService,
            IMedicineApp medicineApp,
            IHomePageStatisticsApp homePageStatisticsApp,
            ISysConfigRepo sysConfigRepo) : base(organizeApp)
        {
            _sysUserDmnService = sysUserDmnService;
            _sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
            _pyDmnService = pyDmnService;
            _medicineApp = medicineApp;
            _homePageStatisticsApp = homePageStatisticsApp;
            _sysConfigRepo = sysConfigRepo;
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override IActionResult Index()
        {
            var cookieLoginFlag = Request.Cookies[Constants.AppId + "_" + OperatorProvider.GetCurrent().UserCode + "_" + "LoginFlag"];
            ViewBag.cookieLoginFlag = cookieLoginFlag;
            return base.Index();
        }

        /// <summary>
        /// 用户切换药房
        /// </summary>
        /// <returns></returns>
        public IActionResult SetUserPharmacyForm()
        {            
            ViewBag.yfbmList = _sysUserDmnService.GetYfbmListByUserId(OperatorProvider.GetCurrent().UserId, OperatorProvider.GetCurrent().OrganizeId);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opr"></param>
        public void SwithOrgSuccessOprBeforeSave(OperatorModel opr)
        {
            //更新用户药房部分
            if (ConfigurationHelper.GetAppConfigBoolValue("Is_UserYfbmRelated") == true && opr.OrganizeId != null)
            {
                //当前要操作的药房部门
                if (!string.IsNullOrWhiteSpace(opr.UserId) && !string.IsNullOrWhiteSpace(opr.OrganizeId))
                {
                    opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId)?.ToList();
                }
                if (opr.yfbmCodeList != null && opr.yfbmCodeList.Count > 0)
                {
                    //当前要操作的药房部门    //在OrgId关联了多个yfbm怎么办
                    var curYfbmCode = opr.yfbmCodeList.FirstOrDefault();
                    var curYfbmObj = _sysPharmacyDepartmentRepo.GetUserYfbmByCode(curYfbmCode, opr.OrganizeId);
                    if (curYfbmObj != null)
                    {
                        Constants.SetCurrentYfbm(opr.UserId, new LoginUserCurrentYfbmModel()
                        {
                            yfbmCode = curYfbmCode,
                            yfbmjb = curYfbmObj.yfbmjb,
                            mzzybz = curYfbmObj.mzzybz,
                            yfbmmc = curYfbmObj.yfbmmc
                        });
                    }                    
                }
                else
                {
                    Constants.SetCurrentYfbm(opr.UserId, null);  //移除
                }
            }

            //切换药房部门
            Response.Cookies.Append(Constants.AppId + "_" + "CookieKey_IsHospAdministrator", opr.IsHospAdministrator.ToString().ToLower());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Default()
        {
            return View();
        }

        /// <summary>
        /// 获取待处理html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNeedDealDiv()
        {
            var result = _homePageStatisticsApp.AssembleNeedDealHtml();
            return Success("", result);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// 获取带处理总数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPendingCount()
        {
            return Content(_homePageStatisticsApp.GetPendingCount().ToJson());
        }

        /// <summary>
        /// 获取门诊月处方发药次
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFyCountVoByYfbm()
        {
            var result = new MonthlySummaryDO[2];
            result[0] = _medicineApp.GetFyCountVoByYfbm(_pyDmnService.GetMzFyCountVoByYfbm());
            result[1] = _medicineApp.GetFyCountVoByYfbm(_pyDmnService.GetZyFyCountVoByYfbm());
            return Content(result.ToJson());
        }

        /// <summary>
        /// 获取门诊月处方发药次
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFyCountBydl()
        {
            var result = _medicineApp.GetFyCountBydl() ?? new List<FyClassificationStatistics>();
            return Content(result.ToJson());
        }

        /// <summary>
        /// 切换登陆用户药房
        /// </summary>
        /// <param name="kfId">库房ID</param>
        /// <param name="kfName">库房名称</param>
        /// <param name="kfLeve">库房等级</param>
        /// <returns></returns>
        public IActionResult SetUserPharmacy(string yfbmCode, string mzzybz, string yfbmjb, string yfbmmc)
        {
            var opr = Constants.CurrentYfbm;
            opr.yfbmCode = yfbmCode;
            opr.yfbmjb = yfbmjb;
            opr.mzzybz = mzzybz;
            opr.yfbmmc = yfbmmc;
            Constants.SetCurrentYfbm(OperatorProvider.GetCurrent().UserId, opr);
            return Success("切换成功");
        }

		#region Nhắc tin nhắn
        public IActionResult getmesquery()
		{
            string yfbm= Constants.CurrentYfbm.yfbmCode;
            string kcyjz = SysConfigReader.String("GET_YFKCYG");//库存预警值
            int gqyjz = -SysConfigReader.Int("GET_YFGQYG");//过期预警值
            var retdata = _sysUserDmnService.MSGQuery(yfbm,this.OrganizeId, gqyjz, kcyjz);
            int gqyjcount=0,kcyjcount=0,kcdryjcount=0;
			if (retdata!=null)
			{
                kcyjcount=retdata.Where(p => p.typeas == "1").Count();
                gqyjcount = retdata.Where(p => p.typeas == "2").Count();
                kcdryjcount = retdata.Where(p => p.typeas == "3").Count();
            }
            var data =new {
                rows = retdata,
                kcyj = kcyjcount,
                gqyj= gqyjcount,
                drgqyj= kcdryjcount
            };
            return Content(data.ToJson());
		}
        #endregion
        
        /**
         * 同步系统配置参数
         */
        public IActionResult SyncSysConfigParams(string orgId)
        {
            //基础数据
            var sysConfigBaseEntities = _sysConfigRepo.GetList("", "*").ToList();
            //组织机构自带数据
            var sysConfigEntities = _sysConfigRepo.GetList("", orgId).ToList();
            //根据code 去重
            var sysConfigCodes = new HashSet<string>(sysConfigEntities.Select(entity => entity.Code));
            var filteredEntities = sysConfigBaseEntities
                .Where(baseEntity => !sysConfigCodes.Contains(baseEntity.Code))
                .ToList();
            foreach (var item in filteredEntities)
            {
                item.Id = Guid.NewGuid().ToString();
                item.OrganizeId = orgId;
            }
            var insert = _sysConfigRepo.Insert(filteredEntities);
            return Success("",insert);
        }
    }
}