using System;
using System.Linq;
using NewtouchHIS.Lib.Base;

namespace DbConfigHelper
{
    /// <summary>
    /// Giai doan 7: thay ten DB hardcode trong cau SQL 3 phan bang ten DB cau hinh.
    /// Ten DB literal (vd [Newtouch_EMR]) luon tro ve DB demo bat chap app dang chay
    /// DB staging nao; ham nay tra ve ten DB cau hinh hien tai (vd Newtouch_EMR_STG
    /// tren staging, nho env override BusinessDB__DBConf__*), fallback ve ten goc neu
    /// khong tim thay ConnId — nen tren prod hanh vi khong doi. Tra ve dang [ten].
    /// </summary>
    internal static class DbNames
    {
        public static string Name(string connId, string fallback)
        {
            var conf = ConfigInitHelper.DbConfig?.DBConf?.FirstOrDefault(p =>
                string.Equals(p.ConnId, connId, StringComparison.OrdinalIgnoreCase));
            var name = string.IsNullOrWhiteSpace(conf?.DBName) ? fallback : conf.DBName;
            return "[" + name.Replace("]", "]]") + "]";
        }
    }
}
