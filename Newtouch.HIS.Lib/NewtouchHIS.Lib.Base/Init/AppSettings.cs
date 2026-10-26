using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using SqlSugar.Extensions;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public class AppSettings
    {
        static IConfiguration Configuration { get; set; }

        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// đóng gói ký tự cần thao tác
        /// </summary>
        /// <param name="sections">cấu hình nút</param>
        /// <returns></returns>
        public static string App(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return "";
        }

        /// <summary>
        /// lấy đệ quy mảng thông tin cấu hình
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static List<T> App<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            // tham chiếu Microsoft.Extensions.Configuration.Binder gói
            Configuration.Bind(string.Join(":", sections), list);
            return list;
        }
        public static T Bind<T>(string key, T t)
        {
            Configuration.Bind(key, t);

            return t;
        }

        public static T? GetAppConfig<T>(string key, T? defaultValue = default)
        {
            if (Configuration[key] == null)
            {
                return defaultValue;
            }
            T setting = (T)Convert.ChangeType(Configuration[key], typeof(T));
            var value = setting;
            if (setting == null)
                value = defaultValue;
            return value;
        }

        /// <summary>
        /// lấy tệp cấu hình 
        /// </summary>
        /// <param name="key">eg: WeChat:Token</param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            return Configuration[key];
        }

        /// <summary>
        /// lấy giá trị cấu hình kiểu boolean mặc địnhfalse
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetAppConfigBoolValue(string key, bool? defaultValue = null)
        {
            string val = Configuration[key];
            if (string.IsNullOrEmpty(val) || !val.ObjToBool())
            {
                return defaultValue ?? false;
            }
            return val.ObjToBool();
        }
        #region đọcAppConfigmở rộng cấu hình tùy chỉnh
        /// <summary>
        /// lấy tệp cấu hình 
        /// (đọc đường dẫn：AppConfigExt)
        /// </summary>
        /// <param name="key">eg: WeChat:Token</param>
        /// <returns></returns>
        public static string GetConfigExt(string key)
        {
            key = AssemblyKey(key);
            return Configuration[key];
        }
        /// <summary>
        /// lấy giá trị cấu hình kiểu boolean mặc địnhfalse
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetAppConfigBoolValueExt(string key, bool? defaultValue = null)
        {
            key = AssemblyKey(key);
            string val = Configuration[key];
            if (string.IsNullOrEmpty(val) || !val.ObjToBool())
            {
                return defaultValue ?? false;
            }
            return val.ObjToBool();
        }

        private static string AssemblyKey(string key)
        {
            return $"AppConfigExt:{key}";
        }
        #endregion
    }
}
