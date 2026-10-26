using System;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin vật tư trong danh sách thả xuống
    /// </summary>
    public class VSelProductInfoVO
    {
        /// <summary>
        /// Vật tưID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Tồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Tồn kho khả dụng Đơn vị nhỏ nhất
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// Số lượng đơn vị khoa phòng(Kèm đơn vị)
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal minlsj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị khoa phòng
        /// </summary>
        public decimal bmlsj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi đơn vị khoa phòng
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
        /// Nhà sản xuấtId
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// Tên nhà sản xuất
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtID
        /// </summary>
        public string zxdwId { get; set; }

        /// <summary>
        /// Tên đơn vị nhỏ nhất
        /// </summary>
        public string mindwmc { get; set; }

        /// <summary>
        /// Đơn vị khoa phòngID
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// Tên đơn vị khoa phòng
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
    /// Thông tin lô vật tư
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
        /// Số lượng tồn kho,Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Tồn kho khả dụng(kcsl-djsl),Đơn vị nhỏ nhất
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// Tồn kho đơn vị,Đơn vị khoa phòng
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime yxq { get; set; }

        /// <summary>
        /// Ngày sản xuất
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// Giá nhập đơn vị khoa phòng
        /// </summary>
        public decimal bmjj { get; set; }

        /// <summary>
        /// Giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal minjj { get; set; }

        /// <summary>
        /// Tổng số tiền giá nhập
        /// </summary>
        public decimal jjzje { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Đơn giá đơn vị giá nhập
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
