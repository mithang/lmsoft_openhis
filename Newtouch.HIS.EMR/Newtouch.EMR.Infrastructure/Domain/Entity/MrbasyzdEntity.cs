using System;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2020-03-13 11:29
    /// Mô tả:Trang đầu bệnh án-Bản ghi chẩn đoán
    /// </summary>
    [SugarTable("mr_basy_zd")]
    public class MrbasyzdEntity 
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// BAH
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }
        /// <summary>
        /// WM  TCM
        /// </summary>
        public string ZDLB { get; set; }
        /// <summary>
        /// ZDLX 1 Chẩn đoán chính 2 Chẩn đoán khác 3 Bệnh chính Đông y 4 Chứng chính Đông y
        /// </summary>
        /// <returns></returns>
        public string ZDLX { get; set; }
        /// <summary>
        /// ZDOrder
        /// </summary>
        /// <returns></returns>
        public int ZDOrder { get; set; }

        /// <summary>
        /// JBDM
        /// </summary>
        /// <returns></returns>
        public string JBDM { get; set; }

        /// <summary>
        /// JBMC
        /// </summary>
        /// <returns></returns>
        public string JBMC { get; set; }

        /// <summary>
        /// RYBQ
        /// </summary>
        /// <returns></returns>
        public string RYBQ { get; set; }

        /// <summary>
        /// RYBQMS
        /// </summary>
        /// <returns></returns>
        public string RYBQMS { get; set; }

        /// <summary>
        /// CYQK
        /// </summary>
        /// <returns></returns>
        public string CYQK { get; set; }

        /// <summary>
        /// CYQKMS
        /// </summary>
        /// <returns></returns>
        public string CYQKMS { get; set; }

        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

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
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

    }
}
