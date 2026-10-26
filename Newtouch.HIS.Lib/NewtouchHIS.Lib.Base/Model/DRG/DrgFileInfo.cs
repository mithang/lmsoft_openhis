namespace NewtouchHIS.Lib.Base.Model.DRG
{
    public class DrgFileInfo
    {
        /// <summary>
        /// tên tệp
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// đường dẫn tệp
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// kiểu tệp
        /// </summary>
        public string FileExtName { get; set; }
        /// <summary>
        /// tên cột tệp
        /// </summary>
        public string[] FileCols { get; set; }
        public bool? IsTmpFile { get; set; } = true;
    }

    public class DrgAreaFileInfo : DrgFileInfo
    {
        /// <summary>
        /// phiên bản bộ phân nhóm
        /// </summary>
        public string version { get; set; }
        public int? versionPolicy { get; set; }
    }


}
