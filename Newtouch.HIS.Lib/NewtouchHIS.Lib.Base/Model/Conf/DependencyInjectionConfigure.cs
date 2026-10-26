namespace NewtouchHIS.Lib.Base.Model
{
    public class DependencyInjectionConfigure
    {
        /// <summary>
        /// tên assembly tầng dịch vụ
        /// </summary>
        public string ServiceAssemblyName { get; set; }

        /// <summary>
        /// tên kết thúc của dịch vụ
        /// </summary>
        public string ServiceEndsWith { get; set; }

        /// <summary>
        /// tên assembly tầng kho
        /// </summary>
        public string RepositoryAssemblyName { get; set; }

        /// <summary>
        /// tên kết thúc của kho
        /// </summary>
        public string RepositoryEndsWith { get; set; }
    }
}
