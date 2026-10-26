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
        /// số lượng đóng gói
        /// </summary>
        public decimal bzs { get; set; }

        /// <summary>
        /// đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// đơn vị bộ phận
        /// </summary>
        public string deptdw { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 转换成部门单位的总可用数量
        /// </summary>
        public int? bmdwsl { get; set; }

        /// <summary>
        /// đơn vị bộ phận
        /// </summary>
        public string bmdw { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }
        
        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypcode { get; set; }

        /// <summary>
        /// giá bán buôn bộ phận
        /// </summary>
        public decimal pfj { get; set; }

        /// <summary>
        /// giá bán lẻ bộ phận
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string klsl { get; set; }

        /// <summary>
        /// số lượng tồn kho
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// số lượng đóng băng
        /// </summary>
        public int? djsl { get; set; }

        /// <summary>
        /// tồn kho khả dụng số lượng
        /// </summary>
        public int? kykcsl { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// đơn vị đóng gói số lượng
        /// </summary>
        public int? bzdwsl { get; set; }

        /// <summary>
        /// kèm đơn vị của đơn vị đóng gói số lượng
        /// </summary>
        public string bzdwslstr { get; set; }

        /// <summary>
        /// đơn vị kho thuốc/đơn vị đóng gói
        /// </summary>
        public string ykdw { get; set; }

        /// <summary>
        /// giá nhập đơn vị bộ phận
        /// </summary>
        public decimal? jj { get; set; }
    }
}
