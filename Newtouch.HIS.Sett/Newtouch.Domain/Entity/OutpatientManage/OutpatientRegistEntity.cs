using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Đăng ký khám ngoại trú
    /// </summary>
    [Table("mz_gh")]
    public class OutpatientRegistEntity : IEntity<OutpatientRegistEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ghnm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 0 -Cửa sổ  1-Hẹn khám
        /// </summary>
        public string ghly { get; set; }

        /// <summary>
        /// 1:Khám ngoại trú 2:Cấp cứu Chuyên gia
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Theo khoa,Số xếp hàng do loại đăng ký khám tạo ra from mz_jzxh
        /// </summary>
        public Int16? jzxh { get; set; }

        /// <summary>
        /// Phí đăng ký khám Hạng mục đăng ký khám from xt_sfxm where dl = Phí đăng ký khám and zt = '1'
        /// </summary>
        public string ghlx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ghf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zlf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ckf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal gbf { get; set; }

        /// <summary>
        /// 0 Chờ quyết toán 1 Đã quyết toán 2 Đã trả lại   --2006-06-17 Kích hoạt,Trường dư thừa để dễ nhận biết tình hình quyết toán      --2006.06.13 ??Khi đó không dùng chữ này??   Hình như có thể thông qua mã nội bộ quyết toán＝0Xác định chưa quyết toán,>0Xác định đã quyết toán,Mã nội bộ hủy quyết toán>0Xác định đã trả lại?Tạm thời không dùng trường này
        /// </summary>
        public string ghzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// Cờ khám bệnh(Trạng thái khám bệnh) EnumEnumOutpatientJzbz
        /// </summary>
        public string jzbz { get; set; }

        /// <summary>
        /// Thời gian khám bệnh
        /// </summary>
        public DateTime? jzrq { get; set; }

        /// <summary>
        /// 0-Đăng ký khám thông thường   1-Đăng ký khám rỗng   2-Đăng ký khám giường gia đình   3-Đăng ký khám tự chi trả chuyển sang BHYT         ---------------------------------------------------   0 Chờ đăng ký khám   1 Đăng ký khám   2 Trả số (Cũng có thể thông qua việc xác định bản ghi nàyjsnmCó bị bản ghi khác dùng làm hay khôngcxjsnm,Biết có trả số hay không,Nhưng làm vậy hiệu quả cao)
        /// </summary>
        public string ghxz { get; set; }

        /// <summary>
        /// 1.Tái khám 0.Khám lần đầu
        /// </summary>
        public byte? fzbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dbxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gsrdh { get; set; }

        /// <summary>
        /// Hạng mục bệnh chuyên khoa tương ứng trong bảng lịch trực đăng ký khám from mz_ghpb
        /// </summary>
        public int? ghzbbh { get; set; }

        /// <summary>
        /// Hạng mục bệnh chuyên khoa tương ứng trong bảng lịch trực đăng ký khám from mz_ghpb
        /// </summary>
        public string ghzb { get; set; }

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
        public bool zzjm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdicd10 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? csny { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Đơn hàngId
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CardTypeName { get; set; }

        public string brly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? xlbrbz { get; set; }

        public DateTime? ghrq { get; set; }
        public string lxr { get; set; }
        public string lxrgx { get; set; }
        public string lxrdh { get; set; }

        /// <summary>
        /// Số quyết toán BHYT(Tiền tố)
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// Lý do khám
        /// </summary>
        public string jzyy { get; set; }
        /// <summary>
        /// Người giới thiệu
        /// </summary>
        public string tjr { get; set; }

        /// <summary>
        /// Đăng ký khám hẹn trước ngoại trúID Tương ứngNewtouch_CIS.dbo.mz_yygh.Id
        /// </summary>
        public string yyghId { get; set; }

        /// <summary>
        /// Hiển thị tuổi dạng văn bản
        /// </summary>
        public string nlshow { get; set; }

        /// <summary>
        /// Số thứ tự bồi thường ngoại trú Bảo hiểm nông thôn mới Quý Châu dùng
        /// </summary>
        public string outpId { get; set; }
        /// <summary>
        /// Dấu phụ nguồn đăng ký khám Phân biệt bệnh nhân có dấu khác nhau cùng nguồn
        /// Dùng để khám sức khỏe
        /// </summary>
        public string ghlybz { get; set; }
        /// <summary>
        /// Lịch trực đăng ký khámId
        /// </summary>
        public decimal? ScheduId { get; set; }
        /// <summary>
        /// Khám bệnhID
        /// </summary>
        public string jzid { get; set; }
        public string jzpzlx { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }

        public string ecToken { get; set; }
    }
}
