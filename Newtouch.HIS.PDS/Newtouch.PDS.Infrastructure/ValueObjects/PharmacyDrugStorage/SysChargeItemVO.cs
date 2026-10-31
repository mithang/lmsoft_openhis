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
        /// Xác định tỷ lệ tiền mặt tự trả,Không liên quan tính chất phần này(Tự trả theo loại,Tự lo)   Xem trườngzfxz      Chú ý:Khi tỷ lệ tự trả là số âm,Biểu thị tự trả định mức   (tỷ lệ:Một vật tư nào đó12300,Có thể báo10000,vậy tỷ lệ tự trả=-2300)
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// Xác định phần tự trả(Phí×Tỷ lệ tự trả)tính chất của   0  Có thể báo 1 Tự lo 2 Tự trả theo loại 3 Tuyệt đối tự lo(Xử lý chuyên gia nghỉ hưu,Phí đăng ký đặc biệt)      (Phí×(1-Tỷ lệ tự trả) là phần có thể ghi sổ)
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0:Chung,1:Ngoại trú,2:Nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 0 Không cần thực hiện,(Nhập sổ trực tiếp nội trú)    1 Cần thực hiện,(Nội trú sau khi thực hiện mới được nhập sổ)   from xt_yjxmdz
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 0 Hạng mục thường 1 Hạng mục đặc biệt refer xt_sfxmtsbz   Hạng mục đặc biệt chỉ dùng cho tính chất bệnh nhân đặc biệt. 
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// 0 Hạng mục không thu phí(tỷ lệ:Phí đăng ký..) 1 Hạng mục thu phí
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
        /// Thời lượng liên tục(Đơn vị:phần)
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Số đo lường đơn vị
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
        /// Thời gian đồng bộ BHYT cuối
        /// </summary>
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// Cấp phẫu thuật
        /// </summary>
        public string ssdj { get; set; }

        public string sqlx { get; set; }
        /// <summary>
        /// Cờ BHYT1:BHYT;0:Tự trả
        /// </summary>
        public string ybbz { get; set; }
        /// <summary>
        /// Mã BHYT nông thôn mới
        /// </summary>
        public string xnhybdm { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia
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
