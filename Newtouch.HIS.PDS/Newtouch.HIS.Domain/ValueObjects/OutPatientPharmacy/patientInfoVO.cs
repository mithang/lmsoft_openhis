using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// phát thuốc ngoại trú thông tin
    /// </summary>
    public class OutpatienDrugDeliveryInfo
    {
        /// <summary>
        /// bộ phận nhà thuốc mã
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// người thao tác tài khoản
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// phát thuốc bệnh nhân thông tin
        /// </summary>
        public List<patientInfoVO> PatientInfo { get; set; }
    }


    /// <summary>
    /// bệnh nhân thông tin 
    /// </summary>
    [Serializable]
    public class patientInfoVO
    {
        /// <summary>
        /// số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// thời gian thu phí
        /// </summary>
        public string ShowSfsj { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        public string cfnm { get; set; }

        /// <summary>
        /// mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// có tách lẻ không
        /// 1: có
        /// 2: không
        /// </summary>
        public int? sfcl { get; set; }

        public string ypdm { get; set; }
        public string zt { get; set; }
        public string ysshyj { get; set; }
    }
}
