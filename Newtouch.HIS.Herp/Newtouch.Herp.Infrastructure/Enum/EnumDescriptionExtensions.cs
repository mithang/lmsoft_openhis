using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Newtouch.Herp.Infrastructure.Enum
{
    /// <summary>
    /// ToSomeDescSelectList port (the Core Lib's EnumExtensions commented this
    /// builder out, like ToDescSelectList). Used by the BillManage approval /
    /// temporary-bill views to offer only a subset of bill-type values
    /// (e.g. "3,4" for non-level-1 warehouses). Returns SelectListItems so
    /// Html.DropDownList accepts it directly; Text is the Description
    /// attribute, so PROTECTED EnumOutOrInStorageBillType descriptions render
    /// unchanged (receipt-number keys, not display text).
    /// </summary>
    public static class EnumDescriptionExtensions
    {
        public static List<SelectListItem> ToSomeDescSelectList(this System.Enum enumeration, string someValues)
        {
            var list = new List<SelectListItem>();
            if (enumeration == null) return list;
            var type = enumeration.GetType();
            var filter = new HashSet<int>();
            if (!string.IsNullOrWhiteSpace(someValues))
            {
                foreach (var part in someValues.Split(','))
                {
                    if (int.TryParse(part.Trim(), out var v)) filter.Add(v);
                }
            }
            foreach (var value in System.Enum.GetValues(type))
            {
                int intValue = Convert.ToInt32(value);
                if (filter.Count > 0 && !filter.Contains(intValue)) continue;
                var field = type.GetField(value.ToString());
                var desc = field?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
                list.Add(new SelectListItem { Value = intValue.ToString(), Text = desc });
            }
            return list;
        }
    }
}
