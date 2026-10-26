using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DTO.S24
{
    /// <summary>
    /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành ngoại trú mô phỏng quyết toán bản tin trả về
    /// </summary>
    public class S24ResponseDTO
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
        /// mức khởi điểm (đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal undulatingLine { get; set; }

        /// <summary>
        /// Bồi thư ờng quỹ chung(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string generalRedeem { get; set; }

        /// <summary>
        /// Bồi thư ờng tài khoản gia đ ình(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal accRedeem { get; set; }

        /// <summary>
        /// hợp tác nông thôn chi phí bồi thư ờng(đ ộ chính xác 4 chữ số thập phân)
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
        /// trợ giúp dân chính chính xácbồi thư ờng(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaMZCost { get; set; }

        /// <summary>
        /// Chi phí bồi thư ờng sàn(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string bottomRedeem { get; set; }

        /// <summary>
        /// Chi phí bồi thư ờng sàn lần hai
        /// </summary>
        public string bottomSecondRedeem { get; set; }

        /// <summary>
        /// Bồi thư ờng danh mục chính xác(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public string medicineCost { get; set; }

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
        /// Tínhtiến đ ộ 
        /// </summary>
        public List<calcProgress> list { get; set; }

        /// <summary>
        /// nuôi dư ỡng ngư ời đ ặc biệt khó khăn bồi thư ờng
        /// </summary>
        public string specialPovertyCost { get; set; }

        /// <summary>
        /// trợ giúp y tế bồi thư ờng
        /// </summary>
        public string medicalAssistanceCost { get; set; }

        /// <summary>
        /// hợp tác y tế lần hai lần bồi thư ờng
        /// </summary>
        public string medicalSecondCost { get; set; }
    }

    /// <summary>
    /// Tínhtiến đ ộ 
    /// </summary>
    public class calcProgress
    {
        /// <summary>
        ///   TínhTên
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
        ///   Kết quả
        /// </summary>
        public string calcAfter { get; set; }
    }
}