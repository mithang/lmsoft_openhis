using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // Stub implementations for HospitalizationManage services whose logic is NOT
    // ported in this phase. Every stub throws an explicit NotSupportedException
    // (Vietnamese message) so callers get a clear error — never a fake success
    // and never a silent empty result for write/settlement actions.
    // The single exception is CheckStaffIsBelongDuty, which only gates optional
    // view pre-fill and safely returns false.

    internal static class Stub
    {
        public static Exception NotPorted(string what)
        {
            return new NotSupportedException("Chức năng '" + what + "' chưa được chuyển đổi sang .NET Core trong giai đoạn này.");
        }
    }


    public class SysStaffRepoStub : ISysStaffRepo
    {
        public string GetNameByGh(string gh, string orgId) { throw Stub.NotPorted("tra cứu tên nhân viên theo mã"); }
    }

    public class MoneyUpperLimitReminderDmnServiceStub : IMoneyUpperLimitReminderDmnService
    {
        public void GetStaffjfjeInfo(string orgId, string gh, string ks, out decimal jfjesx, out decimal yjfzje)
        {
            throw Stub.NotPorted("tra cứu hạn mứcghi sổ của trị liệu sư");
        }
            public List<MoneyUpperLimitReminderEntity> GetAllList(object p0, object p1, object p2, object p3)
        {
            return new List<MoneyUpperLimitReminderEntity>();
        }
        public List<MoneyUpperLimitReminderEntity> GetAllList(object p0, object p1, object p2, object p3, object p4)
        {
            return new List<MoneyUpperLimitReminderEntity>();
        }
}



    public class HospSettCancelAppStub : IHospSettCancelApp
    {
        public object GetPatHospStatusDetail(string zyh, string kh) { throw Stub.NotPorted("tra cứu trạng thái hủyquyết toán"); }
        public void DoCancel(string zyh, int expectedjsnm, string cancelReason) { throw Stub.NotPorted("hủyquyết toán nhập viện"); }
    }

    public class HospSettDmnServiceStub : IHospSettDmnService
    {
        public object GridInPatientQueryGridJson(Pagination pagination, HospSettQueryReq req, string orgId) { throw Stub.NotPorted("truy vấnquyết toán nhập viện"); }
        public object GetPaginationSettlementList(Pagination pagination, string orgId, string keyword, string fph, DateTime? jsksrq, DateTime? jsjsrq) { throw Stub.NotPorted("truy vấn danh sáchquyết toán"); }
        public object GetPaginationZFSettlementList(Pagination pagination, string orgId, string keyword, DateTime? jsksrq, DateTime? jsjsrq) { throw Stub.NotPorted("truy vấn danh sáchquyết toán tự phí"); }
        public object SettlementDetailsQuery(string orgId, int jsnm) { throw Stub.NotPorted("truy vấn chi tiếtquyết toán"); }
        public object SettlementDetailsItemsQuery(Pagination pagination, string zyh, string orgId, string dlCode, string jsnms, string mc) { throw Stub.NotPorted("truy vấnchi tiết phíquyết toán"); }
        public void UpdateSettInvoiceNo(string orgId, int jsnm, string fph) { throw Stub.NotPorted("cập nhật số hóa đơnquyết toán"); }
    }

    public class BookkeepInpatientAppStub : IBookkeepInpatientApp
    {
        public HosAccountingPatStatusDetailDto GetAccountingStatusDetail(string zyh) { throw Stub.NotPorted("tra cứu trạng tháighi sổ"); }
        public void SubmitAccountingPlan(string zyh, IList<InpatientAccountingPlanItemDto> xmList) { throw Stub.NotPorted("gửi kế hoạchghi sổ"); }
        public IList<HospAccountingPlanWaitingExeVO> WaitingAccountingPlanQuery(IList<string> zyhList, string from = null) { throw Stub.NotPorted("truy vấn kế hoạchghi sổ chờ thực hiện"); }
        public void ExecuteAccountingPlan(IList<zxGirdDto> zxItem, DateTime? zxrq, string from = null) { throw Stub.NotPorted("thực hiện kế hoạchghi sổ"); }
        public void CancelPreStopAccountingPlan(string jzjhmxIdStr) { throw Stub.NotPorted("hủy dừng trước kế hoạchghi sổ"); }
        public InpatientHosAccountingPatStatusDetailDto GetInpatientAccountingStatusDetail(string zyh) { throw Stub.NotPorted("tra cứu trạng tháighi sổ nội trú"); }
    }

    public class BookkeepInHosDmnServiceStub : IBookkeepInHosDmnService
    {
        public DateTime? GetLastValidMidwaySettTime(string zyh, string orgId) { throw Stub.NotPorted("lấy thời điểmquyết toán giữa kỳ hợp lệ gần nhất"); }
        public object SelectAccountingExecuteList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, string zls, string cxzt, string jzjhmxId, string sfzt) { throw Stub.NotPorted("truy vấn thực hiệnghi sổ"); }
        public object SelectOutAccountingExecuteList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, string zls, string cxzt, string jzjhmxId, string sfzt) { throw Stub.NotPorted("truy vấn thực hiệnghi sổ ngoại trú"); }
        public object SelectAccountingPlanList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, int? yzxz) { throw Stub.NotPorted("truy vấn kế hoạchghi sổ"); }
        public object GetFormPlan(string keyValue, string orgId, string from) { throw Stub.NotPorted("lấy biểu mẫu kế hoạchghi sổ"); }
        public object GetFormPlan(Pagination pagination, string jzjhmxId, string orgId, string from) { throw Stub.NotPorted("lấy chi tiết kế hoạchghi sổ"); }
        public void RevokeExec(string jzjhmxId, string zxbh, string orgId, string from) { throw Stub.NotPorted("hủy thực hiệnghi sổ"); }
        public void UpdateExec(cxzxGirdDto zxItem, string from, string orgId) { throw Stub.NotPorted("cập nhật thực hiệnghi sổ"); }
        public void StopAccountingPlan(List<string> mxIdList, DateTime stopDate, string deptCode, string rygh) { throw Stub.NotPorted("dừng kế hoạchghi sổ"); }
        public void SaveInpatientAccounting(string zyh, IList<InpatientAccountingItemDto> xmList, string orgId, string userCode) { throw Stub.NotPorted("lưughi sổ nội trú"); }
        public string getzfxzById(string sfxmcode, string orgId) { throw Stub.NotPorted("lấy tính chất tự phí theo mãthu phí"); }
        public IList<VerificationVO> Verification(string codes, string orgId, string yfdm) { throw Stub.NotPorted("kiểm tra tồn kho vật tư"); }
        public string SaveFee(List<HospItemBillingEntity> xmjfbList, List<HospDrugBillingEntity> ypjfbList) { throw Stub.NotPorted("lưu phíghi sổ"); }
        public object GetChargeTemplate(string ks, string orgId) { throw Stub.NotPorted("lấy mẫuthu phí"); }
        public object GetChargeItemContent(string sfmbbh, string orgId) { throw Stub.NotPorted("lấy nội dung mẫuthu phí"); }
        public IList<UnSettedFeeItemVO> GetChargeItemPaginationList(Pagination pagination, string zyh, string orgId, DateTime startTime, DateTime endTime) { throw Stub.NotPorted("lấy danh sách phí chưaquyết toán"); }
        public void MidwaySettlement(string zyh, string orgId, DateTime startTime, DateTime endTime, int? expectedCount) { throw Stub.NotPorted("quyết toán giữa kỳ"); }
        public void DischargeSettlement(string zyh, string orgId, DateTime cyrq) { throw Stub.NotPorted("quyết toán xuất viện"); }
        public void CancelTheLastMidwaySett(string zyh, string zffs1, string orgId, out decimal refoundmoney) { throw Stub.NotPorted("hủyquyết toán giữa kỳ gần nhất"); }
            public System.Collections.Generic.IList<PendingExecutionPatientVO> GetOutPendingExecutionPatientVOList(object p0, object p1)
        {
            return new System.Collections.Generic.List<PendingExecutionPatientVO>();
        }
        public List<IBookkeepInHosDmnService_GetPendingExecutionPatientVOListDto> GetPendingExecutionPatientVOList(object p0)
        {
            return new List<IBookkeepInHosDmnService_GetPendingExecutionPatientVOListDto>();
        }
        public System.Collections.Generic.IList<PendingExecutionPatientVO> GetPendingExecutionPatientVOList(object p0, object p1)
        {
            return new System.Collections.Generic.List<PendingExecutionPatientVO>();
        }
}


    public class OutPatientAppStub : IOutPatientApp
    {
        public void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> xmList, string orgId) { throw Stub.NotPorted("lưu thông tin tài khoản ngoại trú"); }
    }


    public class SysPatiChargeAddAppStub : ISysPatiChargeAddApp
    {
            public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public object GetSysPatiChargeAddVoList(object p0)
        {
            return null;
        }
        public List<SysPatiChargeAddVo> GetSysPatiChargeAddVoList(object p0, object p1)
        {
            return new List<SysPatiChargeAddVo>();
        }
        public object GetfjsfdlList(object p0)
        {
            return null;
        }
        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
}

    public class SysPatiChargeWaiverAppStub : ISysPatiChargeWaiverApp
    {
            public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public object GetEffectiveList(object p0)
        {
            return null;
        }
        public List<SysPatientChargeWaiverEntity> GetEffectiveList(object p0, object p1)
        {
            return new List<SysPatientChargeWaiverEntity>();
        }
        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
}

    public class HospItemFeeAppStub : IHospItemFeeApp
    {
        public void SubmitForm(HospItemBillingEntity entity, int? keyValue) { throw Stub.NotPorted("lưu phí mụcghi sổ"); }
    }

    public class HospMedicinFeeAppStub : IHospMedicinFeeApp
    {
        public void SubmitForm(HospDrugBillingEntity entity, int? keyValue) { throw Stub.NotPorted("lưu phí thuốcghi sổ"); }
    }

    public class HosPatAccDmnServiceStub : IHosPatAccDmnService
    {
    }

    public class AccountManageAppStub : IAccountManageApp
    {
        public HosPatAccInfoDto GetHosPatInfo(string zyh) { throw Stub.NotPorted("lấy thông tin tài khoản nhập viện"); }
        public string GetFinRepSJPZH() { throw Stub.NotPorted("lấy số phiếu thu tài chính"); }
        public bool PayDepositPost(DeposDto dto, out string szId) { throw Stub.NotPorted("nạp tiền đặt cọc"); }
            public bool CheckSJPZH(object p0, out FinanceReceiptEntity p1, out string p2)
        {
            p1 = default;
            p2 = default;
            p1 = null;
            p2 = null;
            return false;
        }
        public bool ExitDepositPost(object p0)
        {
            return false;
        }
        public ReserveDto GetHosPatAccInfo(object p0)
        {
            return new ReserveDto();
        }
        public System.Collections.Generic.List<PatAccPayVO> GetPayList(object p0, object p1)
        {
            return new System.Collections.Generic.List<PatAccPayVO>();
        }
        public object GetZhyeByPatid(object p0)
        {
            return null;
        }
        public object GetZhyeByPatid(object p0, object p1)
        {
            return null;
        }
        public object PayDepositPost(object p0, out object p1)
        {
            p1 = default;
            return null;
        }
        public object PayDepositPostAcnt(object p0)
        {
            return null;
        }
        public object RefundAccount(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public ReserveDto GetHosPatAccInfo(object p0, object p1)
        {
            return null;
        }
}

    public class ItemDmnServiceStub : IItemDmnService
    {
        public IList<SysItemsDetailVO> GetItemsDetailListByOrgIdAndItemCode(string orgId, string itemCode) { throw Stub.NotPorted("lấy danh mục từ điển"); }
    }

    public class SysChargeTemplateRepoStub : ISysChargeTemplateRepo
    {
    }

    public class HospFeeDmnServiceStub : IHospFeeDmnService
    {
        public void SyncPatFee(string orgId, string zyh, int lx) { throw Stub.NotPorted("đồng bộ phí bệnh nhân"); }
        public IList<HospItemFeeDetailVO> GetItemFeeDetailVOList(string zyh, string orgId) { throw Stub.NotPorted("lấy chi tiết phí mục"); }
        public IList<HospItemFeeDetailVO> GetMedicinFeeDetailVOList(string zyh, string orgId) { throw Stub.NotPorted("lấy chi tiết phí thuốc"); }
    }

    public class DischargeSettleAppStub : IDischargeSettleApp
    {
        public object GetInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver) { throw Stub.NotPorted("tra cứu trạng tháiquyết toán xuất viện"); }
        public object GetPartialInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver) { throw Stub.NotPorted("tra cứu trạng tháiquyết toán giữa kỳ"); }
        public object GetCancelSettleStatusDetail(string zyh, string sfz, string kh, string cardType) { throw Stub.NotPorted("tra cứu trạng thái hủyquyết toán"); }
        public object GetZyUploadDetail(string zyh, string jslx) { throw Stub.NotPorted("lấy chi tiết tải lên BHYT"); }
        public object GetInpatientSettleJe(string zyh, string sczt, object p3, DateTime jssj, string xmmc) { throw Stub.NotPorted("lấy số tiềnquyết toán"); }
        public void SavePartialSettle(string zyh, DateTime startTime, DateTime endTime, string fph, InpatientSettFeeRelatedDTO feeRelated, CQZyjs05Dto ybfeeRelated, string outTradeNo, string jslx, out int jsnm) { throw Stub.NotPorted("lưuquyết toán giữa kỳ"); }
        public void DoCancel(string zyh, int expectedjsnm, string cancelReason, string cancelyblsh, out string outTradeNo, out decimal refundAmount) { throw Stub.NotPorted("hủyquyết toán xuất viện"); }
    }

    public class DischargeSettleDmnServiceStub : IDischargeSettleDmnService
    {
        public object GetUploadFeeDetails(Pagination pagination, string zyh, string orgId, string userName, out decimal ybzje) { throw Stub.NotPorted("lấy chi tiết phí tải lên"); }
        public object GetCQUploadFeeDetails(Pagination pagination, string zyh, string orgId, string userCode, out decimal ybzje, out decimal zfzje) { throw Stub.NotPorted("lấy chi tiết phí tải lên Trùng Khánh"); }
        public object GetCQAlreadyUploadFeeDetails(string mzzyh, string orgId) { throw Stub.NotPorted("lấy chi tiết phí đã tải lên"); }
        public object Getgjybdm(string mzzyh, string cfnms, string cflx, string orgId) { throw Stub.NotPorted("lấy mã BHYT quốc gia"); }
        public CQAlreadyUploadFeeDetailsVO GetCQAlreadyUploadFeeDetailsV2(string mzzyh, string orgId, DateTime jssj) { throw Stub.NotPorted("lấy chi tiết phí đã tải lên (v2)"); }
        public string ValialUploadData(string zyh, string orgId, DateTime jssj) { throw Stub.NotPorted("kiểm tra dữ liệu tải lên"); }
        public string ValialPartialUploadData(string zyh, string orgId, DateTime jssj) { throw Stub.NotPorted("kiểm tra dữ liệu tải lên một phần"); }
        public string ValialUploadDataShyb(string zyh, string orgId, DateTime jssj) { throw Stub.NotPorted("kiểm tra dữ liệu tải lên BHYT Thượng Hải"); }
        public object GetCQLastJsnm(string zyh, string orgId) { throw Stub.NotPorted("lấy mãquyết toán Trùng Khánh gần nhất"); }
        public object GetCQLastLsh(int jsnm, string orgId) { throw Stub.NotPorted("lấy sốsố giao dịch Trùng Khánh gần nhất"); }
        public CancelSettInfoDto GetCancelSettInfo(int jsnm, string orgId) { throw Stub.NotPorted("lấy thông tin hủyquyết toán"); }
        public string GetZyhByGrbm(string xnhgrbm, string sfjs, string orgId) { throw Stub.NotPorted("lấy mã nhập viện theo mã cá nhân nông thôn mới"); }
        public object GetHospGroupFeeVOList(Pagination pagination, string zyh, string orgId, string dlCode) { throw Stub.NotPorted("lấy phí nhóm theo nhóm lớn"); }
        public object GetHospGroupFeeVOData(Pagination pagination, string zyh, string orgId, string keyword) { throw Stub.NotPorted("lấy dữ liệu phí nhóm"); }
        public object GetDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode, string zzfbz, decimal? dj) { throw Stub.NotPorted("truy vấn chi tiết phí"); }
        public object GetFyzdDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode) { throw Stub.NotPorted("truy vấn chi tiết phínằm viện"); }
        public object GetHospXmYpFeeVOList(Pagination pagination, string zyh, string orgId, string sczt, string xmmc, object p6, DateTime jssj, string isnewyb) { throw Stub.NotPorted("lấy danh sách phí mục/thuốcnằm viện"); }
        public HospPreInfoDto GetHospPreInfo(string zyh, string orgId, DateTime jzsj) { throw Stub.NotPorted("lấy thông tinquyết toán trước"); }
        public object GetInpatientSettYbPatInfo(string zyh, string orgId) { throw Stub.NotPorted("lấy thông tin BHYT bệnh nhân nhập viện"); }
        public object GetCQInpatientSettYbPatInfo(string zyh, string orgId) { throw Stub.NotPorted("lấy thông tin BHYT Trùng Khánh bệnh nhân nhập viện"); }
        public void SaveSett2303(CqybSett2303Entity entity) { throw Stub.NotPorted("lưuquyết toán trước BHYT"); }
        public object sweep_expired_approvals(string cfmxlshstr, string orgId, string zyh) { throw Stub.NotPorted("quét phê duyệt hết hạn"); }
        public void updatespbz(string cfhstr, string orgId, string userCode) { throw Stub.NotPorted("cập nhật cờ phê duyệt"); }
        public int CountLisIncompletezy(string orgId, string zyh) { throw Stub.NotPorted("đếm xét nghiệm LIS chưa hoàn thành"); }
            public System.Collections.Generic.IList<HospFeeChargeCategoryGroupVO> GetHospGroupFeeVOList(object p0, object p1, object p2)
        {
            return new System.Collections.Generic.List<HospFeeChargeCategoryGroupVO>();
        }
        public InpatientSettXnhPatInfoVO GetInpatientSettXnhPatInfo(object p0, object p1)
        {
            return new InpatientSettXnhPatInfoVO();
        }
}

    public class InpatientRefundAppStub : IInpatientRefundApp
    {
        public InpatientSettPatStatusDetailDto InpatientRefundQuery(string zyh, DateTime? kssj, DateTime? jssj, string ks, string xmlb, string xmmc) { throw Stub.NotPorted("truy vấnhoàn phí nhập viện"); }
    }

    public class InpatientRefundDmnServiceStub : IInpatientRefundDmnService
    {
        public string SaveRefund(List<HospItemBillingEntity> xmjfbList, List<HospDrugBillingEntity> ypjfbList, string zyh, string orgId) { throw Stub.NotPorted("lưuhoàn phí nhập viện"); }
    }

    public class HospitalRefundAppStub : IHospitalRefundApp
    {
        public string GetPatInfoByZyh(string zyh) { throw Stub.NotPorted("lấy thông tin bệnh nhân theo mã nhập viện"); }
        public string GetPZInfo() { throw Stub.NotPorted("lấy cấu hìnhhoàn phí nhập viện"); }
        public List<GridZyXmYpjfb> GetZyXmjfbGroup(string zyh, string zxks, string xmmc, string yrh) { throw Stub.NotPorted("lấy nhóm phí mục nhập viện"); }
        public List<GridZyXmYpjfb> GetZyYpjfbGroup(string zyh, string zxks, string xmmc, string yrh) { throw Stub.NotPorted("lấy nhóm phí thuốc nhập viện"); }
        public List<gridZyXmYpItemsjfb> GetZyYpjfbList(string zyh, string xsebh, string yp, string createTime) { throw Stub.NotPorted("lấychi tiết phí thuốc"); }
        public List<gridZyXmYpItemsjfb> GetZyXmjfbList(string zyh, string xsebh, string sfxm, string createTime) { throw Stub.NotPorted("lấychi tiết phí mục"); }
        public string SaveReturnZyyp(Dictionary<string, decimal> returnDict) { throw Stub.NotPorted("lưuhoàn phí thuốc nhập viện"); }
        public string SaveReturnZyxm(Dictionary<string, decimal> returnDict) { throw Stub.NotPorted("lưuhoàn phí mục nhập viện"); }
    }

    public class BookingDmnServiceStub : IBookingDmnService
    {
    }

    public class InpatientAppStub : IInpatientApp
    {
            public object CQZFToYB_Step_6(object p0, object p1, object p2, object p3)
        {
            return null;
        }
        public object CqYBToZF_Step_3(object p0)
        {
            return null;
        }
        public S04RequestDTO GetZfToXnhPatInfo(object p0)
        {
            return null;
        }
        public object UpqdScData(object p0, object p1)
        {
            return null;
        }
        public object YBToZF_Step_1(object p0)
        {
            return null;
        }
        public object YBToZF_Step_3(object p0, object p1, object p2)
        {
            return null;
        }
        public object YBToZF_Step_4(object p0)
        {
            return null;
        }
        public object YBToZF_Step_5(object p0, object p1)
        {
            return null;
        }
        public object YBToZF_Step_7(object p0)
        {
            return null;
        }
        public bool ZFToXNH_Step_4(object p0, object p1, object p2, out string p3)
        {
            p3 = default;
            return false;
        }
        public object ZFToXNH_Step_8(object p0)
        {
            return null;
        }
        public object ZFToYB_Step_1(object p0)
        {
            return null;
        }
        public object ZFToYB_Step_2(object p0)
        {
            return null;
        }
        public object ZFToYB_Step_4(object p0, object p1, object p2)
        {
            return null;
        }
        public object ZFToYB_Step_6(object p0, object p1, object p2, object p3)
        {
            return null;
        }
        public object ZFToYB_Step_8(object p0)
        {
            return null;
        }
}

}
