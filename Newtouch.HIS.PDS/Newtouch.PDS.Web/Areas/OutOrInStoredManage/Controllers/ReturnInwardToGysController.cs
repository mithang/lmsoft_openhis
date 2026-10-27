using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// 外部出库
    /// </summary>
    [Area("OutOrInStoredManage")]
    public class ReturnInwardToGysController : ControllerBase
    {
        /// <summary>
        /// submit
        /// </summary>
        /// <param name="djInfoDTO"></param>
        /// <returns></returns>
        public IActionResult SubmitReturnInwardToGys(DjInfoDTO djInfoDTO)
        {
            djInfoDTO.djlx = (int)EnumDanJuLX.waibucuku;
            djInfoDTO.ckbm = Constants.CurrentYfbm.yfbmCode;
            var result = new ReturnInwardToGysProcess(djInfoDTO).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
    }
}