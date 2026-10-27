using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày：2018-06-28 15:37
    /// Mô tả：Nhập việnbệnh nhânChẩn đoánthông tin
    /// </summary>
    [SugarTable("zy_PatDxInfo")]
    public class InpatientPatientDiagnosisEntity
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
        /// WM tâyy  TCM trongy
        /// </summary>
        public string zddl { get; set; }
        /// <summary>
        /// 1Chẩn đoán nhập viện2Xuất việnChẩn đoán
        /// </summary>
        /// <returns></returns>
        public string zdlb { get; set; }
        /// <summary>
        /// 0chínhcầnChẩn đoán1thứmộthỗ trợChẩn đoán2thứhaihỗ trợChẩn đoán3thứbahỗ trợChẩn đoán9tựđịnhnghĩaChẩn đoán
        /// </summary>
        /// <returns></returns>
        public string zdlx { get; set; }
        /// <summary>
        /// zddm
        /// </summary>
        /// <returns></returns>
        public string zddm { get; set; }
        /// <summary>
        /// zdmc
        /// </summary>
        /// <returns></returns>
        public string zdmc { get; set; }
        /// <summary>
        /// zdyzdmc
        /// </summary>
        /// <returns></returns>
        public string zdyzdmc { get; set; }
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
        /// Xuất việntình hình
        /// </summary>
        public int? cyqk { get; set; }
        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }
    }
}
