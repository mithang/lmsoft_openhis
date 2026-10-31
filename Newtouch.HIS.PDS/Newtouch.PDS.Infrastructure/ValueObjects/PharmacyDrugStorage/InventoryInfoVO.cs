using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin kiểm kê
    /// </summary>
    public class InventoryInfoVO
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Tên hãng thuốc=cd
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Mã loại lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Chi tiết kiểm kêID
        /// </summary>
        public string pdmxId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public string yxq { get; set; }

        /// <summary>
        /// Số lượng lý thuyết
        /// </summary>
        public string llsl { get; set; }

        /// <summary>
        /// Số lượng thực tế
        /// </summary>
        public int sjsl { get; set; }

        /// <summary>
        /// Đơn vị bộ phận
        /// </summary>
        public string deptdw { get; set; }

        /// <summary>
        /// Số lượng kho thuốc
        /// </summary>
        public int yksl { get; set; }

        /// <summary>
        /// Giá bán buôn
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal? yklsj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Số thực tế bộ phận (Dùng kết hợp với số thực tế đơn vị nhỏ nhất)
        /// </summary>
        public int deptSjsl { get; set; }

        /// <summary>
        /// Số thực tế đơn vị nhỏ nhất (Dùng kết hợp với số thực tế bộ phận)
        /// </summary>
        public int minSjsl { get; set; }

        /// <summary>
        /// Đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// Số tiền bán buôn lý thuyết
        /// </summary>
        public decimal? llpfje { get; set; }

        /// <summary>
        /// Số tiền bán lẻ lý thuyết
        /// </summary>
        public decimal? lllsje { get; set; }

        /// <summary>
        /// Số tiền bán buôn thực tế
        /// </summary>
        public decimal? sjpfje { get; set; }

        /// <summary>
        /// Số tiền bán lẻ thực tế
        /// </summary>
        public decimal? sjlsje { get; set; }

        /// <summary>
        /// Thời gian kiểm kê
        /// </summary>
        public string pdsj { get; set; }

        /// <summary>
        /// Số thứ tự sổ sách
        /// </summary>
        public string zcxh { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Số lượng đóng gói tối thiểu lý thuyết
        /// </summary>
        public string llsl_zxbz { get; set; }

        /// <summary>
        /// Số lượng đóng gói thực tế
        /// </summary>
        public string sjslstr { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Số tiền chênh lệch bán lẻ
        /// </summary>
        public decimal? pdlsjcy { get; set; }
        /// <summary>
        /// Số tiền chênh lệch bán buôn
        /// </summary>
        public decimal? pdpfjcy { get; set; }
        /// <summary>
        /// Chênh lệch số kiểm kê = Số thực tế-Số lý thuyết
        /// </summary>
        public string pdscy { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// Lãi lỗ Số lượng = Số thực tế - Số lý thuyết
        /// </summary>
        //public int? slyk { get; set; }
        /// <summary>
        /// Số tiền kiểm kê thực tế = Số thực tế * Số tiền bán lẻ(thực)
        /// </summary>
        public decimal? spje { get; set; }
        /// <summary>
        /// Số tiền trước kiểm kê = Số lý thuyết * Số tiền bán lẻ(lý)
        /// </summary>
        public decimal? pqje { get; set; }
        /// <summary>
        /// Số tiền lãi lỗ = Số tiền kiểm kê thực tế - Số tiền trước kiểm kê
        /// </summary>
        public decimal? ykje { get; set; }

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1: Có
        /// 2: Không
        /// </summary>
        public int? sfcl { get; set; }

    }
}
