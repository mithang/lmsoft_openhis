using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S06")]
    [XmlRoot("body")]
    public class S06RequestDTO
    {
       
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string inpatientNo { get; set; }
        /// <summary>
        /// Ngày nhập viện(yyyy-mm-dd)
        /// </summary>
        public string admissionDate { get; set; }
        /// <summary>
        /// Khoa nhập viện(xem từ điển khoa)
        /// </summary>
        public string admissionDepartments { get; set; }
        /// <summary>
        /// Bác sĩ điều trị
        /// </summary>
        public string treatingPhysician { get; set; }
        /// <summary>
        /// Trạng thái nhập viện(xem từ điển 入xuất viện trạng thái)
        /// </summary>
        public string admissionStatus { get; set; }
        /// <summary>
        /// Mã bệnh(Giao diệnS29Tải xuống và lấy)
        /// </summary>
        public string diseaseCode { get; set; }
        /// <summary>
        /// Chẩn đoán sơ bộ
        /// </summary>
        public string initialDiagnosis { get; set; }
        /// <summary>
        /// phẫu thuật mã(giao diệnS31 tải xuống và lấy)
        /// </summary>
        public string surgeryCode { get; set; }
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
        /// Giấy tờ đ ã đ ầy đ ủ chưa0 không 齐全1齐全
        /// </summary>
        public string isIncrease { get; set; }
        /// <summary>
        /// Có chuyển viện hay không1 chuyển tuyến mặc đ ịnh0
        /// </summary>
        public string isReferra { get; set; }
        /// <summary>
        /// Loại bệnh nhân(xem từ điển loại bệnh nhân)
        /// </summary>
        public string inpatientTypeOflocal { get; set; }
        /// <summary>
        /// bảo hiểm số tiền
        /// </summary>
        public string bxAccount { get; set; }
        /// <summary>
        /// Số điện thoại liên hệ
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// Số thẻ ngân hàng
        /// </summary>
        public string bankCardNo { get; set; }
        /// <summary>
        /// Họ của chủ tài khoản thẻ ngân hàng
        /// </summary>
        public string accountHolder { get; set; }
        /// <summary>
        /// Quan hệ giữa bệnh nhân và chủ tài khoản
        /// </summary>
        public string holderRelation { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }
    }
}