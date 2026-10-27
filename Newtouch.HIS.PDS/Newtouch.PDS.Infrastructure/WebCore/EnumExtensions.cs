using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.HIS.Web.Core.Extensions
{
    public static class EnumExtensions
    {
        private static ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>> typesNameIdEnumerableResult
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>>();
        private static ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>> typesDescIdEnumerableResult
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>>();

        public static SelectList ToSelectList(this Enum enumeration)
            => new SelectList(getNameIdEnumrator(enumeration), "Value", "Text");
        public static SelectList ToDescSelectList(this Enum enumeration)
            => new SelectList(getDescIdEnumrator(enumeration), "Value", "Text");
        public static SelectList ToDescSelectList(this Enum enumeration, object selectedValue)
            => new SelectList(getDescIdEnumrator(enumeration), "Value", "Text", selectedValue);
        public static SelectList ToSomeDescSelectList(this Enum enumeration, string someValues)
        {
            var list = getDescIdEnumrator(enumeration);
            if (!string.IsNullOrEmpty(someValues))
            {
                int[] arrValue = ArrayConvertor.Convert(someValues);
                list = list.Where(e => arrValue.Contains(e.Value)).ToList();
            }
            return new SelectList(list, "Value", "Text");
        }
        public static SelectList ToSomeDescSelectList(this Enum enumeration, string someValues, object selectedValue)
        {
            var list = getDescIdEnumrator(enumeration);
            if (!string.IsNullOrEmpty(someValues))
            {
                int[] arrValue = ArrayConvertor.Convert(someValues);
                list = list.Where(e => arrValue.Contains(e.Value)).ToList();
            }
            return new SelectList(list, "Value", "Text", selectedValue);
        }

        private static IEnumerable<TextValuePair> getNameIdEnumrator(this Enum enumeration)
        {
            if (typesNameIdEnumerableResult.TryGetValue(enumeration.GetType().TypeHandle, out var result)) return result;
            result = (from Enum d in Enum.GetValues(enumeration.GetType())
                      select new TextValuePair
                      {
                          Value = (int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d)),
                          Text = d.ToString()
                      }).ToList();
            typesNameIdEnumerableResult.TryAdd(enumeration.GetType().TypeHandle, result);
            return result;
        }
        private static IEnumerable<TextValuePair> getDescIdEnumrator(this Enum enumeration)
        {
            if (typesDescIdEnumerableResult.TryGetValue(enumeration.GetType().TypeHandle, out var result)) return result;
            result = (from Enum d in Enum.GetValues(enumeration.GetType())
                      select new TextValuePair
                      {
                          Value = (int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d)),
                          Text = d.GetDescription()
                      }).ToList();
            typesDescIdEnumerableResult.TryAdd(enumeration.GetType().TypeHandle, result);
            return result;
        }
        private static class ArrayConvertor
        {
            public static int[] Convert(string s)
            {
                if (string.IsNullOrWhiteSpace(s)) return new int[0];
                return s.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.TryParse(x.Trim(), out var i) ? i : (int?)null)
                    .Where(i => i != null).Select(i => i.Value).ToArray();
            }
        }
        private class TextValuePair
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
    }
}

namespace Newtouch.HIS.Web.Core.Attributes
{
    /// <summary>Marker: skips the authorization filter (resolved via parent namespace Newtouch.HIS.Web).</summary>
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAuthorizeIgnoreAttribute : System.Attribute { }
}
