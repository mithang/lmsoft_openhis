using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationPharmacy;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Phát thuốc
    /// </summary>
    public interface IDispenseIndexInfoDmnService
    {
        #region Phát thuốc
        /// <summary>
        /// Chuẩn bị thuốc hoàn thành，Sửa trạng thái thực hiện y lệnh
        /// </summary>
        /// <param name="tdrq"></param>
        /// <param name="zxId"></param>
        /// <param name="fybz"></param>
        /// <param name="ypdj"></param>
        /// <param name="ypje"></param>
        /// <param name="zfbl"></param>
        /// <param name="zfxz"></param>
        /// <returns></returns>
        string UPBQPYyzzxZT(string tdrq, string zxId, string fybz, string ypdj, string ypje, string zfbl, string zfxz);


        #endregion

        #region Chuẩn bị thuốc
        string GetBQPYXXList(DispenseBQInfoVO model);

        //Thực hiện thao tác chuẩn bị thuốc
        string GetBQTYXXList(DispenseBQInfoVO model);
        #endregion

        #region Trả thuốc

        /// <summary>
        /// Lấy bảng chi tiết trả thuốc，Bảng y lệnh liên kếtID
        /// </summary>
        /// <returns></returns>
        List<ModelZyTyYszyzIdInfoVO> YpTyyszyzidList();
        /// <summary>
        ///  Lấy bảng chi tiết trả thuốc và bảng số lô y lệnh Thông tin thuốc có thể trả
        /// </summary>
        /// <returns></returns>
        List<ModelBQBRYZZXTYInfoVO> YpTymxxxList(List<ModelZyTyYszyzIdInfoVO> list);

        /// <summary>
        /// Thao tác trả thuốc khu bệnh
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string ZYBQTYOperate(List<ModelBQBRYZZXTYInfoVO> list, string zyyf,string OrganizeId);

        /// <summary>
        /// Lấy bệnh nhân trả thuốc nội trú
        /// </summary>
        /// <param name="zybrbq"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<YPFYPatientInfoVO> GetTyPatient(string zybrbq, string organizeId);
        #endregion

        #region Phát thuốc nội trú
        /// <summary>
        /// Lấy chi tiết phát thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<YPFYPatientInfoVO> SelectDispenseDrugDetail(HospitalizationDispenseDrugParam req);

        /// <summary>
        /// Xóa thông tin y lệnh
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="zxId">Thực hiện y lệnhID</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string DeleteYzxx(string yzId, string zxId, string organizeId, string userCode);

        /// <summary>
        /// Lấy khu bệnh cần phát thuốc（Khử trùng lặp）
        /// </summary>
        /// <returns></returns>
        IList<ZYFYBQModelVO> GetFyBq(string orgId);

        /// <summary>
        /// Lấy bệnh nhân trả thuốc và khu bệnh
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<YPFYPatientInfoVO> GetDispensePatientInfo(string yfbmCode, string organizeId);

        /// <summary>
        /// Lấy bệnh nhân cần phát thuốc
        /// </summary>
        /// <returns></returns>
        IList<YPFYPatientInfoVO> GetFyPatient(string zybrbq);

        /// <summary>
        /// Thao tác trả lại phát thuốc
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        string ZYBQFYReturn(List<DispenseBQRYXXIndexVO> list, string fybz);

        /// <summary>
        /// Thao tác nút phát thuốc nội trú
        /// </summary>
        /// <param name="list"></param>
        /// <param name="zyyf"></param>
        /// <returns></returns>
        string ZYBQFYOperate(List<DispenseBQRYXXIndexVO> list, string zyyf);

        /// <summary>
        /// Phát nội trú、Truy vấn trả thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetZyDrugQueryList(Pagination pagination, QueryZYFYInfoReqVO req);

        /// <summary>
        /// Truy vấn phát thuốc nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetZyfyQueryList(Pagination pagination, QueryZYFYInfoReqVO req);

        /// <summary>
        /// Truy vấn trả thuốc nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetZytyQueryList(Pagination pagination, QueryZYFYInfoReqVO req);
        /// <summary>
        /// Theo nhật ký thao tác y lệnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetYzCzjlList(Pagination pagination, QueryZYFYInfoReqVO req);

        /// <summary>
        /// Chèn đơn xin trả thuốc
        /// </summary>
        /// <param name="applyBillDetail"></param>
        /// <param name="applyBill"></param>
        /// <returns></returns>
        string InsertReturnDispensingMedicine(List<ZyReturnDrugApplyBillDetailEntity> applyBillDetail,
           List<ZyReturnDrugApplyBillEntity> applyBill);

        /// <summary>
        /// Lấy bệnh nhân trả thuốc và khu bệnh
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<YPFYPatientInfoVO> GetReturnDispensePatientInfo(string yfbmCode, string organizeId);

        /// <summary>
        /// Lấy chi tiết trả thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetail(HospitalizationReturnDrugParam req);

        /// <summary>
        /// Lấy chi tiết trả thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetailNoBatch(HospitalizationReturnDrugParam req);

        List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetailNoBatchV2(HospitalizationReturnDrugParam req);

        List<ZyTyApplyNoVO> GetZyTyApplyNo(HospitalizationReturnDrugParam req);

        List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetailV2(HospitalizationReturnDrugParam req);

        IList<HospitalizationDispenseDetail> GetYzCzjlListV2(Pagination pagination, QueryZYFYInfoReqVO req);

        List<YPFYPatientInfoVO> GetFybdBrxxTree(string yfbmCode, string organizeId);

        IList<HospitalizationDispenseDetailV2> GetFybdList(Pagination pagination, QueryZYFYInfoReqVOV2 req);

        IList<FybdComboboxList> GetFybdComboboxList( QueryZYFYInfoReqVO req);

        #endregion

        #region Chuẩn bị thuốc cho khoa

        /// <summary>
        /// Lấy đơn xin của khoa chờ phát thuốc/Trả lại tồn kho chuẩn bị thuốc
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<KSBYSQDInfoVO> GetDeptApplySendInfo(string yfbmCode, string organizeId,string bylx);

        /// <summary>
        /// Danh sách đơn xin
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="OrgId"></param>
        /// <param name="Yfbm"></param>
        /// <returns></returns>
        List<DeptMedicineApplySendVo> SelectDeptApplySendList(string SqdArr, string OrgId, string Yfbm);

        /// <summary>
        /// Phát thuốc
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string ApplyNoSendDrugs(string SqdArr, string userCode, string yfbmCode, string organizeId);

        /// <summary>
        /// Danh sách trả lại tồn kho chuẩn bị thuốc cho khoa
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="OrgId"></param>
        /// <param name="Yfbm"></param>
        /// <returns></returns>
        List<DeptMedicineApplySendVo> SelectDeptApplyReturnList(string SqdArr, string OrgId, string Yfbm);

        /// <summary>
        /// Trả lại tồn kho
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string KcApplyNoReturnDrugs(string SqdArr, string userCode, string yfbmCode, string organizeId);
        #endregion


        #region  Đơn y lệnh nội trú
        IList<ZycfcxList> GetZycfList(Pagination pagination, ZycfcxVo req);
        IList<ZycfcxDetailList> GetZycfDetailList(Pagination pagination, ZycfcxVo req);
        #endregion
    }
}
