using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NewtouchHIS.Lib.Base.Extension
{
    public static class EnumExtensions
    {
        /// <summary>
        /// lấy đặc tính (DisplayAttribute) tên của；nếu chưa dùng đặc tính này，thì trả về tên của enum。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this System.Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return attrs.Length > 0 ? attrs[0].Name : enumValue.ToString();
        }

        /// <summary>
        /// lấy đặc tính (DisplayAttribute) mô tả của；nếu chưa dùng đặc tính này，thì trả về tên của enum。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayDescription(this System.Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// lấy đặc tính (DescriptionAttribute) mô tả của；nếu chưa dùng đặc tính này，thì trả về tên của enum。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this System.Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// lấy trực tiếp đặc tính（nhẹ hơn、dễ sử dụng hơn，không cần đóng gói“lấy từng đặc tính tùy chỉnh”phương thức mở rộng của）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(this System.Enum enumValue) where T : Attribute
        {
            Type type = enumValue.GetType();
            MemberInfo[] memInfo = type.GetMember(enumValue.ToString());
            object[] attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// lấy thông tin mục enum theo kiểu enum
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<EnumItemInfo> getEnumNameValueDescInfo(this Type t)
        {
            var result = (from Enum d in Enum.GetValues(t)
                          select new EnumItemInfo()
                          {
                              Name = d.ToString(),
                              Value = (int)Enum.Parse(t, Enum.GetName(t, d)),
                              Desc = d.GetDescription()
                          }).ToList();
            return result;
        }

        /// <summary>
        /// （dùng để hiển thị danh sách thả xuống của enum）lấy SelectList Text：Name Value：Id
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        //public static SelectList ToSelectList(this System.Enum enumeration)
        //{
        //    return new SelectList(getNameIdEnumrator(enumeration), "Value", "Text");
        //}

        ///// <summary>
        ///// （dùng để hiển thị danh sách thả xuống của enum）lấy SelectList Text：Desc Value：Id
        ///// </summary>
        ///// <param name="enumeration"></param>
        ///// <returns></returns>
        //public static SelectList ToDescSelectList(this System.Enum enumeration)
        //{
        //    return new SelectList(getDescIdEnumrator(enumeration), "Value", "Text");
        //}

        ///// <summary>
        ///// （dùng để hiển thị danh sách thả xuống của enum）lấy SelectList Text：Desc Value：Id
        ///// </summary>
        ///// <param name="enumeration"></param>
        ///// <param name="selectedValue">mục đã chọn</param>
        ///// <returns></returns>
        //public static SelectList ToDescSelectList(this System.Enum enumeration, object selectedValue)
        //{
        //    return new SelectList(getDescIdEnumrator(enumeration), "Value", "Text", selectedValue);
        //}

        ///// <summary>
        ///// （dùng để hiển thị danh sách thả xuống của enum）cung cấp một phần tùy chọn  Text：Desc Value：Id
        ///// </summary>
        ///// <param name="enumeration"></param>
        ///// <param name="someValues"></param>
        ///// <returns></returns>
        //public static SelectList ToSomeDescSelectList(this System.Enum enumeration, string someValues)
        //{
        //    var list = getDescIdEnumrator(enumeration);
        //    if (!string.IsNullOrEmpty(someValues))
        //    {
        //        int[] arrValue = ArrayConvertor.Convert(someValues);
        //        list = list.Where(e => arrValue.Contains(e.Value)).ToList();
        //    }
        //    return new SelectList(list, "Value", "Text");
        //}

        ///// <summary>
        ///// （dùng để hiển thị danh sách thả xuống của enum）cung cấp một phần tùy chọn  Text：Desc Value：Id
        ///// </summary>
        ///// <param name="enumeration"></param>
        ///// <param name="someValues"></param>
        ///// <param name="selectedValue">mục đã chọn</param>
        ///// <returns></returns>
        //public static SelectList ToSomeDescSelectList(this System.Enum enumeration, string someValues, object selectedValue)
        //{
        //    var list = getDescIdEnumrator(enumeration);
        //    if (!string.IsNullOrEmpty(someValues))
        //    {
        //        int[] arrValue = ArrayConvertor.Convert(someValues);
        //        list = list.Where(e => arrValue.Contains(e.Value)).ToList();
        //    }
        //    return new SelectList(list, "Value", "Text", selectedValue);
        //}

        #region private
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="enumeration"></param>
        //private static IEnumerable<TextValuePair> getNameIdEnumrator(this System.Enum enumeration)
        //{
        //    IEnumerable<TextValuePair> result;
        //    if (typesNameIdEnumerableResult.TryGetValue(enumeration.GetType().TypeHandle, out result))
        //    {
        //        return result;
        //    }
        //    result = (from Enum d in Enum.GetValues(enumeration.GetType())
        //              select new TextValuePair
        //              {
        //                  Value = (int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d)),
        //                  Text = d.ToString()
        //              }).ToList();
        //    typesNameIdEnumerableResult.TryAdd(enumeration.GetType().TypeHandle, result);
        //    return result;
        //}
        #endregion
        public class TextValuePair
        {
            /// <summary>
            /// Value
            /// </summary>
            public int Value { get; set; }

            /// <summary>
            /// Text
            /// </summary>
            public string Text { get; set; }
        }
        public class EnumItemInfo
        {
            public string Name { get; set; }
            public int Value { get; set; }
            public string? Desc { get; set; }

        }
    }
}