using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S07")]
    [XmlRoot("body")]
    public class S07RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// Ngày xuất viện(yyyy-mm-dd)
        /// </summary>
        public string dischargeDate { get; set; }
        /// <summary>
        /// Khoa xuất viện(xem từ điển khoa)
        /// </summary>
        public string dischargeDepartments { get; set; }
        /// <summary>
        /// Xuất viện trạng thái(xem từ điển 入xuất viện trạng thái)
        /// </summary>
        public string dischargeStatus { get; set; }
        /// <summary>
        /// hợp tác nông thôn bệnh xuất viện chẩn đoán(icd mã)(liên tỉnh)
        /// </summary>
        public string icdAllNo { get; set; }
        /// <summary>
        /// Thứ二 bệnh chẩn đoán(icd mã)(liên tỉnh)
        /// </summary>
        public string secondIcdNo { get; set; }
        /// <summary>
        /// Thứ ba bệnh chẩn đoán(icd mã)(liên tỉnh)
        /// </summary>
        public string threeIcdNo { get; set; }
        /// <summary>
        /// Thứ bốn bệnh chẩn đoán(icd mã)(liên tỉnh)
        /// </summary>
        public string fourNo { get; set; }
        /// <summary>
        /// Thứ năm bệnh chẩn đoán(icd mã)(liên tỉnh)
        /// </summary>
        public string fiveNo { get; set; }
        /// <summary>
        /// điều trị phư ơng thức mã (liên tỉnh)
        /// </summary>
        public string treatCode { get; set; }
        /// <summary>
        /// Thứ二 điều trị phư ơng thức mã (liên tỉnh)
        /// </summary>
        public string secondTreatCode { get; set; }
        /// <summary>
        /// Thứ ba điều trị phư ơng thức mã (liên tỉnh)
        /// </summary>
        public string threeTreatCode { get; set; }
        /// <summary>
        /// Thứ bốn điều trị phư ơng thức mã (liên tỉnh)
        /// </summary>
        public string fourTreatCode { get; set; }
        /// <summary>
        /// Thứ năm điều trị phư ơng thức mã (liên tỉnh)
        /// </summary>
        public string fiveTreatCode { get; set; }
        /// <summary>
        /// chuyển tuyến loại (0 bình thư ờng nhập viện 1 huyện ngoài khám chữa bệnh chuyển tuyến 2 chuyển viện)(liên tỉnh)
        /// </summary>
        public string turnMode { get; set; }
        /// <summary>
        /// chuyển tuyến chuyển viện mã (huyện ngoài khám chữa bệnh chuyển tuyến - này giá trị là chuyển tuyến xin đ ơn số; chuyển viện - này giá trị là chuyển viện nhập viện登记 số)(liên tỉnh)
        /// </summary>
        public string turnCode { get; set; }
        /// <summary>
        /// chuyển viện ngày (yyyy-mm-dd hh:mm:ss)(liên tỉnh)
        /// </summary>
        public string turnDate { get; set; }
        /// <summary>
        /// HIShệ thống trong bệnh nhân tổng chi phí(liên tỉnh)
        /// </summary>
        public string hisTotal { get; set; }
        /// <summary>
        /// mã hành chính (liên tỉnh)
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// Có phải bệnh nhân khám chữa bệnh liên tỉnh không1:Là0:Không(liên tỉnh)
        /// </summary>
        public string isTransProvincial { get; set; }
        /// <summary>
        /// bệnh nặng 主chẩn đoán mã 
        /// </summary>
        public string majorDiseaseICD { get; set; }
        /// <summary>
        /// bệnh nặng thứ二chẩn đoán mã 
        /// </summary>
        public string secondMajorDiseaseICD { get; set; }
        /// <summary>
        /// bệnh nặng thứ ba chẩn đoán mã 
        /// </summary>
        public string threeMajorDiseaseICD { get; set; }
        /// <summary>
        /// bệnh nặng thứ bốn chẩn đoán mã 
        /// </summary>
        public string fourMajorDiseaseICD { get; set; }
        /// <summary>
        /// bệnh nặng thứ năm chẩn đoán mã 
        /// </summary>
        public string fiveMajorDiseaseICD { get; set; }
    }
}