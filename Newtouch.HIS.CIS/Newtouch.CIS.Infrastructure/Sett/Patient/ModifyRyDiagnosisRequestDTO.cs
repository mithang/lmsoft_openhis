using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Sửavàokhu Chẩn đoányêu cầubáovăn
    /// </summary>
    public class ModifyRyDiagnosisRequestDTO : RequestBase
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// hiện tại Đăng nhậpngười
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
        /// vàokhu Chẩn đoán1Tên
        /// </summary>
        public string ryzdmc { get; set; }

        /// <summary>
        /// vàokhu Chẩn đoán1Mã
        /// </summary>
        public string ryzddm { get; set; }

        /// <summary>
        /// vàokhu Chẩn đoán1ICD10
        /// </summary>
        public string ryzdICD10 { get; set; }

        /// <summary>
        /// Chẩn đoán Thứ tự  1:Chẩn đoán1 2:Chẩn đoán2  3:Chẩn đoán3
        /// </summary>
        public string zdpx { get; set; }
    }
}