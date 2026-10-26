using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    /// <summary>
    /// trả thuốc thao tác tham số
    /// </summary>
    public class tyRequestPar
    {
        /// <summary>
        /// chi tiết đơn thuốc trong mã
        /// </summary>
        public int cfmxnm { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// số tần suất
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// tần suất
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// thuốc
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// số lượng
        /// </summary>
        public int sl { get; set; }
    }

    /// <summary>
    /// trả thuốc thông tin
    /// </summary>
    public class tyInfo
    {
        /// <summary>
        /// bộ phận trả thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// thao tác người
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// trả thuốc đơn đơn số
        /// </summary>
        public string returnDrugBillNo { get; set; }

        /// <summary>
        /// trả thuốc chi tiết thuốc
        /// </summary>
        public List<tyParam> tyDrugDetail { get; set; }

        /// <summary>
        /// trả thuốc đơn đơn số
        /// </summary>
        public string zytyapplyno { get; set; }

    }

    /// <summary>
    /// trả thuốc nội trú yêu cầu thông tin
    /// </summary>
    public class HospitalizationReturnDrugParem
    {
        /// <summary>
        /// trả thuốc đơn đơn số
        /// </summary>
        public string returnDrugBillNo { get; set; }

        /// <summary>
        /// số phiếu đề nghị trả thuốc
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// thao tác người
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// trả thuốc chi tiết
        /// </summary>
        public List<TyRpInfo> rpInfo { get; set; }
    }

    /// <summary>
    /// Chi tiết y lệnh trả thuốc
    /// </summary>
    public class TyRpInfo
    {
        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// thực hiện y lệnh lô
        /// </summary>
        public List<ExecuteBatchDetail> executeBatchDetail { get; set; }
    }

    /// <summary>
    /// thực hiện lô thông tin
    /// </summary>
    public class ExecuteBatchDetail
    {

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// trả thuốc y lệnh chi tiết
        /// </summary>
        public List<RpDetail> tyRpDetail { get; set; }
    }

    /// <summary>
    /// y lệnh chi tiết
    /// </summary>
    public class RpDetail
    {

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// thuốc lô thông tin
        /// </summary>
        public List<DrugBatch> drugBatch { get; set; }


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
    }

    /// <summary>
    /// thuốc lô thông tin
    /// </summary>
    public class DrugBatch
    {

        /// <summary>
        /// số lượng(số lượng đơn vị bộ phận, zhyz*sl=số lượng đơn vị nhỏ nhất)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// tần suất
        /// </summary>
        public string pc { get; set; }
    }

    /// <summary>
    /// trả thuốc thao tác tham số
    /// </summary>
    public class tyParam
    {
        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// tần suất
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// số lượng(số lượng đơn vị bộ phận, zhyz*sl=số lượng đơn vị nhỏ nhất)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// số phiếu đề nghị trả thuốc
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// thao tác người
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// số nhóm
        /// </summary>
        public string czh { get; set; }

        public string zytyapplyno { get; set; }
        public string zyh { get; set; }

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
    }

}
