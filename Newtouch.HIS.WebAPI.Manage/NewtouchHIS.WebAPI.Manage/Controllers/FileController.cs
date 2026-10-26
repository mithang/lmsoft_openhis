using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using Spire.Xls;
using System.Net.Http.Headers;
using System.Text;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    /// <summary>
    /// thao tác tệp
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileManageConfig fileManageConf = ConfigInitHelper.SysConfig!.FileConfig ?? new FileManageConfig();
        /// <summary>
        /// tải tệp lên
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadFile")]
        public async Task<Dictionary<string, string>> UploadFileAsync(IEnumerable<IFormFile> files)
        {
            var fileStream = files.FirstOrDefault()?.OpenReadStream();
            var fileContent = new StringBuilder();
            if (fileStream != null)
            {
                using var reader = new StreamReader(fileStream!);
                while (reader.Peek() >= 0)
                {
                    fileContent.AppendLine(await reader.ReadLineAsync());
                }
            }

            var result = new Dictionary<string, string>()
            {
                ["fileContent"] = fileContent.ToString()
            };

            return result;
        }
        /// <summary>
        /// webtải tệp lên
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("WebUploaFile")]
        public async Task<Dictionary<string, string>> UploadFileForWebAsync()
        {
            var fileStream = HttpContext.Request.Form.Files.FirstOrDefault()?.OpenReadStream();
            var fileContent = new StringBuilder();
            if (fileStream != null)
            {
                using var reader = new StreamReader(fileStream!);
                while (reader.Peek() >= 0)
                {
                    fileContent.AppendLine(await reader.ReadLineAsync());
                }
            }

            var result = new Dictionary<string, string>()
            {
                ["fileContent"] = fileContent.ToString()
            };

            return result;
        }
        /// <summary>
        /// Exceltải lên
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DrgExcelUpload")]
        public async Task<IActionResult> DrgExcelUpload(IFormFile file)
        {
            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName; // tên tệp gốc（bao gồm lộ trình）
            var extName = filename.Substring(filename.LastIndexOf('.')).Replace("\"", "");// phần mở rộng
            string shortfilename = $"{Guid.NewGuid()}{extName}";// tên tệp mới
            string fileSavePath = $"{Directory.GetCurrentDirectory()}{fileManageConf.DrgDataFileDirPathWithDate ?? "/File/Drg/"}"; //thư mục tạm của tệp，sau khi nhập xong xóa
            filename = fileSavePath + shortfilename; // tên tệp mới（bao gồm lộ trình）
            if (!Directory.Exists(fileSavePath))
            {
                Directory.CreateDirectory(fileSavePath);
            }
            using (FileStream fs = System.IO.File.Create(filename)) // tạo tệp mới
            {
                await file.CopyToAsync(fs);// sao chép tệp
                fs.Flush();// xóa dữ liệu bộ đệm
                           //theo filename 【đường dẫn đĩa máy chủ tệp】có thể thực hiện thao tác nghiệp vụ đối với tệp
            }
            if (extName != ".csv")
            {
                //tảixlstài liệu chuyển đổicsv
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(filename);
                //lấy bảng làm việc đầu tiên
                Worksheet sheet = workbook.Worksheets[0];
                //lưu thànhcsvđịnh dạng
                sheet.SaveToFile(filename + ".csv", ",", Encoding.UTF8);
            }

            //sau khi xử lý xong，xóa tệp đã tải lên
            //if (System.IO.File.Exists(filename))
            //{
            //    System.IO.File.Delete(filename);
            //}
            return new JsonResult(Path.GetFileName(filename + ".csv"));
        }

        [HttpPost]
        [Route("FileDelete")]
        public async Task<IActionResult> FileDelete(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            return new JsonResult("ok");
        }
    }


}
