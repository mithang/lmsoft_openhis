using Microsoft.AspNetCore.Mvc.Rendering;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    /// <summary>Framework ToDescSelectList: a SelectList of ALL values of the
    /// enum type, Text = Description, Value = int. (Batch B shipped a
    /// single-item stub; this restores Framework behaviour so status
    /// dropdowns show every option, not just the passed value.)</summary>
    public static class EnumSelectListExtensions
    {
        public static List<SelectListItem> ToDescSelectList(this Enum value)
        {
            return ToSomeDescSelectList(value, null);
        }

        /// <summary>Framework ToSomeDescSelectList: like ToDescSelectList but
        /// restricted to the comma-separated int values in <paramref name="includeValues"/>
        /// (null/empty = all values).</summary>
        public static List<SelectListItem> ToSomeDescSelectList(this Enum value, string includeValues)
        {
            var keep = includeValues == null ? null :
                new HashSet<string>(includeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            var list = new List<SelectListItem>();
            foreach (Enum v in Enum.GetValues(value.GetType()))
            {
                var iv = ((int)(object)v).ToString();
                if (keep != null && !keep.Contains(iv)) continue;
                list.Add(new SelectListItem { Text = v.GetDescription(), Value = iv });
            }
            return list;
        }
    }
}
