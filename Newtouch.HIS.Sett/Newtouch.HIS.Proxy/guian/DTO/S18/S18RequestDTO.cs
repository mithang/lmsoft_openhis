using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S18
{
    /// <summary>
    /// theo lấy của gia đ ình tham gia hợp tác danh sách tiến hành ngoại trú tải lên liên tỉnh đăng kýsửa cũng là này giao diện,sử dụnguploadType bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S18")]
    [XmlRoot("body")]
    public class S18RequestDTO
    {

        /// <summary>
        /// mã cá nhân 
        /// </summary>
        public string memberId { get; set; }

        /// <summary>
        /// Khám bệnhThời gian(yyyy-mm-dd)
        /// </summary>
        public string inpatientDate { get; set; }

        /// <summary>
        /// Khoa khám bệnh(xem từ điển khoa)
        /// </summary>
        public string inpatientDepartments { get; set; }

        /// <summary>
        /// Bác sĩ điều trị
        /// </summary>
        public string treatingPhysician { get; set; }

        /// <summary>
        /// Mã bệnh(Giao diệnS29Tải xuống và lấy)
        /// </summary>
        public string diseaseCode { get; set; }

        /// <summary>
        /// Chẩn đoán sơ bộ
        /// </summary>
        public string initialDiagnosis { get; set; }

        /// <summary>
        /// Có hay không nâng cao (1 nâng cao 0 không nâng cao)
        /// </summary>
        public string isIncrease { get; set; }

        /// <summary>
        /// Có chuyển viện hay không(1 chuyển tuyến mặc đ ịnh0)
        /// </summary>
        public string isInfusion { get; set; }

        /// <summary>
        /// Có hay khôngkhấu trừ gia đ ình tài khoản
        /// </summary>
        public string isAccountPay { get; set; }

        /// <summary>
        /// Có hay không quỹ chung 
        /// </summary>
        public string isOutpPay { get; set; }

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
        /// truyền dịch số lần 
        /// </summary>
        public string infusionCount { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// nhiều chẩn đoán nút 
        /// </summary>
        public string disList { get; set; }

        /// <summary>
        /// Mã bệnh(nhiều chẩn đoán)
        /// </summary>
        public string disCode { get; set; }

        /// <summary>
        /// Tải lênLoại 0Thêm mới 1Sửa
        /// </summary>
        public string uploadType { get; set; }

        /// <summary>
        /// HISsố khám ngoại trú(liên tỉnh)cần đảm bảo bản y tế cơ quan trong HISsố khám ngoại trú duy nhất 
        /// </summary>
        public string hisClinicNo { get; set; }

        /// <summary>
        /// Số hộ gia đ ình(liên tỉnh)
        /// </summary>
        public string familyNo { get; set; }

        /// <summary>
        /// chiều cao(liên tỉnh)
        /// </summary>
        public string stature { get; set; }

        /// <summary>
        /// cân nặng(liên tỉnh)
        /// </summary>
        public string weight { get; set; }

        /// <summary>
        /// Thứhai bệnh chẩn đoán(liên tỉnh)
        /// </summary>
        public string secondIcdNo { get; set; }

        /// <summary>
        /// Thứ ba bệnh chẩn đoán(liên tỉnh)
        /// </summary>
        public string threeIcdNo { get; set; }

        /// <summary>
        /// điều trị phư ơng thức mã (liên tỉnh)
        /// </summary>
        public string treatCode { get; set; }

        /// <summary>
        /// Loại khám bệnh(liên tỉnh)xem phụ lục bảng,nhóm tệp có tệp 
        /// </summary>
        public string cureCode { get; set; }

        /// <summary>
        /// đếnkhám bệnh trạng thái(liên tỉnh)xem phụ lục bảng 
        /// </summary>
        public string inHosId { get; set; }

        /// <summary>
        /// người xử lý y tế cơ quanđăng ký số thứ tự ID(liên tỉnh)
        /// </summary>
        public string registerID { get; set; }

        /// <summary>
        /// hợp tác nông thôn Đ ăng ký ngoại trú số thứ tự (liên tỉnh)Đ ăng ký ngoại trú sửa giờ truyền vào
        /// </summary>
        public string ClinicNo { get; set; }

        /// <summary>
        /// mã hành chính (liên tỉnh)
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// Có phải bệnh nhân khám chữa bệnh liên tỉnh không1:Là0:Không(liên tỉnh)
        /// </summary>
        public string isTransProvincial { get; set; }

        /// <summary>
        /// bệnh nặng chẩn đoán mã 
        /// </summary>
        public string majorDiseaseICD { get; set; }

        /// <summary>
        /// Bác sĩ cấp bậc 
        /// </summary>
        public string docatorRank { get; set; }

    }
}