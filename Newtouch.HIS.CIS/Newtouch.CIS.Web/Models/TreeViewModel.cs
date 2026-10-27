namespace Newtouch.CIS.Web.Models
{
    /// <summary>Tree node model (ported from binary FrameworkBase Common.Model).</summary>
    public class TreeViewModel
    {
        public string id { get; set; }
        public string value { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public string parentId { get; set; }
        public bool hasChildren { get; set; }
        public bool isexpand { get; set; }
        public string Code { get; set; }
        public bool complete { get; set; }
        public bool showcheck { get; set; }
        public string Ex1 { get; set; }
        public string Ex2 { get; set; }
        public string img { get; set; }
        public int checkstate { get; set; }
        public string Ex3 { get; set; }
        public string Ex4 { get; set; }
        public string Ex5 { get; set; }
        public string Ex6 { get; set; }
    }
}
