using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Services.ClinicService
{
    /// <summary>
    /// từ điển phòng khám：trạng thái đăng ký khám chữa bệnh từ xa
    /// （0:chưa nộp, 1:chờ xác nhận、2:đang khám、3:đã kết thúc、4:đã phát thuốc、5:đã trả lại）
    /// </summary>
    public enum EmunClinicRemoteTreatedStu
    {
        [Description("未提交")]
        wtj = 0,
        [Description("待确认")]
        dqr = 1,
        [Description("就诊中")]
        jzz = 2,
        [Description("已结束")]
        yjs = 3,
        [Description("已发药")]
        yfy = 4,
        [Description("已退回")]
        yth = 5,
    }
}
