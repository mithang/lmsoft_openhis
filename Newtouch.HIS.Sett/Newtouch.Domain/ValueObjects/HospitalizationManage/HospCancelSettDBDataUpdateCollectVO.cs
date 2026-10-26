using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class HospCancelSettDBDataUpdateCollectVO
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Trạng thái đang nằm viện
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// Ngày xuất viện
        /// </summary>
        public DateTime? cyrq { get; set; }

        /// <summary>
        /// Hủy quyết toán
        /// </summary>
        public HospSettlementEntity cxjs { get; set; }

        /// <summary>
        /// Danh sách chi tiết hủy quyết toán
        /// </summary>
        public IList<HospSettlementDetailEntity> cx_zyjsmxList { get; set; }

        /// <summary>
        /// Hủy Bản ghi thu chi tài khoản bệnh nhân hệ thống
        /// </summary>
        public IList<SysPatientAccountRevenueAndExpenseEntity> cx_xt_brzhszjlList { get; set; }

        /// <summary>
        /// Hủy Phương thức thanh toán quyết toán nội trú
        /// </summary>
        public IList<HospSettlementPaymentModelEntity> cx_zy_jszffsList { get; set; }



    }

}
