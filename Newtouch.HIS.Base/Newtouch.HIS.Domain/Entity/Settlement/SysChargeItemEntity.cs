using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_sfxm")]
    public class SysChargeItemEntity : IEntity<SysChargeItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int sfxmId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string badlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

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
        public string flCode { get; set; }

        /// <summary>
        /// Xác định tỷ lệ tự trả bằng tiền mặt, không liên quan đến tính chất của phần này (tự trả theo phân loại, tự trả)   Xem trường zfxz      Lưu ý: Khi tỷ lệ tự trả là số âm, nghĩa là tự trả định mức   (Ví dụ: một vật liệu 12300, có thể báo 10000, vậy tỷ lệ tự trả = -2300)
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// Xác định tính chất của phần tự trả (chi phí × tỷ lệ tự trả)   0 Có thể báo 1 Tự trả 2 Tự trả theo phân loại 3 Tự trả tuyệt đối (xử lý phí đăng ký khám chuyên gia nghỉ hưu, khám theo yêu cầu)      (Chi phí × (1 - tỷ lệ tự trả) là phần có thể ghi sổ)
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0: Chung, 1: Ngoại trú, 2: Nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 0 Không cần thực hiện (nội trú ghi sổ trực tiếp)    1 Cần thực hiện (nội trú chỉ ghi sổ sau khi thực hiện)   from xt_yjxmdz
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 0 Hạng mục thông thường 1 Hạng mục đặc biệt refer xt_sfxmtsbz   Hạng mục đặc biệt chỉ được sử dụng cho tính chất bệnh nhân cụ thể. 
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// 0 Hạng mục không thu phí (ví dụ: phí đăng ký khám..) 1 Hạng mục thu phí
        /// </summary>
        public string sfbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wjdm { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// Thời lượng kéo dài (đơn vị: phút)
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Số lượng đo theo đơn vị
        /// </summary>
        public int? dwjls { get; set; }

        /// <summary>
        /// Chiến lược tính giá
        /// </summary>
        public int? jjcl { get; set; }

        /// <summary>
        /// Khoa thực hiện mặc định
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }
        /// <summary>
        /// Thời gian đồng bộ bảo hiểm y tế cuối cùng
        /// </summary>
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// Cấp độ phẫu thuật
        /// </summary>
        public string ssdj { get; set; }

        public string sqlx { get; set; }
        /// <summary>
        /// Cờ bảo hiểm y tế 1: Bảo hiểm y tế; 0: Tự trả
        /// </summary>
        public string ybbz { get; set; }
        /// <summary>
        /// Mã bảo hiểm y tế nông thôn mới
        /// </summary>
        public string xnhybdm { get; set; }
        /// <summary>
        /// Mã bảo hiểm y tế quốc gia
        /// </summary>
        public string gjybdm { get; set; }
        /// <summary>
        /// Số tiền vượt giá giới hạn
        /// </summary>
        public decimal? cxjje { get; set; }
        public string pzwh { get; set; }
        public string sccj { get; set; }
        public string gjybmc { get; set; }
		public string iswzsame { get; set; }

	}
}
