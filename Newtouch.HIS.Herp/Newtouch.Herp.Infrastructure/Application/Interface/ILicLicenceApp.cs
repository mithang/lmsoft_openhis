using System.Web;

using Microsoft.AspNetCore.Http;
namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Quản lý chứng chỉ
    /// </summary>
    public interface ILicLicenceApp
    {
        /// <summary>
        /// Tải lên tệp
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string UploadFile(IFormFile file);
    }
}