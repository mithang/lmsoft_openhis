using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Newtouch.Sett.Domain
{
    // Core replacement for the Framework List<TreeViewModel>.TreeViewJson()
    // extension (binary-only Newtouch.Tools): flat id/parentId list ->
    // nested children JSON. TreeViewJson() and TreeViewJson(null) both
    // build the whole tree (roots = parentId null/empty).
    public static class TreeViewExtensions
    {
        public static string TreeViewJson(this List<TreeViewModel> list, object parentId = null)
        {
            if (list == null || list.Count == 0)
            {
                return "[]";
            }
            string root = parentId == null ? null : parentId.ToString();
            var arr = new JArray();
            foreach (var node in list.Where(n => IsRoot(n.parentId, root)))
            {
                arr.Add(BuildNode(list, node));
            }
            return arr.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static bool IsRoot(string parentId, string root)
        {
            if (root != null)
            {
                return parentId == root;
            }
            return string.IsNullOrEmpty(parentId);
        }

        private static JObject BuildNode(List<TreeViewModel> list, TreeViewModel node)
        {
            var o = JObject.FromObject(node);
            var children = list.Where(n => n.parentId == node.id).ToList();
            if (children.Count > 0)
            {
                var arr = new JArray();
                foreach (var c in children)
                {
                    arr.Add(BuildNode(list, c));
                }
                o["children"] = arr;
            }
            return o;
        }
    }
}
