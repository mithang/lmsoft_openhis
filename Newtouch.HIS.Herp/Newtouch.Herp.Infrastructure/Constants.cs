using System;
using System.Collections.Concurrent;
using Newtouch.Herp.Infrastructure.Model;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.Herp.Infrastructure
{
    /// <summary>
    /// 系统常量 (Core port: Redis-backed KF context replaced by an in-memory
    /// per-user store — this app runs as a single instance in this deployment,
    /// and nothing outside this process reads the KF key. AppId/TopOrganizeId
    /// are set once from appsettings by Program.cs via Init, replacing
    /// ConfigurationManager.AppSettings.)
    /// </summary>
    public class Constants
    {
        private static string _topOrganizeId;
        private static string _appId;

        /// <summary>Set once at startup from appsettings (BusinessConfig).</summary>
        public static void Init(string topOrganizeId, string appId)
        {
            _topOrganizeId = topOrganizeId;
            _appId = appId;
        }

        /// <summary>
        /// App-settings reader, set once at startup by Program.cs (replaces
        /// ConfigurationManager.AppSettings / ConfigurationHelper.GetAppConfigValue).
        /// </summary>
        public static Func<string, string> AppConfigReader { get; set; }

        /// <summary>Reads a Web.config-style appSettings value (null when unset).</summary>
        public static string GetAppConfigValue(string key)
        {
            return AppConfigReader?.Invoke(key);
        }

        public static string TopOrganizeId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_topOrganizeId))
                {
                    throw new Exception("Top_OrganizeId未配置");
                }
                return _topOrganizeId;
            }
        }

        public static DateTime MinDateTime
        {
            get { return Convert.ToDateTime("1970-01-01"); }
        }

        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public const string OrganizeId = "OrganizeId";
        public const string KfId = "KfId";
        public const string CreatorCode = "CreatorCode";

        public static string AppId
        {
            get { return _appId; }
        }

        private static readonly ConcurrentDictionary<string, LoginUserCurrentKfModel> _kfStore
            = new ConcurrentDictionary<string, LoginUserCurrentKfModel>();

        public static void SetCurrentKf(string userId, LoginUserCurrentKfModel value)
        {
            if (value == null)
            {
                _kfStore.TryRemove(userId, out _);
            }
            else
            {
                _kfStore[userId] = value;
            }
        }

        private static LoginUserCurrentKfModel GetCurrentKf(string userId)
        {
            return _kfStore.TryGetValue(userId, out var value)
                ? value
                : new LoginUserCurrentKfModel();
        }

        public static LoginUserCurrentKfModel CurrentKf
        {
            get
            {
                var operate = OperatorProvider.GetCurrent();
                return GetCurrentKf(operate.UserId);
            }
        }
    }
}
