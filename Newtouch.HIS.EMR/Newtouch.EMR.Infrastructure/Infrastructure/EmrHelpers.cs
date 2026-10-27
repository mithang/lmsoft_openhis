using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Utilities;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Port of EFDBBaseFuncHelper (EF6/IDatabaseFactory) to SqlSugar over
    /// EmrDb. GetNewFieldUniqueValue still calls the same stored procedure
    /// (spFieldGetNewID) with the same output parameters; only the ADO
    /// plumbing changed from SqlParameter to SugarParameter.
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        private EFDBBaseFuncHelper()
        {
        }

        public static EFDBBaseFuncHelper Instance
        {
            get { return new EFDBBaseFuncHelper(); }
        }

        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            return new EFDBBaseFuncHelper();
        }

        public string MedicalRecordLSH(string orgId)
        {
            return "RNO" + EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("dzbl_id", orgId, "{0:D10}", false);
        }

        public string GetNewFieldUniqueValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var tpl = GetNewFieldUniqueObjectValue(fieldName, orgId, initFormat, dateFlag);
            if (tpl.Item2 == null || string.IsNullOrWhiteSpace(tpl.Item2.ToString()))
            {
                return tpl.Item1.ToString();
            }
            else
            {
                return string.Format(tpl.Item2.ToString(), Convert.ToInt32(tpl.Item1));
            }
        }

        private Tuple<object, object> GetNewFieldUniqueObjectValue(string fieldName, string orgId, string initFormat, bool dateFlag)
        {
            var numOut = new SugarParameter("@NumStr", "") { Direction = ParameterDirection.Output, Size = 50 };
            var fmtOut = new SugarParameter("@Format", "") { Direction = ParameterDirection.Output, Size = 50 };
            var sqlParList = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", orgId),
                new SugarParameter("@TopOrganizeId", Constants.TopOrganizeId),
                new SugarParameter("@FieldName", fieldName),
                new SugarParameter("@DateFlag", dateFlag),
                new SugarParameter("@currDate", DateTime.Now.ToString("yyyyMMdd")),
                new SugarParameter("@InitFormat", initFormat),
                numOut,
                fmtOut
            };
            Newtouch.EMR.Infrastructure.EmrDb.GetConnection().Ado.ExecuteCommand(
                "exec spFieldGetNewID @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out",
                sqlParList.ToArray());
            return Tuple.Create(numOut.Value, fmtOut.Value);
        }

        public static Dictionary<string, string> GetEnumDescription<T>()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;
                    dic.Add(field.Name, description);
                }
            }
            return dic;
        }

        public static List<KeyValuePair<string, string>> GetEnumDescriptionList<T>()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;
                    result.Add(new KeyValuePair<string, string>(field.Name, description));
                }
            }
            return result;
        }
    }

    public enum EnumDBFactoryType
    {
        DefaultDB,
    }

    /// <summary>
    /// Port of EMR Constants. The old ConstantsBase (binary FrameworkBase)
    /// is replaced by the vendor net6.0 ConfigInitHelper business config.
    /// </summary>
    public class Constants
    {
        public static string TopOrganizeId
        {
            get { return ConfigInitHelper.SysConfig != null ? ConfigInitHelper.SysConfig.Top_OrganizeId : null; }
        }

        public static string AppId
        {
            get { return ConfigInitHelper.SysConfig != null ? ConfigInitHelper.SysConfig.AppId : null; }
        }
    }

    /// <summary>
    /// Port of CommmHelper (verbatim logic; no framework dependency).
    /// </summary>
    public partial class CommmHelper
    {
        public static Dictionary<string, int> GetDic<TEnum>()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            Type t = typeof(TEnum);
            var arr = Enum.GetValues(t);
            foreach (var item in arr)
            {
                dic.Add(item.ToString(), (int)item);
            }
            return dic;
        }

        public static int CalculateAgeCorrect(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }
    }
}

