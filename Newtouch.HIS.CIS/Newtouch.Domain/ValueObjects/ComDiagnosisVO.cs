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
        /// Cá nhân hay khoa
        /// </summary>
        public string isgr { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public System.Int64? px { get; set; }

    }
}
