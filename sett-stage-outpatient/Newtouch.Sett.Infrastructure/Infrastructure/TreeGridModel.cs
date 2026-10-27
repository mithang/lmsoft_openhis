using Newtonsoft.Json.Linq;

namespace Newtouch.Sett.Infrastructure
{
    /// <summary>
    /// jqGrid tree-grid row model. Re-implemented for the Core port
    /// (the original lived in the Framework-only Newtouch.Common binary).
    /// </summary>
    public class TreeGridModel
    {
        public string id { get; set; }
        public bool isLeaf { get; set; }
        public string parentId { get; set; }
        public bool expanded { get; set; }
        public string entityJson { get; set; }
    }

    public static class TreeGridExtensions
    {
        /// <summary>
        /// Builds the treegrid JSON payload: { rows: [ merged entity fields + tree meta ] }.
        /// entityJson, when valid JSON, is merged into the row so the grid columns bind.
        /// </summary>
        public static string TreeGridJson(this System.Collections.Generic.List<TreeGridModel> treeList, object _)
        {
            var rows = new JArray();
            if (treeList != null)
            {
                foreach (var item in treeList)
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
                        row["entityJson"] = item.entityJson;
                    }
                    row["id"] = item.id;
                    row["parentId"] = item.parentId;
                    row["isLeaf"] = item.isLeaf;
                    row["expanded"] = item.expanded;
                    rows.Add(row);
                }
            }
            return new JObject { ["rows"] = rows }.ToString();
        }
    }
}
