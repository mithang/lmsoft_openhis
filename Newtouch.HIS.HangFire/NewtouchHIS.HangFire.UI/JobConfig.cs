using Hangfire;
using Hangfire.Dashboard.Management.Metadata;
using Hangfire.Server;
using System.ComponentModel;

namespace NewtouchHIS.HangFire.UI
{
    [ManagementPage("任务管理")]
    public class JobConfig
    {
        [Hangfire.Dashboard.Management.Support.Job] //bắt buộc phải khai báo thuộc tính này 

        [DisplayName("任务标题")]

        [Description("任务描述")]

        [AutomaticRetry(Attempts = 0)] //số lần thử lại

        [DisableConcurrentExecution(90)]//vô hiệu hóa thực hiện song song

        public string Test(PerformContext context, IJobCancellationToken token,

           [DisplayData("Output Text", "Enter text to output.")] string outputText,

           [DisplayData("Bool类型参数", "Bool展示名称")] bool repeat,

           [DisplayData("Test Date", "Enter date")] DateTime testDate)

        {

            return (testDate + ":" + outputText);

        }
    }
}
