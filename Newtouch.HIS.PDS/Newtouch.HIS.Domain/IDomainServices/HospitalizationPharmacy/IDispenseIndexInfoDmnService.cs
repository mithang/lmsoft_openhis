using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationPharmacy;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// phát thuốc
    /// </summary>
    public interface IDispenseIndexInfoDmnService
    {
        #region 发药
        /// <summary>
        /// 配药完成, sửa thực hiện y lệnh trạng thái
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

        #region 配药
        string GetBQPYXXList(DispenseBQInfoVO model);

        //执行配药操作
        string GetBQTYXXList(DispenseBQInfoVO model);
        #endregion

        #region 退药

        /// <summary>
        /// lấy chi tiết trả thuốc bảng, liên kết y lệnh bảng ID
        /// </summary>
        /// <returns></returns>
        List<ModelZyTyYszyzIdInfoVO> YpTyyszyzidList();
        /// <summary>
        ///  lấy chi tiết trả thuốc bằng và y lệnh số lô bảng có thể trả thuốc thông tin
        /// </summary>
        /// <returns></returns>
        List<ModelBQBRYZZXTYInfoVO> YpTymxxxList(List<ModelZyTyYszyzIdInfoVO> list);

        /// <summary>
        /// khu bệnh trả thuốc thao tác
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string ZYBQTYOperate(List<ModelBQBRYZZXTYInfoVO> list, string zyyf,string OrganizeId);

        /// <summary>
        /// lấy trả thuốc nội trú của bệnh nhân
        /// </summary>
        /// <param name="zybrbq"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<YPFYPatientInfoVO> GetTyPatient(string zybrbq, string organizeId);
        #endregion

        #region 住院发药
        /// <summary>
        /// lấy chi tiết phát thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<YPFYPatientInfoVO> SelectDispenseDrugDetail(HospitalizationDispenseDrugParam req);

        /// <summary>
        /// xóa y lệnh thông tin
        /// </summary>
        /// <param name="yzId">y lệnh ID</param>
        /// <param name="zxId">thực hiện y lệnh ID</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string DeleteYzxx(string yzId, string zxId, string organizeId, string userCode);

        /// <summary>
        /// lấy cần phát thuốc của khu bệnh (loại bỏ trùng lặp)
        /// </summary>
        /// <returns></returns>
        IList<ZYFYBQModelVO> GetFyBq(string orgId);

        /// <summary>
        /// lấy trả thuốc bệnh nhân và khu bệnh
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<YPFYPatientInfoVO> GetDispensePatientInfo(string yfbmCode, string organizeId);

        /// <summary>
        /// lấy cần phát thuốc của bệnh nhân
        /// </summary>
        /// <returns></returns>
        IList<YPFYPatientInfoVO> GetFyPatient(string zybrbq);

        /// <summary>
        /// phát thuốc trả lại thao tác
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        string ZYBQFYReturn(List<DispenseBQRYXXIndexVO> list, string fybz);

        /// <summary>
        /// phát thuốc nội trú nút thao tác
        /// </summary>
        /// <param name="list"></param>
        /// <param name="zyyf"></param>
        /// <returns></returns>
        string ZYBQFYOperate(List<DispenseBQRYXXIndexVO> list, string zyyf);

        /// <summary>
        /// nội trú phát, trả thuốc tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetZyDrugQueryList(Pagination pagination, QueryZYFYInfoReqVO req);

        /// <summary>
        /// tra cứu phát thuốc nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetZyfyQueryList(Pagination pagination, QueryZYFYInfoReqVO req);

        /// <summary>
        /// trả thuốc nội trú tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetZytyQueryList(Pagination pagination, QueryZYFYInfoReqVO req);
        /// <summary>
        /// theo y lệnh thao tác nhật ký
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetYzCzjlList(Pagination pagination, QueryZYFYInfoReqVO req);

        /// <summary>
        /// chèn trả thuốc phiếu đề nghị
        /// </summary>
        /// <param name="applyBillDetail"></param>
        /// <param name="applyBill"></param>
        /// <returns></returns>
        string InsertReturnDispensingMedicine(List<ZyReturnDrugApplyBillDetailEntity> applyBillDetail,
           List<ZyReturnDrugApplyBillEntity> applyBill);

        /// <summary>
        /// lấy trả thuốc bệnh nhân và khu bệnh
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<YPFYPatientInfoVO> GetReturnDispensePatientInfo(string yfbmCode, string organizeId);

        /// <summary>
        /// lấy chi tiết trả thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetail(HospitalizationReturnDrugParam req);

        /// <summary>
        /// lấy chi tiết trả thuốc
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

        #region 科室备药

        /// <summary>
        /// 获取科室申请单待发药/备药库存退回
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<KSBYSQDInfoVO> GetDeptApplySendInfo(string yfbmCode, string organizeId,string bylx);

        /// <summary>
        /// phiếu đề nghị danh sách
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="OrgId"></param>
        /// <param name="Yfbm"></param>
        /// <returns></returns>
        List<DeptMedicineApplySendVo> SelectDeptApplySendList(string SqdArr, string OrgId, string Yfbm);

        /// <summary>
        /// phát thuốc
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string ApplyNoSendDrugs(string SqdArr, string userCode, string yfbmCode, string organizeId);

        /// <summary>
        /// 科室备药库存退回列表
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="OrgId"></param>
        /// <param name="Yfbm"></param>
        /// <returns></returns>
        List<DeptMedicineApplySendVo> SelectDeptApplyReturnList(string SqdArr, string OrgId, string Yfbm);

        /// <summary>
        /// trả lại tồn kho
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string KcApplyNoReturnDrugs(string SqdArr, string userCode, string yfbmCode, string organizeId);
        #endregion


        #region  住院医嘱处方
        IList<ZycfcxList> GetZycfList(Pagination pagination, ZycfcxVo req);
        IList<ZycfcxDetailList> GetZycfDetailList(Pagination pagination, ZycfcxVo req);
        #endregion
    }
}
