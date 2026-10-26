using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    [Table("Com_Diagnosis")]
    public class ComDiagnosisEntity : IEntity<ComDiagnosisEntity>
    {

        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Mã cơ sở
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Tên chẩn đoán thường dùng
        /// </summary>
        public string cyzdmc { get; set; }
        /// <summary>
        /// Mã chẩn đoán thường dùng
        /// </summary>
        public string cyzdbm { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// Loại chẩn đoán thường dùng(1 Tây y 2 Đông y)
        /// </summary>
        public string cyzdtype { get; set; }
        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// Người cập nhật
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        ///Thời gian cập nhật
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// Trạng thái(0 Vô hiệu 1 Có hiệu lực)
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Kiểm soát quyền 1Cá nhân 2Khoa
        /// </summary>
        public string qxkz { get; set; }
    }
}
