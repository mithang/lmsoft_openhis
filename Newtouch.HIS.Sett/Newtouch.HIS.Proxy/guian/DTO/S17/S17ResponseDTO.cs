using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    public class inp
    {
        /// <summary>
        /// năm 
        /// </summary>
        public string year { get; set; }
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// mã cá nhân 
        /// </summary>
        public string memberId { get; set; }
        /// <summary>
        /// Số thẻ chứng nhận y tế
        /// </summary>
        public string medicalNo { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string memberName { get; set; }
        /// <summary>
        /// Bệnh nhânGiới tính
        /// </summary>
        public string memberSex { get; set; }
        /// <summary>
        /// Bệnh nhânSố CMND
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// Tuổi
        /// </summary>
        public string memberAge { get; set; }
        /// <summary>
        /// Số hộ gia đ ình
        /// </summary>
        public string familyId { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string inpatientNo { get; set; }
        /// <summary>
        /// Loại khám bệnh
        /// </summary>
        public string inpatientType { get; set; }
        /// <summary>
        /// Đ ịa chỉ
        /// </summary>
        public string areaName { get; set; }
        /// <summary>
        /// Thuộc tính nhân sự
        /// </summary>
        public string memberPro { get; set; }
        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        public string admissionDate { get; set; }
        /// <summary>
        /// Ngày xuất viện
        /// </summary>
        public string dischargeDate { get; set; }
        /// <summary>
        /// thực tế nhập viện số ngày
        /// </summary>
        public string dateNum { get; set; }
        /// <summary>
        /// khám chữa bệnh mã cơ quan
        /// </summary>
        public string hospitalNo { get; set; }
        /// <summary>
        /// khám chữa bệnh cơ quan tên
        /// </summary>
        public string hospitalName { get; set; }
        /// <summary>
        /// khám chữa bệnh cơ quan cấp bậc 
        /// </summary>
        public string HospitalLevel { get; set; }
        /// <summary>
        /// Khoa nhập viện(xem từ điển khoa)
        /// </summary>
        public string admissionDepartments { get; set; }
        /// <summary>
        /// Khoa nhập viện
        /// </summary>
        public string admissionDepartmentsName { get; set; }
        /// <summary>
        /// Khoa xuất viện(xem từ điển khoa)
        /// </summary>
        public string dischargeDepartments { get; set; }
        /// <summary>
        /// Khoa xuất viện
        /// </summary>
        public string dischargeDepartmentsName { get; set; }
        /// <summary>
        /// Bác sĩ điều trị
        /// </summary>
        public string treatingPhysician { get; set; }
        /// <summary>
        /// Trạng thái nhập viện
        /// </summary>
        public string admissionStatus { get; set; }
        /// <summary>
        /// Xuất việnTrạng thái
        /// </summary>
        public string dischargeStatus { get; set; }
        /// <summary>
        /// Mã bệnh
        /// </summary>
        public string diseaseCode { get; set; }
        /// <summary>
        /// bệnh tên
        /// </summary>
        public string diseaseName { get; set; }
        /// <summary>
        /// Khu bệnh
        /// </summary>
        public string wardArea { get; set; }
        /// <summary>
        /// phòng bệnh số 
        /// </summary>
        public string wardNo { get; set; }
        /// <summary>
        /// giư ờng bệnh số 
        /// </summary>
        public string berthNo { get; set; }
        /// <summary>
        /// Loại bồi thư ờng
        /// </summary>
        public string compensateType { get; set; }
        /// <summary>
        /// Bồi thư ờng cơ quan
        /// </summary>
        public string compensateMechanism { get; set; }
        /// <summary>
        /// Bồi thư ờng cơ quan tên
        /// </summary>
        public string compensateMechanismName { get; set; }
        /// <summary>
        /// Bồi thư ờng ngày 
        /// </summary>
        public string compensateDate { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string managerCode { get; set; }
        /// <summary>
        /// mức khởi điểm 
        /// </summary>
        public decimal undulatingLine { get; set; }
        /// <summary>
        /// tự 付số tiền
        /// </summary>
        public string setPayCost { get; set; }
        /// <summary>
        /// 核 tính số tiền
        /// </summary>
        public string auditCompensateCost { get; set; }
        /// <summary>
        /// 初审扣减
        /// </summary>
        public string auditDeductionCost { get; set; }
        /// <summary>
        /// 终审扣减
        /// </summary>
        public string lastAuditDeductionCost { get; set; }
        /// <summary>
        /// bảo hiểm nông nghiệp bồi thư ờng比
        /// </summary>
        public string compensateProportion { get; set; }
        /// <summary>
        /// Giấy tờ đ ã đ ầy đ ủ chưa
        /// </summary>
        public string isIncrease { get; set; }
        /// <summary>
        /// Có chuyển viện hay không
        /// </summary>
        public string isReferra { get; set; }
        /// <summary>
        /// Thao tác viên số hiệu
        /// </summary>
        public string userCode { get; set; }
        /// <summary>
        /// Thao tác viên 
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// Có hay khôngLiên kếtSố thẻ ngân hàng
        /// </summary>
        public string isBankCardNo { get; set; }
        /// <summary>
        /// bảo hiểm số tiền
        /// </summary>
        public string bxAccount { get; set; }
        /// <summary>
        /// Tính公 kiểu 
        /// </summary>
        public string jsgs { get; set; }
        /// <summary>
        /// 大额bồi thư ờng số tiền
        /// </summary>
        public string bigAccountCost { get; set; }
        /// <summary>
        /// Số tiền bồi thư ờng trọn gói giư ờng bệnh
        /// </summary>
        public string cwAccountCost { get; set; }
        /// <summary>
        /// 二 lần bồi thư ờng số tiền
        /// </summary>
        public string secondCompensateCost { get; set; }
        /// <summary>
        /// gia đ ình tài khoản抵扣
        /// </summary>
        public string accRedeem { get; set; }
        /// <summary>
        /// lũy kế bồi thư ờng có hay kh ôngĐ ã达封顶线
        /// </summary>
        public string isOverLimitLine { get; set; }
        /// <summary>
        /// Có hay không保底 0 không là1 là
        /// </summary>
        public string isLessLimit { get; set; }
        /// <summary>
        /// năm bồi thư ờng số lần 
        /// </summary>
        public string inpYearCount { get; set; }
        /// <summary>
        /// Bệnh nhân thuộc về huyện Quận số hiệu
        /// </summary>
        public string countryCode { get; set; }
        /// <summary>
        /// tham gia hợp tác huyện
        /// </summary>
        public string countryName { get; set; }
        /// <summary>
        /// Bồi thư ờng trạng thái
        /// </summary>
        public bool state { get; set; }
        /// <summary>
        /// 终审 dấu hiệu trạng thái
        /// </summary>
        public string lastAuditState { get; set; }
        /// <summary>
        /// ThuốcChi phí
        /// </summary>
        public string yp { get; set; }
        /// <summary>
        /// kiểm tra chi phí
        /// </summary>
        public string jc { get; set; }
        /// <summary>
        /// điều trị chi phí
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// giư ờng bệnh chi phí
        /// </summary>
        public string cw { get; set; }
        /// <summary>
        /// 材料chi phí
        /// </summary>
        public string cl { get; set; }
        /// <summary>
        /// KhácChi phí
        /// </summary>
        public string qt { get; set; }
        /// <summary>
        /// bảo hiểm thư ơng mại bồi thư ờng số tiền
        /// </summary>
        public string sbCost { get; set; }
        /// <summary>
        /// bảo hiểm thư ơng mại tải lên dấu hiệu 
        /// </summary>
        public string sbState { get; set; }
        /// <summary>
        /// 清 tính 锁定trạng thái
        /// </summary>
        public string lockedStatus { get; set; }
        /// <summary>
        /// Số điện thoại liên hệ
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// Số thẻ ngân hàng
        /// </summary>
        public string bankCardNo { get; set; }
        /// <summary>
        /// Họ của chủ tài khoản thẻ ngân hàng名
        /// </summary>
        public string accountHolder { get; set; }
        /// <summary>
        /// Quan hệ giữa bệnh nhân và chủ tài khoản
        /// </summary>
        public string holderRelation { get; set; }
        /// <summary>
        /// Tổng chi phí(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal totalCost { get; set; }
        /// <summary>
        /// Chi phí trong phạm vi bảo hiểm(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal insuranceCost { get; set; }
        /// <summary>
        /// Chi phí bồi thư ờng(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string compensateCost { get; set; }
        /// <summary>
        /// 精准民政 thuộc tính 
        /// </summary>
        public string jzmzName { get; set; }
        /// <summary>
        /// 精准计生 thuộc tính 
        /// </summary>
        public string jzjsName { get; set; }
        /// <summary>
        /// 精准目录进口目录chi phí
        /// </summary>
        public string jzImportCost { get; set; }
        /// <summary>
        /// 精准目录国产目录chi phí
        /// </summary>
        public string jzHomeCost { get; set; }
        /// <summary>
        /// Bồi thư ờng danh mục chính xác số tiền
        /// </summary>
        public string jzCompensateCost { get; set; }
        /// <summary>
        /// 兜底bồi thư ờng số tiền
        /// </summary>
        public string ddCost { get; set; }
        /// <summary>
        /// Bồi thư ờng trợ cấp giao thông
        /// </summary>
        public string trafficCost { get; set; }
        /// <summary>
        /// Bồi thư ờng trợ cấp sinh hoạt
        /// </summary>
        public string liveCost { get; set; }
        /// <summary>
        /// Bồi thư ờng trợ cấp y tế
        /// </summary>
        public string medicalCost { get; set; }
        /// <summary>
        /// Bồi thư ờng ưu đ ãi chính xác mới
        /// </summary>
        public string salvaYFCost { get; set; }
        /// <summary>
        /// Bồi thư ờng hội ngư ời khuyết tật chính xác mới
        /// </summary>
        public string salvaCLCost { get; set; }
        /// <summary>
        /// Bồi thư ờng xóa đ ói giảm nghèo chính xác mới
        /// </summary>
        public string salvaFPCost { get; set; }
        /// <summary>
        /// Bồi thư ờng kiểm soát dịch bệnh chính xác mới
        /// </summary>
        public string salvaJKCost { get; set; }
        /// <summary>
        /// mới 精准优抚 thuộc tính 
        /// </summary>
        public string salvaYFName { get; set; }
        /// <summary>
        /// mới 精准残联 thuộc tính 
        /// </summary>
        public string salvaCLName { get; set; }
        /// <summary>
        /// mới 精准扶贫 thuộc tính 
        /// </summary>
        public string salvaFPName { get; set; }
        /// <summary>
        /// mới 精准疾控 thuộc tính 
        /// </summary>
        public string salvaJKName { get; set; }
        /// <summary>
        /// 兜底二 lần bồi thư ờng số tiền
        /// </summary>
        public string ddSecondCost { get; set; }
        /// <summary>
        /// Bồi thư ờng tai nạn
        /// </summary>
        public string accidentCost { get; set; }
        /// <summary>
        /// nuôi dư ỡng ngư ời đ ặc biệt khó khăn 
        /// </summary>
        public decimal? specialPovertyCost { get; set; }
        /// <summary>
        /// trợ giúp y tế 
        /// </summary>
        public decimal? medicalAssistanceCost { get; set; }
        /// <summary>
        /// Số tiền bồi thư ờng bảo hiểm Dadi
        /// </summary>
        public decimal? continentInsuranceCost { get; set; }
        /// <summary>
        /// 护理bồi thư ờng
        /// </summary>
        public decimal? nurseCost { get; set; }
    }
}