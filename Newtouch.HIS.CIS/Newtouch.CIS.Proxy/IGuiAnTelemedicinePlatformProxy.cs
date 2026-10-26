namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// Đại diện giao diện y tế từ xa Quý An
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public abstract class GuiAnTelemedicinePlatformProxy<T, TV> where T : class
    {
        protected string Url = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("GuiAnTelemedicinePlatform_his_01");
        protected string IntegratedModuleUrl = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("IntegratedModuleUrl");

        /// <summary>
        /// Thực thi
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public abstract TV Execute(T requestDto);


    }
}