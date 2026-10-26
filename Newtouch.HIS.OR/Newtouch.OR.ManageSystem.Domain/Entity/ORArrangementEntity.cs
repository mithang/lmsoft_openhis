using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-11-06 10:22
    /// Mô tả：Hồ sơ xếp lịch phẫu thuật
    /// </summary>
    [Table("OR_Arrangement")]
    public class ORArrangementEntity : IEntity<ORArrangementEntity>
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Đăng ký phẫu thuậtID(ApplyInfoId)
        /// </summary>
        /// <returns></returns>
        public string ApplyId { get; set; }

        /// <summary>
        /// Mã đăng ký
        /// </summary>
        /// <returns></returns>
        public string Applyno { get; set; }

        /// <summary>
        /// Mã đăng ký phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssxh { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        /// <returns></returns>
        public string xm { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        /// <returns></returns>
        public string ks { get; set; }

        /// <summary>
        /// Khu bệnh
        /// </summary>
        /// <returns></returns>
        public string bq { get; set; }

        /// <summary>
        /// Số giường
        /// </summary>
        /// <returns></returns>
        public string ch { get; set; }

        /// <summary>
        /// Chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string zd { get; set; }

        /// <summary>
        /// Trạng thái đăng ký
        /// </summary>
        /// <returns></returns>
        public string sqzt { get; set; }

        /// <summary>
        /// Tên phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssmc { get; set; }

        /// <summary>
        /// Mã phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssdm { get; set; }

        /// <summary>
        /// Thời gian phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? sssj { get; set; }

        /// <summary>
        /// Mã bác sĩ phẫu thuật chính
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }

        /// <summary>
        /// Tên bác sĩ phẫu thuật chính
        /// </summary>
        /// <returns></returns>
        public string ysxm { get; set; }

        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        /// <returns></returns>
        public string AnesCode { get; set; }

        /// <summary>
        /// Phòng mổ
        /// </summary>
        /// <returns></returns>
        public string oproom { get; set; }

        /// <summary>
        /// Lượt mổ
        /// </summary>
        /// <returns></returns>
        public string oporder { get; set; }

        /// <summary>
        /// Bác sĩ phụ tá1
        /// </summary>
        /// <returns></returns>
        public string zlys1 { get; set; }

        /// <summary>
        /// Bác sĩ phụ tá2
        /// </summary>
        /// <returns></returns>
        public string zlys2 { get; set; }

        /// <summary>
        /// Bác sĩ phụ tá3
        /// </summary>
        /// <returns></returns>
        public string zlys3 { get; set; }

        /// <summary>
        /// Bác sĩ phụ tá4
        /// </summary>
        /// <returns></returns>
        public string zlys4 { get; set; }

        /// <summary>
        /// Y tá lưu động
        /// </summary>
        /// <returns></returns>
        public string xhhs { get; set; }

        /// <summary>
        /// Y tá rửa tay
        /// </summary>
        /// <returns></returns>
        public string xshs { get; set; }

        /// <summary>
        /// Vị trí phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssbw { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0：Không hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        
        /// <summary>
        /// Giới tính
        /// </summary>
        //public string xb { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        //public string nl { get; set; }

    }
}