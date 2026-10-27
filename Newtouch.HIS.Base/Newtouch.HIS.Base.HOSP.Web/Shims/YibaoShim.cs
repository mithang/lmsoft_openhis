using System.Collections.Generic;

namespace newtouchyibao
{
    /// <summary>
    /// STUB for the Yibao (medical-insurance) integration library
    /// (newtouchyibao.YibaoUniteInterface), which has no source in this repo and no
    /// counterpart service in this Linux deployment (the Guizhou insurance front
    /// server is not present). Mcatalogrl deliberately returns a FAILURE result so the
    /// two call sites (SysMedicineController.YibaoUploadApi, SysChargeItemController
    /// Yibao sync) surface an explicit "insurance gateway not connected" error instead
    /// of a fake success. Result shape (Code / Data[].TPCODE / ErrorMsg) matches the
    /// fields the call sites read.
    /// </summary>
    public class McatalogResultItem
    {
        public int TPCODE { get; set; }
    }

    public class McatalogResult
    {
        public int Code { get; set; }
        public List<McatalogResultItem> Data { get; set; }
        public string ErrorMsg { get; set; }
    }

    public static class YibaoUniteInterface
    {
        public static McatalogResult Mcatalogrl(string p1, string p2, string p3, string p4, string p5,
            string p6, string p7, string p8, string p9, object p10, object p11)
        {
            return new McatalogResult
            {
                Code = -1,
                Data = new List<McatalogResultItem>(),
                ErrorMsg = "Chưa kết nối cổng bảo hiểm y tế (dịch vụ Yibao chưa triển khai trên Linux)"
            };
        }
    }
}
