using System;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// lãi lỗ Gửi
    /// </summary>
    public class LossAndProditSubmitDTO
    {
        /// <summary>
        /// lãi lỗ tình hình
        /// </summary>
        public string syqk { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất số
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// chuyển hóa vì con
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// nguyên nhân lãi lỗ
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// nguyên nhân lãi lỗ ID
        /// </summary>
        public string syyyId { get; set; }

        /// <summary>
        /// lãi lỗ Cờ
        /// </summary>
        public string sybz { get; set; }

        /// <summary>
        /// phiếu số phiếu
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// vật tư Tên
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// vào giá  chuyển hóa vì con tương ứng vào giá
        /// </summary>
        public decimal Jj { get; set; }

        /// <summary>
        /// Giá bán lẻ  chuyển hóa vì con tương ứng vào giá
        /// </summary>
        public decimal Lsj { get; set; }

        /// <summary>
        /// tổng bán lẻ
        /// </summary>
        public decimal Lsjze { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất Tên
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public DateTime Yxq { get; set; }

        /// <summary>
        /// Trách nhiệm người
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// Trách nhiệm người Tên
        /// </summary>
        public string zrrmc { get; set; }

        /// <summary>
        /// còn lại Tồn kho hiện có Tồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Đơn vị giáô Đơn vị,và zhyz một thẳng
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// Số lượng,Khoa phòng Đơn vị
        /// </summary>
        public int bmsl { get; set; }

        /// <summary>
        /// Số lượng,Đơn vị nhỏ nhất
        /// </summary>
        public int zxsl { get; set; }

        /// <summary>
        /// Số lượng,mang Đơn vị  
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// jqgrid row id
        /// </summary>
        public int jqRowId { get; set; }
    }
}
