using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtouch.MR.ManageSystem.Domain.Entity;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.MRMS.Web.Models
{
    /// <summary>
    /// jqGrid tree-grid row (replaces the binary FrameworkBase TreeGridModel).
    /// NOTE: the original controller sets isLeaf = hasChildren (i.e. the flag
    /// actually means "has children"), so TreeGridJson maps it to jqGrid's
    /// leaf = !isLeaf.
    /// </summary>
    public class TreeGridModel
    {
        public string id { get; set; }
        public bool isLeaf { get; set; }
        public string parentId { get; set; }
        public bool expanded { get; set; }
        public string entityJson { get; set; }
    }

    /// <summary>
    /// Tree JSON helpers (replace the binary FrameworkBase TreeGridJson /
    /// TreeViewJson extensions, which have no .NET Core build).
    /// </summary>
    public static class TreeJsonExtensions
    {
        /// <summary>
        /// jqGrid adjacency tree: each row merges its entityJson fields with
        /// tree metadata (id/parent/level/leaf/expanded) under { rows: [...] }
        /// (jqGrid's default jsonReader root).
        /// </summary>
        public static string TreeGridJson(this List<TreeGridModel> list, string parentId)
        {
            var rows = new List<JObject>();
            if (list == null) return new { rows }.ToJson();
            var byId = list.Where(t => t != null).ToDictionary(t => t.id, t => t);
            foreach (var item in list)
            {
                if (item == null) continue;
                JObject row;
                try { row = string.IsNullOrWhiteSpace(item.entityJson) ? new JObject() : JObject.Parse(item.entityJson); }
                catch { row = new JObject(); }
                row["id"] = item.id;
                row["parentId"] = item.parentId;
                row["parent"] = item.parentId;
                row["expanded"] = item.expanded;
                row["isLeaf"] = item.isLeaf;
                row["leaf"] = !item.isLeaf;
                int level = 0;
                string pid = item.parentId;
                var guard = 0;
                while (!string.IsNullOrWhiteSpace(pid) && byId.TryGetValue(pid, out var parent) && guard++ < 100)
                {
                    level++;
                    pid = parent.parentId;
                }
                row["level"] = level;
                rows.Add(row);
            }
            return new { rows }.ToJson();
        }

        /// <summary>Flat tree-select list (zTree/jqGrid simpleData use parentId).</summary>
        public static string TreeViewJson(this List<TreeViewModel> list, string parentId)
        {
            return (list ?? new List<TreeViewModel>()).ToJson();
        }
    }
}
