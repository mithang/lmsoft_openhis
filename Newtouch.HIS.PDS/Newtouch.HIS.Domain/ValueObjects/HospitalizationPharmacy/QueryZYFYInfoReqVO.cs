using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// tra cứu nội trú phí thông tin phát thuốc
    /// </summary>
    public class QueryZYFYInfoReqVO
    {
        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// nội trú nhà thuốc
        /// </summary>
        public string zyyf { get; set; }

        /// <summary>
        /// khu bệnh mã
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// số nội trú
        /// </summary>
        public string Zyh { get; set; }

        /// <summary>
        /// thời gian bắt đầu
        /// </summary>
        public DateTime Kssj { get; set; }

        /// <summary>
        /// thời gian kết thúc
        /// </summary>
        public DateTime Jssj { get; set; }

        /// <summary>
        /// trạng thái phát thuốc 0: chưa phát; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string Fyzt { get; set; }

        /// <summary>
        /// loại thao tác 1: phát thuốc  2: trả thuốc
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// đề nghị trả thuốc cờ 0: không  1: có
        /// </summary>
        public string sqtybz { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// tính chất y lệnh
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số giường
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// Dấu thời gian
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }

    public class QueryZYFYInfoReqVOV2 : QueryZYFYInfoReqVO
    {
        public string operatetime { get; set; }
    }

    /// <summary>
    /// phát thuốc nội trú thuốc
    /// </summary>
    public class HospitalizationDispenseDrugParam : HospitalizationReturnDrugParam
    {

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// tính chất y lệnh
        /// </summary>
        public string yzxz { get; set; }
    }

    /// <summary>
    /// trả thuốc nội trú
    /// </summary>
    public class HospitalizationReturnDrugParam
    {
        /// <summary>
        /// số nội trú
        /// </summary>
        public string Zyh { get; set; }

        /// <summary>
        /// đề nghị trả thuốc thời gian bắt đầu
        /// </summary>
        public DateTime Kssj { get; set; }

        /// <summary>
        /// đề nghị trả thuốc thời gian kết thúc
        /// </summary>
        public DateTime Jssj { get; set; }

        /// <summary>
        /// giường
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// cơ cấu tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// cờ phát thuốc  0: chưa phát; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string fybz { get; set; }

        public string zytyapplyno { get; set; }
    }

    public class ZyTyApplyNoVO
    {
        public string zytyapplyno { get; set; }

        public string zyh { get; set; }
    }
}
