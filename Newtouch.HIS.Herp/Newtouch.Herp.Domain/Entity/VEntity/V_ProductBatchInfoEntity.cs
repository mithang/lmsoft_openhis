using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin lô vật tư
    /// </summary>
    public class VProductBatchInfoEntity
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
        /// Tổng tiền nhập
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
}
