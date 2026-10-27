namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class SysDepartmentVO
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Cấp chaId
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Cờ y kỹ thuật(Có phải khoa y kỹ thuật hay không,như khoa X-quang)
        /// </summary>
        public bool yjbz { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú
        /// </summary>
        public byte mzzybz { get; set; }

        /// <summary>
        /// Chữ cái đầu phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Cờ hiệu lực
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Khoa khám chữa bệnh
        /// </summary>
        public string zlks { get; set; }
    }

    public class HisDeptVO
    {
        public string? OrganizeId { get; set; }
        public string? ks { get; set; }
        public string? ksmc { get; set; }
        /// <summary>
        /// Cờ y kỹ thuật(Có phải khoa y kỹ thuật hay không,như khoa X-quang)
        /// </summary>
        public bool? yjbz { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú
        /// </summary>
        public byte? mzzybz { get; set; }

        /// <summary>
        /// Cờ hiệu lực
        /// </summary>
        public string? zt { get; set; }

    }
}
