using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Newtouch.Sett.API.Services
{
    /// <summary>
    /// Truy cập SQL thô cho host Sett-API. Lý do dùng ADO.NET thay vì
    /// SqlSugar/Entity: toàn bộ logic port từ Sett.API gốc vốn là SQL thuần
    /// (BookingDmnService/OrderDmnService/DischargeSettleDmnService) kèm tên DB
    /// hardcode; viết lại qua ORM sẽ làm mờ các câu SQL cần đối chiếu.
    ///
    /// Quy ước DB:
    /// - Mọi truy vấn chạy trên connection Sett (Initial Catalog = SettDb).
    ///   Bảng không ghi schema DB sẽ phân giải vào SettDb — đúng như bản gốc
    ///   (DefaultDB của Sett.API là DB Sett). Khi chạy staging, SettDb =
    ///   NewtouchHIS_Sett_STG nên mọi ghi đều rơi vào STG, không chạm demo.
    /// - Tham chiếu chéo sang Base/Interface trong SQL gốc viết dạng
    ///   [NewtouchHIS_Base]/[NewtouchHIS_Sett] được thay bằng tên DB cấu hình
    ///   ([{BaseDb}]/[{SettDb}]/[{InterfaceDb}]) trước khi chạy, để staging
    ///   vẫn đọc Base demo (chỉ đọc) và ghi đúng STG.
    /// - Tên DB chỉ lấy từ appsettings/launcher env, KHÔNG BAO GIỜ từ input
    ///   người dùng, nên việc thay tên DB không tạo lỗ hổng SQL injection.
    /// </summary>
    public class SettDb
    {
        private readonly string _connTemplate;
        public string SettDbName { get; }
        public string BaseDbName { get; }
        public string InterfaceDbName { get; }
        public string TopOrganizeId { get; }
        public int LockedOrderExpiredMinute { get; }

        public SettDb(IConfiguration cfg)
        {
            _connTemplate = cfg["BusinessDB:DefaultConn"];
            SettDbName = cfg["SettApi:SettDb"];
            BaseDbName = cfg["SettApi:BaseDb"];
            InterfaceDbName = cfg["SettApi:InterfaceDb"];
            TopOrganizeId = cfg["BusinessConfig:Top_OrganizeId"];
            int m;
            LockedOrderExpiredMinute = int.TryParse(cfg["SettApi:LockedOrderExpiredMinute"], out m) ? m : 5;
        }

        public string SettConnString
        {
            get { return string.Format(_connTemplate, SettDbName); }
        }

        public SqlConnection OpenSett()
        {
            var c = new SqlConnection(SettConnString);
            c.Open();
            return c;
        }

        /// <summary>Thay tên DB hardcode của SQL gốc bằng tên DB cấu hình.</summary>
        public string Q(string sql)
        {
            if (sql == null) return null;
            return sql
                .Replace("[NewtouchHIS_Base]", "[" + BaseDbName + "]")
                .Replace("[NewtouchHIS_Sett]", "[" + SettDbName + "]")
                .Replace("NewtouchHIS_Base..", BaseDbName + "..")
                .Replace("NewtouchHIS_Base.", BaseDbName + ".")
                .Replace("NewtouchHIS_Sett..", SettDbName + "..")
                .Replace("Newtouch_Interface..", InterfaceDbName + "..")
                .Replace("[Newtouch_Interface]", "[" + InterfaceDbName + "]");
        }

        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        public static List<Dictionary<string, object>> Read(SqlCommand cmd)
        {
            var rows = new List<Dictionary<string, object>>();
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    var d = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        d[r.GetName(i)] = r.IsDBNull(i) ? null : r.GetValue(i);
                    }
                    rows.Add(d);
                }
            }
            return rows;
        }

        public static Dictionary<string, object> ReadOne(SqlCommand cmd)
        {
            var rows = Read(cmd);
            return rows.Count > 0 ? rows[0] : null;
        }

        public static object Scalar(SqlCommand cmd)
        {
            var v = cmd.ExecuteScalar();
            return v == DBNull.Value ? null : v;
        }

        /// <summary>spGetNewID của DB Sett: sinh key int cho bảng (dùng cho
        /// mz_gh, mz_gh_book, mz_js, mz_xm, mz_jsmx, xt_brjbxx...).</summary>
        public static int GetNewId(SqlConnection conn, SqlTransaction tx, string table)
        {
            using (var cmd = new SqlCommand("exec spGetNewID @tableName, @NumStr out", conn, tx))
            {
                cmd.Parameters.Add(P("@tableName", table));
                var outP = new SqlParameter("@NumStr", SqlDbType.VarChar, 8) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outP);
                cmd.ExecuteNonQuery();
                var raw = (outP.Value == null ? "" : outP.Value.ToString()).TrimStart('0');
                if (string.IsNullOrEmpty(raw)) throw new InvalidOperationException("Lấy số thứ tự mới thất bại cho bảng " + table);
                return Convert.ToInt32(raw);
            }
        }

        /// <summary>spGetJZXH: lấy số thứ tự khám (jzxh) cho lịch trực.</summary>
        public static short GetJzxh(SqlConnection conn, SqlTransaction tx, string ghlx, string ks, string ys, string ghzb, string user, string orgId)
        {
            using (var cmd = new SqlCommand("spGetJZXH", conn, tx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(P("@ghlx", ghlx ?? ""));
                cmd.Parameters.Add(P("@ks", ks ?? ""));
                cmd.Parameters.Add(P("@ys", ys ?? ""));
                cmd.Parameters.Add(P("@ghzb", ghzb ?? ""));
                cmd.Parameters.Add(P("@CreatorCode", user));
                cmd.Parameters.Add(P("@OrganizeId", orgId));
                var outP = new SqlParameter("@NumStr", SqlDbType.SmallInt) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outP);
                cmd.ExecuteNonQuery();
                return Convert.ToInt16(outP.Value);
            }
        }

        /// <summary>spFieldGetNewID: sinh số thẻ (xt_card), số hồ sơ (xt_blh, format {0:D5}).</summary>
        public string GetFieldNewId(SqlConnection conn, SqlTransaction tx, string orgId, string field, string initFormat)
        {
            using (var cmd = new SqlCommand("exec spFieldGetNewID @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out", conn, tx))
            {
                cmd.Parameters.Add(P("@OrganizeId", orgId));
                cmd.Parameters.Add(P("@TopOrganizeId", TopOrganizeId));
                cmd.Parameters.Add(P("@FieldName", field));
                cmd.Parameters.Add(P("@DateFlag", false));
                cmd.Parameters.Add(P("@currDate", DateTime.Now.ToString("yyyyMMdd")));
                cmd.Parameters.Add(P("@InitFormat", initFormat ?? ""));
                var num = new SqlParameter("@NumStr", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
                var fmt = new SqlParameter("@Format", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(num);
                cmd.Parameters.Add(fmt);
                cmd.ExecuteNonQuery();
                var f = fmt.Value == null ? "" : fmt.Value.ToString();
                var n = num.Value == null ? "" : num.Value.ToString();
                if (string.IsNullOrWhiteSpace(f)) return n;
                return string.Format(f, Convert.ToInt32(n));
            }
        }

        /// <summary>Sys_Config của DB Sett (bản gốc: ISysConfigRepo trên DefaultDB).</summary>
        public string SysConfig(SqlConnection conn, SqlTransaction tx, string orgId, string code)
        {
            using (var cmd = new SqlCommand("select top 1 Value from Sys_Config with(nolock) where OrganizeId=@org and Code=@code and zt='1'", conn, tx))
            {
                cmd.Parameters.Add(P("@org", orgId));
                cmd.Parameters.Add(P("@code", code));
                var v = Scalar(cmd);
                return v == null ? null : v.ToString();
            }
        }

        public bool SysConfigBool(SqlConnection conn, SqlTransaction tx, string orgId, string code)
        {
            var v = SysConfig(conn, tx, orgId, code);
            if (v == null) return false;
            v = v.Trim();
            return v.Equals("ON", StringComparison.OrdinalIgnoreCase)
                || v.Equals("1") || v.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        public static string Str(Dictionary<string, object> row, string col)
        {
            if (row == null) return null;
            object v;
            if (!row.TryGetValue(col, out v) || v == null) return null;
            return v.ToString();
        }

        public static decimal? Dec(Dictionary<string, object> row, string col)
        {
            if (row == null) return null;
            object v;
            if (!row.TryGetValue(col, out v) || v == null) return null;
            return Convert.ToDecimal(v);
        }

        public static int? Int(Dictionary<string, object> row, string col)
        {
            if (row == null) return null;
            object v;
            if (!row.TryGetValue(col, out v) || v == null) return null;
            return Convert.ToInt32(v);
        }
    }
}
