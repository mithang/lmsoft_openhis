using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// trongyquán_đẩyBệnh nhân
    /// </summary>
    [SugarTable("cmm_his_01")]
    public class CmmHis01Entity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Bệnh nhânSố hiệu（mộtBệnh nhântương ứngmộtSố hiệu，vàvàCMND/CCCDsốmãmộtmộttương ứng），tương ứnggiao diệnidchữđoạn，bảnđịaLưublh
        /// </summary>
        public string cmmId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Bệnh nhânGiới tínhMã
        /// bắt buộc
        /// </summary>
        public string genderCode { get; set; }

        /// <summary>
        /// Bệnh nhânGiới tính
        /// bắt buộc
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Ngày sinh YYYYMMDD
        /// bắt buộc
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// chứngmụcMã loại
        /// bắt buộc
        /// </summary>
        public string cardTypeCode { get; set; }

        /// <summary>
        /// chứngmụcLoại（Bình thườngchỉcư dânCMND/CCCDsố）
        /// bắt buộc
        /// </summary>
        public string cardType { get; set; }

        /// <summary>
        /// Số giấy tờ
        /// bắt buộc
        /// </summary>
        public string cardNo { get; set; }

        /// <summary>
        /// Mã nghề nghiệp
        /// </summary>
        public string occupationCode { get; set; }

        /// <summary>
        /// Nghề nghiệp
        /// </summary>
        public string occupation { get; set; }

        /// <summary>
        /// Tình trạng hôn nhânMã
        /// </summary>
        public string marriedCode { get; set; }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string married { get; set; }

        /// <summary>
        /// quốc tịchMã
        /// </summary>
        public string countryCode { get; set; }

        /// <summary>
        /// quốc tịch
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// dântộcMã
        /// </summary>
        public string nationalityCode { get; set; }

        /// <summary>
        /// dântộc
        /// </summary>
        public string nationality { get; set; }

        /// <summary>
        /// nhàTỉnh địa chỉ nhàMã
        /// bắt buộc
        /// </summary>
        public string provinceCode { get; set; }

        /// <summary>
        /// nhàThành phố địa chỉ nhàcấpMã
        /// bắt buộc
        /// </summary>
        public string cityCode { get; set; }

        /// <summary>
        /// nhàĐịa chỉ nhàkhuhuyệnMã（bảogiữvàmâybìnhbàncủaPhân vùng hành chínhMãnhất quán）
        /// bắt buộc
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// nhàTỉnh địa chỉ nhà
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// nhàThành phố địa chỉ nhàcấp
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// nhàĐịa chỉ nhàkhuhuyện
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// phốđườngthông tin
        /// </summary>
        public string streetInfo { get; set; }

        /// <summary>
        /// bưubiên
        /// </summary>
        public string postcode { get; set; }

        /// <summary>
        /// Người liên hệTên
        /// </summary>
        public string ctName { get; set; }

        /// <summary>
        /// vàNgười liên hệquan hệ
        /// </summary>
        public string ctRoleId { get; set; }

        /// <summary>
        /// Người liên hệĐịa chỉ
        /// </summary>
        public string ctAddr { get; set; }

        /// <summary>
        /// Người liên hệĐiện thoại
        /// </summary>
        public string ctTelephone { get; set; }

        /// <summary>
        /// Bệnh nhânMã loại
        /// </summary>
        public string patiTypeCode { get; set; }

        /// <summary>
        /// Bệnh nhânLoại
        /// </summary>
        public string patiType { get; set; }

        /// <summary>
        /// Bệnh nhânĐiện thoại
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// Số khám ngoại trú, tứcKhám bệnhdòngchảysố
        /// bắt buộc
        /// </summary>
        public string outpatientNo { get; set; }

        /// <summary>
        /// Cơ sởMã
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// gửiứngdùngtrìnhthứ tự，chỉđịnhlà：HIS
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// nhậnthuứngdùngtrìnhthứ tự，chỉđịnhlà：EMR,HEAL,PLAT
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// Thời gian gửi，định dạng：yyyyMMddHHmmss
        /// </summary>
        public string sendTime { get; set; }

        /// <summary>
        /// hủytinLoại：TCM_HIS_01
        /// </summary>
        public string msgType { get; set; }

        /// <summary>
        /// hủytin ID(ứngdùngtrìnhthứ tựMã+phát gửi khi giữa ) ， nếu ：HIS20170606020202
        /// </summary>
        public string msgID { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Quay lạikếtnếuTrạng tháimã  1： thành công； 0：thất bại
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// Quay lạikếtnếuMô tả  thành công；thất bại(Bổ sungthất bạinguyên nhânMô tả)
        /// </summary>
        public string resultDesc { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0：Vô hiệu
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạongười
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửangười
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
