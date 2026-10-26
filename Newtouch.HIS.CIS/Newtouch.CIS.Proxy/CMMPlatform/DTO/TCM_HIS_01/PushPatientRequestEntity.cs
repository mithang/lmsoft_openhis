using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01
{
    /// <summary>
    /// Thân yêu cầu đẩy thông tin bệnh nhân
    /// </summary>
    [XmlRoot("Request")]
    public class PushPatientRequestEntity : RequestBase
    {
        /// <summary>
        /// Thân thông tin bệnh nhân
        /// </summary>
        public Patient Patient { get; set; }
    }

    /// <summary>
    /// Thân thông tin bệnh nhân
    /// </summary>
    public class Patient
    {

        private string _id = "";

        /// <summary>
        ///Mã bệnh nhân (một bệnh nhân tương ứng với một mã, và tương ứng một-một với số chứng minh nhân dân)
        /// Bắt buộc
        /// </summary>
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _name = "";
        /// <summary>
        /// Tên bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _genderCode = "";
        /// <summary>
        /// Mã giới tính bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string genderCode
        {
            get { return _genderCode; }
            set { _genderCode = value; }
        }

        private string _gender = "";
        /// <summary>
        /// Giới tính bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private string _birthday = "";
        /// <summary>
        /// Ngày sinh YYYYMMDD
        /// Bắt buộc
        /// </summary>
        public string birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        private string _cardTypeCode = "";
        /// <summary>
        /// Mã loại giấy tờ
        /// Bắt buộc
        /// </summary>
        public string cardTypeCode
        {
            get { return _cardTypeCode; }
            set { _cardTypeCode = value; }
        }

        private string _cardType = "";
        /// <summary>
        /// Loại giấy tờ (thường chỉ số chứng minh nhân dân)
        /// Bắt buộc
        /// </summary>
        public string cardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }

        private string _cardNo = "";
        /// <summary>
        /// Số giấy tờ
        /// Bắt buộc
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
        /// Mã tình trạng hôn nhân
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
        /// Mã quốc tịch
        /// </summary>
        public string countryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
        }

        private string _country = "";
        /// <summary>
        /// Quốc tịch
        /// </summary>
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _nationalityCode = "";
        /// <summary>
        /// Mã dân tộc
        /// </summary>
        public string nationalityCode
        {
            get { return _nationalityCode; }
            set { _nationalityCode = value; }
        }

        private string _nationality = "";
        /// <summary>
        /// Dân tộc
        /// </summary>
        public string nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }

        private string _provinceCode = "";
        /// <summary>
        /// Mã tỉnh của địa chỉ gia đình
        /// Bắt buộc
        /// </summary>
        public string provinceCode
        {
            get { return _provinceCode; }
            set { _provinceCode = value; }
        }

        private string _cityCode = "";
        /// <summary>
        /// Mã cấp thành phố của địa chỉ gia đình
        /// Bắt buộc
        /// </summary>
        public string cityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        private string _areaCode = "";
        /// <summary>
        /// Mã quận/huyện của địa chỉ gia đình (giữ thống nhất với mã hành chính của nền tảng đám mây)
        /// Bắt buộc
        /// </summary>
        public string areaCode
        {
            get { return _areaCode; }
            set { _areaCode = value; }
        }

        private string _province = "";
        /// <summary>
        /// Tỉnh của địa chỉ gia đình
        /// </summary>
        public string province
        {
            get { return _province; }
            set { _province = value; }
        }

        private string _city = "";
        /// <summary>
        /// Cấp thành phố của địa chỉ gia đình
        /// </summary>
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _area = "";
        /// <summary>
        /// Quận/huyện của địa chỉ gia đình
        /// </summary>
        public string area
        {
            get { return _area; }
            set { _area = value; }
        }

        private string _streetInfo = "";
        /// <summary>
        /// Thông tin đường phố
        /// </summary>
        public string streetInfo
        {
            get { return _streetInfo; }
            set { _streetInfo = value; }
        }

        private string _postcode = "";
        /// <summary>
        /// Mã bưu chính
        /// </summary>
        public string postcode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }

        private string _ctName = "";
        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        public string ctName
        {
            get { return _ctName; }
            set { _ctName = value; }
        }

        private string _ctRoleId = "";
        /// <summary>
        /// Quan hệ với người liên hệ
        /// </summary>
        public string ctRoleId
        {
            get { return _ctRoleId; }
            set { _ctRoleId = value; }
        }

        private string _ctAddr = "";
        /// <summary>
        /// Địa chỉ người liên hệ
        /// </summary>
        public string ctAddr
        {
            get { return _ctAddr; }
            set { _ctAddr = value; }
        }

        private string _ctTelephone = "";
        /// <summary>
        /// Điện thoại người liên hệ
        /// </summary>
        public string ctTelephone
        {
            get { return _ctTelephone; }
            set { _ctTelephone = value; }
        }

        private string _patiTypeCode = "";
        /// <summary>
        /// Mã loại bệnh nhân
        /// </summary>
        public string patiTypeCode
        {
            get { return _patiTypeCode; }
            set { _patiTypeCode = value; }
        }

        private string _patiType = "";
        /// <summary>
        /// Loại bệnh nhân
        /// </summary>
        public string patiType
        {
            get { return _patiType; }
            set { _patiType = value; }
        }

        private string _telephone = "";
        /// <summary>
        /// Điện thoại bệnh nhân
        /// </summary>
        public string telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        private string _outpatientNo = "";
        /// <summary>
        /// Số ngoại trú, tức số thứ tự khám bệnh
        /// Bắt buộc
        /// </summary>
        public string outpatientNo
        {
            get { return _outpatientNo; }
            set { _outpatientNo = value; }
        }

        private string _orgCode = "";
        /// <summary>
        /// Mã tổ chức
        /// Bắt buộc
        /// </summary>
        public string orgCode
        {
            get { return _orgCode; }
            set { _orgCode = value; }
        }
    }

    /// <summary>
    /// Cấu hình cơ quan hành chính
    /// </summary>
    public class ConfigurationInfo
    {
        /// <summary>
        /// Thị trấn
        /// </summary>
        public string xz { get; set; }

        /// <summary>
        /// Mã hành chính
        /// </summary>
        public string xzbm { get; set; }

        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public string jgdm { get; set; }

        /// <summary>
        /// Mã quận/huyện của địa chỉ gia đình (giữ thống nhất với mã hành chính của nền tảng đám mây)
        /// </summary>
        public string areaCode { get; set; }
    }
}