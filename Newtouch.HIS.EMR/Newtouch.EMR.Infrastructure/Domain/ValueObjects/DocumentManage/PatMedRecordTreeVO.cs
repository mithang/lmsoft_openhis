using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class PatMedRecordTreeVO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Blzt { get; set; }
        public string Doccode { get; set; }
        public string Docname { get; set; }
        public DateTime? Blrq { get; set; }

        public string parentId { get; set; }
        public string parentbllx { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifierTime { get; set; }
        public string BllxId { get; set; }
        public string zyh { get; set; }
        public string mzh { get; set; }
        public string BlId { get; set; }
        /// <summary>
        /// Loại bệnh án EnumBllx
        /// </summary>
        public string bllx { get; set; }
        /// <summary>
        /// Dấu hiệu có cho phép thêm tệp hay không
        /// </summary>
        public int? addPermit { get; set; }
        /// <summary>
        /// Cờ kiểm soát quyền đọc/sửa tệp EnummbqxFp
        /// </summary>
        public int? ctrlLevel { get; set; }
        /// <summary>
        /// Mã phương thức tương ứng tải lên bảo hiểm y tế
        /// </summary>
        public string Ybbm { get; set; }
        /// <summary>
        /// Trạng thái kế hoạch EnumPlanStu
        /// </summary>
        public string PlanStu { get; set; }
        public string mblj { get; set; }
        public int? LoadWay { get; set; }
        public int? IsLock { get; set; }
        public string doctype { get; set; }
    }
}
