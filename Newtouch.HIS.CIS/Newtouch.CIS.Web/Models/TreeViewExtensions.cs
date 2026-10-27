using System.Collections.Generic;
using Newtouch.CIS.Web.Models;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Newtouch.CIS.Web.Models
{
    /// <summary>Port of the binary FrameworkBase TreeViewJson helper:
    /// nests a flat node list by parentId and serializes it.</summary>
    public static class TreeViewExtensions
    {
        public static string TreeViewJson(this List<TreeViewModel> list, object _ = null)
        {
            return TreeViewJsonImpl(list);
        }

        private static string TreeViewJsonImpl(List<TreeViewModel> list)
        {
            var nodes = (list ?? new List<TreeViewModel>()).Select(n => new JObject
            {
                ["id"] = n.id,
                ["value"] = n.value,
                ["text"] = n.text,
                ["title"] = n.title,
                ["parentId"] = n.parentId,
                ["hasChildren"] = n.hasChildren,
                ["isexpand"] = n.isexpand,
                ["Code"] = n.Code,
                ["complete"] = n.complete,
                ["showcheck"] = n.showcheck,
                ["Ex1"] = n.Ex1,
                ["Ex2"] = n.Ex2,
                ["img"] = n.img,
                ["checkstate"] = n.checkstate,
                ["Ex3"] = n.Ex3,
                ["Ex4"] = n.Ex4,
                ["Ex5"] = n.Ex5,
            }).ToList();
            var byId = nodes.ToDictionary(x => (string)x["id"]);
            var roots = new JArray();
            foreach (var n in nodes)
            {
                var pid = (string)n["parentId"];
                if (string.IsNullOrEmpty(pid) || !byId.ContainsKey(pid))
                {
                    roots.Add(n);
                }
                else
                {
                    var parent = byId[pid];
                    if (parent["children"] == null) parent["children"] = new JArray();
                    ((JArray)parent["children"]).Add(n);
                }
            }
            return roots.ToString(Formatting.None);
        }
    }
}

    public class TreeSelectModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string parentId { get; set; }
    }

    public static class TreeSelectExtensions
    {
        public static string TreeSelectJson(this List<TreeSelectModel> list, object _ = null)
        {
            var nodes = (list ?? new List<TreeSelectModel>()).Select(n => new JObject
            {
                ["id"] = n.id,
                ["text"] = n.text,
                ["parentId"] = n.parentId,
            }).ToList();
            var byId = nodes.ToDictionary(x => (string)x["id"]);
            var roots = new JArray();
            foreach (var n in nodes)
            {
                var pid = (string)n["parentId"];
                if (string.IsNullOrEmpty(pid) || !byId.ContainsKey(pid))
                {
                    roots.Add(n);
                }
                else
                {
                    var parent = byId[pid];
                    if (parent["children"] == null) parent["children"] = new JArray();
                    ((JArray)parent["children"]).Add(n);
                }
            }
            return roots.ToString(Formatting.None);
        }
    }

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
        /// <summary>Port of the binary TreeWhere helper: keeps matches plus
        /// all of their ancestors (by Id/ParentId reflection).</summary>
        public static System.Collections.Generic.List<T> TreeWhere<T>(this System.Collections.Generic.List<T> list, System.Func<T, bool> predicate, string parentId)
        {
            var matched = list.Where(predicate).ToList();
            var idProp = typeof(T).GetProperty("Id");
            var parentProp = typeof(T).GetProperty("ParentId") ?? typeof(T).GetProperty(parentId);
            var result = new System.Collections.Generic.List<T>(matched);
            var seen = new System.Collections.Generic.HashSet<T>(matched);
            var queue = new System.Collections.Generic.Queue<T>(matched);
            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                var pid = parentProp?.GetValue(cur)?.ToString();
                if (string.IsNullOrEmpty(pid)) continue;
                var parent = list.FirstOrDefault(x => idProp?.GetValue(x)?.ToString() == pid);
                if (parent != null && seen.Add(parent))
                {
                    result.Add(parent);
                    queue.Enqueue(parent);
                }
            }
            return list.Where(x => seen.Contains(x)).ToList();
        }

        public static string TreeGridJson(this System.Collections.Generic.List<TreeGridModel> list, object _ = null)
        {
            var nodes = (list ?? new System.Collections.Generic.List<TreeGridModel>()).Select(n => new JObject
            {
                ["id"] = n.id,
                ["isLeaf"] = n.isLeaf,
                ["parentId"] = n.parentId,
                ["expanded"] = n.expanded,
                ["entityJson"] = n.entityJson,
            }).ToList();
            var byId = nodes.ToDictionary(x => (string)x["id"]);
            var roots = new JArray();
            foreach (var n in nodes)
            {
                var pid = (string)n["parentId"];
                if (string.IsNullOrEmpty(pid) || !byId.ContainsKey(pid))
                {
                    roots.Add(n);
                }
                else
                {
                    var parent = byId[pid];
                    if (parent["children"] == null) parent["children"] = new JArray();
                    ((JArray)parent["children"]).Add(n);
                }
            }
            return roots.ToString(Formatting.None);
        }
    }
