using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// trongyquán_thảo dượcđẩy_ghi chép
    /// </summary>
    [SugarTable("cmm_his_02_record")]
    public class CmmHis02RecordEntity
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// ThuốcSố hiệu
        /// bắt buộc
        /// </summary>
        public string cmmId { get; set; }

        /// <summary>
        /// Tên thuốc
        /// bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Mã thuốc
        /// bắt buộc
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string specification { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string manufacturer { get; set; }

        /// <summary>
        /// làKhôngKích hoạt
        /// </summary>
        public string activeFlg { get; set; }

        /// <summary>
        /// Cơ sởMã
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Quay lạikếtnếuTrạng tháimã  1: thành công; 0:thất bại
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// Quay lạikếtnếuMô tả  thành công;thất bại(Bổ sungthất bạinguyên nhânMô tả)
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
