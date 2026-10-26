using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysChargeItemVO
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// Xác định tỷ lệ tiền mặt tự chịu, không liên quan đến tính chất của phần này (tự chi trả theo loại, tự lo). Xem trường zfxz. Chú ý: khi tỷ lệ tự chi trả là số âm, biểu thị tự chịu theo định mức (ví dụ: một vật tư 12300, được báo 10000, vậy tỷ lệ tự chịu = -2300)
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// Xác định tính chất của phần tự chịu (chi phí × tỷ lệ tự chi trả): 0 được báo, 1 tự lo, 2 tự chi trả theo loại, 3 hoàn toàn tự lo (xử lý chuyên gia nghỉ hưu, phí đăng ký khám đặc biệt). (chi phí × (1 - tỷ lệ tự chi trả) là phần có thể ghi sổ)
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0: chung, 1: ngoại trú, 2: nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 0 không cần thực hiện, (nội trú ghi sổ trực tiếp); 1 cần thực hiện, (nội trú sau khi thực hiện mới được ghi sổ) from xt_yjxmdz
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 0 hạng mục thường 1 đặc biệt hạng mục refer xt_sfxmtsbz   đặc biệt hạng mục mô tả có đặc thù của tính chất bệnh nhân mới có thể sử dụng của. 
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// 0 không thu phí hạng mục (ví dụ: phí đăng ký khám..), 1 thu phí hạng mục
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
        /// thời lượng liên tục (đơn vị: phần)
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Số lượng đo đơn vị
        /// </summary>
        public int? dwjls { get; set; }

        /// <summary>
        /// Chiến lược định giá
        /// </summary>
        public int? jjcl { get; set; }

        /// <summary>
        /// mặc định thực hiện khoa
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }
        /// <summary>
        /// BHYT cuối cùng đồng bộ thời gian
        /// </summary>
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// Cấp độ phẫu thuật
        /// </summary>
        public string ssdj { get; set; }

        public string sqlx { get; set; }
        /// <summary>
        /// cờ BHYT 1: BHYT; 0: tự chi trả
        /// </summary>
        public string ybbz { get; set; }
        /// <summary>
        /// mã BHYT nông thôn mới
        /// </summary>
        public string xnhybdm { get; set; }
        /// <summary>
        /// mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
        /// <summary>
        /// vượt giá giới hạn số tiền
        /// </summary>
        public decimal? cxjje { get; set; }
        public string pzwh { get; set; }
        public string sccj { get; set; }
        public string gjybmc { get; set; }
        public string iswzsame { get; set; }
    }
}
