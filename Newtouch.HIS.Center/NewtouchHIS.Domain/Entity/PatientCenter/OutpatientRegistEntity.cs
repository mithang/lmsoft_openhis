using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.PatientCenter
{
    [Tenant(DBEnum.SettDb)]
    [SugarTable("mz_gh", "OutpatientRegistEntity")]
    public partial class OutpatientRegistEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]

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
        /// 0 -Cửa sổ  1-Đặt hẹn
        /// </summary>
        public string ghly { get; set; }

        /// <summary>
        /// 1:Ngoại trú 2:Cấp cứu Chuyên gia
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
        /// Theo khoa,số xếp hàng phát sinh theo loại đăng ký khám from mz_jzxh
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
        /// 0 Chờ quyết toán 1 Đã quyết toán 2 Đã trả   --2006-06-17 Bật,làm trường dư thừa để tiện nhận biết tình hình quyết toán      --2006.06.13 ??lúc đó không dùng chữ này??   Dường như có thể qua mã nội bộ quyết toán=0xét chưa quyết toán,>0xét đã quyết toán,Hủy mã nội bộ quyết toán>0xét đã trả?Tạm thời không dùng trường này
        /// </summary>
        public string ghzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// Cờ khám bệnh(Trạng thái khám) EnumEnumOutpatientJzbz
        /// </summary>
        public string jzbz { get; set; }

        /// <summary>
        /// Thời gian khám
        /// </summary>
        public DateTime? jzrq { get; set; }

        /// <summary>
        /// 0-Đăng ký khám thường   1-Đăng ký khám rỗng   2-Đăng ký khám giường tại nhà   3-Đăng ký khám chuyển tự trả sang BHYT         ---------------------------------------------------   0 Chờ đăng ký khám   1 Đăng ký khám   2 Trả số khám (Cũng có thể qua việc xét bản ghi nàyjsnmcó bị bản ghi khác dùng làm hay khôngcxjsnm,để biết có trả số khám hay không,nhưng làm vậy hiệu quả cao)
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
        /// Hạng mục bệnh chuyên khoa tương ứng trong bảng lịch đăng ký khám from mz_ghpb
        /// </summary>
        public int? ghzbbh { get; set; }

        /// <summary>
        /// Hạng mục bệnh chuyên khoa tương ứng trong bảng lịch đăng ký khám from mz_ghpb
        /// </summary>
        public string ghzb { get; set; }


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
        /// Số quyết toán BHYT(Tiền đặt)
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// Nguyên nhân khám
        /// </summary>
        public string jzyy { get; set; }
        /// <summary>
        /// Người giới thiệu
        /// </summary>
        public string tjr { get; set; }

        /// <summary>
        /// Đăng ký khám đặt hẹn ngoại trúID Tương ứngNewtouch_CIS.dbo.mz_yygh.Id
        /// </summary>
        public string yyghId { get; set; }

        /// <summary>
        /// Hiển thị văn bản tuổi
        /// </summary>
        public string nlshow { get; set; }

        /// <summary>
        /// Số thứ tự bồi thường ngoại trú Dùng cho hợp tác y tế nông thôn mới Quý Châu
        /// </summary>
        public string outpId { get; set; }
        /// <summary>
        /// Mã phụ trợ nguồn đăng ký khám Phân biệt dấu hiệu khác nhau của cùng một nguồnBệnh nhân
        /// Dùng cho khám sức khỏe
        /// </summary>
        public string ghlybz { get; set; }
        /// <summary>
        /// Lịch đăng ký khámId
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
