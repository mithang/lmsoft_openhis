using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Tài khoản bệnh nhân hệ thống
    /// Dùng cho:Cá nhân(Bao gồm bệnh nhân từ thiện),Tài khoản đơn vị v.v.,Bảng tạm ứng
    /// </summary>
    [Table("xt_brzh")]
    public class SysPatientAccountEntity : IEntity<SysPatientAccountEntity>
    {
        /// <summary>
        /// Mã tài khoản
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int zhbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public int zh { get; set; }

        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// Tài khoản đơn vị brnm = 0
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Chủ tài khoản
        /// </summary>
        public string zhsyr { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Ngày kích hoạt
        /// </summary>
        public DateTime qyrq { get; set; }

        /// <summary>
        /// Ngày chấm dứt
        /// </summary>
        public DateTime zzrq { get; set; }

        /// <summary>
        /// Số dư tài khoản
        /// Trường này không lưu số dư,Nếu muốn tra cứu số dư,thì tra bảngxt_brzhszjlTra cứu bản ghi cuối cùng   --------------------------------------      Tạm dùng để xử lý bệnh nhân từ thiện   -1 Thể hiện không cần kiểm tra số dư      
        /// </summary>
        public decimal zhye { get; set; }

        /// <summary>
        /// Tính chất tài khoản
        /// 0 Tài khoản đơn vị   1 Tài khoản cá nhân,   2 Tài khoản giường gia đình,   3 Tài khoản tạm ứng nội trú,   4 Tài khoản trợ giúp khó khăn      --2007.4.10 Tài khoản tương ứng với nhiều tính chất,Xử lý riêng,Không ảnh hưởng lẫn nhau      -- 2 Tài khoản giường gia đình 2006.11.19  Không lập riêng tài khoản giường gia đình,Khấu trừ từ tài khoản cá nhân,Thông qua"Bản ghi thu chi tài khoản"trong"Tính chất thu chi"Phân biệt   
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// Ghi chú tài khoản
        /// Chẩn đoán bệnh nhân giường gia đình
        /// </summary>
        public string zhbz { get; set; }

        /// <summary>
        /// Trạng thái
        /// 0 Không hiệu lực 1 Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Số nhập viện liên kết
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

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
        /// Tên người tạo
        /// </summary>
        public string CreatorUserName { get; set; }

    }
}
