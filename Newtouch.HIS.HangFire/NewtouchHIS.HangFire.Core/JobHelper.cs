using Hangfire.HttpJob.Client;
using System.Net;

namespace NewtouchHIS.HangFire.Core
{
    public static class JobHelper
    {
        public async static Task<string> AddBackgroundJob(string serviceUrl)
        {
            //dưới đây dùng phương thức đồng bộ，cũng có thể dùng bất đồng bộ： await HangfireJobClient.AddBackgroundJobAsync
            var result = await HangfireJobClient.AddBackgroundJobAsync(serviceUrl, new BackgroundJob
            {
                JobName = "测试api",
                Method = "Get",
                Url = "http://localhost:20951/api/notice/getmsg/yy/hello",
                SendSuccess = true,
                DelayFromMinutes = 1, //ở đây là thực hiện sau bao nhiêu phút
                QueueName = "default"
                //RunAt = new DateTime(2020,7,25,10,5,1) // cũng có thể không cần chỉ định DelayFromMinutes tham số chỉ định trực tiếp thời điểm chạy。
            }, new HangfireServerPostOption
            {
                BasicUserName = "admin",//đây làhangfiređã thiết lậpbasicauth
                BasicPassword = "test"//đây làhangfiređã thiết lậpbasicauth
            });

            return result.JobId;
            //result.JobId chính là một lần nàyjobcủaid có thể thông qua cái nàyidxóa nó trước khi nó chạy
        }
        public async static Task<HttpJobResponse> AddBackgroundJobPost(HttpJobRequest request)
        {
            var backJobRequest = new BackgroundJob
            {
                JobName = request.JobName,
                Method = "Post",
                Url = request.ApiUrl,
                Data = request.PostData,
                SendSuccess = true,
                DelayFromMinutes = request.DelayFromMinutes ?? 10, //ở đây là thực hiện sau bao nhiêu phút
                QueueName = request.QueueName ?? "default",
                //RunAt = new DateTime(2020,7,25,10,5,1) // cũng có thể không cần chỉ định DelayFromMinutes tham số chỉ định trực tiếp thời điểm chạy。
            };
            //gọi lại khi thành công
            if (request.SuccessCall != null && !string.IsNullOrWhiteSpace(request.SuccessCall.Url))
            {
                backJobRequest.Success = request.SuccessCall;
            }
            //gọi lại khi thất bại
            if (request.FailedCall != null && !string.IsNullOrWhiteSpace(request.FailedCall.Url))
            {
                backJobRequest.Fail = request.FailedCall;
            }
            //dưới đây dùng phương thức đồng bộ，cũng có thể dùng bất đồng bộ： await HangfireJobClient.AddBackgroundJobAsync
            var result = await HangfireJobClient.AddBackgroundJobAsync(request.serviceUrl, backJobRequest, new HangfireServerPostOption
            {
                BasicUserName = "admin",//đây làhangfiređã thiết lậpbasicauth
                BasicPassword = "test"//đây làhangfiređã thiết lậpbasicauth
            });

            return new HttpJobResponse
            {
                ErrMessage = result.ErrMessage,
                JobId = result.JobId,
                IsSuccess = result.IsSuccess
            };
            //result.JobId chính là một lần nàyjobcủaid có thể thông qua cái nàyidxóa nó trước khi nó chạy
        }

        public async static Task<HttpJobResponse> AddRecurringJobPost(HttpJobRequest request)
        {
            var result = HangfireJobClient.AddRecurringJob(request.ApiUrl, new RecurringJob()
            {
                JobName = request.JobName,
                Method = "Post",
                Data = request.PostData,
                Url = request.ApiUrl,
                SendSuccess = false,
                QueueName = request.QueueName ?? "recurring",
                Cron = request.Cron,
            }, new HangfireServerPostOption
            {
                BasicUserName = "admin",
                BasicPassword = "test"
            });
            return new HttpJobResponse
            {
                ErrMessage = result.ErrMessage,
                IsSuccess = result.IsSuccess
            };
        }
    }
}
