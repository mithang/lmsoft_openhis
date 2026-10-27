using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtouch.Sett.Domain
{
    /// <summary>
    /// Core reconstruction of the Framework Newtouch.Common TreeWhere()
    /// extension (binary-only): filter a flat tree list by predicate, keeping
    /// matched nodes plus their ancestors so the tree stays connected.
    /// The id property is "Id" when present, else "brxzbh" (patient-nature
    /// trees); the parent property name comes from the parentId argument.
    /// With stub (empty) data this is just a filter; documented as a
    /// reconstruction, not a verified Framework port.
    /// </summary>
    public static class TreeWhereExtensions
    {
        public static List<T> TreeWhere<T>(this List<T> list, Func<T, bool> predicate, string parentId = "ParentId")
        {
            if (list == null) return new List<T>();
            var matched = list.Where(predicate).ToList();
            if (matched.Count == 0) return matched;
            var idProp = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty("brxzbh");
            var parentProp = typeof(T).GetProperty(parentId);
            if (idProp == null || parentProp == null) return matched;
            var keep = new HashSet<T>(matched);
            var ids = new HashSet<string>(matched.Select(m => Convert.ToString(idProp.GetValue(m))));
            bool added;
            do
            {
                added = false;
                foreach (var item in list)
                {
                    if (keep.Contains(item)) continue;
                    var pid = Convert.ToString(parentProp.GetValue(item));
                    if (pid != null && ids.Contains(pid))
                    {
                        keep.Add(item);
                        ids.Add(Convert.ToString(idProp.GetValue(item)));
                        added = true;
                    }
                }
            } while (added);
            return list.Where(keep.Contains).ToList();
        }
    }
}
