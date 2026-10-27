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
        /// Đơn thuốcLoại
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
        /// tạoNgười dùng
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa cuốiNgười dùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// dánsố
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// Đơn thuốc
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// làKhôngsắc hộ   1là  0Không
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Đồng bộCờ
        /// </summary>
        public bool? tbbz { get; set; }

        /// <summary>
        /// 临giườngnguyên nhân
        /// </summary>
        public string lcyx { get; set; }

        /// <summary>
        /// Đăng kýGhi chú
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// trảCờ
        /// </summary>
        public bool? tbz { get; set; }

        /// <summary>
        /// Số phiếu
        /// </summary>
        public string sqdh { get; set; }

        /// <summary>
        /// xatrìnhy tếTải lênTrạng thái
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
        /// Đơn thuốcTrạng thái
        /// </summary>
        public string cfzt { get; set; }

        /// <summary>
        /// Đơn thuốcchỉnhliềuY lệnhMô tả
        /// </summary>
        public string rxDrordDscr { get; set; }

        /// <summary>
        /// Đơn thuốcHiệu lựcSố ngày
        /// </summary>
        public int? valiDays { get; set; }

        /// <summary>
        /// lạidùng(nhiều lần)Sử dụngCờ
        /// </summary>
        public string reptFlag { get; set; }

        /// <summary>
        /// nhấtlớnSử dụng lầnsố
        /// </summary>
        public int? maxReptCnt { get; set; }

        /// <summary>
        /// Sử dụngnhấtnhỏgiữacách(Số ngày)
        /// </summary>
        public int? minInrvDays { get; set; }

        /// <summary>
        /// tiếpphươngCờ
        /// </summary>
        public string rxCotnFlag { get; set; }

        /// <summary>
        /// Dài hạnĐơn thuốcCờ
        /// </summary>
        public string longRxFlag { get; set; }

        /// <summary>
        /// Đơn thuốctruy xuấtmã
        /// </summary>
        public string rxTraceCode { get; set; }

        /// <summary>
        /// Bảo hiểm y tếĐơn thuốcSố hiệu
        /// </summary>
        public string hiRxno { get; set; }
    }
}
