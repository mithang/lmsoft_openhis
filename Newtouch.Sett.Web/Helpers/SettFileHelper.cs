using System;
using System.IO;
using NewtouchHIS.Lib.Base.Exceptions;

namespace Newtouch.Sett.Web.Helpers
{
    /// <summary>Core replacements for Framework file helpers used by
    /// SysPatientMedicalRecord upload (binary-only in Framework):
    /// CommmHelper.GetLocalFilePath, FileHelper.GetExtension/GetTypeByExtension.
    /// GetTypeByExtension mapping is a RECONSTRUCTION (the Framework helper
    /// has no source): images -&gt; 1, office/pdf documents -&gt; 2, else 0.
    /// attachType is display-only in the ported views (hidden grid column).</summary>
    public static class SettFileHelper
    {
        public static string GetLocalFilePath(string path)
        {
            var baseDir = SysConfigReader.String("LocalFileBaseDir");
            if (string.IsNullOrWhiteSpace(baseDir))
            {
                baseDir = Path.Combine(SettPathHelper.ContentRoot, "uploads");
            }
            var normalized = (path ?? "").Replace('\\', Path.DirectorySeparatorChar)
                .Replace('/', Path.DirectorySeparatorChar);
            // Fail closed: the resolved path must stay inside the uploads base.
            // Client-controlled segments (blh, multipart file name) may contain
            // ".." — resolve them and reject anything that escapes the base
            // instead of writing outside it (cycle-82 containment pattern).
            var baseFull = Path.GetFullPath(baseDir);
            var filePath = Path.GetFullPath(Path.Combine(baseFull, normalized.TrimStart(Path.DirectorySeparatorChar)));
            if (!filePath.StartsWith(baseFull + Path.DirectorySeparatorChar, StringComparison.Ordinal))
            {
                throw new FailedException("Đường dẫn tải lên không hợp lệ.");
            }
            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return filePath;
        }

        public static int GetTypeByExtension(string fileName)
        {
            var ext = Path.GetExtension(fileName ?? "").ToLowerInvariant();
            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                case ".bmp":
                case ".webp":
                    return 1;
                case ".doc":
                case ".docx":
                case ".pdf":
                case ".xls":
                case ".xlsx":
                case ".ppt":
                case ".pptx":
                case ".txt":
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
