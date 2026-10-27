using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Newtouch.Tools
{
    /// <summary>
    /// Tree view models + JSON builders, ported from the binary Newtouch.Common.Model /
    /// Newtouch.Tools helpers used by the Framework HOSP web. TreeViewJson below is a
    /// verbatim port of the implementation found in WebAPI.Manage TreeViewBO.cs.
    /// TreeSelectJson returns a FLAT depth-first list (bindSelect iterates a flat array;
    /// levels are encoded as ideographic-space indentation in Text, which the select2
    /// change handler strips for display). TreeGridJson returns flat adjacency rows for
    /// jqGrid treeGridModel:"adjacency" (default treeReader: parent/level/isLeaf/expanded),
    /// merging each row's entityJson fields into the row object.
    /// </summary>
    public class TreeViewModel
    {
        public string id { get; set; }
        public string parentId { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public int? checkstate { get; set; }
        public bool showcheck { get; set; }
        public bool complete { get; set; }
        public bool isexpand { get; set; }
        public bool hasChildren { get; set; }
        public string img { get; set; }
        public string title { get; set; }
        public string Code { get; set; }
        public string Ex1 { get; set; }
        public string Ex2 { get; set; }
        public string Ex3 { get; set; }
        public string Ex4 { get; set; }
        public string Ex5 { get; set; }
        public string Ex6 { get; set; }
    }

    public class TreeSelectModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string parentId { get; set; }
    }

    public class TreeGridModel
    {
        public string id { get; set; }
        /// <summary>NOTE: call sites set this to hasChildren (inverted name kept for compat).</summary>
        public bool isLeaf { get; set; }
        public string parentId { get; set; }
        public bool expanded { get; set; }
        public string entityJson { get; set; }
    }

    public static class TreeExtensions
    {
        public static string TreeViewJson(this List<TreeViewModel> data, string parentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<TreeViewModel> item = data.FindAll(t => t.parentId == parentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (TreeViewModel entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"id\":\"" + entity.id + "\",");
                    strJson.Append("\"text\":\"" + entity.text.Replace("&nbsp;", "") + "\",");
                    strJson.Append("\"value\":\"" + entity.value + "\",");
                    if (entity.Code != null)
                    {
                        strJson.Append("\"Code\":\"" + entity.Code + "\",");
                    }
                    if (entity.Ex1 != null)
                    {
                        strJson.Append("\"Ex1\":\"" + entity.Ex1 + "\",");
                    }
                    if (entity.Ex2 != null)
                    {
                        strJson.Append("\"Ex2\":\"" + entity.Ex2 + "\",");
                    }
                    if (entity.Ex3 != null)
                    {
                        strJson.Append("\"Ex3\":\"" + entity.Ex3 + "\",");
                    }
                    if (entity.Ex4 != null)
                    {
                        strJson.Append("\"Ex4\":\"" + entity.Ex4 + "\",");
                    }
                    if (entity.Ex5 != null)
                    {
                        strJson.Append("\"Ex5\":\"" + entity.Ex5 + "\",");
                    }
                    if (entity.Ex6 != null)
                    {
                        strJson.Append("\"Ex6\":\"" + entity.Ex6 + "\",");
                    }
                    if (entity.title != null && !string.IsNullOrEmpty(entity.title.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"title\":\"" + entity.title.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.img != null && !string.IsNullOrEmpty(entity.img.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"img\":\"" + entity.img.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.checkstate != null)
                    {
                        strJson.Append("\"checkstate\":" + entity.checkstate + ",");
                    }
                    if (entity.parentId != null)
                    {
                        strJson.Append("\"parentnodes\":\"" + entity.parentId + "\",");
                    }
                    strJson.Append("\"showcheck\":" + entity.showcheck.ToString().ToLower() + ",");
                    strJson.Append("\"isexpand\":" + entity.isexpand.ToString().ToLower() + ",");
                    if (entity.complete == true)
                    {
                        strJson.Append("\"complete\":" + entity.complete.ToString().ToLower() + ",");
                    }
                    strJson.Append("\"hasChildren\":" + entity.hasChildren.ToString().ToLower() + ",");
                    strJson.Append("\"ChildNodes\":" + TreeViewJson(data, entity.id) + "");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }

        public static string TreeSelectJson(this List<TreeSelectModel> data, string parentId = null)
        {
            var rows = new List<object>();
            AppendSelectLevel(data, parentId, 0, rows);
            return JsonConvert.SerializeObject(rows);
        }

        private static void AppendSelectLevel(List<TreeSelectModel> data, string parentId, int depth, List<object> rows)
        {
            var level = data.Where(t => IsParentMatch(t.parentId, parentId)).ToList();
            foreach (var node in level)
            {
                string indent = string.Concat(Enumerable.Repeat("　　", depth));
                rows.Add(new { id = node.id, text = indent + (node.text ?? ""), parentId = node.parentId });
                AppendSelectLevel(data, node.id, depth + 1, rows);
            }
        }

        private static bool IsParentMatch(string itemParentId, string wantedParentId)
        {
            if (wantedParentId == null)
            {
                return string.IsNullOrEmpty(itemParentId) || itemParentId == "0";
            }
            return itemParentId == wantedParentId;
        }

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

        /// <summary>Same as TreeWhere but the id property is named by <paramref name="keyValue"/> (int keys).</summary>
        public static List<T> TreeWhereForKeyInt<T>(this List<T> list, Func<T, bool> predicate, string keyValue = "Id", string parentId = "ParentId")
            where T : class
        {
            return TreeWhereCore(list, predicate, keyValue, parentId);
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
}
