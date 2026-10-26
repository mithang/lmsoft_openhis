using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Hạng mục thu phí
    /// </summary>
    [Table("xt_sfxm")]
    [Obsolete("please use the view")]
    public class SysChargeItemEntity : IEntity<SysChargeItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sfxmbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fl { get; set; }

        /// <summary>
        /// Xác định tỷ lệ tiền mặt tự chi trả,Không liên quan đến tính chất của phần này(Tự chi trả theo phân loại,Tự lo)   Xem trườngzfxz      Chú ý:Khi tỷ lệ tự chi trả là số âm,Thể hiện mức tự chi trả cố định   (Ví dụ:Một loại vật tư nào đó12300,Được thanh toán10000,thì tỷ lệ tự chi trả＝-2300)
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// Xác định phần tự chi trả(Chi phí×Tỷ lệ tự chi trả)Tính chất của   0  Được thanh toán 1 Tự lo 2 Tự chi trả theo phân loại 3 Tự lo tuyệt đối(Xử lý chuyên gia nghỉ hưu,Phí đăng ký khám đặc biệt)      (Chi phí×(1－Tỷ lệ tự chi trả) Là phần được ghi sổ)
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0:Dùng chung,1:Khám ngoại trú,2:Nhập viện
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 0 Không cần thực hiện,(Ghi sổ trực tiếp nội trú)    1 Cần thực hiện,(Sau khi thực hiện nội trú mới được ghi sổ)   from xt_yjxmdz
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 0 Hạng mục thông thường 1 Hạng mục đặc biệt refer xt_sfxmtsbz   Giải thích hạng mục đặc biệt là chỉ tính chất bệnh nhân đặc biệt mới được dùng. 
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jsbz { get; set; }

        /// <summary>
        /// 0 Hạng mục không thu phí(Ví dụ:Phí đăng ký khám..) 1 Hạng mục thu phí
        /// </summary>
        public string sfbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlffbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wjdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string badl { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }

    }
}
