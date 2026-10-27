using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Core.Extensions
{
    /// <summary>
    /// Port of Newtouch.HIS.Web.Core Extensions/EnumExtensions.cs (Framework):
    /// System.Web.Mvc.SelectList -> Microsoft.AspNetCore.Mvc.Rendering.SelectList,
    /// GetDescription() now comes from NewtouchHIS.Lib.Base.Extension.
    /// TextValuePair is private-nested exactly as in the original; ArrayConvertor is a
    /// local minimal port (original lived in binary Tools).
    /// </summary>
    public static class EnumExtensions
    {
        private static ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>> typesNameIdEnumerableResult
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>>();

        private static ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>> typesDescIdEnumerableResult
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>>();

        public static SelectList ToSelectList(this Enum enumeration)
        {
            return new SelectList(getNameIdEnumrator(enumeration), "Value", "Text");
        }

        public static SelectList ToDescSelectList(this Enum enumeration)
        {
            return new SelectList(getDescIdEnumrator(enumeration), "Value", "Text");
        }

        public static SelectList ToDescSelectList(this Enum enumeration, object selectedValue)
        {
            return new SelectList(getDescIdEnumrator(enumeration), "Value", "Text", selectedValue);
        }

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
            IEnumerable<TextValuePair> result;
            if (typesNameIdEnumerableResult.TryGetValue(enumeration.GetType().TypeHandle, out result))
            {
                return result;
            }
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
            IEnumerable<TextValuePair> result;
            if (typesDescIdEnumerableResult.TryGetValue(enumeration.GetType().TypeHandle, out result))
            {
                return result;
            }
            result = (from Enum d in Enum.GetValues(enumeration.GetType())
                      select new TextValuePair
                      {
                          Value = (int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d)),
                          Text = d.GetDescription()
                      }).ToList();
            typesDescIdEnumerableResult.TryAdd(enumeration.GetType().TypeHandle, result);
            return result;
        }

        private class TextValuePair
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
    }
}

namespace Newtouch.Tools
{
    /// <summary>Minimal local port of binary Tools.ArrayConvertor (only Convert is used).</summary>
    public static class ArrayConvertor
    {
        public static int[] Convert(string values)
        {
            if (string.IsNullOrWhiteSpace(values)) return new int[0];
            var list = new List<int>();
            foreach (var part in values.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(part.Trim(), out int v)) list.Add(v);
            }
            return list.ToArray();
        }
    }
}
