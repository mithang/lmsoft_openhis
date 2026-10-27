namespace NewtouchHIS.Domain.InterfaceObjets
{
    public class PacsIndexVO
    {
        /// <summary>
        /// 门诊：Số ngoại trú  住院：Số nhập viện
        /// </summary>
        public string? ywlsh { get; set; }
        public string? sqdh { get; set; }
        /// <summary>
        /// Enummzzybz 1 门诊 2 住院
        /// </summary>
        public int? mzzybz { get; set; }

    }
}
