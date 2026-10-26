using Newtouch.HIS.Proxy.Attribute;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    /// <summary>
    /// theo lấy của gia đ ình tham gia hợp tác danh sách tiến hành nhập viện làm thủ tục liên tỉnh sửa cũng là này giao diện uploadType nút điều khiển bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S04")]
    [XmlRoot("body")]
    public class S04RequestDTO
    {
        /// <summary>
        /// mã cá nhân 
        /// </summary>
        public string memberId { get; set; }
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
        /// <summary>
        /// bệnh nặng xin thứ tự số 
        /// </summary>
        public string bigDiseaseNo { get; set; }
        /// <summary>
        /// chuyển tuyến số 
        /// </summary>
        public string isReferraNo { get; set; }
        /// <summary>
        /// Tải lênLoại 0Thêm mới 1Sửa
        /// </summary>
        public string uploadType { get; set; }
        /// <summary>
        /// Số hộ gia đ ình(liên tỉnh)
        /// </summary>
        public string familySysno { get; set; }
        /// <summary>
        /// 身高(liên tỉnh)
        /// </summary>
        public string Stature { get; set; }
        /// <summary>
        /// 体重(liên tỉnh)
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// Thứ二 bệnh chẩn đoán(icd mã)(liên tỉnh)
        /// </summary>
        public string secondIcdNo { get; set; }
        /// <summary>
        /// Thứ ba bệnh chẩn đoán(icd mã)(liên tỉnh)
        /// </summary>
        public string threeIcdNo { get; set; }
        /// <summary>
        /// phẫu thuật số hiệu(liên tỉnh)
        /// </summary>
        public string opsId { get; set; }
        /// <summary>
        /// điều trị phư ơng thức mã (liên tỉnh)
        /// </summary>
        public string treatCode { get; set; }
        /// <summary>
        /// Loại khám bệnh(liên tỉnh)群 tệp có文档
        /// </summary>
        public string cureId { get; set; }
        /// <summary>
        /// và 发症(liên tỉnh) truyền tên, chỉ có thể truyền một 
        /// </summary>
        public string complication { get; set; }
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
        /// bệnh viện nhập việnthu phí số biên lai (liên tỉnh)
        /// </summary>
        public string ticketNo { get; set; }
        /// <summary>
        /// 民政 thông báo 书 số (liên tỉnh)
        /// </summary>
        public string ministerNotice { get; set; }
        /// <summary>
        /// 生育证 số (liên tỉnh)
        /// </summary>
        public string procreateNotice { get; set; }
        /// <summary>
        /// Có hay không trẻ sơ sinh nhập viện1:Là0:Không(liên tỉnh)
        /// </summary>
        public string isNewborn { get; set; }
        /// <summary>
        /// isNewborn=1 giờ không có thể là trống (liên tỉnh)
        /// </summary>
        public string newbornBirthday { get; set; }
        /// <summary>
        /// isNewborn=1 giờ không có thể là trống (liên tỉnh)
        /// </summary>
        public string newbornName { get; set; }
        /// <summary>
        /// isNewborn=1 giờ không có thể là trống 1:男2:女(liên tỉnh)
        /// </summary>
        public string newbornSex { get; set; }
        /// <summary>
        /// 经办 bệnh viện 登记 số thứ tự duy nhất ID(liên tỉnh)
        /// </summary>
        public string registerID { get; set; }
        /// <summary>
        /// Nhập viện登记 số thứ tự nhập viện登记sửa giờ truyền 入(liên tỉnh)
        /// </summary>
        public string inpatientSn { get; set; }
        /// <summary>
        /// mã hành chính (liên tỉnh)
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// Có phải bệnh nhân khám chữa bệnh liên tỉnh không1:Là0:Không(liên tỉnh)
        /// </summary>
        public string isTransProvincial { get; set; }
        /// <summary>
        /// bệnh nặng 主chẩn đoán nhóm mã 
        /// </summary>
        public string majorDiseaseICD { get; set; }
        /// <summary>
        /// bệnh nặng thứ二chẩn đoán nhóm mã 
        /// </summary>
        public string secondMajorDiseaseICD { get; set; }
        /// <summary>
        /// bệnh nặng thứ ba chẩn đoán nhóm mã 
        /// </summary>
        public string threeMajorDiseaseICD { get; set; }
        /// <summary>
        /// Thứ二 điều trị phư ơng thức mã,Tư ơng ứng thứ二chẩn đoán
        /// </summary>
        public string secondTreatCode { get; set; }
        /// <summary>
        /// Thứ ba điều trị phư ơng thức mã,Tư ơng ứng thứ ba chẩn đoán
        /// </summary>
        public string threeTreatCode { get; set; }
        /// <summary>
        /// nhiều chẩn đoán nút 
        /// </summary>
        public List<valueList> disList { get; set; }
    }

    public class valueList
    {
        /// <summary>
        /// Mã bệnh(nhiều chẩn đoán)
        /// </summary>
        public string disCode { get; set; }
    }
}