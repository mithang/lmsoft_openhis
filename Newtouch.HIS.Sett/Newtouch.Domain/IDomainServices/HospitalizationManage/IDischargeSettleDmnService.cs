using Newtouch.Core.Common;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Proxy.guian.DTO;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.HospitalizationManage
{
    public interface IDischargeSettleDmnService
    {

        #region 出院结算
        /// <summary>
        /// Lấy thông tin cơ bản bệnh nhân theo số nhập viện
        /// </summary>
        List<InpatientSettPatInfoVO> SelectInpatientSettPatInfo(string zyh, string orgId);
	    
		/// <summary>
		/// Lấy bệnh nhân theo số nhập viện Thông tin liên quan BHYT
		/// </summary>
		/// <param name="zyh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		InpatientSettYbPatInfoVO GetInpatientSettYbPatInfo(string zyh, string orgId);

		/// <summary>
		/// Bảng tính phí hạng mục điều trị(zy_xmjfb)
		/// </summary>
		/// <param name="zyh"></param>
		/// <param name="OrganizeId"></param>
		/// <returns></returns>
		List<TreatmentItemFeeDetailVO> SelectTreatmentItemList(string zyh, string orgId, DateTime? kssj = null, DateTime? jssj = null,string ks=null);

        decimal GetInpatSettFeeSum(string zyh, string orgId, string sczt, string xmmc, DateTime? kssj = null, DateTime? jssj = null);
        /// <summary>
        /// Bảng tính phí thuốc
        /// </summary>
        List<DrugFeeDetailVO> SelectDrugList(string zyh, string orgId, DateTime? kssj = null, DateTime? jssj = null, string ks = null);

        List<DrugFeeDetailVO> SelectWsfDrugList(string zyh, string orgId, DateTime? kssj = null, DateTime? jssj = null, string ks = null);
        List<DrugFeeDetailVO> SelectWsfDrugList(string zyh, string orgId, DateTime? kssj = null, DateTime? jssj = null, string ks = null, string xmlb = null, string xmmc = null);
        List<TreatmentItemFeeDetailVO> SelectWsfItemList(string zyh, string orgId, DateTime? kssj, DateTime? jssj, string ks = null, string xmlb = null, string xmmc = null);
        /// <summary>
        /// Bảng tính phí hạng mục không điều trị
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<NonTreatmentItemFeeDetailVO> SelectNonTreatmentItemList(string zyh, string orgId);

        /// <summary>
        /// Lưu quyết toán
        /// </summary>
        /// <param name="settpatInfo"></param>
        /// <param name="settleItemsBo"></param>
        /// <param name="expectedcyrq"></param>
        /// <param name="orgId"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated">Số tiền và thông tin thanh toán</param>
        /// <param name="ybfeeRelated">Thông tin chi phí liên quan BHYT</param>
        void SaveSett(InpatientSettPatInfoVO settpatInfo, InpatientSettleItemBO settleItemsBo, DateTime expectedcyrq, string orgId, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, S14OutResponseDTO xnhfeeRelated
            , string outTradeNo,string jslx, out int jsnm);

        /// <summary>
        /// Lấy chi phí nhóm theo loại lớn
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupVO> GetHospGroupFeeVOList(string zyh, string orgId, string ver);
        /// <summary>
        /// Lấy chi phí nhóm theo loại lớn-Chi phí chi tiết
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfdl"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetHospGroupFeeVOList(Pagination pagination, string zyh, string orgId, string sfdl);
        /// <summary>
        /// Lấy chi phí nhóm theo loại lớn-Kèm tra cứu mờ
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfdl"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetHospGroupFeeVOData(Pagination pagination, string zyh, string orgId,string keyword);
        /// <summary>
        /// Lấy chi tiết thu phí
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfxmCode"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode, string zfbz,decimal? dj);
        /// <summary>
        /// Trung tâm bệnh nhân nội trú lấy hóa đơn chi phí
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfxmCode"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetFyzdDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode);
        

        IList<HospFeeUploadDetailVO> GetHospXmYpFeeVOList(Pagination pagination,string zyh, string orgId,string sczt, string xmmc,DateTime? kssj,DateTime jssj,string isnewyb);
        #endregion

        #region 取消出院结算

        /// <summary>
        /// Hủy quyết toán xuất viện
        /// </summary>
        /// <param name="preSettlEntity"></param>
        /// <param name="settlDetailList"></param>
        /// <param name="cancelReason"></param>
        /// <param name="orgId"></param>
        void DoCancel(HospSettlementEntity preSettlEntity, List<HospSettlementDetailEntity> settlDetailList, string cancelReason, string cancelyblsh,string orgId);

        #endregion

        #region 模拟结算（GA）

        /// <summary>
        /// Lưu dữ liệu quyết toán trước BHYT
        /// </summary>
        void SaveSett2303(CqybSett2303Entity YuJieSuan);

        #endregion

        #region 医保上传明细
        InZyfymxxrDto GetUploadFeeDetails(Pagination pagination, string zyh,string orgId,string usrName, out decimal ybzje);
        #endregion

        #region 新农合结算

        InpatientSettXnhPatInfoVO GetInpatientSettXnhPatInfo(string zyh, string orgId);
        S10RequestDTO GetUploadXnhFeeDetails(string zyh, string orgId, out decimal nhfyzje);
        S07RequestDTO GetXnhS07RequestDTO(string zyh, string orgId);
        string GetZyhByGrbm(string xnhgrbm,string sfjs, string orgId);

		#endregion

		#region 重庆医保

	    UploadPrescriptionsInPut GetCQUploadFeeDetails(Pagination pagination, string mzh, string orgId, string usercode, out decimal ybzje,
		    out decimal zfzje);
        string Getgjybdm(string mzzyh, string cfnms, string cflx,string orgId);
        decimal GetCQAlreadyUploadFeeDetails(string mzh, string orgId);
        HosCqybJsPatInfoVO GetCQInpatientSettYbPatInfo(string zyh, string orgId);

        /// <summary>
        /// Lấy chi tiết chưa phê duyệt
        /// </summary>
        /// <param name="cfmxlshstr">Chuỗi số giao dịch</param>
        /// <param name="orgId"></param>
        /// <param name="FinalType">Loại phê duyệt</param>
        /// <param name="highPrices">Giá thu phí cao</param>
        /// <returns></returns>
        IList<RequestApprovalVO> sweep_expired_approvals(string cfmxlshstr, string orgId, string zyh);

        void updatespbz(string cfhstr, string orgId, string usercode);

        void commitPartialSettle(string zyh, string orgId, DateTime startTime, DateTime endTime, InpatientSettFeeRelatedDTO feeRelated, CQZyjs05Dto ybfeeRelated, string jslx, InpatientSettleItemBO ypjfbList, out int jsnm, int? expectedCount = null);
        /// <summary>
        /// Khi chuyển viện quyết toán thì cập nhật thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        void UpdatePatientInfo(string zyh, string orgId);

        /// <summary>
        /// Số giao dịch lần cuối cùng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        int GetCQLastJsnm(string zyh, string orgId);

        /// <summary>
        /// Số giao dịch lần cuối cùng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCQLastLsh(int jsnm, string orgId);

        CancelSettInfo GetCancelSettInfo(int jsnm, string orgId);

        #endregion

        #region 医保数据传输
        /// <summary>
        /// Tra cứuHISTổng chi phí
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        InpatIentFeeObj GetHospPreInfo(string zyh, string orgId,DateTime jzsj);
        /// <summary>
        /// Có tồn tại chi tiết chưa tải lên hay không
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string ValialUploadData(string zyh, string orgId, DateTime jssj);
        string ValialUploadDataShyb(string zyh, string orgId, DateTime jssj);
        string ValialPartialUploadData(string zyh, string orgId, DateTime jssj);
        InpatIentFeeInfo GetCQAlreadyUploadFeeDetailsV2(string zyh, string orgId, DateTime jssj);
        #endregion
        int CountLisIncompletezy(string orgId, string zyh);
    }
}
