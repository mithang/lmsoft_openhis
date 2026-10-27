using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin lãi lỗ thuốc
    /// </summary>
    public class YpSyxxVo
    {
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số hiệu mã nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// Giá trị mặc địnhGetdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// Số dương：Báo thừa，Số âm：Báo hỏng  Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// Giá bán buôn
        /// </summary>
        public decimal Pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal Lsj { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal Yklsj { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// Lý do lãi lỗ
        /// </summary>
        public string Syyy { get; set; }

        /// <summary>
        /// Người chịu trách nhiệm
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// Tồn kho còn lại
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal Jj { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }
    }
}
