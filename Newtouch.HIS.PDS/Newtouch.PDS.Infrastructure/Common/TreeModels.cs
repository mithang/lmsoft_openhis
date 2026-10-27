using System.Collections.Generic;
using Newtonsoft.Json;

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
