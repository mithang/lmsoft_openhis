using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Newtouch.Domain.Entity.Outpatient;

namespace Newtouch.Domain.BusinessObjects
{
    public class MRTemplateBO
    {
        #region mẫu nội dung
        /// <summary>
        /// 
        /// </summary>
        public string mbmc { get; set; }
        /// <summary>
        /// chính bảo
        /// </summary>
        public string zs { get; set; }

        /// <summary>
        /// hiện bệnh sử
        /// </summary>
        public string xbs { get; set; }

        /// <summary>
        /// Tiền sử
        /// </summary>
        public string jws { get; set; }

        /// <summary>
        /// tra thể
        /// </summary>
        public string ct { get; set; }
        /// <summary>
        /// chỗ lý
        /// </summary>
        public string clfa { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public List<WMDiagnosisHtmlVO> xyzdList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<TCMDiagnosisHtmlVO> zyzdList { get; set; }
        
        public List<CfTemplateGroupPackageEntity> mbztList { get; set; }
        /// <summary>
        /// Tháng tiền sử bệnh
        /// </summary>
        public string yjs { get; set; }
        /// <summary>
        /// qua Tiền sử dị ứng
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// Hôn nhân
        /// </summary>
        public string hy { get; set; }
    }

    public class MRTemplateListBO
    {
        public string mbId { get; set; }
        public string mbmc { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }
}
