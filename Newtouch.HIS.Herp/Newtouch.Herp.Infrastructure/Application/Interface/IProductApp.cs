using Newtouch.Herp.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System.Web;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Application.Interface
{
    public interface IProductApp
    {
        /// <summary>
        /// Gửi biểu mẫu bảo trì vật tư
        /// </summary>
        /// <param name="wzProductEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        int SubmitForm(VProductInfoEntity wzProductEntity, string keyWord);

        /// <summary>
        /// Tải lên hình ảnh
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Đường dẫn tải lên hình ảnh</returns>
        string UploadImag(IFormFile file);

        /// <summary>
        /// Gửi đơn vị vật tư
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int SubmitRelProductUnit(ProductUnitRelVo rel, string organizeId);
    }
}