using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Đơn thuốc ngoại trú
    /// </summary>
    [Table("mz_cf")]
    public class OutpatientPrescriptionEntity : IEntity<OutpatientPrescriptionEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cfnm { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// Mã nội bộ bệnh nhân của lần đăng ký khám này－Trường dư thừa để nâng cao hiệu năng
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân của lần đăng ký khám này－Trường dư thừa để nâng cao hiệu năng
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 1:Khám ngoại trú 2:Cấp cứu
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 0 Chờ quyết toán 1 Đã quyết toán    (Không có cờ đã trả lại,Vì tồn tại phần??Đơn thuốc trả thuốc)   --2006-06-17 Kích hoạt,Trường dư thừa để dễ nhận biết tình hình quyết toán   Quan trọng:Khi hủy"Chưa quyết toán"Khi kê đơn,nàycfztSẽ được đặt thành1,Vì vậy lấy tất cả đơn thuốc đã quyết toán(Vì cho rằng hủy thì đặt mã nội bộ quyết toán thành-1,Vì đã thay đổi mã nội bộ quyết toán nên cũng là một loại quyết toán)
        /// </summary>
        public string cfzt { get; set; }

        /// <summary>
        /// 0 Nơi thu phí 1 Trạm bác sĩ
        /// </summary>
        public string cfly { get; set; }

        /// <summary>
        /// 0 Chờ pha thuốc 1 Chờ phát 2 Đã phát   (Không có cờ đã trả lại,Vì tồn tại đơn thuốc trả thuốc một phần)       Trả đơn thuốc có cùng nghĩa với đơn thuốc   --2007.03.27Chú thích: Khi trả đơn thuốc:Đặt trực tiếp2
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// Vì phải chọn thuốc từ tồn kho,Vì vậy khi nhập đã xác định nhà thuốc phát thuốc.
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Cửa sổ lĩnh thuốc đặt lưu khi quyết toán thì hợp lý hơn,Nhưng có thể gây ra lượng lớn dữ liệu không thể quyết toán(Đóng cửa sổ),Vì vậy đặt tạo khi lưu đơn thuốc,Hiệu quả không có khác biệt.Có thể
        /// </summary>
        public string lyck { get; set; }

        /// <summary>
        /// Vốn nên đặt trong bảng loại lớn quyết toán,Một số thứ tự thuốc Trung y,Một số thứ tự thuốc thành phẩm Tây y,Nhưng xét đến tính phức tạp của dữ liệu,Đặc biệt đặt tại đây.   Hiện nay mỗi đơn thuốc có một số thứ tự lĩnh thuốc,Bệnh nhân quẹt thẻ phát thuốc,Số lĩnh thuốc liên tục của bản thân sẽ tự động bỏ qua sau khi phát thuốc xong.      －－Giai đoạn 1 tạm thời chưa làm2006.06.21
        /// </summary>
        public short? lyxh { get; set; }

        /// <summary>
        /// Ngày pha thuốc
        /// </summary>
        public string pyry { get; set; }

        /// <summary>
        /// Nhân viên pha thuốc
        /// </summary>
        public DateTime? pyrq { get; set; }

        /// <summary>
        /// Nhân viên phát thuốc
        /// </summary>
        public string fyry { get; set; }

        /// <summary>
        /// Ngày phát thuốc
        /// </summary>
        public DateTime? fyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete]
        public int? jsnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// Đến từ số đơn thuốc trạm làm việc bác sĩ,Đơn thuốc viết taycmsHệ thống tự động tạo số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        ///// <summary>
        ///// Số nhóm Đặt trong bảng chi tiết đơn thuốc
        ///// </summary>
        //public string czh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        public string ksmc { get; set; }
        public string ysmc { get; set; }

        /// <summary>
        /// Loại đơn thuốc EnumEnumPrescriptionType
        /// </summary>
        public int? cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cflxmc { get; set; }

        /// <summary>
        /// Ngày thu phí 20180329cộng
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// Số thang thuốc Trung y
        /// </summary>
        public int? ts { get; set; }

        /// <summary>
        /// Cờ sắc thuốc hộ 1 Cần sắc hộ
        /// </summary>
        public int? djbz { get; set; }

        /// <summary>
        /// Trạng thái nơi đăng ký(Xét nghiệm kiểm tra 0,Đã đăng ký 1,Đã nhận 2,Đã hoàn thành)
        /// </summary>
        public int? sqdzt { get; set; }

    }
}
