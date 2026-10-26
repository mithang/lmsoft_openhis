using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MzpbResponse
    {
        public DateTime OutDate { get; set; }
        public string OutDept { get; set; }
        public string OutDeptName { get; set; }
        /// <summary>
        /// mjzbz Loại ngoại trú
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// lịch trực mô tả
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Tổng số nguồn số
        /// </summary>
        public int? TotalNum { get; set; }
        /// <summary>
        /// còn lại nguồn số 
        /// </summary>
        public int? BookNum { get; set; }
    }
}
