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

    public class SysUserDmnServiceStub : ISysUserDmnService
    {
        public bool CheckStaffIsBelongDuty(string staffId, string duty) { return false; }
    }

    public class SysStaffRepoStub : ISysStaffRepo
    {
        public string GetNameByGh(string gh, string orgId) { throw Stub.NotPorted("tra cứu tên nhân viên theo mã"); }
    }

    public class MoneyUpperLimitReminderDmnServiceStub : IMoneyUpperLimitReminderDmnService
    {
        public void GetStaffjfjeInfo(string orgId, string gh, string ks, out decimal jfjesx, out decimal yjfzje)
        {
            throw Stub.NotPorted("tra cứu hạn mức记账 của trị liệu sư");
        }
    }

    public class SyncTreatmentServiceRecordDmnServiceStub : ISyncTreatmentServiceRecordDmnService
    {
        public object GetList(string organizeId, int? clzt = null, string mzh = null, string zyh = null, string zlsgh = null)
        {
            throw Stub.NotPorted("tra cứu bản ghi đồng bộ dịch vụ điều trị");
        }
    }

    public class RefundDmnServiceStub : IRefundDmnService
    {
        public object GetBasicInfoSearchListInRegister(string keyword, string orgId)
        {
            throw Stub.NotPorted("tìm kiếm bệnh nhân đăng ký");
        }

        public object GetBasicInfoSearchList(string keyword, string orgId, string userCode, string p4, string from, string brzybzType)
        {
            throw Stub.NotPorted("tìm kiếm bệnh nhân记账");
        }
    }

    public class HospSettCancelAppStub : IHospSettCancelApp
    {
        public object GetPatHospStatusDetail(string zyh, string kh) { throw Stub.NotPorted("tra cứu trạng thái hủy结算"); }
        public void DoCancel(string zyh, int expectedjsnm, string cancelReason) { throw Stub.NotPorted("hủy结算 nhập viện"); }
    }

    public class HospSettDmnServiceStub : IHospSettDmnService
    {
        public object GridInPatientQueryGridJson(Pagination pagination, HospSettQueryReq req, string orgId) { throw Stub.NotPorted("truy vấn结算 nhập viện"); }
        public object GetPaginationSettlementList(Pagination pagination, string orgId, string keyword, string fph, DateTime? jsksrq, DateTime? jsjsrq) { throw Stub.NotPorted("truy vấn danh sách结算"); }
        public object GetPaginationZFSettlementList(Pagination pagination, string orgId, string keyword, DateTime? jsksrq, DateTime? jsjsrq) { throw Stub.NotPorted("truy vấn danh sách结算 tự phí"); }
        public object SettlementDetailsQuery(string orgId, int jsnm) { throw Stub.NotPorted("truy vấn chi tiết结算"); }
        public object SettlementDetailsItemsQuery(Pagination pagination, string zyh, string orgId, string dlCode, string jsnms, string mc) { throw Stub.NotPorted("truy vấn明细 phí结算"); }
        public void UpdateSettInvoiceNo(string orgId, int jsnm, string fph) { throw Stub.NotPorted("cập nhật số hóa đơn结算"); }
    }

    public class BookkeepInpatientAppStub : IBookkeepInpatientApp
    {
        public HosAccountingPatStatusDetailDto GetAccountingStatusDetail(string zyh) { throw Stub.NotPorted("tra cứu trạng thái记账"); }
        public void SubmitAccountingPlan(string zyh, IList<InpatientAccountingPlanItemDto> xmList) { throw Stub.NotPorted("gửi kế hoạch记账"); }
        public IList<HospAccountingPlanWaitingExeVO> WaitingAccountingPlanQuery(IList<string> zyhList, string from = null) { throw Stub.NotPorted("truy vấn kế hoạch记账 chờ thực hiện"); }
        public void ExecuteAccountingPlan(IList<zxGirdDto> zxItem, DateTime? zxrq, string from = null) { throw Stub.NotPorted("thực hiện kế hoạch记账"); }
        public void CancelPreStopAccountingPlan(string jzjhmxIdStr) { throw Stub.NotPorted("hủy dừng trước kế hoạch记账"); }
        public InpatientHosAccountingPatStatusDetailDto GetInpatientAccountingStatusDetail(string zyh) { throw Stub.NotPorted("tra cứu trạng thái记账 nội trú"); }
    }

    public class BookkeepInHosDmnServiceStub : IBookkeepInHosDmnService
    {
        public DateTime? GetLastValidMidwaySettTime(string zyh, string orgId) { throw Stub.NotPorted("lấy thời điểm结算 giữa kỳ hợp lệ gần nhất"); }
        public object SelectAccountingExecuteList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, string zls, string cxzt, string jzjhmxId, string sfzt) { throw Stub.NotPorted("truy vấn thực hiện记账"); }
        public object SelectOutAccountingExecuteList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, string zls, string cxzt, string jzjhmxId, string sfzt) { throw Stub.NotPorted("truy vấn thực hiện记账 ngoại trú"); }
        public object SelectAccountingPlanList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, int? yzxz) { throw Stub.NotPorted("truy vấn kế hoạch记账"); }
        public object GetFormPlan(string keyValue, string orgId, string from) { throw Stub.NotPorted("lấy biểu mẫu kế hoạch记账"); }
        public object GetFormPlan(Pagination pagination, string jzjhmxId, string orgId, string from) { throw Stub.NotPorted("lấy chi tiết kế hoạch记账"); }
        public void RevokeExec(string jzjhmxId, string zxbh, string orgId, string from) { throw Stub.NotPorted("hủy thực hiện记账"); }
        public void UpdateExec(cxzxGirdDto zxItem, string from, string orgId) { throw Stub.NotPorted("cập nhật thực hiện记账"); }
        public void StopAccountingPlan(List<string> mxIdList, DateTime stopDate, string deptCode, string rygh) { throw Stub.NotPorted("dừng kế hoạch记账"); }
        public void SaveInpatientAccounting(string zyh, IList<InpatientAccountingItemDto> xmList, string orgId, string userCode) { throw Stub.NotPorted("lưu记账 nội trú"); }
        public string getzfxzById(string sfxmcode, string orgId) { throw Stub.NotPorted("lấy tính chất tự phí theo mã收费"); }
        public IList<VerificationVO> Verification(string codes, string orgId, string yfdm) { throw Stub.NotPorted("kiểm tra tồn kho vật tư"); }
        public string SaveFee(List<HospItemBillingEntity> xmjfbList, List<HospDrugBillingEntity> ypjfbList) { throw Stub.NotPorted("lưu phí记账"); }
        public object GetChargeTemplate(string ks, string orgId) { throw Stub.NotPorted("lấy mẫu收费"); }
        public object GetChargeItemContent(string sfmbbh, string orgId) { throw Stub.NotPorted("lấy nội dung mẫu收费"); }
        public IList<UnSettedFeeItemVO> GetChargeItemPaginationList(Pagination pagination, string zyh, string orgId, DateTime startTime, DateTime endTime) { throw Stub.NotPorted("lấy danh sách phí chưa结算"); }
        public void MidwaySettlement(string zyh, string orgId, DateTime startTime, DateTime endTime, int? expectedCount) { throw Stub.NotPorted("结算 giữa kỳ"); }
        public void DischargeSettlement(string zyh, string orgId, DateTime cyrq) { throw Stub.NotPorted("结算 xuất viện"); }
        public void CancelTheLastMidwaySett(string zyh, string zffs1, string orgId, out decimal refoundmoney) { throw Stub.NotPorted("hủy结算 giữa kỳ gần nhất"); }
    }

    public class OutPatientDmnServiceStub : IOutPatientDmnService
    {
        public object SelectAccountingPlanList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, int? zsftx, int? sycstx, string sfzt) { throw Stub.NotPorted("truy vấn kế hoạch记账 ngoại trú"); }
        public void overAccountingPlan(string mxId, string orgId, string rygh) { throw Stub.NotPorted("kết thúc kế hoạch记账 ngoại trú"); }
    }

    public class OutPatientAppStub : IOutPatientApp
    {
        public void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> xmList, string orgId) { throw Stub.NotPorted("lưu thông tin tài khoản ngoại trú"); }
    }

    public class OutPatChargeAppStub : IOutPatChargeApp
    {
        public List<ChargeItemDetailVO> GetYpItemInfo(string keyword) { throw Stub.NotPorted("tra cứu thuốc/mục收费"); }
    }

    public class SysPatiChargeAddAppStub : ISysPatiChargeAddApp
    {
    }

    public class SysPatiChargeWaiverAppStub : ISysPatiChargeWaiverApp
    {
    }

    public class HospItemFeeAppStub : IHospItemFeeApp
    {
        public void SubmitForm(HospItemBillingEntity entity, int? keyValue) { throw Stub.NotPorted("lưu phí mục记账"); }
    }

    public class HospMedicinFeeAppStub : IHospMedicinFeeApp
    {
        public void SubmitForm(HospDrugBillingEntity entity, int? keyValue) { throw Stub.NotPorted("lưu phí thuốc记账"); }
    }

    public class HosPatAccDmnServiceStub : IHosPatAccDmnService
    {
    }

    public class AccountManageAppStub : IAccountManageApp
    {
        public HosPatAccInfoDto GetHosPatInfo(string zyh) { throw Stub.NotPorted("lấy thông tin tài khoản nhập viện"); }
        public string GetFinRepSJPZH() { throw Stub.NotPorted("lấy số phiếu thu tài chính"); }
        public bool PayDepositPost(DeposDto dto, out string szId) { throw Stub.NotPorted("nạp tiền đặt cọc"); }
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
        public object GetInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver) { throw Stub.NotPorted("tra cứu trạng thái结算 xuất viện"); }
        public object GetPartialInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver) { throw Stub.NotPorted("tra cứu trạng thái结算 giữa kỳ"); }
        public object GetCancelSettleStatusDetail(string zyh, string sfz, string kh, string cardType) { throw Stub.NotPorted("tra cứu trạng thái hủy结算"); }
        public object GetZyUploadDetail(string zyh, string jslx) { throw Stub.NotPorted("lấy chi tiết tải lên BHYT"); }
        public object GetInpatientSettleJe(string zyh, string sczt, object p3, DateTime jssj, string xmmc) { throw Stub.NotPorted("lấy số tiền结算"); }
        public void SavePartialSettle(string zyh, DateTime startTime, DateTime endTime, string fph, InpatientSettFeeRelatedDTO feeRelated, CQZyjs05Dto ybfeeRelated, string outTradeNo, string jslx, out int jsnm) { throw Stub.NotPorted("lưu结算 giữa kỳ"); }
        public void DoCancel(string zyh, int expectedjsnm, string cancelReason, string cancelyblsh, out string outTradeNo, out decimal refundAmount) { throw Stub.NotPorted("hủy结算 xuất viện"); }
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
        public object GetCQLastJsnm(string zyh, string orgId) { throw Stub.NotPorted("lấy mã结算 Trùng Khánh gần nhất"); }
        public object GetCQLastLsh(int jsnm, string orgId) { throw Stub.NotPorted("lấy số流水 Trùng Khánh gần nhất"); }
        public CancelSettInfoDto GetCancelSettInfo(int jsnm, string orgId) { throw Stub.NotPorted("lấy thông tin hủy结算"); }
        public string GetZyhByGrbm(string xnhgrbm, string sfjs, string orgId) { throw Stub.NotPorted("lấy mã nhập viện theo mã cá nhân nông thôn mới"); }
        public object GetHospGroupFeeVOList(Pagination pagination, string zyh, string orgId, string dlCode) { throw Stub.NotPorted("lấy phí nhóm theo大类"); }
        public object GetHospGroupFeeVOData(Pagination pagination, string zyh, string orgId, string keyword) { throw Stub.NotPorted("lấy dữ liệu phí nhóm"); }
        public object GetDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode, string zzfbz, decimal? dj) { throw Stub.NotPorted("truy vấn chi tiết phí"); }
        public object GetFyzdDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode) { throw Stub.NotPorted("truy vấn chi tiết phí住院"); }
        public object GetHospXmYpFeeVOList(Pagination pagination, string zyh, string orgId, string sczt, string xmmc, object p6, DateTime jssj, string isnewyb) { throw Stub.NotPorted("lấy danh sách phí mục/thuốc住院"); }
        public HospPreInfoDto GetHospPreInfo(string zyh, string orgId, DateTime jzsj) { throw Stub.NotPorted("lấy thông tin预结算"); }
        public object GetInpatientSettYbPatInfo(string zyh, string orgId) { throw Stub.NotPorted("lấy thông tin BHYT bệnh nhân nhập viện"); }
        public object GetCQInpatientSettYbPatInfo(string zyh, string orgId) { throw Stub.NotPorted("lấy thông tin BHYT Trùng Khánh bệnh nhân nhập viện"); }
        public void SaveSett2303(CqybSett2303Entity entity) { throw Stub.NotPorted("lưu预结算 BHYT"); }
        public object sweep_expired_approvals(string cfmxlshstr, string orgId, string zyh) { throw Stub.NotPorted("quét phê duyệt hết hạn"); }
        public void updatespbz(string cfhstr, string orgId, string userCode) { throw Stub.NotPorted("cập nhật cờ phê duyệt"); }
        public int CountLisIncompletezy(string orgId, string zyh) { throw Stub.NotPorted("đếm xét nghiệm LIS chưa hoàn thành"); }
    }

    public class InpatientRefundAppStub : IInpatientRefundApp
    {
        public InpatientSettPatStatusDetailDto InpatientRefundQuery(string zyh, DateTime? kssj, DateTime? jssj, string ks, string xmlb, string xmmc) { throw Stub.NotPorted("truy vấn退费 nhập viện"); }
    }

    public class InpatientRefundDmnServiceStub : IInpatientRefundDmnService
    {
        public string SaveRefund(List<HospItemBillingEntity> xmjfbList, List<HospDrugBillingEntity> ypjfbList, string zyh, string orgId) { throw Stub.NotPorted("lưu退费 nhập viện"); }
    }

    public class HospitalRefundAppStub : IHospitalRefundApp
    {
        public string GetPatInfoByZyh(string zyh) { throw Stub.NotPorted("lấy thông tin bệnh nhân theo mã nhập viện"); }
        public string GetPZInfo() { throw Stub.NotPorted("lấy cấu hình退费 nhập viện"); }
        public List<GridZyXmYpjfb> GetZyXmjfbGroup(string zyh, string zxks, string xmmc, string yrh) { throw Stub.NotPorted("lấy nhóm phí mục nhập viện"); }
        public List<GridZyXmYpjfb> GetZyYpjfbGroup(string zyh, string zxks, string xmmc, string yrh) { throw Stub.NotPorted("lấy nhóm phí thuốc nhập viện"); }
        public List<gridZyXmYpItemsjfb> GetZyYpjfbList(string zyh, string xsebh, string yp, string createTime) { throw Stub.NotPorted("lấy明细 phí thuốc"); }
        public List<gridZyXmYpItemsjfb> GetZyXmjfbList(string zyh, string xsebh, string sfxm, string createTime) { throw Stub.NotPorted("lấy明细 phí mục"); }
        public string SaveReturnZyyp(Dictionary<string, decimal> returnDict) { throw Stub.NotPorted("lưu退费 thuốc nhập viện"); }
        public string SaveReturnZyxm(Dictionary<string, decimal> returnDict) { throw Stub.NotPorted("lưu退费 mục nhập viện"); }
    }

    public class BookingDmnServiceStub : IBookingDmnService
    {
    }

    public class InpatientAppStub : IInpatientApp
    {
    }

    public class PayAppStub : IPayApp
    {
        public int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason, string outRequestNo, out string errorMsg)
        {
            throw Stub.NotPorted("hoàn tiền qua cổng thanh toán");
        }
    }
}
