using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// 贵安远程医疗接口代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public abstract class GuiAnTelemedicinePlatformProxy<T, TV> where T : class
    {
        protected string Url = Newtouch.CIS.Compat.AppConfig.Get("GuiAnTelemedicinePlatform_his_01");
        protected string IntegratedModuleUrl = Newtouch.CIS.Compat.AppConfig.Get("IntegratedModuleUrl");

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public abstract TV Execute(T requestDto);

    }
}