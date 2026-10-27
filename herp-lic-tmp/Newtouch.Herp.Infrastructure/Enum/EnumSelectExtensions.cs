using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Newtouch.Herp.Infrastructure.Enum
{
    /// <summary>
    /// ToDescSelectList replacement. The Core Lib's EnumExtensions dropped the
    /// SelectList builders (commented out); Herp views use them for dropdowns.
    /// Builds one SelectListItem per enum value (Value = int, Text = Description
    /// or name) with the receiver value marked selected, matching the original
    /// call shape (e.g. EnumWarehouseLevel.OneLevel.ToDescSelectList()).
    /// </summary>
    public static class EnumSelectExtensions
    {
        public static List<SelectListItem> ToDescSelectList(this System.Enum enumeration)
        {
            var list = new List<SelectListItem>();
            if (enumeration == null) return list;
            var type = enumeration.GetType();
            foreach (var value in System.Enum.GetValues(type))
            {
                var field = type.GetField(value.ToString());
                var desc = field?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
                list.Add(new SelectListItem
                {
                    Value = Convert.ToInt32(value).ToString(),
                    Text = desc,
                    Selected = value.Equals(enumeration)
                });
            }
            return list;
        }
    }
}
