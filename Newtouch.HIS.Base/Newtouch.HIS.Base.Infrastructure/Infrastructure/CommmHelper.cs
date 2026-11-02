using System;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    public class CommmHelper
    {
        /// <summary>
        /// Lấy đường dẫn cục bộ tương ứng với tệp cục bộ
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetLocalFilePath(string path)
        {
            // No Windows D:\ drive on Linux: fall back to the OS temp dir
            // (same mapping as the CIS/PDS ports of this helper).
            var configFileExportBaseDir = ConfigurationHelper.GetAppConfigValue("LocalFileBaseDir");
            if (string.IsNullOrWhiteSpace(configFileExportBaseDir))
            {
                configFileExportBaseDir = System.IO.Path.GetTempPath();
            }
            var normalized = (path ?? "").Replace('\\', System.IO.Path.DirectorySeparatorChar).Replace('/', System.IO.Path.DirectorySeparatorChar);
            var filePath = System.IO.Path.Combine(configFileExportBaseDir, normalized.TrimStart(System.IO.Path.DirectorySeparatorChar));
            var dirPath = System.IO.Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(dirPath) && !System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }

            return filePath;
        }

    }
}
