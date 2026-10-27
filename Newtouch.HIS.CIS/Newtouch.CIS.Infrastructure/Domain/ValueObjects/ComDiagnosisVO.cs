using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class ComDiagnosisVO
    {
        public string zdmc { get; set; }
        public string zdCode { get; set; }
        //public string py { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// Cơ sởMã
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// thường dùngTên chẩn đoán
        /// </summary>
        public string cyzdmc { get; set; }
        /// <summary>
        /// thường dùngMã chẩn đoán
        /// </summary>
        public string cyzdbm { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// thường dùngLoại chẩn đoán(1 tâyy 2 trongy)
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
        /// Bác sĩMã nhân viên
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
        /// Cập nhật người
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        ///Thời gian cập nhật
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// Trạng thái(0 Vô hiệu 1 Hiệu lực)
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Cá nhâncònlàKhoa
        /// </summary>
        public string isgr { get; set; }
        /// <summary>
        /// Thứ tự
        /// </summary>
        public System.Int64? px { get; set; }

    }
}
