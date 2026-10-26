using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Tài khoản bệnh nhân
    /// </summary>
    [Table("xt_zh")]
    public class SysAccountEntity : IEntity<SysAccountEntity>
    {
        /// <summary>
        /// Mã tài khoản
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public int zhCode { get; set; }

        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// Tài khoản đơn vị brnm = 0
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Số dư tài khoản
        /// Trường này không lưu số dư,Nếu muốn tra cứu số dư,thì tra bảngxt_brzhszjlTra cứu bản ghi cuối cùng   --------------------------------------      Tạm dùng để xử lý bệnh nhân từ thiện   -1 Thể hiện không cần kiểm tra số dư      
        /// </summary>
        public decimal zhye { get; set; }

        /// <summary>
        /// enumzhxz
        public int? zhxz { get; set; }

        /// <summary>
        /// Trạng thái
        /// 0 Không hiệu lực 1 Hiệu lực
        /// </summary>
        public string zt { get; set; }

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

    }
}
