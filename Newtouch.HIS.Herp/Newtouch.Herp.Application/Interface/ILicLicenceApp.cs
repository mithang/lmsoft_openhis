using System.Web;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Quản lý giấy phép
    /// </summary>
    public interface ILicLicenceApp
    {
        /// <summary>
        /// Tải lên tệp
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string UploadFile(HttpPostedFileBase file);
    }
}