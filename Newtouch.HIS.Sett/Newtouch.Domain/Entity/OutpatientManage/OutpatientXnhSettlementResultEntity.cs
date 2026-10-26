using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Kết quả quyết toán ngoại trú Bảo hiểm nông thôn mới
    /// </summary>
    [Table("mz_xnh_settResult")]
    public class OutpatientXnhSettlementResultEntity : IEntity<OutpatientXnhSettlementResultEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// Số thứ tự bồi thường
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// Tổng chi phí(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal totalCost { get; set; }

        /// <summary>
        /// Tổng chi phí(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal insuranceCost { get; set; }

        /// <summary>
        /// Đường khởi điểm(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal undulatingLine { get; set; }

        /// <summary>
        /// Bồi thường từ quỹ chung(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string generalRedeem { get; set; }

        /// <summary>
        /// Bồi thường tài khoản gia đình(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string accRedeem { get; set; }

        /// <summary>
        /// Chi phí bồi thường Bảo hiểm nông thôn mới(Độ chính xác 4 chữ số thập phân)
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
        /// Chi phí bồi thường lần hai đảm bảo mức tối thiểu
        /// </summary>
        public string bottomSecondRedeem { get; set; }

        /// <summary>
        /// Bồi thường danh mục chính xác(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string medicineCost { get; set; }

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
        /// Số tiền bồi thường bảo hiểm Đại Địa
        /// </summary>
        public string continentInsuranceCost { get; set; }

        /// <summary>
        /// Bồi thường nuôi dưỡng người đặc biệt khó khăn
        /// </summary>
        public string specialPovertyCost { get; set; }

        /// <summary>
        /// Bồi thường hỗ trợ y tế
        /// </summary>
        public string medicalAssistanceCost { get; set; }

        /// <summary>
        /// Bồi thường lần hai y tế hợp tác
        /// </summary>
        public string medicalSecondCost { get; set; }

        /// <summary>
        /// Bồi thường cứu trợ dân chính chính xác(Độ chính xác 4 chữ số thập phân)
        /// </summary>
        public string salvaMZCost { get; set; }

        /// <summary>
        /// Trạng thái quyết toán 1-Đã quyết toán 2-Đã trả lại   Mặc định đã quyết toán
        /// </summary>
        public int jszt { get; set; }

        /// <summary>
        /// Trạng thái 1-Hiệu lực  0-Không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}