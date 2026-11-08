using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    [SugarTable("xt_dzcf")]
    public class DzcfEntity
    {

        /// <summary>
        /// Đơn thuốc id
        /// </summary>
        [Key]
        public string cfId { get; set; }

        /// <summary>
        /// Tổ chức id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khám bệnh id
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Cờ thu phí
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// Đơn thuốc Loại
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạo Người dùng
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa cuối Người dùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// dán số
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// Đơn thuốc
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// là Không sắc hộ   1là  0Không
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Đồng bộ Cờ
        /// </summary>
        public bool? tbbz { get; set; }

        /// <summary>
        /// Nguyên nhân lâm sàng
        /// </summary>
        public string lcyx { get; set; }

        /// <summary>
        /// Đăng ký Ghi chú
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// trả Cờ
        /// </summary>
        public bool? tbz { get; set; }

        /// <summary>
        /// Số phiếu
        /// </summary>
        public string sqdh { get; set; }

        /// <summary>
        /// xa trình y tế Tải lên Trạng thái
        /// </summary>
        public string SyncStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cftag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djts { get; set; }

        /// <summary>
        /// Đơn thuốc Trạng thái
        /// </summary>
        public string cfzt { get; set; }

        /// <summary>
        /// Đơn thuốc chỉnh liềuY lệnh Mô tả
        /// </summary>
        public string rxDrordDscr { get; set; }

        /// <summary>
        /// Đơn thuốc Hiệu lực Số ngày
        /// </summary>
        public int? valiDays { get; set; }

        /// <summary>
        /// lại dùng(nhiều lần)Sử dụng Cờ
        /// </summary>
        public string reptFlag { get; set; }

        /// <summary>
        /// nhất lớn Sử dụng lần số
        /// </summary>
        public int? maxReptCnt { get; set; }

        /// <summary>
        /// Sử dụng nhất nhỏ giữa cách(Số ngày)
        /// </summary>
        public int? minInrvDays { get; set; }

        /// <summary>
        /// tiếp phương Cờ
        /// </summary>
        public string rxCotnFlag { get; set; }

        /// <summary>
        /// Dài hạn Đơn thuốc Cờ
        /// </summary>
        public string longRxFlag { get; set; }

        /// <summary>
        /// Đơn thuốc truy xuất mã
        /// </summary>
        public string rxTraceCode { get; set; }

        /// <summary>
        /// Bảo hiểm y tế Đơn thuốc Số hiệu
        /// </summary>
        public string hiRxno { get; set; }
    }
}
