using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Tạo：Lý Hâm
    /// Ngày tháng:2019-07-19 14:00
    /// Mô tả:Quyết toán nội trú-Chi phí BHYT-Quý An
    /// </summary>
    [Table("zy_js_gaxnhjyfy")]
    public class HospSettlementGAXNHFeeEntity : IEntity<HospSettlementGAXNHFeeEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        public string inpId { get; set; }
        
        /// <summary>
        /// Liên kếtzy_js jsnm
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// Tổng chi phí(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? totalCost { get; set; }
        /// <summary>
        /// Chi phí trong bảo hiểm(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? insuranceCost { get; set; }
        /// <summary>
        /// Đường khởi điểm(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? undulatingLine { get; set; }
        /// <summary>
        /// Bồi thường từ quỹ chung(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? generalRedeem { get; set; }
        /// <summary>
        /// Bồi thường tổn thương bất ngờ(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? accidentRedeem { get; set; }
        /// <summary>
        /// Bồi thường tài khoản gia đình(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal? accRedeem { get; set; }
        /// <summary>
        /// Chi phí bồi thường(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string compensateCost { get; set; }
        /// <summary>
        /// Chi phí cứu trợ dân chính(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string civilCost { get; set; }
        /// <summary>
        /// Bồi thường bảo hiểm thương mại bệnh nặng(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string insureCost { get; set; }
        /// <summary>
        /// Chi phí cứu trợ kế hoạch hóa gia đình(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaJSCost { get; set; }
        /// <summary>
        /// Chi phí bồi thường đảm bảo mức tối thiểu(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string bottomRedeem { get; set; }
        /// <summary>
        /// Chi phí bồi thường lần hai đảm bảo mức tối thiểu(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string bottomSecondRedeem { get; set; }
        /// <summary>
        /// Bồi thường danh mục chính xác(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string medicineCost { get; set; }
        /// <summary>
        /// Bồi thường trợ cấp đi lại(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string trafficCost { get; set; }
        /// <summary>
        /// Bồi thường trợ cấp sinh hoạt(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string liveCost { get; set; }
        /// <summary>
        /// Bồi thường trợ cấp y tế(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string medicalCost { get; set; }
        /// <summary>
        /// Bồi thường ưu đãi chính xác mới(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaYFCost { get; set; }
        /// <summary>
        /// Bồi thường Hội người khuyết tật chính xác mới(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaCLCost { get; set; }
        /// <summary>
        /// Bồi thường giảm nghèo chính xác mới(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaFPCost { get; set; }
        /// <summary>
        /// Bồi thường kiểm soát dịch bệnh chính xác mới(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaJKCost { get; set; }
        /// <summary>
        /// Mã thành viên
        /// </summary>
        public string memberNo { get; set; }
        /// <summary>
        /// Tên thành viên
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Thẻ y tế,Số thẻ
        /// </summary>
        public string bookNo { get; set; }
        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string sexName { get; set; }
        /// <summary>
        /// Ngày tháng năm sinh
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// Tên chủ hộ
        /// </summary>
        public string masterName { get; set; }
        /// <summary>
        /// Tên quan hệ với chủ hộ
        /// </summary>
        public string relationName { get; set; }
        /// <summary>
        /// Tên thuộc tính thân phận cá nhân
        /// </summary>
        public string identityName { get; set; }
        /// <summary>
        /// Số CMND/CCCD
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// Số lần đã bồi thường nội trú thành viên trong năm hiện tại
        /// </summary>
        public string currYearRedeemCount { get; set; }
        /// <summary>
        /// Tổng chi phí y tế đã bồi thường nội trú thành viên trong năm hiện tại(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string currYearTotal { get; set; }
        /// <summary>
        /// Tổng chi phí trong bảo hiểm đã bồi thường nội trú thành viên trong năm hiện tại(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string currYearEnableMoney { get; set; }
        /// <summary>
        /// Số tiền đã bồi thường nội trú thành viên trong năm hiện tại(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string currYearReddemMoney { get; set; }
        /// <summary>
        /// Mã gia đình thành viên
        /// </summary>
        public string familyNo { get; set; }
        /// <summary>
        /// Địa chỉ gia đình thành viên
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Tên thuộc tính tham gia hợp tác
        /// </summary>
        public string joinPropName { get; set; }
        /// <summary>
        /// Số lần đã bồi thường nội trú gia đình trong năm hiện tại
        /// </summary>
        public string currFamilyRedeemCount { get; set; }
        /// <summary>
        /// Tổng chi phí y tế đã bồi thường nội trú gia đình trong năm hiện tại(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string currFamilyTotal { get; set; }
        /// <summary>
        /// Chi phí trong bảo hiểm đã bồi thường nội trú gia đình trong năm hiện tại(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string currFamilyEnableMoney { get; set; }
        /// <summary>
        /// Số tiền đã bồi thường nội trú gia đình trong năm hiện tại(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string currFamilyReddemMoney { get; set; }
        /// <summary>
        /// Tổng chi phí y tế nội trú lần này(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string totalCosts { get; set; }
        /// <summary>
        /// Chi phí trong bảo hiểm nội trú lần này(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string enableMoney { get; set; }
        /// <summary>
        /// Chi phí thuốc cơ bản quốc định trong chi phí nội trú lần này(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string essentialMedicineMoney { get; set; }
        /// <summary>
        /// Chi phí thuốc cơ bản bổ sung cấp tỉnh trong chi phí nội trú lần này(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string provinceMedicineMoney { get; set; }
        /// <summary>
        /// Số tiền bồi thường nội trú lần này trừ mức khởi điểm(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string startMoney { get; set; }
        /// <summary>
        /// Số tiền bồi thường nội trú lần này(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string calculateMoney { get; set; }
        /// <summary>
        /// Tên loại bồi thường
        /// </summary>
        public string redeemTypeName { get; set; }
        /// <summary>
        /// Có phải là bồi thường bệnh đơn lẻ hay không
        /// </summary>
        public string isSpecial { get; set; }
        /// <summary>
        /// Có thực hiện bồi thường đảm bảo mức tối thiểu hay không
        /// </summary>
        public string isPaul { get; set; }
        /// <summary>
        /// Số tiền truy bổ,Phần tăng mức bồi thường thuốc Trung y và thuốc cơ bản quốc định(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string anlagernMoney { get; set; }
        /// <summary>
        /// Định mức chi phí bệnh đơn lẻ(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string fundPayMoney { get; set; }
        /// <summary>
        /// Chi phí cơ sở y tế chịu(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string hospAssumeMoney { get; set; }
        /// <summary>
        /// Chi phí cá nhân tự trả(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string personalPayMoney { get; set; }
        /// <summary>
        /// Trợ cấp y tế ưu đãi dân chính
        /// </summary>
        public string YFmedicalAid { get; set; }
        /// <summary>
        /// Cứu trợ y tế thành thị và nông thôn dân chính
        /// </summary>
        public string CXmedicalAid { get; set; }
        /// <summary>
        /// Chi phí vượt giá giới hạn vật tư giá cao(Đơn vị: đồng,Giữ lại hai chữ số sau dấu thập phân)
        /// </summary>
        public string materialMoney { get; set; }
        /// <summary>
        /// Danh sách thông tin bồi thường theo đoạn
        /// </summary>
        public string gradeList { get; set; }
        /// <summary>
        /// Phương pháp tính toán quyết toán lần này
        /// </summary>
        public string calculationMethod { get; set; }
        /// <summary>
        /// Số tiền Hội từ thiện thanh toán
        /// </summary>
        public string ChinaCharityPay { get; set; }
        /// <summary>
        /// Có phải là trả phí cố định chu kỳ dài hay không
        /// </summary>
        public string isLongPeriod { get; set; }
        /// <summary>
        /// Có vào bảo hiểm bệnh nặng hay không
        /// </summary>
        public string isCII { get; set; }
        /// <summary>
        /// Chi phí hợp lệ bảo hiểm bệnh nặng
        /// </summary>
        public string CIIEligibleCosts { get; set; }
        /// <summary>
        /// Mức khởi điểm bảo hiểm bệnh nặng lần này
        /// </summary>
        public string CIIStartMoney { get; set; }
        /// <summary>
        /// Số tiền bồi thường bảo hiểm bệnh nặng lần này
        /// </summary>
        public string CIICalculateMoney { get; set; }
        /// <summary>
        /// Lũy kế số tiền bồi thường bảo hiểm bệnh nặng
        /// </summary>
        public string CIICumulativePay { get; set; }
        /// <summary>
        /// Lũy kế số tiền bảo hiểm bệnh nặng trừ mức khởi điểm
        /// </summary>
        public string CIICumulativeStart { get; set; }
        /// <summary>
        /// Lũy kế chi phí hợp lệ vào bảo hiểm bệnh nặng
        /// </summary>
        public string CIICumulativeEligible { get; set; }
        /// <summary>
        /// Số tiền giảm miễn chi phí hai hộ kế hoạch hóa gia đình
        /// </summary>
        public string FamilyPlanningWaiver { get; set; }
        /// <summary>
        /// Bồi thường khác
        /// </summary>
        public string OtherPay { get; set; }
        /// <summary>
        /// Thanh toán từ tài khoản gia đình
        /// </summary>
        public string familyAccount { get; set; }
        /// <summary>
        /// Số tiền bồi thường bảo hiểm Đại Địa
        /// </summary>
        public string continentInsuranceCost { get; set; }
        /// <summary>
        /// Trợ cấp điều dưỡng
        /// </summary>
        public string nurseCost { get; set; }
        /// <summary>
        /// Số tiền bồi thường khoán giường bệnh
        /// </summary>
        public string cwAccountCost { get; set; }
        /// <summary>
        /// Bồi thường nuôi dưỡng người đặc biệt khó khăn
        /// </summary>
        public string specialPovertyCost { get; set; }
        /// <summary>
        /// Bồi thường hỗ trợ y tế
        /// </summary>
        public string medicalAssistanceCost { get; set; }
        /// <summary>
        /// Bồi thường cứu trợ dân chính chính xác(Số thập phân chính xác 4 chữ số)
        /// </summary>
        public string salvaMZCost { get; set; }
        /// <summary>
        /// Số tiền quỹ chung khác
        /// </summary>
        public decimal? salvaQTCost { get; set; }
        /// <summary>
        /// Tổng số tiền Bảo hiểm nông thôn mới
        /// </summary>
        public decimal? nhzje { get; set; }
        /// <summary>
        /// Thanh toán tiền mặt Bảo hiểm nông thôn mới
        /// </summary>
        public decimal? nhxjzf { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

    }
}