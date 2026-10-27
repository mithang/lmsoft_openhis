using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// SửavàokhuChẩn đoányêu cầubáovăn
    /// </summary>
    public class ModifyRyDiagnosisRequestDTO : RequestBase
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// hiện tạiĐăng nhậpngười
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Chẩn đoán nhập việnnội dung
        /// </summary>
        public List<RyDiagnosisDetail> RyDiagnosisDetails { get; set; }
    }

    /// <summary>
    /// Chẩn đoán nhập việnnội dung
    /// </summary>
    public class RyDiagnosisDetail
    {

        /// <summary>
        /// vàokhuChẩn đoán1Tên
        /// </summary>
        public string ryzdmc { get; set; }

        /// <summary>
        /// vàokhuChẩn đoán1Mã
        /// </summary>
        public string ryzddm { get; set; }

        /// <summary>
        /// vàokhuChẩn đoán1ICD10
        /// </summary>
        public string ryzdICD10 { get; set; }

        /// <summary>
        /// Chẩn đoánThứ tự  1：Chẩn đoán1 2：Chẩn đoán2  3：Chẩn đoán3
        /// </summary>
        public string zdpx { get; set; }
    }
}