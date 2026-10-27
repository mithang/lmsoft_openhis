using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace Newtouch.EMR.Web.Models
{
    /// <summary>
    /// jqGrid adjacency tree-grid row, ported from the FrameworkBase binary
    /// TreeGridModel (no source in this repo). Field usage in this app:
    /// id, parentId, expanded, isLeaf (see TreeGridJson note), entityJson.
    /// </summary>
    public class TreeGridModel
    {
        public string id { get; set; }
        public string parentId { get; set; }
        public bool expanded { get; set; }
        public bool isLeaf { get; set; }
        public string entityJson { get; set; }
    }

    /// <summary>
    /// Serializes a flat TreeGridModel list into the jqGrid adjacency shape.
    /// Each row merges its entityJson fields with the tree fields (id, parentId,
    /// level, expanded, isLeaf, loaded). NOTE on isLeaf: every call site in this
    /// app assigns `isLeaf = hasChildren` (inverted vs jqGrid semantics, where
    /// isLeaf=true means "no children"). The original serializer must have
    /// negated it back — otherwise root rows (isLeaf=true, expanded=false)
    /// could never be expanded. This implementation serializes
    /// isLeaf = !model.isLeaf so the trees expand correctly.
    /// The parentId argument of the original signature selected a root subset;
    /// all call sites pass null, so it is ignored and the full flat list is
    /// returned (adjacency grids need the whole list).
    /// </summary>
    public static class TreeGridJsonExtensions
    {
        public static string TreeGridJson(this List<TreeGridModel> data, string parentId)
        {
            if (data == null)
            {
                return "[]";
            }
            var byId = new Dictionary<string, TreeGridModel>();
            foreach (var row in data)
            {
                if (row?.id != null && !byId.ContainsKey(row.id))
                {
                    byId[row.id] = row;
                }
            }
            var sb = new StringBuilder();
            sb.Append("[");
            var first = true;
            foreach (var row in data)
            {
                if (row == null)
                {
                    continue;
                }
                JObject obj;
                try
                {
                    obj = string.IsNullOrWhiteSpace(row.entityJson)
                        ? new JObject()
                        : JObject.Parse(row.entityJson);
                }
                catch
                {
                    obj = new JObject();
                }
                obj["id"] = row.id;
                obj["parentId"] = row.parentId != null ? new JValue(row.parentId) : JValue.CreateNull();
                obj["level"] = LevelOf(row, byId);
                obj["expanded"] = row.expanded;
                obj["isLeaf"] = !row.isLeaf;
                obj["loaded"] = true;
                if (!first)
                {
                    sb.Append(",");
                }
                first = false;
                sb.Append(obj.ToString(Formatting.None));
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static int LevelOf(TreeGridModel row, Dictionary<string, TreeGridModel> byId)
        {
            var level = 0;
            var seen = new HashSet<string>();
            var current = row;
            while (current?.parentId != null && byId.TryGetValue(current.parentId, out var parent)
                && seen.Add(current.parentId))
            {
                level++;
                current = parent;
            }
            return level;
        }
    }
}
