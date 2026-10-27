using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.CIS.Compat;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày：2018-06-28 15:37
    /// Mô tả：Nhập việnbệnh nhândấu hiệu sinh tồn
    /// </summary>
    [SugarTable("zy_brsmtz")]
    public class InpatientVitalSignsEntity
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
        /// Thời gianđiểm 4 8 12 16 20 24
        /// </summary>
        /// <returns></returns>
        public int sj { get; set; }
        /// <summary>
        /// Liệt kê vui lònggiả từ chốiđo đợi
        /// </summary>
        /// <returns></returns>
        public int? brzt { get; set; }
        /// <summary>
        /// tw
        /// </summary>
        /// <returns></returns>
        public decimal? tw { get; set; }
        /// <summary>
        /// Nhiệt độđolượngMô tả miệngnhiệt Nhiệt độ nách Nhiệt độ hậu môn
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
        /// từ điểnkhôngkhuphútOrg
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
        /// từ điểnkhôngkhuphútOrg
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
        /// vượtdịchlượng
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
        /// từ điểnkhôngkhuphútOrg
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
        /// tựđịnhnghĩaThời gianđiểm
        /// </summary>
        public string zdysjd { get; set; }

        public string lrflag { get; set; }

        /// <summary>
        /// điều dưỡngýbiết
        /// </summary>
        public string hlys { get; set; }
        /// <summary>
        /// bệnh nhânuốngăn
        /// </summary>
        public string brfood { get; set; }
        /// <summary>
        /// datình hình
        /// </summary>
        public string pfqk { get; set; }
        /// <summary>
        /// quảnđườngđiều dưỡng
        /// </summary>
        public string gdhl { get; set; }

        /// <summary>
        /// Tình trạng bệnhquan sátvàbiện phápthi
        /// </summary>
        public string bqgcjcs { get; set; }
        /// <summary>
        /// uốngănlượngĐường
        /// </summary>
        public string ysltj { get; set; }
        /// <summary>
        /// nônlượngmàu sắc
        /// </summary>
        public string otlys { get; set; }
        /// <summary>
        /// nước tiểulượngmàu sắc
        /// </summary>
        public string nlys { get; set; }
        /// <summary>
        /// Điều dưỡngchữ ký
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// nhập lượng dịch
        /// </summary>
        public int? srytl { get; set; }
        /// <summary>
        /// wljw
        /// </summary>
        public decimal? wljw { get; set; }
        /// <summary>
        /// bệnh nhânTrạng thái-giờ
        /// </summary>
        public string brzt_hh { get; set; }
        /// <summary>
        /// bệnh nhânTrạng thái-phút
        /// </summary>
        public string brzt_mi { get; set; }
        /// <summary>
        /// dẫndònglượng
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
        /// quamẫnthuốcvật
        /// </summary>
        public string gmyw { get; set; }
        /// <summary>
        /// thâncao
        /// </summary>
        public string sg { get; set; }
        /// <summary>
        /// ĐauChấm điểm
        /// </summary>
        public string ttpf { get; set; }
    }
}
