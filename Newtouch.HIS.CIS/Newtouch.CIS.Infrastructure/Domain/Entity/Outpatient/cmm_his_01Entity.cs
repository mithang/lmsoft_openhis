using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// trong y quán_đẩy Bệnh nhân
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
        /// Bệnh nhân Số hiệu(một Bệnh nhân tương ứng một Số hiệu,và và CMND/CCCD số mã một một tương ứng),tương ứng giao diện id chữ đoạn,bản địa Lưu blh
        /// </summary>
        public string cmmId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Bệnh nhân Giới tính Mã
        /// bắt buộc
        /// </summary>
        public string genderCode { get; set; }

        /// <summary>
        /// Bệnh nhân Giới tính
        /// bắt buộc
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Ngày sinh YYYYMMDD
        /// bắt buộc
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// chứng mục Mã loại
        /// bắt buộc
        /// </summary>
        public string cardTypeCode { get; set; }

        /// <summary>
        /// chứng mục Loại(Bình thường chỉ cư dân CMND/CCCD số)
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
        /// Tình trạng hôn nhân Mã
        /// </summary>
        public string marriedCode { get; set; }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string married { get; set; }

        /// <summary>
        /// quốc tịch Mã
        /// </summary>
        public string countryCode { get; set; }

        /// <summary>
        /// quốc tịch
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// dân tộc Mã
        /// </summary>
        public string nationalityCode { get; set; }

        /// <summary>
        /// dân tộc
        /// </summary>
        public string nationality { get; set; }

        /// <summary>
        /// nhà Tỉnh địa chỉ nhà Mã
        /// bắt buộc
        /// </summary>
        public string provinceCode { get; set; }

        /// <summary>
        /// nhà Thành phố địa chỉ nhà cấp Mã
        /// bắt buộc
        /// </summary>
        public string cityCode { get; set; }

        /// <summary>
        /// nhà Địa chỉ nhà khu huyện Mã(bảo giữ và mây bình bàn của Phân vùng hành chính Mã nhất quán)
        /// bắt buộc
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// nhà Tỉnh địa chỉ nhà
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// nhà Thành phố địa chỉ nhà cấp
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// nhà Địa chỉ nhà khu huyện
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// phố đường thông tin
        /// </summary>
        public string streetInfo { get; set; }

        /// <summary>
        /// bưu biên
        /// </summary>
        public string postcode { get; set; }

        /// <summary>
        /// Người liên hệ Tên
        /// </summary>
        public string ctName { get; set; }

        /// <summary>
        /// và Người liên hệ quan hệ
        /// </summary>
        public string ctRoleId { get; set; }

        /// <summary>
        /// Người liên hệ Địa chỉ
        /// </summary>
        public string ctAddr { get; set; }

        /// <summary>
        /// Người liên hệ Điện thoại
        /// </summary>
        public string ctTelephone { get; set; }

        /// <summary>
        /// Bệnh nhân Mã loại
        /// </summary>
        public string patiTypeCode { get; set; }

        /// <summary>
        /// Bệnh nhân Loại
        /// </summary>
        public string patiType { get; set; }

        /// <summary>
        /// Bệnh nhân Điện thoại
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// Số khám ngoại trú, tức Khám bệnh dòng chảy số
        /// bắt buộc
        /// </summary>
        public string outpatientNo { get; set; }

        /// <summary>
        /// Cơ sở Mã
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// gửi ứng dùng trình thứ tự,chỉ định là:HIS
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// nhận thu ứng dùng trình thứ tự,chỉ định là:EMR,HEAL,PLAT
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// Thời gian gửi,định dạng:yyyyMMddHHmmss
        /// </summary>
        public string sendTime { get; set; }

        /// <summary>
        /// hủy tin Loại:TCM_HIS_01
        /// </summary>
        public string msgType { get; set; }

        /// <summary>
        /// hủy tin ID(ứng dùng trình thứ tự Mã+phát gửi khi giữa ) , nếu :HIS20170606020202
        /// </summary>
        public string msgID { get; set; }

        /// <summary>
        /// Tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Quay lại kết nếu Trạng thái mã  1: thành công; 0:thất bại
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// Quay lại kết nếu Mô tả  thành công;thất bại(Bổ sung thất bại nguyên nhân Mô tả)
        /// </summary>
        public string resultDesc { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0:Vô hiệu
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạo người
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa người
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
