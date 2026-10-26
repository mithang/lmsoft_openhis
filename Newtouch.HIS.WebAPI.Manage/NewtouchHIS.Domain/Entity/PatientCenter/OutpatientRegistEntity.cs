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
        /// 0 -cửa sổ  1-hẹn
        /// </summary>
        public string ghly { get; set; }

        /// <summary>
        /// 1：khám ngoại trú 2：cấp cứu chuyên gia
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
        /// theo khoa、số xếp hàng do loại đăng ký khám tạo ra from mz_jzxh
        /// </summary>
        public Int16? jzxh { get; set; }

        /// <summary>
        /// phí đăng ký khám hạng mục đăng ký khám from xt_sfxm where dl = phí đăng ký khám and zt = '1'
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
        /// 0 chờ kết toán 1 đã kết toán 2 đã trả lại   --2006-06-17 bật,là trường dự phòng để dễ nhận biết tình hình thanh toán      --2006.06.13 ??khi không dùng chữ này??   hình như có thể thông qua mã nội bộ thanh toán＝0phán đoán chưa kết toán，>0phán đoán đã kết toán，thu hồi mã nội bộ thanh toán>0phán đoán đã trả lại？tạm thời không dùng trường này
        /// </summary>
        public string ghzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// cờ khám bệnh（trạng thái khám bệnh） enumEnumOutpatientJzbz
        /// </summary>
        public string jzbz { get; set; }

        /// <summary>
        /// thời gian khám bệnh
        /// </summary>
        public DateTime? jzrq { get; set; }

        /// <summary>
        /// 0-đăng ký khám thông thường   1-đăng ký khám trống   2-đăng ký khám giường tại nhà   3-đăng ký khám chuyển từ tự trả sang bảo hiểm y tế         ---------------------------------------------------   0 chờ đăng ký khám   1 đăng ký khám   2 trả số khám (cũng có thể thông qua phán đoán của bản ghi nàyjsnmcó được bản ghi khác dùng làm hay khôngcxjsnm，biết được có trả số khám hay không，nhưng như vậy hiệu quả cao)
        /// </summary>
        public string ghxz { get; set; }

        /// <summary>
        /// 1.tái khám 0.khám lần đầu
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
        /// hạng mục bệnh chuyên khoa tương ứng trong bảng lịch đăng ký khám from mz_ghpb
        /// </summary>
        public int? ghzbbh { get; set; }

        /// <summary>
        /// hạng mục bệnh chuyên khoa tương ứng trong bảng lịch đăng ký khám from mz_ghpb
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
        /// đơn hàngId
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
        /// số thanh toán bảo hiểm y tế（đặt trước）
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// nguyên nhân khám bệnh
        /// </summary>
        public string jzyy { get; set; }
        /// <summary>
        /// người giới thiệu
        /// </summary>
        public string tjr { get; set; }

        /// <summary>
        /// đăng ký khám hẹn ngoại trúID tương ứngNewtouch_CIS.dbo.mz_yygh.Id
        /// </summary>
        public string yyghId { get; set; }

        /// <summary>
        /// hiển thị văn bản tuổi
        /// </summary>
        public string nlshow { get; set; }

        /// <summary>
        /// số thứ tự bồi thường ngoại trú dùng chung hợp tác y tế nông thôn mới Quý Châu
        /// </summary>
        public string outpId { get; set; }
        /// <summary>
        /// định danh phụ nguồn đăng ký khám phân biệt bệnh nhân được đánh dấu khác nhau từ cùng một nguồn
        /// dùng cho khám sức khỏe
        /// </summary>
        public string ghlybz { get; set; }
        /// <summary>
        /// lịch đăng ký khámId
        /// </summary>
        public decimal? ScheduId { get; set; }
        /// <summary>
        /// khám bệnhID
        /// </summary>
        public string jzid { get; set; }
        public string jzpzlx { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }

        public string ecToken { get; set; }
    }
}
