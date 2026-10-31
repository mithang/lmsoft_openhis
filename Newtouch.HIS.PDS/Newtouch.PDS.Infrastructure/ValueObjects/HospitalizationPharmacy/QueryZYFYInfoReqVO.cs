using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Truy vấn thông tin phát thuốc phí nội trú
    /// </summary>
    public class QueryZYFYInfoReqVO
    {
        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Nhà thuốc nội trú
        /// </summary>
        public string zyyf { get; set; }

        /// <summary>
        /// Mã khu bệnh
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Số nội trú
        /// </summary>
        public string Zyh { get; set; }

        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime Kssj { get; set; }

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime Jssj { get; set; }

        /// <summary>
        /// Trạng thái phát thuốc 0:Chưa phát;1:Đã xếp;2:Đã phát;3:Đã trả
        /// </summary>
        public string Fyzt { get; set; }

        /// <summary>
        /// Loại thao tác 1:Phát thuốc  2:Trả thuốc
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// Cờ xin trả thuốc 0:Không  1:Có
        /// </summary>
        public string sqtybz { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Tính chất y lệnh
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số giường bệnh
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
    /// Thuốc phát nội trú
    /// </summary>
    public class HospitalizationDispenseDrugParam : HospitalizationReturnDrugParam
    {

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// Tính chất y lệnh
        /// </summary>
        public string yzxz { get; set; }
    }

    /// <summary>
    /// Trả thuốc nội trú
    /// </summary>
    public class HospitalizationReturnDrugParam
    {
        /// <summary>
        /// Số nội trú
        /// </summary>
        public string Zyh { get; set; }

        /// <summary>
        /// Thời gian bắt đầu xin trả thuốc
        /// </summary>
        public DateTime Kssj { get; set; }

        /// <summary>
        /// Thời gian kết thúc xin trả thuốc
        /// </summary>
        public DateTime Jssj { get; set; }

        /// <summary>
        /// Giường bệnh
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Cơ cấu tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Cờ phát thuốc  0:Chưa phát;1:Đã xếp;2:Đã phát;3:Đã trả
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
