using System.Xml.Serialization;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01
{
    /// <summary>
    /// đẩy Thông tin bệnh nhân yêu cầu thể
    /// </summary>
    [XmlRoot("Request")]
    public class PushPatientRequestEntity : RequestBase
    {
        /// <summary>
        /// Thông tin bệnh nhân thể
        /// </summary>
        public Patient Patient { get; set; }
    }

    /// <summary>
    /// Thông tin bệnh nhân thể
    /// </summary>
    public class Patient
    {

        private string _id = "";

        /// <summary>
        ///Bệnh nhân Số hiệu(một Bệnh nhân tương ứng một Số hiệu,và và CMND/CCCD số mã một một tương ứng)
        /// bắt buộc
        /// </summary>
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name = "";
        /// <summary>
        /// Tên bệnh nhân
        /// bắt buộc
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _genderCode = "";
        /// <summary>
        /// Bệnh nhân Giới tính Mã
        /// bắt buộc
        /// </summary>
        public string genderCode
        {
            get { return _genderCode; }
            set { _genderCode = value; }
        }

        private string _gender = "";
        /// <summary>
        /// Bệnh nhân Giới tính
        /// bắt buộc
        /// </summary>
        public string gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private string _birthday = "";
        /// <summary>
        /// Ngày sinh YYYYMMDD
        /// bắt buộc
        /// </summary>
        public string birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        private string _cardTypeCode = "";
        /// <summary>
        /// chứng mục Mã loại
        /// bắt buộc
        /// </summary>
        public string cardTypeCode
        {
            get { return _cardTypeCode; }
            set { _cardTypeCode = value; }
        }

        private string _cardType = "";
        /// <summary>
        /// chứng mục Loại(Bình thường chỉ cư dân CMND/CCCD số)
        /// bắt buộc
        /// </summary>
        public string cardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }

        private string _cardNo = "";
        /// <summary>
        /// Số giấy tờ
        /// bắt buộc
        /// </summary>
        public string cardNo
        {
            get { return _cardNo; }
            set { _cardNo = value; }
        }

        private string _occupationCode = "";
        /// <summary>
        /// Mã nghề nghiệp
        /// </summary>
        public string occupationCode
        {
            get { return _occupationCode; }
            set { _occupationCode = value; }
        }

        private string _occupation = "";
        /// <summary>
        /// Nghề nghiệp
        /// </summary>
        public string occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }

        private string _marriedCode = "";
        /// <summary>
        /// Tình trạng hôn nhân Mã
        /// </summary>
        public string marriedCode
        {
            get { return _marriedCode; }
            set { _marriedCode = value; }
        }

        private string _married = "";
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string married
        {
            get { return _married; }
            set { _married = value; }
        }

        private string _countryCode = "";
        /// <summary>
        /// quốc tịch Mã
        /// </summary>
        public string countryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
        }

        private string _country = "";
        /// <summary>
        /// quốc tịch
        /// </summary>
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _nationalityCode = "";
        /// <summary>
        /// dân tộc Mã
        /// </summary>
        public string nationalityCode
        {
            get { return _nationalityCode; }
            set { _nationalityCode = value; }
        }

        private string _nationality = "";
        /// <summary>
        /// dân tộc
        /// </summary>
        public string nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }

        private string _provinceCode = "";
        /// <summary>
        /// nhà Tỉnh địa chỉ nhà Mã
        /// bắt buộc
        /// </summary>
        public string provinceCode
        {
            get { return _provinceCode; }
            set { _provinceCode = value; }
        }

        private string _cityCode = "";
        /// <summary>
        /// nhà Thành phố địa chỉ nhà cấp Mã
        /// bắt buộc
        /// </summary>
        public string cityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        private string _areaCode = "";
        /// <summary>
        /// nhà Địa chỉ nhà khu huyện Mã(bảo giữ và mây bình bàn của Phân vùng hành chính Mã nhất quán)
        /// bắt buộc
        /// </summary>
        public string areaCode
        {
            get { return _areaCode; }
            set { _areaCode = value; }
        }

        private string _province = "";
        /// <summary>
        /// nhà Tỉnh địa chỉ nhà
        /// </summary>
        public string province
        {
            get { return _province; }
            set { _province = value; }
        }

        private string _city = "";
        /// <summary>
        /// nhà Thành phố địa chỉ nhà cấp
        /// </summary>
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _area = "";
        /// <summary>
        /// nhà Địa chỉ nhà khu huyện
        /// </summary>
        public string area
        {
            get { return _area; }
            set { _area = value; }
        }

        private string _streetInfo = "";
        /// <summary>
        /// phố đường thông tin
        /// </summary>
        public string streetInfo
        {
            get { return _streetInfo; }
            set { _streetInfo = value; }
        }

        private string _postcode = "";
        /// <summary>
        /// bưu biên
        /// </summary>
        public string postcode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }

        private string _ctName = "";
        /// <summary>
        /// Người liên hệ Tên
        /// </summary>
        public string ctName
        {
            get { return _ctName; }
            set { _ctName = value; }
        }

        private string _ctRoleId = "";
        /// <summary>
        /// và Người liên hệ quan hệ
        /// </summary>
        public string ctRoleId
        {
            get { return _ctRoleId; }
            set { _ctRoleId = value; }
        }

        private string _ctAddr = "";
        /// <summary>
        /// Người liên hệ Địa chỉ
        /// </summary>
        public string ctAddr
        {
            get { return _ctAddr; }
            set { _ctAddr = value; }
        }

        private string _ctTelephone = "";
        /// <summary>
        /// Người liên hệ Điện thoại
        /// </summary>
        public string ctTelephone
        {
            get { return _ctTelephone; }
            set { _ctTelephone = value; }
        }

        private string _patiTypeCode = "";
        /// <summary>
        /// Bệnh nhân Mã loại
        /// </summary>
        public string patiTypeCode
        {
            get { return _patiTypeCode; }
            set { _patiTypeCode = value; }
        }

        private string _patiType = "";
        /// <summary>
        /// Bệnh nhân Loại
        /// </summary>
        public string patiType
        {
            get { return _patiType; }
            set { _patiType = value; }
        }

        private string _telephone = "";
        /// <summary>
        /// Bệnh nhân Điện thoại
        /// </summary>
        public string telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        private string _outpatientNo = "";
        /// <summary>
        /// Số khám ngoại trú, tức Khám bệnh dòng chảy số
        /// bắt buộc
        /// </summary>
        public string outpatientNo
        {
            get { return _outpatientNo; }
            set { _outpatientNo = value; }
        }

        private string _orgCode = "";
        /// <summary>
        /// Cơ sở Mã
        /// bắt buộc
        /// </summary>
        public string orgCode
        {
            get { return _orgCode; }
            set { _orgCode = value; }
        }
    }

    /// <summary>
    /// dòng chính Cơ sở cấu hình
    /// </summary>
    public class ConfigurationInfo
    {
        /// <summary>
        /// Xã/trấn
        /// </summary>
        public string xz { get; set; }

        /// <summary>
        /// dòng chính Mã
        /// </summary>
        public string xzbm { get; set; }

        /// <summary>
        /// Cơ sở Mã
        /// </summary>
        public string jgdm { get; set; }

        /// <summary>
        /// nhà Địa chỉ nhà khu huyện Mã(bảo giữ và mây bình bàn của Phân vùng hành chính Mã nhất quán)
        /// </summary>
        public string areaCode { get; set; }
    }
}