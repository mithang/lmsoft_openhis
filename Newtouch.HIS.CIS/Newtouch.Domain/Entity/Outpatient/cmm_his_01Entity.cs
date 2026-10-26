using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Phòng khám đông y_Đẩy bệnh nhân
    /// </summary>
    [Table("cmm_his_01")]
    public class CmmHis01Entity : IEntity<CmmHis01Entity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Số bệnh nhân(Một bệnh nhân tương ứng với một số,và tương ứng một-một với số CMND),Giao diện tương ứngidTrường,Lưu cục bộblh
        /// </summary>
        public string cmmId { get; set; }

        /// <summary>
        /// Họ tên bệnh nhân
        /// Bắt buộc nhập
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Mã giới tính bệnh nhân
        /// Bắt buộc nhập
        /// </summary>
        public string genderCode { get; set; }

        /// <summary>
        /// Giới tính bệnh nhân
        /// Bắt buộc nhập
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Ngày sinh YYYYMMDD
        /// Bắt buộc nhập
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// Mã loại giấy tờ
        /// Bắt buộc nhập
        /// </summary>
        public string cardTypeCode { get; set; }

        /// <summary>
        /// Loại giấy tờ(Thường chỉ số CMND cư dân)
        /// Bắt buộc nhập
        /// </summary>
        public string cardType { get; set; }

        /// <summary>
        /// Số giấy tờ
        /// Bắt buộc nhập
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
        /// Mã tình trạng hôn nhân
        /// </summary>
        public string marriedCode { get; set; }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string married { get; set; }

        /// <summary>
        /// Mã quốc tịch
        /// </summary>
        public string countryCode { get; set; }

        /// <summary>
        /// Quốc tịch
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// Mã dân tộc
        /// </summary>
        public string nationalityCode { get; set; }

        /// <summary>
        /// Dân tộc
        /// </summary>
        public string nationality { get; set; }

        /// <summary>
        /// Mã tỉnh địa chỉ gia đình
        /// Bắt buộc nhập
        /// </summary>
        public string provinceCode { get; set; }

        /// <summary>
        /// Mã thành phố địa chỉ gia đình
        /// Bắt buộc nhập
        /// </summary>
        public string cityCode { get; set; }

        /// <summary>
        /// Mã quận/huyện địa chỉ gia đình(Giữ nhất quán với mã phân vùng hành chính của nền tảng đám mây)
        /// Bắt buộc nhập
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// Tỉnh địa chỉ gia đình
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// Thành phố địa chỉ gia đình
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// Quận/huyện địa chỉ gia đình
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// Thông tin đường phố
        /// </summary>
        public string streetInfo { get; set; }

        /// <summary>
        /// Mã bưu điện
        /// </summary>
        public string postcode { get; set; }

        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        public string ctName { get; set; }

        /// <summary>
        /// Quan hệ với người liên hệ
        /// </summary>
        public string ctRoleId { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ
        /// </summary>
        public string ctAddr { get; set; }

        /// <summary>
        /// Số điện thoại người liên hệ
        /// </summary>
        public string ctTelephone { get; set; }

        /// <summary>
        /// Mã loại bệnh nhân
        /// </summary>
        public string patiTypeCode { get; set; }

        /// <summary>
        /// Loại bệnh nhân
        /// </summary>
        public string patiType { get; set; }

        /// <summary>
        /// Số điện thoại bệnh nhân
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// Số ngoại trú, Tức là số thứ tự khám bệnh
        /// Bắt buộc nhập
        /// </summary>
        public string outpatientNo { get; set; }

        /// <summary>
        /// Mã cơ sở
        /// Bắt buộc nhập
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Ứng dụng gửi,Được chỉ định là:HIS
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// Ứng dụng nhận,Được chỉ định là:EMR,HEAL,PLAT
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// Thời gian gửi,Định dạng:yyyyMMddHHmmss
        /// </summary>
        public string sendTime { get; set; }

        /// <summary>
        /// Loại tin nhắn:TCM_HIS_01
        /// </summary>
        public string msgType { get; set; }

        /// <summary>
        /// Tin nhắn ID(Mã ứng dụng+Thời gian gửi ) , Ví dụ :HIS20170606020202
        /// </summary>
        public string msgID { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã trạng thái kết quả trả về  1: Thành công; 0:Thất bại
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// Mô tả kết quả trả về  Thành công;Thất bại(Mô tả bổ sung lý do thất bại)
        /// </summary>
        public string resultDesc { get; set; }

        /// <summary>
        /// Trạng thái     1:Có hiệu lực  0:Vô hiệu
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}