using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Điều chỉnh giá hạng mục thu phí hệ thống
    /// </summary>
    [Table("xt_sfxmtj")]
    public class SysChargeItemPriceAdjustEntity : IEntity<SysChargeItemPriceAdjustEntity>
    {
        /// <summary>
        /// Mã điều chỉnh giá
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tjbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Hạng mục thu phí
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// Tên hạng mục thu phí
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Tỷ lệ tự chi trả
        /// Xác định tỷ lệ tiền mặt tự chi trả,Không liên quan đến tính chất của phần này(Tự chi trả theo phân loại,Tự lo)   Xem trườngzfxz      Chú ý:Khi tỷ lệ tự chi trả là số âm,Thể hiện mức tự chi trả cố định   (Ví dụ:Một loại vật tư nào đó12300,Được thanh toán10000,thì tỷ lệ tự chi trả＝-2300)
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// Tính chất tự chi trả
        /// Xác định phần tự chi trả(Chi phí×Tỷ lệ tự chi trả)Tính chất của   1 Tự lo 2 Tự chi trả theo phân loại 3 Tự lo tuyệt đối(Xử lý chuyên gia nghỉ hưu,Phí đăng ký khám đặc biệt)      (Chi phí×(1－Tỷ lệ tự chi trả) Là phần được ghi sổ)
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// Trạng thái
        /// 0:Không hiệu lực  1:Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Cờ thực hiện
        /// 0:Chưa thực hiện  1:Đã thực hiện
        /// </summary>
        public string zxbz { get; set; }

        /// <summary>
        /// Ngày thực hiện
        /// </summary>
        public DateTime zxrq { get; set; }

        /// <summary>
        /// Ngày có hiệu lực tại điểm chỉ định
        /// </summary>
        public DateTime ddsxrq { get; set; }

        /// <summary>
        /// Cờ có hiệu lực
        /// 0:Có hiệu lực ngay  1:Có hiệu lực tại điểm chỉ định
        /// </summary>
        public string sxbz { get; set; }

        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

    }
}
