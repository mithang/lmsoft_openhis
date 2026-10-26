using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// thuốc thông tin lãi/lỗ
    /// </summary>
    public class YpSyxxVo
    {
        /// <summary>
        /// tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// nhà thuốc mã số hiệu
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// giá trị mặc định Getdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// đang số: báo thừa, số âm: báo hỏng  số lượng đơn vị nhỏ nhất
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// giá bán buôn
        /// </summary>
        public decimal Pfj { get; set; }

        /// <summary>
        /// giá bán lẻ
        /// </summary>
        public decimal Lsj { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        public decimal Yklsj { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// nguyên nhân lãi/lỗ
        /// </summary>
        public string Syyy { get; set; }

        /// <summary>
        /// người chịu trách nhiệm
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// số chứng từ
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// tồn kho còn lại
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// giá nhập
        /// </summary>
        public decimal Jj { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        public string remark { get; set; }
    }
}
