using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Newtouch.Common
{
    public class TreeSelectModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string parentId { get; set; }
    }

    public static class TreeSelectExtensions
    {
        public static string TreeSelectJson(this List<TreeSelectModel> list, string parentId)
        {
            return JsonConvert.SerializeObject(list ?? new List<TreeSelectModel>());
        }
    }
}

    public class TreeViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        // QA-phase2: UserController (port tu mau HOSP) gan staffTree.Code =
        // UserCode; model PDS thieu thuoc tinh nay nen controller khong build
        // duoc va bi Remove khoi csproj. Them 1 dong, khong doi cac user khac.
        public string Code { get; set; }
        // NX2-001: RoleAuthorizeController gan tree.img = item.Icon; model PDS
        // thieu thuoc tinh nay (Base Web TreeShims.TreeViewModel co san img).
        // Them 1 dong theo dung tien le Code phia tren.
        public string img { get; set; }
        public string parentId { get; set; }
        public bool isexpand { get; set; }
        public bool complete { get; set; }
        public bool showcheck { get; set; }
        public int checkstate { get; set; }
        public bool hasChildren { get; set; }
        public string Ex1 { get; set; }
        public string Ex2 { get; set; }
        public string Ex3 { get; set; }
        public string Ex4 { get; set; }
        public string Ex5 { get; set; }
        public string Ex6 { get; set; }
    }

    public static class TreeViewExtensions
    {
        public static string TreeViewJson(this List<TreeViewModel> list, string parentId)
        {
            return JsonConvert.SerializeObject(list ?? new List<TreeViewModel>());
        }
    }

    // NX2-001: port trung thuc tu Newtouch.HIS.Base Web Shims/TreeShims.cs
    // (namespace Newtouch.Tools ben do) — PDS khong co TreeGridModel,
    // TreeWhere, TreeGridJson nen ModuleController khong build duoc.
    // Dat o day theo dung kieu file nay (TreeViewModel cung o global
    // namespace, khong phai Newtouch.Common).
    public class TreeGridModel
    {
        public string id { get; set; }
        /// <summary>NOTE: call sites set this to hasChildren (inverted name kept for compat).</summary>
        public bool isLeaf { get; set; }
        public string parentId { get; set; }
        public bool expanded { get; set; }
        public string entityJson { get; set; }
    }

    public static class TreeGridExtensions
    {
        public static string TreeGridJson(this List<TreeGridModel> data, string parentId = null)
        {
            // Depth of each row, computed from the flat parent chain (cycle-safe).
            var byId = new Dictionary<string, TreeGridModel>();
            foreach (var m in data)
            {
                if (m.id != null && !byId.ContainsKey(m.id)) byId[m.id] = m;
            }
            var rows = new List<JObject>();
            foreach (var m in data)
            {
                JObject row;
                try
                {
                    row = string.IsNullOrWhiteSpace(m.entityJson) ? new JObject() : JObject.Parse(m.entityJson);
                }
                catch
                {
                    row = new JObject();
                }
                int level = 0;
                var cur = m;
                var guard = 0;
                while (cur != null && !string.IsNullOrEmpty(cur.parentId) && cur.parentId != "0"
                    && byId.TryGetValue(cur.parentId, out var parent) && guard++ < 10000)
                {
                    level++;
                    cur = parent;
                }
                row["id"] = m.id;
                row["parentId"] = m.parentId;
                row["parent"] = m.parentId;
                row["level"] = level;
                // Model.isLeaf actually holds hasChildren at every call site -> invert for jqGrid.
                row["isLeaf"] = !m.isLeaf;
                row["expanded"] = m.expanded;
                rows.Add(row);
            }
            return JsonConvert.SerializeObject(rows);
        }

        /// <summary>
        /// Keyword-filtered tree pruning: keeps nodes matching the predicate plus all
        /// of their ancestors (so the tree path stays intact), in original list order.
        /// Id property is "Id", parent property is named by <paramref name="parentId"/>.
        /// </summary>
        public static List<T> TreeWhere<T>(this List<T> list, Func<T, bool> predicate, string parentId = "ParentId")
            where T : class
        {
            return TreeWhereCore(list, predicate, "Id", parentId);
        }

        private static List<T> TreeWhereCore<T>(List<T> list, Func<T, bool> predicate, string idName, string parentName)
            where T : class
        {
            PropertyInfo idProp = typeof(T).GetProperty(idName);
            PropertyInfo parentProp = typeof(T).GetProperty(parentName);
            if (idProp == null || parentProp == null)
            {
                return list.Where(predicate).ToList();
            }
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
