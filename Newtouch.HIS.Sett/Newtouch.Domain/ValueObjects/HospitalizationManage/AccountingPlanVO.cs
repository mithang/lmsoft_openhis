using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    /// <summary>
    /// Tra cứu kế hoạch ghi sổ Dùng chung ngoại trú/nội trú
    /// </summary>
    public class AccountingPlanVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? startDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? endDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zxzt { get; set; }

        /// <summary>
        /// Số ngoại trú/nội trú
        /// </summary>
        public string mzzyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zcs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? yzxcs { get; set; }

        /// <summary>
        /// Số lần còn lại
        /// </summary>
        public int? sycs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Thời gian hệ thống thực hiện lần cuối(Thao tác trên trang của chúng ta'Thực hiện'Thời gian của)
        /// </summary>
        public DateTime? zhxtzxsj { get; set; }

        /// <summary>
        /// Số lượng(Được quy đổi từ liều điều trị mỗi lần)
        /// </summary>
        public int? sl { get; set; }
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// Tính chất y lệnh 1Tạm thời 2Dài hạn
        /// </summary>
        public string yzxz { get; set; }
    }
}
