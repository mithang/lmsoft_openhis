using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Enum
{
    public static class MrqcEnum
    {
        public enum ApplyTypeEnum
        {
            [Description("Lập bệnh án mới")]
            xjbl = 0,
            [Description("Sửa bệnh án")]
            xgbl = 1
        }
        public enum ApplyStatusEnum
        {
            /// <summary>
            /// Chưa phê duyệt
            /// </summary>
            [Description("Chưa phê duyệt")]
            wpz = 0,
            /// <summary>
            /// Đã phê duyệt
            /// </summary>
            [Description("Đã phê duyệt")]
            ysp = 1,
            /// <summary>
            /// Trả lại
            /// </summary>
            [Description("Trả lại")]
            th = 2
        }
    }
}
