using Newtouch.Infrastructure;
using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Thực hiện y lệnh thuốc nội trú
    /// </summary>
    [Table("zy_ypyzxx")]
    [SugarTable("zy_ypyzxx")]
    public class ZyYpyzxxEntity : IEntity<ZyYpyzxxEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID，Do nhà thuốc kho thuốc tự tạo，Tạo mỗi lần gọi thực hiện y lệnh
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Y lệnhId，Y lệnh trạm làm việc phối hợp y tế tương ứngId
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// Số thứ tự nhận thuốc
        /// </summary>
        public long lyxh { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Số lượng（Đơn vị bộ phận）
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Hệ số quy đổi sl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Uống、Truyền tĩnh mạch...
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// Thời gian thực hiện：04,06,08...
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// Ngày thực hiện
        /// </summary>
        public DateTime zxrq { get; set; }

        /// <summary>
        /// tương ứng với số lượng thực hiện
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// Liều dùng
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Số hiệu công tác bác sĩ
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime? ksrq { get; set; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// Nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 1：Tạm thời；2：Dài hạn
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// Dặn dò
        /// </summary>
        public string yzbz { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public int zxsl { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal dj { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Tỷ lệ tự trả
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// Tính chất tự trả
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Mã khu bệnh
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// Giường bệnh
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// Cờ phát thuốc  0：Chưa phát；1：Đã xếp；2：Đã phát；3：Đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// Người xin thực hiện y lệnh
        /// </summary>
        public string yzzxsqr { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// Số miếng dán
        /// </summary>
        public decimal ts { get; set; }
        /// <summary>
        /// Loại y lệnh Yzlx
        /// </summary>
        public int? yzlx { get; set; }

        public string zt { get; set; }

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1： Có
        /// 2： Không
        /// </summary>
        public int? sfcl { get; set; }
    }
}
