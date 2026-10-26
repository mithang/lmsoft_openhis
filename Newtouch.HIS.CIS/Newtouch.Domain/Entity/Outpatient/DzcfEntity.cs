using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    [Table("xt_dzcf")]
    public class DzcfEntity : IEntity<DzcfEntity>
    {

        /// <summary>
        /// Đơn thuốcid
        /// </summary>
        [Key]
        public string cfId { get; set; }

        /// <summary>
        /// Tổ chứcid
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khám bệnhid
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Cờ thu phí
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// Loại đơn thuốc
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
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Số thang
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// Đơn thuốc
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// Có sắc hộ hay không   1Có  0Không
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Cờ đồng bộ
        /// </summary>
        public bool? tbbz { get; set; }

        /// <summary>
        /// Lý do lâm sàng
        /// </summary>
        public string lcyx { get; set; }

        /// <summary>
        /// Ghi chú yêu cầu
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// Cờ trả lại
        /// </summary>
        public bool? tbz { get; set; }

        /// <summary>
        /// Số phiếu yêu cầu
        /// </summary>
        public string sqdh { get; set; }

        /// <summary>
        /// Trạng thái tải lên y tế từ xa
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
        /// Trạng thái đơn thuốc
        /// </summary>
        public string cfzt { get; set; }

        /// <summary>
        /// Giải thích y lệnh nguyên thang đơn thuốc
        /// </summary>
        public string rxDrordDscr { get; set; }

        /// <summary>
        /// Số ngày hiệu lực đơn thuốc
        /// </summary>
        public int? valiDays { get; set; }

        /// <summary>
        /// Tái sử dụng(Nhiều lần)Cờ sử dụng
        /// </summary>
        public string reptFlag { get; set; }

        /// <summary>
        /// Số lần sử dụng tối đa
        /// </summary>
        public int? maxReptCnt { get; set; }

        /// <summary>
        /// Khoảng cách tối thiểu khi sử dụng(Số ngày)
        /// </summary>
        public int? minInrvDays { get; set; }

        /// <summary>
        /// Cờ kê đơn tiếp
        /// </summary>
        public string rxCotnFlag { get; set; }

        /// <summary>
        /// Cờ đơn thuốc dài hạn
        /// </summary>
        public string longRxFlag { get; set; }

        /// <summary>
        /// Mã truy xuất đơn thuốc
        /// </summary>
        public string rxTraceCode { get; set; }

        /// <summary>
        /// Số đơn thuốc BHYT
        /// </summary>
        public string hiRxno { get; set; }
    }
}
