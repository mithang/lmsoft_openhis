using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Nhập việntrảthuốcyêu cầukho
    /// </summary>
    [SugarTable("zy_tyjl")]
    public class InpatientMedicineReturnEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// tương ứngphát thuốcghi chépId
        /// </summary>
        public string fyId { get; set; }
        /// <summary>
        /// Tổ chứcID
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// trảthuốcthứ tựsố (dấughimột lầntrảthuốc)
        /// </summary>
        /// <returns></returns>
        public decimal tyxh { get; set; }
        /// <summary>
        /// Y lệnhthứ tựsố
        /// </summary>
        /// <returns></returns>
        public string yzxh { get; set; }
        /// <summary>
        /// phútnhómthứ tựsố
        /// </summary>
        public int? fzxh { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string hzxm { get; set; }
        /// <summary>
        /// Khu bệnhMã
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// phát thuốcyêu cầuthứ tựsố
        /// </summary>
        /// <returns></returns>
        public decimal? fyqqxh { get; set; }
        /// <summary>
        /// Mã thuốc
        /// </summary>
        /// <returns></returns>
        public string ypdm { get; set; }
        /// <summary>
        /// trảthuốcSố lượng
        /// </summary>
        /// <returns></returns>
        public int tysl { get; set; }
        /// <summary>
        /// có thểtrảThuốcSố lượng
        /// </summary>
        /// <returns></returns>
        public int ktypsl { get; set; }
        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// Đơn vị thuốc
        /// </summary>
        /// <returns></returns>
        public string ypdw { get; set; }
        /// <summary>
        /// Đơn vịhệsố
        /// </summary>
        /// <returns></returns>
        public decimal? dwxs { get; set; }
        /// <summary>
        /// Kho thuốchệsố
        /// </summary>
        /// <returns></returns>
        public decimal? ykxs { get; set; }
        /// <summary>
        /// ThuốcĐơn giá
        /// </summary>
        /// <returns></returns>
        public decimal? ypdj { get; set; }
        /// <summary>
        /// (0 Đăng kýtrảthuốc 1 trảthuốcthành công)
        /// </summary>
        /// <returns></returns>
        public int? tyqrbz { get; set; }
        /// <summary>
        /// trảthuốcxác nhậnthứ tựsố thaythaygốc:tyqqxh
        /// </summary>
        /// <returns></returns>
        public decimal? tyqrxh { get; set; }
        /// <summary>
        /// trảthuốcThao tácviênMã
        /// </summary>
        /// <returns></returns>
        public string tyczydm { get; set; }
        /// <summary>
        /// Nhà thuốctrảthuốcThao tácviênMã
        /// </summary>
        /// <returns></returns>
        public string yftyczydm { get; set; }
        /// <summary>
        /// Nhà thuốctrảthuốcNgày
        /// </summary>
        /// <returns></returns>
        public DateTime? yftyrq { get; set; }
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
        /// 0Vô hiệu1Hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// trảthuốcphiếusố
        /// </summary>
        /// <returns></returns>
        public string tydh { get; set; }
    }
}
