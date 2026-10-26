namespace Newtouch.CIS.Proxy.Dapper
{
    /// <summary>
    /// Tương thích
    /// </summary>
    /// <typeparam name="TProxyRequest"></typeparam>
    /// <typeparam name="TProxyResponse"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ProxyDapperBase<TProxyRequest, TProxyResponse, TResult> : IProxyDapperExecutable
    {
        public TProxyRequest Request;
        public TProxyResponse Response;
        /// <summary>
        /// Xây dựng bản tin yêu cầu
        /// </summary>
        /// <returns></returns>
        public abstract void BuildProxyRequest();

        /// <summary>
        /// Thực thi giao diện đại diện
        /// </summary>
        /// <returns></returns>
        public abstract void ExecuteProxy();

        /// <summary>
        /// Trích xuất dữ liệu trả về
        /// </summary>
        /// <returns></returns>
        public abstract TResult ExtractProxyResponse();

        /// <summary>
        /// Thao tác
        /// </summary>
        /// <returns></returns>
        public dynamic Execute()
        {
            BuildProxyRequest();
            ExecuteProxy();
            return ExtractProxyResponse();
        }
    }
}