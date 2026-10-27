using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.Dapper
{
    /// <summary>
    /// thíchphối
    /// </summary>
    /// <typeparam name="TProxyRequest"></typeparam>
    /// <typeparam name="TProxyResponse"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ProxyDapperBase<TProxyRequest, TProxyResponse, TResult> : IProxyDapperExecutable
    {
        public TProxyRequest Request;
        public TProxyResponse Response;
        /// <summary>
        /// nhómxâyyêu cầubáovăn
        /// </summary>
        /// <returns></returns>
        public abstract void BuildProxyRequest();

        /// <summary>
        /// Thực hiệnthaylýgiao diện
        /// </summary>
        /// <returns></returns>
        public abstract void ExecuteProxy();

        /// <summary>
        /// nhắclấyQuay lạiDữ liệu
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