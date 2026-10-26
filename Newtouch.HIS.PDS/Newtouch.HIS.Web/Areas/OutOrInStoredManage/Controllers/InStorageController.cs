using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.Infrastructure;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// nhập kho từ bên ngoài
    /// </summary>
    public class InStorageController : ControllerBase
    {
        /// <summary>
        /// gửi nhập kho từ bên ngoài
        /// </summary>
        /// <param name="djInfoDto"></param>
        /// <returns></returns>
        public ActionResult SubmitInStorage(DjInfoDTO djInfoDto)
        {
            djInfoDto.djlx = (int)EnumDanJuLX.yaopinruku;
            djInfoDto.rkbm = Constants.CurrentYfbm.yfbmCode;
            var result = new InStorageProcess(djInfoDto).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
    }
}