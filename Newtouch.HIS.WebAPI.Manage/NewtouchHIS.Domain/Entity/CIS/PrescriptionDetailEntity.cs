using System.ComponentModel.DataAnnotations;
using SqlSugar;
using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Domain.Entity.CIS
{
    [Tenant(DBEnum.CisDb)]
    [SugarTable("xt_cfmx", "PrescriptionDetailEntity")]
    public class PrescriptionDetailEntity
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cfmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// bảng đơn thuốc liên kết
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// liên kếtbasebảng hạng mục thu phí của cơ sở dữ liệu
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// trường dự phòng
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// liên kếtbasebảng thuốc của cơ sở dữ liệu
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// mã dự phòng
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// liều điều trị mỗi lần  đối với hạng mục phục hồi chức năng
        /// </summary>
        public int? mczll { get; set; }
        public decimal? mcjl { get; set; }

        /// <summary>
        /// lấybasebảng thuộc tính thuốc của cơ sở dữ liệu
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// liên kếtbasebảng cách dùng thuốc của cơ sở dữ liệu
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// liên kếtbasetần suất y lệnh của cơ sở dữ liệu
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// tổng số lượng tính phí
        /// </summary>
        public int? zl { get; set; }

        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string urgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string purpose { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// mã duy nhất bảo hiểm y tế
        /// </summary>
        public string ybwym { get; set; }
        /// <summary>
        /// cờ hạn chế sử dụng0không giới hạn 1không phù hợp quy định hạn chế 2phù hợp quy định hạn chế
        /// </summary>
        public string xzsybz { get; set; }
        /// <summary>
        /// số ngày
        /// </summary>
        public decimal? ts { get; set; }
        /// <summary>
        /// nhómid
        /// </summary>
        public string ztId { get; set; }
        /// <summary>
        /// tên nhóm
        /// </summary>
        public string ztmc { get; set; }
        /// <summary>
        /// số hiệu nhóm
        /// </summary>
        public string ztCode { get; set; }

        /// <summary>
        /// nguyên nhân kháng sinh
        /// </summary>
        public string kssReason { get; set; }

        /// <summary>
        /// phương pháp vị trí
        /// </summary>
        public string bwff { get; set; }

        public string sqlx { get; set; }

        public int? px { get; set; }
        /// <summary>
        /// tốc độ truyền
        /// </summary>
        public int? ds { get; set; }

        /// <summary>
        /// cờ chuyển sang tự trả
        /// </summary>
        public int? zzfbz { get; set; }

        //cờ thử phản ứng da
        public string ispsbz { get; set; }

        //cờ theo dõi
        public string islgbz { get; set; }
        public int? ztsl { get; set; }
        public int? sfzt { get; set; }
        public string zysm { get; set; }
        /// <summary>
        /// nhóm hạng mục tương ứng với cách dùng
        /// </summary>
        public string syncfbz { get; set; }
    }

}
