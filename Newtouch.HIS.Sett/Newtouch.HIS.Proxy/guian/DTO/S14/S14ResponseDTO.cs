using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    public class S14ResponseDTO
    {
        /// <summary>
        /// Tổng chi phí(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? totalCost { get; set; }
        /// <summary>
        /// Chi phí trong phạm vi bảo hiểm(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? insuranceCost { get; set; }
        /// <summary>
        /// mức khởi điểm (đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? undulatingLine { get; set; }
        /// <summary>
        /// Bồi thư ờng quỹ chung(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? generalRedeem { get; set; }
        /// <summary>
        /// Bồi thư ờng tai nạn(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? accidentRedeem { get; set; }
        /// <summary>
        /// Bồi thư ờng tài khoản gia đ ình(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? accRedeem { get; set; }
        /// <summary>
        /// Chi phí bồi thư ờng(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string compensateCost { get; set; }
        /// <summary>
        /// Chi phí cứu trợ dân chính(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string civilCost { get; set; }
        /// <summary>
        /// Bồi thư ờng bảo hiểm thư ơng mại bệnh nặng(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string insureCost { get; set; }
        /// <summary>
        /// Chi phí cứu trợ kế hoạch hóa gia đ ình(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaJSCost { get; set; }
        /// <summary>
        /// Chi phí bồi thư ờng sàn(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string bottomRedeem { get; set; }
        /// <summary>
        /// Chi phí bồi thư ờng sàn lần hai(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string bottomSecondRedeem { get; set; }
        /// <summary>
        /// Bồi thư ờng danh mục chính xác(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string medicineCost { get; set; }
        /// <summary>
        /// Bồi thư ờng trợ cấp giao thông(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string trafficCost { get; set; }
        /// <summary>
        /// Bồi thư ờng trợ cấp sinh hoạt(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string liveCost { get; set; }
        /// <summary>
        /// Bồi thư ờng trợ cấp y tế(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string medicalCost { get; set; }
        /// <summary>
        /// Bồi thư ờng ưu đ ãi chính xác mới(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaYFCost { get; set; }
        /// <summary>
        /// Bồi thư ờng hội ngư ời khuyết tật chính xác mới(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaCLCost { get; set; }
        /// <summary>
        /// Bồi thư ờng xóa đ ói giảm nghèo chính xác mới(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaFPCost { get; set; }
        /// <summary>
        /// Bồi thư ờng kiểm soát dịch bệnh chính xác mới(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaJKCost { get; set; }
        /// <summary>
        /// 成 viên mã 
        /// </summary>
        public string memberNo { get; set; }
        /// <summary>
        /// 成 viên họ tên
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// số chứng nhận y tế, số thẻ
        /// </summary>
        public string bookNo { get; set; }
        /// <summary>
        /// Giới tínhTên
        /// </summary>
        public string sexName { get; set; }
        /// <summary>
        /// Năm sinhNgày
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 户主họ tên
        /// </summary>
        public string masterName { get; set; }
        /// <summary>
        /// Và户主 quan hệ tên
        /// </summary>
        public string relationName { get; set; }
        /// <summary>
        /// 个人 thân phận thuộc tính tên
        /// </summary>
        public string identityName { get; set; }
        /// <summary>
        /// Số CMND mã 
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// Khi trư ớc năm 成 viên nhập vi ệnĐ ãbồi thư ờng số lần 
        /// </summary>
        public string currYearRedeemCount { get; set; }
        /// <summary>
        /// Tổng chi phí y tế nhập viện đ ã bồi thư ờng của thành viên trong năm hiện tại(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string currYearTotal { get; set; }
        /// <summary>
        /// Tổng chi phí trong phạm vi bảo hiểm nhập viện đ ã bồi thư ờng của thành viên trong năm hiện tại(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string currYearEnableMoney { get; set; }
        /// <summary>
        /// Số tiền nhập viện đ ã bồi thư ờng của thành viên trong năm hiện tại(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string currYearReddemMoney { get; set; }
        /// <summary>
        /// 成 viên gia đ ình mã 
        /// </summary>
        public string familyNo { get; set; }
        /// <summary>
        /// 成 viên gia đ ình 住址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// tham gia hợp tác thuộc tính tên
        /// </summary>
        public string joinPropName { get; set; }
        /// <summary>
        /// Khi trư ớc năm gia đ ình nhập vi ệnĐ ãbồi thư ờng số lần 
        /// </summary>
        public string currFamilyRedeemCount { get; set; }
        /// <summary>
        /// Tổng chi phí y tế nhập viện đ ã bồi thư ờng của hộ gia đ ình trong năm hiện tại(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string currFamilyTotal { get; set; }
        /// <summary>
        /// Tổng chi phí trong phạm vi bảo hiểm nhập viện đ ã bồi thư ờng của hộ gia đ ình trong năm hiện tại(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string currFamilyEnableMoney { get; set; }
        /// <summary>
        /// Số tiền nhập viện đ ã bồi thư ờng của hộ gia đ ình trong năm hiện tại(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string currFamilyReddemMoney { get; set; }
        /// <summary>
        /// Tổng chi phí y tế nhập viện lần này(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string totalCosts { get; set; }
        /// <summary>
        /// Chi phí trong phạm vi bảo hiểm nhập viện lần này(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string enableMoney { get; set; }
        /// <summary>
        /// lần này nhập việnchi phí trong 国定 cơ bản thuốcchi phí(Đ ơn vị tệ,giữ 2 chữ số thập phân)
        /// </summary>
        public string essentialMedicineMoney { get; set; }
        /// <summary>
        /// lần này nhập việnchi phí trong tỉnh补 cơ bản thuốcchi phí(Đ ơn vị tệ,giữ 2 chữ số thập phân)
        /// </summary>
        public string provinceMedicineMoney { get; set; }
        /// <summary>
        /// lần này nhập viện bồi thư ờng扣除 mức khởi trả 线số tiền(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string startMoney { get; set; }
        /// <summary>
        /// lần này nhập viện bồi thư ờng số tiền(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string calculateMoney { get; set; }
        /// <summary>
        /// Bồi thư ờng loại tên
        /// </summary>
        public string redeemTypeName { get; set; }
        /// <summary>
        /// Có hay không là bệnh đ ơn lẻ bồi thư ờng
        /// </summary>
        public string isSpecial { get; set; }
        /// <summary>
        /// Có hay không实 dòng 保底bồi thư ờng
        /// </summary>
        public string isPaul { get; set; }
        /// <summary>
        /// 追补số tiền, trong 药và国定 cơ bản thuốc nâng cao bồi thư ờng额một phần(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string anlagernMoney { get; set; }
        /// <summary>
        /// bệnh đ ơn lẻ chi phí định mức(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string fundPayMoney { get; set; }
        /// <summary>
        /// y tế cơ quan chịu chi phí(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string hospAssumeMoney { get; set; }
        /// <summary>
        /// 个人 tự 付chi phí(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string personalPayMoney { get; set; }
        /// <summary>
        /// 民政优抚 y tế 补助
        /// </summary>
        public string YFmedicalAid { get; set; }
        /// <summary>
        /// 民政城乡 y tế 救助
        /// </summary>
        public string CXmedicalAid { get; set; }
        /// <summary>
        /// 高额材料 giá trần 超额chi phí(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string materialMoney { get; set; }
        /// <summary>
        /// Bồi thư ờng分 đoạn thông tindanh sách
        /// </summary>
        public string gradeList { get; set; }
        /// <summary>
        /// lần này quyết toán tính Phư ơng thức
        /// </summary>
        public string calculationMethod { get; set; }
        /// <summary>
        /// 慈善总 sẽ thanh toán số tiền
        /// </summary>
        public string ChinaCharityPay { get; set; }
        /// <summary>
        /// Có hay không长tuần kỳ Đ ịnh mức付费
        /// </summary>
        public string isLongPeriod { get; set; }
        /// <summary>
        /// Có hay không进入大病 bảo hiểm 
        /// </summary>
        public string isCII { get; set; }
        /// <summary>
        /// 大病 bảo hiểm 合规chi phí
        /// </summary>
        public string CIIEligibleCosts { get; set; }
        /// <summary>
        /// lần này 大病 bảo hiểm mức khởi trả 线
        /// </summary>
        public string CIIStartMoney { get; set; }
        /// <summary>
        /// lần này 大病 bảo hiểm bồi thư ờng số tiền
        /// </summary>
        public string CIICalculateMoney { get; set; }
        /// <summary>
        /// lũy kế 大病 bảo hiểm bồi thư ờng额
        /// </summary>
        public string CIICumulativePay { get; set; }
        /// <summary>
        /// lũy kế 大病 bảo hiểm 扣除 mức khởi trả 线số tiền
        /// </summary>
        public string CIICumulativeStart { get; set; }
        /// <summary>
        /// lũy kế 进入大病 bảo hiểm 合规chi phí
        /// </summary>
        public string CIICumulativeEligible { get; set; }
        /// <summary>
        /// 计生 hai 户chi phí giảm trừ số tiền
        /// </summary>
        public string FamilyPlanningWaiver { get; set; }
        /// <summary>
        /// Khác bồi thư ờng
        /// </summary>
        public string OtherPay { get; set; }
        /// <summary>
        /// gia đ ình tài khoản thanh toán 
        /// </summary>
        public string familyAccount { get; set; }
        /// <summary>
        /// Số tiền bồi thư ờng bảo hiểm Dadi
        /// </summary>
        public string continentInsuranceCost { get; set; }
        /// <summary>
        /// 护理补贴
        /// </summary>
        public string nurseCost { get; set; }
        /// <summary>
        /// Số tiền bồi thư ờng trọn gói giư ờng bệnh
        /// </summary>
        public string cwAccountCost { get; set; }
        /// <summary>
        /// nuôi dư ỡng ngư ời đ ặc biệt khó khăn bồi thư ờng
        /// </summary>
        public string specialPovertyCost { get; set; }
        /// <summary>
        /// trợ giúp y tế bồi thư ờng
        /// </summary>
        public string medicalAssistanceCost { get; set; }
        /// <summary>
        /// 精准民政救助bồi thư ờng(bốn vị 精准小数)
        /// </summary>
        public string salvaMZCost { get; set; }

        public List<calcProgress> list { get; set; }
    }

    public class S14OutResponseDTO : S14ResponseDTO
    {
        /// <summary>
        /// Khác quỹ chung số tiền
        /// </summary>
        public decimal? salvaQTCost { get; set; }
        /// <summary>
        /// hợp tác nông thôn tổng số tiền
        /// </summary>
        public decimal? nhzje { get; set; }
        /// <summary>
        /// hợp tác nông thôn thanh toán tiền mặt
        /// </summary>
        public decimal? nhxjzf { get; set; }
    }
}