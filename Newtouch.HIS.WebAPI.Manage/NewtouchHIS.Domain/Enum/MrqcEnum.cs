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
            [Description("新建病历")]
            xjbl = 0,
            [Description("修改病历")]
            xgbl = 1
        }
        public enum ApplyStatusEnum
        {
            /// <summary>
            /// chưa phê duyệt
            /// </summary>
            [Description("未批准")]
            wpz = 0,
            /// <summary>
            /// đã phê duyệt
            /// </summary>
            [Description("已审批")]
            ysp = 1,
            /// <summary>
            /// trả lại
            /// </summary>
            [Description("退回")]
            th = 2
        }
    }
}
