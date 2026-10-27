using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class HandOutMedicinesrmVO
    {
        public string zt { get; set; }

        public string ypmc { get; set; }
        public string py { get; set; }

        public string ypgg { get; set; }

        public string ycmc { get; set; }

        public string dlmc { get; set; }

        /// <summary>
        /// Số lượng đóng gói
        /// </summary>
        public decimal bzs { get; set; }

        /// <summary>
        /// Đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Đơn vị bộ phận
        /// </summary>
        public string deptdw { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Chuyển thành tổng Số lượng khả dụng đơn vị bộ phận
        /// </summary>
        public int? bmdwsl { get; set; }

        /// <summary>
        /// Đơn vị bộ phận
        /// </summary>
        public string bmdw { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }
        
        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypcode { get; set; }

        /// <summary>
        /// Giá bán buôn bộ phận
        /// </summary>
        public decimal pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ bộ phận
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string klsl { get; set; }

        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Số lượng đóng băng
        /// </summary>
        public int? djsl { get; set; }

        /// <summary>
        /// Số lượng tồn kho khả dụng
        /// </summary>
        public int? kykcsl { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// Số lượng đơn vị đóng gói
        /// </summary>
        public int? bzdwsl { get; set; }

        /// <summary>
        /// Số lượng đơn vị đóng gói kèm đơn vị
        /// </summary>
        public string bzdwslstr { get; set; }

        /// <summary>
        /// Đơn vị kho thuốc/Đơn vị đóng gói
        /// </summary>
        public string ykdw { get; set; }

        /// <summary>
        /// Giá nhập Đơn vị bộ phận
        /// </summary>
        public decimal? jj { get; set; }
    }
}
