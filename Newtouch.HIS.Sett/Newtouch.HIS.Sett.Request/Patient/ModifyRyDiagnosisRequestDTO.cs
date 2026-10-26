using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Sửa yêu cầu bản tin chẩn đoán nhập viện
    /// </summary>
    public class ModifyRyDiagnosisRequestDTO : RequestBase
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Người đăng nhập hiện tại
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Chẩn đoán nhập viện nội dung 
        /// </summary>
        public List<RyDiagnosisDetail> RyDiagnosisDetails { get; set; }
    }

    /// <summary>
    /// Chẩn đoán nhập viện nội dung 
    /// </summary>
    public class RyDiagnosisDetail
    {

        /// <summary>
        /// Tên chẩn đoán nhập viện 1
        /// </summary>
        public string ryzdmc { get; set; }

        /// <summary>
        /// Mã chẩn đoán nhập viện 1
        /// </summary>
        public string ryzddm { get; set; }

        /// <summary>
        /// ICD10 chẩn đoán nhập viện 1
        /// </summary>
        public string ryzdICD10 { get; set; }

        /// <summary>
        /// Chẩn đoán sắp xếp 1:Chẩn đoán1 2:Chẩn đoán2 3:Chẩn đoán3
        /// </summary>
        public string zdpx { get; set; }
    }
}