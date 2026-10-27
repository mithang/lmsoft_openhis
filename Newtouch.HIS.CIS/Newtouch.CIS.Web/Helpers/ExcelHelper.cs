using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Newtouch.Tools.Excel
{
    public class ExcelColumn
    {
        public string Field { get; set; }
        public string Title { get; set; }
        public ExcelColumn() { }
        public ExcelColumn(string field, string title) { Field = field; Title = title; }
    }

    public class ExcelSheet
    {
        public string Title { get; set; }
        public List<ExcelColumn> columns { get; set; }
    }

    /// <summary>
    /// Port of the original NPOI-based ToExcel helper WITHOUT NPOI:
    /// NPOI 2.7.x breaks startup because Lib's AddDependencyService scans
    /// every bin assembly with DefinedTypes() and NPOI.Core contains a type
    /// (NullBufferedCipher.ProcessByte) with no implementation on .NET 6.
    /// The output is an HTML table saved with a .xls extension, which Excel
    /// opens natively (classic HTML-spreadsheet trick). Same public API and
    /// same controller call shape as the original.
    /// </summary>
    public static class ExcelHelper
    {
        public static bool ToExcel<T>(this IEnumerable<T> data, string filePath, ExcelSheet sheet)
        {
            try
            {
                var dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
                var props = typeof(T).GetProperties();
                var sb = new StringBuilder();
                sb.Append("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /></head><body><table border=\"1\">");
                if (!string.IsNullOrEmpty(sheet?.Title))
                {
                    sb.Append("<tr><td colspan=\"").Append(sheet.columns?.Count ?? 1).Append("\"><b>")
                      .Append(HttpUtility.HtmlEncode(sheet.Title)).Append("</b></td></tr>");
                }
                sb.Append("<tr>");
                foreach (var col in sheet.columns)
                    sb.Append("<th>").Append(HttpUtility.HtmlEncode(col.Title)).Append("</th>");
                sb.Append("</tr>");
                foreach (var row in data ?? Enumerable.Empty<T>())
                {
                    sb.Append("<tr>");
                    foreach (var col in sheet.columns)
                    {
                        var p = props.FirstOrDefault(x => string.Equals(x.Name, col.Field, StringComparison.OrdinalIgnoreCase));
                        var v = p?.GetValue(row);
                        sb.Append("<td>").Append(HttpUtility.HtmlEncode(v?.ToString() ?? "")).Append("</td>");
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</table></body></html>");
                File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
