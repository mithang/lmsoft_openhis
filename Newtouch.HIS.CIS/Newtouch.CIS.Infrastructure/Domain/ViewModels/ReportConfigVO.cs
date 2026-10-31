using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ViewModels
{
    public class ReportConfigVO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// ứngdùngMãSett\CIS đợi
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// Tên báo cáo
        /// </summary>
        public string RptName { get; set; }
        /// <summary>
        /// Báo cáophụcvụroot
        /// </summary>
        public string RptHost { get; set; }
        /// <summary>
        /// Báo cáoĐịa chỉ
        /// </summary>
        public string RptUrl { get; set; }
        /// <summary>
        /// Nguồn dữ liệu
        /// </summary>
        public string RptSource { get; set; }
        /// <summary>
        /// tham số
        /// </summary>
        public string Paras { get; set; }
        /// <summary>
        /// côngcụ thểcột date(phiếuNgày) ,daterange(Thời gianphạm vi)
        /// </summary>
        public string Tools { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public string zt { get; set; }
        public IList<ReportPara> Paralist { get; set; }
        /// <summary>
        /// Báo cáo Mã
        /// </summary>
        public string RptCode { get; set; }
    }

    public class ReportPara
    {
        public string p { get; set; }
    }

    public class ReportParaDetail
    {
        public string orgId { get; set; }
        public string zyh { get; set; }
        public string mzh { get; set; }
        public DateTime? kssj { get; set; }
        public DateTime? jssj { get; set; }
        public string rq { get; set; }
        public string doctor { get; set; }
        public string dept { get; set; }
        public string keyValue { get; set; }
        public string cfh { get; set; }
        public string sqdh { get; set; }
        public string lsh { get; set; }
    }
}
