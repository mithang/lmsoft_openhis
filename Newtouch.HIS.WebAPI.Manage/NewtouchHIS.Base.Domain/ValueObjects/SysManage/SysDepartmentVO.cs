namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class SysDepartmentVO
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// cấp chaId
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// cờ kỹ thuật y tế（có phải là khoa kỹ thuật y tế hay không，như khoa X-quang）
        /// </summary>
        public bool yjbz { get; set; }

        /// <summary>
        /// cờ ngoại trú – nội trú
        /// </summary>
        public byte mzzybz { get; set; }

        /// <summary>
        /// chữ cái đầu của phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// cờ hiệu lực
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// khoa khám chữa bệnh
        /// </summary>
        public string zlks { get; set; }
    }

    public class HisDeptVO
    {
        public string? OrganizeId { get; set; }
        public string? ks { get; set; }
        public string? ksmc { get; set; }
        /// <summary>
        /// cờ kỹ thuật y tế（có phải là khoa kỹ thuật y tế hay không，như khoa X-quang）
        /// </summary>
        public bool? yjbz { get; set; }

        /// <summary>
        /// cờ ngoại trú – nội trú
        /// </summary>
        public byte? mzzybz { get; set; }

        /// <summary>
        /// cờ hiệu lực
        /// </summary>
        public string? zt { get; set; }

    }
}
