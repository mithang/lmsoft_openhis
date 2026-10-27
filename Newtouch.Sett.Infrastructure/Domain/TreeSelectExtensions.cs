using System.Collections.Generic;
using System.Text.Json;

namespace Newtouch.Sett.Domain
{
    /// <summary>
    /// Core replacement for the Framework Newtouch.Common TreeSelectJson()
    /// extension. The only in-app consumer (NonTreatmentItem charge-category
    /// dropdown) feeds framework-ui.js bindSelect, a FLAT select2 dropdown
    /// that iterates data[i].id / data[i].text -- so this serializes the
    /// flat list (id/text/parentId preserved), not a nested tree.
    /// </summary>
    public static class TreeSelectExtensions
    {
        public static string TreeSelectJson(this List<TreeSelectModel> data, string parentId = null)
        {
            return JsonSerializer.Serialize(data ?? new List<TreeSelectModel>());
        }
    }
}
