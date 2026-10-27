using System.Collections.Generic;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Stock;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Quản lý phát thuốc
    /// </summary>
    public interface IDispensingDmnService
    {

        /// <summary>
        /// Đặt trước đơn ngoại trú（Đóng băng tồn kho）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        string OutPatientBook(BookItemDo bookItemDo);

        /// <summary>
        /// Đặt trước đơn ngoại trú（Đóng băng tồn kho） V2
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        string OutPatientBookV2(BookItemDo bookItemDo);

        /// <summary>
        /// Hủy đặt trước
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string OutPatientBookCancel(string ypCode, int sl, string yfbmCode, string cfh, string organizeId, string creatorCode);

        /// <summary>
        /// Hủy đặt trước
        /// </summary>
        /// <param name="mxphlist"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string OutPatientBookCancel(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphlist, string organizeId, string creatorCode);

        /// <summary>
        /// HISHủy thanh toán  Trả lại đóng băng
        /// </summary>
        /// <param name="mxphlist"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string OutPatientYpdjReturn(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphlist, string organizeId, string creatorCode);

        /// <summary>
        /// outpatient commit
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string OutpatientCommit(string ypCode, string yfbmCode, string cfh, string organizeId, string creatorCode);

        /// <summary>
        /// Trừ tồn kho ngoại trú(Phát thuốc)
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string OutpatientDispensingReduceStock(string ypCode, string yfbmCode, string cfh, string organizeId, string userCode);

        /// <summary>
        /// Phát thuốc ngoại trú(Trừ tồn kho、Sửa cờ phát thuốc đơn thuốc、Tạo bản ghi phát thuốc)
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="cfmxId">mz_cfmx.id</param>
        /// <returns></returns>
        string OutpatientDispensingDrug(string ypCode, long cfmxId, int sl, string yfbmCode, string cfh, string organizeId, string userCode);

        /// <summary>
        /// Trả thuốc ngoại trú trả tồn kho
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="zxdwsl"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="returnDrugBillNo">Số đơn trả thuốc</param>
        /// <returns></returns>
        string OutpatientReturnDrugAddStock(string ypCode, string ph,
           string pc, int zxdwsl,
           string yfbmCode, string cfh,
           string organizeId, string userCode, string returnDrugBillNo);

        /// <summary>
        /// Nội trúBookĐóng băng tồn kho và lưuzy_ypyzzxph
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        string HospitalizationBook(BookItemDo bookItemDo);

        /// <summary>
        /// Nội trúBookĐóng băng tồn kho và lưuzy_ypyzzxph
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="zxId">Thực hiệnID</param>
        /// <param name="organizeId">Tổ chứcID</param>
        /// <param name="yfbmCode">Mã bộ phận nhà thuốc</param>
        /// <param name="creatorCode">Người tạo</param>
        /// <param name="patientName">Bệnh nhân thuộc y lệnh</param>
        /// <returns></returns>
        string HospitalizationBook(string yzId, string zxId, string organizeId, string yfbmCode, string creatorCode,
            string patientName);

        /// <summary>
        /// Hủy đóng băng，và xóa vật lý thông tin y lệnh Thận trọng khi dùng
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <returns></returns>
        string CancelForzenAndDeleteYz(string yzId, string zxId);

        /// <summary>
        /// Nội trúBookĐóng băng tồn kho và lưuzy_ypyzzxph
        /// </summary>
        /// <param name="zxId">Thực hiệnID</param>
        /// <param name="yzDetails"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string HospitalizationBook(string zxId, List<YzDetail> yzDetails, string organizeId);

        /// <summary>
        /// Nội trúBookĐóng băng tồn kho và lưuzy_ypyzzxph
        /// </summary>
        /// <param name="zxId"></param>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        string HospitalizationCancelBook(string zxId, BookItemDo bookItemDo);

        /// <summary>
        /// Hủy xếp thuốc
        /// </summary>
        /// <param name="cancelArrangement"></param>
        /// <returns></returns>
        string HospitalizationCancelArrangement(CancelArrangement cancelArrangement);

        /// <summary>
        /// Hủy xếp thuốc Giao dịch
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string HospitalizationCancelArrangementWithTrans(CancelArrangement param);

        /// <summary>
        /// Phát thuốc nội trú
        /// </summary>
        /// <param name="zyphLs"></param>
        /// <param name="yzLs"></param>
        /// <param name="userCode"></param>
        /// <param name="fyid"></param>
        /// <returns></returns>
        string HospitalizationDispensing(List<ZyYpyzzxphEntity> zyphLs, List<ZyYpyzxxEntity> yzLs, string userCode,out string fyid,string organizeId);

        /// <summary>
        /// Lấy loại lớn thuốc
        /// </summary>
        /// <param name="ypcode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string getYpDl(string ypcode, string organizeId);
        string PrepareMedicine(BYDjInfoDTO yplist, string organizeId,string yhgh);
        string PrepareMedicineReturn(BythDjInfoDTO yplist, string organizeId, string yhgh);
        string WithdrawalPreparationReturn(string Djh, string organizeId, string yhgh, string thzt);
        void Updatezy_brxxexpand(string OrganizeId, string zyh);
        void SyncPatFee(string orgId, string zyh, int zxtype);
    }
}