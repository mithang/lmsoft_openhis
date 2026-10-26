namespace Newtouch.HIS.Proxy.guian.DTO.S27
{
    /// <summary>
    /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành lấy quyết toán ngoại trú đ ơn thông tin bản tin trả về
    /// </summary>
    public class S27ResponseDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng ngoại trú
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// mã cá nhân 
        /// </summary>
        public string memberId { get; set; }

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
        /// Bệnh nhânTuổi
        /// </summary>
        public string memberAge { get; set; }

        /// <summary>
        /// Số hộ gia đ ình
        /// </summary>
        public string familyId { get; set; }

        /// <summary>
        /// Số thẻ chứng nhận y tế
        /// </summary>
        public string medicalNo { get; set; }

        /// <summary>
        /// Loại khám bệnh
        /// </summary>
        public string inpatientType { get; set; }

        /// <summary>
        /// Khám bệnh ngày 
        /// </summary>
        public string inpatientDate { get; set; }

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
        public string hospitalLevel { get; set; }

        /// <summary>
        /// 民政性质
        /// </summary>
        public string mzPro { get; set; }

        /// <summary>
        /// Thuộc tính nhân sự
        /// </summary>
        public string memberPro { get; set; }

        /// <summary>
        /// Thuộc tính nhân sự
        /// </summary>
        public string memberProName { get; set; }

        /// <summary>
        /// Khoa khám bệnh
        /// </summary>
        public string inpatientDepartments { get; set; }

        /// <summary>
        /// Khoa khám bệnh
        /// </summary>
        public string inpatientDepartmentsName { get; set; }

        /// <summary>
        /// Bác sĩ điều trị
        /// </summary>
        public string treatingPhysician { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        public string diseaseCode { get; set; }

        /// <summary>
        /// bệnh tên
        /// </summary>
        public string diseaseName { get; set; }

        /// <summary>
        /// Chẩn đoán sơ bộ
        /// </summary>
        public string initialDiagnosis { get; set; }

        /// <summary>
        /// Loại bồi thư ờng
        /// </summary>
        public string compensateType { get; set; }

        /// <summary>
        /// Loại bồi thư ờng
        /// </summary>
        public string compensateTypeName { get; set; }

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
        /// Tổng chi phí
        /// </summary>
        public string totalCost { get; set; }

        /// <summary>
        /// Chi phí trong phạm vi bảo hiểm
        /// </summary>
        public string insuranceCost { get; set; }

        /// <summary>
        /// mức khởi điểm 
        /// </summary>
        public string undulatingLine { get; set; }

        /// <summary>
        /// Chi phí bồi thư ờng
        /// </summary>
        public string compensateCost { get; set; }

        /// <summary>
        /// tự 付số tiền
        /// </summary>
        public string setPayCost { get; set; }

        /// <summary>
        /// 保 trong tự 付số tiền
        /// </summary>
        public string insurancePayCost { get; set; }

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
        /// Số điện thoại liên hệ
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// bảo hiểm nông nghiệp bồi thư ờng比
        /// </summary>
        public string compensateProportion { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Giấy tờ đ ã đ ầy đ ủ chưa
        /// </summary>
        public string isIncrease { get; set; }

        /// <summary>
        /// Có chuyển viện hay không
        /// </summary>
        public string isReferra { get; set; }

        /// <summary>
        /// Có hay không扣 gia đ ình tài khoản
        /// </summary>
        public string isAccountPay { get; set; }

        /// <summary>
        /// Là quỹ chung 
        /// </summary>
        public string isOutpPay { get; set; }

        /// <summary>
        /// Đ ịa chỉ
        /// </summary>
        public string areaName { get; set; }

        /// <summary>
        /// Thao tác viên số hiệu
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Thao tác viên 
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Loại bệnh nhân
        /// </summary>
        public string inpatientTypeOflocal { get; set; }

        /// <summary>
        /// Số thẻ ngân hàng
        /// </summary>
        public string bankCardNo { get; set; }

        /// <summary>
        /// Có hay khôngLiên kếtSố thẻ ngân hàng
        /// </summary>
        public string isBankCardNo { get; set; }

        /// <summary>
        /// Họ của chủ tài khoản thẻ ngân hàng
        /// </summary>
        public string accountHolder { get; set; }

        /// <summary>
        /// Quan hệ giữa bệnh nhân và chủ tài khoản
        /// </summary>
        public string holderRelation { get; set; }

        /// <summary>
        /// khám chữa bệnh cơ quan tênghi chú
        /// </summary>
        public string hospitalNameRemark { get; set; }

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
        /// Có hay không保底
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
        /// Bệnh nhân thuộc về huyện Quận tên
        /// </summary>
        public string countryName { get; set; }

        /// <summary>
        /// 初审thời gian
        /// </summary>
        public string auditDate { get; set; }

        /// <summary>
        /// 初审cơ quan
        /// </summary>
        public string auditOrg { get; set; }

        /// <summary>
        /// 初审 nhân sự 
        /// </summary>
        public string auditUser { get; set; }

        /// <summary>
        /// 初审扣减 nguyên nhân 
        /// </summary>
        public string auditRemark { get; set; }

        /// <summary>
        /// 终审thời gian
        /// </summary>
        public string lastAuditDate { get; set; }

        /// <summary>
        /// 终审cơ quan
        /// </summary>
        public string lastAuditOrg { get; set; }

        /// <summary>
        /// 终审 nhân sự 
        /// </summary>
        public string lastAuditUser { get; set; }

        /// <summary>
        /// 终审扣减 nguyên nhân 
        /// </summary>
        public string lastAuditRemark { get; set; }

        /// <summary>
        /// Bồi thư ờng trạng thái
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// ghi đ ỏ nhân sự 
        /// </summary>
        public string destoryUserName { get; set; }

        /// <summary>
        /// ghi đ ỏ thời gian
        /// </summary>
        public string destoryDate { get; set; }

        /// <summary>
        /// 终审 dấu hiệu trạng thái
        /// </summary>
        public string lastAuditState { get; set; }

        /// <summary>
        /// Thuốc费
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// kiểm tra 费
        /// </summary>
        public string jc { get; set; }

        /// <summary>
        /// điều trị 费
        /// </summary>
        public string zl { get; set; }

        /// <summary>
        /// giư ờng bệnh 费
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 材料费
        /// </summary>
        public string cl { get; set; }

        /// <summary>
        /// Khác费
        /// </summary>
        public string qt { get; set; }

        /// <summary>
        /// 民政局bồi thư ờng số tiền
        /// </summary>
        public string mzjCompensateCost { get; set; }

        /// <summary>
        /// 计生bồi thư ờng số tiền
        /// </summary>
        public string jsCompensateCost { get; set; }

        /// <summary>
        /// 大病 bảo hiểm bồi thư ờng số tiền
        /// </summary>
        public string sbCost { get; set; }

        /// <summary>
        /// 大病 bảo hiểm tải lên dấu hiệu 
        /// </summary>
        public string sbState { get; set; }

        /// <summary>
        /// 付 khoản Đ ối tư ợng
        /// </summary>
        public string bus_type { get; set; }

        /// <summary>
        /// 锁定
        /// </summary>
        public string lockedStatus { get; set; }

        /// <summary>
        /// chủ yếu điều trị phư ơng thức 
        /// </summary>
        public string cureType { get; set; }

        /// <summary>
        /// chủ yếu điều trị phư ơng thức 
        /// </summary>
        public string cureTypeName { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string degree { get; set; }

        /// <summary>
        /// Khám bệnh số lần 
        /// </summary>
        public string visits { get; set; }

        /// <summary>
        /// gia đ ình số dư tài khoản
        /// </summary>
        public string accountLeftCost { get; set; }

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
        /// 合医二 lần bồi thư ờng
        /// </summary>
        public decimal medicalSecondCost { get; set; }

        /// <summary>
        /// nuôi dư ỡng ngư ời đ ặc biệt khó khăn 
        /// </summary>
        public decimal specialPovertyCost { get; set; }

        /// <summary>
        /// trợ giúp y tế 
        /// </summary>
        public decimal medicalAssistanceCost { get; set; }

        /// <summary>
        /// Bác sĩ cấp bậc 
        /// </summary>
        public string docatorRank { get; set; }

        /// <summary>
        /// Số tiền bồi thư ờng bảo hiểm Dadi
        /// </summary>
        public decimal continentInsuranceCost { get; set; }

    }
}