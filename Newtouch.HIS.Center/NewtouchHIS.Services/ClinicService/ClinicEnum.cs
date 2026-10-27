using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Services.ClinicService
{
    /// <summary>
    /// Từ điển phòng khám:Từ xaĐăng ký khám chữa bệnhTrạng thái
    /// (0:Chưa nộp, 1:Chờ xác nhận,2:Đang khám,3:Đã kết thúc,4:Đã phát thuốc,5:Đã trả lại)
    /// </summary>
    public enum EmunClinicRemoteTreatedStu
    {
        [Description("Chưa nộp")]
        wtj = 0,
        [Description("Chờ xác nhận")]
        dqr = 1,
        [Description("Đang khám")]
        jzz = 2,
        [Description("Đã kết thúc")]
        yjs = 3,
        [Description("Đã phát thuốc")]
        yfy = 4,
        [Description("Đã trả lại")]
        yth = 5,
    }
}
