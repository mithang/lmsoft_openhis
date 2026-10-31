using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo：毛浩泽
    /// Ngày:2021-07-02 15:37
    /// Mô tả:Nhập việnY lệnhChẩn đoánthông tin
    /// </summary>
    [SugarTable("zy_clyzzd")]
    public class InpatientDiagnosisEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// yzId
        /// </summary>
        /// <returns></returns>
        public string yzId { get; set; }
        /// <summary>
        /// yzh
        /// </summary>
        /// <returns></returns>
        public string yzh { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// Chẩn đoánThời gian
        /// </summary>
        /// <returns></returns>
        public DateTime? zdsj { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        /// <returns></returns>
        public string icd10 { get; set; }
        /// <summary>
        /// Tên chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string zdmc { get; set; }
        /// <summary>
        /// làKhôngthuốc gây nghiện hướng thầnloạiY lệnh
        /// </summary>
        /// <returns></returns>
        public string yztag { get; set; }
        /// <summary>
        /// Y lệnhThứ tự
        /// </summary>
        /// <returns></returns>
        public string yzpx { get; set; }
        /// <summary>
        /// Kiểu y lệnh
        /// </summary>
        /// <returns></returns>
        public int? yzlx { get; set; }
        /// <summary>
        /// Y lệnhTính chất
        /// </summary>
        /// <returns></returns>
        public string yzxz { get; set; }
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
        /// yviệnduy nhấtmộtid
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
    }
}
