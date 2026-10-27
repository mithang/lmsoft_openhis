using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtouch.CIS.Compat;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Phương phápvận hànhdònggiám sát
    /// </summary>
    public class PerformanceMonitoring 
    {
        /// <summary>
        /// ghi chéptiêu haokhi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="missingHandler"></param>
        /// <param name="request"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static void TimeConsuming<T>(Action<T> missingHandler, T request, string title, string message, Dictionary<string, string> tags = null)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();//  Bắt đầugiám sátxemMãvận hànhdòngThời gian
            missingHandler.Invoke(request);//Mãchínhthể
            stopwatch.Stop();//  Dừnggiám sátxem
            LogCore.Info(string.Format("Title:{0};\n message:{1} Phương pháptiêu haokhi-{2}", title, message, stopwatch.Elapsed.TotalMilliseconds), message, tags);//ghi chépngàychí
        }

        /// <summary>
        /// Mãvận hànhdòngThời gian
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="missingHandler"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static T Running<T>(Func<T> missingHandler, string title, string message, Dictionary<string, string> tags = null)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();//  Bắt đầugiám sátxemMãvận hànhdòngThời gian
            dynamic value = missingHandler.Invoke();//Mãchínhthể
            stopwatch.Stop();//  Dừnggiám sátxem
            LogCore.Info(string.Format("Title:{0};\n message:{1} Phương pháptiêu haokhi-{2}", title, message, stopwatch.Elapsed.TotalMilliseconds), message, tags);//ghi chépngàychí
            return value;
        }

        /// <summary>
        /// phụcGiám sát nghiệp vụ
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="action"></param>
        /// <param name="request"></param>
        /// <param name="requestTitle"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static TResponse RequestMonitor<TRequest, TResponse>(Func<TRequest, TResponse> action, TRequest request, string requestTitle, Dictionary<string, string> tags = null)
        {
            try
            {
                var stopwatch = new Stopwatch();
                LogCore.Info(requestTitle, string.Format("{0}_RequestXML;\n Message:{1}", requestTitle, request), tags);
                stopwatch.Start();
                var response = action.Invoke(request);
                stopwatch.Stop();
                LogCore.Info(requestTitle, string.Format("{0}_ResponseXML;\n Message:{1}", requestTitle, response));
                LogCore.Info(string.Format("Title:{0};Thời gian chạy phương thức-{1}", requestTitle, stopwatch.Elapsed.TotalMilliseconds), requestTitle, tags);
                return response;
            }
            catch (Exception ex)
            {
                LogCore.Error(requestTitle, ex, addInfo: tags);
                throw;
            }
        }

        /// <summary>
        /// phụcGiám sát nghiệp vụ
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="action"></param>
        /// <param name="request"></param>
        /// <param name="requestTitle"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static void RequestMonitor<TRequest>(Action<TRequest> action, TRequest request, string requestTitle, Dictionary<string, string> tags = null)
        {
            try
            {
                var stopwatch = new Stopwatch();
                LogCore.Info(requestTitle, string.Format("{0}_RequestXML;\n Message:{1}", requestTitle, request), tags);
                stopwatch.Start();
                action.Invoke(request);
                stopwatch.Stop();
                LogCore.Info(string.Format("Title:{0};Thời gian chạy phương thức-{1}", requestTitle, stopwatch.Elapsed.TotalMilliseconds), requestTitle, tags);
            }
            catch (Exception ex)
            {
                LogCore.Error(requestTitle, ex, addInfo: tags);
                throw;
            }
        }
    }
}