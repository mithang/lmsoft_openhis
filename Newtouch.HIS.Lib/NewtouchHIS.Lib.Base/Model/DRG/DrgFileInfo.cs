namespace NewtouchHIS.Lib.Base.Model.DRG
{
    public class DrgFileInfo
    {
        /// <summary>
        /// Tên tệp
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Đường dẫn tệp
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Loại tệp
        /// </summary>
        public string FileExtName { get; set; }
        /// <summary>
        /// Tên cột tệp
        /// </summary>
        public string[] FileCols { get; set; }
        public bool? IsTmpFile { get; set; } = true;
    }

    public class DrgAreaFileInfo : DrgFileInfo
    {
        /// <summary>
        /// Phiên bản bộ nhóm
        /// </summary>
        public string version { get; set; }
        public int? versionPolicy { get; set; }
    }


}
