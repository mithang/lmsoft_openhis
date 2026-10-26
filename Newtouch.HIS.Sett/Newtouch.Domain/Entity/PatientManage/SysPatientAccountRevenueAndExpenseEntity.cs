using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bản ghi thu chi tài khoản bệnh nhân hệ thống
    /// </summary>
    [Table("xt_brzhszjl")]
    public class SysPatientAccountRevenueAndExpenseEntity : IEntity<SysPatientAccountRevenueAndExpenseEntity>
    {
        /// <summary>
        /// Số bản ghi thu chi tài khoản
        /// </summary>
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int zhszjlbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Số tiền thu chi
        /// </summary>
        public decimal szje { get; set; }

        /// <summary>
        /// Số dư tài khoản
        /// </summary>
        public decimal zhye { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// Tính chất thu chi
        /// 0 Nạp tiền ngoại trú,Rút tiền(Gửi/rút tài khoản)      1 Ghi sổ giường gia đình,Trả lại(Ghi sổ giường gia đình)   2 Quyết toán ngoại trú,Trả lại(Quyết toán ngoại trú??,Bao gồm quyết toán giường gia đình)      3 Ghi sổ khu bệnh,Trả lại(Ghi sổ khu bệnh)   4 Quyết toán nội trú,Trả lại(Quyết toán nội trú)Quyết toán nội trú sẽ không phát sinh chi phí,Vì vậy thu chi ở đây là để bù đắp tài khoản âm do ghi sổ khu bệnh gây ra(Số dư tài khoản là số dương,Thì không cần thao tác)      5 Nạp tiền nội trú,Rút tiền(Gửi/rút tài khoản)   6 Khoản bổ sung nội trú   
        /// </summary>
        public string szxz { get; set; }

        /// <summary>
        /// Phương thức thanh toán tiền mặt Mã
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int? jsnm { get; set; }

        /// <summary>
        /// Số nhập viện liên kết
        /// Giường gia đình thì là số giường gia đình   Nội trú thì là số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Họ tên người tạo
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
