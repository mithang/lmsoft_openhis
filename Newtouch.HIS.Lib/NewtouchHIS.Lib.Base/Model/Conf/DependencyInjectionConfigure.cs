namespace NewtouchHIS.Lib.Base.Model
{
    public class DependencyInjectionConfigure
    {
        /// <summary>
        /// Tên assembly dịch vụ
        /// </summary>
        public string ServiceAssemblyName { get; set; }

        /// <summary>
        /// Tên kết thúc dịch vụ
        /// </summary>
        public string ServiceEndsWith { get; set; }

        /// <summary>
        /// Tên assembly kho
        /// </summary>
        public string RepositoryAssemblyName { get; set; }

        /// <summary>
        /// Tên kết thúc kho
        /// </summary>
        public string RepositoryEndsWith { get; set; }
    }
}
