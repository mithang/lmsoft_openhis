namespace NewtouchHIS.Lib.Services.Logger
{
    /// <summary>
    /// giao diện nhật ký
    /// </summary>
    public interface ILoggerHelper
    {
        /// <summary>
        /// thông tin gỡ lỗi
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Debug(object source, string message);
        /// <summary>
        /// thông tin gỡ lỗi
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="ps">ps</param>
        void Debug(object source, string message, params object[] ps);
        /// <summary>
        /// thông tin gỡ lỗi
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Debug(Type source, string message);
        /// <summary>
        /// thông tin quan trọng
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Info(object source, object message);
        /// <summary>
        /// thông tin quan trọng
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Info(Type source, object message);
        /// <summary>
        /// thông tin cảnh báo
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Warn(object source, object message);
        /// <summary>
        /// thông tin cảnh báo
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Warn(Type source, object message);
        /// <summary>
        /// thông tin lỗi
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Error(object source, object message);
        /// <summary>
        /// thông tin lỗi
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Error(Type source, object message);
        /// <summary>
        /// thông tin thất bại
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Fatal(object source, object message);
        /// <summary>
        /// thông tin thất bại
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Fatal(Type source, object message);

        /* Log a message object and exception */

        /// <summary>
        /// thông tin gỡ lỗi
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Debug(object source, object message, Exception exception);
        /// <summary>
        /// thông tin gỡ lỗi
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Debug(Type source, object message, Exception exception);
        /// <summary>
        /// thông tin quan trọng
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Info(object source, object message, Exception exception);
        /// <summary>
        /// thông tin quan trọng
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Info(Type source, object message, Exception exception);
        /// <summary>
        /// thông tin cảnh báo
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Warn(object source, object message, Exception exception);
        /// <summary>
        /// thông tin cảnh báo
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Warn(Type source, object message, Exception exception);
        /// <summary>
        /// thông tin lỗi
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Error(object source, object message, Exception exception);
        /// <summary>
        /// thông tin lỗi
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Error(Type source, object message, Exception exception);
        /// <summary>
        /// thông tin thất bại
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Fatal(object source, object message, Exception exception);
        /// <summary>
        /// thông tin thất bại
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Fatal(Type source, object message, Exception exception);
    }
}
