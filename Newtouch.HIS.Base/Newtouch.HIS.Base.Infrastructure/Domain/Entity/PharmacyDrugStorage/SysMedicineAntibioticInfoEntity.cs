using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Thông tin kháng sinh của thuốc
    /// </summary>
    [SugarTable("xt_ypKss")]
    public class SysMedicineAntibioticInfoEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
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
        /// Cấp quyền(0 Thuốc dùng không hạn chế,1 Thuốc dùng hạn chế,2 Thuốc sử dụng đặc biệt)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// Phạm vi liều lượng(Lớn hơn)
        /// </summary>
        public decimal? jlfwBegin { get; set; }

        /// <summary>
        /// Phạm vi liều lượng(Nhỏ hơn)
        /// </summary>
        public decimal? jlfwEnd { get; set; }
        
        /// <summary>
        /// Phạm vi tần suất(Lớn hơn)
        /// </summary>
        public decimal? pcfwBegin { get; set; }

        /// <summary>
        /// Phạm vi tần suất(Nhỏ hơn)
        /// </summary>
        public decimal? pcfwEnd { get; set; }

        /// <summary>
        /// DDDGiá trị
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
        /// 1:Hiệu lực 0:Vô hiệu
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Đơn vị liều kháng sinh
        /// </summary>
        public string kssjldw { get; set; }
    }
}