namespace Newtouch.EMR.Infrastructure
{
    /// <summary>
    /// Port of EmrLogger. The original hard-coded a Windows path
    /// (D:\log_EmrExSqlerror); on Linux the log goes under the app base
    /// directory instead. Failures are still swallowed, as in the original.
    /// </summary>
    public class EmrLogger
    {
        public static void Info(string message)
        {
            try
            {
                var root = Path.Combine(AppContext.BaseDirectory, "log_EmrExSqlerror");
                var date = DateTime.Now.ToString("yyyyMMddHHmm");
                var dirPath = Path.Combine(root, date.Substring(0, 8));
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                var filePath = Path.Combine(dirPath, date.Substring(8, 2) + ".txt");
                File.AppendAllText(filePath, string.Format("\r\n\r\n{0}.Info.{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
            }
            catch { }
        }
    }

    /// <summary>
    /// Minimal port of the vendor API response envelope that
    /// ZybrjbxxDmnService.Sync_HisPatinfo relies on. The original
    /// APIRequestHelper lived in the binary Newtouch.Common.Web; the
    /// result-code values (SUCCESS = 10000) are taken from the vendor
    /// ResponseResultCodeAPI enum used by the sibling CIS module.
    /// </summary>
    public static class APIRequestHelper
    {
        public class DefaultResponse<T>
        {
            public int code { get; set; }
            public string msg { get; set; }
            public T data { get; set; }
        }

        public static class ResponseResultCode
        {
            public const int SUCCESS = 10000;
            public const int ERROR = 20000;
            public const int FAIL = 40004;
        }
    }

    /// <summary>
    /// Shared HttpClient plumbing for the EMR site-API helpers. The original
    /// helpers inherited the binary SiteAPIRequestHelperBase; this port
    /// follows the MRQC AuthenManageHelper pattern instead (HttpClient,
    /// proxy bypassed so loopback hosts stay reachable, app token cached
    /// in memory for 20 minutes). The auth-center host comes from the web
    /// app's configuration (SiteAuthCenterHost); until the web agent wires
    /// it, GetToken throws, exactly like the MRQC port.
    /// </summary>
    public abstract class SiteApiHelperBase
    {
        private static readonly HttpClient _http = new HttpClient(new HttpClientHandler { UseProxy = false });
        private static string _cachedToken = "";
        private static DateTime _cachedTokenExpiry = DateTime.MinValue;

        protected static string GetAppToken()
        {
            if (!string.IsNullOrWhiteSpace(_cachedToken) && DateTime.Now < _cachedTokenExpiry)
            {
                return _cachedToken;
            }
            string host = AppSettings.GetConfig("SiteAuthCenterHost");
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new NewtouchHIS.Lib.Base.Exceptions.FailedException("[Xác thực] Chưa cấu hình SiteAuthCenterHost");
            }
            string appId = ConfigInitHelper.SysConfig != null ? ConfigInitHelper.SysConfig.AppId : null;
            var reqObj = new { AppId = appId, Data = AppSettings.GetConfig("Token_ApiManage") };
            string resp = HttpPostRaw(host.TrimEnd('/') + "/api/Auth/GetAppToken", JsonConvert.SerializeObject(reqObj), null).GetAwaiter().GetResult();
            var tokenResp = JsonConvert.DeserializeObject<AuthenTokenHelper>(resp);
            var data = JsonConvert.DeserializeObject<Tokendata>(tokenResp.BusData.data.ToString());
            _cachedToken = data.Token;
            _cachedTokenExpiry = DateTime.Now.AddMinutes(20);
            return _cachedToken;
        }

        protected static T Request<T>(string host, string relativeUrl, string json)
        {
            string token = GetAppToken();
            string url = host.TrimEnd('/') + "/" + relativeUrl.TrimStart('/');
            string resp = HttpPostRaw(url, json, token).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<T>(resp);
        }

        private static async Task<string> HttpPostRaw(string uri, string json, string token)
        {
            using (var req = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                req.Content = new StringContent(json ?? "", Encoding.UTF8, "application/json");
                if (!string.IsNullOrWhiteSpace(token))
                {
                    req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                using (var resp = await _http.SendAsync(req))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
            }
        }

        private class AuthenTokenHelper
        {
            public AuthenBusData BusData { get; set; }
        }

        private class AuthenBusData
        {
            public object data { get; set; }
        }

        private class Tokendata
        {
            public string Token { get; set; }
        }
    }

    /// <summary>Port of SiteHispatinfoAPIHelper (Sett patient-info API).</summary>
    public class SiteHispatinfoAPIHelper : SiteApiHelperBase
    {
        private readonly static string _domainConfigKey = "SiteSettAPIHost";
        private readonly static string _apiName = "Sett";

        public static string GetToken()
        {
            return GetAppToken();
        }

        public static T Request<T>(string relativeUrl, string json)
        {
            return Request<T>(AppSettings.GetConfig(_domainConfigKey), relativeUrl, json);
        }
    }

    /// <summary>Port of SiteCISAPIHelper (CIS API).</summary>
    public class SiteCISAPIHelper : SiteApiHelperBase
    {
        private readonly static string _domainConfigKey = "SiteCISAPIHost";
        private readonly static string _apiName = "CIS";

        public static string GetToken()
        {
            return GetAppToken();
        }

        public static T Request<T>(string relativeUrl, string json)
        {
            return Request<T>(AppSettings.GetConfig(_domainConfigKey), relativeUrl, json);
        }
    }

    /// <summary>同步住院病人信息 (verbatim DTO from the original file).</summary>
    public class InpatientPatientInfoDTO
    {
        public string orgId { get; set; }
        public string zyh { get; set; }
        public string rygh { get; set; }
        public string username { get; set; }
        public string GetMAC { get; set; }
    }
}
