using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Đơn ngoại trú
    /// </summary>
    [Table("mz_cf")]
    [SugarTable("mz_cf")]
    public class MzCfEntity : IEntity<MzCfEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã nội bộ thanh toán
        /// </summary>
        public long jsnm { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Nhà thuốc nhận thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Thời gian thu phí
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// 0:Chưa xếp;1:Đã xếp;2:Đã phát;3:Đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số tiền đơn thuốc
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 0:Không hiệu lực 1:Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string mzh = "";
}

    /// <summary>
    /// Số nhóm đơn ngoại trú
    /// </summary>
    public class GetMzCfZt
    {
        public string zh { get; set; }
    }
}
