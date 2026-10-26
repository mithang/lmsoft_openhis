using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_brsfsf")]
    public class SysPatientChargeAlgorithmEntity : IEntity<SysPatientChargeAlgorithmEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brsfsfbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// *Đại diện cho tất cả các loại lớn thu phí (dl)
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte sfjb { get; set; }

        /// <summary>
        /// Chi phí trong phạm vi nào tham gia tính toán tỷ lệ tự chi trả      0 Phần được ghi sổ tham gia tính toán(Tạm thời không có)   1 Được ghi sổ＋Tự chi trả theo phân loại(Nghỉ hưu,Từ thiện v.v. đa số……)   2 Được ghi sổ＋Tự chi trả theo phân loại＋Tự lo－Tự lo tuyệt đối(Phí đăng ký khám nghỉ hưu0.1khi,Tự lo tuyệt đối xử lý phần không được thanh toán như phí đăng ký khám đặc biệt nghỉ hưu－Xem hạng mục thu phí hệ thống.Tính chất tự chi trả)      "Cùng loại lớn"của"Tất cả các cấp"Phạm vi được thanh toán phải nhất quán
        /// </summary>
        public string fyfw { get; set; }

        /// <summary>
        /// Xác định ghi sổ,Tỷ lệ tự chi trả,Bất kể BHYT có thanh toán hay không(Xem cờ tự chi trả)   Khi tỷ lệ là số âm,Thể hiện mức tự chi trả cố định,Không còn tính theo tỷ lệ nữa.Ví dụ(Thanh toán định mức phí đăng ký khám nghỉ hưu)
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// Xác định tính chất của phần tự chi trả tính được theo tỷ lệ tự chi trả   0 Tự chi trả(Tiền mặt tự chi trả sau giao dịch BHYT)   1 Tự lo
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0 Tất cả chi phí đều dùng tỷ lệ này,Không xét giới hạn trên
        /// </summary>
        public decimal fysx { get; set; }

        /// <summary>
        /// 0:Dùng chung,1:Khám ngoại trú,2:Nhập viện
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

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
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
