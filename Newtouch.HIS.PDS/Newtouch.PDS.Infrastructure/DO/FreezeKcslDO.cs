using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DO
{
    /// <summary>
    /// Thông tin tồn kho đóng băng
    /// </summary>
    public class FreezeKcslDO
    {

        /// <summary>
        /// Nhà thuốc bị đóng băng/Mã kho
        /// </summary>
        public string YfbmCode { get; set; }

        /// <summary>
        /// Thông tin thuốc bị đóng băng
        /// </summary>
        public List<FreezeKcslDetailDO> Detail { get; set; }

        /// <summary>
        /// Mã người thao tác
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// Thông tin thuốc bị đóng băng
    /// </summary>
    public class FreezeKcslDetailDO
    {

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string Ypmc { get; set; }

        /// <summary>
        /// Tổng Số lượng thuốc cần đóng băng（Đơn vị nhỏ nhất）
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? Yxq { get; set; }
    }


    /// <summary>
    /// Thông tin tồn kho đóng băng
    /// </summary>
    public class UnFreezeKcslDO
    {

        /// <summary>
        /// Nhà thuốc bị đóng băng/Mã kho
        /// </summary>
        public string YfbmCode { get; set; }

        /// <summary>
        /// Hủy đóng băng thông tin thuốc
        /// </summary>
        public List<UnFreezeKcslDetailDO> Detail { get; set; }

        /// <summary>
        /// Mã người thao tác
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// Hủy đóng băng thông tin thuốc
    /// </summary>
    public class UnFreezeKcslDetailDO
    {

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// Tổng số lượng thuốc cần hủy đóng băng（Đơn vị nhỏ nhất）
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }
    }

    public class ReturningInventoryDO
    {
        /// <summary>
        /// Mã bộ phận nhà thuốc cần tăng tồn kho
        /// </summary>
        public string NeedAddInventoryYfbmCode { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc cần giảm tồn kho
        /// </summary>
        public string NeedSubtractInventoryYfbmCode { get; set; }

        /// <summary>
        /// Chi tiết tồn kho cần trả
        /// </summary>
        public List<InventoryDetailDO> Detail { get; set; }

        /// <summary>
        /// Mã người thao tác
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// Chi tiết tồn kho
    /// </summary>
    public class InventoryDetailDO
    {

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// Tổng số lượng thuốc cần hủy đóng băng（Đơn vị nhỏ nhất）
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

    }
}
