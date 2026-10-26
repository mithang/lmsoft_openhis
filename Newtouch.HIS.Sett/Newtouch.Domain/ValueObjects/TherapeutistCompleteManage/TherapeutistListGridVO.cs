

using System;

namespace Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage
{
    /// <summary>
    /// Hiển thị trong danh sách công việc của kỹ thuật viên điều trịGrid
    /// </summary>
    public class TherapeutistListGridVO
    {
        public string type { get; set; }
        public decimal sl { get; set; }
        public decimal zlsc { get; set; }
        public DateTime? jzsj { get; set; }
        public int jfbbh { get; set; }
        public string ysgh { get; set; }
        public string ysxm { get; set; }
        public string hzxm { get; set; }
        public string dlmc { get; set; }
        public string dlcode { get; set; }
        public string sfxmmc { get; set; }
    }
}
