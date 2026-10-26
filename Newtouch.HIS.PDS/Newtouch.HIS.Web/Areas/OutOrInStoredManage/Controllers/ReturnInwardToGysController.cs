using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.Infrastructure;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// xuất kho ra bên ngoài
    /// </summary>
    public class ReturnInwardToGysController : ControllerBase
    {
        /// <summary>
        /// submit
        /// </summary>
        /// <param name="djInfoDTO"></param>
        /// <returns></returns>
        public ActionResult SubmitReturnInwardToGys(DjInfoDTO djInfoDTO)
        {
            djInfoDTO.djlx = (int)EnumDanJuLX.waibucuku;
            djInfoDTO.ckbm = Constants.CurrentYfbm.yfbmCode;
            var result = new ReturnInwardToGysProcess(djInfoDTO).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
    }
}