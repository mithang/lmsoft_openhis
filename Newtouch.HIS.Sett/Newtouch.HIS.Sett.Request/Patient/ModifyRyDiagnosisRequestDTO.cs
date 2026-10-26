using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Sửa入Quận chẩn đoán bản tin yêu cầu
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
        /// Khi trư ớc đ ăng nhập 者
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
        /// 入Quận chẩn đoán1 tên
        /// </summary>
        public string ryzdmc { get; set; }

        /// <summary>
        /// 入Quận chẩn đoán1 mã
        /// </summary>
        public string ryzddm { get; set; }

        /// <summary>
        /// 入Quận chẩn đoán1ICD10
        /// </summary>
        public string ryzdICD10 { get; set; }

        /// <summary>
        /// Chẩn đoán sắp xếp 1:Chẩn đoán1 2:Chẩn đoán2 3:Chẩn đoán3
        /// </summary>
        public string zdpx { get; set; }
    }
}