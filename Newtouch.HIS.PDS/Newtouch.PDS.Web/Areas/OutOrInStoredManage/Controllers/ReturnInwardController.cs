using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// 内部发药退回
    /// </summary>
    [Area("OutOrInStoredManage")]
    public class ReturnInwardController : ControllerBase
    {
        /// <summary>
        /// 提交内部发药退回请求
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitReturnInward(DjInfoDTO djInfoDto)
        {
            djInfoDto.djlx = (int)EnumDanJuLX.neibufayaotuihui;
            djInfoDto.ckbm = Constants.CurrentYfbm.yfbmCode;
            var result = new ReturnInwardProcess(djInfoDto).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
    }
}