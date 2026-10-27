using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// 外部入库
    /// </summary>
    [Area("OutOrInStoredManage")]
    public class InStorageController : ControllerBase
    {
        /// <summary>
        /// 提交外部入库
        /// </summary>
        /// <param name="djInfoDto"></param>
        /// <returns></returns>
        public IActionResult SubmitInStorage(DjInfoDTO djInfoDto)
        {
            djInfoDto.djlx = (int)EnumDanJuLX.yaopinruku;
            djInfoDto.rkbm = Constants.CurrentYfbm.yfbmCode;
            var result = new InStorageProcess(djInfoDto).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
    }
}