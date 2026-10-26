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
        /// phân loại cấp 1 Id
        /// </summary>
        public string kssLevel1TypeId { get; set; }

        /// <summary>
        /// phân loại cấp 2 Id
        /// </summary>
        public string kssLevel2TypeId { get; set; }

        /// <summary>
        /// cấp quyền(0 không hạn chế sử dụng thuốc,1 hạn chế sử dụng thuốc,2 đặc biệt sử dụng thuốc)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// liều dùng phạm vi(lớn hơn)
        /// </summary>
        public decimal? jlfwBegin { get; set; }

        /// <summary>
        /// liều dùng phạm vi(nhỏ hơn)
        /// </summary>
        public decimal? jlfwEnd { get; set; }

        /// <summary>
        /// tần suất phạm vi(lớn hơn)
        /// </summary>
        public decimal? pcfwBegin { get; set; }

        /// <summary>
        /// tần suất phạm vi(nhỏ hơn)
        /// </summary>
        public decimal? pcfwEnd { get; set; }

        /// <summary>
        /// DDD giá trị
        /// </summary>
        public decimal? DDDnum { get; set; }

        /// <summary>
        /// DDD đơn vị
        /// </summary>
        public string DDDdw { get; set; }

        /// <summary>
        /// hiệu giá
        /// </summary>
        public decimal? xj { get; set; }

        /// <summary>
        /// trống lọ(0 không,1 có)
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
        /// 1: còn hiệu lực 0: không hiệu lực
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// kháng sinh đơn vị liều dùng
        /// </summary>
        public string kssjldw { get; set; }
    }
}
