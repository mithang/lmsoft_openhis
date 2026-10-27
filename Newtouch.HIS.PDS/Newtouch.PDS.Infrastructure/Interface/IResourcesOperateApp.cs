using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Hủy đặt trước
    /// </summary>
    public interface IResourcesOperateApp
    {
        /// <summary>
        /// Đặt trước ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ActResult OutpatientBook(OutpatientBookRequestDTO request);

        /// <summary>
        /// Sửa đặt trước ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ActResult OutpatientBookModify(OutpatientBookModifyRequestDTO request);

        /// <summary>
        /// Hủy đặt trước ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCancelBook(OutpatientCancelPartBookRequestDTO request);

        /// <summary>
        /// Hủy đặt trước ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCancelBook(OutpatientCancelAllBookRequestDTO request);

        /// <summary>
        /// Trả phí ngoại trú Trả lại tồn kho đóng băng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCancelDjYpReturn(OutpatientCancelAllBookRequestDTO request);

        /// <summary>
        /// Xác định tài nguyên ngoại trú（commit）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCommit(OutpatientCommitRequestDTO request);

        /// <summary>
        /// Trả thuốc ngoại trú
        /// </summary>
        /// <param name="tyParam"></param>
        /// <returns></returns>
        string OutpatientReturnDrugs(tyInfo tyParam, out List<string> returnDrugBillNoList);

        /// <summary>
        /// (Trước khi phát thuốc)Trả thuốc một phần
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientPartReturnBeforeDispensingMedicine(OutpatientPartReturnBeforeDispensingMedicineRequestDTO request);

        /// <summary>
        /// Lưu y lệnh nội trú  Lưu thông tin y lệnh、Xếp thuốc、Đóng băng tồn kho
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ActResult HospitalizatiionBook(ZyypyzzxRequest request);

        /// <summary>
        /// Trả thuốc nội trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string HospitalizatiionReturnDispensingMedicine(HospitalizatiionReturnDispensingMedicineRequestDTO request);

        /// <summary>
        /// Trả thuốc nội trú
        /// </summary>
        /// <param name="tyDetail"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="returnDrugBillNo">Số đơn trả thuốc</param>
        /// <returns></returns>
        string HospitalizatiionReturnMedicine(List<tyParam> tyDetail, string yfbmCode, string organizeId,
            string userCode, out string returnDrugBillNo);

        /// <summary>
        /// Hủy xếp thuốc ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCancelArrangement(OutpatientCancelArrangementRequestDTO request);

        string HospitalizatiionReturnMedicineV2(List<tyParam> tyDetail, string yfbmCode, string organizeId, string userCode, out string returnDrugBillNo);
    }
}