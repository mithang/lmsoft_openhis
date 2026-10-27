namespace Newtouch.EMR.Web.Areas.MedicalRecordManage
{
    /// <summary>
    /// Demo-page file item (FullName/Name/Selected), ported from the original
    /// EMR Domain FileItem. Only used by the unrouted MedicalRecordEditDemo
    /// demo view; kept so the view still compiles.
    /// </summary>
    public class FileItem
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
