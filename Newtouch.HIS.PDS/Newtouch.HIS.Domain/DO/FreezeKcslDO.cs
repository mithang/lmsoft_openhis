using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DO
{
    /// <summary>
    /// tồn kho bị đóng băng thông tin
    /// </summary>
    public class FreezeKcslDO
    {

        /// <summary>
        /// nhà thuốc bị đóng băng/mã kho
        /// </summary>
        public string YfbmCode { get; set; }

        /// <summary>
        /// thông tin thuốc bị đóng băng
        /// </summary>
        public List<FreezeKcslDetailDO> Detail { get; set; }

        /// <summary>
        /// người thao tác mã
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// tổ chức mã
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// thông tin thuốc bị đóng băng
    /// </summary>
    public class FreezeKcslDetailDO
    {

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string Ypmc { get; set; }

        /// <summary>
        /// 需要冻结药品总数量 (đơn vị nhỏ nhất)
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? Yxq { get; set; }
    }


    /// <summary>
    /// tồn kho bị đóng băng thông tin
    /// </summary>
    public class UnFreezeKcslDO
    {

        /// <summary>
        /// nhà thuốc bị đóng băng/mã kho
        /// </summary>
        public string YfbmCode { get; set; }

        /// <summary>
        /// hủy đóng băng thông tin thuốc
        /// </summary>
        public List<UnFreezeKcslDetailDO> Detail { get; set; }

        /// <summary>
        /// người thao tác mã
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// tổ chức mã
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// hủy đóng băng thông tin thuốc
    /// </summary>
    public class UnFreezeKcslDetailDO
    {

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// tổng số lượng thuốc cần hủy đóng băng (đơn vị nhỏ nhất)
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string Ph { get; set; }
    }

    public class ReturningInventoryDO
    {
        /// <summary>
        /// cần tăng tồn kho của bộ phận nhà thuốc mã
        /// </summary>
        public string NeedAddInventoryYfbmCode { get; set; }

        /// <summary>
        /// cần giảm tồn kho của bộ phận nhà thuốc mã
        /// </summary>
        public string NeedSubtractInventoryYfbmCode { get; set; }

        /// <summary>
        /// 需要还的库存明细
        /// </summary>
        public List<InventoryDetailDO> Detail { get; set; }

        /// <summary>
        /// người thao tác mã
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// tổ chức mã
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// tồn kho chi tiết
    /// </summary>
    public class InventoryDetailDO
    {

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// tổng số lượng thuốc cần hủy đóng băng (đơn vị nhỏ nhất)
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string Ph { get; set; }

    }
}
