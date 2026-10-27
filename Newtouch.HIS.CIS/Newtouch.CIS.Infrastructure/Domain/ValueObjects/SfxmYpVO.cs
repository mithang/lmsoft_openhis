using System.Collections.Generic;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Charge-item + drug selection result (originally FrameworkBase
    /// binary). Minimal surface derived from CIS usage: FormatRpDetail
    /// reads dw / sfxmmc off the JSON-converted DrugDataEx.
    /// </summary>
    public class SfxmYpSelectResultVO
    {
        public string dw { get; set; }
        public string sfxmmc { get; set; }
    }

    /// <summary>
    /// Filter DTO for the charge-item + drug selection query (originally
    /// FrameworkBase binary). Properties are exactly those set by
    /// OutpatientCmmManagerApp.FormatRpDetail.
    /// </summary>
    public class SelectSfxmYpFilterDTO
    {
        public int topCount { get; set; }
        public string orgId { get; set; }
        public string mzzybz { get; set; }
        public string dllb { get; set; }
        public string sfdllx { get; set; }
        public string keyword { get; set; }
        public string dlCode { get; set; }
        public bool isContansChildDl { get; set; }
        public bool useypckflag { get; set; }
        public string ypyfbmCode { get; set; }
        public bool containyp0ck { get; set; }
        public bool onlyybflag { get; set; }
        public bool isQyKssKZ { get; set; }
        public int? qxjb { get; set; }
    }
}

namespace Newtouch.CIS.Compat
{
    /// <summary>
    /// STUB for FrameworkBase BaseDataDmnService.SelectSfxmYp (charge-item +
    /// drug selection query, binary-only). Returns an empty list.
    /// Unreachable in practice: the only caller (FormatRpDetail) runs after
    /// a TcmHis08 SOAP pull, whose WCF client is itself stubbed to throw.
    /// To restore: reimplement the selection query against Base tables
    /// (Sys_ItemsDetail / drug catalog) with SqlSugar.
    /// </summary>
    public static class CmmStub
    {
        public static T Throw<T>(string message) { throw new FailedException(message); }
        public static List<Newtouch.Domain.ValueObjects.SfxmYpSelectResultVO> SelectSfxmYp(
            Newtouch.Domain.ValueObjects.SelectSfxmYpFilterDTO filter)
        {
            return new List<Newtouch.Domain.ValueObjects.SfxmYpSelectResultVO>();
        }
    }
}
