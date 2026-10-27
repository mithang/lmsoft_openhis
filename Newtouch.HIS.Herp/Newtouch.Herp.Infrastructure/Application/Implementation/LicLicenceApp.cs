using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Infrastructure;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 证照管理 (ported from Framework HttpPostedFileBase/MapPath to ASP.NET Core
    /// IFormFile; uploads are saved ONLY under this app's own
    /// &lt;ContentRoot&gt;/Upload/licence directory, never under the source tree)
    /// </summary>
    public class LicLicenceApp : AppBase, ILicLicenceApp
    {
        private readonly IWebHostEnvironment _env;

        public LicLicenceApp(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns>图片上传路径</returns>
        public string UploadFile(IFormFile file)
        {
            try
            {
                var result = "";
                if (!ValidateFile(file)) return result;
                var urn = Constants.GetAppConfigValue("fileUrn");
                urn = string.IsNullOrWhiteSpace(urn) ? "/Upload/licence" : urn;
                // ContentRootPath is this copy's Web directory; strip any leading
                // slash so the folder is always created inside this app's tree.
                var relative = urn.TrimStart('/', '\\');
                var url = Path.Combine(_env.ContentRootPath, relative);
                if (!Directory.Exists(url)) Directory.CreateDirectory(url);
                var ext = Path.GetExtension(file.FileName);
                var newFileName = Guid.NewGuid() + ext;
                result = urn + "/" + newFileName;
                var parth = Path.Combine(url, newFileName);
                using (var stream = new FileStream(parth, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }

        #region private function

        /// <summary>
        /// 图片效验
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateFile(IFormFile file)
        {
            if (file == null) return false;
            if (!ValidateExt(file)) return false;
            if (!ValidateSize(file)) return false;
            return true;
        }

        /// <summary>
        /// 类型验证
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateExt(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(ext)) return false;
            // Original read legalFileUploadExt from Web.config; that key is not
            // present in this deployment's appsettings, so fall back to the
            // whitelist the Form view itself enforces client-side.
            var legalFileUploadExt = Constants.GetAppConfigValue("legalFileUploadExt");
            if (string.IsNullOrWhiteSpace(legalFileUploadExt))
            {
                legalFileUploadExt = ".jpg,.bmp,.png,.ppt,.pptx,.xls,.xlsx,.doc,.docx,.pdf";
            }
            return legalFileUploadExt.ToLower().Contains(ext.ToLower());
        }

        /// <summary>
        /// 大小验证
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateSize(IFormFile file)
        {
            var maxSize = Constants.GetAppConfigValue("imageMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            var size = file.Length;
            return size / 1024 / 1024 <= Convert.ToInt32(maxSize);
        }

        #endregion
    }
}
