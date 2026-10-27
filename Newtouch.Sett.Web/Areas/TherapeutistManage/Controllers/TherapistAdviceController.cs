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

namespace Newtouch.Sett.Web.Areas.TherapeutistManage.Controllers
{

    [Area("TherapeutistManage")]

    public class TherapistAdviceController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ITherapistSuggestionDmnService _TherapistSuggestionDmnService;
        public TherapistAdviceController(ITherapistSuggestionDmnService therapistSuggestionDmnService)
        {
            _TherapistSuggestionDmnService = therapistSuggestionDmnService;
        }


        // GET: TherapeutistManage/TherapistAdvice
        //public IActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>
        /// Lưu
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData( List<TherapistSuggestionEntity> mxList,string delRowIds, string blh,string brlx,string mzzyh)
        {
            if (mxList!=null &&mxList.Count>0)
            {
                foreach (var item in mxList)
                {
                    item.blh = blh;
                    item.brlx = brlx;
                    item.mzzyh = mzzyh;
                }
            }
            var delIds = JsonConvert.DeserializeObject<List<string>>(delRowIds);
            string orgId = this.OrganizeId;

            _TherapistSuggestionDmnService.SaveData(mxList, delIds, orgId);
            return Success();
        }

        public IActionResult GetPatZLJYInfo(string mzzyh,string brlx)
        {
            var resultDto = new TherapistSuggestionDto()
            {
                MzZyPatInfoDto=null,
                TherapistAdviceDto = null
            };
            var patInfoList = _TherapistSuggestionDmnService.GetMzZyPatInfo(mzzyh, this.OrganizeId, brlx);
            if (patInfoList !=null && patInfoList.Count>0)
            {
                resultDto.MzZyPatInfoDto = patInfoList.FirstOrDefault();
                resultDto.TherapistAdviceDto= _TherapistSuggestionDmnService.GetZLJYList(mzzyh, this.OrganizeId, brlx);
                return Success("Gửi thành công", resultDto);
            }
            return Error("Tra cứu thất bại");
        }
        
    }
}
