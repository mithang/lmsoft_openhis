using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    [Area("SystemManage")]

    public class AccoutManageController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IAccountManageApp _accountApp;
        public AccoutManageController(IAccountManageApp accountApp)
        {
            _accountApp = accountApp;
        }

        /// <summary>
        /// 查询病人基本信息和账户信息
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public IActionResult PatAccountInfo(int patid,string zhxz)
        {
            ReserveDto dto=new ReserveDto();
            if (!string.IsNullOrWhiteSpace(zhxz))
            {
                dto= _accountApp.GetHosPatAccInfo(patid, zhxz);
            }
            else
            {
                dto = _accountApp.GetHosPatAccInfo(patid);
            }
            return Success("", dto);
        }

        public IActionResult GetZhyeByPatid(int patid, string zhxz = null)
        {
            if (!string.IsNullOrWhiteSpace(zhxz))
            {
                var zhInfo = _accountApp.GetZhyeByPatid(patid, Convert.ToInt16(zhxz));
                return Success(null, zhInfo);
            }
            else
            {
                var zhye = _accountApp.GetZhyeByPatid(patid);
                return Success(null, zhye);
            }

        }

    }
}
