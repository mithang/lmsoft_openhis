using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    public class S13ResponseDTO
    {
        /// <summary>
        /// Tổng chi phí(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal totalCost { get; set; }
        /// <summary>
        /// Chi phí trong phạm vi bảo hiểm(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal insuranceCost { get; set; }
        /// <summary>
        /// Bồi thư ờng quỹ chung(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal generalRedeem { get; set; }
        /// <summary>
        /// Bồi thư ờng tai nạn(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal accidentRedeem { get; set; }
        /// <summary>
        /// Bồi thư ờng tài khoản gia đ ình(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal accRedeem { get; set; }
        /// <summary>
        /// mức khởi điểm (đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal undulatingLine { get; set; }
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
        /// thành viên mã 
        /// </summary>
        public string memberNo { get; set; }
        /// <summary>
        /// thành viên họ tên
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
        /// chủ hộhọ tên
        /// </summary>
        public string masterName { get; set; }
        /// <summary>
        /// Vàchủ hộ quan hệ tên
        /// </summary>
        public string relationName { get; set; }
        /// <summary>
        /// cá nhân thân phận thuộc tính tên
        /// </summary>
        public string identityName { get; set; }
        /// <summary>
        /// Số CMND mã 
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// Khi trư ớc năm thành viên nhập vi ệnĐ ãbồi thư ờng số lần 
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
        /// thành viên gia đ ình mã 
        /// </summary>
        public string familyNo { get; set; }
        /// <summary>
        /// thành viên gia đ ình địa chỉ
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
        /// lần này nhập việnchi phí trong quy định quốc gia cơ bản thuốcchi phí(Đ ơn vị tệ,giữ 2 chữ số thập phân)
        /// </summary>
        public string essentialMedicineMoney { get; set; }
        /// <summary>
        /// lần này nhập việnchi phí trong tỉnhbổ sung cơ bản thuốcchi phí(Đ ơn vị tệ,giữ 2 chữ số thập phân)
        /// </summary>
        public string provinceMedicineMoney { get; set; }
        /// <summary>
        /// lần này nhập viện bồi thư ờngkhấu trừ mức khởi trả mứcsố tiền(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
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
        /// Có hay khôngthực hiện dòng bảo đảm tối thiểubồi thư ờng
        /// </summary>
        public string isPaul { get; set; }
        /// <summary>
        /// bổ sung truy thusố tiền, trong thuốcvàquy định quốc gia cơ bản thuốc nâng cao bồi thư ờngsố tiềnmột phần(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
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
        /// cá nhân tự trảchi phí(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string personalPayMoney { get; set; }
        /// <summary>
        /// ưu đãi dân chính y tế trợ cấp
        /// </summary>
        public string YFmedicalAid { get; set; }
        /// <summary>
        /// thành thị và nông thôn dân chính y tế cứu trợ
        /// </summary>
        public string CXmedicalAid { get; set; }
        /// <summary>
        /// vật tư giá cao giá trần vượt mứcchi phí(đ ơn vị tính bằng tệ, giữ 2 chữ số thập phân)
        /// </summary>
        public string materialMoney { get; set; }
        /// <summary>
        /// Bồi thư ờngphân đoạn thông tindanh sách
        /// </summary>
        public string gradeList { get; set; }
        /// <summary>
        /// lần này quyết toán tính Phư ơng thức
        /// </summary>
        public string calculationMethod { get; set; }
        /// <summary>
        /// tổng hội từ thiện sẽ thanh toán số tiền
        /// </summary>
        public string ChinaCharityPay { get; set; }
        /// <summary>
        /// Có hay khôngdàituần kỳ Đ ịnh mứctrả phí
        /// </summary>
        public string isLongPeriod { get; set; }
        /// <summary>
        /// Có hay khôngvào bảo hiểm bệnh nặng bảo hiểm 
        /// </summary>
        public string isCII { get; set; }
        /// <summary>
        /// bệnh nặng bảo hiểm hợp quychi phí
        /// </summary>
        public string CIIEligibleCosts { get; set; }
        /// <summary>
        /// lần này bệnh nặng bảo hiểm mức khởi trả mức
        /// </summary>
        public string CIIStartMoney { get; set; }
        /// <summary>
        /// lần này bệnh nặng bảo hiểm bồi thư ờng số tiền
        /// </summary>
        public string CIICalculateMoney { get; set; }
        /// <summary>
        /// lũy kế bệnh nặng bảo hiểm bồi thư ờngsố tiền
        /// </summary>
        public string CIICumulativePay { get; set; }
        /// <summary>
        /// lũy kế bệnh nặng bảo hiểm khấu trừ mức khởi trả mứcsố tiền
        /// </summary>
        public string CIICumulativeStart { get; set; }
        /// <summary>
        /// lũy kế vào bảo hiểm bệnh nặng bảo hiểm hợp quychi phí
        /// </summary>
        public string CIICumulativeEligible { get; set; }
        /// <summary>
        /// kế hoạch hóa gia đình hai hộchi phí giảm trừ số tiền
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
        /// trợ cấp điều dưỡng
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

        public List<calcProgress> list { get; set; }
    }
    public class S13OutResponseDTO : S13ResponseDTO
    {
        /// <summary>
        /// Khác quỹ chung số tiền
        /// </summary>
        public decimal salvaQTCost { get; set; }
        /// <summary>
        /// hợp tác nông thôn tổng số tiền
        /// </summary>
        public decimal nhzje { get; set; }
        /// <summary>
        /// hợp tác nông thôn thanh toán tiền mặt
        /// </summary>
        public decimal nhxjzf { get; set; }
    }
    public class S13OtherResponseDTO
    {
        /// <summary>
        /// Khác quỹ chung số tiền
        /// </summary>
        public decimal salvaQTCost { get; set; }
        /// <summary>
        /// hợp tác nông thôn tổng số tiền
        /// </summary>
        public decimal nhzje { get; set; }
        /// <summary>
        /// hợp tác nông thôn thanh toán tiền mặt
        /// </summary>
        public decimal nhxjzf { get; set; }
    }
    public class calcProgress
    {
        /// <summary>
        /// TínhTên
        /// </summary>
        public string calcName { get; set; }
        /// <summary>
        /// Tínhgiải thích
        /// </summary>
        public string calcMemo { get; set; }
        /// <summary>
        /// lần này tính
        /// </summary>
        public string calcBefore { get; set; }
        /// <summary>
        /// Kết quả
        /// </summary>
        public string calcAfter { get; set; }
    }
}