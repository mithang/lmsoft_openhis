using Newtouch.HIS.Application.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// Xuất kho trực tiếp
    /// </summary>
    [Area("OutOrInStoredManage")]
    public class DeliveryDirectController : ControllerBase
    {
        private readonly IDeliveryDirectDmnService deliveryDirectDmnService;

        public IActionResult From()
        {
            return View();
        }

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="djInfoDTO"></param>
        /// <returns></returns>
        public IActionResult SubmitDeliveryDirect(DjInfoDTO djInfoDTO)
        {
            djInfoDTO.djlx = (int)EnumDanJuLX.zhijiefayao;
            djInfoDTO.ckbm = Constants.CurrentYfbm.yfbmCode;
            var result = new DeliveryDirectProcess(djInfoDTO).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
        /// <summary>
        /// getHóa đơn nhập kho
        /// </summary>
        /// <param name="djlx"></param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IActionResult GetRkFphData(int djlx,string fph,DateTime kssj,DateTime jssj)
        {
            var data = deliveryDirectDmnService.GetRkFphData(djlx,fph,kssj,jssj);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Chi tiết hóa đơn nhập kho thuốc
        /// </summary>
        /// <param name="djlx"></param>
        /// <param name="fph"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        public IActionResult GetRkFphMxData(int djlx, string fph, string pc)
        {
            var data = deliveryDirectDmnService.GetRkFphMxData(djlx, fph, pc);
            return Content(data.ToJson());
        }

    }
}