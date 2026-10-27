using Microsoft.AspNetCore.Mvc.Rendering;
using Newtouch.Sett.Domain;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    /// <summary>STUB: Framework ToDescSelectList built a SelectList from enum Description attributes.
    /// Returns a single-item list (the enum value itself) so DropDownList helpers compile/render.</summary>
    public static class EnumSelectListExtensions
    {
        public static List<SelectListItem> ToDescSelectList(this Enum value)
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = value.GetDescription(), Value = ((int)(object)value).ToString() }
            };
        }
    }
}
