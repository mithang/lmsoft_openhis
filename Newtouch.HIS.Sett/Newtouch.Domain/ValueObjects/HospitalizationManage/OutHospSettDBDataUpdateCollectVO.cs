using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Quyết toán xuất viện Gửi quyết toán
    /// </summary>
    public class OutHospSettDBDataUpdateCollectVO
    {
        /// <summary>
        /// Quyết toán nội trú
        /// </summary>
        public HospSettlementEntity zy_js { get; set; }

        /// <summary>
        /// Chi tiết quyết toán nội trú
        /// </summary>
        public IList<HospSettlementDetailEntity> zy_jsmxList { get; set; }

        /// <summary>
        /// Quyết toán giao dịch nội trú
        /// </summary>
        public IList<HospTransactionSettlementEntity> zy_jyjsList { get; set; }

        /// <summary>
        /// Chi tiết quyết toán giao dịch nội trú
        /// </summary>
        public IList<HospTransactionSettlementDetailEntity> zy_jyjsmxList { get; set; }

        /// <summary>
        /// Phương thức thanh toán quyết toán nội trú
        /// </summary>
        public IList<HospSettlementPaymentModelEntity> zy_jszffsList { get; set; }

        /// <summary>
        /// Bản ghi thu chi tài khoản bệnh nhân hệ thống
        /// </summary>
        public IList<SysPatientAccountRevenueAndExpenseEntity> xt_brzhszjlList { get; set; }

        /// <summary>
        /// Loại lớn quyết toán nội trú
        /// </summary>
        public IList<HospSettlementCategoryEntity> zy_jsdlList { get; set; }

        //update Thông tin cơ bản bệnh nhân nội trú
        //insert Thay đổi thông tin cơ bản bệnh nhân nội trú
        //update Hóa đơn tài chính Cập nhật số hóa đơn hiện tại

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
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

    }

}
