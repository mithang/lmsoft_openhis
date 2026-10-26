using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// ngoại trú đơn thuốc
    /// </summary>
    [Table("mz_cf")]
    public class MzCfEntity : IEntity<MzCfEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// mã nội bộ đơn thuốc
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// quyết toán trong mã
        /// </summary>
        public long jsnm { get; set; }

        /// <summary>
        /// số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// giới tính
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// tuổi
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// nhà thuốc lĩnh thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// thời gian thu phí
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// 0: chưa xếp; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// đơn thuốc số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 0: không hiệu lực 1: còn hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// ngày sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// số ngoại trú
        /// </summary>
        public string mzh = "";
}

    /// <summary>
    /// ngoại trú đơn thuốc số nhóm
    /// </summary>
    public class GetMzCfZt
    {
        public string zh { get; set; }
    }
}
