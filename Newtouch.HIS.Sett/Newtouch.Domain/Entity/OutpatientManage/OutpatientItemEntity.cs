using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Hạng mục ngoại trú
    /// </summary>
    [Table("mz_xm")]
    public class OutpatientItemEntity : IEntity<OutpatientItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int xmnm { get; set; }

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
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Số lượng tính giá
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0 Chờ quyết toán 1 Đã quyết toán 2 Đã trả lại
        /// (Bất kể có liên kết đơn thuốc hay không,Đều phải gán giá trị)
        /// </summary>
        public string xmzt { get; set; }

        /// <summary>
        /// 0 Nơi thu phí 1 Trạm bác sĩ
        /// (Bất kể có liên kết đơn thuốc hay không,Đều phải gán giá trị)
        /// </summary>
        public string xmly { get; set; }

        /// <summary>
        /// 0 Chờ thực hiện 1 Đã thực hiện(Đã kết thúc thực hiện/Không còn thực hiện) 2Không cần thực hiện 9Trong quá trình thực hiện
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ssry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ssrq { get; set; }

        /// <summary>
        /// Mã nội bộ quyết toán(Liên kết quyết toán nhiều lần,Hủy quyết toán Vì vậy việc gán giá trị không có ý nghĩa gì)
        /// </summary>
        public int? jsnm { get; set; }

        /// <summary>
        /// Ngày quyết toán(Liên kết quyết toán nhiều lần,Hủy quyết toán Vì vậy việc gán giá trị không có ý nghĩa gì)
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// Tỷ lệ giảm miễn
        /// </summary>
        private decimal? _jmbl { get; set; }
        /// <summary>
        /// Tỷ lệ giảm miễn
        /// </summary>
        public decimal? jmbl
        {
            get
            {
                return _jmbl ?? 0;
            }
            set
            {
                _jmbl = value;
            }
        }

        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        private decimal? _jmje { get; set; }
        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        public decimal? jmje
        {
            get
            {
                return _jmje ?? 0;
            }
            set
            {
                _jmje = value;
            }
        }

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

        public string ysmc { get; set; }
        public string ksmc { get; set; }

        public string jzjhmxId { get; set; }
        public string kflb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? ttbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// Tổng liều điều trị
        /// </summary>
        public int? zzll { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// 20180326Hạng mục cũng bắt đầu liên kết bảng chính đơn thuốc
        /// </summary>
        public int? cfnm { get; set; }

        /// <summary>
        /// Ngày thu phí 20180329cộng
        /// </summary>
        public DateTime? sfrq { get; set; }
        /// <summary>
        /// Ghi chú 20180408cộng
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Liều điều trị mỗi lần(Cần khi tạo kế hoạch từ hạng mục)
        /// </summary>
        public int? dczll { get; set; }

        /// <summary>
        /// Số lần thực hiện(Cần khi tạo kế hoạch từ hạng mục)
        /// </summary>
        public int? zxcs { get; set; }
        /// <summary>
        /// Cờ chuyển sang tự chi trả
        /// </summary>
        public int? zzfbz { get; set; }

        public string ztId { get; set; }
        public string ztmc { get; set; }
        public int? ztsl { get; set; }
    }
}
