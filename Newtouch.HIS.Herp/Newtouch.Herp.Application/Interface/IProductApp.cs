using Newtouch.Herp.Domain.Entity;
using System.Web;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Application.Interface
{
    public interface IProductApp
    {
        /// <summary>
        /// Gui bieu mau bao tri vat tu
        /// </summary>
        /// <param name="wzProductEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        int SubmitForm(VProductInfoEntity wzProductEntity, string keyWord);

        /// <summary>
        /// Tai len hinh anh
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Duong dan tai len hinh anh</returns>
        string UploadImag(HttpPostedFileBase file);

        /// <summary>
        /// Gui don vi vat tu
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int SubmitRelProductUnit(ProductUnitRelVo rel, string organizeId);
    }
}