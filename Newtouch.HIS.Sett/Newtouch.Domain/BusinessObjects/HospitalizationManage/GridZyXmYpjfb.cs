namespace Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage
{
    /// <summary>
    /// Danh sách hạng mục tính phí nội trú và thuốc
    /// </summary>
    public class GridZyXmYpjfb
    {
        public string ZYH { get; set; }
        //public string XSEBH { get; set; }

        public string SFXM { get; set; }
        public string IS_SFXM { get; set; }
        public string CreateTime { get; set; }
        public string SFXMMC { get; set; }
        public string JFDW { get; set; }
        public string SL { get; set; }
        public string DJ { get; set; }
        


    }

    /// <summary>
    /// Chi tiết hạng mục tính phí nội trú và thuốc
    /// </summary>
    public class gridZyXmYpItemsjfb
    {
        public string JFBBH { get; set; }
        public string CXZYJFBBH { get; set; }
        public string CreateTime { get; set; }
        /// <summary>
        /// Số hiệu
        /// </summary>
        public string SFXMCode { get; set; }
        /// <summary>
        /// Tên hạng mục hoặc thuốc
        /// </summary>
        public string SFXMMC { get; set; }
        public string JFDW { get; set; }
        public string SL { get; set; }
        public string RETURN_SL { get; set; }



    }
}
