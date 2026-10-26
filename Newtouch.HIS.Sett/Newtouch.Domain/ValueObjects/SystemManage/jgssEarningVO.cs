namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// Bảng thống kê thu chi của trạm Thông tin thu nhập
    /// </summary>
    public class jgssEarningVO
    {
        public string Id { get; set; }
        public string siteId { get; set; }
        public string zdmc { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public decimal jgss { get; set; }
        public decimal grsss { get; set; }
        public string shzt { get; set; }
        public decimal zsr { get; set; }
        public decimal mzsfzje { get; set; }
        public decimal zysfzje { get; set; }
        public string shr { get; set; }
        public string cjr { get; set; }
        public string jgssfdbl { get; set; }
    }

    public class jgssAttachmentVO {
        public string Id { get; set; }
        public string fjmc { get; set; }
        public string ContentType { get; set; }
        public string fjPath { get; set; }
    }
}
