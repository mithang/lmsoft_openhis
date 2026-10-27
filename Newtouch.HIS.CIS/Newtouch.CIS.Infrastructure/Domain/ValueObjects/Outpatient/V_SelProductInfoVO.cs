using System;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Danh sách thả xuốngvật tưthông tin
    /// </summary>
    public class VSelProductInfoVO
    {
        /// <summary>
        /// vật tưID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúc
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// vật tưTên
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Tồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// có thểdùngTồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// Khoa phòngĐơn vịSố lượng（mangĐơn vị）
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtGiá bán lẻ
        /// </summary>
        public decimal minlsj { get; set; }

        /// <summary>
        /// Khoa phòngĐơn vịGiá bán lẻ
        /// </summary>
        public decimal bmlsj { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Khoa phòngĐơn vịchuyểnhóavìcon
        /// </summary>
        public int bmdwzhyz { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// sinhNhà sản xuấtId
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// sinhNhà sản xuấtTên
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtID
        /// </summary>
        public string zxdwId { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtTên
        /// </summary>
        public string mindwmc { get; set; }

        /// <summary>
        /// Khoa phòngĐơn vịID
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// Khoa phòngTên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// LoạiID
        /// </summary>
        public string lbId { get; set; }
    }


    /// <summary>
    /// vật tưLôthông tin
    /// </summary>
    public class VProductBatchInfoVO
    {
        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Tồn khoSố lượng，Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// có thểdùngTồn kho（kcsl-djsl），Đơn vị nhỏ nhất
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// Đơn vịTồn kho，Khoa phòngĐơn vị
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public DateTime yxq { get; set; }

        /// <summary>
        /// sinhsản xuấtNgày
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// Khoa phòngĐơn vịvàogiá
        /// </summary>
        public decimal bmjj { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtvàogiá
        /// </summary>
        public decimal minjj { get; set; }

        /// <summary>
        /// vàogiátổngmức
        /// </summary>
        public decimal jjzje { get; set; }

        /// <summary>
        /// Hóa đơnsố
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// vàogiáĐơn vịĐơn giá
        /// </summary>
        public string jjdwdj { get; set; }
    }


    public class RelWarehouseVO
    {
        public string ID { get; set; }
        public string OrganizeId { get; set; }
        public string name { get; set; }
        public string py { get; set; }
        public string isDefSyn { get; set; }
    }
}
