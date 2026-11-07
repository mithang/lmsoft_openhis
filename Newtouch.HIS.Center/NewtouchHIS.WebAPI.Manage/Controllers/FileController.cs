using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using Spire.Xls;
using System.Net.Http.Headers;
using System.Text;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    /// <summary>
    /// Thao tác tệp
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileManageConfig fileManageConf = ConfigInitHelper.SysConfig!.FileConfig ?? new FileManageConfig();
        /// <summary>
        /// Tải tệp lên
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
        /// web Tải tệp lên
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("WebUploaFile")]
        public async Task<Dictionary<string, string>> UploadFileForWebAsync()
        {
            // Không phải form multipart thì Request.Form ném InvalidDataException;
            // coi như không có tệp (giống trường hợp form không có file + tiền lệ Herp LicenceController).
            if (!HttpContext.Request.HasFormContentType)
            {
                return new Dictionary<string, string>()
                {
                    ["fileContent"] = string.Empty
                };
            }
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
        /// Excel Tải lên
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DrgExcelUpload")]
        public async Task<IActionResult> DrgExcelUpload(IFormFile file)
        {
            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName; // Tên tệp gốc(bao gồm đường dẫn)
            var dotIdx = string.IsNullOrEmpty(filename) ? -1 : filename.LastIndexOf('.');
            // Tên như "file." có dấu chấm cuối nhưng phần mở rộng rỗng: từ chối như tên không có phần mở rộng.
            var extName = dotIdx < 0 ? string.Empty : filename.Substring(dotIdx).Replace("\"", ""); // Phần mở rộng
            // Phần mở rộng chứa dấu phân cách thư mục (từ tên file crafted kiểu "a.txt/../x")
            // không bao giờ là phần mở rộng thật: từ chối như tên không có phần mở rộng, thay vì
            // để File.Create ném ngoại lệ kèm đường dẫn tuyệt đối của server.
            if (dotIdx < 0 || extName.Length <= 1 || extName.IndexOfAny(new[] { '/', '\\' }) >= 0)
            {
                throw new FailedException("Tên tệp tải lên phải có phần mở rộng.");
            }
            string shortfilename = $"{Guid.NewGuid()}{extName}";// Tên tệp mới
            string fileSavePath = $"{Directory.GetCurrentDirectory()}{fileManageConf.DrgDataFileDirPathWithDate ?? "/File/Drg/"}"; //Thư mục tạm của tệp,sau khi nhập xong Xóa
            filename = fileSavePath + shortfilename; // Tên tệp mới(bao gồm đường dẫn)
            if (!Directory.Exists(fileSavePath))
            {
                Directory.CreateDirectory(fileSavePath);
            }
            using (FileStream fs = System.IO.File.Create(filename)) // Tạo tệp mới
            {
                await file.CopyToAsync(fs);// Sao chép tệp
                fs.Flush();// Xóa dữ liệu bộ đệm
                           //Theo filename [Đường dẫn đĩa của máy chủ tệp]có thể thao tác nghiệp vụ với tệp
            }
            if (extName != ".csv")
            {
                //Tải vàoxls Tài liệu Chuyển đổicsv
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(filename);
                //Lấy trang tính đầu tiên
                Worksheet sheet = workbook.Worksheets[0];
                //Lưu thànhcsv Định dạng
                sheet.SaveToFile(filename + ".csv", ",", Encoding.UTF8);
            }

            //Sau khi xử lý xong,Xóa tệp đã tải lên
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
            // chặn xóa file tùy ý: chỉ cho phép xóa file nằm dưới thư mục gốc của app
            if (!string.IsNullOrWhiteSpace(filepath))
            {
                var appRoot = Path.GetFullPath(Directory.GetCurrentDirectory()).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                var fullPath = Path.GetFullPath(filepath);
                if (fullPath.StartsWith(appRoot + Path.DirectorySeparatorChar, StringComparison.Ordinal)
                    && System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return new JsonResult("ok");
        }
    }


}
