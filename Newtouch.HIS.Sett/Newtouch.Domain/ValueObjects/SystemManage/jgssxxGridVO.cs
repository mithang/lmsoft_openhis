namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class jgssxxGridVO
    {
        public string Id { get; set; }
        //Tên trạm
        public string zdmc { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public decimal zsr { get; set; }
        public decimal? gdcb { get; set; }
        public decimal? grscb { get; set; }
        //Thu nhập cơ sở
        public decimal? jgss { get; set; }
        //GRSThu nhập
        public decimal? grsss { get; set; }
        public string shzt { get; set; }
        public decimal? hssr { get; set; }
        public decimal? ce { get; set; }
    }
}
