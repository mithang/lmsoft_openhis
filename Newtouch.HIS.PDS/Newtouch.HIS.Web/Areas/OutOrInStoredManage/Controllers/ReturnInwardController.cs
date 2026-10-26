using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.Infrastructure;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// trả lại thuốc phát nội bộ
    /// </summary>
    public class ReturnInwardController : ControllerBase
    {
        /// <summary>
        /// gửi trả lại thuốc phát nội bộ yêu cầu
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitReturnInward(DjInfoDTO djInfoDto)
        {
            djInfoDto.djlx = (int)EnumDanJuLX.neibufayaotuihui;
            djInfoDto.ckbm = Constants.CurrentYfbm.yfbmCode;
            var result = new ReturnInwardProcess(djInfoDto).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
    }
}