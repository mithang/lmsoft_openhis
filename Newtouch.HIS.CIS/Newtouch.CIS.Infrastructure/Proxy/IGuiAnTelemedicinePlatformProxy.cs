using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// đắtanxatrìnhy tếgiao diệnthaylý
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public abstract class GuiAnTelemedicinePlatformProxy<T, TV> where T : class
    {
        protected string Url = Newtouch.CIS.Compat.AppConfig.Get("GuiAnTelemedicinePlatform_his_01");
        protected string IntegratedModuleUrl = Newtouch.CIS.Compat.AppConfig.Get("IntegratedModuleUrl");

        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public abstract TV Execute(T requestDto);

    }
}