using System;
using System.Linq;
using NewtouchHIS.Lib.Base;

namespace DbConfigHelper
{
    /// <summary>
    /// R7-B2: thay ten DB hardcode trong cau SQL 3 phan bang ten DB cau hinh.
    /// Ten DB literal (vd NewtouchHIS_Base) luon tro ve DB demo bat chap app dang chay
    /// DB staging nao; ham nay tra ve ten DB cau hinh hien tai (vd NewtouchHIS_Base_STG
    /// tren staging, nho env override BusinessDB__DBConf__*), fallback ve ten goc neu
    /// khong tim thay ConnId — nen tren prod hanh vi khong doi. Tra ve dang [ten].
    /// Mau giong het DbNames.cs cua PDS/CIS/EMR/Herp/MRMS/OR/Center (cung ConnId
    /// "BaseDb"). Luu y: cac repo Base chay tren BaseDb.GetConnection() nhung tien to
    /// DB literal trong SQL van ep SQL Server ve DB demo, nen phai thay bang ham nay
    /// (ly do V6 "dung BaseDb connection nen khong doi" la sai voi truong hop co
    /// tien to 3 phan — xem r7-b2-report.md).
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
