using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class DrugStockInfoVEntity
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Tên nhóm lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Tên nhóm lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// Số lượng khoa phòng
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// Số lượng khoa phòng kèm đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Số lượng tồn kho khoa phòng kèm đơn vị
        /// </summary>
        public string kcslStr { get; set; }

        /// <summary>
        /// Số lượng đóng băng khoa phòng kèm đơn vị
        /// </summary>
        public string djslStr { get; set; }

        /// <summary>
        /// Đơn vị số lượng đóng gói
        /// </summary>
        public int? bzdwsl { get; set; }

        /// <summary>
        /// Đơn vị khoa phòng
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Đơn vị số lượng đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Số lượng đóng gói
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// Giá bán sỉ khoa phòng
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// Tổng số tiền bán sỉ
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        /// Giá bán lẻ của khoa phòng
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwlsj { get; set; }

        /// <summary>
        /// Tổng số tiền bán lẻ
        /// </summary>
        public decimal? lsze { get; set; }

        /// <summary>
        /// Tổng số tiền giá nhập
        /// </summary>
        public decimal? jjze { get; set; }

        /// <summary>
        /// Giá bán sỉ kho thuốc
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal? yklsj { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Tên nhà máy dược
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Tồn kho khả dụng  Đơn vị nhỏ nhất
        /// </summary>
        public int? kykc { get; set; }

        /// <summary>
        /// Số giấy phép
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Đơn vị đơn giá bán lẻ
        /// </summary>
        public string lsjdjdw { get; set; }

        /// <summary>
        /// Đơn vị đơn giá bán sỉ
        /// </summary>
        public string pfjdjdw { get; set; }

        /// <summary>
        /// Đơn vị đơn giá nhập
        /// </summary>
        public string jjdjdw { get; set; }

        /// <summary>
        /// Giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwjj { get; set; }

        /// <summary>
        /// Giá nhập đơn vị đóng gói
        /// </summary>
        public decimal? bzdwjj { get; set; }

        /// <summary>
        /// Giá nhập đơn vị khoa phòng
        /// </summary>
        public decimal? bmdwjj { get; set; }

        /// <summary>
        /// Trạng thái tồn kho  0-Vô hiệu  1-Có hiệu lực
        /// </summary>
        public string kczt { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }
    }
}
