using System;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Danh sách thả xuốngvật tưthông tin
    /// </summary>
    public class VSelProductInfoVO
    {
        /// <summary>
        /// vật tư ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúc
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// vật tư Tên
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Tồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// có thểdùng Tồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vị Số lượng(mang Đơn vị)
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất Giá bán lẻ
        /// </summary>
        public decimal minlsj { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vị Giá bán lẻ
        /// </summary>
        public decimal bmlsj { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vịchuyểnhóavìcon
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
        /// sinh Nhà sản xuất Id
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// sinh Nhà sản xuất Tên
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất ID
        /// </summary>
        public string zxdwId { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất Tên
        /// </summary>
        public string mindwmc { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vị ID
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// Khoa phòng Tên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Loại ID
        /// </summary>
        public string lbId { get; set; }
    }


    /// <summary>
    /// vật tư Lôthông tin
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
        /// Tồn kho Số lượng,Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// có thểdùng Tồn kho(kcsl-djsl),Đơn vị nhỏ nhất
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// Đơn vị Tồn kho,Khoa phòng Đơn vị
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public DateTime yxq { get; set; }

        /// <summary>
        /// sinhsản xuất Ngày
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vịvàogiá
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
        /// vàogiá Đơn vị Đơn giá
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
