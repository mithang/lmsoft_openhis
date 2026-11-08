using System;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Danh sách thả xuống vật tư thông tin
    /// </summary>
    public class VSelProductInfoVO
    {
        /// <summary>
        /// vật tư ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chức kết cấu trúc
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
        /// có thể dùng Tồn kho Đơn vị nhỏ nhất
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
        /// chuyển hóa vì con
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vị chuyển hóa vì con
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
    /// vật tư Lô thông tin
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
        /// có thể dùng Tồn kho(kcsl-djsl),Đơn vị nhỏ nhất
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
        /// sinh sản xuất Ngày
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vị vào giá
        /// </summary>
        public decimal bmjj { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất vào giá
        /// </summary>
        public decimal minjj { get; set; }

        /// <summary>
        /// vào giá tổng mức
        /// </summary>
        public decimal jjzje { get; set; }

        /// <summary>
        /// Hóa đơn số
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// vào giá Đơn vị Đơn giá
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
