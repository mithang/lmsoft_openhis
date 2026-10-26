using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Thông tin kháng sinh của thuốc
    /// </summary>
    [Table("xt_ypKss")]
    public class SysMedicineAntibioticInfoEntity : IEntity<SysMedicineAntibioticInfoEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Id phân loại cấp một
        /// </summary>
        public string kssLevel1TypeId { get; set; }

        /// <summary>
        /// Id phân loại cấp hai
        /// </summary>
        public string kssLevel2TypeId { get; set; }
        
        /// <summary>
        /// Cấp độ quyền (0 Thuốc sử dụng không hạn chế, 1 Thuốc sử dụng hạn chế, 2 Thuốc sử dụng đặc biệt)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// Phạm vi liều lượng (lớn hơn)
        /// </summary>
        public decimal? jlfwBegin { get; set; }

        /// <summary>
        /// Phạm vi liều lượng (nhỏ hơn)
        /// </summary>
        public decimal? jlfwEnd { get; set; }
        
        /// <summary>
        /// Phạm vi tần suất (lớn hơn)
        /// </summary>
        public decimal? pcfwBegin { get; set; }

        /// <summary>
        /// Phạm vi tần suất (nhỏ hơn)
        /// </summary>
        public decimal? pcfwEnd { get; set; }

        /// <summary>
        /// Giá trị DDD
        /// </summary>
        public decimal? DDDnum { get; set; }

        /// <summary>
        /// Đơn vị DDD
        /// </summary>
        public string DDDdw { get; set; }

        /// <summary>
        /// Hiệu giá
        /// </summary>
        public decimal? xj { get; set; }

        /// <summary>
        /// Bình rỗng (0 Không, 1 Có)
        /// </summary>
        public string kp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 1: Có hiệu lực 0: Không hiệu lực
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Đơn vị liều lượng kháng sinh
        /// </summary>
        public string kssjldw { get; set; }
    }
}
