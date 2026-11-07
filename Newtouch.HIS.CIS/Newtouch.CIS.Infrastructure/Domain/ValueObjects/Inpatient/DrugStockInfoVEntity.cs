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
        /// Hạn hiệu lực
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Tồn kho Số lượng
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// Khoa phòng Số lượng
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// mang Đơn vị Khoa phòng Số lượng
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// mang Đơn vị Khoa phòng Tồn kho Số lượng
        /// </summary>
        public string kcslStr { get; set; }

        /// <summary>
        /// mang Đơn vị Khoa phòngđôngkết Số lượng
        /// </summary>
        public string djslStr { get; set; }

        /// <summary>
        /// góilắpsố Đơn vị
        /// </summary>
        public int? bzdwsl { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// góilắpsố Đơn vị
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// góilắpsố
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// Khoa phòng Giá bán buôn
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// lôpháttổngmức
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        /// Khoa phòng Giá bán lẻ
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất Giá bán lẻ
        /// </summary>
        public decimal? zxdwlsj { get; set; }

        /// <summary>
        /// tổng bán lẻ
        /// </summary>
        public decimal? lsze { get; set; }

        /// <summary>
        /// vàogiátổngmức
        /// </summary>
        public decimal? jjze { get; set; }

        /// <summary>
        /// Kho thuốc Giá bán buôn
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// Kho thuốc Giá bán lẻ
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
        /// Tên nhà sản xuất
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// có thểdùng Tồn kho  Đơn vị nhỏ nhất
        /// </summary>
        public int? kykc { get; set; }

        /// <summary>
        /// Số đăng ký
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Giá bán lẻ Đơn giá Đơn vị
        /// </summary>
        public string lsjdjdw { get; set; }

        /// <summary>
        /// lôphát Đơn giá Đơn vị
        /// </summary>
        public string pfjdjdw { get; set; }

        /// <summary>
        /// vàogiá Đơn giá Đơn vị
        /// </summary>
        public string jjdjdw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtvàogiá
        /// </summary>
        public decimal? zxdwjj { get; set; }

        /// <summary>
        /// Đơn vị đóng góivàogiá
        /// </summary>
        public decimal? bzdwjj { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vịvàogiá
        /// </summary>
        public decimal? bmdwjj { get; set; }

        /// <summary>
        /// Tồn kho Trạng thái  0-Vô hiệu  1-Hiệu lực
        /// </summary>
        public string kczt { get; set; }

        /// <summary>
        /// Nhà cung cấp Tên
        /// </summary>
        public string gysmc { get; set; }
    }
}
