using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi phí nội trú theodlNhóm
    /// </summary>
    public class HospFeeChargeCategoryGroupVO
    {
        /// <summary>
        /// Mã loại lớn thu phí
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Tên loại lớn thu phí
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }
    }
    public class HospFeeChargeCategoryGroupDetailVO : HospFeeChargeCategoryGroupVO
    {
        public string tdrq { get; set; }
        public string sfxm { get; set; }
        public string sfxmmc { get; set; }
        public decimal dj { get; set; }
        public decimal? zje { get; set; }
        public decimal sl { get; set; }
        public string jfdw { get; set; }
        /// <summary>
        /// Tính chất tự chi trả
        /// </summary>
        public string zfxz { get; set; }
        public string gg { get; set; }
        public string zzfbz { get; set; }
    }
    /// <summary>
    /// Tải lên chi phí
    /// </summary>
    public class HospFeeUploadDetailVO
    {
        public string jfbbh { get; set; }
        public int cxzyjfbbh { get; set; }
        public DateTime tdrq { get; set; }
        public string yp { get; set; }
        public string ys { get; set; }
        public string ysmc { get; set; }
        public string ks { get; set; }
        public string ksmc { get; set; }
        public decimal dj { get; set; }
        public decimal sl { get; set; }
        public decimal je { get; set; }
        public string jfdw { get; set; }
        public string ypmc { get; set; }
        public string gjybdm { get; set; }
        public string ybdm { get; set; }
        /// <summary>
        /// Tính chất tự chi trả 4:Giáp 5:đã 6:Bính 1:Tự chi trả
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// Trạng thái tải lên BHYT 1: Đã tải lên  0:Chưa tải lên
        /// </summary>
        public string ybsczt { get; set; }
        public string zzfbz { get; set; }
        /// <summary>
        /// Trong bảng bản ghi tải lên chi phíjfbbh
        /// </summary>
        public string feedetl_sn { get; set; }
        public string dlmc { get; set; }
        /// <summary>
        /// Bảng tham số đầu vàozyh
        /// </summary>
        public string zyh { get; set; }
    }
    public class InpatIentFeeSumVo
    {
        public decimal zje { get; set; }
    }

    public class InpatIentFeeInfo
    {
        public decimal zje { get; set; }
        public DateTime cyrq { get; set; }


    }
    public class InpatIentFeeObj
    {
        public decimal zje { get; set; }
        public decimal ybzje { get; set; }
        public DateTime cyrq { get; set; }
    }
}
