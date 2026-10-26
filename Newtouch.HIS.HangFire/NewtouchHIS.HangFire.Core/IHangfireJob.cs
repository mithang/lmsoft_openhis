using Hangfire;
using Hangfire.HttpJob.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackgroundJob = Hangfire.HttpJob.Client.BackgroundJob;

namespace NewtouchHIS.HangFire.Core
{
    public interface IHangfireJob
    {
        Task<string> AddBackgroundJob(string serverUrl);
    }

    public class HangfireJob:IHangfireJob
    {
        public async Task<string> AddBackgroundJob(string serverUrl)
        {
            //dưới đây dùng phương thức đồng bộ，cũng có thể dùng bất đồng bộ： await HangfireJobClient.AddBackgroundJobAsync
            var result = await HangfireJobClient.AddBackgroundJobAsync(serverUrl, new BackgroundJob
            {
                JobName = "测试api",
                Method = "Get",
                Url = "http://localhost:20951/api/notice/getmsg/yy/hello",
                SendSuccess = true,
                DelayFromMinutes = 1 //ở đây là thực hiện sau bao nhiêu phút
                                     //RunAt = new DateTime(2020,7,25,10,5,1) // cũng có thể không cần chỉ định DelayFromMinutes tham số chỉ định trực tiếp thời điểm chạy。
            }, new HangfireServerPostOption
            {
                BasicUserName = "admin",//đây làhangfiređã thiết lậpbasicauth
                BasicPassword = "test"//đây làhangfiređã thiết lậpbasicauth
            });

            return result.JobId;
            //result.JobId chính là một lần nàyjobcủaid có thể thông qua cái nàyidxóa nó trước khi nó chạy
        }

        public async Task<bool> RemoveBackgroundJob(string serverUrl,string jobId)
        {
            var result = HangfireJobClient.RemoveBackgroundJob(serverUrl, jobId, new HangfireServerPostOption
            {
                BasicUserName = "admin",//đây làhangfiređã thiết lậpbasicauth
                BasicPassword = "test"//đây làhangfiređã thiết lậpbasicauth
            });
            return result.IsSuccess;
        }
    }

   
}
