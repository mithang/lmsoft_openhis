using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// kiểm kê thông tin
    /// </summary>
    public class InventoryInfoVO
    {
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// tên nhà máy dược=cd
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// nhóm lớn mã
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// chi tiết kiểm kê ID
        /// </summary>
        public string pdmxId { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// tần suất
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public string yxq { get; set; }

        /// <summary>
        /// số lượng lý thuyết lượng
        /// </summary>
        public string llsl { get; set; }

        /// <summary>
        /// số lượng thực tế
        /// </summary>
        public int sjsl { get; set; }

        /// <summary>
        /// đơn vị bộ phận
        /// </summary>
        public string deptdw { get; set; }

        /// <summary>
        /// kho thuốc số lượng
        /// </summary>
        public int yksl { get; set; }

        /// <summary>
        /// giá bán buôn
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        public decimal? yklsj { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// cờ ngoại trú/nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// bộ phận số thực tế (sử dụng kết hợp với số thực tế đơn vị nhỏ nhất)
        /// </summary>
        public int deptSjsl { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất số thực tế (sử dụng kết hợp với số thực tế của bộ phận)
        /// </summary>
        public int minSjsl { get; set; }

        /// <summary>
        /// đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// tiền bán buôn lý thuyết
        /// </summary>
        public decimal? llpfje { get; set; }

        /// <summary>
        /// tiền bán lẻ lý thuyết
        /// </summary>
        public decimal? lllsje { get; set; }

        /// <summary>
        /// tiền bán buôn thực tế
        /// </summary>
        public decimal? sjpfje { get; set; }

        /// <summary>
        /// tiền bán lẻ thực tế
        /// </summary>
        public decimal? sjlsje { get; set; }

        /// <summary>
        /// thời gian kiểm kê
        /// </summary>
        public string pdsj { get; set; }

        /// <summary>
        /// số thứ tự sổ sách
        /// </summary>
        public string zcxh { get; set; }

        /// <summary>
        /// trạng thái
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
        /// thời gian tạo
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
        /// Chênh lệch số kiểm kê = Số thực tế - số lượng lý thuyết
        /// </summary>
        public string pdscy { get; set; }

        /// <summary>
        /// giá nhập
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// số lượng lãi/lỗ = số thực tế - số lượng lý thuyết
        /// </summary>
        //public int? slyk { get; set; }
        /// <summary>
        /// kiểm kê thực tế số tiền = số thực tế * tiền bán lẻ(thực)
        /// </summary>
        public decimal? spje { get; set; }
        /// <summary>
        /// trước kiểm kê số tiền = số lượng lý thuyết * tiền bán lẻ(lý thuyết)
        /// </summary>
        public decimal? pqje { get; set; }
        /// <summary>
        /// lãi/lỗ số tiền = kiểm kê thực tế số tiền - trước kiểm kê số tiền
        /// </summary>
        public decimal? ykje { get; set; }

        /// <summary>
        /// mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// có tách lẻ không
        /// 1: có
        /// 2: không
        /// </summary>
        public int? sfcl { get; set; }

    }
}
