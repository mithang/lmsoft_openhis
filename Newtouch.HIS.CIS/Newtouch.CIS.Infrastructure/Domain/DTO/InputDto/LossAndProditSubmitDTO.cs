using System;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// lãi lỗGửi
    /// </summary>
    public class LossAndProditSubmitDTO
    {
        /// <summary>
        /// lãi lỗtình hình
        /// </summary>
        public string syqk { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtsố
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// nguyên nhân lãi lỗ
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// nguyên nhân lãi lỗID
        /// </summary>
        public string syyyId { get; set; }

        /// <summary>
        /// lãi lỗCờ
        /// </summary>
        public string sybz { get; set; }

        /// <summary>
        /// phiếusố phiếu
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// vật tưTên
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// vàogiá  chuyểnhóavìcontương ứngvàogiá
        /// </summary>
        public decimal Jj { get; set; }

        /// <summary>
        /// Giá bán lẻ  chuyểnhóavìcontương ứngvàogiá
        /// </summary>
        public decimal Lsj { get; set; }

        /// <summary>
        /// tổng bán lẻ
        /// </summary>
        public decimal Lsjze { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtTên
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
        /// Trách nhiệm ngườiTên
        /// </summary>
        public string zrrmc { get; set; }

        /// <summary>
        /// còn lạiTồn kho hiệncóTồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Đơn vị giáôĐơn vị,vàzhyzmộtthẳng
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// Số lượng,Khoa phòngĐơn vị
        /// </summary>
        public int bmsl { get; set; }

        /// <summary>
        /// Số lượng,Đơn vị nhỏ nhất
        /// </summary>
        public int zxsl { get; set; }

        /// <summary>
        /// Số lượng,mangĐơn vị  
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// jqgrid row id
        /// </summary>
        public int jqRowId { get; set; }
    }
}
