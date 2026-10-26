using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Newtouch.Infrastructure
{
    public static class EnumHelper
    {
        #region 获取枚举列表

        /// <summary>
        /// thông qua enum Đ ối tư ợng lấy enum danh sách
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> GetEnumList<T>(this T value)
        {
            var list = new List<T>();
            if (value is Enum)
            {
                var valData = Convert.ToInt32((T)Enum.Parse(typeof(T), value.ToString()));
                var tps = Enum.GetValues(typeof(T));

                list.AddRange(from object tp in tps where ((int)Convert.ToInt32((T)Enum.Parse(typeof(T), tp.ToString())) & valData) == valData select (T)tp);
            }

            return list;
        }
 
        /// <summary>
        /// thông qua enum loại lấy enum danh sách
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> GetEnumList<T>()
        {
            List<T> list = Enum.GetValues(typeof(T)).OfType<T>().ToList();
            return list;
        }
         

        /// <summary>
        /// thông qua enum Đ ối tư ợng lấy tất cả các enum 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>(this Enum value)
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }

        /// <summary>
        /// Gets all items for an enum type.(thông qua enum loại lấy tất cả các enum)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>() where T : struct
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }

        #endregion

        #region dictionary
        /// <summary>
        /// Enum chuyển Dictionary
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumDic<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(t => Convert.ToInt32(t), t => t.ToString());
        }
        /// <summary>
        /// enum chuyển từ điển tập hợp (Key Làvalue,Value Làdescription(nếu không tồn tại Tạidescription thì Làname))
        /// </summary>
        /// <typeparam name="T"> enum loại tên</typeparam>
        /// <param name="keyDefault">Mặc đ ịnhkey giá trị </param>
        /// <param name="valueDefault">Mặc đ ịnhvalue giá trị </param>
        /// <returns>Quay lại tạo của từ điển tập hợp </returns>
        public static Dictionary<string, object> EnumListDic<T>(string keyDefault, string valueDefault = "")
        {
            Dictionary<string, object> dicEnum = new Dictionary<string, object>();
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }
            if (!string.IsNullOrEmpty(keyDefault)) // phán đoán có hay không thêm mặc đ ịnh tùy chọn 
            {
                dicEnum.Add(keyDefault, valueDefault);
            }
            string[] fieldstrs = Enum.GetNames(enumType); //Lấy enum trư ờng mảng nhóm 
            foreach (var item in fieldstrs)
            {
                string description = string.Empty;
                var field = enumType.GetField(item);
                object[] arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true); //Lấy thuộc tính trư ờng mảng nhóm 
                if (arr != null && arr.Length > 0)
                {
                    description = ((DescriptionAttribute)arr[0]).Description;   // thuộc tính mô tả
                }
                else
                {
                    description = item;  //Mô tả không tồn tại tạilấy trư ờng tên
                }
                dicEnum.Add(description, (int)Enum.Parse(enumType, item));  // không cần enum Củavalue giá trị làmlà từ điển key giá trị của nguyên nhân từ enum ví dụ có thể nhìn ra, thực ra này bênnên phán đoán nócủa giá trị không tồn tại tại,mặc đ ịnhlấy trư ờng tên
            }
            return dicEnum;
        }

        #endregion
    }
}
