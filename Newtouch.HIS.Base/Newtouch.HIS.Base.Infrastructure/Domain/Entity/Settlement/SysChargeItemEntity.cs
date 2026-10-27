using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_sfxm")]
    public class SysChargeItemEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
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
        /// Xác định tỷ lệ tiền mặt tự chi trả，Không liên quan tính chất của phần này（Tự trả theo loại、Tự chi trả）   xem trườngzfxz      Chú ý：Khi tỷ lệ tự chi trả là số âm，thể hiện tự chi trả định mức   (VD：Một vật tư nào đó12300，Được thanh toán10000,thì tỷ lệ tự chi trả＝-2300)
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// Xác định phần tự chi trả（Chi phí×Tỷ lệ tự trả）tính chất của   0  Được thanh toán 1 Tự chi trả 2 Tự trả theo loại 3 Tự túc hoàn toàn（Xử lý chuyên gia nghỉ hưu、Phí đăng ký khám đặc biệt）      （Chi phí×（1－Tỷ lệ tự trả） Cho phần có thể ghi sổ）
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0：Chung，1：Ngoại trú，2：Nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 0 Không cần thực hiện，（Nội trú ghi sổ trực tiếp）    1 Cần thực hiện，（Nội trú sau khi thực hiện mới được ghi sổ）   from xt_yjxmdz
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 0 Hạng mục thông thường 1 Hạng mục đặc biệt refer xt_sfxmtsbz   Giải thích hạng mục đặc biệt chỉ nhóm bệnh nhân có tính chất nhất định mới được dùng。 
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// 0 Hạng mục không thu phí（VD：Phí đăng ký khám..） 1 Hạng mục thu phí
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
        /// Thời lượng duy trì（Đơn vị：phút）
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Số lượng đơn vị tính
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
        /// Thời gian đồng bộ BHYT cuối cùng
        /// </summary>
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// Cấp phẫu thuật
        /// </summary>
        public string ssdj { get; set; }

        public string sqlx { get; set; }
        /// <summary>
        /// Cờ BHYT1：BHYT；0：Tự trả
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
        /// Số tiền vượt giá trần
        /// </summary>
        public decimal? cxjje { get; set; }
        public string pzwh { get; set; }
        public string sccj { get; set; }
        public string gjybmc { get; set; }
		public string iswzsame { get; set; }

	}
}
