using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Infrastructure.Log;
using Newtouch.Tools;

namespace Newtouch.Infrastructure.Enum
{
    /// <summary>
    /// Sử dụng enum
    /// </summary>
    public class EnumCommon
    {
        #region 单例
        private static readonly EnumCommon _instance = new EnumCommon();

        static EnumCommon()
        {
        }

        public static EnumCommon Instance()
        {
            return _instance;
        }

        #endregion

        /// <summary>
        /// Lấy mô tả enum cờ phát thuốc
        /// </summary>
        /// <param name="fybz"></param>
        /// <returns></returns>
        public string GetFybzCn(string fybz)
        {
            try
            {
                var t = (EnumFybz)Convert.ToInt32(fybz);
                switch (t)
                {
                    case EnumFybz.Wp:
                        return EnumFybz.Wp.GetDescription();
                    case EnumFybz.Yf:
                        return EnumFybz.Yf.GetDescription();
                    case EnumFybz.Yp:
                        return EnumFybz.Yp.GetDescription();
                    case EnumFybz.Yt:
                        return EnumFybz.Yt.GetDescription();
                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("EnumCommon GetFybzCn Error", ex);
                return "";
            }
        }

        /// <summary>
        /// Lấy mô tả enum cờ phát thuốc
        /// </summary>
        /// <param name="fybz"></param>
        /// <returns></returns>
        public string GetFybzCn(int fybz)
        {
            try
            {
                var t = (EnumFybz)fybz;
                switch (t)
                {
                    case EnumFybz.Wp:
                        return EnumFybz.Wp.GetDescription();
                    case EnumFybz.Yf:
                        return EnumFybz.Yf.GetDescription();
                    case EnumFybz.Yp:
                        return EnumFybz.Yp.GetDescription();
                    case EnumFybz.Yt:
                        return EnumFybz.Yt.GetDescription();
                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("EnumCommon GetFybzCn Error", ex);
                return "";
            }
        }

        /// <summary>
        /// Lấy tập hợp tên enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string[] GetNamesArr<T>()
        {
            return System.Enum.GetNames(typeof(T));
        }

        /// <summary>
        /// Chuyển đổi enum thành tập hợp từ điển
        /// </summary>
        /// <typeparam name="T">Loại enum</typeparam>
        /// <returns></returns>
        public Dictionary<string, int> GetEnumDic<T>()
        {
            var resultList = new Dictionary<string, int>();
            var type = typeof(T);
            var strList = GetNamesArr<T>().ToList();
            foreach (var key in strList)
            {
                var val = System.Enum.Format(type, System.Enum.Parse(type, key), "d");
                resultList.Add(key, int.Parse(val));
            }
            return resultList;
        }

        /// <summary>
        /// Chuyển đổi enum thành từ điển
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public Dictionary<string, int> GetEnum<TEnum>()
        {
            var t = typeof(TEnum);
            var arr = System.Enum.GetValues(t);
            return arr.Cast<object>().ToDictionary(item => item.ToString(), item => (int)item);
        }

        /// <summary>
        /// Chuyển đổi enum thành từ điển
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public Dictionary<string, int> GetEnumDesAndVal<TEnum>()
        {
            var t = typeof(TEnum);
            var arr = System.Enum.GetValues(t);
            return arr.Cast<object>().ToDictionary(item => GetFybzCn((int)item), item => (int)item);
        }
    }
}
