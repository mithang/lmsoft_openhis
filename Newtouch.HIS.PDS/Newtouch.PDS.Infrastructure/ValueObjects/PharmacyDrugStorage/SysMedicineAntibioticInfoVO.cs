using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysMedicineAntibioticInfoVO : IEntity<SysMedicineAntibioticInfoVO>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Phân loại cấp 1Id
        /// </summary>
        public string kssLevel1TypeId { get; set; }

        /// <summary>
        /// Phân loại cấp 2Id
        /// </summary>
        public string kssLevel2TypeId { get; set; }

        /// <summary>
        /// Cấp quyền(0 Thuốc không hạn chế sử dụng,1 Thuốc hạn chế sử dụng,2 Thuốc sử dụng đặc biệt)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// Phạm vi liều lượng(lớn hơn)
        /// </summary>
        public decimal? jlfwBegin { get; set; }

        /// <summary>
        /// Phạm vi liều lượng(nhỏ hơn)
        /// </summary>
        public decimal? jlfwEnd { get; set; }

        /// <summary>
        /// Phạm vi tần suất(lớn hơn)
        /// </summary>
        public decimal? pcfwBegin { get; set; }

        /// <summary>
        /// Phạm vi tần suất(nhỏ hơn)
        /// </summary>
        public decimal? pcfwEnd { get; set; }

        /// <summary>
        /// DDDgiá trị
        /// </summary>
        public decimal? DDDnum { get; set; }

        /// <summary>
        /// DDDĐơn vị
        /// </summary>
        public string DDDdw { get; set; }

        /// <summary>
        /// Hiệu giá
        /// </summary>
        public decimal? xj { get; set; }

        /// <summary>
        /// Bình rỗng(0 Không,1 Có)
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
        /// 1：Hiệu lực 0：Không hiệu lực
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Đơn vị liều lượng kháng sinh
        /// </summary>
        public string kssjldw { get; set; }
    }
}
