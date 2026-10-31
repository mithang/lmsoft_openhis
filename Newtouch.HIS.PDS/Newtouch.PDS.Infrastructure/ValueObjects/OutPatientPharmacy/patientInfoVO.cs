using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin phát thuốc ngoại trú
    /// </summary>
    public class OutpatienDrugDeliveryInfo
    {
        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tài khoản nhân viên thao tác
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Thông tin bệnh nhân phát thuốc
        /// </summary>
        public List<patientInfoVO> PatientInfo { get; set; }
    }


    /// <summary>
    /// Thông tin bệnh nhân 
    /// </summary>
    [Serializable]
    public class patientInfoVO
    {
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Thời gian thu phí
        /// </summary>
        public string ShowSfsj { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        public string cfnm { get; set; }

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1: Có
        /// 2: Không
        /// </summary>
        public int? sfcl { get; set; }

        public string ypdm { get; set; }
        public string zt { get; set; }
        public string ysshyj { get; set; }
    }
}
