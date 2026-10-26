using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// nội trú thuốc thực hiện y lệnh
    /// </summary>
    [Table("zy_ypyzxx")]
    public class ZyYpyzxxEntity : IEntity<ZyYpyzxxEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID, do nhà thuốc/kho thuốc tự sinh, mỗi lần gọi thực hiện y lệnh thì sinh
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// y lệnh Id, tương ứng với Id y lệnh của trạm làm việc phối hợp y hộ
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// lĩnh thuốc số thứ tự
        /// </summary>
        public long lyxh { get; set; }

        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// số nội trú
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// nhóm lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// số lượng (đơn vị bộ phận)
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// hệ số chuyển đổi sl*zhyz=số lượng đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// uống, truyền tĩnh mạch...
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// thời gian thực hiện: 04,06,08...
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// thực hiện ngày
        /// </summary>
        public DateTime zxrq { get; set; }

        /// <summary>
        /// và thực hiện số lượng tương ứng
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// liều lượng
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// đơn vị liều lượng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// bác sĩ công số
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// ngày bắt đầu
        /// </summary>
        public DateTime? ksrq { get; set; }

        /// <summary>
        /// ngày kết thúc
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 1: tạm thời; 2: dài hạn
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// lời dặn
        /// </summary>
        public string yzbz { get; set; }

        /// <summary>
        /// tần suất
        /// </summary>
        public int zxsl { get; set; }

        /// <summary>
        /// đơn giá
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal dj { get; set; }

        /// <summary>
        /// số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// tỷ lệ tự chi trả
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// tính chất tự chi trả
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// mã khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// khu bệnh mã
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// giường
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// cờ phát thuốc  0: chưa phát; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// người đề nghị thực hiện y lệnh
        /// </summary>
        public string yzzxsqr { get; set; }

        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// số nhóm
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// thang số
        /// </summary>
        public decimal ts { get; set; }
        /// <summary>
        /// y lệnh loại Yzlx
        /// </summary>
        public int? yzlx { get; set; }

        public string zt { get; set; }

        /// <summary>
        /// mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// có tách lẻ không
        /// 1: có
        /// 2: không
        /// </summary>
        public int? sfcl { get; set; }
    }
}
