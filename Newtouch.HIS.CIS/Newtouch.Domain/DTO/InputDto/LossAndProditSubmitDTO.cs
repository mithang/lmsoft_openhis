using System;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Gửi chênh lệch
    /// </summary>
    public class LossAndProditSubmitDTO
    {
        /// <summary>
        /// Tình hình chênh lệch
        /// </summary>
        public string syqk { get; set; }

        /// <summary>
        /// Số đơn vị nhỏ nhất
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Nguyên nhân chênh lệch
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// Nguyên nhân chênh lệchID
        /// </summary>
        public string syyyId { get; set; }

        /// <summary>
        /// Cờ chênh lệch
        /// </summary>
        public string sybz { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// Vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Giá nhập  Giá nhập tương ứng với hệ số chuyển đổi
        /// </summary>
        public decimal Jj { get; set; }

        /// <summary>
        /// Giá bán lẻ  Giá nhập tương ứng với hệ số chuyển đổi
        /// </summary>
        public decimal Lsj { get; set; }

        /// <summary>
        /// Tổng số tiền bán lẻ
        /// </summary>
        public decimal Lsjze { get; set; }

        /// <summary>
        /// Tên đơn vị nhỏ nhất
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
        /// Hạn sử dụng
        /// </summary>
        public DateTime Yxq { get; set; }

        /// <summary>
        /// Người chịu trách nhiệm
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// Tên người chịu trách nhiệm
        /// </summary>
        public string zrrmc { get; set; }

        /// <summary>
        /// Tồn kho còn lại Tồn kho hiện có Đơn vị nhỏ nhất
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Đơn vị Đơn vị giá,vàzhyzluôn
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// Số lượng,Đơn vị khoa phòng
        /// </summary>
        public int bmsl { get; set; }

        /// <summary>
        /// Số lượng,Đơn vị nhỏ nhất
        /// </summary>
        public int zxsl { get; set; }

        /// <summary>
        /// Số lượng,Kèm đơn vị  
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// jqgrid row id
        /// </summary>
        public int jqRowId { get; set; }
    }
}
