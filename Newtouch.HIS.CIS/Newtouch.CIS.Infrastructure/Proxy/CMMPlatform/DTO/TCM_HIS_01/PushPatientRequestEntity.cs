using System.Xml.Serialization;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01
{
    /// <summary>
    /// đẩyThông tin bệnh nhânyêu cầuthể
    /// </summary>
    [XmlRoot("Request")]
    public class PushPatientRequestEntity : RequestBase
    {
        /// <summary>
        /// Thông tin bệnh nhânthể
        /// </summary>
        public Patient Patient { get; set; }
    }

    /// <summary>
    /// Thông tin bệnh nhânthể
    /// </summary>
    public class Patient
    {

        private string _id = "";

        /// <summary>
        ///Bệnh nhânSố hiệu（mộtBệnh nhântương ứngmộtSố hiệu，vàvàCMND/CCCDsốmãmộtmộttương ứng）
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
        /// Bệnh nhânGiới tínhMã
        /// bắt buộc
        /// </summary>
        public string genderCode
        {
            get { return _genderCode; }
            set { _genderCode = value; }
        }

        private string _gender = "";
        /// <summary>
        /// Bệnh nhânGiới tính
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
        /// chứngmụcMã loại
        /// bắt buộc
        /// </summary>
        public string cardTypeCode
        {
            get { return _cardTypeCode; }
            set { _cardTypeCode = value; }
        }

        private string _cardType = "";
        /// <summary>
        /// chứngmụcLoại（Bình thườngchỉcư dânCMND/CCCDsố）
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
        /// chức业Mã
        /// </summary>
        public string occupationCode
        {
            get { return _occupationCode; }
            set { _occupationCode = value; }
        }

        private string _occupation = "";
        /// <summary>
        /// chức业
        /// </summary>
        public string occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }

        private string _marriedCode = "";
        /// <summary>
        /// Tình trạng hôn nhânMã
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
        /// quốc tịchMã
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
        /// dântộcMã
        /// </summary>
        public string nationalityCode
        {
            get { return _nationalityCode; }
            set { _nationalityCode = value; }
        }

        private string _nationality = "";
        /// <summary>
        /// dântộc
        /// </summary>
        public string nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }

        private string _provinceCode = "";
        /// <summary>
        /// nhàTỉnh địa chỉ nhàMã
        /// bắt buộc
        /// </summary>
        public string provinceCode
        {
            get { return _provinceCode; }
            set { _provinceCode = value; }
        }

        private string _cityCode = "";
        /// <summary>
        /// nhàThành phố địa chỉ nhàcấpMã
        /// bắt buộc
        /// </summary>
        public string cityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        private string _areaCode = "";
        /// <summary>
        /// nhàĐịa chỉ nhàkhuhuyệnMã（bảogiữvàmâybìnhbàncủaPhân vùng hành chínhMãnhất quán）
        /// bắt buộc
        /// </summary>
        public string areaCode
        {
            get { return _areaCode; }
            set { _areaCode = value; }
        }

        private string _province = "";
        /// <summary>
        /// nhàTỉnh địa chỉ nhà
        /// </summary>
        public string province
        {
            get { return _province; }
            set { _province = value; }
        }

        private string _city = "";
        /// <summary>
        /// nhàThành phố địa chỉ nhàcấp
        /// </summary>
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _area = "";
        /// <summary>
        /// nhàĐịa chỉ nhàkhuhuyện
        /// </summary>
        public string area
        {
            get { return _area; }
            set { _area = value; }
        }

        private string _streetInfo = "";
        /// <summary>
        /// phốđườngthông tin
        /// </summary>
        public string streetInfo
        {
            get { return _streetInfo; }
            set { _streetInfo = value; }
        }

        private string _postcode = "";
        /// <summary>
        /// bưubiên
        /// </summary>
        public string postcode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }

        private string _ctName = "";
        /// <summary>
        /// Người liên hệTên
        /// </summary>
        public string ctName
        {
            get { return _ctName; }
            set { _ctName = value; }
        }

        private string _ctRoleId = "";
        /// <summary>
        /// vàNgười liên hệquan hệ
        /// </summary>
        public string ctRoleId
        {
            get { return _ctRoleId; }
            set { _ctRoleId = value; }
        }

        private string _ctAddr = "";
        /// <summary>
        /// Người liên hệĐịa chỉ
        /// </summary>
        public string ctAddr
        {
            get { return _ctAddr; }
            set { _ctAddr = value; }
        }

        private string _ctTelephone = "";
        /// <summary>
        /// Người liên hệĐiện thoại
        /// </summary>
        public string ctTelephone
        {
            get { return _ctTelephone; }
            set { _ctTelephone = value; }
        }

        private string _patiTypeCode = "";
        /// <summary>
        /// Bệnh nhânMã loại
        /// </summary>
        public string patiTypeCode
        {
            get { return _patiTypeCode; }
            set { _patiTypeCode = value; }
        }

        private string _patiType = "";
        /// <summary>
        /// Bệnh nhânLoại
        /// </summary>
        public string patiType
        {
            get { return _patiType; }
            set { _patiType = value; }
        }

        private string _telephone = "";
        /// <summary>
        /// Bệnh nhânĐiện thoại
        /// </summary>
        public string telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        private string _outpatientNo = "";
        /// <summary>
        /// Số khám ngoại trú, tứcKhám bệnhdòngchảysố
        /// bắt buộc
        /// </summary>
        public string outpatientNo
        {
            get { return _outpatientNo; }
            set { _outpatientNo = value; }
        }

        private string _orgCode = "";
        /// <summary>
        /// Cơ sởMã
        /// bắt buộc
        /// </summary>
        public string orgCode
        {
            get { return _orgCode; }
            set { _orgCode = value; }
        }
    }

    /// <summary>
    /// dòngchínhCơ sởcấu hình
    /// </summary>
    public class ConfigurationInfo
    {
        /// <summary>
        /// 乡trấn
        /// </summary>
        public string xz { get; set; }

        /// <summary>
        /// dòngchínhMã
        /// </summary>
        public string xzbm { get; set; }

        /// <summary>
        /// Cơ sởMã
        /// </summary>
        public string jgdm { get; set; }

        /// <summary>
        /// nhàĐịa chỉ nhàkhuhuyệnMã（bảogiữvàmâybìnhbàncủaPhân vùng hành chínhMãnhất quán）
        /// </summary>
        public string areaCode { get; set; }
    }
}