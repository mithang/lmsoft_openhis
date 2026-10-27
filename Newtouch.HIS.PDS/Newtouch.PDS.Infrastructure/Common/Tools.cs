using System;

namespace Newtouch.Infrastructure.Common
{
    /// <summary>
    /// Lớp công cụ
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// LọcSQLCâu lệnh,Ngăn chặn tiêm nhiễm
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FilterSql(this string s)
        {
            s = s.ToLower().Trim();
            if (s.IndexOf("exec")>-1)
            {
                s = s.Replace("exec", "");
                throw new Exception("Trong tham số không được xuất hiện[ exec ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("delete") > -1)
            {
                s = s.Replace("delete", "");
                throw new Exception("Trong tham số không được xuất hiện[ delete ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("select") > -1)
            {
                s = s.Replace("select", "");
                throw new Exception("Trong tham số không được xuất hiện[ select ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("update") > -1)
            {
                s = s.Replace("update", "");
                throw new Exception("Trong tham số không được xuất hiện[ update ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("master") > -1)
            {
                s = s.Replace("master", "");
                throw new Exception("Trong tham số không được xuất hiện[ master ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("truncate") > -1)
            {
                s = s.Replace("truncate", "");
                throw new Exception("Trong tham số không được xuất hiện[ truncate ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("declare") > -1)
            {
                s = s.Replace("declare", "");
                throw new Exception("Trong tham số không được xuất hiện[ declare ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("create") > -1)
            {
                s = s.Replace("create", "");
                throw new Exception("Trong tham số không được xuất hiện[ create ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("xp_") > -1)
            {
                s = s.Replace("xp_", "");
                throw new Exception("Trong tham số không được xuất hiện[ xp_ ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("-") > -1)
            {
                s = s.Replace("-", "");
                throw new Exception("Trong tham số không được xuất hiện[ - ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("#") > -1)
            {
                s = s.Replace("#", "");
                throw new Exception("Trong tham số không được xuất hiện[ # ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("or") > -1)
            {
                s = s.Replace("or", "");
                throw new Exception("Trong tham số không được xuất hiện[ or ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("and") > -1)
            {
                s = s.Replace("and", "");
                throw new Exception("Trong tham số không được xuất hiện[ and ],Vui lòng nhập đúng quy định");
            }
            if (s.IndexOf("'") > -1)
            {
                s = s.Replace("'", "");
                throw new Exception("Trong tham số không được xuất hiện[ ' ],Vui lòng nhập đúng quy định");
            }
            return s;
        }
    }
}
