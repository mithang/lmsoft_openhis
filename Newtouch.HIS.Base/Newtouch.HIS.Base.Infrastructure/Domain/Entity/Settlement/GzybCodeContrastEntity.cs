using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("Gzyb_CodeContrast")]
    public class GzybCodeContrastEntity : EntityBase
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int id { get; set; }

        /// <summary>
        /// Loại mã
        /// </summary>
        public string aaa100 { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string aaa101 { get; set; }

        /// <summary>
        /// Giá trị mã
        /// </summary>
        public string aaa102 { get; set; }

        /// <summary>
        /// Tên mã
        /// </summary>
        public string aaa103 { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Nhân viên lập hồ sơ
        /// </summary>
        public string jdry { get; set; }

        /// <summary>
        /// Ngày lập hồ sơ
        /// </summary>
        public DateTime jdrq { get; set; }
        
    }
}
