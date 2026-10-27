using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    /// <summary>
    /// Tham số thao tác trả thuốc
    /// </summary>
    public class tyRequestPar
    {
        /// <summary>
        /// Mã nội bộ chi tiết đơn thuốc
        /// </summary>
        public int cfmxnm { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Số tần suất
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Thuốc
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int sl { get; set; }
    }

    /// <summary>
    /// Thông tin trả thuốc
    /// </summary>
    public class tyInfo
    {
        /// <summary>
        /// Bộ phận trả thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Người thao tác
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Số đơn trả thuốc
        /// </summary>
        public string returnDrugBillNo { get; set; }

        /// <summary>
        /// Chi tiết thuốc trả
        /// </summary>
        public List<tyParam> tyDrugDetail { get; set; }

        /// <summary>
        /// Số đơn trả thuốc
        /// </summary>
        public string zytyapplyno { get; set; }

    }

    /// <summary>
    /// Thông tin yêu cầu trả thuốc nội trú
    /// </summary>
    public class HospitalizationReturnDrugParem
    {
        /// <summary>
        /// Số đơn trả thuốc
        /// </summary>
        public string returnDrugBillNo { get; set; }

        /// <summary>
        /// Số đơn xin trả thuốc
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Người thao tác
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Chi tiết trả thuốc
        /// </summary>
        public List<TyRpInfo> rpInfo { get; set; }
    }

    /// <summary>
    /// Chi tiết y lệnh trả thuốc
    /// </summary>
    public class TyRpInfo
    {
        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// Lô thực hiện y lệnh
        /// </summary>
        public List<ExecuteBatchDetail> executeBatchDetail { get; set; }
    }

    /// <summary>
    /// Thông tin lô thực hiện
    /// </summary>
    public class ExecuteBatchDetail
    {

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Chi tiết y lệnh trả thuốc
        /// </summary>
        public List<RpDetail> tyRpDetail { get; set; }
    }

    /// <summary>
    /// Chi tiết y lệnh
    /// </summary>
    public class RpDetail
    {

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Thông tin lô thuốc
        /// </summary>
        public List<DrugBatch> drugBatch { get; set; }


        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1： Có
        /// 2： Không
        /// </summary>
        public int? sfcl { get; set; }
    }

    /// <summary>
    /// Thông tin lô thuốc
    /// </summary>
    public class DrugBatch
    {

        /// <summary>
        /// Số lượng(Số lượng đơn vị bộ phận，zhyz*sl=Số lượng đơn vị nhỏ nhất)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public string pc { get; set; }
    }

    /// <summary>
    /// Tham số thao tác trả thuốc
    /// </summary>
    public class tyParam
    {
        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Số lượng(Số lượng đơn vị bộ phận，zhyz*sl=Số lượng đơn vị nhỏ nhất)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Số đơn xin trả thuốc
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Người thao tác
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string czh { get; set; }

        public string zytyapplyno { get; set; }
        public string zyh { get; set; }

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1： Có
        /// 2： Không
        /// </summary>
        public int? sfcl { get; set; }
    }

}
