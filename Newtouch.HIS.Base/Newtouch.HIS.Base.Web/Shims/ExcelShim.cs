using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Newtouch.Tools.Excel
{
    /// <summary>
    /// NPOI port of the Framework Tools.Excel helper (binary-only, Office Interop
    /// based). Shapes mirror the frontend contract: the staff page posts columns
    /// as [{Heading, Name, Width}] and the server computes WidthTimes from the
    /// standard width before calling ToExcel. Output is .xls (HSSF) like before.
    /// </summary>
    public class ExcelColumn
    {
        public string Heading { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double WidthTimes { get; set; }
    }

    public class ExcelSheet
    {
        public string Title { get; set; }
        public IList<ExcelColumn> columns { get; set; }
        public Dictionary<string, string> filters { get; set; }
    }

    public static class ExcelExtensions
    {
        public static bool ToExcel<T>(this IEnumerable<T> data, string filePath, ExcelSheet sheet)
        {
            try
            {
                var dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                IWorkbook wb = new HSSFWorkbook();
                ISheet ws = wb.CreateSheet(string.IsNullOrWhiteSpace(sheet.Title) ? "Sheet1" : sheet.Title);
                int row = 0;
                if (sheet.filters != null && sheet.filters.Count > 0)
                {
                    foreach (var kv in sheet.filters)
                    {
                        var fr = ws.CreateRow(row++);
                        fr.CreateCell(0).SetCellValue(kv.Key + ": " + kv.Value);
                    }
                }
                var columns = sheet.columns ?? new List<ExcelColumn>();
                var header = ws.CreateRow(row++);
                for (int c = 0; c < columns.Count; c++)
                {
                    header.CreateCell(c).SetCellValue(columns[c].Heading ?? columns[c].Name ?? "");
                    // NPOI width unit = 1/256 char; WidthTimes is relative to the
                    // 70px standard column, ~10 chars per unit is a fair mapping.
                    ws.SetColumnWidth(c, (int)(columns[c].WidthTimes * 2560));
                }
                var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var item in data ?? Enumerable.Empty<T>())
                {
                    var dr = ws.CreateRow(row++);
                    for (int c = 0; c < columns.Count; c++)
                    {
                        var prop = props.FirstOrDefault(p =>
                            string.Equals(p.Name, columns[c].Name, StringComparison.OrdinalIgnoreCase));
                        if (prop == null)
                        {
                            continue;
                        }
                        var val = prop.GetValue(item);
                        if (val == null)
                        {
                            continue;
                        }
                        var cell = dr.CreateCell(c);
                        if (val is DateTime dt)
                        {
                            cell.SetCellValue(dt.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else if (val is bool b)
                        {
                            cell.SetCellValue(b);
                        }
                        else if (val is sbyte || val is byte || val is short || val is ushort
                            || val is int || val is uint || val is long || val is ulong
                            || val is float || val is double || val is decimal)
                        {
                            cell.SetCellValue(Convert.ToDouble(val));
                        }
                        else
                        {
                            cell.SetCellValue(val.ToString());
                        }
                    }
                }
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    wb.Write(fs);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
