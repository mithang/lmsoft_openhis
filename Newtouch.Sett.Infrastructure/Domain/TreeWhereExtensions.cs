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
    /// NX10-002 (D-NX10-002-AMEND): the previous body kept matched nodes
    /// plus their DESCENDANTS (code contradicted this comment and the
    /// independent HOSP.Web/Shims TreeWhereCore port, and defeated the
    /// authorization filter in ModuleController by exposing unauthorised
    /// children of an authorised parent). Now ancestor-keeping, mirroring
    /// HOSP TreeWhereCore (walk ParentId up to the root, guard 10000).
    /// </summary>
    public static class TreeWhereExtensions
    {
        public static List<T> TreeWhere<T>(this List<T> list, Func<T, bool> predicate, string parentId = "ParentId")
        {
            if (list == null) return new List<T>();
            var idProp = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty("brxzbh");
            var parentProp = typeof(T).GetProperty(parentId);
            if (idProp == null || parentProp == null) return list.Where(predicate).ToList();
            var byId = new Dictionary<object, T>();
            foreach (var item in list)
            {
                var id = idProp.GetValue(item);
                if (id != null && !byId.ContainsKey(id)) byId[id] = item;
            }
            var keep = new HashSet<T>();
            foreach (var item in list.Where(predicate))
            {
                var cur = item;
                int guard = 0;
                while (cur != null && keep.Add(cur) && guard++ < 10000)
                {
                    var p = parentProp.GetValue(cur);
                    if (p == null) break;
                    if (p is string ps && string.IsNullOrEmpty(ps)) break;
                    if (!byId.TryGetValue(p, out var parent)) break;
                    cur = parent;
                }
            }
            return list.Where(x => keep.Contains(x)).ToList();
        }
    }
}
