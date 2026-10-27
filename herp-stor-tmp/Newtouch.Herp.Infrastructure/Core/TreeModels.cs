using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Newtouch.Herp.Infrastructure
{
    /// <summary>
    /// Tree models + JSON builders replacing the FrameworkBase binary helpers
    /// (TreeGridModel/TreeViewModel/TreeSelectModel and the TreeGridJson/
    /// TreeViewJson/TreeSelectJson/TreeWhere extensions, no source).
    /// TreeGridJson merges each row's entityJson with the tree fields jqGrid's
    /// adjacency treegrid needs: id, parent (=parentId), level (computed by
    /// walking parents), expanded, loaded, and isLeaf. NOTE on isLeaf: the
    /// original controllers set TreeGridModel.isLeaf = hasChildren (the field
    /// actually carries "has children"), so TreeGridJson inverts it to jqGrid's
    /// leaf semantics (leaf = no children); without the inversion every parent
    /// node would render as a leaf.
    /// </summary>
    public class TreeGridModel
    {
        public string id { get; set; }
        public bool isLeaf { get; set; }
        public string parentId { get; set; }
        public bool expanded { get; set; }
        public string entityJson { get; set; }
    }

    public class TreeViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public string parentId { get; set; }
        public bool isexpand { get; set; }
        public bool complete { get; set; }
        public bool showcheck { get; set; }
        public bool hasChildren { get; set; }
        public int checkstate { get; set; }
    }

    public class TreeSelectModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string parentId { get; set; }
    }

    public static class TreeExtensions
    {
        private static PropertyInfo FindProp(Type type, string name)
        {
            return type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                ?? type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        private static string StrProp(object obj, string name)
        {
            if (obj == null) return null;
            var prop = FindProp(obj.GetType(), name);
            var val = prop?.GetValue(obj);
            return val == null ? null : val.ToString();
        }

        /// <summary>Keeps matching nodes plus all of their ancestors (original order).</summary>
        public static List<T> TreeWhere<T>(this List<T> list, Func<T, bool> predicate, string parentId = "ParentId")
        {
            if (list == null) return new List<T>();
            var byId = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in list)
            {
                var id = StrProp(item, "Id");
                if (!string.IsNullOrWhiteSpace(id) && !byId.ContainsKey(id)) byId[id] = item;
            }
            var keep = new HashSet<T>();
            foreach (var item in list)
            {
                if (!predicate(item)) continue;
                var cur = item;
                var guard = 0;
                while (cur != null && guard++ < 1000)
                {
                    keep.Add(cur);
                    var pid = StrProp(cur, parentId);
                    if (string.IsNullOrWhiteSpace(pid) || !byId.TryGetValue(pid, out var parent)) break;
                    cur = parent;
                }
            }
            return list.Where(keep.Contains).ToList();
        }

        public static string TreeGridJson(this List<TreeGridModel> list, object unused = null)
        {
            var rows = new JArray();
            var byId = new Dictionary<string, TreeGridModel>(StringComparer.OrdinalIgnoreCase);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (!string.IsNullOrWhiteSpace(item.id) && !byId.ContainsKey(item.id)) byId[item.id] = item;
                }
                foreach (var item in list)
                {
                    JObject row;
                    try
                    {
                        row = string.IsNullOrWhiteSpace(item.entityJson)
                            ? new JObject()
                            : JObject.Parse(item.entityJson);
                    }
                    catch
                    {
                        row = new JObject();
                    }
                    row["id"] = item.id;
                    row["parent"] = item.parentId;
                    row["parentId"] = item.parentId;
                    row["expanded"] = item.expanded;
                    // Controller's isLeaf actually means "has children": invert to jqGrid semantics.
                    row["isLeaf"] = !item.isLeaf;
                    row["loaded"] = true;
                    int level = 0;
                    var pid = item.parentId;
                    var guard = 0;
                    while (!string.IsNullOrWhiteSpace(pid) && byId.TryGetValue(pid, out var parent) && guard++ < 1000)
                    {
                        level++;
                        pid = parent.parentId;
                    }
                    row["level"] = level;
                    rows.Add(row);
                }
            }
            return JsonConvert.SerializeObject(new { rows });
        }

        public static string TreeViewJson(this List<TreeViewModel> list, object unused = null)
        {
            return JsonConvert.SerializeObject(list ?? new List<TreeViewModel>());
        }

        public static string TreeSelectJson(this List<TreeSelectModel> list, object unused = null)
        {
            return JsonConvert.SerializeObject(list ?? new List<TreeSelectModel>());
        }
    }
}
