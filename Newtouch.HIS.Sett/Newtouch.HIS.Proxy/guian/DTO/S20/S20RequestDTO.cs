using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S20
{
    /// <summary>
    /// theo S18 số thứ tự bồi thư ờng ngoại trú trả về sau khi tải lênoutpid tiến hành ngoại trú thông tin sửa bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S20")]
    [XmlRoot("body")]
    public class S20RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng ngoại trú
        /// </summary>
        public string outpId { get; set; }

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
        /// Có hay khôngkhấu trừ gia đ ình tài khoản
        /// </summary>
        public string isAccountPay { get; set; }

        /// <summary>
        /// Là quỹ chung 
        /// </summary>
        public string isOutpPay { get; set; }

        /// <summary>
        /// Có hay không nâng cao (1 nâng cao 0 không nâng cao)
        /// </summary>
        public string isIncrease { get; set; }

        /// <summary>
        /// Có chuyển viện hay không(1 chuyển tuyến mặc đ ịnh0)
        /// </summary>
        public string isReferra { get; set; }

        /// <summary>
        /// Loại bệnh nhân(xem từ điển loại bệnh nhân)
        /// </summary>
        public string inpatientTypeOflocal { get; set; }

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