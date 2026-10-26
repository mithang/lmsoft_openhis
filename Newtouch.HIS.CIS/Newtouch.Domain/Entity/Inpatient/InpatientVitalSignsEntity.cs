using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Dấu hiệu sinh tồn bệnh nhân nội trú
    /// </summary>
    [Table("zy_brsmtz")]
    public class InpatientVitalSignsEntity : IEntity<InpatientVitalSignsEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// rq
        /// </summary>
        /// <returns></returns>
        public DateTime rq { get; set; }
        /// <summary>
        /// Thời điểm 4 8 12 16 20 24
        /// </summary>
        /// <returns></returns>
        public int sj { get; set; }
        /// <summary>
        /// Enum Xin nghỉ Từ chối đo v.v.
        /// </summary>
        /// <returns></returns>
        public int? brzt { get; set; }
        /// <summary>
        /// tw
        /// </summary>
        /// <returns></returns>
        public decimal? tw { get; set; }
        /// <summary>
        /// Hướng dẫn đo thân nhiệt Nhiệt độ miệng Nhiệt độ nách Nhiệt độ hậu môn
        /// </summary>
        /// <returns></returns>
        public int? twclfs { get; set; }
        /// <summary>
        /// xl
        /// </summary>
        /// <returns></returns>
        public int? xl { get; set; }
        /// <summary>
        /// mb
        /// </summary>
        /// <returns></returns>
        public int? mb { get; set; }
        /// <summary>
        /// qbq
        /// </summary>
        /// <returns></returns>
        public bool? qbq { get; set; }
        /// <summary>
        /// hx
        /// </summary>
        /// <returns></returns>
        public int? hx { get; set; }
        /// <summary>
        /// hxj
        /// </summary>
        /// <returns></returns>
        public bool? hxj { get; set; }
        /// <summary>
        /// xysz
        /// </summary>
        /// <returns></returns>
        public int? xysz { get; set; }
        /// <summary>
        /// xyxz
        /// </summary>
        /// <returns></returns>
        public int? xyxz { get; set; }
        /// <summary>
        /// tz
        /// </summary>
        /// <returns></returns>
        public string tz { get; set; }
        /// <summary>
        /// Từ điển không phân biệtOrg
        /// </summary>
        /// <returns></returns>
        public string tzclbz { get; set; }
        /// <summary>
        /// syl
        /// </summary>
        /// <returns></returns>
        public int? syl { get; set; }
        /// <summary>
        /// ysl
        /// </summary>
        /// <returns></returns>
        public int? ysl { get; set; }
        /// <summary>
        /// sxl
        /// </summary>
        /// <returns></returns>
        public int? sxl { get; set; }
        /// <summary>
        /// dbcs
        /// </summary>
        /// <returns></returns>
        public int? dbcs { get; set; }
        /// <summary>
        /// Từ điển không phân biệtOrg
        /// </summary>
        /// <returns></returns>
        public string dbcsbz { get; set; }
        /// <summary>
        /// otl
        /// </summary>
        /// <returns></returns>
        public int? otl { get; set; }
        /// <summary>
        /// xbl
        /// </summary>
        /// <returns></returns>
        public int? xbl { get; set; }
        /// <summary>
        /// Lượng dịch vượt mức
        /// </summary>
        /// <returns></returns>
        public int? cys { get; set; }
        /// <summary>
        /// skyll
        /// </summary>
        /// <returns></returns>
        public int? skyll { get; set; }
        /// <summary>
        /// xyl
        /// </summary>
        /// <returns></returns>
        public int? xyl { get; set; }
        /// <summary>
        /// wyl
        /// </summary>
        /// <returns></returns>
        public int? wyl { get; set; }
        /// <summary>
        /// fxxq
        /// </summary>
        /// <returns></returns>
        public int? fxxq { get; set; }
        /// <summary>
        /// qtsc
        /// </summary>
        /// <returns></returns>
        public int? qtsc { get; set; }
        /// <summary>
        /// xyll
        /// </summary>
        /// <returns></returns>
        public int? xyll { get; set; }
        /// <summary>
        /// Từ điển không phân biệtOrg
        /// </summary>
        /// <returns></returns>
        public string xyfs { get; set; }
        /// <summary>
        /// xybhd
        /// </summary>
        /// <returns></returns>
        public int? xybhd { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// px
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// Thời điểm tùy chỉnh
        /// </summary>
        public string zdysjd { get; set; }

        public string lrflag { get; set; }

        /// <summary>
        /// Nhận thức chăm sóc
        /// </summary>
        public string hlys { get; set; }
        /// <summary>
        /// Chế độ ăn bệnh nhân
        /// </summary>
        public string brfood { get; set; }
        /// <summary>
        /// Tình trạng da
        /// </summary>
        public string pfqk { get; set; }
        /// <summary>
        /// Chăm sóc ống dẫn
        /// </summary>
        public string gdhl { get; set; }

        /// <summary>
        /// Theo dõi bệnh tình và biện pháp
        /// </summary>
        public string bqgcjcs { get; set; }
        /// <summary>
        /// Đường dùng lượng ăn
        /// </summary>
        public string ysltj { get; set; }
        /// <summary>
        /// Lượng và màu sắc nôn
        /// </summary>
        public string otlys { get; set; }
        /// <summary>
        /// Lượng và màu sắc nước tiểu
        /// </summary>
        public string nlys { get; set; }
        /// <summary>
        /// Chữ ký điều dưỡng
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// Lượng dịch truyền vào
        /// </summary>
        public int? srytl { get; set; }
        /// <summary>
        /// wljw
        /// </summary>
        public decimal? wljw { get; set; }
        /// <summary>
        /// Trạng thái bệnh nhân-Giờ
        /// </summary>
        public string brzt_hh { get; set; }
        /// <summary>
        /// Trạng thái bệnh nhân-phút
        /// </summary>
        public string brzt_mi { get; set; }
        /// <summary>
        /// Lượng dịch dẫn lưu
        /// </summary>
        public string yll { get; set; }
        /// <summary>
        /// Tổng lượng vào
        /// </summary>
        public string zrl { get; set; }
        /// <summary>
        /// Tổng lượng ra
        /// </summary>
        public string zcl { get; set; }
        /// <summary>
        /// Thuốc gây dị ứng
        /// </summary>
        public string gmyw { get; set; }
        /// <summary>
        /// Chiều cao
        /// </summary>
        public string sg { get; set; }
        /// <summary>
        /// Điểm đau
        /// </summary>
        public string ttpf { get; set; }
    }
}